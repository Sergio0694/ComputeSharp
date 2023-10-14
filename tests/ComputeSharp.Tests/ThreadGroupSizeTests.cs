using ComputeSharp.__Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("ThreadGroupSize")]
public partial class ThreadGroupSizeTests
{
    [TestMethod]
    public unsafe void Verify_ThreadGroupSize()
    {
        Assert.AreEqual(64, ((IShader)default(DispatchXShader)).ThreadsX);
        Assert.AreEqual(1, ((IShader)default(DispatchXShader)).ThreadsY);
        Assert.AreEqual(1, ((IShader)default(DispatchXShader)).ThreadsZ);

        Assert.AreEqual(8, ((IShader)default(DispatchXYShader)).ThreadsX);
        Assert.AreEqual(8, ((IShader)default(DispatchXYShader)).ThreadsY);
        Assert.AreEqual(1, ((IShader)default(DispatchXYShader)).ThreadsZ);

        Assert.AreEqual(4, ((IShader)default(DispatchXYZShader)).ThreadsX);
        Assert.AreEqual(4, ((IShader)default(DispatchXYZShader)).ThreadsY);
        Assert.AreEqual(4, ((IShader)default(DispatchXYZShader)).ThreadsZ);

        Assert.AreEqual(11, ((IShader)default(DispatchCustomShader)).ThreadsX);
        Assert.AreEqual(14, ((IShader)default(DispatchCustomShader)).ThreadsY);
        Assert.AreEqual(6, ((IShader)default(DispatchCustomShader)).ThreadsZ);
    }

    [AutoConstructor]
    [EmbeddedBytecode(DispatchAxis.X)]
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
    internal readonly partial struct DispatchCustomShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.X] = 0;
        }
    }
}