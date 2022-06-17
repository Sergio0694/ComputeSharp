using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CA1416

namespace ComputeSharp.Graphics.Helpers;

/// <inheritdoc/>
internal static partial class DeviceHelper
{
    /// <summary>
    /// The local cache of <see cref="GraphicsDevice"/> instances that are currently usable.
    /// </summary>
    internal static readonly Dictionary<Luid, GraphicsDevice> DevicesCache = new();

    /// <summary>
    /// The local map of <see cref="ID3D12InfoQueue"/> instances for the existing devices.
    /// </summary>
    private static readonly Dictionary<Luid, ComPtr<ID3D12InfoQueue>> D3D12InfoQueueMap = new();

    /// <summary>
    /// Indicates whether or not DRED is enabled (see <see href="https://devblogs.microsoft.com/directx/dred/"/>).
    /// </summary>
    private static volatile bool IsDeviceRemovedExtendedDataConfigurationEnabled;

    /// <summary>
    /// Ensures the DRED settings are enabled, if not already. If DRED is not configured, this method does nothing.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe void EnsureDeviceRemovedExtendedDataConfiguration()
    {
        // Enables DRED, if not enabled already
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void EnableDeviceRemovedExtendedDataConfigurationIfNeeded()
        {
            if (IsDeviceRemovedExtendedDataConfigurationEnabled)
            {
                return;
            }

            lock (DevicesCache)
            {
                if (IsDeviceRemovedExtendedDataConfigurationEnabled)
                {
                    return;
                }

                using ComPtr<ID3D12DeviceRemovedExtendedDataSettings> d3D12DeviceRemovedExtendedDataSettings = default;

                DirectX.D3D12GetDebugInterface(
                    riid: Windows.__uuidof<ID3D12DeviceRemovedExtendedDataSettings>(),
                    ppvDebug: d3D12DeviceRemovedExtendedDataSettings.GetVoidAddressOf()).Assert();

                // Enable the auto-breadcrumbs and page faults reporting
                d3D12DeviceRemovedExtendedDataSettings.Get()->SetAutoBreadcrumbsEnablement(D3D12_DRED_ENABLEMENT.D3D12_DRED_ENABLEMENT_FORCED_ON);
                d3D12DeviceRemovedExtendedDataSettings.Get()->SetPageFaultEnablement(D3D12_DRED_ENABLEMENT.D3D12_DRED_ENABLEMENT_FORCED_ON);

                IsDeviceRemovedExtendedDataConfigurationEnabled = true;
            }
        }

        // If the app context switch is set, ensure the DRED setting is enabled
        if (Configuration.IsDeviceRemovedExtendedDataEnabled)
        {
            EnableDeviceRemovedExtendedDataConfigurationIfNeeded();
        }
    }

    /// <summary>
    /// Retrieves a <see cref="GraphicsDevice"/> instance for an <see cref="ID3D12Device"/> object.
    /// </summary>
    /// <param name="d3D12Device">The <see cref="ID3D12Device"/> to use to get a <see cref="GraphicsDevice"/> instance.</param>
    /// <param name="dxgiAdapter">The <see cref="IDXGIAdapter"/> that <paramref name="d3D12Device"/> was created from.</param>
    /// <param name="dxgiDescription1">The available info for the <see cref="GraphicsDevice"/> instance.</param>
    /// <returns>A <see cref="GraphicsDevice"/> instance for the input device.</returns>
    private static unsafe GraphicsDevice GetOrCreateDevice(ID3D12Device* d3D12Device, IDXGIAdapter* dxgiAdapter, DXGI_ADAPTER_DESC1* dxgiDescription1)
    {
        lock (DevicesCache)
        {
            Luid luid = Luid.FromLUID(dxgiDescription1->AdapterLuid);

            if (!DevicesCache.TryGetValue(luid, out GraphicsDevice? device))
            {
                device = new GraphicsDevice(d3D12Device, dxgiAdapter, dxgiDescription1);

                DevicesCache.Add(luid, device);

                if (Configuration.IsDebugOutputEnabled)
                {
                    D3D12InfoQueueMap.Add(luid, d3D12Device->CreateInfoQueue());
                }
            }

            return device;
        }
    }

    /// <summary>
    /// Removes a <see cref="GraphicsDevice"/> from the internal cache.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to remove from the internal cache.</param>
    public static void NotifyDisposedDevice(GraphicsDevice device)
    {
        lock (DevicesCache)
        {
            DevicesCache.Remove(device.Luid);

            if (Configuration.IsDebugOutputEnabled)
            {
                D3D12InfoQueueMap.Remove(device.Luid, out ComPtr<ID3D12InfoQueue> queue);

                queue.Dispose();
            }
        }
    }
}
