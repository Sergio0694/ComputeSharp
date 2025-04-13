using System;
using ComputeSharp.D2D1.Interop;

namespace ComputeSharp.D2D1.Descriptors;

/// <summary>
/// An interface for a descriptor for a given D2D1 pixel shader.
/// Descriptors contain metadata associated to D2D1 pixel shaders,
/// with all information needed to run them and create effects.
/// </summary>
/// <typeparam name="T">The type of D2D1 pixel shader being described.</typeparam>
public interface ID2D1PixelShaderDescriptor<T>
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// Gets the effect id of the D2D effect using this shader.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="D2D1PixelShaderEffect"/>.
    /// </remarks>
    static abstract ref readonly Guid EffectId { get; }

    /// <summary>
    /// Gets the display name of the D2D effect using this shader, if specified.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="D2D1PixelShaderEffect"/>.
    /// </remarks>
    static abstract string? EffectDisplayName { get; }

    /// <summary>
    /// Gets the description of the D2D effect using this shader, if specified.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="D2D1PixelShaderEffect"/>.
    /// </remarks>
    static abstract string? EffectDescription { get; }

    /// <summary>
    /// Gets the category of the D2D effect using this shader, if specified.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="D2D1PixelShaderEffect"/>.
    /// </remarks>
    static abstract string? EffectCategory { get; }

    /// <summary>
    /// Gets the author of the D2D effect using this shader, if specified.
    /// </summary>
    /// <remarks>
    /// This only applies to effects created from <see cref="D2D1PixelShaderEffect"/>.
    /// </remarks>
    static abstract string? EffectAuthor { get; }

    /// <summary>
    /// Gets a pointer to the effect factory for the current shader.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This only applies to effects created from <see cref="D2D1PixelShaderEffect"/>.
    /// </para>
    /// <para>
    /// The returned function pointer should have a signature matching <a href="https://learn.microsoft.com/windows/win32/api/d2d1_1/nc-d2d1_1-pd2d1_effect_factory"><c>PD2D1_EFFECT_FACTORY</c></a>,
    /// and it should point to a method calling <see cref="D2D1PixelShaderEffect.CreateEffectUnsafe"/>. Using other D2D effect implementations is not a supported scenario, and might not work correctly.
    /// </para>
    /// </remarks>
    static abstract nint EffectFactory { get; }

    /// <summary>
    /// Gets the size in bytes of the constant buffer for the current shader.
    /// </summary>
    static abstract int ConstantBufferSize { get; }

    /// <summary>
    /// Gets the number of inputs for the current shader.
    /// </summary>
    static abstract int InputCount { get; }

    /// <summary>
    /// Gets the number of resource textures for the current shader.
    /// </summary>
    static abstract int ResourceTextureCount { get; }

    /// <summary>
    /// Gets the input types for the current shader.
    /// </summary>
    static abstract ReadOnlyMemory<D2D1PixelShaderInputType> InputTypes { get; }

    /// <summary>
    /// Gets the input descriptions for the current shader.
    /// </summary>
    static abstract ReadOnlyMemory<D2D1InputDescription> InputDescriptions { get; }

    /// <summary>
    /// Gets the resource texture descriptions for the shader.
    /// </summary>
    static abstract ReadOnlyMemory<D2D1ResourceTextureDescription> ResourceTextureDescriptions { get; }

    /// <summary>
    /// Gets the pixel options for the current shader.
    /// </summary>
    static abstract D2D1PixelOptions PixelOptions { get; }

    /// <summary>
    /// Gets the buffer precision for the shader.
    /// </summary>
    static abstract D2D1BufferPrecision BufferPrecision { get; }

    /// <summary>
    /// Gets the channel depth for the shader.
    /// </summary>
    static abstract D2D1ChannelDepth ChannelDepth { get; }

    /// <summary>
    /// Gets the shader profile for the current shader.
    /// </summary>
    /// <remarks>
    /// This shader profile is either explicitly set by users, or the default one for D2D1 shaders.
    /// </remarks>
    static abstract D2D1ShaderProfile ShaderProfile { get; }

    /// <summary>
    /// Gets the compile options for the current shader.
    /// </summary>
    /// <remarks>
    /// This compile options are either explicitly set by users, or the default one for D2D1 shaders.
    /// </remarks>
    static abstract D2D1CompileOptions CompileOptions { get; }

    /// <summary>
    /// Gets the HLSL source code for the current shader instance.
    /// </summary>
    static abstract string HlslSource { get; }

    /// <summary>
    /// Gets the HLSL bytecode for the current shader, if available.
    /// </summary>
    /// <remarks>
    /// If no precompiled HLSL bytecode is available, the returned <see cref="ReadOnlyMemory{T}"/> instance will be empty.
    /// </remarks>
    static abstract ReadOnlyMemory<byte> HlslBytecode { get; }

    /// <summary>
    /// Creates a new <typeparamref name="T"/> shader instance from a constant buffer.
    /// </summary>
    /// <param name="buffer">The input constant buffer to read.</param>
    /// <returns>The resulting <typeparamref name="T"/> instance.</returns>
    /// <remarks>The input buffer must be retrieved from <see cref="LoadConstantBuffer"/>.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="buffer"/> is not large enough for the current shader type.</exception>
    static abstract T CreateFromConstantBuffer(ReadOnlySpan<byte> buffer);

    /// <summary>
    /// Loads the constant buffer of a given input shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of data loader being used.</typeparam>
    /// <param name="shader">The input shader to load the constant buffer for.</param>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the constant buffer.</param>
    static abstract void LoadConstantBuffer<TLoader>(in T shader, ref TLoader loader)
        where TLoader : struct, ID2D1ConstantBufferLoader;
}