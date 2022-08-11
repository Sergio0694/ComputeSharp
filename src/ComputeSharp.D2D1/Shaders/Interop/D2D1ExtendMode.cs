using static TerraFX.Interop.DirectX.D2D1_EXTEND_MODE;

namespace ComputeSharp.D2D1;

/// <summary>
/// Specifies how a brush paints areas outside of its normal content area.
/// </summary>
/// <remarks>
/// This type exposes the available values in <see href="https://docs.microsoft.com/windows/win32/api/d2d1/ne-d2d1-d2d1_extend_mode"/>.
/// </remarks>
public enum D2D1ExtendMode
{
    /// <summary>
    /// Repeat the edge pixels of the brush's content for all regions outside the normal content area.
    /// </summary>
    Clamp = (int)D2D1_EXTEND_MODE_CLAMP,

    /// <summary>
    /// Repeat the brush's content.
    /// </summary>
    Wrap = (int)D2D1_EXTEND_MODE_WRAP,

    /// <summary>
    /// The same as <see cref="Wrap"/>, except that alternate tiles of the brush's
    /// content are flipped (the brush's normal content is drawn untransformed).
    /// </summary>
    Mirror = (int)D2D1_EXTEND_MODE_MIRROR
}
