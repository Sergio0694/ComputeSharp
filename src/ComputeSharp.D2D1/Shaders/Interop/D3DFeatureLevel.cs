using static TerraFX.Interop.DirectX.D3D_FEATURE_LEVEL;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Describes the set of features targeted by a Direct3D device.
/// </summary>
public enum D3DFeatureLevel
{
    /// <summary>
    /// Allows Microsoft Compute Driver Model (MCDM) devices to be used, or more feature-rich devices (such as
    /// traditional GPUs) that support a superset of the functionality. MCDM is the overall driver model for
    /// compute-only; it's a scaled-down peer of the larger scoped Windows Device Driver Model (WDDM).
    /// </summary>
    FeatureLevel1_0_Core = D3D_FEATURE_LEVEL_1_0_CORE,

    /// <summary>
    /// Targets features supported by <see href="https://learn.microsoft.com/windows/win32/direct3d11/overviews-direct3d-11-devices-downlevel-intro">feature level</see> 9.1, including shader model 2.
    /// </summary>
    FeatureLevel9_1 = D3D_FEATURE_LEVEL_9_1,

    /// <summary>
    /// Targets features supported by <see href="https://learn.microsoft.com/windows/win32/direct3d11/overviews-direct3d-11-devices-downlevel-intro">feature level</see> 9.2, including shader model 2.
    /// </summary>
    FeatureLevel9_2 = D3D_FEATURE_LEVEL_9_2,

    /// <summary>
    /// Targets features supported by <see href="https://learn.microsoft.com/windows/win32/direct3d11/overviews-direct3d-11-devices-downlevel-intro">feature level</see> 9.3, including shader model 2.
    /// </summary>
    FeatureLevel9_3 = D3D_FEATURE_LEVEL_9_3,

    /// <summary>
    /// Targets features supported by Direct3D 10.0, including shader model 4.
    /// </summary>
    FeatureLevel10_0 = D3D_FEATURE_LEVEL_10_0,

    /// <summary>
    /// Targets features supported by Direct3D 10.1, including shader model 4.
    /// </summary>
    FeatureLevel10_1 = D3D_FEATURE_LEVEL_10_1,

    /// <summary>
    /// Targets features supported by Direct3D 11.0, including shader model 5.
    /// </summary>
    FeatureLevel11_0 = D3D_FEATURE_LEVEL_11_0,

    /// <summary>
    /// Targets features supported by Direct3D 11.1, including shader model 5 and logical blend operations. This
    /// feature level requires a display driver that is at least implemented to WDDM for Windows 8 (WDDM 1.2).
    /// </summary>
    FeatureLevel11_1 = D3D_FEATURE_LEVEL_11_1,

    /// <summary>
    /// Targets features supported by Direct3D 12.0, including shader model 5.
    /// </summary>
    FeatureLevel12_0 = D3D_FEATURE_LEVEL_12_0,

    /// <summary>
    /// Targets features supported by Direct3D 12.1, including shader model 5.
    /// </summary>
    FeatureLevel12_1 = D3D_FEATURE_LEVEL_12_1,

    /// <summary>
    /// Targets features supported by Direct3D 12.2, including shader model 6.5. For more information about feature
    /// level 12_2, see its <see href="https://microsoft.github.io/DirectX-Specs/d3d/D3D12_FeatureLevel12_2.html">specification page</see>.
    /// Feature level 12_2 is available in Windows SDK builds 20170 and later.
    /// </summary>
    FeatureLevel12_2 = D3D_FEATURE_LEVEL_12_2
}