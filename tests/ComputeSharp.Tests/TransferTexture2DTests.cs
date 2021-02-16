using System;
using System.Linq;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.Toolkit.HighPerformance.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("TransferTexture2D")]
    public partial class TransferTexture2DTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(UploadTexture2D<>))]
        [Resource(typeof(ReadBackTexture2D<>))]
        public unsafe void Allocate_Uninitialized_Ok(Device device, Type bufferType)
        {
            using TransferTexture2D<float> buffer = device.Get().AllocateTransferTexture2D<float>(bufferType, 128, 256);

            Assert.IsNotNull(buffer);
            Assert.AreEqual(buffer.Width, 128);
            Assert.AreEqual(buffer.Height, 256);
            Assert.AreEqual(buffer.View.Width, 128);
            Assert.AreEqual(buffer.View.Height, 256);
            Assert.AreSame(buffer.GraphicsDevice, device.Get());
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(UploadTexture2D<>))]
        [Resource(typeof(ReadBackTexture2D<>))]
        [Data(128, -14253)]
        [Data(128, -1)]
        [Data(0, -4314)]
        [Data(-14, -53)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Device device, Type bufferType, int width, int height)
        {
            using TransferTexture2D<float> buffer = device.Get().AllocateTransferTexture2D<float>(bufferType, width, height);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(UploadTexture2D<>))]
        [Resource(typeof(ReadBackTexture2D<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Device device, Type bufferType)
        {
            using TransferTexture2D<float> buffer = device.Get().AllocateTransferTexture2D<float>(bufferType, 32, 32);

            buffer.Dispose();

            _ = buffer.View;
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Allocate_UploadTexture2D_Copy_Full(Device device)
        {
            using UploadTexture2D<int> uploadTexture2D = device.Get().AllocateUploadTexture2D<int>(256, 256);

            for (int i = 0; i < uploadTexture2D.Height; i++)
            {
                new Random(i).NextBytes(uploadTexture2D.View.GetRowSpan(i).AsBytes());
            }

            using ReadOnlyTexture2D<int> readOnlyTexture2D = device.Get().AllocateReadOnlyTexture2D<int>(uploadTexture2D.Width, uploadTexture2D.Height);

            uploadTexture2D.CopyTo(readOnlyTexture2D);

            int[,] result = readOnlyTexture2D.ToArray();

            Assert.AreEqual(uploadTexture2D.Width, result.GetLength(1));
            Assert.AreEqual(uploadTexture2D.Height, result.GetLength(0));

            for (int i = 0; i < uploadTexture2D.Height; i++)
            {
                Assert.IsTrue(uploadTexture2D.View.GetRowSpan(i).SequenceEqual(result.GetRowSpan(i)));
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(0, 0, 2048, 2048)]
        [Data(128, 0, 1813, 2048)]
        [Data(0, 128, 2000, 944)]
        [Data(97, 33, 512, 794)]
        public void Allocate_UploadTexture2D_Copy_Range(Device device, int x, int y, int width, int height)
        {
            using UploadTexture2D<int> uploadTexture2D = device.Get().AllocateUploadTexture2D<int>(2048, 2048);

            for (int i = 0; i < uploadTexture2D.Height; i++)
            {
                new Random(i).NextBytes(uploadTexture2D.View.GetRowSpan(i).AsBytes());
            }

            using ReadOnlyTexture2D<int> readOnlyTexture2D = device.Get().AllocateReadOnlyTexture2D<int>(uploadTexture2D.Width, uploadTexture2D.Height);

            uploadTexture2D.CopyTo(readOnlyTexture2D, x, y, width, height);

            int[,] result = readOnlyTexture2D.ToArray();

            for (int i = 0; i < height; i++)
            {
                Span<int> sourceRow = uploadTexture2D.View.GetRowSpan(i + y).Slice(x, width);
                Span<int> destinationRow = result.GetRowSpan(i).Slice(0, width);

                Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Allocate_ReadBackTexture2D_Copy_Full(Device device)
        {
            int[,] source = new int[256, 256];

            for (int i = 0; i < 256; i++)
            {
                new Random(42).NextBytes(source.GetRowSpan(i).AsBytes());
            }

            using ReadOnlyTexture2D<int> readOnlyTexture2D = device.Get().AllocateReadOnlyTexture2D(source);
            using ReadBackTexture2D<int> readBackTexture2D = device.Get().AllocateReadBackTexture2D<int>(256, 256);

            readOnlyTexture2D.CopyTo(readBackTexture2D);

            Assert.AreEqual(readBackTexture2D.Width, 256);
            Assert.AreEqual(readBackTexture2D.Height, 256);

            for (int i = 0; i < readBackTexture2D.Height; i++)
            {
                Assert.IsTrue(readBackTexture2D.View.GetRowSpan(i).SequenceEqual(source.GetRowSpan(i)));
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(0, 0, 2048, 2048)]
        [Data(128, 0, 1813, 2048)]
        [Data(0, 128, 2000, 944)]
        [Data(97, 33, 512, 794)]
        public void Allocate_ReadBackTexture2D_Copy_Range(Device device, int x, int y, int width, int height)
        {
            int[,] source = new int[2048, 2048];

            for (int i = 0; i < 2048; i++)
            {
                new Random(42).NextBytes(source.GetRowSpan(i).AsBytes());
            }

            using ReadOnlyTexture2D<int> readOnlyTexture2D = device.Get().AllocateReadOnlyTexture2D(source);
            using ReadBackTexture2D<int> readBackTexture2D = device.Get().AllocateReadBackTexture2D<int>(2048, 2048);

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
