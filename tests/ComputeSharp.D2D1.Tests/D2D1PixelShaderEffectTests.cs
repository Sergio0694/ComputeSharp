using System;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Effects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("D2D1PixelShaderEffect")]
public class D2D1PixelShaderEffectTests
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
}