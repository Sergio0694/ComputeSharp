using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating the pixel options to use.
/// Using this attribute is optional when defining a D2D1 shader.
/// </summary>
/// <remarks>
/// For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/ne-d2d1effectauthor-d2d1_pixel_options"/>.
/// </remarks>
/// <param name="options">The options to specify for the shader.</param>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DPixelOptionsAttribute(D2D1PixelOptions options) : Attribute
{
    /// <summary>
    /// Gets the options to specify for the shader.
    /// </summary>
    public D2D1PixelOptions Options { get; } = options;
}