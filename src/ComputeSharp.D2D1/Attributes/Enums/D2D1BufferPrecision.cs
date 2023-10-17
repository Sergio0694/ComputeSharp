#if SOURCE_GENERATOR
using static Windows.Win32.Graphics.Direct2D.D2D1_BUFFER_PRECISION;
#else
using static TerraFX.Interop.DirectX.D2D1_BUFFER_PRECISION;
#endif

namespace ComputeSharp.D2D1;

/// <summary>
/// Indicates the bit depth to use in the imaging pipeline in Direct2D to produce the result of a D2D1 shader.
/// </summary>
/// <remarks>
/// <para>This type exposes the available values in <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/ne-d2d1_1-d2d1_buffer_precision"/>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct2d/precision-and-clipping-in-effect-graphs"/>.</para>
/// </remarks>
public enum D2D1BufferPrecision
{
    /// <summary>
    /// The buffer precision is not specified.
    /// </summary>
    Unknown = (int)D2D1_BUFFER_PRECISION_UNKNOWN,

    /// <summary>
    /// The buffer precision uses 8-bit normalized integer values per channel.
    /// </summary>
    UInt8Normalized = (int)D2D1_BUFFER_PRECISION_8BPC_UNORM,

    /// <summary>
    /// The buffer precision uses 8-bit normalized integer values per channel with standard RGB data.
    /// </summary>
    UInt8NormalizedSrgb = (int)D2D1_BUFFER_PRECISION_8BPC_UNORM_SRGB,

    /// <summary>
    /// The buffer precision uses 16-bit normalized integer values per channel.
    /// </summary>
    UInt16Normalized = (int)D2D1_BUFFER_PRECISION_16BPC_UNORM,

    /// <summary>
    /// The buffer precision uses 16-bit float values per channel.
    /// </summary>
    Float16 = (int)D2D1_BUFFER_PRECISION_16BPC_FLOAT,

    /// <summary>
    /// The buffer precision uses 32-bit float values per channel.
    /// </summary>
    Float32 = (int)D2D1_BUFFER_PRECISION_32BPC_FLOAT
}