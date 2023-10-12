using System;

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <summary>
/// A container for ASCII encoded, null-terminated constant strings.
/// </summary>
internal static class ASCII
{
    /// <summary>
    /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"D2D_FUNCTION"</c> ASCII text.
    /// </summary>
    public static ReadOnlySpan<byte> D2D_FUNCTION => "D2D_FUNCTION"u8;

    /// <summary>
    /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"D2D_FULL_SHADER"</c> ASCII text.
    /// </summary>
    public static ReadOnlySpan<byte> D2D_FULL_SHADER => "D2D_FULL_SHADER"u8;

    /// <summary>
    /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>'\0'</c> ASCII character.
    /// </summary>
    public static ReadOnlySpan<byte> NullTerminator => ""u8;

    /// <summary>
    /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"D2D_ENTRY"</c> ASCII text.
    /// </summary>
    public static ReadOnlySpan<byte> D2D_ENTRY => "D2D_ENTRY"u8;

    /// <summary>
    /// Gets a <see cref="ReadOnlySpan{T}"/> with the <c>"Execute"</c> ASCII text.
    /// </summary>
    public static ReadOnlySpan<byte> Execute => "Execute"u8;

    /// <summary>
    /// Gets a <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a library.
    /// </summary>
    /// <param name="shaderProfile">The input shaader profile to use.</param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a library.</returns>
    public static ReadOnlySpan<byte> GetLibraryProfile(D2D1ShaderProfile shaderProfile)
    {
        return shaderProfile switch
        {
            D2D1ShaderProfile.PixelShader40Level91 => "lib_4_0_level_9_1"u8,
            D2D1ShaderProfile.PixelShader40Level93 => "lib_4_0_level_9_3"u8,
            D2D1ShaderProfile.PixelShader40 => "lib_4_0"u8,
            D2D1ShaderProfile.PixelShader41 => "lib_4_1"u8,
            _ => "lib_5_0"u8
        };
    }

    /// <summary>
    /// Gets a <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a full shader.
    /// </summary>
    /// <param name="shaderProfile">The input shaader profile to use.</param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> for a given shader profile, for a full shader.</returns>
    public static ReadOnlySpan<byte> GetPixelShaderProfile(D2D1ShaderProfile shaderProfile)
    {
        return shaderProfile switch
        {
            D2D1ShaderProfile.PixelShader40Level91 => "ps_4_0_level_9_1"u8,
            D2D1ShaderProfile.PixelShader40Level93 => "ps_4_0_level_9_3"u8,
            D2D1ShaderProfile.PixelShader40 => "ps_4_0"u8,
            D2D1ShaderProfile.PixelShader41 => "ps_4_1"u8,
            _ => "ps_5_0"u8
        };
    }
}