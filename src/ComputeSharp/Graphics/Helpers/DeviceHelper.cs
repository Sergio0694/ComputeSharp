using System;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D_FEATURE_LEVEL;
using static TerraFX.Interop.DirectX.D3D_SHADER_MODEL;
using static TerraFX.Interop.DirectX.DXGI_GPU_PREFERENCE;

#pragma warning disable CA1416

namespace ComputeSharp.Graphics.Helpers;

/// <summary>
/// A <see langword="class"/> with methods to inspect the available devices on the current machine.
/// </summary>
internal static partial class DeviceHelper
{
    /// <summary>
    /// The vendor id for Microsoft adapters (the "Microsoft Basic Render").
    /// </summary>
    private const uint MicrosoftVendorId = 0x1414;

    /// <summary>
    /// The device id for the WARP device.
    /// </summary>
    private const uint WarpDeviceId = 0x8C;

    /// <summary>
    /// Gets the default <see cref="GraphicsDevice"/> instance.
    /// </summary>
    /// <returns>The default <see cref="GraphicsDevice"/> instance supporting <see cref="D3D_FEATURE_LEVEL_11_0"/> and <see cref="D3D_SHADER_MODEL_6_0"/>.</returns>
    /// <exception cref="NotSupportedException">Thrown when a default device is not available.</exception>
    private static unsafe GraphicsDevice GetOrCreateDefaultDevice()
    {
        using ComPtr<ID3D12Device> d3D12Device = default;
        using ComPtr<IDXGIAdapter> dxgiAdapter = default;

        DXGI_ADAPTER_DESC1 dxgiDescription1;

        if (TryGetDefaultDevice(d3D12Device.GetAddressOf(), dxgiAdapter.GetAddressOf(), &dxgiDescription1) ||
            TryGetWarpDevice(d3D12Device.GetAddressOf(), dxgiAdapter.GetAddressOf(), &dxgiDescription1))
        {
            return GetOrCreateDevice(d3D12Device.Get(), dxgiAdapter.Get(), &dxgiDescription1);
        }

        return ThrowHelper.ThrowNotSupportedException<GraphicsDevice>("Failed to retrieve the default device.");
    }

    /// <summary>
    /// Tries to check or create a default <see cref="ID3D12Device"/> object.
    /// </summary>
    /// <param name="d3D12Device">A pointer to the <see cref="ID3D12Device"/> object to create, or <see langword="null"/>.</param>
    /// <param name="dxgiAdapter">A pointer to the <see cref="IDXGIAdapter"/> object used to create <paramref name="d3D12Device"/>, or <see langword="null"/>.</param>
    /// <param name="dxgiDescription1">A pointer to the <see cref="DXGI_ADAPTER_DESC1"/> value for the device found.</param>
    /// <returns>Whether a default device was found with the requested feature level.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the target device was lost and incorrectly disposed (see remarks).</exception>
    private static unsafe bool TryGetDefaultDevice(ID3D12Device** d3D12Device, IDXGIAdapter** dxgiAdapter, DXGI_ADAPTER_DESC1* dxgiDescription1)
    {
        using ComPtr<IDXGIFactory6> dxgiFactory6 = default;

        CreateDXGIFactory6(dxgiFactory6.GetAddressOf());

        uint i = 0;

        while (true)
        {
            using ComPtr<IDXGIAdapter1> dxgiAdapter1 = default;

            HRESULT enumAdapters1Result = dxgiFactory6.Get()->EnumAdapterByGpuPreference(
                i++,
                DXGI_GPU_PREFERENCE_HIGH_PERFORMANCE,
                Windows.__uuidof<IDXGIAdapter1>(),
                dxgiAdapter1.GetVoidAddressOf());

            if (enumAdapters1Result == DXGI.DXGI_ERROR_NOT_FOUND)
            {
                return false;
            }

            enumAdapters1Result.Assert();

            dxgiAdapter1.Get()->GetDesc1(dxgiDescription1).Assert();

            // Skip the WARP adapter
            if (dxgiDescription1->VendorId == MicrosoftVendorId &&
                dxgiDescription1->DeviceId == WarpDeviceId)
            {
                continue;
            }

            using ComPtr<ID3D12Device> d3D12DeviceCandidate = default;

            // Try to create a device for the current adapter. There are 3 possible results here:
            //   1) The call succeeds. In this case the first device retrieved is the default one.
            //   2) The call fails for a device lost reason. This indicates a default device was
            //      previously retrieved but not disposed correctly. In this case, the code throws.
            //   3) The call fails for other reasons. In this case the adapter is simply skipped.
            //      This might be the case if an adapter doesn't support the requested feature level.
            HRESULT createDeviceResult = DirectX.D3D12CreateDevice(
                dxgiAdapter1.AsIUnknown().Get(),
                D3D_FEATURE_LEVEL_11_0,
                Windows.__uuidof<ID3D12Device>(),
                d3D12DeviceCandidate.GetVoidAddressOf());

            // Check and throw if the device is a device lost state and not disposed properly
            if (createDeviceResult.IsDeviceLostReason())
            {
                ThrowHelper.ThrowInvalidOperationException("The default device is in device lost state and has not been disposed properly.");
            }

            // Check for success, and then also filter out devices that support SM6.0. This can
            // only be checked on a concrete ID3D12Device instance, so it can't be done earlier.
            if (Windows.SUCCEEDED(createDeviceResult) &&
                d3D12DeviceCandidate.Get()->IsShaderModelSupported(D3D_SHADER_MODEL_6_0))
            {
                d3D12DeviceCandidate.CopyTo(d3D12Device);
                dxgiAdapter1.CopyTo(dxgiAdapter);

                return true;
            }
        }
    }

