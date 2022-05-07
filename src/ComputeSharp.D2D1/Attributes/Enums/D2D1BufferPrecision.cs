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
    Unknown,

    /// <summary>
    /// The buffer precision uses 8-bit normalized integer values per channel.
    /// </summary>
    Int8Normalized,

    /// <summary>
    /// The buffer precision uses 8-bit normalized integer values per channel with standard RGB data.
    /// </summary>
    Int8NormalizedSRGB,

    /// <summary>
    /// The buffer precision uses 16-bit normalized integer values per channel.
    /// </summary>
    Int16Normalized,

    /// <summary>
    /// The buffer precision uses 16-bit float values per channel.
    /// </summary>
    Float16,

    /// <summary>
    /// The buffer precision uses 32-bit float values per channel.
    /// </summary>
    Float32
}
