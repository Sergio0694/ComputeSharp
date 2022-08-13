using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Helpers;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("D2D1ResourceTextureManager")]
public partial class D2D1ResourceTextureManagerTests
{
    [TestMethod]
    public unsafe void VerifyInterfaces()
    {
        using ComPtr<IUnknown> resourceTextureManager = default;

        D2D1ResourceTextureManager.Create((void**)resourceTextureManager.GetAddressOf());

        Assert.IsTrue(resourceTextureManager.Get() is not null);

        _ = resourceTextureManager.Get()->AddRef();

        Assert.AreEqual(1u, resourceTextureManager.Get()->Release());

        using ComPtr<IUnknown> unknown = default;
        using ComPtr<IUnknown> resourceTextureManager2 = default;
        using ComPtr<IUnknown> resourceTextureManagerInternal = default;

        Guid uuidOfIUnknown = typeof(IUnknown).GUID;
        Guid uuidOfResourceTextureManager = new("3C4FC7E4-A419-46CA-B5F6-66EB4FF18D64");
        Guid uuidOfResourceTextureManagerInternal = new("5CBB1024-8EA1-4689-81BF-8AD190B5EF5D");

        // The object implements IUnknown and the two resource texture manager interfaces
        Assert.AreEqual(0, (int)resourceTextureManager.AsIID(&uuidOfIUnknown, &unknown));
        Assert.AreEqual(0, (int)resourceTextureManager.AsIID(&uuidOfResourceTextureManager, &resourceTextureManager2));
        Assert.AreEqual(0, (int)resourceTextureManager.AsIID(&uuidOfResourceTextureManagerInternal, &resourceTextureManagerInternal));

        Assert.IsTrue(unknown.Get() is not null);
        Assert.IsTrue(resourceTextureManager2.Get() is not null);
        Assert.IsTrue(resourceTextureManagerInternal.Get() is not null);

        using ComPtr<IUnknown> garbage = default;

        Guid uuidOfGarbage = Guid.NewGuid();

        // Any other random QueryInterface should fail
        Assert.AreEqual(E.E_NOINTERFACE, (int)resourceTextureManager.AsIID(&uuidOfGarbage, &garbage));

        Assert.IsTrue(garbage.Get() is null);
    }

    [TestMethod]
    public unsafe void LoadPixelsFromResourceTexture2D_CreateAfterGettingEffectContext()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<IndexFrom2DResourceTextureShader>(d2D1Factory2.Get(), null, out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<IndexFrom2DResourceTextureShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(default(IndexFrom2DResourceTextureShader), d2D1Effect.Get());

        using ComPtr<IUnknown> resourceTextureManager = default;

        D2D1ResourceTextureManager.Create((void**)resourceTextureManager.GetAddressOf());

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager.Get(), 0);

        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string expectedPath = Path.Combine(assemblyPath, "Assets", "Landscape.png");

        using Image<Rgba32> expected = Image.Load<Rgba32>(expectedPath);

        if (!expected.DangerousTryGetSinglePixelMemory(out Memory<Rgba32> pixels))
        {
            Assert.Inconclusive();
        }

