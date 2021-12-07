using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS0618

namespace ComputeSharp.Tests.Internals;

[TestClass]
[TestCategory("ShaderBytecode")]
public partial class ShaderBytecodeTests
{
    [TestMethod]
    public void NonPrecompiledShader_Test()
    {
        Assert.IsFalse(((IComputeShader)default(NonPrecompiledShader)).TryGetBytecode(32, 1, 1, out ReadOnlySpan<byte> bytecode));
        Assert.IsFalse(bytecode.Length > 0);
    }

    [AutoConstructor]
    internal readonly partial struct NonPrecompiledShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer;
        public readonly float factor;

        /// <inheritdoc/>
        public void Execute()
        {
            buffer[ThreadIds.X] *= factor;
        }
    }

    [TestMethod]
    public void ComputeShader_Test()
    {
        Assert.IsTrue(((IComputeShader)default(ComputeShader)).TryGetBytecode(32, 1, 1, out ReadOnlySpan<byte> bytecode));
        Assert.IsTrue(bytecode.Length > 0);

        Assert.IsFalse(((IComputeShader)default(ComputeShader)).TryGetBytecode(64, 1, 1, out bytecode));
        Assert.IsFalse(bytecode.Length > 0);
    }

    [AutoConstructor]
    [EmbeddedBytecode(32, 1, 1)]
    internal readonly partial struct ComputeShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer;
        public readonly float factor;

        /// <inheritdoc/>
        public void Execute()
        {
            buffer[ThreadIds.X] *= factor;
        }
    }

    [TestMethod]
    public void PixelShader_Test()
    {
        Assert.IsTrue(((IPixelShader<float4>)default(PixelShader)).TryGetBytecode(8, 8, 1, out ReadOnlySpan<byte> bytecode));
        Assert.IsTrue(bytecode.Length > 0);

        Assert.IsFalse(((IPixelShader<float4>)default(PixelShader)).TryGetBytecode(32, 1, 1, out bytecode));
        Assert.IsFalse(bytecode.Length > 0);
    }

    [AutoConstructor]
    [EmbeddedBytecode(8, 8, 1)]
    internal readonly partial struct PixelShader : IPixelShader<float4>
    {
        public readonly ReadWriteTexture2D<float> buffer;
        public readonly float factor;

        /// <inheritdoc/>
        public float4 Execute()
        {
            return buffer[ThreadIds.XY] * factor;
        }
    }
}
