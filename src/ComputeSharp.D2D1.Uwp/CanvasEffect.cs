using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ABI.Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas;

#pragma warning disable CA1063

namespace ComputeSharp.D2D1.Uwp;

/// <summary>
/// A base type to implement packaged and easy to use <see cref="ICanvasImage"/>-based effects that can be used with Win2D.
/// </summary>
public abstract partial class CanvasEffect : ICanvasImage, ICanvasImageInterop.Interface
{
    /// <summary>
    /// Lock object used to synchronize calls to <see cref="ICanvasImageInterop"/> APIs.
    /// </summary>
    private readonly object lockObject = new();

    /// <summary>
    /// The current cached result for <see cref="GetCanvasImage"/>, if available.
    /// </summary>
    private ICanvasImage? canvasImage;

    /// <summary>
    /// Indicates whether the current state has been invalidated (requiring <see cref="ConfigureCanvasImage"/> to be called).
    /// </summary>
    /// <remarks>
    /// This is initially <see langword="true"/> so that <see cref="ConfigureCanvasImage"/> will always be called when
    /// first creating the image, even if the effect has not bee invalidated. This ensures default parameters are set.
    /// </remarks>
    private bool isInvalidated = true;

    /// <summary>
    /// Indicates whether the effect is disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// Creates the resulting <see cref="ICanvasImage"/> representing the output node of the effect graph for this <see cref="CanvasEffect"/> instance.
    /// </summary>
    /// <returns>The resulting <see cref="ICanvasImage"/> representing the output node of the effect graph for this <see cref="CanvasEffect"/> instance.</returns>
    /// <remarks>
    /// <para>
    /// This method is only called once and its result is cached until explicitly invalidated by calling <see cref="InvalidateCanvasImage"/> and related methods.
    /// </para>
    /// <para>
    /// This method should never be called directly. Instead, it is used internally to produce an <see cref="ICanvasImage"/> instance when the effect is used.
    /// </para>
    /// </remarks>
    protected abstract ICanvasImage CreateCanvasImage();

    /// <summary>
    /// Configures the current <see cref="ICanvasImage"/> instance when the effect is used.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method is guaranteed to be called after <see cref="CreateCanvasImage"/> has been invoked already.
    /// </para>
    /// <para>
    /// This method should never be called directly. Instead, it is used internally to configure the current <see cref="ICanvasImage"/> instance when the effect is used.
    /// </para>
    /// </remarks>
    protected abstract void ConfigureCanvasImage();

    /// <summary>
    /// Invalidates the last returned result from <see cref="GetCanvasImage"/>.
    /// </summary>
    /// <param name="invalidationType">The invalidation type to request.</param>
    /// <remarks>
    /// <para>
    /// This method is used to signal when the effect graph should be updated, and it can indicate
    /// that either the entire graph should be created again (by calling <see cref="GetCanvasImage"/>),
    /// or whether it simply needs to refresh its internal state (by calling <see cref="ConfigureCanvasImage"/>).
    /// </para>
    /// <para>
    /// If <see cref="InvalidateCanvasImage"/> is called with <see cref="InvalidationType.Update"/>, the effect
    /// graph will only be refreshed the next time the image is actually requested. That is, repeated requests
    /// for updates do not result in unnecessarily calls to <see cref="ConfigureCanvasImage"/>.
    /// </para>
    /// </remarks>
    protected void InvalidateCanvasImage(InvalidationType invalidationType = InvalidationType.Update)
    {
        lock (this.lockObject)
        {
            // If the effect graph should be created again, dispose the image and throw it away
            if (invalidationType == InvalidationType.Creation)
            {
                this.canvasImage?.Dispose();

                this.canvasImage = null;
                this.isInvalidated = false;
            }
            else
            {
                // Otherwise, just mark the internal state as not being valid anymore. The next
                // time the image is requested, the effect graph will be configured if needed.
                this.isInvalidated = true;
            }
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
    protected void SetAndInvalidateCanvasImage<T>([NotNullIfNotNull(nameof(value))] ref T storage, T value, InvalidationType invalidationType = InvalidationType.Update)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return;
        }

        storage = value;

        InvalidateCanvasImage(invalidationType);
    }

    /// <summary>
    /// Indicates the invalidation type to request when invoking <see cref="InvalidateCanvasImage"/> and related methods.
    /// </summary>
    protected enum InvalidationType
    {
        /// <summary>
        /// Fully invalidates the effect graph, causing it to be created again the next time it is needed.
        /// </summary>
        /// <remarks>
        /// This will cause the last returned <see cref="ICanvasImage"/> instance to be disposed and discarded,
        /// and <see cref="CreateCanvasImage"/> to be called again the next time the effect is used for drawing.
        /// </remarks>
        Creation,

        /// <summary>
        /// Invalidates the state of the effect graph, causing it to be configured again the next time it is used.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This will preserve the last returned <see cref="ICanvasImage"/> instance, if available, and it will only
        /// mark the internal state as being out of date, resulting in <see cref="ConfigureCanvasImage"/> to be called
        /// the next time the effect is used for drawing.
        /// </para>
        /// <para>
        /// This is much less expensive than creating the effect graph again, and should be preferred if possible.
        /// </para>
        /// </remarks>
        Update
    }
}