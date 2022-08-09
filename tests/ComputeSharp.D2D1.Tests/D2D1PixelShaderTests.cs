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
    public unsafe void GetResourceTextureDescriptions_Empty()
    {
        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<CheckerboardClipEffect>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<InvertEffect>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<InvertWithThresholdEffect>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<PixelateEffect.Shader>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<ZonePlateEffect>().Length, 0);

        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<ShaderWithMultipleInputs>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<EmptyShader>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<OnlyBufferPrecisionShader>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<OnlyChannelDepthShader>().Length, 0);
        Assert.AreEqual(D2D1PixelShader.GetResourceTextureDescriptions<CustomBufferOutputShader>().Length, 0);
    }

    [TestMethod]
    public unsafe void GetResourceTextureDescriptions_Custom()
    {
        ReadOnlyMemory<D2D1ResourceTextureDescription> resourceTextureDescriptions = D2D1PixelShader.GetResourceTextureDescriptions<ShaderWithResourceTextures>();

        Assert.AreEqual(resourceTextureDescriptions.Length, 3);

        ReadOnlySpan<D2D1ResourceTextureDescription> span = resourceTextureDescriptions.Span;

        Assert.AreEqual(span[0].Index, 5);
        Assert.AreEqual(span[0].Rank, 1);

        Assert.AreEqual(span[1].Index, 6);
        Assert.AreEqual(span[1].Rank, 2);

        Assert.AreEqual(span[2].Index, 7);
        Assert.AreEqual(span[2].Rank, 3);
    }

    [D2DInputCount(4)]
    [D2DInputSimple(0)]
    [D2DInputSimple(2)]
    [D2DInputComplex(1)]
    [D2DInputComplex(3)]
    [AutoConstructor]
    partial struct ShaderWithResourceTextures : ID2D1PixelShader
    {
        float number;

        [D2DResourceTextureIndex(5)]
        D2D1ResourceTexture1D myTexture1;

        [D2DResourceTextureIndex(6)]
        D2D1ResourceTexture2D myTexture2;

        [D2DResourceTextureIndex(7)]
        D2D1ResourceTexture3D myTexture3;

        public Float4 Execute()
        {
            float4 pixel1 = myTexture1[0];
            float4 pixel2 = myTexture1[0.4f];
            float4 pixel3 = myTexture2[0, 1];
            float4 pixel4 = myTexture2[0.4f, 0.3f];
            float4 pixel5 = myTexture3[0, 1, 2];
            float4 pixel6 = myTexture3[0.4f, 0.5f, 0.6f];

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

    [TestMethod]
    public unsafe void GetConstantBufferSize_Empty()
    {
        int size = D2D1PixelShader.GetConstantBufferSize<ShaderWithNoCapturedValues>();

        Assert.AreEqual(size, 0);

        // Repeated calls always return the same result
        size = D2D1PixelShader.GetConstantBufferSize<ShaderWithNoCapturedValues>();

        Assert.AreEqual(size, 0);
    }

    [TestMethod]
    public unsafe void GetConstantBuffer_Empty_ReadOnlyMemory()
    {
        ShaderWithNoCapturedValues shader = default;

        ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in shader);

        Assert.AreEqual(constantBuffer.Length, 0);
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/323
    [TestMethod]
    public unsafe void GetConstantBuffer_Empty_Span()
    {
        ShaderWithNoCapturedValues shader = default;

        byte[] buffer = Array.Empty<byte>();

        int bytesWritten = D2D1PixelShader.GetConstantBuffer(in shader, buffer);

        Assert.AreEqual(bytesWritten, 0);
    }

    [D2DInputCount(1)]
    [D2DInputSimple(0)]
    [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
    [D2DPixelOptions(D2D1PixelOptions.TrivialSampling)]
    private readonly partial struct ShaderWithNoCapturedValues : ID2D1PixelShader
    {
        public float4 Execute()
        {
            float4 color = D2D.GetInput(0);

            return color * color;
        }
    }

    [TestMethod]
    public unsafe void GetConstantBufferSize()
    {
        int size = D2D1PixelShader.GetConstantBufferSize<ShaderWithScalarVectorAndMatrixTypes>();

        Assert.AreEqual(size, 124);

        // Same as above: repeated calls always return the same result
        size = D2D1PixelShader.GetConstantBufferSize<ShaderWithScalarVectorAndMatrixTypes>();

        Assert.AreEqual(size, 124);
    }

    [TestMethod]
    public unsafe void GetConstantBuffer_ReadOnlyMemory()
    {
        GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

        ReadOnlyMemory<byte> constantBuffer = D2D1PixelShader.GetConstantBuffer(in shader);

        ValidateShaderWithScalarVectorAndMatrixTypesConstantBuffer(constantBuffer.Span);
    }

    [TestMethod]
    public unsafe void GetConstantBuffer_Span()
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
    public unsafe void GetConstantBuffer_Span_DestinationTooShort(int size)
    {
        GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

        byte[] buffer = new byte[size];

        _ = D2D1PixelShader.GetConstantBuffer(in shader, buffer);

        Assert.Fail();
    }

    [TestMethod]
    public unsafe void GetConstantBuffer_TryGetSpan()
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
    public unsafe void GetConstantBuffer_TryGetSpan_DestinationTooShort(int size)
    {
        GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader);

        byte[] buffer = new byte[size];

        bool success = D2D1PixelShader.TryGetConstantBuffer(in shader, buffer, out int bytesWritten);

        Assert.IsFalse(success);
        Assert.AreEqual(bytesWritten, 0);
    }

    // Gets a ShaderWithScalarVectorAndMatrixTypes instance with test values
    private void GetShaderWithScalarVectorAndMatrixTypes(out ShaderWithScalarVectorAndMatrixTypes shader)
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
        Assert.AreEqual(31 * sizeof(uint), span.Length);

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

    [D2DInputCount(0)]
    [AutoConstructor]
    private readonly partial struct ShaderWithScalarVectorAndMatrixTypes : ID2D1PixelShader
    {
        public readonly int x;
        public readonly int y;
        public readonly int z;
        public readonly float2x3 f2x3;
        public readonly int a;
        public readonly Int1x3 i1x3;
        public readonly double2 d2;
        public readonly int c;
        public readonly Int1x2 i1x2;
        public readonly int2x2 i2x2;
        public readonly int d;

        public float4 Execute()
        {
            return 0;
        }
    }
}