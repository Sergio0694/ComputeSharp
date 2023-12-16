using System;
using System.Runtime.CompilerServices;
using Microsoft.Graphics.Canvas;

namespace ComputeSharp.D2D1.WinUI;

/// <inheritdoc/>
partial class CanvasEffect
{
    /// <summary>
    /// An object representing an effect graph being built or configured.
    /// </summary>
    protected readonly ref struct EffectGraph
    {
        /// <summary>
        /// The owning <see cref="CanvasEffect"/> instance.
        /// </summary>
        private readonly CanvasEffect owner;

        /// <summary>
        /// Creates a new <see cref="EffectGraph"/> instance with the specified parameters.
        /// </summary>
        /// <param name="owner">The owning <see cref="CanvasEffect"/> instance.</param>
        internal EffectGraph(CanvasEffect owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Gets a previously registered <see cref="ICanvasImage"/> object associated with a given effect graph node.
        /// </summary>
        /// <param name="effectNode">The <see cref="IEffectNode"/> instance to use to lookup the <see cref="ICanvasImage"/> object to retrieve.</param>
        /// <returns>The <see cref="ICanvasImage"/> object associated with <paramref name="effectNode"/> in the effect graph.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="effectNode"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="effectNode"/> is not currently registered in the effect graph.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the current <see cref="EffectGraph"/> instance is not valid.</exception>
        public ICanvasImage GetNode(IEffectNode effectNode)
        {
            default(ArgumentNullException).ThrowIfNull(effectNode);
            default(InvalidOperationException).ThrowIf(this.owner is null);

            // Try to get the canvas image associated with the input effect node marker.
            // This must have been previously registered in a call to BuildEffectGraph.
            if (!this.owner.transformNodes.TryGetValue(effectNode, out ICanvasImage? canvasImage))
            {
                default(ArgumentException).Throw(nameof(effectNode), "The specified node is not registered in the effect graph.");
            }

            return canvasImage;
        }

        /// <summary>
        /// Gets a previously registered <typeparamref name="T"/> object (an <see cref="ICanvasImage"/> instance) associated with a given effect graph node.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ICanvasImage"/> object to retrieve.</typeparam>
        /// <param name="effectNode">The <see cref="IEffectNode{T}"/> instance to use to lookup the <see cref="ICanvasImage"/> object to retrieve.</param>
        /// <returns>The <typeparamref name="T"/> object associated with <paramref name="effectNode"/> in the effect graph.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="effectNode"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="effectNode"/> is not currently registered in the effect graph.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the current <see cref="EffectGraph"/> instance is not valid.</exception>
        public T GetNode<T>(IEffectNode<T> effectNode)
            where T : class, ICanvasImage
        {
            default(ArgumentNullException).ThrowIfNull(effectNode);
            default(InvalidOperationException).ThrowIf(this.owner is null);

            // Retrieve the registered canvas image, same as above
            if (!this.owner.transformNodes.TryGetValue(effectNode, out ICanvasImage? canvasImage))
            {
                default(ArgumentException).Throw(nameof(effectNode), "The specified node is not registered in the effect graph.");
            }

            // Return the T node (we can skip the expensive cast since this is guaranteed to be valid).
            // This is because EffectNode<T> being registered can only associate T instances with them.
            return Unsafe.As<T>(canvasImage);
        }

        /// <summary>
        /// Registers an <see cref="ICanvasImage"/> object in the effect graph.
        /// </summary>
        /// <param name="canvasImage">The <see cref="ICanvasImage"/> object to register in the effect graph.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="canvasImage"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the current <see cref="EffectGraph"/> instance is not valid or if the effect graph does not support modifications at this time.
        /// </exception>
        /// <remarks>
        /// <para><inheritdoc cref="RegisterNode{T}(EffectNode{T}, T)" path="/remarks/node()"/></para>
        /// <para>
        /// This method can be used when <paramref name="canvasImage"/> doesn't need to be retrieved from <see cref="ConfigureEffectGraph"/>, ie.
        /// when it has no properties that the current effect instance will need to mutate over it. Using this method still allows the effect to
        /// correctly track the image ownership, so that it can be disposed when the current effect instance is disposed.
        /// </para>
        /// <para>
        /// If performing lookups on <paramref name="canvasImage"/> is required, use the <see cref="RegisterNode{T}(EffectNode{T}, T)"/> overload.
        /// </para>
        /// </remarks>
        public void RegisterNode(ICanvasImage canvasImage)
        {
            default(ArgumentNullException).ThrowIfNull(canvasImage);
            default(InvalidOperationException).ThrowIf(this.owner is null);
            default(InvalidOperationException).ThrowIf(this.owner.invalidationType != InvalidationType.Creation, "The effect graph cannot be modified after its creation.");

            // Use a new dummy object as key to register the anonymous effect node. This is cheaper than having to maintain a different
            // data structure (eg. a HashSet<ICanvasImage> instance) just to hold the anonymous effects that may be registered. This way,
            // the same dictionary can be reused for all kinds of effect nodes instead. Anonymous effects are generally a nicher scenario.
            _ = this.owner.transformNodes.TryAdd(new object(), canvasImage);
        }

        /// <summary>
        /// Registers an <see cref="ICanvasImage"/> object in the effect graph, associated with a given <see cref="EffectNode{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ICanvasImage"/> object to register.</typeparam>
        /// <param name="effectNode">The <see cref="EffectNode{T}"/> instance to use to register <paramref name="canvasImage"/>.</param>
        /// <param name="canvasImage">The <see cref="ICanvasImage"/> object to register in the effect graph.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="effectNode"/> or <paramref name="canvasImage"/> are <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the current <see cref="EffectGraph"/> instance is not valid or if the effect graph does not support modifications at this time.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="effectNode"/> is already registered in the effect graph.</exception>
        /// <remarks>
        /// This method can only be called from an <see cref="EffectGraph"/> value being passed to <see cref="BuildEffectGraph"/>. If this
        /// method is called from within <see cref="ConfigureEffectGraph"/>, it will fail, as the effect graph cannot be mutated from there.
        /// </remarks>
        public void RegisterNode<T>(EffectNode<T> effectNode, T canvasImage)
            where T : class, ICanvasImage
        {
            default(ArgumentNullException).ThrowIfNull(effectNode);
            default(ArgumentNullException).ThrowIfNull(canvasImage);
            default(InvalidOperationException).ThrowIf(this.owner is null);
            default(InvalidOperationException).ThrowIf(this.owner.invalidationType != InvalidationType.Creation, "The effect graph cannot be modified after its creation.");

            // Try to add the new canvas image associated with the input effect node marker.
            // This must not have been previously added from another RegisterNode call.
            if (!this.owner.transformNodes.TryAdd(effectNode, canvasImage))
            {
                default(ArgumentException).Throw(nameof(effectNode), "The specified node is already registered in the effect graph.");
            }
        }

        /// <summary>
        /// Registers an <see cref="ICanvasImage"/> object in the effect graph, and marks it as the output node for the effect graph being built.
        /// </summary>
        /// <remarks>
        /// <para><inheritdoc cref="RegisterNode{T}(EffectNode{T}, T)" path="/remarks/node()"/></para>
        /// <para>
        /// This method can be used when <paramref name="canvasImage"/> doesn't need to be retrieved from <see cref="ConfigureEffectGraph"/>, ie.
        /// when it has no properties that the current effect instance will need to mutate over it. Using this method still allows the effect to
        /// correctly track the image ownership, so that it can be disposed when the current effect instance is disposed.
        /// </para>
        /// <para>
        /// If performing lookups on <paramref name="canvasImage"/> is required, use the <see cref="RegisterOutputNode{T}(EffectNode{T}, T)"/> overload.
        /// </para>
        /// </remarks>
        /// <inheritdoc cref="RegisterNode(ICanvasImage)"/>
        public void RegisterOutputNode(ICanvasImage canvasImage)
        {
            default(ArgumentNullException).ThrowIfNull(canvasImage);
            default(InvalidOperationException).ThrowIf(this.owner is null);
            default(InvalidOperationException).ThrowIf(this.owner.invalidationType != InvalidationType.Creation, "The effect graph cannot be modified after its creation.");

            // Register the anonymous effect node with a dummy object, same as above
            _ = this.owner.transformNodes.TryAdd(new object(), canvasImage);

            // Store the anonymous output node for later use
            this.owner.canvasImage = canvasImage;
        }

        /// <summary>
        /// Registers an <see cref="ICanvasImage"/> object in the effect graph, associated with a given <see cref="EffectNode{T}"/> instance.
        /// Additionally, it also marks the input <see cref="ICanvasImage"/> object as the output node for the effect graph being built.
        /// </summary>
        /// <inheritdoc cref="RegisterNode"/>
        public void RegisterOutputNode<T>(EffectNode<T> effectNode, T canvasImage)
            where T : class, ICanvasImage
        {
            default(ArgumentNullException).ThrowIfNull(effectNode);
            default(ArgumentNullException).ThrowIfNull(canvasImage);
            default(InvalidOperationException).ThrowIf(this.owner is null);
            default(InvalidOperationException).ThrowIf(this.owner.invalidationType != InvalidationType.Creation, "The effect graph cannot be modified after its creation.");

            // Try to add the output node as in the method above (but with an extra check before doing so)
            if (!this.owner.transformNodes.TryAdd(effectNode, canvasImage))
            {
                default(ArgumentException).Throw(nameof(effectNode), "The specified node is already registered in the effect graph.");
            }

            // Store the output node for later use
            this.owner.canvasImage = canvasImage;
        }

        /// <summary>
        /// Sets a previously registered <see cref="IEffectNode"/> instance as the output node for the effect graph.
        /// </summary>
        /// <param name="effectNode">The <see cref="IEffectNode"/> instance to set as output node for the effect graph.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="effectNode"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the current <see cref="EffectGraph"/> instance is not valid.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="effectNode"/> is not registered in the effect graph.</exception>
        /// <remarks>
        /// This method can be called from both <see cref="BuildEffectGraph"/> and <see cref="ConfigureEffectGraph"/>. It can be used for
        /// efficiently changing the output node of a graph, without having to rebuild it. For instance, this can be used for effects that
        /// have multiple paths that can be taken for their inputs, depending on the value of some property describing the effect behavior.
        /// </remarks>
        public void SetOutputNode(IEffectNode effectNode)
        {
            default(ArgumentNullException).ThrowIfNull(effectNode);
            default(InvalidOperationException).ThrowIf(this.owner is null);

            // Get the canvas image for the registered effect node (which has to exist at this point)
            if (!this.owner.transformNodes.TryGetValue(effectNode, out ICanvasImage? canvasImage))
            {
                default(ArgumentException).Throw(nameof(effectNode), "The specified output node is not registered in the effect graph.");
            }

            // Store the new output node
            this.owner.canvasImage = canvasImage;
        }
    }

    /// <summary>
    /// A marker interface for an effect node that was registered from an <see cref="EffectGraph"/> value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface allows using <see cref="EffectGraph.GetNode"/> and <see cref="EffectGraph.SetOutputNode"/> in more scenarios,
    /// including where the concrete type of the image being used (eg. to set the output node) is not known (or needed) by the caller.
    /// </para>
    /// <para>
    /// This interface only implemented by <see cref="EffectNode{T}"/> and it's not meant to be implemented by external types.
    /// </para>
    /// </remarks>
    protected interface IEffectNode;

    /// <summary>
    /// A marker interface for a generic effect node that was registered from an <see cref="EffectGraph"/> value.
    /// </summary>
    /// <typeparam name="T">The covariant type of <see cref="ICanvasImage"/> associated with the current effect node.</typeparam>
    /// <remarks>
    /// <para>
    /// This interface allows using <see cref="EffectGraph.GetNode"/> and <see cref="EffectGraph.SetOutputNode"/> in more scenarios,
    /// including with ternary expressions returning multiple concrete <see cref="EffectNode{T}"/> instances with a common image type.
    /// </para>
    /// <para>
    /// This interface only implemented by <see cref="EffectNode{T}"/> and it's not meant to be implemented by external types.
    /// </para>
    /// </remarks>
    protected interface IEffectNode<out T> : IEffectNode
        where T : ICanvasImage;

    /// <summary>
    /// A marker type for an effect node that can be registered and retrieved from an <see cref="EffectGraph"/> value.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ICanvasImage"/> associated with the current effect node.</typeparam>
    protected sealed class EffectNode<T> : IEffectNode<T>
        where T : class, ICanvasImage;
}