using System;
#if WINDOWS_UWP
using ComputeSharp.Uwp.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using ComputeSharp.WinUI.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#endif

#if WINDOWS_UWP
namespace ComputeSharp.Uwp;
#else
namespace ComputeSharp.WinUI;
#endif

/// <summary>
/// <para>
/// A custom <see cref="SwapChainPanel"/> that can be used to render animated backgrounds.
/// It is powered by ComputeSharp and leverages compute shaders to create frames to display.
/// </para>
/// <para>
/// This panel accepts a custom <see cref="IFrameRequestQueue"/> instance to customize frame requests.
/// </para>
/// </summary>
public sealed partial class ComputeShaderPanel : SwapChainPanel, IDisposable
{
    /// <summary>
    /// The <see cref="SwapChainManager{TOwner}"/> instance handling rendering.
    /// </summary>
    private readonly SwapChainManager<ComputeShaderPanel> swapChainManager;

    /// <summary>
    /// Creates a new <see cref="ComputeShaderPanel"/> instance.
    /// </summary>
    public unsafe ComputeShaderPanel()
    {
        this.swapChainManager = new SwapChainManager<ComputeShaderPanel>(this);

        this.Loaded += ComputeShaderPanel_Loaded;
        this.Unloaded += ComputeShaderPanel_Unloaded;
        this.SizeChanged += ComputeShaderPanel_SizeChanged;
        this.CompositionScaleChanged += ComputeShaderPanel_CompositionScaleChanged;
    }

    // Initializes the swap chain and starts the render thread
    private void ComputeShaderPanel_Loaded(object sender, RoutedEventArgs e)
    {
        this.swapChainManager.QueueResize(ActualWidth, ActualHeight);
        this.swapChainManager.QueueCompositionScaleChange(CompositionScaleX, CompositionScaleY);
        this.swapChainManager.QueueResolutionScaleChange(ResolutionScale);
        this.swapChainManager.QueueDynamicResolutionModeChange(IsDynamicResolutionEnabled);
        this.swapChainManager.QueueVerticalSyncModeChange(IsVerticalSyncEnabled);

        if (FrameRequestQueue is IFrameRequestQueue frameRequestQueue &&
            ShaderRunner is IShaderRunner shaderRunner)
        {
            this.swapChainManager.StartRenderLoop(frameRequestQueue, shaderRunner);
        }
    }

    // Requests a cancellation for the render thread
    private void ComputeShaderPanel_Unloaded(object sender, RoutedEventArgs e)
    {
        this.swapChainManager.StopRenderLoop();
    }

    // Updates the background store for the frame size factors used by the render thread
    private void ComputeShaderPanel_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        this.swapChainManager.QueueResize(e.NewSize.Width, e.NewSize.Height);
    }

    // Updates the background store for the composition scale factors used by the render thread
    private void ComputeShaderPanel_CompositionScaleChanged(SwapChainPanel sender, object args)
    {
        this.swapChainManager.QueueCompositionScaleChange(CompositionScaleX, CompositionScaleY);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.swapChainManager.Dispose();
    }
}
