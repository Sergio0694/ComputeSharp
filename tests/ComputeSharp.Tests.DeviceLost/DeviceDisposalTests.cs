using System;
#if USE_D3D12MA
using ComputeSharp.D3D12MemoryAllocator;
#endif
using ComputeSharp.Interop;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.DeviceLost.Helpers;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Win32;
using Win32.Graphics.Direct3D12;

namespace ComputeSharp.Tests.DeviceLost;

using Win32 = Win32.Apis;

[TestClass]
[TestCategory("DeviceDisposal")]
public partial class DeviceDisposalTests
{
    [AssemblyInitialize]
    public static void ConfigureImageSharp(TestContext _)
    {
#if USE_D3D12MA
        // If requested by the test runner, configure D3D12MA
        AllocationServices.ConfigureAllocatorFactory(new D3D12MemoryAllocatorFactory());
#endif
    }

    [TestMethod]
    public void DeviceDisposal_GetDefault_ReferenceCounting()
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = GraphicsDevice.GetDefault())
        {
            GraphicsDeviceHelper.GetD3D12Device(graphicsDevice, in d3D12Device);
        }

        Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(d3D12Device));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void DeviceDisposal_ReferenceCounting(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            GraphicsDeviceHelper.GetD3D12Device(graphicsDevice, in d3D12Device);
        }

        Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(d3D12Device));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(ObjectDisposedException))]
    public unsafe void DeviceDisposal_InteropServicesThrowsObjectDisposedException(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        GraphicsDevice graphicsDevice = device.Get();

        graphicsDevice.Dispose();

        InteropServices.GetID3D12Device(graphicsDevice, Win32.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void DeviceDisposal_DisposedResources_ReferenceCounting(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            using ConstantBuffer<float> r1 = graphicsDevice.AllocateConstantBuffer<float>(128);
            using ReadOnlyBuffer<float> r2 = graphicsDevice.AllocateReadOnlyBuffer<float>(128);
            using ReadWriteBuffer<float> r3 = graphicsDevice.AllocateReadWriteBuffer<float>(128);
            using ReadOnlyTexture2D<float> r4 = graphicsDevice.AllocateReadOnlyTexture2D<float>(128, 128);
            using ReadWriteTexture2D<float> r5 = graphicsDevice.AllocateReadWriteTexture2D<float>(128, 128);
            using UploadBuffer<float> r6 = graphicsDevice.AllocateUploadBuffer<float>(128);
            using ReadBackBuffer<float> r7 = graphicsDevice.AllocateReadBackBuffer<float>(128);
            using UploadTexture2D<float> r8 = graphicsDevice.AllocateUploadTexture2D<float>(128, 128);
            using ReadBackTexture2D<float> r9 = graphicsDevice.AllocateReadBackTexture2D<float>(128, 128);

            GraphicsDeviceHelper.GetD3D12Device(graphicsDevice, in d3D12Device);
        }

        Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(d3D12Device));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void DeviceDisposal_DisposedResourcesAfterDevice_ReferenceCounting(Device device)
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

            GraphicsDeviceHelper.GetD3D12Device(graphicsDevice, in d3D12Device);
        }

        r1.Dispose();
        r2.Dispose();
        r3.Dispose();
        r4.Dispose();
        r5.Dispose();

        Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(d3D12Device));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void DeviceDisposal_WithComputeShader_ReferenceCounting(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            using ReadWriteBuffer<float> buffer = graphicsDevice.AllocateReadWriteBuffer<float>(128);

            using (ComputeContext context = graphicsDevice.CreateComputeContext())
            {
                context.For(buffer.Length, new InitializeShader(buffer));
            }

            GraphicsDeviceHelper.GetD3D12Device(graphicsDevice, in d3D12Device);
        }

        Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(d3D12Device));
    }

    [EmbeddedBytecode(DispatchAxis.X)]
    [AutoConstructor]
    private readonly partial struct InitializeShader : IComputeShader
    {
        private readonly ReadWriteBuffer<float> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.X] = ThreadIds.X;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void DeviceDisposal_WithPixelShader_ReferenceCounting(Device device)
    {
        using ComPtr<ID3D12Device> d3D12Device = default;

        using (GraphicsDevice graphicsDevice = device.Get())
        {
            using ReadWriteTexture2D<Rgba32, float4> texture = graphicsDevice.AllocateReadWriteTexture2D<Rgba32, float4>(128, 128);

            using (ComputeContext context = graphicsDevice.CreateComputeContext())
            {
                context.ForEach(texture, default(HelloWorldShader));
            }

            GraphicsDeviceHelper.GetD3D12Device(graphicsDevice, in d3D12Device);
        }

        Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(d3D12Device));
    }

    [EmbeddedBytecode(DispatchAxis.XY)]
    private partial struct HelloWorldShader : IPixelShader<float4>
    {
        public float4 Execute()
        {
            float2 uv = ThreadIds.Normalized.XY;
            float3 col = 0.5f + (0.5f * Hlsl.Cos(new float3(uv, uv.X) + new float3(0, 2, 4)));

            return new(col, 1f);
        }
    }
}