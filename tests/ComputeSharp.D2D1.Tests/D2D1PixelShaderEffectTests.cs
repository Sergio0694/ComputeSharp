using System;
using System.ComponentModel;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Effects;
using ComputeSharp.D2D1.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Win32;
using Win32.Graphics.Direct2D;

#pragma warning disable IDE0022, IDE0044

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("D2D1PixelShaderEffect")]
public partial class D2D1PixelShaderEffectTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void RegisterForD2D1Factory1_NullD2D1Factory1()
    {
        D2D1PixelShaderEffect.RegisterForD2D1Factory1<InvertEffect>(null, out _);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void RegisterForD2D1Factory1_WithTransformMapperFactory_NullD2D1Factory1()
    {
        D2D1PixelShaderEffect.RegisterForD2D1Factory1<PixelateEffect>(null, out _);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void CreateFromD2D1DeviceContext_NullD2D1DeviceContext()
    {
        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<InvertEffect>(null, (void**)1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void CreateFromD2D1DeviceContext_NullD2D1Effect()
    {
        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<InvertEffect>((void*)1, null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void SetConstantBufferForD2D1Effect_NullD2D1Effect()
    {
        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect<InvertEffect>(null, default);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void SetResourceTextureManagerForD2D1Effect_NullD2D1Effect()
    {
        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(null, (void*)1, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void SetResourceTextureManagerForD2D1Effect_NullD2D1ResourceTextureManager()
    {
        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect((void*)1, (void*)null, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void SetResourceTextureManagerForD2D1Effect_RCW_NullD2D1Effect()
    {
        D2D1ResourceTextureManager resourceTextureManager = new(
            extents: stackalloc uint[] { 64 },
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.One,
            filter: D2D1Filter.MinLinearMagMipPoint,
            extendModes: stackalloc D2D1ExtendMode[] { D2D1ExtendMode.Clamp });

        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(null, resourceTextureManager, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void SetResourceTextureManagerForD2D1Effect_RCW_NullD2D1ResourceTextureManager()
    {
        D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect((void*)1, (D2D1ResourceTextureManager)null!, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void SetTransformMapperForD2D1Effect_RCW_NullD2D1Effect()
    {
        D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect(null, (void*)1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void SetTransformMapperForD2D1Effect_RCW_NullD2D1ResourceTextureManager()
    {
        D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect((void*)1, (D2D1TransformMapper<NullConstantBufferShader>)null!);
    }

    [TestMethod]
    [ExpectedException(typeof(Win32Exception))]
    public unsafe void NullConstantBuffer_DrawImageFails()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<NullConstantBufferShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<NullConstantBufferShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), 128, 128);

        D2D1Helper.DrawEffect(d2D1DeviceContext.Get(), d2D1Effect.Get());
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [AutoConstructor]
    private partial struct NullConstantBufferShader : ID2D1PixelShader
    {
        private float dummy;

        public float4 Execute()
        {
            return this.dummy;
        }
    }

    [TestMethod]
    public unsafe void GetValueSize_ConstantBuffer()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<ConstantBufferSizeTestShader>(d2D1Factory2.Get(), out _);

        using ComPtr<ID2D1Effect> d2D1Effect = default;

        D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<ConstantBufferSizeTestShader>(d2D1DeviceContext.Get(), (void**)d2D1Effect.GetAddressOf());

        uint size = d2D1Effect.Get()->GetValueSize(D2D1PixelShaderEffectProperty.ConstantBuffer);

        Assert.AreEqual(D2D1PixelShader.GetConstantBufferSize<ConstantBufferSizeTestShader>(), (int)size);
    }

    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [AutoConstructor]
    private partial struct ConstantBufferSizeTestShader : ID2D1PixelShader
    {
        private float a;
        private float b;
        private float3 c;
        private int d;
        private int e;

        public float4 Execute()
        {
            return this.a + this.b + this.c.X + this.d + this.e;
        }
    }

    [TestMethod]
    public unsafe void DefaultEffectId_MatchesValue()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<ShaderWithDefaultEffectId>(d2D1Factory2.Get(), out Guid effectId);

        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectId<ShaderWithDefaultEffectId>(), effectId);
        Assert.AreNotEqual(D2D1PixelShaderEffect.GetEffectId<ShaderWithDefaultEffectId2>(), effectId);
    }

    [D2DInputCount(0)]
    private partial struct ShaderWithDefaultEffectId : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return 0;
        }
    }

    [D2DInputCount(0)]
    private partial struct ShaderWithDefaultEffectId2 : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return 0;
        }
    }

    [TestMethod]
    public unsafe void ExplicitEffectId_MatchesValue()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<ShaderWithExplicitEffectId>(d2D1Factory2.Get(), out Guid effectId);

        Assert.AreEqual(Guid.Parse("8E1F7F49-EF0D-4242-8912-08ADA36AB4EC"), effectId);
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectId<ShaderWithExplicitEffectId>(), effectId);
    }

    [D2DInputCount(0)]
    [D2DEffectId("8E1F7F49-EF0D-4242-8912-08ADA36AB4EC")]
    private partial struct ShaderWithExplicitEffectId : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return 0;
        }
    }
}