using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ABI.Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <summary>
/// A base type to implement packaged and easy to use <see cref="ICanvasImage"/>-based effects that can be used with Win2D.
/// </summary>
public abstract partial class CanvasEffect : ICanvasImage, ICanvasImageInterop.Interface
{
    /// <summary>
    /// The mapping of registered transform nodes for the current effect graph.
    /// </summary>
    /// <remarks>
    /// When not empty (ie. when an effect graph has been built), this will always also include <see cref="canvasImage"/>.
    /// </remarks>
    private readonly Dictionary<object, ICanvasImage> transformNodes = new();

    /// <summary>
    /// The current cached result for <see cref="GetCanvasImage"/>, if available.
    /// </summary>
    private ICanvasImage? canvasImage;

    /// <summary>
    /// Indicates the current invalidation state for the effect graph (which controls the logic in <see cref="GetCanvasImage"/>).
    /// </summary>
    private InvalidationType invalidationType = InvalidationType.Creation;

    /// <summary>
    /// Indicates whether the effect is disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// Builds the effect graph for the current <see cref="CanvasEffect"/> instance, and configures all effect nodes, as well as the output
    /// node for the graph. That <see cref="ICanvasImage"/> instance will then be passed to Win2D to perform the actual drawing, when needed.
    /// </summary>
    /// <param name="effectGraph">The input <see cref="EffectGraph"/> value to use to build the effect graph.</param>
    /// <remarks>
    /// <para>
    /// This method is called once before the current effect is drawn, and the resulting image is automatically cached and reused.
    /// It will remain in use until <see cref="InvalidateEffectGraph"/> is called with <see cref="InvalidationType.Creation"/>.
    /// </para>
    /// <para>
    /// If the effect is invalidated with <see cref="InvalidationType.Update"/>, only <see cref="ConfigureEffectGraph"/> will be
    /// called. As such, derived types should save any effect graph nodes that might need updates into instance fields, for later use.
    /// </para>
    /// <para>
    /// For instance, consider a <c>FrostedGlassEffect</c> type deriving from <see cref="CanvasEffect"/>, with this effect graph:
    /// <code>
    /// ┌────────┐   ┌──────┐   ┌──────┐   ┌───────┐   ┌────────┐
    /// │ source ├──►│ blur ├──►│ tint ├──►│ noise ├──►│ output │
    /// └────────┘   └──────┘   └──────┘   └───────┘   └────────┘
    /// </code>
    /// </para>
    /// <para>
    /// In this example, <c>blur</c> is a <see cref="Microsoft.Graphics.Canvas.Effects.GaussianBlurEffect"/> instance, <c>tint</c>
    /// is a <see cref="Microsoft.Graphics.Canvas.Effects.TintEffect"/>. and <c>noise</c> is some other custom effect. In this case,
    /// the <see cref="ICanvasImage"/> instance returned by <see cref="BuildEffectGraph"/> will be <c>noise</c>, as that's the output
    /// node for the effect graph. At the same time, both <c>blur</c> and <c>tint</c> will also need to be saved as instance fields,
    /// so that <see cref="ConfigureEffectGraph"/> will be able to set properties on them when needed. For instance, the effect might
    /// expose a property to control the blur amount, as well as the tint color and opacity.
    /// </para>
    /// <para>
    /// Generally speaking, an implementation of <see cref="BuildEffectGraph"/> will consist of these steps:
    /// <list type="bullet">
    ///   <item>Create an instance of all necessary nodes in the effect graph.</item>
    ///   <item>Connect the effect nodes as needed to build the connected graph.</item>
    ///   <item>Register all effects as effect nodes in the graph, including the output node.</item>
    /// </list>
    /// </para>
    /// <para>
    /// For convenience, it is recommended to store all necessary <see cref="EffectNode{T}"/> objects in <see langword="static"/>
    /// <see langword="readonly"/> fields, so they can easily be accessed from <see cref="BuildEffectGraph"/> and <see cref="ConfigureEffectGraph"/>.
    /// </para>
    /// <para>
    /// This method should never be called directly. It is automatically invoked when an effect graph is needed.
    /// </para>
    /// </remarks>
    protected abstract void BuildEffectGraph(EffectGraph effectGraph);

    /// <summary>
    /// Configures the current effect graph whenever it is invalidated.
    /// </summary>
    /// <param name="effectGraph">The input <see cref="EffectGraph"/> value to use to configure the effect graph.</param>
    /// <remarks>
    /// <para>
    /// This method is guaranteed to be called after <see cref="BuildEffectGraph"/> has been invoked already. As such, any instance
    /// fields that are set by <see cref="BuildEffectGraph"/> can be assumed to never be <see langword="null"/> when this method runs.
    /// </para>
    /// <para>
    /// This method should never be called directly. It is used internally to configure the current effect graph.
    /// </para>
    /// </remarks>
    protected abstract void ConfigureEffectGraph(EffectGraph effectGraph);

