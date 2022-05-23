using System;
using System.Buffers;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Effects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("D2D1PixelShader")]
public partial class D2D1PixelShaderTests
{
    [TestMethod]
    public unsafe void GetInputCount()
    {
        Assert.AreEqual(D2D1PixelShader.GetInputCount<InvertEffect>(), 1);
        Assert.AreEqual(D2D1PixelShader.GetInputCount<PixelateEffect.Shader>(), 1);
        Assert.AreEqual(D2D1PixelShader.GetInputCount<ZonePlateEffect>(), 0);
        Assert.AreEqual(D2D1PixelShader.GetInputCount<ShaderWithMultipleInputs>(), 7);
    }

    [TestMethod]
    public unsafe void GetInputType()
    {
        Assert.AreEqual(D2D1PixelShader.GetInputType<ShaderWithMultipleInputs>(0), D2D1PixelShaderInputType.Simple);
        Assert.AreEqual(D2D1PixelShader.GetInputType<ShaderWithMultipleInputs>(1), D2D1PixelShaderInputType.Complex);
        Assert.AreEqual(D2D1PixelShader.GetInputType<ShaderWithMultipleInputs>(2), D2D1PixelShaderInputType.Simple);
        Assert.AreEqual(D2D1PixelShader.GetInputType<ShaderWithMultipleInputs>(3), D2D1PixelShaderInputType.Complex);
        Assert.AreEqual(D2D1PixelShader.GetInputType<ShaderWithMultipleInputs>(4), D2D1PixelShaderInputType.Complex);
        Assert.AreEqual(D2D1PixelShader.GetInputType<ShaderWithMultipleInputs>(5), D2D1PixelShaderInputType.Complex);
    }

    [D2DInputCount(7)]
    [D2DInputSimple(0)]
    [D2DInputSimple(2)]
    [D2DInputComplex(1)]
    [D2DInputComplex(3)]
    [D2DInputComplex(5)]
    [D2DInputSimple(6)]
    partial struct ShaderWithMultipleInputs : ID2D1PixelShader
    {
        public Float4 Execute()
        {
            return 0;
        }
    }

    [TestMethod]
    public unsafe void GetOutputBufferPrecision()
    {
        Assert.AreEqual(D2D1PixelShader.GetOutputBufferPrecision<ShaderWithMultipleInputs>(), D2D1BufferPrecision.Unknown);
        Assert.AreEqual(D2D1PixelShader.GetOutputBufferPrecision<OnlyBufferPrecisionShader>(), D2D1BufferPrecision.UInt16Normalized);
        Assert.AreEqual(D2D1PixelShader.GetOutputBufferPrecision<OnlyChannelDepthShader>(), D2D1BufferPrecision.Unknown);
        Assert.AreEqual(D2D1PixelShader.GetOutputBufferPrecision<CustomBufferOutputShader>(), D2D1BufferPrecision.UInt8NormalizedSrgb);
    }

    [TestMethod]
    public unsafe void GetOutputBufferChannelDepth()
    {
        Assert.AreEqual(D2D1PixelShader.GetOutputBufferChannelDepth<ShaderWithMultipleInputs>(), D2D1ChannelDepth.Default);
        Assert.AreEqual(D2D1PixelShader.GetOutputBufferChannelDepth<OnlyBufferPrecisionShader>(), D2D1ChannelDepth.Default);
        Assert.AreEqual(D2D1PixelShader.GetOutputBufferChannelDepth<OnlyChannelDepthShader>(), D2D1ChannelDepth.Four);
        Assert.AreEqual(D2D1PixelShader.GetOutputBufferChannelDepth<CustomBufferOutputShader>(), D2D1ChannelDepth.One);
    }

    [D2DInputCount(0)]
    partial struct EmptyShader : ID2D1PixelShader
    {
        public Float4 Execute()
        {
            return 0;
        }
    }

    [D2DInputCount(0)]
    [D2DOutputBuffer(D2D1BufferPrecision.UInt16Normalized)]
    partial struct OnlyBufferPrecisionShader : ID2D1PixelShader
    {
        public Float4 Execute()
        {
            return 0;
        }
    }

