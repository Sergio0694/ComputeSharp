using System;
using System.Diagnostics;
using System.Threading;
using ComputeSharp.Core.Extensions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TerraFX.Interop;
using WinRT;

namespace ComputeSharp.WinUI
{
    /// <summary>
    /// A custom <see cref="SwapChainPanel"/> that can be used to render animated backgrounds.
    /// It is powered by ComputeSharp and leverages compute shaders to create frames to display.
    /// </summary>
    public sealed unsafe partial class ComputeShaderPanel : SwapChainPanel
    {
        private Thread? renderThread;

        /// <summary>
        /// Creates a new <see cref="ComputeShaderPanel"/> instance.
        /// </summary>
        public ComputeShaderPanel()
        {
            this.Loaded += ComputeShaderPanel_Loaded;
            this.SizeChanged += ComputeShaderPanel_SizeChanged;
            this.CompositionScaleChanged += ComputeShaderPanel_CompositionScaleChanged;
        }

        // Initializes the swap chain and starts the render thread
        private void ComputeShaderPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.width = ActualWidth;
            this.height = ActualHeight;
            this.compositionScaleX = CompositionScaleX;
            this.compositionScaleY = CompositionScaleY;
            this.resolutionScale = ResolutionScale;

            OnInitialize();

            // Extract the ISwapChainPanelNative reference from the current panel, then query the
            // IDXGISwapChain reference just created and set that as the swap chain panel to use.
            using (ComPtr<ISwapChainPanelNative> swapChainPanelNative = default)
            {
                IUnknown* swapChainPanel = (IUnknown*)((IWinRTObject)this).NativeObject.ThisPtr;
                Guid iSwapChainPanelNativeUuid = Guid.Parse("63AAD0B8-7C24-40FF-85A8-640D944CC325");

                swapChainPanel->QueryInterface(
                    &iSwapChainPanelNativeUuid,
                    (void**)&swapChainPanelNative).Assert();

                using ComPtr<IDXGISwapChain> idxgiSwapChain = default;

                this.dxgiSwapChain1.CopyTo(&idxgiSwapChain).Assert();

                swapChainPanelNative.Get()->SetSwapChain(idxgiSwapChain.Get()).Assert();
            }

            this.renderThread = new Thread(static args =>
            {
                var panel = (ComputeShaderPanel)args!;

                Stopwatch
                    startStopwatch = Stopwatch.StartNew(),
                    frameStopwatch = Stopwatch.StartNew();

                const long targetFrameTimeInTicksFor60fps = 166666;

                panel.OnUpdate(TimeSpan.Zero);

                while (true)
                {
                    if (frameStopwatch.ElapsedTicks >= targetFrameTimeInTicksFor60fps)
                    {
                        frameStopwatch.Restart();

                        panel.OnUpdate(startStopwatch.Elapsed);
                    }
                }
            });

            this.renderThread.Start(this);
        }

        // Updates the background store for the frame size factors used by the render thread
        private void ComputeShaderPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.width = e.NewSize.Width;
            this.height = e.NewSize.Height;

            OnResize();
        }

        // Updates the background store for the composition scale factors used by the render thread
        private void ComputeShaderPanel_CompositionScaleChanged(SwapChainPanel sender, object args)
        {
            this.compositionScaleX = CompositionScaleX;
            this.compositionScaleY = CompositionScaleY;

            OnResize();
        }
    }
}
