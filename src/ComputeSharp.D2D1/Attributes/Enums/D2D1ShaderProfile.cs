namespace ComputeSharp.D2D1;

/// <summary>
/// A <see langword="enum"/> to be used with <see cref="D2DShaderProfileAttribute"/> to indicate the shader profile to use to precompile a shader.
/// </summary>
/// <remarks>
/// For more info on these shader profiles, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/specifying-compiler-targets"/>.
/// </remarks>
public enum D2D1ShaderProfile
{
    /// <summary>
    /// Indicates a D2D1 compute shader compiled for Direct3D 10.0 feature level.
    /// <para>
    /// This has the following limitations:
    /// <list type="bullet">
    ///   <item>The maximum number of threads is limited to <c>D3D11_CS_4_X_THREAD_GROUP_MAX_THREADS_PER_GROUP</c> (768) per group.</item>
    ///   <item>
    ///     The X and Y dimension of numthreads is limited to <c>D3D11_CS_4_X_THREAD_GROUP_MAX_X</c>
    ///     (768) and <c>D3D11_CS_4_X_THREAD_GROUP_MAX_Y</c> (768).
    ///   </item>
    ///   <item>The Z dimension of <c>numthreads</c> is limited to 1.</item>
    ///   <item>The Z dimension of dispatch is limited to <c>D3D11_CS_4_X_DISPATCH_MAX_THREAD_GROUPS_IN_Z_DIMENSION</c> (1).</item>
    ///   <item>Only one unordered-access view can be bound to the shader (<c>D3D11_CS_4_X_UAV_REGISTER_COUNT</c> is 1).</item>
    ///   <item>
    ///     Only <see href="https://learn.microsoft.com/en-us/windows/desktop/direct3dhlsl/sm5-object-rwstructuredbuffer"><c>RWStructuredBuffer</c></see>-s
    ///     and <see href="https://learn.microsoft.com/en-us/windows/desktop/direct3dhlsl/sm5-object-rwbyteaddressbuffer"><c>RWByteAddressBuffer</c></see>-s
    ///     are available as unordered-access views.
    ///   </item>
    ///   <item>A thread can only access its own region in groupshared memory for writing, though it can read from any location.</item>
    ///   <item>
    ///     <see href="https://learn.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ff471569(v=vs.85)"><c>SV_GroupIndex</c></see> or
    ///     <see hcref="https://learn.microsoft.com/en-us/windows/desktop/direct3dhlsl/sv-groupthreadid"><c>SV_GroupThreadID</c></see> must be used
    ///     when accessing groupshared memory for writing.
    ///   </item>
    ///   <item><c>Groupshared</c> memory is limited to 16KB per group.</item>
    ///   <item>A single thread is limited to a 256 byte region of groupshared memory for writing.</item>
    ///   <item>No atomic instructions are available.</item>
    ///   <item>No double-precision values are available.</item>
    /// </list>
    /// </para>
    /// </summary>
    ComputeShader40,

    /// <summary>
    /// Indicates a D2D1 compute shader compiled for Direct3D 10.1 feature level.
    /// </summary>
    /// <remarks>
    /// The same limitations apply for shaders of this profile as those using <see cref="ComputeShader41"/>.
    /// </remarks>
    ComputeShader41,

    /// <summary>
    /// Indicates a D2D1 compute shader compiled for Direct3D 11.0 and 11.1 feature levels.
    /// <para>
    /// This has the following items to be considered:
    /// <list type="bullet">
    ///   <item>The maximum number of threads is limited to <c>D3D11_CS_THREAD_GROUP_MAX_THREADS_PER_GROUP</c> (1024) per group.</item>
    ///   <item>
    ///     The X and Y dimension of numthreads is limited to <c>D3D11_CS_THREAD_GROUP_MAX_X</c>
    ///     (1024) and <c>D3D11_CS_THREAD_GROUP_MAX_Y</c> (1024).
    ///   </item>
    ///   <item>The Z dimension of numthreads is limited to <c>D3D11_CS_THREAD_GROUP_MAX_Z</c> (64).</item>
    ///   <item>The maximum dimension of dispatch is limited to <c>D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION</c> (65535).</item>
    ///   <item>The maximum number of unordered-access views that can be bound to a shader is <c>D3D11_PS_CS_UAV_REGISTER_COUNT</c> (8).</item>
    ///   <item>
    ///     Supports <see href="https://learn.microsoft.com/en-us/windows/desktop/direct3dhlsl/sm5-object-rwstructuredbuffer"><c>RWStructuredBuffer</c></see>-s,
    ///     <see href="https://learn.microsoft.com/en-us/windows/desktop/direct3dhlsl/sm5-object-rwbyteaddressbuffer"><c>RWByteAddressBuffer</c></see>-s, and
    ///     typed unordered-access views (<see href="https://learn.microsoft.com/en-us/windows/desktop/direct3dhlsl/sm5-object-rwtexture1d"><c>RWTexture1D</c></see>,
    ///     <see href="https://learn.microsoft.com/en-us/windows/desktop/direct3dhlsl/sm5-object-rwtexture2d"><c>RWTexture2D</c></see>,
    ///     <see href="https://learn.microsoft.com/en-us/windows/desktop/direct3dhlsl/sm5-object-rwtexture3d"><c>RWTexture3D</c></see>, and so on).
    ///   </item>
    ///   <item>Atomic instructions are available.</item>
    ///   <item>Double-precision support might be available.</item>
    /// </list>
    /// </para>
    /// </summary>
    ComputeShader50,

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
    PixelShader40Level93,

    /// <summary>
    /// Indicates a D2D1 pixel shader compiled for Direct3D 10.0 feature level.
    /// </summary>
    PixelShader40,

    /// <summary>
    /// Indicates a D2D1 pixel shader compiled for Direct3D 10.1 feature level.
    /// </summary>
    PixelShader41,

    /// <summary>
    /// Indicates a D2D1 pixel shader compiled for Direct3D 11.0 and 11.1 feature levels.
    /// </summary>
    PixelShader50
}