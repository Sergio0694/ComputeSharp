using System;
using System.Threading.Tasks;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.DeviceLost.Helpers;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.DeviceLost;

[TestClass]
[TestCategory("DeviceLost")]
public class DeviceLostTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public async Task DeviceLost_RaiseEvent(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        TaskCompletionSource<(object? Sender, DeviceLostReason Reason)> tcs = new();

        // Register the device lost callback
        graphicsDevice.DeviceLost += (s, e) => tcs.SetResult((s, e));

        GraphicsDeviceHelper.RemoveDevice(graphicsDevice);

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

        await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

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

        await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

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

        await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

        using var _ = graphicsDevice.AllocateTexture3D<float>(textureType, 128, 128, 3);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsDoublePrecisionSupportAvailable(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsDoublePrecisionSupportAvailable();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsReadOnlyTexture2DSupportedForType(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsReadOnlyTexture2DSupportedForType<float>();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsReadWriteTexture2DSupportedForType(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsReadWriteTexture2DSupportedForType<float>();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsReadOnlyTexture3DSupportedForType(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsReadOnlyTexture3DSupportedForType<float>();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_IsReadWriteTexture3DSupportedForType(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

        _ = graphicsDevice.IsReadWriteTexture3DSupportedForType<float>();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task DeviceLost_CreateComputeContext(Device device)
    {
        using GraphicsDevice graphicsDevice = device.Get();

        await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

        using var _ = graphicsDevice.CreateComputeContext();
    }
}
