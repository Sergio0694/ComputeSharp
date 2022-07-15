﻿using ComputeSharp.Interop;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CA1416

namespace ComputeSharp.Tests.DeviceLost;

[TestClass]
[TestCategory("DeviceDisposal")]
public class DeviceDisposalTests
{
    [CombinatorialTestMethod]
    public unsafe void DeviceDisposal_GetDefault_ReferenceCounting()
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = GraphicsDevice.Default)
        {
            InteropServices.GetID3D12Device(graphicsDevice, Windows.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());
        }

        // This test has to ensure a GraphicsDevice instance correctly destroys the underlying device
        // when disposed. To verify this, we first get the underlying ID3D12Device* object, which causes
        // its reference count to be incremented by 1. Assuming GraphicsDevice is working correctly, once
        // it is disposed, the underlying ID3D12Device* object should have its ref count set to just 1,
        // which is the one we added when doing QueryInterface on it. To verify this, we first increment
        // that, and then release it to get the updated count. If everything is correct, the ref count
        // we got back should be 1, with the last one just being from our own ID3D12Device* local. We
        // can't just do Release() and check that, as that'd cause the ref count to just go to 0 but
        // without clearing the pointer stored in the ComPtr<T> local, which would then crash when
        // going out of scope and trying to dereference that object to call Release() on it.
        _ = d3D12Device.Get()->AddRef();

        uint refCount = d3D12Device.Get()->Release();

        Assert.AreEqual(refCount, 1u);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void DeviceDisposal_ReferenceCounting(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            InteropServices.GetID3D12Device(graphicsDevice, Windows.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());
        }

        _ = d3D12Device.Get()->AddRef();

        uint refCount = d3D12Device.Get()->Release();

        Assert.AreEqual(refCount, 1u);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void DeviceDisposal_DisposedResources_ReferenceCounting(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            using var r1 = graphicsDevice.AllocateConstantBuffer<float>(128);
            using var r2 = graphicsDevice.AllocateReadOnlyBuffer<float>(128);
            using var r3 = graphicsDevice.AllocateReadWriteBuffer<float>(128);
            using var r4 = graphicsDevice.AllocateReadOnlyTexture2D<float>(128, 128);
            using var r5 = graphicsDevice.AllocateReadWriteTexture2D<float>(128, 128);

            InteropServices.GetID3D12Device(graphicsDevice, Windows.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());
        }

        _ = d3D12Device.Get()->AddRef();

        uint refCount = d3D12Device.Get()->Release();

        Assert.AreEqual(refCount, 1u);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void DeviceDisposal_DisposedResourcesAfterDevice_ReferenceCounting(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        ConstantBuffer<float> r1;
        ReadOnlyBuffer<float> r2;
        ReadWriteBuffer<float> r3;
        ReadOnlyTexture2D<float> r4;
        ReadWriteTexture2D<float> r5;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            r1 = graphicsDevice.AllocateConstantBuffer<float>(128);
            r2 = graphicsDevice.AllocateReadOnlyBuffer<float>(128);
            r3 = graphicsDevice.AllocateReadWriteBuffer<float>(128);
            r4 = graphicsDevice.AllocateReadOnlyTexture2D<float>(128, 128);
            r5 = graphicsDevice.AllocateReadWriteTexture2D<float>(128, 128);

            InteropServices.GetID3D12Device(graphicsDevice, Windows.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());
        }

        r1.Dispose();
        r2.Dispose();
        r3.Dispose();
        r4.Dispose();
        r5.Dispose();

        _ = d3D12Device.Get()->AddRef();

        uint refCount = d3D12Device.Get()->Release();

        Assert.AreEqual(refCount, 1u);
    }
}
