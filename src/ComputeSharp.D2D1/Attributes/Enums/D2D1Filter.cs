using static TerraFX.Interop.DirectX.D2D1_FILTER;

namespace ComputeSharp.D2D1;

/// <summary>
/// Represents the filtering mode that a transform may select to use on input textures.
/// </summary>
/// <remarks>
/// <para>
/// In most scenarios, or when unsure on what values to use, consider this reference:
/// <list type="bullet">
///     <item><description><see cref="MinMagMipPoint"/>: point sampling.</description></item>
///     <item><description><see cref="MinMagMipLinear"/>: linear sampling.</description></item>
/// </list>
/// Other values can be used when explicit fine grained control is needed on how inputs are sampled.
/// </para>
/// <para>This type exposes the available values in <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/ne-d2d1effectauthor-d2d1_filter"/>.</para>
/// </remarks>
public enum D2D1Filter
{
    /// <summary>
    /// Use point sampling for minification, magnification, and mip-level sampling.
    /// </summary>
    MinMagMipPoint = (int)D2D1_FILTER_MIN_MAG_MIP_POINT,

    /// <summary>
    /// Use point sampling for minification and magnification, use linear interpolation for mip-level sampling.
    /// </summary>
    MinMagPointMipLinear = (int)D2D1_FILTER_MIN_MAG_POINT_MIP_LINEAR,

    /// <summary>
    /// Use point sampling for minification, use linear interpolation for magnification, use point sampling for mip-level sampling.
    /// </summary>
    MinPointMagLinearMipPoint = (int)D2D1_FILTER_MIN_POINT_MAG_LINEAR_MIP_POINT,

    /// <summary>
    /// Use point sampling for minification, use linear interpolation for magnification and mip-level sampling.
    /// </summary>
    MinPointMagMipLinear = (int)D2D1_FILTER_MIN_POINT_MAG_MIP_LINEAR,

    /// <summary>
    /// Use linear interpolation for minification, use point sampling for magnification and mip-level sampling.
    /// </summary>
    MinLinearMagMipPoint = (int)D2D1_FILTER_MIN_LINEAR_MAG_MIP_POINT,

    /// <summary>
    /// Use linear interpolation for minification, use point sampling for magnification, use linear interpolation for mip-level sampling.
    /// </summary>
    MinLinearMagPointMinLinear = (int)D2D1_FILTER_MIN_LINEAR_MAG_POINT_MIP_LINEAR,

    /// <summary>
    /// Use linear interpolation for minification and magnification, use point sampling for mip-level sampling.
    /// </summary>
    MinMagLinearMipPoint = (int)D2D1_FILTER_MIN_MAG_LINEAR_MIP_POINT,

    /// <summary>
    /// Use linear interpolation for minification, magnification, and mip-level sampling.
    /// </summary>
    MinMagMipLinear = (int)D2D1_FILTER_MIN_MAG_MIP_LINEAR,

    /// <summary>
    /// Use anisotropic interpolation for minification, magnification, and mip-level sampling.
    /// </summary>
    Anisotropic = (int)D2D1_FILTER_ANISOTROPIC
}
