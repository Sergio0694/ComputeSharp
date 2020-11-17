using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;
using HRESULT = System.Int32;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// A <see langword="class"/> with methods to inspect the available devices on the current machine.
    /// </summary>
    internal static class DeviceHelper2
    {
        /// <summary>
        /// Gets whether or not there is a default device available, without creating it.
        /// </summary>
        /// <returns>Whether or not there is a device supporting at least DX12.0.</returns>   
        [Pure]
        public static unsafe bool IsDefaultDeviceAvailable()
        {
            DXGI_ADAPTER_DESC1 dxgiDescription1;

            return TryGetDefaultDevice(null, &dxgiDescription1);
        }

        /// <summary>
        /// Gets the default <see cref="GraphicsDevice2"/> instance.
        /// </summary>
        /// <returns>The default <see cref="GraphicsDevice2"/> instance supporting at least DX12.0.</returns>
        /// <exception cref="NotSupportedException">Thrown when a default device is not available.</exception>
        public static unsafe GraphicsDevice2 GetDefaultDevice()
        {
            using ComPtr<ID3D12Device> d3d12device = default;

            DXGI_ADAPTER_DESC1 dxgiDescription1;

            if (TryGetDefaultDevice(d3d12device.GetAddressOf(), &dxgiDescription1))
            {
                return new GraphicsDevice2(d3d12device.Move(), &dxgiDescription1);
            }

            static GraphicsDevice2 Throw()
            {
                throw new NotSupportedException("There isn't a supported GPU device on the current machine");
            }

            return Throw();
        }

        /// <summary>
        /// Tries to check or create a default <see cref="ID3D12Device"/> object.
        /// </summary>
        /// <param name="d3d12device">A pointer to the <see cref="ID3D12Device"/> object to create, or <see langword="null"/>.</param>
        /// <param name="dxgiDescription1">A pointer to the <see cref="DXGI_ADAPTER_DESC1"/> value for the device found.</param>
        /// <returns>Whether a default device was found with the requested feature level.</returns>
        public static unsafe bool TryGetDefaultDevice(ID3D12Device** d3d12device, DXGI_ADAPTER_DESC1* dxgiDescription1)
        {
            using ComPtr<IDXGIFactory4> dxgiFactory4 = default;

            Guid dxgiFactory4Guid = FX.IID_IDXGIFactory4;

            EnableDebugMode();

            FX.CreateDXGIFactory2(IDXGIFactoryCreationFlags, &dxgiFactory4Guid, dxgiFactory4.GetVoidAddressOf()).Assert();

            uint i = 0;

            while (true)
            {
                using ComPtr<IDXGIAdapter1> dxgiAdapter1 = default;

                HRESULT enumAdapters1Result = dxgiFactory4.Get()->EnumAdapters1(i++, dxgiAdapter1.GetAddressOf());

                if (enumAdapters1Result == FX.DXGI_ERROR_NOT_FOUND)
                {
                    return false;
                }

                enumAdapters1Result.Assert();

                dxgiAdapter1.Get()->GetDesc1(dxgiDescription1).Assert();

                if (dxgiDescription1->DedicatedVideoMemory == 0) continue;

                Guid d3d12DeviceGuid = FX.IID_ID3D12Device;

                HRESULT createDeviceResult = FX.D3D12CreateDevice(
                    dxgiAdapter1.Upcast<IDXGIAdapter1, IUnknown>().Get(),
                    D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_0,
                    &d3d12DeviceGuid,
                    (void**)d3d12device);

                if (FX.SUCCEEDED(createDeviceResult))
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// The creation flags for <see cref="IDXGIFactory"/> instances.
        /// </summary>
        private const uint IDXGIFactoryCreationFlags =
#if DEBUG
            FX.DXGI_CREATE_FACTORY_DEBUG;
#else
            0;
#endif

        /// <summary>
        /// Enables the debug layer for DirectX APIs.
        /// </summary>
        [Conditional("DEBUG")]
        private static unsafe void EnableDebugMode()
        {
            using ComPtr<ID3D12Debug> d3D12Debug = default;
            using ComPtr<ID3D12Debug1> d3D12Debug1 = default;

            Guid d3D12DebugGuid = FX.IID_ID3D12Debug;

            FX.D3D12GetDebugInterface(&d3D12DebugGuid, d3D12Debug.GetVoidAddressOf()).Assert();

            d3D12Debug.Get()->EnableDebugLayer();

            Guid d3D12Debug1Guid = FX.IID_ID3D12Debug1;

            if (FX.SUCCEEDED(d3D12Debug.CopyTo(d3D12Debug1.GetAddressOf())))
            {
                d3D12Debug1.Get()->SetEnableGPUBasedValidation(FX.TRUE);
                d3D12Debug1.Get()->SetEnableSynchronizedCommandQueueValidation(FX.TRUE);
            }
        }
    }
}
