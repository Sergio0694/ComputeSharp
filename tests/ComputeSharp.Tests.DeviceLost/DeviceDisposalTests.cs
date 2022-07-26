using System;
using ComputeSharp.Interop;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CA1416

namespace ComputeSharp.Tests.DeviceLost;

[TestClass]
[TestCategory("DeviceDisposal")]
public partial class DeviceDisposalTests
{
    [TestMethod]
    public unsafe void DeviceDisposal_GetDefault_ReferenceCounting()
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = GraphicsDevice.GetDefault())
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
    [ExpectedException(typeof(ObjectDisposedException))]
    public unsafe void DeviceDisposal_InteropServicesThrowsObjectDisposedException(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        GraphicsDevice graphicsDevice = device.Get();

        graphicsDevice.Dispose();

        InteropServices.GetID3D12Device(graphicsDevice, Windows.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());
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
#if NET6_0_OR_GREATER
        // TODO: investigate ref count with resources disposed after device when using D3D12MA
        Assert.Inconclusive();
#endif

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

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void DeviceDisposal_WithComputeShader_ReferenceCounting(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            using var buffer = graphicsDevice.AllocateReadWriteBuffer<float>(128);

            using (ComputeContext context = graphicsDevice.CreateComputeContext())
            {
                context.For(buffer.Length, new InitializeShader(buffer));
            }

            InteropServices.GetID3D12Device(graphicsDevice, Windows.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());
        }

        _ = d3D12Device.Get()->AddRef();

        uint refCount = d3D12Device.Get()->Release();

        Assert.AreEqual(refCount, 1u);
    }

    [EmbeddedBytecode(DispatchAxis.X)]
    [AutoConstructor]
    private partial struct InitializeShader : IComputeShader
    {
        private readonly ReadWriteBuffer<float> buffer;

        public void Execute()
        {
            buffer[ThreadIds.X] = ThreadIds.X;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void DeviceDisposal_WithPixelShader_ReferenceCounting(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            using var texture = graphicsDevice.AllocateReadWriteTexture2D<Rgba32, float4>(128, 128);

            using (ComputeContext context = graphicsDevice.CreateComputeContext())
            {
                context.ForEach(texture, default(HelloWorldShader));
            }

            InteropServices.GetID3D12Device(graphicsDevice, Windows.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());
        }

        _ = d3D12Device.Get()->AddRef();

        uint refCount = d3D12Device.Get()->Release();

        Assert.AreEqual(refCount, 1u);
    }

    [EmbeddedBytecode(DispatchAxis.XY)]
    private partial struct HelloWorldShader : IPixelShader<float4>
    {
        public float4 Execute()
        {
            float2 uv = ThreadIds.Normalized.XY;
            float3 col = 0.5f + 0.5f * Hlsl.Cos(new float3(uv, uv.X) + new float3(0, 2, 4));

            return new(col, 1f);
        }
    }
}
