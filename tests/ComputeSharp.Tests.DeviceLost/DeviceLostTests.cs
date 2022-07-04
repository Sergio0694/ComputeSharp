using System;
using System.Threading.Tasks;
using ComputeSharp.Interop;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;

#pragma warning disable CA1416

namespace ComputeSharp.Tests.DeviceLost;

[TestClass]
[TestCategory("DeviceLost")]
public partial class DeviceLostTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public async Task DeviceLost_RaiseEvent(Device device)
    {
        GraphicsDevice graphicsDevice = device.Get();

        TaskCompletionSource<(object? Sender, DeviceLostReason Reason)> tcs = new();

        // Register the device lost callback
        graphicsDevice.DeviceLost += (s, e) => tcs.SetResult((s, e));

        RemoveDevice(graphicsDevice);

        // Wait up to a second for the event to be raised (it's raised asynchronously on a thread pool thread)
        await Task.WhenAny(tcs.Task, Task.Delay(100));

        // Ensure the event has been raised, and get the results
        Assert.IsTrue(tcs.Task.IsCompleted);
        Assert.AreEqual(tcs.Task.Status, TaskStatus.RanToCompletion);

        (object? sender, DeviceLostReason reason) = tcs.Task.Result;

        Assert.IsNotNull(sender);
        Assert.AreSame(sender, graphicsDevice);
        Assert.AreEqual(reason, DeviceLostReason.DeviceRemoved);
    }

    /// <summary>
    /// Removes the underlying device for a given <see cref="GraphicsDevice"/> instance.
    /// </summary>
    /// <param name="graphicsDevice">The target <see cref="GraphicsDevice"/> instance.</param>
    private static unsafe void RemoveDevice(GraphicsDevice graphicsDevice)
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
                d3D12Device->Release();
            }
        }
    }
}
