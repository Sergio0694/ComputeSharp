using System;
using System.Diagnostics.Contracts;
using ComputeSharp.Core.Extensions;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;
using HRESULT = System.Int32;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// A <see langword="class"/> with methods to inspect the available devices on the current machine.
    /// </summary>
    internal static partial class DeviceHelper
    {
        /// <summary>
        /// Gets whether or not there is a default device available, without creating it.
        /// </summary>
        /// <returns>Whether or not there is a device supporting at least DX12.0.</returns>   
        [Pure]
        public static unsafe bool IsDefaultDeviceAvailable()
        {
            DXGI_ADAPTER_DESC1 dxgiDescription1;

            return TryGetDefaultDevice(null, &dxgiDescription1) || TryGetWarpDevice(null, &dxgiDescription1);
        }

        /// <summary>
        /// Gets the <see cref="Luid"/> of the default device.
        /// </summary>
        /// <returns>The <see cref="Luid"/> of the default device supporting at least DX12.0.</returns>
        /// <remarks>This methods assumes that a default device is available.</remarks>
        [Pure]
        public static unsafe Luid GetDefaultDeviceLuid()
        {
            DXGI_ADAPTER_DESC1 dxgiDescription1;

            _ = TryGetDefaultDevice(null, &dxgiDescription1) || TryGetWarpDevice(null, &dxgiDescription1);

            return Luid.FromLUID(dxgiDescription1.AdapterLuid);
        }

        /// <summary>
        /// Gets the default <see cref="GraphicsDevice"/> instance.
        /// </summary>
        /// <returns>The default <see cref="GraphicsDevice"/> instance supporting at least DX12.0.</returns>
        /// <exception cref="NotSupportedException">Thrown when a default device is not available.</exception>
        public static unsafe GraphicsDevice GetDefaultDevice()
        {
            using ComPtr<ID3D12Device> d3D12Device = default;

            DXGI_ADAPTER_DESC1 dxgiDescription1;

            if (TryGetDefaultDevice(d3D12Device.GetAddressOf(), &dxgiDescription1) ||
                TryGetWarpDevice(d3D12Device.GetAddressOf(), &dxgiDescription1))
            {
                return GetOrCreateDevice(d3D12Device.Move(), &dxgiDescription1);
            }

            return ThrowHelper.ThrowNotSupportedException<GraphicsDevice>("There isn't a supported GPU device on the current machine");
        }

        /// <summary>
        /// Tries to check or create a default <see cref="ID3D12Device"/> object.
        /// </summary>
        /// <param name="d3D12Device">A pointer to the <see cref="ID3D12Device"/> object to create, or <see langword="null"/>.</param>
        /// <param name="dxgiDescription1">A pointer to the <see cref="DXGI_ADAPTER_DESC1"/> value for the device found.</param>
        /// <returns>Whether a default device was found with the requested feature level.</returns>
        private static unsafe bool TryGetDefaultDevice(ID3D12Device** d3D12Device, DXGI_ADAPTER_DESC1* dxgiDescription1)
        {
            using ComPtr<IDXGIFactory4> dxgiFactory4 = default;

            EnableDebugMode();

            FX.CreateDXGIFactory2(IDXGIFactoryCreationFlags, FX.__uuidof<IDXGIFactory4>(), dxgiFactory4.GetVoidAddressOf()).Assert();

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

                HRESULT createDeviceResult = FX.D3D12CreateDevice(
                    dxgiAdapter1.AsIUnknown().Get(),
                    D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
                    FX.__uuidof<ID3D12Device>(),
                    (void**)d3D12Device);

                if (FX.SUCCEEDED(createDeviceResult))
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Tries to check or create a warp <see cref="ID3D12Device"/> object.
        /// </summary>
        /// <param name="d3D12Device">A pointer to the <see cref="ID3D12Device"/> object to create, or <see langword="null"/>.</param>
        /// <param name="dxgiDescription1">A pointer to the <see cref="DXGI_ADAPTER_DESC1"/> value for the device found.</param>
        /// <returns>Whether a warp device was created successfully.</returns>
        private static unsafe bool TryGetWarpDevice(ID3D12Device** d3D12Device, DXGI_ADAPTER_DESC1* dxgiDescription1)
        {
            using ComPtr<IDXGIFactory4> dxgiFactory4 = default;

            EnableDebugMode();

            FX.CreateDXGIFactory2(IDXGIFactoryCreationFlags, FX.__uuidof<IDXGIFactory4>(), dxgiFactory4.GetVoidAddressOf()).Assert();

            using ComPtr<IDXGIAdapter1> dxgiAdapter1 = default;

            dxgiFactory4.Get()->EnumWarpAdapter(FX.__uuidof<IDXGIAdapter1>(), dxgiAdapter1.GetVoidAddressOf()).Assert();
            
            dxgiAdapter1.Get()->GetDesc1(dxgiDescription1).Assert();

            HRESULT createDeviceResult = FX.D3D12CreateDevice(
                dxgiAdapter1.AsIUnknown().Get(),
                D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
                FX.__uuidof<ID3D12Device>(),
                (void**)d3D12Device);

            return FX.SUCCEEDED(createDeviceResult);
        }

        /// <summary>
        /// The creation flags for <see cref="IDXGIFactory"/> instances.
        /// </summary>
        private const uint IDXGIFactoryCreationFlags = FX.DXGI_CREATE_FACTORY_DEBUG;

        /// <summary>
        /// Enables the debug layer for DirectX APIs.
        /// </summary>
        private static unsafe void EnableDebugMode()
        {
            using ComPtr<ID3D12Debug> d3D12Debug = default;
            using ComPtr<ID3D12Debug1> d3D12Debug1 = default;

            FX.D3D12GetDebugInterface(FX.__uuidof<ID3D12Debug>(), d3D12Debug.GetVoidAddressOf()).Assert();

            d3D12Debug.Get()->EnableDebugLayer();

            if (FX.SUCCEEDED(d3D12Debug.CopyTo(d3D12Debug1.GetAddressOf())))
            {
                d3D12Debug1.Get()->SetEnableGPUBasedValidation(FX.TRUE);
                d3D12Debug1.Get()->SetEnableSynchronizedCommandQueueValidation(FX.TRUE);
            }
        }
    }
}
