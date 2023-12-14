using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Helpers;
using ComputeSharp.SwapChain.Shaders.D2D1;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Rectangle = System.Drawing.Rectangle;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
public partial class D2D1TransformMapperTests
{
    [TestMethod]
    public unsafe void VerifyInterfaces()
    {
        D2D1DrawTransformMapper<HelloWorld> transformMapper = D2D1DrawTransformMapper<HelloWorld>.Inflate(4);

        using ComPtr<IUnknown> unknown = default;
        using ComPtr<IUnknown> transformMapper2 = default;
        using ComPtr<IUnknown> transformMapperInternal = default;

        Guid uuidOfIUnknown = typeof(IUnknown).GUID;
        Guid uuidOfTransformMapper = new("02E6D48D-B892-4FBC-AA54-119203BAB802");
        Guid uuidOfTransformMapperInternal = new("C5D8FC65-FB86-4C2D-9EF5-95AEC639C952");

        // The object implements IUnknown and the transform mapper interfaces
        Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)transformMapper).GetInterface(ref uuidOfIUnknown, out *(IntPtr*)unknown.GetAddressOf()));
        Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)transformMapper).GetInterface(ref uuidOfTransformMapper, out *(IntPtr*)transformMapper2.GetAddressOf()));
        Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)transformMapper).GetInterface(ref uuidOfTransformMapperInternal, out *(IntPtr*)transformMapperInternal.GetAddressOf()));

        Assert.IsTrue(unknown.Get() is not null);
        Assert.IsTrue(transformMapper2.Get() is not null);
        Assert.IsTrue(transformMapperInternal.Get() is not null);

        using ComPtr<IUnknown> garbage = default;

        Guid uuidOfGarbage = Guid.NewGuid();

        // Any other random QueryInterface should fail
        Assert.AreEqual(CustomQueryInterfaceResult.Failed, ((ICustomQueryInterface)transformMapper).GetInterface(ref uuidOfGarbage, out *(IntPtr*)garbage.GetAddressOf()));

        Assert.IsTrue(garbage.Get() is null);
    }

    [TestMethod]
    public unsafe void VerifyReferenceCounting()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<DummyShader>(d2D1Factory2.Get(), out _);

        D2D1DrawTransformMapper<HelloWorld> transformMapper = D2D1DrawTransformMapper<HelloWorld>.Inflate(4);

        using ComPtr<IUnknown> transformMapper2 = default;

        Guid uuidOfTransformMapper = new("02E6D48D-B892-4FBC-AA54-119203BAB802");

        Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)transformMapper).GetInterface(ref uuidOfTransformMapper, out *(IntPtr*)transformMapper2.GetAddressOf()));

        _ = transformMapper2.Get()->AddRef();

        // Right after creation, the transform mapper has only 2 ref count (one is the managed object, one is this ComPtr<T>)
        Assert.AreEqual(2u, transformMapper2.Get()->Release());

        using (ComPtr<ID2D1Effect> d2D1Effect = default)
        {
            D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<DummyShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

            D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect(d2D1Effect.Get(), transformMapper);

            _ = transformMapper2.Get()->AddRef();

            // After setting a transform mapper to an effect, the count raises to 3
            Assert.AreEqual(3u, transformMapper2.Get()->Release());

            D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect(d2D1Effect.Get(), transformMapper);

            _ = transformMapper2.Get()->AddRef();

            // Adding the same transform mapper again doesn't increment ref count
            Assert.AreEqual(3u, transformMapper2.Get()->Release());
        }

        _ = transformMapper2.Get()->AddRef();

        // If the effect is released, that extra manager reference is also released
        Assert.AreEqual(2u, transformMapper2.Get()->Release());
    }

    [TestMethod]
    public unsafe void VerifyManagedWrapperRetrieval()
    {
        D2D1DrawTransformMapper<HelloWorld> transformMapper = D2D1DrawTransformMapper<HelloWorld>.Inflate(4);

        using ComPtr<IUnknown> unknown = default;
        using ComPtr<IUnknown> transformMapperInternal = default;

        Guid uuidOfIUnknown = typeof(IUnknown).GUID;
        Guid uuidOfTransformMapperInternal = new("C5D8FC65-FB86-4C2D-9EF5-95AEC639C952");

        Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)transformMapper).GetInterface(ref uuidOfIUnknown, out *(IntPtr*)unknown.GetAddressOf()));

        Assert.IsTrue(unknown.Get() is not null);

        HRESULT hresult = unknown.CopyTo(&uuidOfTransformMapperInternal, (void**)transformMapperInternal.GetAddressOf());

        Assert.AreEqual((int)hresult, S.S_OK);
        Assert.IsTrue(transformMapperInternal.Get() is not null);

        IntPtr handlePtr;

        // Invoke GetManagedWrapperHandle
        hresult = ((delegate* unmanaged[MemberFunction]<IUnknown*, void**, int>)(*(void***)transformMapperInternal.Get())[3])(
            transformMapperInternal.Get(),
            (void**)&handlePtr);

        Assert.AreEqual((int)hresult, S.S_OK);

        GCHandle handle = GCHandle.FromIntPtr(handlePtr);

        Assert.IsNotNull(handle.Target);
        Assert.AreSame(handle.Target, transformMapper);
    }

    [TestMethod]
    public unsafe void VerifyReferenceTrackingWithManagedWrapper()
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void GetTransformMapperUnknownAndGCHandle(
            IUnknown** transformMapperUnknown,
            out GCHandle transformMapperHandle)
        {
            D2D1DrawTransformMapper<HelloWorld> transformMapper = D2D1DrawTransformMapper<HelloWorld>.Inflate(4);

            Guid uuidOfTransformMapper = new("02E6D48D-B892-4FBC-AA54-119203BAB802");

            Assert.AreEqual(CustomQueryInterfaceResult.Handled, ((ICustomQueryInterface)transformMapper).GetInterface(ref uuidOfTransformMapper, out *(IntPtr*)transformMapperUnknown));

            transformMapperHandle = GCHandle.Alloc(transformMapper, GCHandleType.Weak);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void Test(out GCHandle transformMapperHandle)
        {
            using ComPtr<IUnknown> transformMapperUnknown = default;

            GetTransformMapperUnknownAndGCHandle(transformMapperUnknown.GetAddressOf(), out transformMapperHandle);

            [MethodImpl(MethodImplOptions.NoInlining)]
            static void Test(GCHandle transformMapperHandle)
            {
                // Sanity check: the wrapper is alive right now
                Assert.IsNotNull(transformMapperHandle.Target);
            }

            Test(transformMapperHandle);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            // The wrapper is still alive, as the CCW is rooting it
            Assert.IsNotNull(transformMapperHandle.Target);
        }

        GCHandle transformMapperHandle = default;

        try
        {
            Test(out transformMapperHandle);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            // The wrapper is gone, as the CCW has been released
            Assert.IsNull(transformMapperHandle.Target);
        }
        finally
        {
            if (transformMapperHandle.IsAllocated)
            {
                transformMapperHandle.Free();
            }
        }
    }

    [D2DInputCount(0)]
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct DummyShader : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return default;
        }
    }

    [TestMethod]
    public unsafe void UpdateConstantBufferFromTransformMapper()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<ShaderWithDispatchArea>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<ShaderWithDispatchArea>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), new ShaderWithDispatchArea(111, 222));

        DispatchAreaTransformMapper transformMapper = new() { ExpectedWidth = 111, ExpectedHeight = 222 };

        D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect(d2D1Effect.Get(), transformMapper);

        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string sourcePath = Path.Combine(assemblyPath, "Assets", "Landscape.png");
        string expectedPath = Path.Combine(assemblyPath, "Assets", "RadialFadeOut.png");

        ReadOnlyMemory<byte> pixels = ImageHelper.LoadBitmapFromFile(sourcePath, out uint width, out uint height);

        using ComPtr<ID2D1Bitmap> d2D1BitmapSource = D2D1Helper.CreateD2D1BitmapAndSetAsSource(d2D1DeviceContext.Get(), pixels, width, height, d2D1Effect.Get());
        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), width, height);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());

        using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

        string destinationPath = Path.Combine(assemblyPath, "temp", $"{nameof(UpdateConstantBufferFromTransformMapper)}.png");

        _ = Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

        ImageHelper.SaveBitmapToFile(destinationPath, width, height, d2D1MappedRect.pitch, d2D1MappedRect.bits);

        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }

    private sealed class DispatchAreaTransformMapper : D2D1DrawTransformMapper<ShaderWithDispatchArea>
    {
        public int ExpectedWidth;
        public int ExpectedHeight;

        public override void MapInputsToOutput(
            D2D1DrawInfoUpdateContext<ShaderWithDispatchArea> drawInfoUpdateContext,
            ReadOnlySpan<Rectangle> inputs,
            ReadOnlySpan<Rectangle> opaqueInputs,
            out Rectangle output,
            out Rectangle opaqueOutput)
        {
            ShaderWithDispatchArea shader = drawInfoUpdateContext.GetConstantBuffer();

            Assert.AreEqual(shader.Width, this.ExpectedWidth);
            Assert.AreEqual(shader.Height, this.ExpectedHeight);

            shader = new ShaderWithDispatchArea(inputs[0].Width, inputs[0].Height);

            drawInfoUpdateContext.SetConstantBuffer(in shader);

            output = inputs[0];
            opaqueOutput = Rectangle.Empty;
        }

        public override void MapInvalidOutput(int inputIndex, in Rectangle invalidInput, out Rectangle invalidOutput)
        {
            invalidOutput = invalidInput;
        }

        public override void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs)
        {
            inputs.Fill(output);
        }
    }

    [D2DInputCount(1)]
    [D2DInputSimple(0)]
    [D2DRequiresScenePosition]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct ShaderWithDispatchArea : ID2D1PixelShader
    {
        public readonly int Width;
        public readonly int Height;

        public float4 Execute()
        {
            // Get the current integer coordinate and input color
            int2 xy = (int2)D2D.GetScenePosition().XY;
            float4 color = D2D.GetInput(0);

            // Calculate the center and size of the largest circle centered in the image
            float2 center = new((uint)this.Width / 2, (uint)this.Height / 2);
            int diameter = Hlsl.Min(this.Width, this.Height);
            int radius = (int)((uint)diameter / 2);

            // Calculate the distance from the center, to adjust the fade out
            float distance = Hlsl.Distance(xy, center);
            float falloff = radius / 2u;
            float alpha = 0;

            // If the pixel is close enough, keep it fully visible
            if (distance <= falloff)
            {
                alpha = 1;
            }
            else if (distance <= radius)
            {
                // Otherwise, linearly fade it out until the outer radius is reached
                float delta = radius - distance;

                alpha = delta / falloff;
            }

            return new(color.RGB, alpha);
        }
    }
}