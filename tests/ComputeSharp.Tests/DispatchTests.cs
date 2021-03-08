using System;
using ComputeSharp.Interop;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Dispatch")]
    public partial class DispatchTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        public unsafe void Verify_ThreadIds(Device device)
        {
            using ReadWriteTexture3D<Int4> buffer = device.Get().AllocateReadWriteTexture3D<Int4>(50, 50, 50);

            device.Get().For(buffer.Width, buffer.Height, buffer.Depth, new ThreadIdsShader(buffer));

            Int4[,,] data = buffer.ToArray();
            int* value = stackalloc int[4];

            for (int z = 0; z < 50; z++)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        *(Int4*)value = data[z, y, x];

                        Assert.AreEqual(x, value[0]);
                        Assert.AreEqual(y, value[1]);
                        Assert.AreEqual(z, value[2]);
                    }
                }
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(0, 8, 8)]
        [Data(8, 0, 8)]
        [Data(8, 8, 0)]
        [Data(8, -3, 16)]
        [Data(-1, -1, -1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Verify_ThreadIds_OutOfRange(Device device, int x, int y, int z)
        {
            using ReadWriteTexture3D<Int4> buffer = device.Get().AllocateReadWriteTexture3D<Int4>(50, 50, 50);

            device.Get().For(x, y, z, new ThreadIdsShader(buffer));

            Assert.Fail();
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

        [CombinatorialTestMethod]
        [AllDevices]
        public unsafe void Verify_ThreadIdsNormalized(Device device)
        {
            using ReadWriteTexture3D<Float4> buffer = device.Get().AllocateReadWriteTexture3D<Float4>(10, 20, 30);

            device.Get().For(buffer.Width, buffer.Height, buffer.Depth, new ThreadIdsNormalizedShader(buffer));

            Float4[,,] data = buffer.ToArray();
            float* value = stackalloc float[4];

            for (int z = 0; z < 30; z++)
            {
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        *(Float4*)value = data[z, y, x];

                        Assert.AreEqual(x / (float)buffer.Width, value[0], 0.000001f);
                        Assert.AreEqual(y / (float)buffer.Height, value[1], 0.000001f);
                        Assert.AreEqual(z / (float)buffer.Depth, value[2], 0.000001f);
                        Assert.AreEqual(x / (float)buffer.Width, value[3], 0.000001f);
                    }
                }
            }
        }

        [AutoConstructor]
        internal readonly partial struct ThreadIdsNormalizedShader : IComputeShader
        {
            public readonly ReadWriteTexture3D<Float4> buffer;

            public void Execute()
            {
                buffer[ThreadIds.XYZ].XYZ = ThreadIds.Normalized.XYZ;
                buffer[ThreadIds.XYZ].XY = ThreadIds.Normalized.XY;
                buffer[ThreadIds.XYZ].W = ThreadIds.Normalized.X;
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public unsafe void Verify_GroupIds(Device device)
        {
            using ReadWriteTexture3D<Int4> buffer = device.Get().AllocateReadWriteTexture3D<Int4>(50, 50, 50);

            device.Get().For(buffer.Width, buffer.Height, buffer.Depth, 4, 4, 4, new GroupIdsShader(buffer));

            Int4[,,] data = buffer.ToArray();
            int* value = stackalloc int[4];

            for (int z = 0; z < 50; z++)
            {
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        *(Int4*)value = data[z, y, x];

                        Assert.AreEqual(x % 4, value[0]);
                        Assert.AreEqual(y % 4, value[1]);
                        Assert.AreEqual(z % 4, value[2]);
                        Assert.AreEqual(value[2] * 4 * 4 + value[1] * 4 + value[0], value[3]);
                    }
                }
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(0, 8, 8)]
        [Data(8, 0, 8)]
        [Data(8, 8, 0)]
        [Data(1025, 8, 8)]
        [Data(8, 2000, 8)]
        [Data(8, 8, 70)]
        [Data(8, -1, 8)]
        [Data(8, 15, 16)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Verify_GroupIds_OutOfRange(Device device, int x, int y, int z)
        {
            using ReadWriteTexture3D<Int4> buffer = device.Get().AllocateReadWriteTexture3D<Int4>(50, 50, 50);

            device.Get().For(buffer.Width, buffer.Height, buffer.Depth, x, y, z, new GroupIdsShader(buffer));

            Assert.Fail();
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

        [CombinatorialTestMethod]
        [AllDevices]
        public void Verify_GroupSize(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(32);

            device.Get().For(1, 1, 1, 4, 15, 7, new GroupSizeShader(buffer));
            
            int[] data = buffer.ToArray();

            Assert.AreEqual(4, data[0]);
            Assert.AreEqual(15, data[1]);
            Assert.AreEqual(7, data[2]);
            Assert.AreEqual(4 * 15 * 7, data[3]);
            Assert.AreEqual(4 + 15, data[4]);
            Assert.AreEqual(4 + 15 + 7, data[5]);
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
                buffer[4] = (int)Hlsl.Dot(GroupSize.XY, Float2.One);
                buffer[5] = (int)Hlsl.Dot(GroupSize.XYZ, Float3.One);
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Verify_GridIds(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(256);

            device.Get().For(256, 1, 1, 32, 1, 1, new GridIdsShader(buffer));

            int[] data = buffer.ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual(data[i], i / 32);
            }
        }

        [AutoConstructor]
        internal readonly partial struct GridIdsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<int> buffer;

            public void Execute()
            {
                buffer[ThreadIds.X] = GridIds.X;
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Verify_DispatchSize(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(6);

            device.Get().For(11, 22, 3, new DispatchSizeShader(buffer));

            ReflectionServices.GetShaderInfo<DispatchSizeShader>(out var info);

            int[] data = buffer.ToArray();

            Assert.AreEqual(data[0], 11 * 22 * 3);
            Assert.AreEqual(data[1], 11);
            Assert.AreEqual(data[2], 22);
            Assert.AreEqual(data[3], 3);
            Assert.AreEqual(data[4], 11 + 22);
            Assert.AreEqual(data[5], 11 + 22 + 3);
        }

        [AutoConstructor]
        internal readonly partial struct DispatchSizeShader : IComputeShader
        {
            public readonly ReadWriteBuffer<int> buffer;

            public void Execute()
            {
                buffer[0] = DispatchSize.Count;
                buffer[1] = DispatchSize.X;
                buffer[2] = DispatchSize.Y;
                buffer[3] = DispatchSize.Z;
                buffer[4] = (int)Hlsl.Dot(DispatchSize.XY, Float2.One);
                buffer[5] = (int)Hlsl.Dot(DispatchSize.XYZ, Float3.One);
            }
        }
    }
}
