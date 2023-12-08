using System;
using ComputeSharp.D2D1.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
public partial class D2D1EffectRegistrationDataTests
{
    [TestMethod]
    public unsafe void EffectRegistrationData_Validate()
    {
        ReadOnlyMemory<byte> blob = D2D1PixelShaderEffect.GetRegistrationBlob<TestRegistrationBlobShader>(out Guid effectId);
        D2D1EffectRegistrationData.V1 data = D2D1EffectRegistrationData.V1.Load(blob);

        Assert.AreEqual(Guid.Parse("8AA3BDF3-5EC8-34D9-CFBC-97A2B4068FFD"), effectId);
        Assert.AreEqual(effectId, data.ClassId);
        Assert.AreEqual(2, data.NumberOfInputs);
        Assert.IsTrue(data.EffectFactory is not null);
        Assert.AreEqual(2, data.PropertyBindings.Length);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ConstantBuffer), data.PropertyBindings.Span[0].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.TransformMapper), data.PropertyBindings.Span[1].PropertyName);

        foreach (ref readonly D2D1PropertyBinding propertyBinding in data.PropertyBindings.Span)
        {
            Assert.IsTrue(propertyBinding.SetFunction is not null);
            Assert.IsTrue(propertyBinding.GetFunction is not null);
        }

        Assert.AreEqual("""
            <?xml version='1.0'?>
            <Effect>
                <Property name='DisplayName' type='string' value=''/>
                <Property name='Description' type='string' value=''/>
                <Property name='Category' type='string' value=''/>
                <Property name='Author' type='string' value=''/>
                <Inputs>
                    <Input name='Source0'/>
                    <Input name='Source1'/>
                </Inputs>
                <Property name='ConstantBuffer' type='blob'>
                    <Property name='DisplayName' type='string' value='ConstantBuffer'/>
                </Property>
                <Property name='TransformMapper' type='iunknown'>
                    <Property name='DisplayName' type='string' value='TransformMapper'/>
                </Property>
            </Effect>
            """, data.PropertyXml);
    }

    [D2DInputCount(2)]
    [D2DRequiresScenePosition]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct TestRegistrationBlobShader : ID2D1PixelShader
    {
        private readonly float a;

        public float4 Execute()
        {
            return this.a;
        }
    }

    [TestMethod]
    public unsafe void EffectRegistrationData_WithCustomMetadata_Validate()
    {
        ReadOnlyMemory<byte> blob = D2D1PixelShaderEffect.GetRegistrationBlob<TestRegistrationBlobWithCustomMetadataShader>(out Guid effectId);
        D2D1EffectRegistrationData.V1 data = D2D1EffectRegistrationData.V1.Load(blob);

        Assert.AreEqual(Guid.Parse("ABF58390-91D2-4F58-AA2B-22D0F6AC069C"), effectId);
        Assert.AreEqual("""
            <?xml version='1.0'?>
            <Effect>
                <Property name='DisplayName' type='string' value='EffectWithCustomMetadata'/>
                <Property name='Description' type='string' value='A test effect with some custom metadata'/>
                <Property name='Category' type='string' value='Test effects'/>
                <Property name='Author' type='string' value='Bob Ross'/>
                <Inputs>
                    <Input name='Source0'/>
                    <Input name='Source1'/>
                </Inputs>
                <Property name='ConstantBuffer' type='blob'>
                    <Property name='DisplayName' type='string' value='ConstantBuffer'/>
                </Property>
                <Property name='TransformMapper' type='iunknown'>
                    <Property name='DisplayName' type='string' value='TransformMapper'/>
                </Property>
            </Effect>
            """, data.PropertyXml);
    }

    [D2DInputCount(2)]
    [D2DEffectId("ABF58390-91D2-4F58-AA2B-22D0F6AC069C")]
    [D2DEffectDisplayName("EffectWithCustomMetadata")]
    [D2DEffectDescription("A test effect with some custom metadata")]
    [D2DEffectCategory("Test effects")]
    [D2DEffectAuthor("Bob Ross")]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct TestRegistrationBlobWithCustomMetadataShader : ID2D1PixelShader
    {
        private readonly float a;

        public float4 Execute()
        {
            return this.a;
        }
    }

    [TestMethod]
    public unsafe void EffectRegistrationData_WithResourceTextures_Validate()
    {
        ReadOnlyMemory<byte> blob = D2D1PixelShaderEffect.GetRegistrationBlob<TestRegistrationBlobWithResourceTextures>(out _);
        D2D1EffectRegistrationData.V1 data = D2D1EffectRegistrationData.V1.Load(blob);

        Assert.IsTrue(data.EffectFactory is not null);
        Assert.AreEqual(14, data.PropertyBindings.Length);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ConstantBuffer), data.PropertyBindings.Span[0].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.TransformMapper), data.PropertyBindings.Span[1].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager0), data.PropertyBindings.Span[2].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager1), data.PropertyBindings.Span[3].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager2), data.PropertyBindings.Span[4].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager3), data.PropertyBindings.Span[5].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager4), data.PropertyBindings.Span[6].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager5), data.PropertyBindings.Span[7].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager6), data.PropertyBindings.Span[8].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager7), data.PropertyBindings.Span[9].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager8), data.PropertyBindings.Span[10].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager9), data.PropertyBindings.Span[11].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager10), data.PropertyBindings.Span[12].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager11), data.PropertyBindings.Span[13].PropertyName);

        foreach (ref readonly D2D1PropertyBinding propertyBinding in data.PropertyBindings.Span)
        {
            Assert.IsTrue(propertyBinding.SetFunction is not null);
            Assert.IsTrue(propertyBinding.GetFunction is not null);
        }

        Assert.AreEqual("""
            <?xml version='1.0'?>
            <Effect>
                <Property name='DisplayName' type='string' value=''/>
                <Property name='Description' type='string' value=''/>
                <Property name='Category' type='string' value=''/>
                <Property name='Author' type='string' value=''/>
                <Inputs>
                </Inputs>
                <Property name='ConstantBuffer' type='blob'>
                    <Property name='DisplayName' type='string' value='ConstantBuffer'/>
                </Property>
                <Property name='TransformMapper' type='iunknown'>
                    <Property name='DisplayName' type='string' value='TransformMapper'/>
                </Property>
                <Property name='ResourceTextureManager0' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager0'/>
                </Property>
                <Property name='ResourceTextureManager1' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager1'/>
                </Property>
                <Property name='ResourceTextureManager2' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager2'/>
                </Property>
                <Property name='ResourceTextureManager3' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager3'/>
                </Property>
                <Property name='ResourceTextureManager4' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager4'/>
                </Property>
                <Property name='ResourceTextureManager5' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager5'/>
                </Property>
                <Property name='ResourceTextureManager6' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager6'/>
                </Property>
                <Property name='ResourceTextureManager7' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager7'/>
                </Property>
                <Property name='ResourceTextureManager8' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager8'/>
                </Property>
                <Property name='ResourceTextureManager9' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager9'/>
                </Property>
                <Property name='ResourceTextureManager10' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager10'/>
                </Property>
                <Property name='ResourceTextureManager11' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager11'/>
                </Property>
            </Effect>
            """, data.PropertyXml);
    }

    [D2DInputCount(0)]
    [D2DGeneratedPixelShaderDescriptor]
    [AutoConstructor]
    internal readonly partial struct TestRegistrationBlobWithResourceTextures : ID2D1PixelShader
    {
        [D2DResourceTextureIndex(0)]
        public readonly D2D1ResourceTexture1D<float> t0;

        [D2DResourceTextureIndex(1)]
        public readonly D2D1ResourceTexture1D<float> t1;

        [D2DResourceTextureIndex(2)]
        public readonly D2D1ResourceTexture1D<float> t2;

        [D2DResourceTextureIndex(3)]
        public readonly D2D1ResourceTexture1D<float> t3;

        [D2DResourceTextureIndex(4)]
        public readonly D2D1ResourceTexture1D<float> t4;

        [D2DResourceTextureIndex(5)]
        public readonly D2D1ResourceTexture1D<float> t5;

        [D2DResourceTextureIndex(6)]
        public readonly D2D1ResourceTexture1D<float> t6;

        [D2DResourceTextureIndex(7)]
        public readonly D2D1ResourceTexture1D<float> t7;

        [D2DResourceTextureIndex(8)]
        public readonly D2D1ResourceTexture1D<float> t8;

        [D2DResourceTextureIndex(9)]
        public readonly D2D1ResourceTexture1D<float> t9;

        [D2DResourceTextureIndex(10)]
        public readonly D2D1ResourceTexture1D<float> t10;

        [D2DResourceTextureIndex(11)]
        public readonly D2D1ResourceTexture1D<float> t11;

        public float4 Execute()
        {
            return 0;
        }
    }
}