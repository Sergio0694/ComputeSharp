namespace ComputeSharp.D2D1;

/// <summary>
/// Indicates how the sampling of a D2D1 pixel shader will be restricted. This indicates whether the vertex buffer
/// is large and tends to change infrequently or smaller and changes frequently (typically frame over frame). 
/// </summary>
/// <remarks>
/// This type exposes the available values in <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/ne-d2d1effectauthor-d2d1_pixel_options"/>.
/// </remarks>
public enum D2D1PixelOption
{
    /// <summary>
    /// The pixel shader is not restricted in its sampling.
    /// </summary>
    None,

    /// <summary>
    /// The pixel shader samples inputs only at the same scene coordinate as the output pixel
    /// and returns transparent black whenever the input pixels are also transparent black.
    /// </summary>
    TrivialSampling
}
