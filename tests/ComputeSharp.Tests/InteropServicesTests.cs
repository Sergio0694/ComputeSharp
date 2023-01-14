using System;
using ComputeSharp.Interop;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Win32;
using Win32.Graphics.Direct3D12;
using D3D12_RESOURCE_DIMENSION = Win32.Graphics.Direct3D12.ResourceDimension;
using HRESULT = Win32.HResult;
using LUID = Win32.Luid;

namespace ComputeSharp.Tests;

using Win32 = Win32.Apis;

[TestClass]
[TestCategory("InteropServices")]
public unsafe partial class InteropServicesTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public void GetDevice(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        InteropServices.GetID3D12Device(device.Get(), Win32.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());

        Assert.IsTrue(d3D12Device.Get() != null);

        LUID luid = d3D12Device.Get()->GetAdapterLuid();

        Assert.IsTrue(*(ulong*)&luid != 0);

        d3D12Device.Dispose();

        int hResult = InteropServices.TryGetID3D12Device(device.Get(), Win32.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());

        Assert.AreEqual(hResult, (int)HRESULT.Ok);
        Assert.IsTrue(d3D12Device.Get() != null);

        luid = d3D12Device.Get()->GetAdapterLuid();

        Assert.IsTrue(*(ulong*)&luid != 0);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ConstantBuffer<>))]
    [Resource(typeof(ReadOnlyBuffer<>))]
    [Resource(typeof(ReadWriteBuffer<>))]
    public void ResourceFromBuffer(Device device, Type bufferType)
    {
        using Buffer<float> buffer = device.Get().AllocateBuffer<float>(bufferType, 128);

        using ComPtr<ID3D12Resource> d3D12Resource = default;

        InteropServices.GetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Buffer);

        d3D12Resource.Dispose();

        int hResult = InteropServices.TryGetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.AreEqual(hResult, (int)HRESULT.Ok);
        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Buffer);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    public void ResourceFromTexture2D(Device device, Type bufferType)
    {
        using Texture2D<float> buffer = device.Get().AllocateTexture2D<float>(bufferType, 16, 16);

        using ComPtr<ID3D12Resource> d3D12Resource = default;

        InteropServices.GetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Texture2D);

        d3D12Resource.Dispose();

        int hResult = InteropServices.TryGetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.AreEqual(hResult, (int)HRESULT.Ok);
        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Texture2D);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    public void ResourceFromTexture3D(Device device, Type bufferType)
    {
        using Texture3D<float> buffer = device.Get().AllocateTexture3D<float>(bufferType, 16, 16, 4);

        using ComPtr<ID3D12Resource> d3D12Resource = default;

        InteropServices.GetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Texture3D);

        d3D12Resource.Dispose();

        int hResult = InteropServices.TryGetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.AreEqual(hResult, (int)HRESULT.Ok);
        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Texture3D);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadBuffer<>))]
    [Resource(typeof(ReadBackBuffer<>))]
    public void ResourceFromTransferBuffer(Device device, Type bufferType)
    {
        using TransferBuffer<float> buffer = device.Get().AllocateTransferBuffer<float>(bufferType, 128);

        using ComPtr<ID3D12Resource> d3D12Resource = default;

        InteropServices.GetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Buffer);

        d3D12Resource.Dispose();

        int hResult = InteropServices.TryGetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.AreEqual(hResult, (int)HRESULT.Ok);
        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Buffer);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture2D<>))]
    [Resource(typeof(ReadBackTexture2D<>))]
    public void ResourceFromTransferTexture2D(Device device, Type bufferType)
    {
        using TransferTexture2D<float> buffer = device.Get().AllocateTransferTexture2D<float>(bufferType, 16, 16);

        using ComPtr<ID3D12Resource> d3D12Resource = default;

        InteropServices.GetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Buffer);

        d3D12Resource.Dispose();

        int hResult = InteropServices.TryGetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.AreEqual(hResult, (int)HRESULT.Ok);
        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Buffer);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture3D<>))]
    [Resource(typeof(ReadBackTexture3D<>))]
    public void ResourceFromTransferTexture3D(Device device, Type bufferType)
    {
        using TransferTexture3D<float> buffer = device.Get().AllocateTransferTexture3D<float>(bufferType, 16, 16, 4);

        using ComPtr<ID3D12Resource> d3D12Resource = default;

        InteropServices.GetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Buffer);

        d3D12Resource.Dispose();

        int hResult = InteropServices.TryGetID3D12Resource(buffer, Win32.__uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

        Assert.AreEqual(hResult, (int)HRESULT.Ok);
        Assert.IsTrue(d3D12Resource.Get() != null);
        Assert.AreEqual(d3D12Resource.Get()->GetDesc().Dimension, D3D12_RESOURCE_DIMENSION.Buffer);
    }
}