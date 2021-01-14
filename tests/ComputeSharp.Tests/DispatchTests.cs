using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Dispatch")]
    public partial class DispatchTests
    {
        [TestMethod]
        public void Verify_ThreadIds()
        {
            using ReadWriteTexture3D<Int4> buffer = Gpu.Default.AllocateReadWriteTexture3D<Int4>(50, 50, 50);

            Gpu.Default.For(buffer.Width, buffer.Height, buffer.Depth, new ThreadIdsShader(buffer));

            Int4[,,] data = buffer.GetData();

            for (int z = 0; z < 50; z++)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        Int4 value = data[z, y, x];

                        Assert.AreEqual(x, value.X);
                        Assert.AreEqual(y, value.Y);
                        Assert.AreEqual(z, value.Z);
                    }
                }
            }
        }

        [AutoConstructor]
        internal readonly partial struct ThreadIdsShader : IComputeShader
        {
            public readonly ReadWriteTexture3D<Int4> buffer;

            public void Execute()
            {
                buffer[ThreadIds.XYZ].XYZ = ThreadIds.XYZ;
            }
        }

        [TestMethod]
        public void Verify_GroupIds()
        {
            using ReadWriteTexture3D<Int4> buffer = Gpu.Default.AllocateReadWriteTexture3D<Int4>(50, 50, 50);

            Gpu.Default.For(buffer.Width, buffer.Height, buffer.Depth, 4, 4, 4, new GroupIdsShader(buffer));

            Int4[,,] data = buffer.GetData();

            for (int z = 0; z < 50; z++)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        Int4 value = data[z, y, x];

                        Assert.AreEqual(x % 4, value.X);
                        Assert.AreEqual(y % 4, value.Y);
                        Assert.AreEqual(z % 4, value.Z);
                        Assert.AreEqual(value.Z * 4 * 4 + value.Y * 4 + value.X, value.W);
                    }
                }
            }
        }

        [AutoConstructor]
        internal readonly partial struct GroupIdsShader : IComputeShader
        {
            public readonly ReadWriteTexture3D<Int4> buffer;

            public void Execute()
            {
                buffer[ThreadIds.XYZ].XYZ = GroupIds.XYZ;
                buffer[ThreadIds.XYZ].W = GroupIds.Index;
            }
        }

        [TestMethod]
        public void Verify_GroupSize()
        {
            using ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer<int>(32);

            Gpu.Default.For(1, 1, 1, 4, 15, 7, new GroupSizeShader(buffer));
            
            int[] data = buffer.GetData();

            Assert.AreEqual(4, data[0]);
            Assert.AreEqual(15, data[1]);
            Assert.AreEqual(7, data[2]);
            Assert.AreEqual(4 * 15 * 7, data[3]);
        }

        [AutoConstructor]
        internal readonly partial struct GroupSizeShader : IComputeShader
        {
            public readonly ReadWriteBuffer<int> buffer;

            public void Execute()
            {
                buffer[0] = GroupSize.X;
                buffer[1] = GroupSize.Y;
                buffer[2] = GroupSize.Z;
                buffer[3] = GroupSize.Count;
            }
        }
    }
}
