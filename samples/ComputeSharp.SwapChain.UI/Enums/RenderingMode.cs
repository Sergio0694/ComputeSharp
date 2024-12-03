namespace ComputeSharp.SwapChain.Core.Enums;

/// <summary>
/// Indicates the rendering mode being used.
/// </summary>
public enum RenderingMode
{
    /// <summary>
    /// Render using DirectX 12 APIs.
    /// </summary>
    DirectX12,

    /// <summary>
    /// Render using Win2D (ie. Direct2D behind the scenes).
    /// </summary>
    Win2D
}