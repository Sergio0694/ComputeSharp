using System;
using System.ComponentModel;
using ComputeSharp.D2D1;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Effects;
using ComputeSharp.D2D1.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

[assembly: D2DEnableRuntimeCompilation]

#pragma warning disable IDE0022, IDE0044

namespace ComputeSharp.D2D1.Tests;

[TestClass]
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
            extents: [64],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.One,
            filter: D2D1Filter.MinLinearMagMipPoint,
            extendModes: [D2D1ExtendMode.Clamp]);

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
        D2D1PixelShaderEffect.SetTransformMapperForD2D1Effect((void*)1, (D2D1DrawTransformMapper<NullConstantBufferShader>)null!);
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
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct NullConstantBufferShader : ID2D1PixelShader
    {
        private readonly float dummy;

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
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct ConstantBufferSizeTestShader : ID2D1PixelShader
    {
        private readonly float a;
        private readonly float b;
        private readonly float3 c;
        private readonly int d;
        private readonly int e;

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
        D2D1PixelShaderEffect.RegisterForD2D1Factory1<ShaderWithDefaultEffectId2>(d2D1Factory2.Get(), out Guid effectId2);

        // Ensure that the dynamically generated GUIDs are deterministic and stable
        Assert.AreEqual(Guid.Parse("F5287184-0EC7-0BC6-3942-8BFB70E77C4B"), effectId);
        Assert.AreEqual(Guid.Parse("96310279-E716-D336-5097-BE516792CBF0"), effectId2);

        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectId<ShaderWithDefaultEffectId>(), effectId);
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectId<ShaderWithDefaultEffectId2>(), effectId2);
    }

    [D2DInputCount(0)]
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct ShaderWithDefaultEffectId : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return 0;
        }
    }

    [D2DInputCount(0)]
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct ShaderWithDefaultEffectId2 : ID2D1PixelShader
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
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct ShaderWithExplicitEffectId : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return 0;
        }
    }

    [TestMethod]
    public unsafe void DefaultEffectMetadata_MatchesValue()
    {
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectDisplayName<ShaderWithDefaultEffectDisplayName>(), null);
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectDisplayName<ShaderWithDefaultEffectDisplayName>(), null);
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectDisplayName<ShaderWithDefaultEffectDisplayName>(), null);
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectDisplayName<ShaderWithDefaultEffectDisplayName>(), null);
    }

    [D2DInputCount(0)]
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct ShaderWithDefaultEffectDisplayName : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return 0;
        }
    }

    [TestMethod]
    public unsafe void ExplicitEffectMetadata1_MatchesValue()
    {
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectDisplayName<ShaderWithExplicitEffectDisplayName1>(), "Fancy blur");
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectDescription<ShaderWithExplicitEffectDisplayName1>(), null);
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectCategory<ShaderWithExplicitEffectDisplayName1>(), null);
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectAuthor<ShaderWithExplicitEffectDisplayName1>(), null);
    }

    [D2DInputCount(0)]
    [D2DEffectDisplayName("Fancy blur")]
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct ShaderWithExplicitEffectDisplayName1 : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return 0;
        }
    }

    [TestMethod]
    public unsafe void ExplicitEffectMetadata2_MatchesValue()
    {
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectDisplayName<ShaderWithExplicitEffectDisplayName2>(), "Fancy&quot;&lt;");
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectDescription<ShaderWithExplicitEffectDisplayName2>(), "A test effect with some custom metadata");
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectCategory<ShaderWithExplicitEffectDisplayName2>(), "Test effects!");
        Assert.AreEqual(D2D1PixelShaderEffect.GetEffectAuthor<ShaderWithExplicitEffectDisplayName2>(), "Bob Ross");
    }

    [D2DInputCount(0)]
    [D2DEffectDisplayName("F\r\na\nncy\"<")]
    [D2DEffectDescription("A test effect with \nsome custom metadata")]
    [D2DEffectCategory("Test effects!")]
    [D2DEffectAuthor("Bob \r\nRoss")]
    [D2DGeneratedPixelShaderDescriptor]
    internal partial struct ShaderWithExplicitEffectDisplayName2 : ID2D1PixelShader
    {
        public float4 Execute()
        {
            return 0;
        }
    }
}