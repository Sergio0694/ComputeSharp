using System;
using System.Linq;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Extensions;
using Microsoft.Toolkit.HighPerformance.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("TransferTexture3D")]
    public partial class TransferTexture3DTests
    {
        [TestMethod]
        [DataRow(typeof(UploadTexture3D<>))]
        [DataRow(typeof(ReadBackTexture3D<>))]
        public unsafe void Allocate_Uninitialized_Ok(Type bufferType)
        {
            using TransferTexture3D<float> buffer = Gpu.Default.AllocateTransferTexture3D<float>(bufferType, 128, 256, 32);

            Assert.IsNotNull(buffer);
            Assert.AreEqual(buffer.Width, 128);
            Assert.AreEqual(buffer.Height, 256);
            Assert.AreEqual(buffer.Depth, 32);
            Assert.AreEqual(buffer.View.Width, 128);
            Assert.AreEqual(buffer.View.Height, 256);
            Assert.AreEqual(buffer.View.Depth, 32);
            Assert.AreSame(buffer.GraphicsDevice, Gpu.Default);
        }

        [TestMethod]
        [DataRow(typeof(UploadTexture3D<>), 128, -14253, 4)]
        [DataRow(typeof(UploadTexture3D<>), 128, -1, 4)]
        [DataRow(typeof(UploadTexture3D<>), 0, -4314, 4)]
        [DataRow(typeof(UploadTexture3D<>), -14, -53, 4)]
        [DataRow(typeof(UploadTexture3D<>), 12, 12, -1)]
        [DataRow(typeof(UploadTexture3D<>), 2, 4, ushort.MaxValue + 1)]
        [DataRow(typeof(ReadBackTexture3D<>), 128, -14253, 4)]
        [DataRow(typeof(ReadBackTexture3D<>), 128, -1, 4)]
        [DataRow(typeof(ReadBackTexture3D<>), 0, -4314, 4)]
        [DataRow(typeof(ReadBackTexture3D<>), -14, -53, 4)]
        [DataRow(typeof(ReadBackTexture3D<>), 12, 12, -1)]
        [DataRow(typeof(ReadBackTexture3D<>), 2, 4, ushort.MaxValue + 1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Type bufferType, int width, int height, int depth)
        {
            using TransferTexture3D<float> buffer = Gpu.Default.AllocateTransferTexture3D<float>(bufferType, width, height, depth);
        }

        [TestMethod]
        [DataRow(typeof(UploadTexture3D<>))]
        //[DataRow(typeof(ReadBackTexture3D<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Type bufferType)
        {
            using TransferTexture3D<float> buffer = Gpu.Default.AllocateTransferTexture3D<float>(bufferType, 32, 32, 16);

            buffer.Dispose();

            _ = buffer.View;
        }

        [TestMethod]
        public void Allocate_UploadTexture3D_Copy_Full()
        {
            using UploadTexture3D<int> uploadTexture3D = Gpu.Default.AllocateUploadTexture3D<int>(256, 256, 16);

            for (int i = 0; i < uploadTexture3D.Depth; i++)
            {
                for (int j = 0; j < uploadTexture3D.Height; j++)
                {
                    new Random(i * 5381 + j).NextBytes(uploadTexture3D.View.GetRowSpan(j, i).AsBytes());
                }
            }

            using ReadOnlyTexture3D<int> readOnlyTexture3D = Gpu.Default.AllocateReadOnlyTexture3D<int>(uploadTexture3D.Width, uploadTexture3D.Height, uploadTexture3D.Depth);

            uploadTexture3D.CopyTo(readOnlyTexture3D);

            int[,,] result = readOnlyTexture3D.ToArray();

            Assert.AreEqual(uploadTexture3D.Width, result.GetLength(2));
            Assert.AreEqual(uploadTexture3D.Height, result.GetLength(1));
            Assert.AreEqual(uploadTexture3D.Depth, result.GetLength(0));

            for (int i = 0; i < uploadTexture3D.Depth; i++)
            {
                for (int j = 0; j < uploadTexture3D.Height; j++)
                {
                    Span<int> sourceRow = uploadTexture3D.View.GetRowSpan(j, i);
                    Span<int> destinationRow = result.AsSpan2D(i).GetRowSpan(j);

                    Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
                }
            }
        }

        [TestMethod]
        [DataRow(0, 0, 0, 256, 256, 16)]
        [DataRow(128, 0, 0, 72, 256, 16)]
        [DataRow(0, 128, 0, 189, 67, 8)]
        [DataRow(0, 0, 15, 32, 64, 1)]
        [DataRow(97, 33, 4, 128, 99, 7)]
        public void Allocate_UploadTexture3D_Copy_Range(int x, int y, int z, int width, int height, int depth)
        {
            using UploadTexture3D<int> uploadTexture3D = Gpu.Default.AllocateUploadTexture3D<int>(256, 256, 16);

            for (int i = 0; i < uploadTexture3D.Depth; i++)
            {
                for (int j = 0; j < uploadTexture3D.Height; j++)
                {
                    new Random(i * 5381 + j).NextBytes(uploadTexture3D.View.GetRowSpan(j, i).AsBytes());
                }
            }

            using ReadOnlyTexture3D<int> readOnlyTexture3D = Gpu.Default.AllocateReadOnlyTexture3D<int>(uploadTexture3D.Width, uploadTexture3D.Height, uploadTexture3D.Depth);

            uploadTexture3D.CopyTo(readOnlyTexture3D, x, y, z, width, height, depth);

            int[,,] result = readOnlyTexture3D.ToArray();

            for (int i = 0; i < depth; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Span<int> sourceRow = uploadTexture3D.View.GetRowSpan(j + y, i + z).Slice(x, width);
                    Span<int> destinationRow = result.AsSpan2D(i).GetRowSpan(j).Slice(0, width);

                    Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
                }
            }
        }

        [TestMethod]
        public void Allocate_ReadBackTexture3D_Copy_Full()
        {
            int[,,] source = new int[16, 256, 256];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    new Random(i * 5381 + j).NextBytes(source.AsSpan2D(i).GetRowSpan(j).AsBytes());
                }
            }

            using ReadOnlyTexture3D<int> readOnlyTexture3D = Gpu.Default.AllocateReadOnlyTexture3D(source);
            using ReadBackTexture3D<int> readBackTexture3D = Gpu.Default.AllocateReadBackTexture3D<int>(256, 256, 16);

            readOnlyTexture3D.CopyTo(readBackTexture3D);

            Assert.AreEqual(readBackTexture3D.Width, 256);
            Assert.AreEqual(readBackTexture3D.Height, 256);
            Assert.AreEqual(readBackTexture3D.Depth, 16);

            for (int i = 0; i < readBackTexture3D.Depth; i++)
            {
                for (int j = 0; j < readBackTexture3D.Height; j++)
                {
                    Span<int> sourceRow = source.AsSpan2D(i).GetRowSpan(j);
                    Span<int> destinationRow = readBackTexture3D.View.GetRowSpan(j, i);

                    Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
                }
            }
        }

        [TestMethod]
        [DataRow(0, 0, 0, 256, 256, 16)]
        [DataRow(128, 0, 0, 72, 256, 16)]
        [DataRow(0, 128, 0, 189, 67, 8)]
        [DataRow(0, 0, 15, 32, 64, 1)]
        [DataRow(97, 33, 4, 128, 99, 7)]
        public void Allocate_ReadBackTexture3D_Copy_Range(int x, int y, int z, int width, int height, int depth)
        {
            int[,,] source = new int[16, 256, 256];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    new Random(i * 5381 + j).NextBytes(source.AsSpan2D(i).GetRowSpan(j).AsBytes());
                }
            }

            using ReadOnlyTexture3D<int> readOnlyTexture3D = Gpu.Default.AllocateReadOnlyTexture3D(source);
            using ReadBackTexture3D<int> readBackTexture3D = Gpu.Default.AllocateReadBackTexture3D<int>(256, 256, 16);

            readOnlyTexture3D.CopyTo(readBackTexture3D, x, y, z, width, height, depth);

            for (int i = 0; i < depth; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Span<int> sourceRow = source.AsSpan2D(i + z).GetRowSpan(j + y).Slice(x, width);
                    Span<int> destinationRow = readBackTexture3D.View.GetRowSpan(j, i).Slice(0, width);

                    Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
                }
            }
        }
    }
}
