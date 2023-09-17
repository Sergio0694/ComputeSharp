using System;
using ComputeSharp.D2D1.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("D2D1EffectRegistrationData")]
public partial class D2D1EffectRegistrationDataTests
{
    [TestMethod]
    public unsafe void EffectRegistrationData_Validate()
    {
        ReadOnlyMemory<byte> blob = D2D1PixelShaderEffect.GetRegistrationBlob<TestRegistrationBlobShader>(out Guid effectId);
        D2D1EffectRegistrationData.V1 data = D2D1EffectRegistrationData.V1.Load(blob);

        Assert.AreEqual(effectId, data.ClassId);
        Assert.AreEqual(2, data.NumberOfInputs);
        Assert.IsTrue(data.EffectFactory is not null);
        Assert.AreEqual(18, data.PropertyBindings.Length);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ConstantBuffer), data.PropertyBindings.Span[0].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager0), data.PropertyBindings.Span[1].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager1), data.PropertyBindings.Span[2].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager2), data.PropertyBindings.Span[3].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager3), data.PropertyBindings.Span[4].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager4), data.PropertyBindings.Span[5].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager5), data.PropertyBindings.Span[6].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager6), data.PropertyBindings.Span[7].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager7), data.PropertyBindings.Span[8].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager8), data.PropertyBindings.Span[9].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager9), data.PropertyBindings.Span[10].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager10), data.PropertyBindings.Span[11].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager11), data.PropertyBindings.Span[12].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager12), data.PropertyBindings.Span[13].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager13), data.PropertyBindings.Span[14].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager14), data.PropertyBindings.Span[15].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager15), data.PropertyBindings.Span[16].PropertyName);
        Assert.AreEqual(nameof(D2D1PixelShaderEffectProperty.TransformMapper), data.PropertyBindings.Span[17].PropertyName);

        foreach (ref readonly D2D1PropertyBinding propertyBinding in data.PropertyBindings.Span)
        {
            Assert.IsTrue(propertyBinding.SetFunction is not null);
            Assert.IsTrue(propertyBinding.GetFunction is not null);
        }

        Assert.AreEqual("""
            <?xml version='1.0'?>
            <Effect>
                <Property name='DisplayName' type='string' value=''/>
                <Property name='Author' type='string' value='ComputeSharp.D2D1'/>
                <Property name='Category' type='string' value='Stylize'/>
                <Property name='Description' type='string' value='A custom D2D1 effect using a pixel shader'/>
                <Inputs>
                    <Input name='Source0'/>
                    <Input name='Source1'/>
                </Inputs>
                <Property name='ConstantBuffer' type='blob'>
                    <Property name='DisplayName' type='string' value='ConstantBuffer'/>
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
                <Property name='ResourceTextureManager12' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager12'/>
                </Property>
                <Property name='ResourceTextureManager13' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager13'/>
                </Property>
                <Property name='ResourceTextureManager14' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager14'/>
                </Property>
                <Property name='ResourceTextureManager15' type='iunknown'>
                    <Property name='DisplayName' type='string' value='ResourceTextureManager15'/>
                </Property>
                <Property name='TransformMapper' type='iunknown'>
                    <Property name='DisplayName' type='string' value='TransformMapper'/>
                </Property>
            </Effect>
            """, data.PropertyXml);
    }

    [D2DInputCount(2)]
    [D2DRequiresScenePosition]
    [AutoConstructor]
    private readonly partial struct TestRegistrationBlobShader : ID2D1PixelShader
    {
        private readonly float a;

        public float4 Execute()
        {
            return this.a;
        }
    }
}