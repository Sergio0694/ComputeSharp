using System;
using System.Diagnostics.Contracts;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Extensions;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;
using HRESULT = System.Int32;
using static TerraFX.Interop.D3D_FEATURE_LEVEL;
using static TerraFX.Interop.D3D_SHADER_MODEL;
using static TerraFX.Interop.DXGI_GPU_PREFERENCE;

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
    /// The <see cref="Lazy{T}"/> instance used to produce the default <see cref="GraphicsDevice"/> instance.
    /// </summary>
    public static readonly Lazy<GraphicsDevice> DefaultFactory = new(GetDefaultDevice);

    /// <summary>
    /// Gets the <see cref="Luid"/> of the default device.
    /// </summary>
    /// <returns>The <see cref="Luid"/> of the default device supporting <see cref="D3D_FEATURE_LEVEL_11_0"/> and <see cref="D3D_SHADER_MODEL_6_0"/>.</returns>
    /// <remarks>This methods assumes that a default device is available.</remarks>
    [Pure]
    public static unsafe Luid GetDefaultDeviceLuid()
    {
        if (DefaultFactory.IsValueCreated)
        {
            return DefaultFactory.Value.Luid;
        }

        DXGI_ADAPTER_DESC1 dxgiDescription1;

        _ = TryGetDefaultDevice(null, null, &dxgiDescription1) || TryGetWarpDevice(null, null, &dxgiDescription1);

        return Luid.FromLUID(dxgiDescription1.AdapterLuid);
    }

    /// <summary>
    /// Gets the default <see cref="GraphicsDevice"/> instance.
    /// </summary>
    /// <returns>The default <see cref="GraphicsDevice"/> instance supporting <see cref="D3D_FEATURE_LEVEL_11_0"/> and <see cref="D3D_SHADER_MODEL_6_0"/>.</returns>
    /// <exception cref="NotSupportedException">Thrown when a default device is not available.</exception>
    private static unsafe GraphicsDevice GetDefaultDevice()
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
                FX.__uuidof<IDXGIAdapter1>(),
                dxgiAdapter1.GetVoidAddressOf());

            if (enumAdapters1Result == FX.DXGI_ERROR_NOT_FOUND)
            {
                return false;
            }

            enumAdapters1Result.Assert();

            dxgiAdapter1.Get()->GetDesc1(dxgiDescription1).Assert();

            if (dxgiDescription1->VendorId == MicrosoftVendorId &&
                dxgiDescription1->DeviceId == WarpDeviceId)
            {
                continue;
            }

            // Explicit paths for when a device is being retrieved or not, with special handling
            // for the additional check that is required for the SM6 level. This can't be checked
            // without creating a device first, so the path for when the target device pointer is
            // null is useful to do an initial filtering using D3D12CreateDevice to avoid creating
            // a device for adapters that would've failed at the FL11 check already.
            if (d3D12Device == null)
            {
                HRESULT createDeviceResult = FX.D3D12CreateDevice(
                    dxgiAdapter1.AsIUnknown().Get(),
                    D3D_FEATURE_LEVEL_11_0,
                    FX.__uuidof<ID3D12Device>(),
                    null);

                if (FX.SUCCEEDED(createDeviceResult))
                {
                    using ComPtr<ID3D12Device> d3D12DeviceCandidate = default;

                    createDeviceResult = FX.D3D12CreateDevice(
                        dxgiAdapter1.AsIUnknown().Get(),
                        D3D_FEATURE_LEVEL_11_0,
                        FX.__uuidof<ID3D12Device>(),
                        d3D12DeviceCandidate.GetVoidAddressOf());

                    if (FX.SUCCEEDED(createDeviceResult) &&
                        d3D12DeviceCandidate.Get()->IsShaderModelSupported(D3D_SHADER_MODEL_6_0))
                    {
                        return true;
                    }
                }
            }
            else
            {
                using ComPtr<ID3D12Device> d3D12DeviceCandidate = default;

                HRESULT createDeviceResult = FX.D3D12CreateDevice(
                    dxgiAdapter1.AsIUnknown().Get(),
                    D3D_FEATURE_LEVEL_11_0,
                    FX.__uuidof<ID3D12Device>(),
                    d3D12DeviceCandidate.GetVoidAddressOf());

                if (FX.SUCCEEDED(createDeviceResult) &&
                    d3D12DeviceCandidate.Get()->IsShaderModelSupported(D3D_SHADER_MODEL_6_0))
                {
                    d3D12DeviceCandidate.CopyTo(d3D12Device);
                    dxgiAdapter1.CopyTo(dxgiAdapter);

                    return true;
                }
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
    private static unsafe bool TryGetWarpDevice(ID3D12Device** d3D12Device, IDXGIAdapter** dxgiAdapter, DXGI_ADAPTER_DESC1* dxgiDescription1)
    {
        using ComPtr<IDXGIFactory6> dxgiFactory6 = default;

        CreateDXGIFactory6(dxgiFactory6.GetAddressOf());

        using ComPtr<IDXGIAdapter1> dxgiAdapter1 = default;

        dxgiFactory6.Get()->EnumWarpAdapter(FX.__uuidof<IDXGIAdapter1>(), dxgiAdapter1.GetVoidAddressOf()).Assert();
        
        dxgiAdapter1.Get()->GetDesc1(dxgiDescription1).Assert();

        HRESULT createDeviceResult = FX.D3D12CreateDevice(
            dxgiAdapter1.AsIUnknown().Get(),
            D3D_FEATURE_LEVEL_11_0,
            FX.__uuidof<ID3D12Device>(),
            (void**)d3D12Device);

        dxgiAdapter1.CopyTo(dxgiAdapter);

        return FX.SUCCEEDED(createDeviceResult);
    }
}