    /// <summary>
    /// Tries to check or create a warp <see cref="ID3D12Device"/> object.
    /// </summary>
    /// <param name="d3D12Device">A pointer to the <see cref="ID3D12Device"/> object to create, or <see langword="null"/>.</param>
    /// <param name="dxgiAdapter">A pointer to the <see cref="IDXGIAdapter"/> object used to create <paramref name="d3D12Device"/>, or <see langword="null"/>.</param>
    /// <param name="dxgiDescription1">A pointer to the <see cref="DXGI_ADAPTER_DESC1"/> value for the device found.</param>
    /// <returns>Whether a warp device was created successfully.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the target device was lost and incorrectly disposed (see remarks).</exception>
    private static unsafe bool TryGetWarpDevice(ID3D12Device** d3D12Device, IDXGIAdapter** dxgiAdapter, DXGI_ADAPTER_DESC1* dxgiDescription1)
    {
        using ComPtr<IDXGIFactory6> dxgiFactory6 = default;

        CreateDXGIFactory6(dxgiFactory6.GetAddressOf());

        using ComPtr<IDXGIAdapter1> dxgiAdapter1 = default;

        dxgiFactory6.Get()->EnumWarpAdapter(Windows.__uuidof<IDXGIAdapter1>(), dxgiAdapter1.GetVoidAddressOf()).Assert();
        
        dxgiAdapter1.Get()->GetDesc1(dxgiDescription1).Assert();

        using ComPtr<ID3D12Device> d3D12DeviceCandidate = default;

        HRESULT createDeviceResult = DirectX.D3D12CreateDevice(
            dxgiAdapter1.AsIUnknown().Get(),
            D3D_FEATURE_LEVEL_11_0,
            Windows.__uuidof<ID3D12Device>(),
            d3D12DeviceCandidate.GetVoidAddressOf());

        if (createDeviceResult.IsDeviceLostReason())
        {
            ThrowHelper.ThrowInvalidOperationException("The default device is in device lost state and has not been disposed properly.");
        }

        // There is no need to check for SM6.0, as it's guaranteed to be supported by Windows
        if (Windows.SUCCEEDED(createDeviceResult))
        {
            d3D12DeviceCandidate.CopyTo(d3D12Device);
            dxgiAdapter1.CopyTo(dxgiAdapter);

            return true;
        }

        return false;
    }
}
