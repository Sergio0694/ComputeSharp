using System;
using System.Runtime.CompilerServices;
using Microsoft.Graphics.Canvas;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <summary>
/// An object representing an effect graph being built or configured.
/// </summary>
public readonly ref struct CanvasEffectGraph
{
    /// <summary>
    ///The owning <see cref="CanvasEffect"/> instance.
    /// </summary>
    private readonly CanvasEffect owner;

    /// <summary>
    /// Creates a new <see cref="CanvasEffectGraph"/> instance with the specified parameters.
    /// </summary>
    /// <param name="owner">The owning <see cref="CanvasEffect"/> instance.</param>
    internal CanvasEffectGraph(CanvasEffect owner)
    {
        this.owner = owner;
    }

    /// <summary>
    /// Gets a previously registered <see cref="ICanvasImage"/> object associated with a given effect graph node.
    /// </summary>
    /// <param name="effectNode">The <see cref="ICanvasEffectNode"/> instance to use to lookup the <see cref="ICanvasImage"/> object to retrieve.</param>
    /// <returns>The <see cref="ICanvasImage"/> object associated with <paramref name="effectNode"/> in the effect graph.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="effectNode"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="effectNode"/> is not currently registered in the effect graph.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the current <see cref="CanvasEffectGraph"/> instance is not valid.</exception>
    public ICanvasImage GetNode(ICanvasEffectNode effectNode)
    {
        default(ArgumentNullException).ThrowIfNull(effectNode);
        default(InvalidOperationException).ThrowIf(this.owner is null);

        // Try to get the canvas image associated with the input effect node marker.
        // This must have been previously registered in a call to BuildEffectGraph.
        if (!this.owner.TransformNodes.TryGetValue(effectNode, out ICanvasImage? canvasImage))
        {
            default(ArgumentException).Throw(nameof(effectNode), "The specified node is not registered in the effect graph.");
        }

        return canvasImage;
    }

    /// <summary>
    /// Gets a previously registered <typeparamref name="T"/> object (an <see cref="ICanvasImage"/> instance) associated with a given effect graph node.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ICanvasImage"/> object to retrieve.</typeparam>
    /// <param name="effectNode">The <see cref="ICanvasEffectNode{T}"/> instance to use to lookup the <see cref="ICanvasImage"/> object to retrieve.</param>
    /// <returns>The <typeparamref name="T"/> object associated with <paramref name="effectNode"/> in the effect graph.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="effectNode"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="effectNode"/> is not currently registered in the effect graph.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the current <see cref="CanvasEffectGraph"/> instance is not valid.</exception>
    public T GetNode<T>(ICanvasEffectNode<T> effectNode)
        where T : class, ICanvasImage
    {
        default(ArgumentNullException).ThrowIfNull(effectNode);
        default(InvalidOperationException).ThrowIf(this.owner is null);

        // Retrieve the registered canvas image, same as above
        if (!this.owner.TransformNodes.TryGetValue(effectNode, out ICanvasImage? canvasImage))
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
    /// Thrown if the current <see cref="CanvasEffectGraph"/> instance is not valid or if the effect graph does not support modifications at this time.
    /// </exception>
    /// <remarks>
    /// <para><inheritdoc cref="RegisterNode{T}(CanvasEffectNode{T}, T)" path="/remarks/node()"/></para>
    /// <para>
    /// This method can be used when <paramref name="canvasImage"/> doesn't need to be retrieved from <see cref="CanvasEffect.ConfigureEffectGraph"/>, ie.
    /// when it has no properties that the current effect instance will need to mutate over it. Using this method still allows the effect to
    /// correctly track the image ownership, so that it can be disposed when the current effect instance is disposed.
    /// </para>
    /// <para>
    /// If performing lookups on <paramref name="canvasImage"/> is required, use the <see cref="RegisterNode{T}(CanvasEffectNode{T}, T)"/> overload.
    /// </para>
    /// </remarks>
    public void RegisterNode(ICanvasImage canvasImage)
    {
        default(ArgumentNullException).ThrowIfNull(canvasImage);
        default(InvalidOperationException).ThrowIf(this.owner is null);
        default(InvalidOperationException).ThrowIf(this.owner.InvalidationType != CanvasEffectInvalidationType.Creation, "The effect graph cannot be modified after its creation.");

        // Use a new dummy object as key to register the anonymous effect node. This is cheaper than having to maintain a different
        // data structure (eg. a HashSet<ICanvasImage> instance) just to hold the anonymous effects that may be registered. This way,
        // the same dictionary can be reused for all kinds of effect nodes instead. Anonymous effects are generally a nicher scenario.
        _ = this.owner.TransformNodes.TryAdd(new object(), canvasImage);
    }

    /// <summary>
    /// Registers an <see cref="ICanvasImage"/> object in the effect graph, associated with a given <see cref="CanvasEffectNode{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ICanvasImage"/> object to register.</typeparam>
    /// <param name="effectNode">The <see cref="CanvasEffectNode{T}"/> instance to use to register <paramref name="canvasImage"/>.</param>
    /// <param name="canvasImage">The <see cref="ICanvasImage"/> object to register in the effect graph.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="effectNode"/> or <paramref name="canvasImage"/> are <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the current <see cref="CanvasEffectGraph"/> instance is not valid or if the effect graph does not support modifications at this time.
    /// </exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="effectNode"/> is already registered in the effect graph.</exception>
    /// <remarks>
    /// This method can only be called from an <see cref="CanvasEffectGraph"/> value being passed to <see cref="CanvasEffect.BuildEffectGraph"/>. If this
    /// method is called from within <see cref="CanvasEffect.ConfigureEffectGraph"/>, it will fail, as the effect graph cannot be mutated from there.
    /// </remarks>
    public void RegisterNode<T>(CanvasEffectNode<T> effectNode, T canvasImage)
        where T : class, ICanvasImage
    {
        default(ArgumentNullException).ThrowIfNull(effectNode);
        default(ArgumentNullException).ThrowIfNull(canvasImage);
        default(InvalidOperationException).ThrowIf(this.owner is null);
        default(InvalidOperationException).ThrowIf(this.owner.InvalidationType != CanvasEffectInvalidationType.Creation, "The effect graph cannot be modified after its creation.");

        // Try to add the new canvas image associated with the input effect node marker.
        // This must not have been previously added from another RegisterNode call.
        if (!this.owner.TransformNodes.TryAdd(effectNode, canvasImage))
        {
            default(ArgumentException).Throw(nameof(effectNode), "The specified node is already registered in the effect graph.");
        }
    }

    /// <summary>
    /// Registers an <see cref="ICanvasImage"/> object in the effect graph, and marks it as the output node for the effect graph being built.
    /// </summary>
    /// <remarks>
    /// <para><inheritdoc cref="RegisterNode{T}(CanvasEffectNode{T}, T)" path="/remarks/node()"/></para>
    /// <para>
    /// This method can be used when <paramref name="canvasImage"/> doesn't need to be retrieved from <see cref="CanvasEffect.ConfigureEffectGraph"/>, ie.
    /// when it has no properties that the current effect instance will need to mutate over it. Using this method still allows the effect to
    /// correctly track the image ownership, so that it can be disposed when the current effect instance is disposed.
    /// </para>
    /// <para>
    /// If performing lookups on <paramref name="canvasImage"/> is required, use the <see cref="RegisterOutputNode{T}(CanvasEffectNode{T}, T)"/> overload.
    /// </para>
    /// </remarks>
    /// <inheritdoc cref="RegisterNode(ICanvasImage)"/>
    public void RegisterOutputNode(ICanvasImage canvasImage)
    {
        default(ArgumentNullException).ThrowIfNull(canvasImage);
        default(InvalidOperationException).ThrowIf(this.owner is null);
        default(InvalidOperationException).ThrowIf(this.owner.InvalidationType != CanvasEffectInvalidationType.Creation, "The effect graph cannot be modified after its creation.");

        // Register the anonymous effect node with a dummy object, same as above
        _ = this.owner.TransformNodes.TryAdd(new object(), canvasImage);

        // Store the anonymous output node for later use
        this.owner.CanvasImage = canvasImage;
    }

    /// <summary>
    /// Registers an <see cref="ICanvasImage"/> object in the effect graph, associated with a given <see cref="CanvasEffectNode{T}"/> instance.
    /// Additionally, it also marks the input <see cref="ICanvasImage"/> object as the output node for the effect graph being built.
    /// </summary>
    /// <inheritdoc cref="RegisterNode"/>
    public void RegisterOutputNode<T>(CanvasEffectNode<T> effectNode, T canvasImage)
        where T : class, ICanvasImage
    {
        default(ArgumentNullException).ThrowIfNull(effectNode);
        default(ArgumentNullException).ThrowIfNull(canvasImage);
        default(InvalidOperationException).ThrowIf(this.owner is null);
        default(InvalidOperationException).ThrowIf(this.owner.InvalidationType != CanvasEffectInvalidationType.Creation, "The effect graph cannot be modified after its creation.");

        // Try to add the output node as in the method above (but with an extra check before doing so)
        if (!this.owner.TransformNodes.TryAdd(effectNode, canvasImage))
        {
            default(ArgumentException).Throw(nameof(effectNode), "The specified node is already registered in the effect graph.");
        }

        // Store the output node for later use
        this.owner.CanvasImage = canvasImage;
    }

    /// <summary>
    /// Sets a previously registered <see cref="ICanvasEffectNode"/> instance as the output node for the effect graph.
    /// </summary>
    /// <param name="effectNode">The <see cref="ICanvasEffectNode"/> instance to set as output node for the effect graph.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="effectNode"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the current <see cref="CanvasEffectGraph"/> instance is not valid.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="effectNode"/> is not registered in the effect graph.</exception>
    /// <remarks>
    /// This method can be called from both <see cref="CanvasEffect.BuildEffectGraph"/> and <see cref="CanvasEffect.ConfigureEffectGraph"/>. It can be used for
    /// efficiently changing the output node of a graph, without having to rebuild it. For instance, this can be used for effects that
    /// have multiple paths that can be taken for their inputs, depending on the value of some property describing the effect behavior.
    /// </remarks>
    public void SetOutputNode(ICanvasEffectNode effectNode)
    {
        default(ArgumentNullException).ThrowIfNull(effectNode);
        default(InvalidOperationException).ThrowIf(this.owner is null);

        // Get the canvas image for the registered effect node (which has to exist at this point)
        if (!this.owner.TransformNodes.TryGetValue(effectNode, out ICanvasImage? canvasImage))
        {
            default(ArgumentException).Throw(nameof(effectNode), "The specified output node is not registered in the effect graph.");
        }

        // Store the new output node
        this.owner.CanvasImage = canvasImage;
    }
}