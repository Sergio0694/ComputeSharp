using System;
#if SOURCE_GENERATOR
using static Windows.Win32.Graphics.Direct2D.D2D1_PIXEL_OPTIONS;
#else
using static ComputeSharp.Win32.D2D1_PIXEL_OPTIONS;
#endif

namespace ComputeSharp.D2D1;

/// <summary>
/// Indicates how the sampling of a D2D1 pixel shader will be restricted.
/// </summary>
/// <remarks>
/// This type exposes the available values in <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/ne-d2d1effectauthor-d2d1_pixel_options"/>.
/// </remarks>
[Flags]
public enum D2D1PixelOptions
{
    /// <summary>
    /// The pixel shader is not restricted in its sampling.
    /// </summary>
    None = (int)D2D1_PIXEL_OPTIONS_NONE,

    /// <summary>
    /// The pixel shader samples inputs only at the same scene coordinate as the output pixel
    /// and returns transparent black whenever the input pixels are also transparent black.
    /// </summary>
    TrivialSampling = (int)D2D1_PIXEL_OPTIONS_TRIVIAL_SAMPLING
}