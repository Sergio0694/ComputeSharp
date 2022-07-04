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
[DoNotParallelize]
public partial class DeviceLostTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public async Task DeviceLost_RaiseEvent(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        TaskCompletionSource<(object? Sender, DeviceLostReason Reason)> tcs = new();

        // Register the device lost callback
        graphicsDevice.DeviceLost += (s, e) => tcs.SetResult((s, e));

        RemoveDevice(graphicsDevice);

        // Wait up to a second for the event to be raised (it's raised asynchronously on a thread pool thread)
        await Task.WhenAny(tcs.Task, Task.Delay(1000));

        // Ensure the event has been raised, and get the results
        Assert.IsTrue(tcs.Task.IsCompleted);

        (object? sender, DeviceLostReason reason) = await tcs.Task;

        Assert.IsNotNull(sender);
        Assert.AreSame(sender, graphicsDevice);
        Assert.AreEqual(reason, DeviceLostReason.DeviceRemoved);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ConstantBuffer<>))]
    [Resource(typeof(ReadOnlyBuffer<>))]
    [Resource(typeof(ReadWriteBuffer<>))]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_AllocateBuffer(Device device, Type bufferType)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await RemoveDeviceAsync(graphicsDevice);

        using var _ = graphicsDevice.AllocateBuffer<float>(bufferType, 128);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_AllocateTexture2D(Device device, Type textureType)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await RemoveDeviceAsync(graphicsDevice);

        using var _ = graphicsDevice.AllocateTexture2D<float>(textureType, 128, 128);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_AllocateTexture3D(Device device, Type textureType)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await RemoveDeviceAsync(graphicsDevice);

        using var _ = graphicsDevice.AllocateTexture3D<float>(textureType, 128, 128, 3);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsDoublePrecisionSupportAvailable(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsDoublePrecisionSupportAvailable();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsReadOnlyTexture2DSupportedForType(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsReadOnlyTexture2DSupportedForType<float>();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsReadWriteTexture2DSupportedForType(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsReadWriteTexture2DSupportedForType<float>();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsReadOnlyTexture3DSupportedForType(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsReadOnlyTexture3DSupportedForType<float>();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsReadWriteTexture3DSupportedForType(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsReadWriteTexture3DSupportedForType<float>();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_CreateComputeContext(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await RemoveDeviceAsync(graphicsDevice);

        using var _ = graphicsDevice.CreateComputeContext();
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

    /// <summary>
    /// Removes the underlying device for a given <see cref="GraphicsDevice"/> instance and waits for it to be reported.
    /// </summary>
    /// <param name="graphicsDevice">The target <see cref="GraphicsDevice"/> instance.</param>
    private static async Task RemoveDeviceAsync(GraphicsDevice graphicsDevice)
    {
        TaskCompletionSource<object?> tcs = new();

        graphicsDevice.DeviceLost += (s, e) => tcs.SetResult(null);

        RemoveDevice(graphicsDevice);

        await Task.WhenAny(tcs.Task, Task.Delay(1000));

        if (!tcs.Task.IsCompleted)
        {
            Assert.Fail();
        }
    }
}
