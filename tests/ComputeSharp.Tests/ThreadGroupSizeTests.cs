using ComputeSharp.Descriptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class ThreadGroupSizeTests
{
    [TestMethod]
    public unsafe void Verify_ThreadGroupSize()
    {
        static (int X, int Y, int Z) GetNumThreads<T>()
            where T : struct, IComputeShaderDescriptor<T>
        {
            return (T.ThreadsX, T.ThreadsY, T.ThreadsZ);
        }

        Assert.AreEqual((64, 1, 1), GetNumThreads<DispatchXShader>());
        Assert.AreEqual((8, 8, 1), GetNumThreads<DispatchXYShader>());
        Assert.AreEqual((4, 4, 4), GetNumThreads<DispatchXYZShader>());
        Assert.AreEqual((11, 14, 6), GetNumThreads<DispatchCustomShader>());
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
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
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
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
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
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
    [ThreadGroupSize(11, 14, 6)]
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