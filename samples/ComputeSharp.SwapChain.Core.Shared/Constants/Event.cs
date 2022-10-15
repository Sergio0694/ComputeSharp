namespace ComputeSharp.SwapChain.Core.Constants;

/// <summary>
/// A <see langword="class"/> with the collection of tracked events for analytics.
/// </summary>
public static class Event
{
    public const string IsVerticalSyncEnabledChanged = "[SETTINGS] IsVerticalSyncEnabled";
    public const string IsDynamicResolutionEnabledChanged = "[SETTINGS] IsDynamicResolutionEnabled";
    public const string SelectedResolutionScaleChanged = "[SETTINGS] SelectedResolutionScale";
    public const string SelectedComputeShaderChanged = "[SETTINGS] SelectedComputeShader";
    public const string IsRenderingPausedChanged = "[SETTINGS] IsRenderingPaused";
    public const string OpenShaderSelectionPanel = "[SHELL] Open ShaderSelectionPanel";
    public const string CloseShaderSelectionPanel = "[SHELL] Close ShaderSelectionPanel";
}