    [D2DInputCount(0)]
    [D2DOutputBuffer(D2D1ChannelDepth.Four)]
    partial struct OnlyChannelDepthShader : ID2D1PixelShader
    {
        public Float4 Execute()
        {
            return 0;
        }
    }

    [D2DInputCount(0)]
    [D2DOutputBuffer(D2D1BufferPrecision.UInt8NormalizedSrgb, D2D1ChannelDepth.One)]
    partial struct CustomBufferOutputShader : ID2D1PixelShader
    {
        public Float4 Execute()
        {
            return 0;
        }
    }

    [TestMethod]
    public unsafe void GetInputDescriptions_Empty()
    {
        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<CheckerboardClipEffect>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<InvertEffect>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<InvertWithThresholdEffect>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<PixelateEffect.Shader>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<ZonePlateEffect>().Length, 0);

        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<ShaderWithMultipleInputs>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<EmptyShader>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<OnlyBufferPrecisionShader>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<OnlyChannelDepthShader>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetInputDescriptions<CustomBufferOutputShader>().Length, 0);
    }

    [TestMethod]
    public unsafe void GetInputDescriptions_Custom()
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
    partial struct ShaderWithInputDescriptions : ID2D1PixelShader
    {
        public Float4 Execute()
        {
            return 0;
        }
    }

    [TestMethod]
    public unsafe void GetBytecode_FromEmbeddedBytecode()
    {
        // Bytecode with no parameters
        ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>();

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? manager));
        Assert.AreEqual("PinnedBufferMemoryManager", manager!.GetType().Name);

        // Matching shader profile
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1ShaderProfile.PixelShader40Level91);

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.AreEqual("PinnedBufferMemoryManager", manager!.GetType().Name);

        // Matching compile options
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1CompileOptions.Default | D2D1CompileOptions.EnableLinking);

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.AreEqual("PinnedBufferMemoryManager", manager!.GetType().Name);

        // Matching shader profile and compile options
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1ShaderProfile.PixelShader40Level91, D2D1CompileOptions.Default | D2D1CompileOptions.EnableLinking);

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.AreEqual("PinnedBufferMemoryManager", manager!.GetType().Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public unsafe void GetBytecode_FromEmbeddedBytecode_WithPackMatrixColumnMajor()
    {
        _ = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1CompileOptions.Default | D2D1CompileOptions.PackMatrixColumnMajor);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public unsafe void GetBytecode_FromEmbeddedBytecode_WithTargetProfileAndPackMatrixColumnMajor()
    {
        _ = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecode>(D2D1ShaderProfile.PixelShader40Level91, D2D1CompileOptions.Default | D2D1CompileOptions.PackMatrixColumnMajor);
    }

    [D2DInputCount(3)]
    [D2DInputSimple(0)]
    [D2DInputSimple(1)]
    [D2DInputSimple(2)]
    [D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader40Level91)]
    private readonly partial struct ShaderWithEmbeddedBytecode : ID2D1PixelShader
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
    public unsafe void GetBytecode_FromEmbeddedBytecode_WithCompileOptions()
    {
        // Bytecode with no parameters
        ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecodeAndCompileOptions>();

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? manager));
        Assert.AreEqual("PinnedBufferMemoryManager", manager!.GetType().Name);

        // Matching shader profile
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecodeAndCompileOptions>(D2D1ShaderProfile.PixelShader40Level91);

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.AreEqual("PinnedBufferMemoryManager", manager!.GetType().Name);

        // Matching compile options
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecodeAndCompileOptions>(D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2);

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.AreEqual("PinnedBufferMemoryManager", manager!.GetType().Name);

        // Matching shader profile and compile options
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithEmbeddedBytecodeAndCompileOptions>(D2D1ShaderProfile.PixelShader40Level91, D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2);

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.AreEqual("PinnedBufferMemoryManager", manager!.GetType().Name);
    }

    [D2DInputCount(3)]
    [D2DInputSimple(0)]
    [D2DInputSimple(1)]
    [D2DInputSimple(2)]
    [D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader40Level91)]
    [D2DCompileOptions(D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2)]
    private readonly partial struct ShaderWithEmbeddedBytecodeAndCompileOptions : ID2D1PixelShader
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
}