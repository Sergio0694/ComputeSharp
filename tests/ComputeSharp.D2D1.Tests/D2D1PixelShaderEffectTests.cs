using System;
using System.ComponentModel;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Effects;
using ComputeSharp.D2D1.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

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
    public unsafe void RegisterForD2D1Factory1_WithTransformMapper_NullD2D1Factory1()
    {
        D2D1PixelShaderEffect.RegisterForD2D1Factory1<PixelateEffect.Shader, PixelateEffect>(null, out _);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
    public unsafe void RegisterForD2D1Factory1_WithTransformMapperFactory_NullD2D1Factory1()
    {
        D2D1PixelShaderEffect.RegisterForD2D1Factory1(null, () => new PixelateEffect(), out _);
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
        D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect<InvertEffect>(default, null);
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
    [ExpectedException(typeof(Win32Exception))]
    public unsafe void NullConstantBuffer_DrawImageFails()
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        D2D1PixelShaderEffect.RegisterForD2D1Factory1<NullConstantBufferShader>(d2D1Factory2.Get(), null, out _);

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
            return dummy;
        }
    }
}