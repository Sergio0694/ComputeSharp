using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Effects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable IDE0044, IDE0059, IDE0161

[D2DInputCount(0)]
[D2DGeneratedPixelShaderDescriptor]
[AutoConstructor]
internal readonly partial struct ShaderWithScalarVectorAndMatrixTypes_Global : ID2D1PixelShader
{
    public readonly int x;
    public readonly int y;
    public readonly int z;
    public readonly float2x3 f2x3;
    public readonly int a;
    public readonly int1x3 i1x3;
    public readonly double2 d2;
    public readonly int c;
    public readonly int1x2 i1x2;
    public readonly int2x2 i2x2;
    public readonly int d;

    public float4 Execute()
    {
        return 0;
    }
}

[AutoConstructor]
public readonly partial struct FieldsContainer1_Global
{
    public readonly int1x2 i1x2;
    public readonly int2x2 i2x2;
    public readonly int d;
}

[AutoConstructor]
public readonly partial struct FieldsContainer2_Global
{
    public readonly int z;
    public readonly float2x3 f2x3;
    public readonly int a;
    public readonly FieldsContainer3_Global container3;
}

[AutoConstructor]
public readonly partial struct FieldsContainer3_Global
{
    public readonly int1x3 i1x3;
    public readonly double2 d2;
}

[D2DInputCount(0)]
[D2DGeneratedPixelShaderDescriptor]
[AutoConstructor]
internal readonly partial struct ShaderWithScalarVectorAndMatrixTypesInNestedStructs_Global : ID2D1PixelShader
{
    public readonly int x;
    public readonly int y;
    public readonly FieldsContainer2_Global container2;
    public readonly int c;
    public readonly FieldsContainer1_Global container1;

    public float4 Execute()
    {
        return 0;
    }
}

namespace ComputeSharp.D2D1.Tests
{
    [TestClass]
    public partial class D2D1PixelShaderTests
    {
        [TestMethod]
        public void GetInputCount()
        {
            Assert.AreEqual(D2D1PixelShader.GetInputCount<InvertEffect>(), 1);
            Assert.AreEqual(D2D1PixelShader.GetInputCount<PixelateEffect>(), 1);
            Assert.AreEqual(D2D1PixelShader.GetInputCount<ZonePlateEffect>(), 0);
            Assert.AreEqual(D2D1PixelShader.GetInputCount<ShaderWithMultipleInputs>(), 7);
        }

        [TestMethod]
        public void GetInputTypes_NoInputs()
        {
            ReadOnlyMemory<D2D1PixelShaderInputType> inputTypes = D2D1PixelShader.GetInputTypes<ZonePlateEffect>();

            Assert.AreEqual(0, inputTypes.Length);
            Assert.IsFalse(MemoryMarshal.TryGetMemoryManager(inputTypes, out MemoryManager<D2D1PixelShaderInputType>? _));
        }

        [TestMethod]
        public void GetInputTypes_MultipleInputs()
        {
            CollectionAssert.AreEqual(new[]
            {
                D2D1PixelShaderInputType.Simple,
                D2D1PixelShaderInputType.Complex,
                D2D1PixelShaderInputType.Simple,
                D2D1PixelShaderInputType.Complex,
                D2D1PixelShaderInputType.Complex,
                D2D1PixelShaderInputType.Complex,
                D2D1PixelShaderInputType.Simple
            }, D2D1PixelShader.GetInputTypes<ShaderWithMultipleInputs>().ToArray());
        }

        [TestMethod]
        public void GetInputTypes_SameManager()
        {
            ReadOnlyMemory<D2D1PixelShaderInputType> inputTypes1 = D2D1PixelShader.GetInputTypes<ShaderWithMultipleInputs>();

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(inputTypes1, out MemoryManager<D2D1PixelShaderInputType>? memoryManager1));

