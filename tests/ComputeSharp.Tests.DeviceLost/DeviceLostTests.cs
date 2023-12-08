using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.DeviceLost.Helpers;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Tests.DeviceLost;

[TestClass]
public class DeviceLostTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public async Task DeviceLost_RaiseEvent(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            GraphicsDeviceHelper.GetD3D12Device(graphicsDevice, in d3D12Device);

            List<(object? Sender, DeviceLostEventArgs Args)> args = [];

            // Register the device lost callback
            graphicsDevice.DeviceLost += (s, e) => args.Add((s, e));

            await GraphicsDeviceHelper.RemoveDeviceAsync(graphicsDevice);

            Assert.AreEqual(1, args.Count);
            Assert.IsNotNull(args[0].Sender);
            Assert.IsNotNull(args[0].Args);
            Assert.AreSame(args[0].Sender, graphicsDevice);
            Assert.AreEqual(args[0].Args.Reason, DeviceLostReason.DeviceRemoved);

            // Trying to remove the device again does nothing
            GraphicsDeviceHelper.RemoveDevice(graphicsDevice);
            GraphicsDeviceHelper.RemoveDevice(graphicsDevice);
            GraphicsDeviceHelper.RemoveDevice(graphicsDevice);
            GraphicsDeviceHelper.RemoveDevice(graphicsDevice);

            // The event can only be raised once
            Assert.AreEqual(1, args.Count);
        }

        // Ensure the device has been disposed correctly
        Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(in d3D12Device));
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

        using Resources.Buffer<float> _ = graphicsDevice.AllocateBuffer<float>(bufferType, 128);
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

        using Resources.Texture2D<float> _ = graphicsDevice.AllocateTexture2D<float>(textureType, 128, 128);
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

        using Resources.Texture3D<float> _ = graphicsDevice.AllocateTexture3D<float>(textureType, 128, 128, 3);
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

        using ComputeContext _ = graphicsDevice.CreateComputeContext();
    }
}