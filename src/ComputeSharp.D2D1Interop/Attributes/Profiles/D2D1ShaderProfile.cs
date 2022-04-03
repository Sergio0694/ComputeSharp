namespace ComputeSharp.D2D1Interop;

/// <summary>
/// A <see langword="enum"/> to be used with <see cref="D2DEmbeddedBytecodeAttribute"/> to indicate the shader profile to use to precompile a shader.
/// </summary>
public enum D2D1ShaderProfile
{
    /// <summary>
    /// Indicates a D2D1 pixel shader compiled for Direct3D 9.1 feature level.
    /// <para>
    /// This has the following features:
    /// <list type="bullet">
    ///   <item>64 arithmetic and 32 texture instructions.</item>
    ///   <item>12 temporary registers.</item>
    ///   <item>4 levels of dependent reads.</item>
    /// </list>
    /// </para>
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/specifying-compiler-targets"/>.
    /// </remarks>
    PixelShader40Level91,

    /// <summary>
    /// Indicates a D2D1 pixel shader compiled for Direct3D 9.3 feature level.
    /// <para>
    /// This has the following features:
    /// <list type="bullet">
    ///   <item>512 instructions.</item>
    ///   <item>32 temporary registers.</item>
    ///   <item>Static flow control (max depth of 4).</item>
    ///   <item>Dynamic flow control (max depth of 24).</item>
    ///   <item><c>D3DPS20CAPS_ARBITRARYSWIZZLE</c>.</item>
    ///   <item><c>D3DPS20CAPS_GRADIENTINSTRUCTIONS</c>.</item>
    ///   <item><c>D3DPS20CAPS_PREDICATION</c>.</item>
    ///   <item><c>D3DPS20CAPS_NODEPENDENTREADLIMIT</c>.</item>
    ///   <item><c>D3DPS20CAPS_NOTEXINSTRUCTIONLIMIT</c>.</item>
    /// </list>
    /// </para>
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/specifying-compiler-targets"/>.
    /// </remarks>
    PixelShader40Level93,

    /// <summary>
    /// Indicates a D2D1 pixel shader compiled for Direct3D 10.0 feature level.
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/specifying-compiler-targets"/>.
    /// </remarks>
    PixelShader40,

    /// <summary>
    /// Indicates a D2D1 pixel shader compiled for Direct3D 10.1 feature level.
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/specifying-compiler-targets"/>.
    /// </remarks>
    PixelShader41,

    /// <summary>
    /// Indicates a D2D1 pixel shader compiled for Direct3D 11.0 and 11.1 feature levels.
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/specifying-compiler-targets"/>.
    /// </remarks>
    PixelShader50
}
