using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.WinUI.Tests.Helpers;
using Microsoft.Graphics.Canvas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

[assembly: D2DEnableRuntimeCompilation]

namespace ComputeSharp.D2D1.WinUI.Tests;

[TestClass]
public partial class PixelShaderEffectTests
{
    [TestMethod]
    public unsafe void ConstantBuffer_IsInitiallySetToDefault()
    {
        using PixelShaderEffect<ShaderWithSomeProperties> effect = new();

        ShaderWithSomeProperties shader = effect.ConstantBuffer;
        ShaderWithSomeProperties defaultShader = default;

        ReadOnlySpan<byte> actual = new(&shader, sizeof(ShaderWithSomeProperties));
        ReadOnlySpan<byte> expected = new(&defaultShader, sizeof(ShaderWithSomeProperties));

        // The default constant buffer is set to 0, and it can be retrieved correctly
        Assert.IsTrue(actual.SequenceEqual(expected));
    }

    [TestMethod]
    public unsafe void ConstantBuffer_RoundTripsCorrectly()
    {
        ShaderWithSomeProperties shader = new(
            A: 3.14f,
            B: 123456,
            C: new int2(111, -222),
            D: new uint2x2(8888, 123456, 98765, 192837465),
            E: new float2(-3.14f, 6.28f));

        using PixelShaderEffect<ShaderWithSomeProperties> effect = new() { ConstantBuffer = shader };

        ShaderWithSomeProperties roundTripShader = effect.ConstantBuffer;

        ReadOnlySpan<byte> expected = new(&shader, sizeof(ShaderWithSomeProperties));
        ReadOnlySpan<byte> actual = new(&roundTripShader, sizeof(ShaderWithSomeProperties));

        // The resulting constant buffer matches the source, when the effect is not realized
        Assert.IsTrue(actual.SequenceEqual(expected));

        // Draw the effect, to force realization
        using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
        using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
        {
            drawingSession.DrawImage(effect);
        }

        roundTripShader = effect.ConstantBuffer;
        actual = new(&roundTripShader, sizeof(ShaderWithSomeProperties));

        // The shader also matches when retrieving it through the realized effect
        Assert.IsTrue(actual.SequenceEqual(expected));

        // Draw the effect on a different device (this forces it to unrealize and re-realize).
        // This also acts as a unit test for the device changed logic, since that's used here.
        using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
        using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
        {
            drawingSession.DrawImage(effect);
        }

        roundTripShader = effect.ConstantBuffer;
        actual = new(&roundTripShader, sizeof(ShaderWithSomeProperties));

        // The shader also matches when retrieving it through the realized effect
        Assert.IsTrue(actual.SequenceEqual(expected));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void SourcesOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        using PixelShaderEffect<ShaderWith0Inputs> effect0 = new();

        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect0.Sources[0] = null);
        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect0.Sources[1] = null);

        using PixelShaderEffect<ShaderWith1Input> effect1 = new();

        effect1.Sources[0] = null;

        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect1.Sources[1] = null);
        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect1.Sources[2] = null);

        using PixelShaderEffect<ShaderWith1Input> effect2 = new();

        effect2.Sources[0] = null;
        effect2.Sources[1] = null;

        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect2.Sources[2] = null);
        _ = Assert.ThrowsException<ArgumentOutOfRangeException>(() => effect2.Sources[3] = null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NullTransformMapper_UnrealizedEffect_ThrowsArgumentNullException()
    {
        using PixelShaderEffect<ShaderWith0Inputs> effect = new();

        effect.TransformMapper = null!;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NullTransformMapper_RealizedEffect_ThrowsArgumentNullException()
    {
        using PixelShaderEffect<ShaderWith0Inputs> effect = new();

        using (CanvasRenderTarget renderTarget = new(new CanvasDevice(), 128, 128, 96.0f))
        using (CanvasDrawingSession drawingSession = renderTarget.CreateDrawingSession())
        {
            drawingSession.DrawImage(effect);
        }

        effect.TransformMapper = null!;
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public unsafe void GraphCycle_IsDetectedCorrectly()
    {
        using PixelShaderEffect<ShaderWith0Inputs> effect1 = new();
        using PixelShaderEffect<ShaderWith2Inputs> effect2 = new();
        using PixelShaderEffect<ShaderWith1Input> effect3 = new();
        using PixelShaderEffect<ShaderWith1Input> effect4 = new();
        using PixelShaderEffect<ShaderWith2Inputs> effect5 = new();
        using PixelShaderEffect<ShaderWith2Inputs> effect6 = new();
        using PixelShaderEffect<ShaderWith0Inputs> effect7 = new();

        // Build a graph with a cycle in it:
        //
        //     ┌───┐   ┌───┐   ┌───┐
        //     │ 1 ├──►│ 2 ├──►│ 3 ├──────────┐
        //     └───┘   └───┘   └───┘          ▼
        // ┌───┐   ┌───┐ ▲       │  ┌───┐   ┌───┐
        // │ 7 ├──►│ 6 ├─┘       └─►│ 4 ├──►│ 5 │
        // └───┘   └───┘            └───┘   └───┘
        //           ▲                 │
        //           └─────────────────┘
        //
        // The cycle is 4 -> 6 -> 2 -> 3 -> 4.
        effect2.Sources[0] = effect1;
        effect2.Sources[1] = effect6;
        effect3.Sources[0] = effect2;
        effect4.Sources[0] = effect3;
        effect5.Sources[0] = effect3;
        effect5.Sources[1] = effect4;
        effect6.Sources[0] = effect4;
        effect6.Sources[1] = effect7;

        using ComPtr<ID2D1Image> d2D1Image = default;

        // Realize the effect for it to detect a graph cycle. We specifically don't want to draw
        // the effect, because in that case we'd still get an exception from D2D, even if the
        // effect from ComputeSharp failed to detect a graph cycle. We want to make sure the
        // detection and subsequent error is coming from our wrapper, not from D2D's logic.
        Win2DHelper.GetD2DImage(effect5, new CanvasDevice(), d2D1Image.GetAddressOf());
    }

    [TestMethod]
    public unsafe void Interop_EffectPropertiesAreForwardedCorrectly()
    {
        ShaderWithSomePropertiesAndInputs shader = new(-1234567);

        using PixelShaderEffect<ShaderWith0Inputs> effect0 = new();
        using PixelShaderEffect<ShaderWithSomePropertiesAndInputs> effect1 = new()
        {
            ConstantBuffer = shader,
            CacheOutput = true,
            BufferPrecision = CanvasBufferPrecision.Precision32Float,
            Sources = { [0] = effect0 }
        };

        CanvasDevice canvasDevice = new();

        using ComPtr<ID2D1Image> d2D1Image = default;

        // Realize the effect and get the D2D effect (this will also apply all local properties)
        Win2DHelper.GetD2DImage(effect1, canvasDevice, d2D1Image.GetAddressOf());

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        Marshal.ThrowExceptionForHR(d2D1Image.CopyTo(d2D1Effect.GetAddressOf()));

        int constantBufferDataSize = D2D1PixelShader.GetConstantBufferSize<ShaderWithSomePropertiesAndInputs>();
        byte* constantBufferData = stackalloc byte[constantBufferDataSize];

        int hresult = d2D1Effect.Get()->GetValue(
            index: D2D1PixelShaderEffectProperty.ConstantBuffer,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BLOB,
            data: constantBufferData,
            dataSize: (uint)constantBufferDataSize);

        Marshal.ThrowExceptionForHR(hresult);

        ReadOnlyMemory<byte> expectedConstantBufferData = D2D1PixelShader.GetConstantBuffer(in shader);

        Assert.IsTrue(expectedConstantBufferData.Span.SequenceEqual(new ReadOnlySpan<byte>(constantBufferData, constantBufferDataSize)));

        int cacheOutput;

        hresult = d2D1Effect.Get()->GetValue(
            index: unchecked((uint)D2D1_PROPERTY.D2D1_PROPERTY_CACHED),
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BOOL,
            data: (byte*)&cacheOutput,
            dataSize: sizeof(int));

        Marshal.ThrowExceptionForHR(hresult);

        Assert.AreEqual(1, cacheOutput);

        D2D1_BUFFER_PRECISION d2D1BufferPrecision;

        hresult = d2D1Effect.Get()->GetValue(
            index: unchecked((uint)D2D1_PROPERTY.D2D1_PROPERTY_PRECISION),
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_UNKNOWN,
            data: (byte*)&d2D1BufferPrecision,
            dataSize: sizeof(D2D1_BUFFER_PRECISION));

        Marshal.ThrowExceptionForHR(hresult);

        Assert.AreEqual(D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_32BPC_FLOAT, d2D1BufferPrecision);

        using ComPtr<ID2D1Image> d2D1Image1 = default;
        using ComPtr<ID2D1Image> d2D1Image2 = default;

        Win2DHelper.GetD2DImage(effect0, canvasDevice, d2D1Image1.GetAddressOf());

        d2D1Effect.Get()->GetInput(0, d2D1Image2.GetAddressOf());

        using ComPtr<IUnknown> d2D1Image1Unknown = default;
        using ComPtr<IUnknown> d2D1Image2Unknown = default;

        Marshal.ThrowExceptionForHR(d2D1Image1.CopyTo(d2D1Image1Unknown.GetAddressOf()));
        Marshal.ThrowExceptionForHR(d2D1Image2.CopyTo(d2D1Image2Unknown.GetAddressOf()));

        // The image from the effect input should be the same retrieved from the original source wrapper
        Assert.IsTrue(d2D1Image1Unknown.Get() == d2D1Image2Unknown.Get());
    }

    [TestMethod]
    public unsafe void Interop_EffectPropertiesAreUpdatedFromNativeEffectCorrectly()
    {
        ShaderWithSomePropertiesAndInputs shader = new(-1234567);

        using PixelShaderEffect<ShaderWith0Inputs> effect0 = new();
        using PixelShaderEffect<ShaderWithSomePropertiesAndInputs> effect1 = new();

        CanvasDevice canvasDevice = new();

        using ComPtr<ID2D1Image> d2D1Image0 = default;
        using ComPtr<ID2D1Image> d2D1Image1 = default;

        // Realize the effects and get the D2D effects
        Win2DHelper.GetD2DImage(effect0, canvasDevice, d2D1Image0.GetAddressOf());
        Win2DHelper.GetD2DImage(effect1, canvasDevice, d2D1Image1.GetAddressOf());

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        Marshal.ThrowExceptionForHR(d2D1Image1.CopyTo(d2D1Effect.GetAddressOf()));

        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect.Get(), in shader);

        int cacheOutput = 1;

        int hresult = d2D1Effect.Get()->SetValue(
            index: unchecked((uint)D2D1_PROPERTY.D2D1_PROPERTY_CACHED),
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BOOL,
            data: (byte*)&cacheOutput,
            dataSize: sizeof(int));

        Marshal.ThrowExceptionForHR(hresult);

        Assert.AreEqual(1, cacheOutput);

        D2D1_BUFFER_PRECISION d2D1BufferPrecision = D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_32BPC_FLOAT;

        hresult = d2D1Effect.Get()->SetValue(
            index: unchecked((uint)D2D1_PROPERTY.D2D1_PROPERTY_PRECISION),
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_UNKNOWN,
            data: (byte*)&d2D1BufferPrecision,
            dataSize: sizeof(D2D1_BUFFER_PRECISION));

        Marshal.ThrowExceptionForHR(hresult);

        d2D1Effect.Get()->SetInput(0, d2D1Image0.Get());

        Assert.AreEqual(shader.Number, effect1.ConstantBuffer.Number);
        Assert.IsTrue(effect1.CacheOutput);
        Assert.AreEqual(CanvasBufferPrecision.Precision32Float, effect1.BufferPrecision);
        Assert.AreSame(effect0, effect1.Sources[0]);
    }

    [TestMethod]
    public unsafe void Interop_IsWrapperRegisteredCorrectly()
    {
        using PixelShaderEffect<ShaderWith0Inputs> effect = new();

        using ComPtr<ID2D1Image> d2D1Image = default;

        Win2DHelper.GetD2DImage(effect, new CanvasDevice(), d2D1Image.GetAddressOf());

        object wrapper = Win2DHelper.GetOrCreate(d2D1Image.Get());

        Assert.AreSame(effect, wrapper);
    }

    [TestMethod]
    public unsafe void Interop_IsWrapperUnregisteredCorrectly_FromExplicitDispose()
    {
        using ComPtr<ID2D1Image> d2D1Image = default;

        PixelShaderEffect<ShaderWith0Inputs> effect = new();
        CanvasDevice canvasDevice = new();

        try
        {
            Win2DHelper.GetD2DImage(effect, canvasDevice, d2D1Image.GetAddressOf());
        }
        finally
        {
            effect.Dispose();
        }

        object wrapper = Win2DHelper.GetOrCreate(d2D1Image.Get(), canvasDevice);

        Assert.IsTrue(wrapper is PixelShaderEffect<ShaderWith0Inputs>);
        Assert.AreNotSame(effect, wrapper);

        // Ensure the new wrapper is disposed as well
        using PixelShaderEffect<ShaderWith0Inputs> _ = (PixelShaderEffect<ShaderWith0Inputs>)wrapper;
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public unsafe void Interop_IsWrapperUnregisteredCorrectly_FromExplicitDispose_FailsWithNoDevice()
    {
        using ComPtr<ID2D1Image> d2D1Image = default;

        using (PixelShaderEffect<ShaderWith0Inputs> effect = new())
        {
            Win2DHelper.GetD2DImage(effect, new CanvasDevice(), d2D1Image.GetAddressOf());
        }

        _ = Win2DHelper.GetOrCreate(d2D1Image.Get());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
    public unsafe void Interop_IsWrapperUnregisteredCorrectly_FromFinalizer_FailsWithNoDevice()
    {
        using ComPtr<ID2D1Image> d2D1Image = default;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void Test(ID2D1Image** d2D1Image)
        {
            PixelShaderEffect<ShaderWith0Inputs> effect = new();

            Win2DHelper.GetD2DImage(effect, new CanvasDevice(), d2D1Image);
        }

        Test(d2D1Image.GetAddressOf());

        GC.Collect();
        GC.WaitForPendingFinalizers();

        _ = Win2DHelper.GetOrCreate(d2D1Image.Get());
    }

    [TestMethod]
    public unsafe void Interop_IsWrapperFactoryRegisteredCorrectlyAndWorking()
    {
        using PixelShaderEffect<ShaderWith0Inputs> effect = new();

        CanvasDevice canvasDevice1 = new();
        CanvasDevice canvasDevice2 = new();

        using ComPtr<ID2D1Image> d2D1Image1 = default;
        using ComPtr<ID2D1Image> d2D1Image2 = default;

        Win2DHelper.GetD2DImage(effect, canvasDevice1, d2D1Image1.GetAddressOf());
        Win2DHelper.GetD2DImage(effect, canvasDevice2, d2D1Image2.GetAddressOf());

        // The first resource is orphaned, so a new wrapper will be created
        object wrapper1 = Win2DHelper.GetOrCreate(d2D1Image1.Get(), canvasDevice1);

        Assert.IsTrue(wrapper1 is PixelShaderEffect<ShaderWith0Inputs>);
        Assert.AreNotSame(wrapper1, effect);

        // Property dispose the new wrapper as well
        using PixelShaderEffect<ShaderWith0Inputs> _ = (PixelShaderEffect<ShaderWith0Inputs>)wrapper1;

        // Getting the same wrapper again should return the newly created one
        object wrapper1FromCache = Win2DHelper.GetOrCreate(d2D1Image1.Get(), canvasDevice1);

        Assert.AreSame(wrapper1, wrapper1FromCache);

        using ComPtr<ID2D1Image> d2D1Image1FromNewWrapper = default;

        // Get the realized image from the newly created wrapper
        Win2DHelper.GetD2DImage((ICanvasImage)wrapper1, canvasDevice1, d2D1Image1FromNewWrapper.GetAddressOf());

        using ComPtr<IUnknown> d2D1Image1Unknown = default;
        using ComPtr<IUnknown> d2D1Image1FromNewWrapperUnknown = default;

        Marshal.ThrowExceptionForHR(d2D1Image1.CopyTo(d2D1Image1Unknown.GetAddressOf()));
        Marshal.ThrowExceptionForHR(d2D1Image1FromNewWrapper.CopyTo(d2D1Image1FromNewWrapperUnknown.GetAddressOf()));

        // The retrieved image should be the same one the new wrapper was created from
        Assert.IsTrue(d2D1Image1Unknown.Get() == d2D1Image1FromNewWrapperUnknown.Get());

        // The second resource is currently registered, so it returns the local wrapper
        object wrapper2 = Win2DHelper.GetOrCreate(d2D1Image2.Get());

        Assert.AreSame(effect, wrapper2);
    }

    [TestMethod]
    public unsafe void Interop_WrapperFactoryDetectsMismatchedDevices()
    {
        using PixelShaderEffect<ShaderWith0Inputs> effect = new();

        CanvasDevice canvasDevice1 = new();
        CanvasDevice canvasDevice2 = new();

        using ComPtr<ID2D1Image> d2D1Image1 = default;
        using ComPtr<ID2D1Image> d2D1Image2 = default;

        Win2DHelper.GetD2DImage(effect, canvasDevice1, d2D1Image1.GetAddressOf());
        Win2DHelper.GetD2DImage(effect, canvasDevice2, d2D1Image2.GetAddressOf());

        // The factory should detect we're passing an incompatible device for the resource
        try
        {
            _ = Win2DHelper.GetOrCreate(d2D1Image1.Get(), canvasDevice2);

            Assert.Fail();
        }
        catch (Exception e)
        {
            Assert.AreEqual(e.HResult, D2DERR.D2DERR_WRONG_FACTORY);
        }
    }

    [D2DInputCount(0)]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct ShaderWithSomeProperties : ID2D1PixelShader
    {
        public readonly float A;
        public readonly int B;
        public readonly int2 C;
        public readonly uint2x2 D;
        public readonly float2 E;

        public float4 Execute()
        {
            return default;
        }
    }

    [D2DInputCount(1)]
    [D2DInputSimple(0)]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct ShaderWithSomePropertiesAndInputs : ID2D1PixelShader
    {
        public readonly int Number;

        public float4 Execute()
        {
            return D2D.GetInput(0);
        }
    }

    [D2DInputCount(0)]
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct ShaderWith0Inputs : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return default;
        }
    }

    [D2DInputCount(1)]
    [D2DInputSimple(0)]
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct ShaderWith1Input : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return D2D.GetInput(0);
        }
    }

    [D2DInputCount(2)]
    [D2DInputSimple(0)]
    [D2DInputSimple(1)]
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct ShaderWith2Inputs : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return D2D.GetInput(0) + D2D.GetInput(1);
        }
    }
}