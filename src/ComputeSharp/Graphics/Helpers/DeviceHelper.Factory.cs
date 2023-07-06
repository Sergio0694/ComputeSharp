using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Helpers;

/// <inheritdoc/>
unsafe partial class DeviceHelper
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
    private static GraphicsDevice? defaultDevice;

    /// <summary>
    /// The global <see cref="ID3D12MemoryAllocatorFactory"/> instance to use, if any.
    /// </summary>
    private static ID3D12MemoryAllocatorFactory* globalAllocatorFactory;

    /// <summary>
    /// Gets the cached <see cref="GraphicsDevice"/> instance or creates and caches a new one.
    /// </summary>
    /// <returns>The default <see cref="GraphicsDevice"/> instance.</returns>
    public static GraphicsDevice GetDefaultDeviceFromCacheOrCreateInstance()
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

            _ = DevicesCache.Remove(device.Luid);

            if (Configuration.IsDebugOutputEnabled)
            {
                _ = D3D12InfoQueueMap.Remove(device.Luid, out ComPtr<ID3D12InfoQueue> queue);

                queue.Dispose();
            }
        }
    }

    /// <summary>
    /// Configures the global <c>ID3D12MemoryAllocatorFactory</c> instance to use for the current process.
    /// </summary>
    /// <param name="allocatorFactory"></param>
    public static void ConfigureAllocatorFactory(ID3D12MemoryAllocatorFactory* allocatorFactory)
    {
        lock (DevicesCache)
        {
            default(InvalidOperationException).ThrowIf(DevicesCache.Count != 0, "The allocator factory can only be configured before creating devices.");
            default(InvalidOperationException).ThrowIf(globalAllocatorFactory is not null, "The allocator factory can only be configured once.");

            _ = ((IUnknown*)allocatorFactory)->AddRef();

            globalAllocatorFactory = allocatorFactory;
        }
    }

    /// <summary>
    /// Retrieves a <see cref="GraphicsDevice"/> instance for an <see cref="ID3D12Device"/> object.
    /// </summary>
    /// <param name="d3D12Device">The <see cref="ID3D12Device"/> to use to get a <see cref="GraphicsDevice"/> instance.</param>
    /// <param name="dxgiAdapter">The <see cref="IDXGIAdapter"/> that <paramref name="d3D12Device"/> was created from.</param>
    /// <param name="dxgiDescription1">The available info for the <see cref="GraphicsDevice"/> instance.</param>
    /// <returns>A <see cref="GraphicsDevice"/> instance for the input device.</returns>
    private static GraphicsDevice GetOrCreateDevice(ID3D12Device* d3D12Device, IDXGIAdapter* dxgiAdapter, DXGI_ADAPTER_DESC1* dxgiDescription1)
    {
        lock (DevicesCache)
        {
            Luid luid = Luid.FromLUID(dxgiDescription1->AdapterLuid);

            if (!DevicesCache.TryGetValue(luid, out GraphicsDevice? device))
            {
                using ComPtr<ID3D12MemoryAllocator> memoryAllocator = default;

                // Try to create a new ID3D12MemoryAllocator instance for the new device
                _ = TryGetMemoryAllocator(d3D12Device, dxgiAdapter, memoryAllocator.GetAddressOf());

                // Create the device passing the underlying device, adapter, and optional allocator
                device = new GraphicsDevice(d3D12Device, dxgiAdapter, dxgiDescription1, memoryAllocator.Get());

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
    /// Tries to create a new <see cref="ID3D12MemoryAllocator"/> instance for a given device.
    /// </summary>
    /// <param name="d3D12Device">The <see cref="ID3D12Device"/> to try to create an allocator for.</param>
    /// <param name="dxgiAdapter">The <see cref="IDXGIAdapter"/> that <paramref name="d3D12Device"/> was created from.</param>
    /// <param name="memoryAllocator">The resulting <see cref="ID3D12MemoryAllocator"/> instance.</param>
    /// <returns>Whether an <see cref="ID3D12MemoryAllocator"/> instance could be created successfully.</returns>
    private static bool TryGetMemoryAllocator(ID3D12Device* d3D12Device, IDXGIAdapter* dxgiAdapter, ID3D12MemoryAllocator** memoryAllocator)
    {
        ID3D12MemoryAllocatorFactory* allocatorFactory = globalAllocatorFactory;

        if (allocatorFactory is null)
        {
            *memoryAllocator = null;

            return false;
        }

        allocatorFactory->CreateAllocator(d3D12Device, dxgiAdapter, memoryAllocator).Assert();

        return true;
    }
}