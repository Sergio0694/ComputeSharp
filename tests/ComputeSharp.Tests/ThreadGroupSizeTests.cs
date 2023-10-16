using ComputeSharp.Descriptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("ThreadGroupSize")]
public partial class ThreadGroupSizeTests
{
    [TestMethod]
    public unsafe void Verify_ThreadGroupSize()
    {
        Assert.AreEqual(64, ((IComputeShaderDescriptor<DispatchXShader>)default(DispatchXShader)).ThreadsX);
        Assert.AreEqual(1, ((IComputeShaderDescriptor<DispatchXShader>)default(DispatchXShader)).ThreadsY);
        Assert.AreEqual(1, ((IComputeShaderDescriptor<DispatchXShader>)default(DispatchXShader)).ThreadsZ);

        Assert.AreEqual(8, ((IComputeShaderDescriptor<DispatchXYShader>)default(DispatchXYShader)).ThreadsX);
        Assert.AreEqual(8, ((IComputeShaderDescriptor<DispatchXYShader>)default(DispatchXYShader)).ThreadsY);
        Assert.AreEqual(1, ((IComputeShaderDescriptor<DispatchXYShader>)default(DispatchXYShader)).ThreadsZ);

        Assert.AreEqual(4, ((IComputeShaderDescriptor<DispatchXYZShader>)default(DispatchXYZShader)).ThreadsX);
        Assert.AreEqual(4, ((IComputeShaderDescriptor<DispatchXYZShader>)default(DispatchXYZShader)).ThreadsY);
        Assert.AreEqual(4, ((IComputeShaderDescriptor<DispatchXYZShader>)default(DispatchXYZShader)).ThreadsZ);

        Assert.AreEqual(11, ((IComputeShaderDescriptor<DispatchCustomShader>)default(DispatchCustomShader)).ThreadsX);
        Assert.AreEqual(14, ((IComputeShaderDescriptor<DispatchCustomShader>)default(DispatchCustomShader)).ThreadsY);
        Assert.AreEqual(6, ((IComputeShaderDescriptor<DispatchCustomShader>)default(DispatchCustomShader)).ThreadsZ);
    }

    [AutoConstructor]
    [EmbeddedBytecode(DispatchAxis.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct DispatchXShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.X] = 0;
        }
    }

    [AutoConstructor]
    [EmbeddedBytecode(DispatchAxis.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct DispatchXYShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.X] = 0;
        }
    }

    [AutoConstructor]
    [EmbeddedBytecode(DispatchAxis.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct DispatchXYZShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.X] = 0;
        }
    }

    [AutoConstructor]
    [EmbeddedBytecode(11, 14, 6)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct DispatchCustomShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.X] = 0;
        }
    }
}