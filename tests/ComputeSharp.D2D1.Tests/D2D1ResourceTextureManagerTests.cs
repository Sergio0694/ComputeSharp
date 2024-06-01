using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Helpers;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649, IDE0044

namespace ComputeSharp.D2D1.Tests;

[TestClass]
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
    public unsafe void VerifyInterfaces_RCW()
    {
        D2D1ResourceTextureManager resourceTextureManager = new(
            extents: [64],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinLinearMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp]);

        using ComPtr<IUnknown> unknown = default;
        using ComPtr<IUnknown> resourceTextureManager2 = default;
        using ComPtr<IUnknown> resourceTextureManagerInternal = default;

        Guid uuidOfIUnknown = typeof(IUnknown).GUID;
        Guid uuidOfResourceTextureManager = new("3C4FC7E4-A419-46CA-B5F6-66EB4FF18D64");
        Guid uuidOfResourceTextureManagerInternal = new("5CBB1024-8EA1-4689-81BF-8AD190B5EF5D");

        // The object implements IUnknown and the two resource texture manager interfaces
        Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)resourceTextureManager).GetInterface(ref uuidOfIUnknown, out *(IntPtr*)unknown.GetAddressOf()));
        Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)resourceTextureManager).GetInterface(ref uuidOfResourceTextureManager, out *(IntPtr*)resourceTextureManager2.GetAddressOf()));
        Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)resourceTextureManager).GetInterface(ref uuidOfResourceTextureManagerInternal, out *(IntPtr*)resourceTextureManagerInternal.GetAddressOf()));

        Assert.IsTrue(unknown.Get() is not null);
        Assert.IsTrue(resourceTextureManager2.Get() is not null);
        Assert.IsTrue(resourceTextureManagerInternal.Get() is not null);

        using ComPtr<IUnknown> garbage = default;

        Guid uuidOfGarbage = Guid.NewGuid();

        // Any other random QueryInterface should fail
        Assert.AreEqual(CustomQueryInterfaceResult.Failed, ((ICustomQueryInterface)resourceTextureManager).GetInterface(ref uuidOfGarbage, out *(IntPtr*)garbage.GetAddressOf()));

        Assert.IsTrue(garbage.Get() is null);
    }

    [TestMethod]
    public unsafe void VerifyReferenceCounting()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<DummyShaderWithResourceTexture>(d2D1Factory2.Get(), out _);

        using ComPtr<IUnknown> resourceTextureManager = default;

        D2D1ResourceTextureManager.Create((void**)resourceTextureManager.GetAddressOf());

        _ = resourceTextureManager.Get()->AddRef();

        // Right after creation, the manager has only 1 ref count
        Assert.AreEqual(1u, resourceTextureManager.Get()->Release());

        using (ComPtr<ID2D1Effect> d2D1Effect = default)
        {
            D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<DummyShaderWithResourceTexture>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

            D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager.Get(), 0);

            _ = resourceTextureManager.Get()->AddRef();

            // After setting a manager to an effect, the manager also has a reference
            Assert.AreEqual(2u, resourceTextureManager.Get()->Release());

            D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager.Get(), 0);

            _ = resourceTextureManager.Get()->AddRef();

            // Adding the same manager again doesn't increment ref count
            Assert.AreEqual(2u, resourceTextureManager.Get()->Release());
        }

        _ = resourceTextureManager.Get()->AddRef();

        // If the effect is released, that extra manager reference is also released
        Assert.AreEqual(1u, resourceTextureManager.Get()->Release());
    }

    [TestMethod]
    [ExpectedException(typeof(Win32Exception))]
    public unsafe void SingleThreadedFactory_AssigningRCWManagerFails()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2(singleThreaded: true);
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<DummyShaderWithResourceTexture>(d2D1Factory2.Get(), out _);

        D2D1ResourceTextureManager resourceTextureManager = new(
            extents: [128, 128],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: [],
            strides: []);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<DummyShaderWithResourceTexture>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager, 0);
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct DummyShaderWithResourceTexture : ID2D1PixelShader
    {
        [D2DResourceTextureIndex(0)]
        private readonly D2D1ResourceTexture2D<float4> source;

        public float4 Execute()
        {
            int2 xy = (int2)D2D.GetScenePosition().XY;

            return this.source[xy];
        }
    }

    [TestMethod]
    public unsafe void LoadPixelsFromResourceTexture2D_CreateAfterGettingEffectContext()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<IndexFrom2DResourceTextureShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<IndexFrom2DResourceTextureShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), default(IndexFrom2DResourceTextureShader));

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
            extents: [(uint)expected.Width, (uint)expected.Height],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: MemoryMarshal.AsBytes(pixels.Span),
            strides: [(uint)(sizeof(Rgba32) * expected.Width)]);

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)expected.Width, (uint)expected.Height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        string destinationPath = Path.Combine(assemblyPath, "temp", "IndexedFromResourceTexture2D_After.png");

        _ = Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        ImageHelper.SaveBitmapToFile(destinationPath, (uint)expected.Width, (uint)expected.Height, d2D1MappedRect.pitch, d2D1MappedRect.bits);

        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    public unsafe void LoadPixelsFromResourceTexture2D_CreateBeforeGettingEffectContext(int drawCount)
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<IndexFrom2DResourceTextureShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<IndexFrom2DResourceTextureShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), default(IndexFrom2DResourceTextureShader));

        using ComPtr<IUnknown> resourceTextureManager = default;

        D2D1ResourceTextureManager.Create((void**)resourceTextureManager.GetAddressOf());

        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string expectedPath = Path.Combine(assemblyPath, "Assets", "Landscape.png");

        using Image<Rgba32> expected = Image.Load<Rgba32>(expectedPath);

        if (!expected.DangerousTryGetSinglePixelMemory(out Memory<Rgba32> pixels))
        {
            Assert.Inconclusive();
        }

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)expected.Width, (uint)expected.Height);

        D2D1ResourceTextureManager.Initialize(
            resourceTextureManager: resourceTextureManager.Get(),
            resourceId: Guid.NewGuid(),
            extents: [(uint)expected.Width, (uint)expected.Height],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: MemoryMarshal.AsBytes(pixels.Span),
            strides: [(uint)(sizeof(Rgba32) * expected.Width)]);

        for (int i = 0; i < drawCount; i++)
        {
            // We need to reassign the resource texture manager before each draw, so the D2D runtime can see
            // some effect property has changed, and it will call PrepareForRender. That's where the resource
            // texture will be retrieved. When drawCount is more than 1, this test is used to validate that
            // getting the resource texture multiple times from the same manager also works correctly.
            D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager.Get(), 0);

            D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());
        }

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        string destinationPath = Path.Combine(assemblyPath, "temp", $"IndexedFromResourceTexture2D_Before_Draw{drawCount}.png");

        _ = Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        ImageHelper.SaveBitmapToFile(destinationPath, (uint)expected.Width, (uint)expected.Height, d2D1MappedRect.pitch, d2D1MappedRect.bits);

        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }

    [TestMethod]
    public unsafe void LoadPixelsFromResourceTexture2D_CreateBeforeGettingEffectContext_RCW()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<IndexFrom2DResourceTextureShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<IndexFrom2DResourceTextureShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), default(IndexFrom2DResourceTextureShader));

        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string expectedPath = Path.Combine(assemblyPath, "Assets", "Landscape.png");

        using Image<Rgba32> expected = Image.Load<Rgba32>(expectedPath);

        if (!expected.DangerousTryGetSinglePixelMemory(out Memory<Rgba32> pixels))
        {
            Assert.Inconclusive();
        }

        D2D1ResourceTextureManager resourceTextureManager = new(
            resourceId: Guid.NewGuid(),
            extents: [(uint)expected.Width, (uint)expected.Height],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: MemoryMarshal.AsBytes(pixels.Span),
            strides: [(uint)(sizeof(Rgba32) * expected.Width)]);

        using ComPtr<IUnknown> unknown = default;

        Guid uuidOfIUnknown = typeof(IUnknown).GUID;

        Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)resourceTextureManager).GetInterface(ref uuidOfIUnknown, out *(IntPtr*)unknown.GetAddressOf()));

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), unknown.Get(), 0);

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)expected.Width, (uint)expected.Height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        string destinationPath = Path.Combine(assemblyPath, "temp", "IndexedFromResourceTexture2D_Before.png");

        _ = Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        ImageHelper.SaveBitmapToFile(destinationPath, (uint)expected.Width, (uint)expected.Height, d2D1MappedRect.pitch, d2D1MappedRect.bits);

        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }

    [TestMethod]
    public unsafe void LoadPixelsFromResourceTexture2D_AlternatingResourceTextureManagers()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<IndexFrom2DResourceTextureShader>(d2D1Factory2.Get(), out _);

        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string wallpaper0Path = Path.Combine(assemblyPath, "Assets", "Wallpaper0.png");
        string wallpaper1Path = Path.Combine(assemblyPath, "Assets", "Wallpaper1.png");

        using Image<Rgba32> expected0 = Image.Load<Rgba32>(wallpaper0Path);
        using Image<Rgba32> expected1 = Image.Load<Rgba32>(wallpaper1Path);

        Memory<Rgba32> pixels0 = Memory<Rgba32>.Empty;
        Memory<Rgba32> pixels1 = Memory<Rgba32>.Empty;

        if (!expected0.DangerousTryGetSinglePixelMemory(out pixels0) ||
            !expected1.DangerousTryGetSinglePixelMemory(out pixels1))
        {
            Assert.Inconclusive();
        }

        using ComPtr<IUnknown> resourceTextureManager0 = default;
        using ComPtr<IUnknown> resourceTextureManager1 = default;

        D2D1ResourceTextureManager.Create((void**)resourceTextureManager0.GetAddressOf());
        D2D1ResourceTextureManager.Create((void**)resourceTextureManager1.GetAddressOf());

        D2D1ResourceTextureManager.Initialize(
            resourceTextureManager: resourceTextureManager0.Get(),
            resourceId: Guid.NewGuid(),
            extents: [(uint)expected0.Width, (uint)expected0.Height],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: MemoryMarshal.AsBytes(pixels0.Span),
            strides: [(uint)(sizeof(Rgba32) * expected0.Width)]);

        D2D1ResourceTextureManager.Initialize(
            resourceTextureManager: resourceTextureManager1.Get(),
            resourceId: Guid.NewGuid(),
            extents: [(uint)expected0.Width, (uint)expected0.Height],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: MemoryMarshal.AsBytes(pixels1.Span),
            strides: [(uint)(sizeof(Rgba32) * expected0.Width)]);

        using (ComPtr<ID2D1Effect> d2D1Effect = default)
        {
            D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<IndexFrom2DResourceTextureShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

            using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)expected0.Width, (uint)expected0.Height);

            for (int i = 0; i < 10; i++)
            {
                ref readonly ComPtr<IUnknown> resourceTextureManager = ref i % 2 == 0
                    ? ref resourceTextureManager0
                    : ref resourceTextureManager1;

                D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager.Get(), 0);

                D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

                using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

                string destinationPath = Path.Combine(assemblyPath, "temp", $"LoadPixelsFromResourceTexture2D_Alternating{i}.png");

                _ = Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

                ImageHelper.SaveBitmapToFile(destinationPath, (uint)expected0.Width, (uint)expected0.Height, d2D1MappedRect.pitch, d2D1MappedRect.bits);

                string expectedPath = i % 2 == 0 ? wallpaper0Path : wallpaper1Path;

                TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
            }
        }

        _ = resourceTextureManager0.Get()->AddRef();

        Assert.AreEqual(1u, resourceTextureManager0.Get()->Release());

        _ = resourceTextureManager1.Get()->AddRef();

        Assert.AreEqual(1u, resourceTextureManager1.Get()->Release());
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct IndexFrom2DResourceTextureShader : ID2D1PixelShader
    {
        [D2DResourceTextureIndex(0)]
        private readonly D2D1ResourceTexture2D<float4> source;

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

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<IndexFrom3DResourceTextureShader>(d2D1Factory2.Get(), out _);

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

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), in shader);

        using ComPtr<IUnknown> resourceTextureManager = default;

        D2D1ResourceTextureManager.Create((void**)resourceTextureManager.GetAddressOf());

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager.Get(), 0);

        D2D1ResourceTextureManager.Initialize(
            resourceTextureManager: resourceTextureManager.Get(),
            resourceId: Guid.NewGuid(),
            extents: [(uint)expected.Width, (uint)(expected.Height / 4), 4u],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: MemoryMarshal.AsBytes(pixels.Span),
            strides: [(uint)(sizeof(Rgba32) * expected.Width), (uint)(sizeof(Rgba32) * expected.Width * (expected.Height / 4))]);

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
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct IndexFrom3DResourceTextureShader(float height) : ID2D1PixelShader
    {
        [D2DResourceTextureIndex(0)]
        private readonly D2D1ResourceTexture3D<float4> source;

        public float4 Execute()
        {
            int2 xy = (int2)D2D.GetScenePosition().XY;

            int x = xy.X;
            int y = (int)((uint)xy.Y % (uint)height);
            int z = (int)((uint)xy.Y / (uint)height);

            return this.source[x, y, z];
        }
    }

    [TestMethod]
    [DataRow(128, 0, 26)]
    [DataRow(128, 0, 67)]
    [DataRow(128, 0, 128)]
    [DataRow(128, 32, 64)]
    [DataRow(1024, 311, 489)]
    [DataRow(1024, 907, 117)]
    public unsafe void UpdateResourceTexture1D(int width, int startOffset, int updateLength)
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<CopyFromResourceTexture1DShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<CopyFromResourceTexture1DShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        CopyFromResourceTexture1DShader shader = new();

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), in shader);

        byte[] texture = new byte[width];

        D2D1ResourceTextureManager resourceTextureManager = new(
            extents: [(uint)width],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.One,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp],
            data: texture,
            strides: null);

        byte[] data = new byte[updateLength];

        RandomNumberGenerator.Create().GetBytes(data);

        resourceTextureManager.Update(
            minimumExtents: [(uint)startOffset],
            maximimumExtents: [(uint)(startOffset + updateLength)],
            strides: [],
            data: data);

        data.CopyTo(texture.AsSpan(startOffset));

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager, 0);

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)width, 1);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        byte[] resultingBytes = new byte[width];
        int i = 0;

        foreach (Bgra32 pixel in new ReadOnlySpan<Bgra32>(d2D1MappedRect.bits, width))
        {
            resultingBytes[i++] = pixel.B;
        }

        Assert.IsTrue(texture.AsSpan().SequenceEqual(resultingBytes));
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct CopyFromResourceTexture1DShader : ID2D1PixelShader
    {
        [D2DResourceTextureIndex(0)]
        private readonly D2D1ResourceTexture1D<float> source;

        public float4 Execute()
        {
            int2 xy = (int2)D2D.GetScenePosition().XY;

            return this.source[xy.X];
        }
    }

    [TestMethod]
    [DataRow(64, 64, 0, 0, 64, 64)]
    [DataRow(64, 64, 32, 32, 32, 32)]
    [DataRow(64, 64, 12, 33, 43, 22)]
    [DataRow(512, 768, 245, 111, 255, 461)]
    [DataRow(1024, 2048, 11, 899, 512, 342)]
    public unsafe void UpdateResourceTexture2D(
        int width,
        int height,
        int startOffsetX,
        int startOffsetY,
        int updateLengthX,
        int updateLengthY)
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<CopyFromResourceTexture2DShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<CopyFromResourceTexture2DShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        CopyFromResourceTexture2DShader shader = new();

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), in shader);

        byte[] texture = new byte[width * height];

        D2D1ResourceTextureManager resourceTextureManager = new(
            extents: [(uint)width, (uint)height],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.One,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: texture,
            strides: [(uint)width]);

        byte[] data = new byte[updateLengthX * updateLengthY];

        RandomNumberGenerator.Create().GetBytes(data);

        resourceTextureManager.Update(
            minimumExtents: [(uint)startOffsetX, (uint)startOffsetY],
            maximimumExtents: [(uint)(startOffsetX + updateLengthX), (uint)(startOffsetY + updateLengthY)],
            strides: [(uint)updateLengthX],
            data: data);

        for (int y = 0; y < updateLengthY; y++)
        {
            ReadOnlySpan<byte> source = data.AsSpan(y * updateLengthX, updateLengthX);
            Span<byte> destination = texture.AsSpan((startOffsetY * width) + (y * width) + startOffsetX, updateLengthX);

            source.CopyTo(destination);
        }

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager, 0);

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)width, (uint)height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        byte[] resultingBytes = new byte[width * height];
        int i = 0;

        for (int y = 0; y < height; y++)
        {
            foreach (Bgra32 pixel in new ReadOnlySpan<Bgra32>(d2D1MappedRect.bits + (d2D1MappedRect.pitch * y), width))
            {
                resultingBytes[i++] = pixel.B;
            }
        }

        Assert.IsTrue(texture.AsSpan().SequenceEqual(resultingBytes));
    }

    [TestMethod]
    public unsafe void UpdateResourceTexture2D_ConcurrentAccess_DoesNotDeadlock()
    {
        const int width = 4096;
        const int height = 4096;

        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());
        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), width, height);

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<CopyFromResourceTexture2DShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<CopyFromResourceTexture2DShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        CopyFromResourceTexture2DShader shader = new();

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), in shader);

        byte[] texture = new byte[width * height];

        D2D1ResourceTextureManager resourceTextureManager = new(
            extents: [width, height],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.One,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: texture,
            strides: [width]);

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager, 0);

        CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(20));

        // Thread As are constantly updating the resource texture
        void UpdateResourceTexture()
        {
            ReadOnlySpan<uint> minimumExtents = [0, 0];
            ReadOnlySpan<uint> maximumExtents = [width, height];
            ReadOnlySpan<uint> strides = [width];

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                resourceTextureManager.Update(
                    minimumExtents: minimumExtents,
                    maximimumExtents: maximumExtents,
                    strides: strides,
                    data: texture);
            }
        }

        // Thread Bs are constantly drawing on the target bitmap
        void Draw()
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());
            }
        }

        Thread threadA = new(UpdateResourceTexture);
        Thread threadB = new(Draw);

        threadA.Start();
        threadB.Start();

        threadA.Join();
        threadB.Join();
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct CopyFromResourceTexture2DShader : ID2D1PixelShader
    {
        [D2DResourceTextureIndex(0)]
        private readonly D2D1ResourceTexture2D<float> source;

        public float4 Execute()
        {
            int2 xy = (int2)D2D.GetScenePosition().XY;

            return this.source[xy];
        }
    }

    [TestMethod]
    [DataRow(64, 64, 3, 0, 0, 0, 64, 64, 3)]
    [DataRow(64, 64, 3, 10, 22, 1, 44, 33, 1)]
    [DataRow(64, 64, 3, 10, 22, 1, 44, 33, 2)]
    [DataRow(64, 64, 3, 10, 22, 0, 44, 33, 2)]
    [DataRow(512, 512, 6, 245, 0, 2, 188, 1, 4)]
    [DataRow(512, 512, 6, 245, 0, 2, 188, 467, 3)]
    [DataRow(512, 512, 6, 245, 113, 1, 256, 322, 2)]
    public unsafe void UpdateResourceTexture3D(
        int width,
        int height,
        int depth,
        int startOffsetX,
        int startOffsetY,
        int startOffsetZ,
        int updateLengthX,
        int updateLengthY,
        int updateLengthZ)
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<CopyFromResourceTexture3DShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<CopyFromResourceTexture3DShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        CopyFromResourceTexture3DShader shader = new(height);

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), in shader);

        byte[] texture = new byte[width * height * depth];

        D2D1ResourceTextureManager resourceTextureManager = new(
            extents: [(uint)width, (uint)height, (uint)depth],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.One,
            filter: D2D1Filter.MinMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp, D2D1ExtendMode.Clamp],
            data: texture,
            strides: [(uint)width, (uint)(width * height)]);

        byte[] data = new byte[updateLengthX * updateLengthY * updateLengthZ];

        RandomNumberGenerator.Create().GetBytes(data);

        resourceTextureManager.Update(
            minimumExtents: [(uint)startOffsetX, (uint)startOffsetY, (uint)startOffsetZ],
            maximimumExtents: [(uint)(startOffsetX + updateLengthX), (uint)(startOffsetY + updateLengthY), (uint)(startOffsetZ + updateLengthZ)],
            strides: [(uint)updateLengthX, (uint)(updateLengthX * updateLengthY)],
            data: data);

        for (int z = 0; z < updateLengthZ; z++)
        {
            for (int y = 0; y < updateLengthY; y++)
            {
                ReadOnlySpan<byte> source = data.AsSpan((z * updateLengthX * updateLengthY) + (y * updateLengthX), updateLengthX);
                Span<byte> destination = texture.AsSpan(((startOffsetZ + z) * width * height) + ((startOffsetY + y) * width) + startOffsetX, updateLengthX);

                source.CopyTo(destination);
            }
        }

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(d2D1Effect.Get(), resourceTextureManager, 0);

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), (uint)width, (uint)(height * depth));

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        byte[] resultingBytes = new byte[width * height * depth];
        int i = 0;

        for (int y = 0; y < height * depth; y++)
        {
            foreach (Bgra32 pixel in new ReadOnlySpan<Bgra32>(d2D1MappedRect.bits + (d2D1MappedRect.pitch * y), width))
            {
                resultingBytes[i++] = pixel.B;
            }
        }

        Assert.IsTrue(texture.AsSpan().SequenceEqual(resultingBytes));
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct CopyFromResourceTexture3DShader(float height) : ID2D1PixelShader
    {
        [D2DResourceTextureIndex(0)]
        private readonly D2D1ResourceTexture3D<float> source;

        public float4 Execute()
        {
            int2 xy = (int2)D2D.GetScenePosition().XY;

            int x = xy.X;
            int y = (int)((uint)xy.Y % (uint)height);
            int z = (int)((uint)xy.Y / (uint)height);

            return this.source[x, y, z];
        }
    }

    [TestMethod]
    [ExpectedException(typeof(Win32Exception))]
    public unsafe void NullResourceTexture_DrawImageFails()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<NullResourceTextureShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<NullResourceTextureShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), default(NullResourceTextureShader));

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), 128, 128);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct NullResourceTextureShader : ID2D1PixelShader
    {
        [D2DResourceTextureIndex(0)]
        private readonly D2D1ResourceTexture2D<float4> source;

        public float4 Execute()
        {
            return this.source[0, 0];
        }
    }
}