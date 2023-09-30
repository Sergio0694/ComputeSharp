using System;
using System.ComponentModel;
using ComputeSharp.D2D1.Interop;

namespace ComputeSharp.D2D1.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a given shader that can be dispatched.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface ID2D1Shader
{
    /// <summary>
    /// Gets the effect id of the D2D effect using this shader.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
    /// </remarks>
    ref readonly Guid EffectId { get; }

    /// <summary>
    /// Gets the display name of the D2D effect using this shader, if specified.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
    /// </remarks>
    string? EffectDisplayName { get; }

    /// <summary>
    /// Gets the description of the D2D effect using this shader, if specified.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
    /// </remarks>
    string? EffectDescription { get; }

    /// <summary>
    /// Gets the category of the D2D effect using this shader, if specified.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
    /// </remarks>
    string? EffectCategory { get; }

    /// <summary>
    /// Gets the author of the D2D effect using this shader, if specified.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
    /// </remarks>
    string? EffectAuthor { get; }

    /// <summary>
    /// Initializes the current shader from a buffer with the serialized dispatch data.
    /// </summary>
    /// <param name="data">The input buffer with the serialized dispatch data.</param>
    /// <remarks>The input buffer must be retrieved from <see cref="LoadDispatchData"/>.</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void InitializeFromDispatchData(ReadOnlySpan<byte> data);

    /// <summary>
    /// Gets the pixel options for the current shader.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    D2D1PixelOptions PixelOptions { get; }

    /// <summary>
    /// Gets the size in bytes of the constant buffer for the current shader.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    int ConstantBufferSize { get; }

    /// <summary>
    /// Gets the number of inputs for the current shader.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    int InputCount { get; }

    /// <summary>
    /// Gets the input types for the current shader.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    ReadOnlyMemory<D2D1PixelShaderInputType> InputTypes { get; }

    /// <summary>
    /// Gets the input descriptions for the current shader.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    ReadOnlyMemory<D2D1InputDescription> InputDescriptions { get; }

    /// <summary>
    /// Gets the resource texture descriptions for the shader.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    ReadOnlyMemory<D2D1ResourceTextureDescription> ResourceTextureDescriptions { get; }

    /// <summary>
    /// Gets the buffer precision for the shader.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    D2D1BufferPrecision BufferPrecision { get; }

    /// <summary>
    /// Gets the channel depth for the shader.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    D2D1ChannelDepth ChannelDepth { get; }

    /// <summary>
    /// Loads the dispatch data for the shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of data loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the data.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    void LoadDispatchData<TLoader>(ref TLoader loader)
        where TLoader : struct, ID2D1DispatchDataLoader;

    /// <summary>
    /// Gets the HLSL source code for the current shader instance.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be called directly by user code")]
    string HlslSource { get; }

    /// <summary>
    /// Gets the shader profile for the current shader.
    /// </summary>
    /// <remarks>
    /// This shader profile is either explicitly set by users, or the default one for D2D1 shaders.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    D2D1ShaderProfile ShaderProfile { get; }

    /// <summary>
    /// Gets the compile options for the current shader.
    /// </summary>
    /// <remarks>
    /// This compile options are either explicitly set by users, or the default one for D2D1 shaders.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    D2D1CompileOptions CompileOptions { get; }

    /// <summary>
    /// Gets the HLSL bytecode for the current shader, if available.
    /// </summary>
    /// <remarks>
    /// If no precompiled HLSL bytecode is available, the returned <see cref="ReadOnlyMemory{T}"/> instance will be empty.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is not intended to be used directly by user code")]
    ReadOnlyMemory<byte> HlslBytecode { get; }
}