        D2D1ResourceTextureManager.Initialize(
            resourceTextureManager: resourceTextureManager.Get(),
            resourceId: Guid.NewGuid(),
            extents: stackalloc[] { (uint)expected.Width, (uint)expected.Height },
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: stackalloc[] { D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp },
            data: MemoryMarshal.AsBytes(pixels.Span),
            strides: stackalloc[] { (uint)(sizeof(Rgba32) * expected.Width) });

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)expected.Width, (uint)expected.Height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        string destinationPath = Path.Combine(assemblyPath, "temp", "IndexedFromResourceTexture2D_After.png");

        _ = Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        ImageHelper.SaveBitmapToFile(destinationPath, (uint)expected.Width, (uint)expected.Height, d2D1MappedRect.pitch, d2D1MappedRect.bits);

        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }

    [TestMethod]
    public unsafe void LoadPixelsFromResourceTexture2D_CreateBeforeGettingEffectContext()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<IndexFrom2DResourceTextureShader>(d2D1Factory2.Get(), null, out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<IndexFrom2DResourceTextureShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(default(IndexFrom2DResourceTextureShader), d2D1Effect.Get());

        using ComPtr<IUnknown> resourceTextureManager = default;

        D2D1ResourceTextureManager.Create((void**)resourceTextureManager.GetAddressOf());

        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string expectedPath = Path.Combine(assemblyPath, "Assets", "Landscape.png");

        using Image<Rgba32> expected = Image.Load<Rgba32>(expectedPath);

        if (!expected.DangerousTryGetSinglePixelMemory(out Memory<Rgba32> pixels))
        {
            Assert.Inconclusive();
        }

        D2D1ResourceTextureManager.Initialize(
            resourceTextureManager: resourceTextureManager.Get(),
            resourceId: Guid.NewGuid(),
            extents: stackalloc[] { (uint)expected.Width, (uint)expected.Height },
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: stackalloc[] { D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp },
            data: MemoryMarshal.AsBytes(pixels.Span),
            strides: stackalloc[] { (uint)(sizeof(Rgba32) * expected.Width) });

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager.Get(), 0);

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)expected.Width, (uint)expected.Height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        string destinationPath = Path.Combine(assemblyPath, "temp", "IndexedFromResourceTexture2D_Before.png");

        _ = Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        ImageHelper.SaveBitmapToFile(destinationPath, (uint)expected.Width, (uint)expected.Height, d2D1MappedRect.pitch, d2D1MappedRect.bits);

        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    private partial struct IndexFrom2DResourceTextureShader : ID2D1PixelShader
    {
        [D2DResourceTextureIndex(0)]
        private D2D1ResourceTexture2D<float4> source;

        public float4 Execute()
        {
            int2 xy = (int2)D2D.GetScenePosition().XY;

            return this.source[xy];
        }
    }

    [TestMethod]
    public unsafe void LoadPixelsFromResourceTexture3D()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<IndexFrom3DResourceTextureShader>(d2D1Factory2.Get(), null, out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<IndexFrom3DResourceTextureShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string expectedPath = Path.Combine(assemblyPath, "Assets", "WallpapersStack.png");

        using Image<Rgba32> expected = Image.Load<Rgba32>(expectedPath);

        if (!expected.DangerousTryGetSinglePixelMemory(out Memory<Rgba32> pixels))
        {
            Assert.Inconclusive();
        }

        IndexFrom3DResourceTextureShader shader = new(expected.Height / 4);

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(in shader, d2D1Effect.Get());

        using ComPtr<IUnknown> resourceTextureManager = default;

        D2D1ResourceTextureManager.Create((void**)resourceTextureManager.GetAddressOf());

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager.Get(), 0);

        D2D1ResourceTextureManager.Initialize(
            resourceTextureManager: resourceTextureManager.Get(),
            resourceId: Guid.NewGuid(),
            extents: stackalloc[] { (uint)expected.Width, (uint)(expected.Height / 4), 4u },
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: stackalloc[] { D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp },
            data: MemoryMarshal.AsBytes(pixels.Span),
            strides: stackalloc[] { (uint)(sizeof(Rgba32) * expected.Width), (uint)(sizeof(Rgba32) * expected.Width * (expected.Height / 4)) });

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)expected.Width, (uint)expected.Height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        string destinationPath = Path.Combine(assemblyPath, "temp", "IndexedFromResourceTexture3D.png");

        _ = Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        ImageHelper.SaveBitmapToFile(destinationPath, (uint)expected.Width, (uint)expected.Height, d2D1MappedRect.pitch, d2D1MappedRect.bits);

        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [AutoConstructor]
    private partial struct IndexFrom3DResourceTextureShader : ID2D1PixelShader
    {
        private int height;

        [D2DResourceTextureIndex(0)]
        private D2D1ResourceTexture3D<float4> source;

        public float4 Execute()
        {
            int2 xy = (int2)D2D.GetScenePosition().XY;

            int x = xy.X;
            int y = (int)((uint)xy.Y % (uint)height);
            int z = (int)((uint)xy.Y / (uint)height);

            return this.source[x, y, z];
        }
    }
}