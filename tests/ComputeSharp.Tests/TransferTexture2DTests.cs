using System;
using System.Linq;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Extensions;
using Microsoft.Toolkit.HighPerformance.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("TransferTexture2D")]
    public partial class TransferTexture2DTests
    {
        [TestMethod]
        [DataRow(typeof(UploadTexture2D<>))]
        [DataRow(typeof(ReadBackTexture2D<>))]
        public unsafe void Allocate_Uninitialized_Ok(Type bufferType)
        {
            using TransferTexture2D<float> buffer = Gpu.Default.AllocateTransferTexture2D<float>(bufferType, 128, 256);

            Assert.IsNotNull(buffer);
            Assert.AreEqual(buffer.Width, 128);
            Assert.AreEqual(buffer.Height, 256);
            Assert.AreEqual(buffer.View.Width, 128);
            Assert.AreEqual(buffer.View.Height, 256);
            Assert.AreSame(buffer.GraphicsDevice, Gpu.Default);
        }

        [TestMethod]
        [DataRow(typeof(UploadTexture2D<>), 128, -14253)]
        [DataRow(typeof(UploadTexture2D<>), 128, -1)]
        [DataRow(typeof(UploadTexture2D<>), 0, -4314)]
        [DataRow(typeof(UploadTexture2D<>), -14, -53)]
        [DataRow(typeof(ReadBackTexture2D<>), 128, -14253)]
        [DataRow(typeof(ReadBackTexture2D<>), 128, -1)]
        [DataRow(typeof(ReadBackTexture2D<>), 0, -4314)]
        [DataRow(typeof(ReadBackTexture2D<>), -14, -53)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Type bufferType, int width, int height)
        {
            using TransferTexture2D<float> buffer = Gpu.Default.AllocateTransferTexture2D<float>(bufferType, width, height);
        }

        [TestMethod]
        [DataRow(typeof(UploadTexture2D<>))]
        [DataRow(typeof(ReadBackTexture2D<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Type bufferType)
        {
            using TransferTexture2D<float> buffer = Gpu.Default.AllocateTransferTexture2D<float>(bufferType, 32, 32);

            buffer.Dispose();

            _ = buffer.View;
        }

        [TestMethod]
        public void Allocate_UploadTexture2D_Copy_Full()
        {
            using UploadTexture2D<int> uploadTexture2D = Gpu.Default.AllocateUploadTexture2D<int>(256, 256);

            for (int i = 0; i < uploadTexture2D.Height; i++)
            {
                new Random(i).NextBytes(uploadTexture2D.View.GetRowSpan(i).AsBytes());
            }

            using ReadOnlyTexture2D<int> readOnlyTexture2D = Gpu.Default.AllocateReadOnlyTexture2D<int>(uploadTexture2D.Width, uploadTexture2D.Height);

            uploadTexture2D.CopyTo(readOnlyTexture2D);

            int[,] result = readOnlyTexture2D.ToArray();

            Assert.AreEqual(uploadTexture2D.Width, result.GetLength(1));
            Assert.AreEqual(uploadTexture2D.Height, result.GetLength(0));

            for (int i = 0; i < uploadTexture2D.Height; i++)
            {
                Assert.IsTrue(uploadTexture2D.View.GetRowSpan(i).SequenceEqual(result.GetRowSpan(i)));
            }
        }

        [TestMethod]
        [DataRow(0, 0, 2048, 2048)]
        [DataRow(128, 0, 1813, 2048)]
        [DataRow(0, 128, 2000, 944)]
        [DataRow(97, 33, 512, 794)]
        public void Allocate_UploadTexture2D_Copy_Range(int x, int y, int width, int height)
        {
            using UploadTexture2D<int> uploadTexture2D = Gpu.Default.AllocateUploadTexture2D<int>(2048, 2048);

            for (int i = 0; i < uploadTexture2D.Height; i++)
            {
                new Random(i).NextBytes(uploadTexture2D.View.GetRowSpan(i).AsBytes());
            }

            using ReadOnlyTexture2D<int> readOnlyTexture2D = Gpu.Default.AllocateReadOnlyTexture2D<int>(uploadTexture2D.Width, uploadTexture2D.Height);

            uploadTexture2D.CopyTo(readOnlyTexture2D, x, y, width, height);

            int[,] result = readOnlyTexture2D.ToArray();

            for (int i = 0; i < height; i++)
            {
                Span<int> sourceRow = uploadTexture2D.View.GetRowSpan(i + y).Slice(x, width);
                Span<int> destinationRow = result.GetRowSpan(i).Slice(0, width);

                Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
            }
        }

        [TestMethod]
        public void Allocate_ReadBackTexture2D_Copy_Full()
        {
            int[,] source = new int[256, 256];

            for (int i = 0; i < 256; i++)
            {
                new Random(42).NextBytes(source.GetRowSpan(i).AsBytes());
            }

            using ReadOnlyTexture2D<int> readOnlyTexture2D = Gpu.Default.AllocateReadOnlyTexture2D(source);
            using ReadBackTexture2D<int> readBackTexture2D = Gpu.Default.AllocateReadBackTexture2D<int>(256, 256);

            readOnlyTexture2D.CopyTo(readBackTexture2D);

            Assert.AreEqual(readBackTexture2D.Width, 256);
            Assert.AreEqual(readBackTexture2D.Height, 256);

            for (int i = 0; i < readBackTexture2D.Height; i++)
            {
                Assert.IsTrue(readBackTexture2D.View.GetRowSpan(i).SequenceEqual(source.GetRowSpan(i)));
            }
        }

        [TestMethod]
        [DataRow(0, 0, 2048, 2048)]
        [DataRow(128, 0, 1813, 2048)]
        [DataRow(0, 128, 2000, 944)]
        [DataRow(97, 33, 512, 794)]
        public void Allocate_ReadBackTexture2D_Copy_Range(int x, int y, int width, int height)
        {
            int[,] source = new int[2048, 2048];

            for (int i = 0; i < 2048; i++)
            {
                new Random(42).NextBytes(source.GetRowSpan(i).AsBytes());
            }

            using ReadOnlyTexture2D<int> readOnlyTexture2D = Gpu.Default.AllocateReadOnlyTexture2D(source);
            using ReadBackTexture2D<int> readBackTexture2D = Gpu.Default.AllocateReadBackTexture2D<int>(2048, 2048);

            readOnlyTexture2D.CopyTo(readBackTexture2D, x, y, width, height);

            for (int i = 0; i < height; i++)
            {
                Span<int> sourceRow = source.GetRowSpan(i + y).Slice(x, width);
                Span<int> destinationRow = readBackTexture2D.View.GetRowSpan(i).Slice(0, width);

                Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
            }
        }
    }
}