    /// <summary>
    /// Invalidates the last returned result from <see cref="GetCanvasImage"/>.
    /// </summary>
    /// <param name="invalidationType">The invalidation type to request.</param>
    /// <remarks>
    /// <para>
    /// This method is used to signal when the effect graph should be updated, and it can indicate
    /// that either the entire graph should be created again (by calling <see cref="GetCanvasImage"/>),
    /// or whether it simply needs to refresh its internal state (by calling <see cref="ConfigureEffectGraph"/>).
    /// </para>
    /// <para>
    /// If <see cref="InvalidateEffectGraph"/> is called with <see cref="InvalidationType.Update"/>, the effect
    /// graph will only be refreshed the next time the image is actually requested. That is, repeated requests
    /// for updates do not result in unnecessarily calls to <see cref="ConfigureEffectGraph"/>.
    /// </para>
    /// </remarks>
    protected void InvalidateEffectGraph(InvalidationType invalidationType = InvalidationType.Update)
    {
        lock (this.transformNodes)
        {
            // This method only updates the invalidation type. The next time the effect is drawn,
            // the current graph (if existing) will be disposed automatically before building a
            // new one. If the invalidation mode just needs an update, no disposal will happen.
            // The new invalidation type is always set to the maximum between the current one and
            // the requested one. This means that eg. if the current invalidation type is creation,
            // and a property only needing an update is set, the creation request will persist.
            this.invalidationType = (InvalidationType)Math.Max((byte)this.invalidationType, (byte)invalidationType);
        }
    }

    /// <summary>
    /// Updates the backing storage for an effect property and checks if it has changed.
    /// If so, it invalidates the effect graph as well with the requested invalidation type.
    /// </summary>
    /// <typeparam name="T">The type of the property that changed.</typeparam>
    /// <param name="storage">The storage for the effect property value.</param>
    /// <param name="value">The new effect property value to set.</param>
    /// <param name="invalidationType">The invalidation type to request.</param>
    protected void SetAndInvalidateEffectGraph<T>([NotNullIfNotNull(nameof(value))] ref T storage, T value, InvalidationType invalidationType = InvalidationType.Update)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return;
        }

        storage = value;

        InvalidateEffectGraph(invalidationType);
    }

    /// <summary>
    /// Disposes any disposable resources in the current instance.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method was called from <see cref="Dispose()"/> of from a finalizer:
    /// <list type="bullet">
    ///   <item><see langword="true"/>: the method was called from <see cref="Dispose()"/>.</item>
    ///   <item><see langword="false"/>: the method was called from a finalizer.</item>
    /// </list>
    /// </param>
    /// <remarks>
    /// <para>
    /// If implementing a type that derives from <see cref="CanvasEffect"/>, this method can be overridden to add logic to dispose
    /// any additional state. Managed objects should only be disposed when <paramref name="disposing"/> is <see langword="true"/>.
    /// </para>
    /// <para>
    /// If the derived type does not need a finalizer (ie. if it doesn't wrap any native resources), this method should not be
    /// called directly. It will be called automatically by <see cref="Dispose()"/>. If the derived type does instead define a
    /// finalizer, it should call <see cref="Dispose(bool)"/> with a value of <see langword="false"/>. As mentioned above, when
    /// implementing <see cref="Dispose(bool)"/> in a derived type, managed objects should only be disposed when
    /// <paramref name="disposing"/> is <see langword="true"/>, otherwise only unmanaged resorces should be disposed.
    /// </para>
    /// <para>
    /// Though the pattern does support this, it is recommended not to add finalizers in types deriving from <see cref="CanvasEffect"/>.
    /// Rather, if an effect needs to wrap native resources, it should do so via some same managed wrapper type (such as
    /// <see cref="System.Runtime.InteropServices.SafeHandle"/>), or some other custom unmanaged resource wrapper type.
    /// </para>
    /// </remarks>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose"/>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Only take a lock and dispose the image if the method was called explicitly from user code. If instead it was
            // called from the finalizer, we can make no guarantees on the order of objects being collected, meaning that
            // the lock itself might have already been collected by the time this code runs, which would lead the lock
            // statement to throw a NullReferenceException. So if a finalizer is running, just let objects be collected
            // on their own. This is fine here since there are no unmanaged references to free, but just managed wrappers.
            lock (this.transformNodes)
            {
                DisposeEffectGraph();
            }
        }

        this.isDisposed = true;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Indicates the invalidation type to request when invoking <see cref="InvalidateEffectGraph"/> and related methods.
    /// </summary>
    protected enum InvalidationType : byte
    {
        /// <summary>
        /// Invalidates the state of the effect graph, causing it to be configured again the next time it is used.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This will preserve the last returned <see cref="ICanvasImage"/> instance, if available, and it will only
        /// mark the internal state as being out of date, resulting in <see cref="ConfigureEffectGraph"/> to be called
        /// the next time the effect is used for drawing.
        /// </para>
        /// <para>
        /// This is much less expensive than creating the effect graph again, and should be preferred if possible.
        /// </para>
        /// </remarks>
        Update,

        /// <summary>
        /// Fully invalidates the effect graph, causing it to be created again the next time it is needed.
        /// </summary>
        /// <remarks>
        /// This will cause the last returned <see cref="ICanvasImage"/> instance to be disposed and discarded,
        /// and <see cref="BuildEffectGraph"/> to be called again the next time the effect is used for drawing.
        /// </remarks>
        Creation
    }
}