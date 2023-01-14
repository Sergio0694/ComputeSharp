using System;
using System.Threading.Tasks;
using ComputeSharp.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Win32;
using Win32.Graphics.Direct3D12;

namespace ComputeSharp.Tests.DeviceLost.Helpers;

using Win32 = Win32.Apis;

/// <summary>
/// A helper to do common checks on native device objects.
/// </summary>
internal static class GraphicsDeviceHelper
{
    /// <summary>
    /// Gets the underlying <see cref="ID3D12Device"/> object from a <see cref="GraphicsDevice"/> instance.
    /// </summary>
    /// <param name="graphicsDevice">The input <see cref="GraphicsDevice"/> instance.</param>
    /// <param name="d3D12Device">The underlying <see cref="ID3D12Device"/> object for <paramref name="graphicsDevice"/>.</param>
    public static unsafe void GetD3D12Device(GraphicsDevice graphicsDevice, in ComPtr<ID3D12Device> d3D12Device)
    {
        fixed (ID3D12Device** ppvObject = d3D12Device)
        {
            InteropServices.GetID3D12Device(graphicsDevice, Win32.__uuidof<ID3D12Device>(), (void**)ppvObject);
        }
    }

    /// <summary>
    /// Gets the reference count for a given <see cref="ID3D12Device"/> object.
    /// </summary>
    /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> object to get the reference count for.</param>
    public static unsafe uint GetD3D12DeviceRefCount(in ComPtr<ID3D12Device> d3D12Device)
    {
        // Tests using this helper have to ensure a GraphicsDevice instance correctly destroys the underlying
        // device when disposed. To verify this, we first get the underlying ID3D12Device* object, which causes
        // its reference count to be incremented by 1. Assuming GraphicsDevice is working correctly, once
        // it is disposed, the underlying ID3D12Device* object should have its ref count set to just 1,
        // which is the one we added when doing QueryInterface on it. To verify this, we first increment
        // that, and then release it to get the updated count. If everything is correct, the ref count
        // we got back should be 1, with the last one just being from our own ID3D12Device* local. We
        // can't just do Release() and check that, as that'd cause the ref count to just go to 0 but
        // without clearing the pointer stored in the ComPtr<T> local, which would then crash when
        // going out of scope and trying to dereference that object to call Release() on it.
        _ = d3D12Device.Get()->AddRef();

        return d3D12Device.Get()->Release();
    }

    /// <summary>
    /// Removes the underlying device for a given <see cref="GraphicsDevice"/> instance.
    /// </summary>
    /// <param name="graphicsDevice">The target <see cref="GraphicsDevice"/> instance.</param>
    public static unsafe void RemoveDevice(GraphicsDevice graphicsDevice)
    {
        ID3D12Device5* d3D12Device = default;
        Guid d3D12Device5Guid = typeof(ID3D12Device5).GUID;

        if (InteropServices.TryGetID3D12Device(graphicsDevice, &d3D12Device5Guid, (void**)&d3D12Device) != 0)
        {
            Assert.Inconclusive();
        }

        try
        {
            d3D12Device->RemoveDevice();
        }
        finally
        {
            if (d3D12Device is not null)
            {
                _ = d3D12Device->Release();
            }
        }
    }

    /// <summary>
    /// Removes the underlying device for a given <see cref="GraphicsDevice"/> instance and waits for it to be reported.
    /// </summary>
    /// <param name="graphicsDevice">The target <see cref="GraphicsDevice"/> instance.</param>
    /// <remarks>
    /// Removing a device will cause the device removed callback to be invoked on a separate thread, to get a 
    /// reference tracking lease for the device and then raise the event. If this is not awaited, it means
    /// that that callback might race against a thread just calling Dispose(), so that that thread would not
    /// actually cause the object to release resources until the device removed callback has also released its
    /// own lease. To avoid issues related to this, the event should always be awaited after removing a device.
    /// </remarks>
    public static async Task RemoveDeviceAsync(GraphicsDevice graphicsDevice)
    {
        TaskCompletionSource<object?> tcs = new();

        graphicsDevice.DeviceLost += (s, e) => tcs.SetResult(null);

        RemoveDevice(graphicsDevice);

        _ = await tcs.Task;
    }
}