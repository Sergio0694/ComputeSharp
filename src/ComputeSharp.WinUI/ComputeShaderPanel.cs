using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Threading;
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
        private readonly ISwapChainPanelNative* swapChainPanelNative;

        private Thread? renderThread;

        /// <summary>
        /// Creates a new <see cref="ComputeShaderPanel"/> instance.
        /// </summary>
        public ComputeShaderPanel()
        {
            this.swapChainPanelNative = GetISwapChainPanelNative();

            this.Loaded += ComputeShaderPanel_Loaded;
            this.SizeChanged += ComputeShaderPanel_SizeChanged;
        }

        private void ComputeShaderPanel_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = XamlRoot.As<IXamlRootNative>().HostWindow;

            OnInitialize(hwnd);
            OnResize();

            using ComPtr<IDXGISwapChain> idxgiSwapChain = default;

            _ = this.dxgiSwapChain1.CopyTo(&idxgiSwapChain);

            this.swapChainPanelNative->SetSwapChain(idxgiSwapChain.Get());

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

        private void ComputeShaderPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OnResize();
        }

        /// <summary>
        /// Gets a pointer to the underlying <see cref="ISwapChainPanelNative"/> object.
        /// </summary>
        /// <returns>The <see cref="ISwapChainPanelNative"/> pointer for the object in use.</returns>
        [Pure]
        private ISwapChainPanelNative* GetISwapChainPanelNative()
        {
            IObjectReference swapChainAsIUnknown = ((IWinRTObject)this).NativeObject;

            IUnknown* iUnknown = (IUnknown*)swapChainAsIUnknown.ThisPtr;
            ISwapChainPanelNative* iSwapChainPanelNative = default;

            Guid iSwapChainPanelNativeUuid = Guid.Parse("63AAD0B8-7C24-40FF-85A8-640D944CC325");

            _ = iUnknown->QueryInterface(
                &iSwapChainPanelNativeUuid,
                (void**)&iSwapChainPanelNative);

            return iSwapChainPanelNative;
        }

        /// <summary>
        /// An interface for the COM object behind <see cref="XamlRoot"/>.
        /// </summary>
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("4CCD7521-9C08-41AD-A5BD-B263EF64C9E7")]
        private interface IXamlRootNative
        {
            /// <summary>
            /// Gets the handle for the host window containing the current instance.
            /// </summary>
            IntPtr HostWindow { get; }
        }
    }
}
