using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Helpers;

/// <inheritdoc/>
partial class DeviceHelper
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
    /// The cached default <see cref="GraphicsDevice"/> instance, if any.
    /// </summary>
    internal static GraphicsDevice? defaultDevice;

    /// <summary>
    /// Gets the cached <see cref="GraphicsDevice"/> instance or creates and caches a new one.
    /// </summary>
    /// <returns>The default <see cref="GraphicsDevice"/> instance.</returns>
    public static unsafe GraphicsDevice GetDefaultDeviceFromCacheOrCreateInstance()
    {
        lock (DevicesCache)
        {
            do
            {
                GraphicsDevice? defaultDevice = DeviceHelper.defaultDevice;

                // Fast path retrieving the cached instance and returning it
                if (defaultDevice is not null)
                {
                    return defaultDevice;
                }

                [MethodImpl(MethodImplOptions.NoInlining)]
                static void InitializeAndAssignDefaultDevice()
                {
                    DeviceHelper.defaultDevice = GetOrCreateDefaultDevice();
                }

                // Non inlined method to load the default device and assign it to the
                // cached field. If this method returns, then the default device has
                // correctly been initialized. Otherwise, the method will just throw.
                InitializeAndAssignDefaultDevice();
            }
            while (true);
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
            // Remove the default device from the cache, if it has been disposed
            if (device == defaultDevice)
            {
                defaultDevice = null;
            }

            DevicesCache.Remove(device.Luid);

            if (Configuration.IsDebugOutputEnabled)
            {
                D3D12InfoQueueMap.Remove(device.Luid, out ComPtr<ID3D12InfoQueue> queue);

                queue.Dispose();
            }
        }
    }
}