            ReadOnlyMemory<D2D1PixelShaderInputType> inputTypes2 = D2D1PixelShader.GetInputTypes<ShaderWithMultipleInputs>();

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(inputTypes2, out MemoryManager<D2D1PixelShaderInputType>? memoryManager2));
            Assert.AreSame(memoryManager1, memoryManager2);
        }

        [D2DInputCount(7)]
        [D2DInputSimple(0)]
        [D2DInputSimple(2)]
        [D2DInputComplex(1)]
        [D2DInputComplex(3)]
        [D2DInputComplex(5)]
        [D2DInputSimple(6)]
        [D2DGeneratedPixelShaderDescriptor]
        internal partial struct ShaderWithMultipleInputs : ID2D1PixelShader
        {
            public Float4 Execute()
            {
                return 0;
            }
        }

        [TestMethod]
        public void GetOutputBufferPrecision()
        {
            Assert.AreEqual(D2D1PixelShader.GetOutputBufferPrecision<ShaderWithMultipleInputs>(), D2D1BufferPrecision.Unknown);
            Assert.AreEqual(D2D1PixelShader.GetOutputBufferPrecision<OnlyBufferPrecisionShader>(), D2D1BufferPrecision.UInt16Normalized);
            Assert.AreEqual(D2D1PixelShader.GetOutputBufferPrecision<OnlyChannelDepthShader>(), D2D1BufferPrecision.Unknown);
            Assert.AreEqual(D2D1PixelShader.GetOutputBufferPrecision<CustomBufferOutputShader>(), D2D1BufferPrecision.UInt8NormalizedSrgb);
        }

        [TestMethod]
        public void GetOutputBufferChannelDepth()
        {
            Assert.AreEqual(D2D1PixelShader.GetOutputBufferChannelDepth<ShaderWithMultipleInputs>(), D2D1ChannelDepth.Default);
            Assert.AreEqual(D2D1PixelShader.GetOutputBufferChannelDepth<OnlyBufferPrecisionShader>(), D2D1ChannelDepth.Default);
            Assert.AreEqual(D2D1PixelShader.GetOutputBufferChannelDepth<OnlyChannelDepthShader>(), D2D1ChannelDepth.Four);
            Assert.AreEqual(D2D1PixelShader.GetOutputBufferChannelDepth<CustomBufferOutputShader>(), D2D1ChannelDepth.One);
        }

        [D2DInputCount(0)]
        [D2DGeneratedPixelShaderDescriptor]
        internal partial struct EmptyShader : ID2D1PixelShader
        {
            public Float4 Execute()
            {
                return 0;
            }
        }

        [D2DInputCount(0)]
        [D2DOutputBuffer(D2D1BufferPrecision.UInt16Normalized)]
        [D2DGeneratedPixelShaderDescriptor]
        internal partial struct OnlyBufferPrecisionShader : ID2D1PixelShader
        {
            public Float4 Execute()
            {
                return 0;
            }
        }

        [D2DInputCount(0)]
        [D2DOutputBuffer(D2D1ChannelDepth.Four)]
        [D2DGeneratedPixelShaderDescriptor]
        internal partial struct OnlyChannelDepthShader : ID2D1PixelShader
        {
            public Float4 Execute()
            {
                return 0;
            }
        }

        [D2DInputCount(0)]
        [D2DOutputBuffer(D2D1BufferPrecision.UInt8NormalizedSrgb, D2D1ChannelDepth.One)]
        [D2DGeneratedPixelShaderDescriptor]
        internal partial struct CustomBufferOutputShader : ID2D1PixelShader
        {
            public Float4 Execute()
            {
                return 0;
            }
        }

        [TestMethod]
        public void GetInputDescriptions_Empty()
        {
            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<CheckerboardClipEffect>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<InvertEffect>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<InvertWithThresholdEffect>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<PixelateEffect>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<ZonePlateEffect>().Length, 0);

            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<ShaderWithMultipleInputs>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<EmptyShader>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<OnlyBufferPrecisionShader>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<OnlyChannelDepthShader>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<CustomBufferOutputShader>().Length, 0);
        }

        [TestMethod]
        public void GetInputDescriptions_Custom()
        {
            ReadOnlyMemory<D2D1InputDescription> inputDescriptions = D2D1PixelShader.GetInputDescriptions<ShaderWithInputDescriptions>();

            Assert.AreEqual(inputDescriptions.Length, 5);

            ReadOnlySpan<D2D1InputDescription> span = inputDescriptions.Span;

            Assert.AreEqual(span[0].Index, 0);
            Assert.AreEqual(span[0].Filter, D2D1Filter.MinPointMagLinearMipPoint);
            Assert.AreEqual(span[0].LevelOfDetailCount, 0);

            Assert.AreEqual(span[1].Index, 1);
            Assert.AreEqual(span[1].Filter, D2D1Filter.Anisotropic);
            Assert.AreEqual(span[1].LevelOfDetailCount, 0);

            Assert.AreEqual(span[2].Index, 2);
            Assert.AreEqual(span[2].Filter, D2D1Filter.MinLinearMagPointMinLinear);
            Assert.AreEqual(span[2].LevelOfDetailCount, 4);

            Assert.AreEqual(span[3].Index, 5);
            Assert.AreEqual(span[3].Filter, D2D1Filter.MinMagPointMipLinear);
            Assert.AreEqual(span[3].LevelOfDetailCount, 0);

            Assert.AreEqual(span[4].Index, 6);
            Assert.AreEqual(span[4].Filter, D2D1Filter.MinPointMagMipLinear);
            Assert.AreEqual(span[4].LevelOfDetailCount, 3);
        }

        [D2DInputCount(7)]
        [D2DInputSimple(0)]
        [D2DInputSimple(2)]
        [D2DInputComplex(1)]
        [D2DInputComplex(3)]
        [D2DInputComplex(5)]
        [D2DInputSimple(6)]
        [D2DInputDescription(0, D2D1Filter.MinPointMagLinearMipPoint)]
        [D2DInputDescription(1, D2D1Filter.Anisotropic)]
        [D2DInputDescription(2, D2D1Filter.MinLinearMagPointMinLinear, LevelOfDetailCount = 4)]
        [D2DInputDescription(5, D2D1Filter.MinMagPointMipLinear)]
        [D2DInputDescription(6, D2D1Filter.MinPointMagMipLinear, LevelOfDetailCount = 3)]
        [D2DGeneratedPixelShaderDescriptor]
        internal partial struct ShaderWithInputDescriptions : ID2D1PixelShader
        {
            public Float4 Execute()
            {
                return 0;
            }
        }

        [TestMethod]
        public void GetResourceTextureDescriptions_Empty()
        {
            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<CheckerboardClipEffect>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<InvertEffect>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<InvertWithThresholdEffect>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<PixelateEffect>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<ZonePlateEffect>().Length, 0);

            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<ShaderWithMultipleInputs>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<EmptyShader>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<OnlyBufferPrecisionShader>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<OnlyChannelDepthShader>().Length, 0);
            Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<CustomBufferOutputShader>().Length, 0);
        }

        [TestMethod]
        public void GetResourceTextureDescriptions_Custom()
        {
            ReadOnlyMemory<D2D1ResourceTextureDescription> resourceTextureDescriptions = D2D1PixelShader.GetResourceTextureDescriptions<ShaderWithResourceTextures>();

            Assert.AreEqual(resourceTextureDescriptions.Length, 4);

            ReadOnlySpan<D2D1ResourceTextureDescription> span = resourceTextureDescriptions.Span;

            Assert.AreEqual(span[0].Index, 4);
            Assert.AreEqual(span[0].Dimensions, 1);

            Assert.AreEqual(span[1].Index, 5);
            Assert.AreEqual(span[1].Dimensions, 2);

            Assert.AreEqual(span[2].Index, 6);
            Assert.AreEqual(span[2].Dimensions, 3);

            Assert.AreEqual(span[3].Index, 7);
            Assert.AreEqual(span[3].Dimensions, 2);
        }

        [TestMethod]
        public void GetResourceTextureCount_IsCorrect()
        {
            Assert.AreEqual(0, D2D1PixelShader.GetResourceTextureCount<ShaderWithoutResourceTextures>());
            Assert.AreEqual(1, D2D1PixelShader.GetResourceTextureCount<ShaderWithJustOneResourceTextures>());
            Assert.AreEqual(4, D2D1PixelShader.GetResourceTextureCount<ShaderWithResourceTextures>());
        }

        [D2DInputCount(4)]
        [D2DInputSimple(0)]
        [D2DInputSimple(2)]
        [D2DInputComplex(1)]
        [D2DInputComplex(3)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DGeneratedPixelShaderDescriptor]
        internal partial struct ShaderWithoutResourceTextures : ID2D1PixelShader
        {
            public Float4 Execute()
            {
                return 0;
            }
        }

        [D2DInputCount(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ShaderWithJustOneResourceTextures : ID2D1PixelShader
        {
            [D2DResourceTextureIndex(0)]
            readonly D2D1ResourceTexture1D<float> myTexture;

            public Float4 Execute()
            {
                return this.myTexture.Sample(0);
            }
        }

        [D2DInputCount(4)]
        [D2DInputSimple(0)]
        [D2DInputSimple(2)]
        [D2DInputComplex(1)]
        [D2DInputComplex(3)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ShaderWithResourceTextures(float number) : ID2D1PixelShader
        {
            [D2DResourceTextureIndex(4)]
            readonly D2D1ResourceTexture1D<float4> myTexture1;

            [D2DResourceTextureIndex(5)]
            readonly D2D1ResourceTexture2D<float4> myTexture2;

            [D2DResourceTextureIndex(6)]
            readonly D2D1ResourceTexture3D<float4> myTexture3;

            [D2DResourceTextureIndex(7)]
            readonly D2D1ResourceTexture2D<float> myTexture4;

            public Float4 Execute()
            {
                float4 pixel1 = this.myTexture1[0];
                float4 pixel2 = this.myTexture1.Sample(0.4f);
                float4 pixel3 = this.myTexture2[0, 1];
                float4 pixel4 = this.myTexture2.Sample(0.4f, 0.3f);
                float4 pixel5 = this.myTexture3[0, 1, 2];
                float4 pixel6 = this.myTexture3.Sample(0.4f, 0.5f, 0.6f);
                float number1 = this.myTexture4[0, 2];
                float number2 = this.myTexture4.Sample(0.4f, 0.6f);
                float number3 = number;

                return 0;
            }
        }

        [TestMethod]
        public void GetBytecode_FromDynamicBytecode()
        {
            ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<ShaderWithoutEmbeddedBytecode>();

            Assert.IsFalse(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? _));
            Assert.IsTrue(MemoryMarshal.TryGetArray(bytecode, out ArraySegment<byte> segment));
            Assert.AreEqual(0, segment.Offset);
            Assert.IsTrue(segment.Count > 0);
        }

        [D2DInputCount(0)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ShaderWithoutEmbeddedBytecode : ID2D1PixelShader
        {
            public float4 Execute()
            {
                return 0;
            }
        }

        [TestMethod]
        public void GetBytecode_FromEmbeddedBytecode()
        {
            // Bytecode with no parameters
            ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>();

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? manager));
            Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            // Matching shader profile
            bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1ShaderProfile.PixelShader40Level91);

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
            Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            // Matching compile options
            bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1CompileOptions.Default);

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
            Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            // Matching shader profile and compile options
            bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1ShaderProfile.PixelShader40Level91, D2D1CompileOptions.Default);

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
            Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            // Incorrect profile
            bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1ShaderProfile.PixelShader50);

            Assert.IsFalse(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? _));
            Assert.IsTrue(MemoryMarshal.TryGetArray(bytecode, out ArraySegment<byte> segment));
            Assert.AreEqual(0, segment.Offset);
            Assert.IsTrue(segment.Count > 0);

            // Incorrect options
            bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1CompileOptions.Default | D2D1CompileOptions.EnableStrictness);

            Assert.IsFalse(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? _));
            Assert.IsTrue(MemoryMarshal.TryGetArray(bytecode, out segment));
            Assert.AreEqual(0, segment.Offset);
            Assert.IsTrue(segment.Count > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBytecode_FromEmbeddedBytecode_WithPackMatrixColumnMajor()
        {
            _ = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1CompileOptions.Default | D2D1CompileOptions.PackMatrixColumnMajor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBytecode_FromEmbeddedBytecode_WithTargetProfileAndPackMatrixColumnMajor()
        {
            _ = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1ShaderProfile.PixelShader40Level91, D2D1CompileOptions.Default | D2D1CompileOptions.PackMatrixColumnMajor);
        }

        [D2DInputCount(3)]
        [D2DInputSimple(0)]
        [D2DInputSimple(1)]
        [D2DInputSimple(2)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader40Level91)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ShaderWithEmbeddedBytecode : ID2D1PixelShader
        {
            public float4 Execute()
            {
                float4 dst = D2D.GetInput(0);
                float4 src = D2D.GetInput(1);
                float4 srcMask = D2D.GetInput(2);
                float4 result = ((1 - srcMask) * dst) + (srcMask * src);

                return result;
            }
        }

        [TestMethod]
        public void GetBytecode_FromEmbeddedBytecode_WithCompileOptions()
        {
            // Bytecode with no parameters
            ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecodeAndCompileOptions>();

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? manager));
            Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            // Bytecode with all output parameters
            ReadOnlyMemory<byte> bytecode2 = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecodeAndCompileOptions>(out D2D1ShaderProfile shaderProfile, out D2D1CompileOptions compileOptions);

            // The bytecode is the same
            Assert.IsTrue(bytecode.Span == bytecode2.Span);
            Assert.AreEqual(shaderProfile, D2D1ShaderProfile.PixelShader40Level91);
            Assert.AreEqual(compileOptions, D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2 | D2D1CompileOptions.PackMatrixRowMajor);

            // Matching compile options
            bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecodeAndCompileOptions>(D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2);

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
            Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            // Matching shader profile and compile options
            bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecodeAndCompileOptions>(D2D1ShaderProfile.PixelShader40Level91, D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2);

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
            Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));
        }

        [D2DInputCount(3)]
        [D2DInputSimple(0)]
        [D2DInputSimple(1)]
        [D2DInputSimple(2)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader40Level91)]
        [D2DCompileOptions(D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ShaderWithEmbeddedBytecodeAndCompileOptions : ID2D1PixelShader
        {
            public float4 Execute()
            {
                float4 dst = D2D.GetInput(0);
                float4 src = D2D.GetInput(1);
                float4 srcMask = D2D.GetInput(2);
                float4 result = ((1 - srcMask) * dst) + (srcMask * src);

                return result;
            }
        }

        [TestMethod]
        public void LoadBytecode_VerifyEffectiveValues_ExplicitShaderProfile()
        {
            _ = D2D1PixelShader.LoadBytecode<SimpleShaderWithExplicitShaderProfileAndNoCompileOptions>(out D2D1ShaderProfile shaderProfile, out D2D1CompileOptions compileOptions);

            Assert.AreEqual(shaderProfile, D2D1ShaderProfile.PixelShader40Level91);
            Assert.AreEqual(compileOptions, D2D1CompileOptions.Default);

            _ = D2D1PixelShader.LoadBytecode<ComplexShaderWithExplicitShaderProfileAndNoCompileOptions>(out shaderProfile, out compileOptions);

            // This shader has a complex input, so it can't support shader linking
            Assert.AreEqual(shaderProfile, D2D1ShaderProfile.PixelShader40Level91);
            Assert.AreEqual(compileOptions, D2D1CompileOptions.Default);
        }

        [D2DInputCount(1)]
        [D2DInputSimple(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader40Level91)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct SimpleShaderWithExplicitShaderProfileAndNoCompileOptions : ID2D1PixelShader
        {
            public float4 Execute()
            {
                return D2D.GetInput(0);
            }
        }

        [D2DInputCount(1)]
        [D2DInputComplex(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader40Level91)]
        [D2DRequiresScenePosition]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ComplexShaderWithExplicitShaderProfileAndNoCompileOptions : ID2D1PixelShader
        {
            public float4 Execute()
            {
                float2 uv = D2D.GetScenePosition().XY;

                return D2D.SampleInput(0, uv);
            }
        }

        [TestMethod]
        public void LoadBytecode_VerifyEffectiveValues_ExplicitCompileOptions()
        {
            _ = D2D1PixelShader.LoadBytecode<ShaderWithExplicitCompileOptionsAndNoShaderProfile>(out D2D1ShaderProfile shaderProfile, out D2D1CompileOptions compileOptions);

            // PS5.0 is the default, when no value is specified
            Assert.AreEqual(shaderProfile, D2D1ShaderProfile.PixelShader50);
            Assert.AreEqual(compileOptions, D2D1CompileOptions.OptimizationLevel2 | D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.PackMatrixRowMajor);

            _ = D2D1PixelShader.LoadBytecode<ShaderWithExplicitCompileOptionsAndNoShaderProfile>(D2D1ShaderProfile.PixelShader40, out compileOptions);

            Assert.AreEqual(compileOptions, D2D1CompileOptions.OptimizationLevel2 | D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.PackMatrixRowMajor);
        }

        [D2DInputCount(1)]
        [D2DInputSimple(0)]
        [D2DCompileOptions(D2D1CompileOptions.OptimizationLevel2 | D2D1CompileOptions.IeeeStrictness)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ShaderWithExplicitCompileOptionsAndNoShaderProfile : ID2D1PixelShader
        {
            public float4 Execute()
            {
                return D2D.GetInput(0);
            }
        }

        [TestMethod]
        public void LoadBytecode_ShaderWithSuppressedFxcWarning()
        {
            ReadOnlyMemory<byte> hlslBytecode = D2D1PixelShader.LoadBytecode<ShaderWithSuppressedFxcWarning>(out _, out D2D1CompileOptions compileOptions);

            Assert.AreEqual(D2D1CompileOptions.Default & ~D2D1CompileOptions.WarningsAreErrors, compileOptions);
            Assert.IsTrue(hlslBytecode.Length > 0);
        }

        // See https://github.com/Sergio0694/ComputeSharp/issues/647
        [D2DInputCount(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DGeneratedPixelShaderDescriptor]
        [D2DCompileOptions(D2D1CompileOptions.Default & ~D2D1CompileOptions.WarningsAreErrors)]
        public readonly partial struct ShaderWithSuppressedFxcWarning(int a) : ID2D1PixelShader
        {
            public float4 Execute()
            {
                return a / 4;
            }
        }

        [TestMethod]
        public void LoadBytecode_EnableLinkingIsAppliedCorrectly()
        {
            ReadOnlyMemory<byte> hlslBytecode1 = D2D1PixelShader.LoadBytecode<ReferenceShaderWithDefaultCompileOptionsAndNoLinking>(out _, out D2D1CompileOptions compileOptions1);
            ReadOnlyMemory<byte> hlslBytecode2 = D2D1PixelShader.LoadBytecode<ReferenceShaderWithDefaultCompileOptions>(out _, out D2D1CompileOptions compileOptions2);

            Assert.AreEqual(D2D1CompileOptions.Default & ~D2D1CompileOptions.EnableLinking, compileOptions1);
            Assert.AreEqual(D2D1CompileOptions.Default, compileOptions2);

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(hlslBytecode1, out MemoryManager<byte>? manager1));
            Assert.IsTrue(manager1!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(hlslBytecode2, out MemoryManager<byte>? manager2));
            Assert.IsTrue(manager2!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            // Very similar checks to those in D2D1ShaderCompilerTests.CompileInvertEffectWithDefaultOptionsAndLinking
            Assert.IsTrue(hlslBytecode1.Length > 700);
            Assert.IsTrue(hlslBytecode2.Length > 1500);
            Assert.IsTrue(hlslBytecode2.Length > hlslBytecode1.Length);
            Assert.IsTrue((hlslBytecode2.Length - hlslBytecode1.Length) > 700);
        }

        [D2DInputCount(1)]
        [D2DInputSimple(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DCompileOptions(D2D1CompileOptions.Default)]
        [D2DGeneratedPixelShaderDescriptor]
        public readonly partial struct ReferenceShaderWithDefaultCompileOptions : ID2D1PixelShader
        {
            public float4 Execute()
            {
                float4 color = D2D.GetInput(0);
                float3 rgb = Hlsl.Saturate(1.0f - color.RGB);

                return new(rgb, 1);
            }
        }

        [D2DInputCount(1)]
        [D2DInputSimple(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DCompileOptions(D2D1CompileOptions.Default & ~D2D1CompileOptions.EnableLinking)]
        [D2DGeneratedPixelShaderDescriptor]
        public readonly partial struct ReferenceShaderWithDefaultCompileOptionsAndNoLinking : ID2D1PixelShader
        {
            public float4 Execute()
            {
                float4 color = D2D.GetInput(0);
                float3 rgb = Hlsl.Saturate(1.0f - color.RGB);

                return new(rgb, 1);
            }
        }

        [TestMethod]
        public void LoadBytecode_StripReflectionDataIsAppliedCorrectly()
        {
            ReadOnlyMemory<byte> hlslBytecode1 = D2D1PixelShader.LoadBytecode<ReferenceShaderWithDefaultCompileOptions>(out _, out D2D1CompileOptions compileOptions1);
            ReadOnlyMemory<byte> hlslBytecode2 = D2D1PixelShader.LoadBytecode<ReferenceShaderWithStripReflectionData>(out _, out D2D1CompileOptions compileOptions2);

            Assert.AreEqual(D2D1CompileOptions.Default, compileOptions1);
            Assert.AreEqual(D2D1CompileOptions.Default | D2D1CompileOptions.StripReflectionData, compileOptions2);

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(hlslBytecode1, out MemoryManager<byte>? manager1));
            Assert.IsTrue(manager1!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(hlslBytecode2, out MemoryManager<byte>? manager2));
            Assert.IsTrue(manager2!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

            // Same checks as in D2D1ShaderCompilerTests.CompileInvertEffectWithDefaultOptionsAndStripReflectionData
            Assert.IsTrue(hlslBytecode1.Length > 700);
            Assert.IsTrue(hlslBytecode2.Length > 400);
            Assert.IsTrue(hlslBytecode1.Length > hlslBytecode2.Length);
            Assert.IsTrue((hlslBytecode1.Length - hlslBytecode2.Length) > 300);
        }

        [D2DInputCount(1)]
        [D2DInputSimple(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DCompileOptions(D2D1CompileOptions.Default | D2D1CompileOptions.StripReflectionData)]
        [D2DGeneratedPixelShaderDescriptor]
        public readonly partial struct ReferenceShaderWithStripReflectionData : ID2D1PixelShader
        {
            public float4 Execute()
            {
                float4 color = D2D.GetInput(0);
                float3 rgb = Hlsl.Saturate(1.0f - color.RGB);

                return new(rgb, 1);
            }
        }

        [TestMethod]
        public void GetConstantBufferSize_Empty()
        {
            int size = D2D1PixelShader.GetConstantBufferSize<ShaderWithNoCapturedValues>();

            Assert.AreEqual(size, 0);

            // Repeated calls always return the same result
            size = D2D1PixelShader.GetConstantBufferSize<ShaderWithNoCapturedValues>();

            Assert.AreEqual(size, 0);
        }

        [TestMethod]
        public void GetConstantBuffer_Empty_ReadOnlyMemory()
        {
            ShaderWithNoCapturedValues shader = default;

            ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in shader);

            Assert.AreEqual(constantBuffer.Length, 0);
        }

        // See https://github.com/Sergio0694/ComputeSharp/issues/323
        [TestMethod]
        public void GetConstantBuffer_Empty_Span()
        {
            ShaderWithNoCapturedValues shader = default;

            int bytesWritten = D2D1PixelShader.GetConstantBuffer(in shader, []);

            Assert.AreEqual(bytesWritten, 0);
        }

        [D2DInputCount(1)]
        [D2DInputSimple(0)]
        [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
        [D2DPixelOptions(D2D1PixelOptions.TrivialSampling)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ShaderWithNoCapturedValues : ID2D1PixelShader
        {
            public float4 Execute()
            {
                float4 color = D2D.GetInput(0);

                return color * color;
            }
        }

        [TestMethod]
        public void GetConstantBufferSize()
        {
            int size = D2D1PixelShader.GetConstantBufferSize<ShaderWithScalarVectorAndMatrixTypes>();

            Assert.AreEqual(size, 124);

            // Same as above: repeated calls always return the same result
            size = D2D1PixelShader.GetConstantBufferSize<ShaderWithScalarVectorAndMatrixTypes>();

            Assert.AreEqual(size, 124);
        }

        [TestMethod]
        public void GetConstantBuffer_RoundTrip()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in shader);

            ShaderWithScalarVectorAndMatrixTypes copy = D2D1PixelShader.CreateFromConstantBuffer<ShaderWithScalarVectorAndMatrixTypes>(constantBuffer.Span);

            ValidateShadersAreEqual(in shader, in copy);
        }

        [TestMethod]
        public void GetConstantBuffer_Global_RoundTrip()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in Unsafe.As<ShaderWithScalarVectorAndMatrixTypes, ShaderWithScalarVectorAndMatrixTypes_Global>(ref shader));

            ShaderWithScalarVectorAndMatrixTypes_Global copy = D2D1PixelShader.CreateFromConstantBuffer<ShaderWithScalarVectorAndMatrixTypes_Global>(constantBuffer.Span);

            ValidateShadersAreEqual(in shader, in Unsafe.As<ShaderWithScalarVectorAndMatrixTypes_Global, ShaderWithScalarVectorAndMatrixTypes>(ref copy));
        }

        [TestMethod]
        public void GetConstantBuffer_WithNestedStructs_RoundTrip()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypesInNestedStructs shader);

            ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in shader);

            ShaderWithScalarVectorAndMatrixTypesInNestedStructs copy = D2D1PixelShader.CreateFromConstantBuffer<ShaderWithScalarVectorAndMatrixTypesInNestedStructs>(constantBuffer.Span);

            ValidateShadersAreEqual(in shader, in copy);
        }

        [TestMethod]
        public void GetConstantBuffer_WithNestedStructs_Global_RoundTrip()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypesInNestedStructs shader);

            ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in Unsafe.As<ShaderWithScalarVectorAndMatrixTypesInNestedStructs, ShaderWithScalarVectorAndMatrixTypesInNestedStructs_Global>(ref shader));

            ShaderWithScalarVectorAndMatrixTypesInNestedStructs_Global copy = D2D1PixelShader.CreateFromConstantBuffer<ShaderWithScalarVectorAndMatrixTypesInNestedStructs_Global>(constantBuffer.Span);

            ValidateShadersAreEqual(in shader, in Unsafe.As<ShaderWithScalarVectorAndMatrixTypesInNestedStructs_Global, ShaderWithScalarVectorAndMatrixTypesInNestedStructs>(ref copy));
        }

        [TestMethod]
        public void GetConstantBuffer_TryCreate_RoundTrip()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in shader);

            Assert.IsTrue(D2D1PixelShader.TryCreateFromConstantBuffer(constantBuffer.Span, out ShaderWithScalarVectorAndMatrixTypes copy));

            ValidateShadersAreEqual(in shader, in copy);
        }

        [TestMethod]
        public void GetConstantBuffer_WithNestedStructs_TryCreate_RoundTrip()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypesInNestedStructs shader);

            ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in shader);

            Assert.IsTrue(D2D1PixelShader.TryCreateFromConstantBuffer(constantBuffer.Span, out ShaderWithScalarVectorAndMatrixTypesInNestedStructs copy));

            ValidateShadersAreEqual(in shader, in copy);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(4)]
        [DataRow(27)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetConstantBuffer_BufferTooShort(int size)
        {
            byte[] buffer = new byte[size];

            _ = D2D1PixelShader.CreateFromConstantBuffer<ShaderWithScalarVectorAndMatrixTypes>(buffer);

            Assert.Fail();
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(4)]
        [DataRow(27)]
        public void GetConstantBuffer_TryCreate_BufferTooShort(int size)
        {
            byte[] buffer = new byte[size];

            Assert.IsFalse(D2D1PixelShader.TryCreateFromConstantBuffer(buffer, out ShaderWithScalarVectorAndMatrixTypes _));
        }

        [TestMethod]
        public void GetConstantBuffer_ReadOnlyMemory()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in shader);

            ValidateShaderWithScalarVectorAndMatrixTypesConstantBuffer(constantBuffer.Span);
        }

        [TestMethod]
        public void GetConstantBuffer_Global_ReadOnlyMemory()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in Unsafe.As<ShaderWithScalarVectorAndMatrixTypes, ShaderWithScalarVectorAndMatrixTypes_Global>(ref shader));

            ValidateShaderWithScalarVectorAndMatrixTypesConstantBuffer(constantBuffer.Span);
        }

        [TestMethod]
        public void GetConstantBuffer_Span()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            byte[] buffer = new byte[1024];

            int bytesWritten = D2D1PixelShader.GetConstantBuffer(in shader, buffer);

            Assert.IsTrue(bytesWritten > 0);
            Assert.IsTrue(bytesWritten <= buffer.Length);

            ValidateShaderWithScalarVectorAndMatrixTypesConstantBuffer(buffer.AsSpan(0, bytesWritten));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(4)]
        [DataRow(27)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetConstantBuffer_Span_DestinationTooShort(int size)
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            byte[] buffer = new byte[size];

            _ = D2D1PixelShader.GetConstantBuffer(in shader, buffer);

            Assert.Fail();
        }

        [TestMethod]
        public void GetConstantBuffer_TryGetSpan()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            byte[] buffer = new byte[1024];

            bool success = D2D1PixelShader.TryGetConstantBuffer(in shader, buffer, out int bytesWritten);

            Assert.IsTrue(success);
            Assert.IsTrue(bytesWritten > 0);
            Assert.IsTrue(bytesWritten <= buffer.Length);

            ValidateShaderWithScalarVectorAndMatrixTypesConstantBuffer(buffer.AsSpan(0, bytesWritten));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(4)]
        [DataRow(27)]
        public void GetConstantBuffer_TryGetSpan_DestinationTooShort(int size)
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            byte[] buffer = new byte[size];

            bool success = D2D1PixelShader.TryGetConstantBuffer(in shader, buffer, out int bytesWritten);

            Assert.IsFalse(success);
            Assert.AreEqual(bytesWritten, 0);
        }

        // Gets a ShaderWithScalarVectorAndMatrixTypes instance with test values
        private static void GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader)
        {
            shader = new ShaderWithScalarVectorAndMatrixTypes(
                x: 111,
                y: 222,
                z: 333,
                f2x3: new(55, 44, 888, 111, 222, 333),
                a: 22,
                i1x3: new(1, 2, 3),
                d2: new(3.14, 6.28),
                c: 42,
                i1x2: new(111, 222),
                i2x2: new(11, 22, 33, 44),
                d: 9999);
        }

        // Validates the constant buffer for the test ShaderWithScalarVectorAndMatrixTypes instance
        private unsafe void ValidateShaderWithScalarVectorAndMatrixTypesConstantBuffer(ReadOnlySpan<byte> span)
        {
            Assert.AreEqual(124, span.Length);

            fixed (byte* buffer = span)
            {
                Assert.AreEqual(111, *(int*)&buffer[0]);
                Assert.AreEqual(222, *(int*)&buffer[4]);
                Assert.AreEqual(333, *(int*)&buffer[8]);
                Assert.AreEqual(55, *(float*)&buffer[16]);
                Assert.AreEqual(44, *(float*)&buffer[20]);
                Assert.AreEqual(888, *(float*)&buffer[24]);
                Assert.AreEqual(111, *(float*)&buffer[32]);
                Assert.AreEqual(222, *(float*)&buffer[36]);
                Assert.AreEqual(333, *(float*)&buffer[40]);
                Assert.AreEqual(22, *(int*)&buffer[44]);
                Assert.AreEqual(1, *(int*)&buffer[48]);
                Assert.AreEqual(2, *(int*)&buffer[52]);
                Assert.AreEqual(3, *(int*)&buffer[56]);
                Assert.AreEqual(3.14, *(double*)&buffer[64]);
                Assert.AreEqual(6.28, *(double*)&buffer[72]);
                Assert.AreEqual(42, *(int*)&buffer[80]);
                Assert.AreEqual(111, *(int*)&buffer[84]);
                Assert.AreEqual(222, *(int*)&buffer[88]);
                Assert.AreEqual(11, *(int*)&buffer[96]);
                Assert.AreEqual(22, *(int*)&buffer[100]);
                Assert.AreEqual(33, *(int*)&buffer[112]);
                Assert.AreEqual(44, *(int*)&buffer[116]);
                Assert.AreEqual(9999, *(int*)&buffer[120]);
            }
        }

        // Validates the constant buffer for the test ShaderWithScalarVectorAndMatrixTypes instance
        private static void ValidateShadersAreEqual(in ShaderWithScalarVectorAndMatrixTypes left, in ShaderWithScalarVectorAndMatrixTypes right)
        {
            Assert.AreEqual(left.x, right.x);
            Assert.AreEqual(left.y, right.y);
            Assert.AreEqual(left.z, right.z);
            Assert.AreEqual(left.f2x3.M11, right.f2x3.M11);
            Assert.AreEqual(left.f2x3.M12, right.f2x3.M12);
            Assert.AreEqual(left.f2x3.M13, right.f2x3.M13);
            Assert.AreEqual(left.f2x3.M21, right.f2x3.M21);
            Assert.AreEqual(left.f2x3.M22, right.f2x3.M22);
            Assert.AreEqual(left.f2x3.M23, right.f2x3.M23);
            Assert.AreEqual(left.i1x3.M11, right.i1x3.M11);
            Assert.AreEqual(left.i1x3.M12, right.i1x3.M12);
            Assert.AreEqual(left.i1x3.M13, right.i1x3.M13);
            Assert.AreEqual(left.d2.X, right.d2.X);
            Assert.AreEqual(left.d2.Y, right.d2.Y);
            Assert.AreEqual(left.c, right.c);
            Assert.AreEqual(left.i1x2.M11, right.i1x2.M11);
            Assert.AreEqual(left.i1x2.M12, right.i1x2.M12);
            Assert.AreEqual(left.i2x2.M11, right.i2x2.M11);
            Assert.AreEqual(left.i2x2.M12, right.i2x2.M12);
            Assert.AreEqual(left.i2x2.M21, right.i2x2.M21);
            Assert.AreEqual(left.i2x2.M22, right.i2x2.M22);
            Assert.AreEqual(left.d, right.d);
        }

        [D2DInputCount(0)]
        [D2DGeneratedPixelShaderDescriptor]
        [AutoConstructor]
        internal readonly partial struct ShaderWithScalarVectorAndMatrixTypes : ID2D1PixelShader
        {
            public readonly int x;
            public readonly int y;
            public readonly int z;
            public readonly float2x3 f2x3;
            public readonly int a;
            public readonly int1x3 i1x3;
            public readonly double2 d2;
            public readonly int c;
            public readonly int1x2 i1x2;
            public readonly int2x2 i2x2;
            public readonly int d;

            public float4 Execute()
            {
                return 0;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public unsafe void SetConstantBufferForD2D1DrawInfo_NullD2D1DrawInfo()
        {
            D2D1PixelShader.SetConstantBufferForD2D1DrawInfo<InvertEffect>(null, default);
        }

        [TestMethod]
        public unsafe void GetConstantBufferSize_WithNestedStructs()
        {
            Assert.AreEqual(156, D2D1PixelShader.GetConstantBufferSize<ShaderWithScalarVectorAndMatrixTypesInNestedStructs>());

            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypesInNestedStructs shader);

            ReadOnlyMemory<byte> memory = D2D1PixelShader.GetConstantBuffer(in shader);

            ValidateShaderWithScalarVectorAndMatrixTypesInNestedStructsConstantBuffer(memory.Span);
        }

        [TestMethod]
        public unsafe void GetConstantBufferSize_Global_WithNestedStructs()
        {
            Assert.AreEqual(156, D2D1PixelShader.GetConstantBufferSize<ShaderWithScalarVectorAndMatrixTypesInNestedStructs_Global>());

            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypesInNestedStructs shader);

            ReadOnlyMemory<byte> memory = D2D1PixelShader.GetConstantBuffer(in Unsafe.As<ShaderWithScalarVectorAndMatrixTypesInNestedStructs, ShaderWithScalarVectorAndMatrixTypesInNestedStructs_Global>(ref shader));

            ValidateShaderWithScalarVectorAndMatrixTypesInNestedStructsConstantBuffer(memory.Span);
        }

        [AutoConstructor]
        public readonly partial struct FieldsContainer1
        {
            public readonly int1x2 i1x2;
            public readonly int2x2 i2x2;
            public readonly int d;
        }

        [AutoConstructor]
        public readonly partial struct FieldsContainer2
        {
            public readonly int z;
            public readonly float2x3 f2x3;
            public readonly int a;
            public readonly FieldsContainer3 container3;
        }

        [AutoConstructor]
        public readonly partial struct FieldsContainer3
        {
            public readonly int1x3 i1x3;
            public readonly double2 d2;
        }

        [D2DInputCount(0)]
        [D2DGeneratedPixelShaderDescriptor]
        [AutoConstructor]
        internal readonly partial struct ShaderWithScalarVectorAndMatrixTypesInNestedStructs : ID2D1PixelShader
        {
            public readonly int x;
            public readonly int y;
            public readonly FieldsContainer2 container2;
            public readonly int c;
            public readonly FieldsContainer1 container1;

            public float4 Execute()
            {
                return 0;
            }
        }

        // Gets a ShaderWithScalarVectorAndMatrixTypesInNestedStructs instance with test values
        private static void GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypesInNestedStructs shader)
        {
            shader = new ShaderWithScalarVectorAndMatrixTypesInNestedStructs(
                x: 111,
                y: 222,
                container2: new FieldsContainer2(
                    z: 333,
                    f2x3: new(55, 44, 888, 111, 222, 333),
                    a: 22,
                    container3: new FieldsContainer3(
                        i1x3: new(1, 2, 3),
                        d2: new(3.14, 6.28))),
                c: 42,
                container1: new FieldsContainer1(
                    i1x2: new(111, 222),
                    i2x2: new(11, 22, 33, 44),
                    d: 9999));
        }

        // Validates the constant buffer for the test ShaderWithScalarVectorAndMatrixTypesInNestedStructs instance
        private unsafe void ValidateShaderWithScalarVectorAndMatrixTypesInNestedStructsConstantBuffer(ReadOnlySpan<byte> span)
        {
            Assert.AreEqual(156, span.Length);

            fixed (byte* buffer = span)
            {
                Assert.AreEqual(111, *(int*)&buffer[0]);
                Assert.AreEqual(222, *(int*)&buffer[4]);
                Assert.AreEqual(333, *(int*)&buffer[16]);
                Assert.AreEqual(55, *(float*)&buffer[32]);
                Assert.AreEqual(44, *(float*)&buffer[36]);
                Assert.AreEqual(888, *(float*)&buffer[40]);
                Assert.AreEqual(111, *(float*)&buffer[48]);
                Assert.AreEqual(222, *(float*)&buffer[52]);
                Assert.AreEqual(333, *(float*)&buffer[56]);
                Assert.AreEqual(22, *(int*)&buffer[60]);
                Assert.AreEqual(1, *(int*)&buffer[64]);
                Assert.AreEqual(2, *(int*)&buffer[68]);
                Assert.AreEqual(3, *(int*)&buffer[72]);
                Assert.AreEqual(3.14, *(double*)&buffer[80]);
                Assert.AreEqual(6.28, *(double*)&buffer[88]);
                Assert.AreEqual(42, *(int*)&buffer[96]);
                Assert.AreEqual(111, *(int*)&buffer[112]);
                Assert.AreEqual(222, *(int*)&buffer[116]);
                Assert.AreEqual(11, *(int*)&buffer[128]);
                Assert.AreEqual(22, *(int*)&buffer[132]);
                Assert.AreEqual(33, *(int*)&buffer[144]);
                Assert.AreEqual(44, *(int*)&buffer[148]);
                Assert.AreEqual(9999, *(int*)&buffer[152]);
            }
        }

        // Validates the constant buffer for the test ShaderWithScalarVectorAndMatrixTypesInNestedStructs instance
        private static void ValidateShadersAreEqual(in ShaderWithScalarVectorAndMatrixTypesInNestedStructs left, in ShaderWithScalarVectorAndMatrixTypesInNestedStructs right)
        {
            Assert.AreEqual(left.x, right.x);
            Assert.AreEqual(left.y, right.y);
            Assert.AreEqual(left.container2.z, right.container2.z);
            Assert.AreEqual(left.container2.f2x3.M11, right.container2.f2x3.M11);
            Assert.AreEqual(left.container2.f2x3.M12, right.container2.f2x3.M12);
            Assert.AreEqual(left.container2.f2x3.M13, right.container2.f2x3.M13);
            Assert.AreEqual(left.container2.f2x3.M21, right.container2.f2x3.M21);
            Assert.AreEqual(left.container2.f2x3.M22, right.container2.f2x3.M22);
            Assert.AreEqual(left.container2.f2x3.M23, right.container2.f2x3.M23);
            Assert.AreEqual(left.container2.container3.i1x3.M11, right.container2.container3.i1x3.M11);
            Assert.AreEqual(left.container2.container3.i1x3.M12, right.container2.container3.i1x3.M12);
            Assert.AreEqual(left.container2.container3.i1x3.M13, right.container2.container3.i1x3.M13);
            Assert.AreEqual(left.container2.container3.d2.X, right.container2.container3.d2.X);
            Assert.AreEqual(left.container2.container3.d2.Y, right.container2.container3.d2.Y);
            Assert.AreEqual(left.c, right.c);
            Assert.AreEqual(left.container1.i1x2.M11, right.container1.i1x2.M11);
            Assert.AreEqual(left.container1.i1x2.M12, right.container1.i1x2.M12);
            Assert.AreEqual(left.container1.i2x2.M11, right.container1.i2x2.M11);
            Assert.AreEqual(left.container1.i2x2.M12, right.container1.i2x2.M12);
            Assert.AreEqual(left.container1.i2x2.M21, right.container1.i2x2.M21);
            Assert.AreEqual(left.container1.i2x2.M22, right.container1.i2x2.M22);
            Assert.AreEqual(left.container1.d, right.container1.d);
        }

        public partial class ContainingClass
        {
            public partial record ContainingRecord
            {
                public partial struct ContainingStruct
                {
                    public partial record struct ContainingRecordStruct
                    {
                        public partial interface IContainingInterface
                        {
                            [D2DInputCount(0)]
                            [D2DGeneratedPixelShaderDescriptor]
                            public readonly partial struct DeeplyNestedShader : ID2D1PixelShader
                            {
                                public float4 Execute()
                                {
                                    return 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void DeeplyNestedShaderType_VerifyGenerator()
        {
            // Just a sanity check that the generated code is present
            Assert.AreEqual(0, D2D1PixelShader.GetInputCount<ContainingClass.ContainingRecord.ContainingStruct.ContainingRecordStruct.IContainingInterface.DeeplyNestedShader>());
        }

        // https://github.com/Sergio0694/ComputeSharp/issues/727
        [TestMethod]
        public unsafe void ShaderWithComputeSharpBoolInstanceField_IsMarshalledCorrectly()
        {
            Assert.AreEqual(4, D2D1PixelShader.GetConstantBufferSize<ShaderWithComputeSharpBoolInstanceField>());

            ShaderWithComputeSharpBoolInstanceField shader = new(true);

            ReadOnlyMemory<byte> memory = D2D1PixelShader.GetConstantBuffer(in shader);

            ShaderWithComputeSharpBoolInstanceField roundTrip = D2D1PixelShader.CreateFromConstantBuffer<ShaderWithComputeSharpBoolInstanceField>(memory.Span);

            Assert.IsTrue(shader.value);
        }

        [D2DInputCount(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DGeneratedPixelShaderDescriptor]
        [AutoConstructor]
        internal readonly partial struct ShaderWithComputeSharpBoolInstanceField : ID2D1PixelShader
        {
            public readonly Bool value;

            public float4 Execute()
            {
                return Hlsl.BoolToFloat(this.value);
            }
        }

        // See https://github.com/Sergio0694/ComputeSharp/issues/808
        [TestMethod]
        public void GetConstantBuffer_IsBitwiseEquatable()
        {
            GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

            [MethodImpl(MethodImplOptions.NoInlining)]
            static void DirtyStack()
            {
                Span<byte> span = stackalloc byte[4096];

                Random.Shared.NextBytes(span);

                Dummy(span);
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            static void Dummy(ReadOnlySpan<byte> span)
            {
            }

            DirtyStack();

            ReadOnlyMemory<byte> memory = D2D1PixelShader.GetConstantBuffer(in shader);

            ValidateShaderWithScalarVectorAndMatrixTypesConstantBuffer_WithPadding(memory.Span);
        }

        // Same as 'ValidateShaderWithScalarVectorAndMatrixTypesConstantBuffer', but also validates the padding
        private unsafe void ValidateShaderWithScalarVectorAndMatrixTypesConstantBuffer_WithPadding(ReadOnlySpan<byte> span)
        {
            Assert.AreEqual(124, span.Length);

            fixed (byte* buffer = span)
            {
                Assert.AreEqual(111, *(int*)&buffer[0]);
                Assert.AreEqual(222, *(int*)&buffer[4]);
                Assert.AreEqual(333, *(int*)&buffer[8]);
                Assert.AreEqual(0, *(int*)&buffer[12]); // Padding
                Assert.AreEqual(55, *(float*)&buffer[16]);
                Assert.AreEqual(44, *(float*)&buffer[20]);
                Assert.AreEqual(888, *(float*)&buffer[24]);
                Assert.AreEqual(0, *(int*)&buffer[28]); // Padding
                Assert.AreEqual(111, *(float*)&buffer[32]);
                Assert.AreEqual(222, *(float*)&buffer[36]);
                Assert.AreEqual(333, *(float*)&buffer[40]);
                Assert.AreEqual(22, *(int*)&buffer[44]);
                Assert.AreEqual(1, *(int*)&buffer[48]);
                Assert.AreEqual(2, *(int*)&buffer[52]);
                Assert.AreEqual(3, *(int*)&buffer[56]);
                Assert.AreEqual(0, *(int*)&buffer[60]); // Padding
                Assert.AreEqual(3.14, *(double*)&buffer[64]);
                Assert.AreEqual(6.28, *(double*)&buffer[72]);
                Assert.AreEqual(42, *(int*)&buffer[80]);
                Assert.AreEqual(111, *(int*)&buffer[84]);
                Assert.AreEqual(222, *(int*)&buffer[88]);
                Assert.AreEqual(0, *(int*)&buffer[92]); // Padding
                Assert.AreEqual(11, *(int*)&buffer[96]);
                Assert.AreEqual(22, *(int*)&buffer[100]);
                Assert.AreEqual(0, *(int*)&buffer[104]); // Padding
                Assert.AreEqual(0, *(int*)&buffer[108]); // Padding
                Assert.AreEqual(33, *(int*)&buffer[112]);
                Assert.AreEqual(44, *(int*)&buffer[116]);
                Assert.AreEqual(9999, *(int*)&buffer[120]);
            }
        }
    }
}