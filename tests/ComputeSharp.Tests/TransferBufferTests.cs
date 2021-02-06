using System;
using System.Linq;
using System.Runtime.CompilerServices;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Extensions;
using Microsoft.Toolkit.HighPerformance.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("TransferBuffer")]
    public partial class TransferBufferTests
    {
        [TestMethod]
        [DataRow(typeof(UploadBuffer<>))]
        [DataRow(typeof(ReadBackBuffer<>))]
        public unsafe void Allocate_Uninitialized_Ok(Type bufferType)
        {
            using TransferBuffer<float> buffer = Gpu.Default.AllocateTransferBuffer<float>(bufferType, 128);

            Assert.IsNotNull(buffer);
            Assert.AreEqual(buffer.Length, 128);
            Assert.AreEqual(buffer.Memory.Length, 128);
            Assert.AreEqual(buffer.Span.Length, 128);
            Assert.IsTrue(Unsafe.AreSame(ref buffer.Memory.Span[0], ref buffer.Span[0]));
            Assert.IsTrue(Unsafe.AreSame(ref *(float*)buffer.Memory.Pin().Pointer, ref buffer.Span[0]));
            Assert.AreSame(buffer.GraphicsDevice, Gpu.Default);
        }

        [TestMethod]
        [DataRow(typeof(UploadBuffer<>), -247824)]
        [DataRow(typeof(UploadBuffer<>), -1)]
        [DataRow(typeof(ReadBackBuffer<>), -247824)]
        [DataRow(typeof(ReadBackBuffer<>), -1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Type bufferType, int length)
        {
            using TransferBuffer<float> buffer = Gpu.Default.AllocateTransferBuffer<float>(bufferType, length);
        }

        [TestMethod]
        [DataRow(typeof(UploadBuffer<>))]
        [DataRow(typeof(ReadBackBuffer<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Type bufferType)
        {
            using TransferBuffer<float> buffer = Gpu.Default.AllocateTransferBuffer<float>(bufferType, 128);

            buffer.Dispose();

            _ = buffer.Span;
        }

        [TestMethod]
        public void Allocate_UploadBuffer_Copy_Full()
        {
            using UploadBuffer<int> uploadBuffer = Gpu.Default.AllocateUploadBuffer<int>(4096);

            new Random(42).NextBytes(uploadBuffer.Span.AsBytes());

            using ReadOnlyBuffer<int> readOnlyBuffer = Gpu.Default.AllocateReadOnlyBuffer<int>(uploadBuffer.Length);

            uploadBuffer.CopyTo(readOnlyBuffer);

            int[] result = readOnlyBuffer.ToArray();

            Assert.AreEqual(uploadBuffer.Length, result.Length);
            Assert.IsTrue(uploadBuffer.Span.SequenceEqual(result));
        }

        [TestMethod]
        [DataRow(0, 0, 4096)]
        [DataRow(128, 0, 2048)]
        [DataRow(0, 128, 2048)]
        [DataRow(97, 33, 512)]
        public void Allocate_UploadBuffer_Copy_Range(int destinationOffset, int bufferOffset, int count)
        {
            using UploadBuffer<int> uploadBuffer = Gpu.Default.AllocateUploadBuffer<int>(4096);

            new Random(42).NextBytes(uploadBuffer.Span.AsBytes());

            using ReadOnlyBuffer<int> readOnlyBuffer = Gpu.Default.AllocateReadOnlyBuffer<int>(uploadBuffer.Length);

            uploadBuffer.CopyTo(readOnlyBuffer, destinationOffset, bufferOffset, count);

            int[] result = readOnlyBuffer.ToArray(destinationOffset, count);

            Assert.AreEqual(result.Length, count);
            Assert.IsTrue(uploadBuffer.Span.Slice(bufferOffset, count).SequenceEqual(result));
        }

        [TestMethod]
        public void Allocate_ReadBackBuffer_Copy_Full()
        {
            int[] source = new int[4096];

            new Random(42).NextBytes(source.AsSpan().AsBytes());

            using ReadOnlyBuffer<int> readOnlyBuffer = Gpu.Default.AllocateReadOnlyBuffer(source);
            using ReadBackBuffer<int> readBackBuffer = Gpu.Default.AllocateReadBackBuffer<int>(readOnlyBuffer.Length);

            readOnlyBuffer.CopyTo(readBackBuffer);

            Assert.AreEqual(source.Length, readBackBuffer.Length);
            Assert.IsTrue(source.AsSpan().SequenceEqual(readBackBuffer.Span));
        }

        [TestMethod]
        [DataRow(0, 0, 4096)]
        [DataRow(128, 0, 2048)]
        [DataRow(0, 128, 2048)]
        [DataRow(97, 33, 512)]
        public void Allocate_ReadBackBuffer_Copy_Range(int destinationOffset, int bufferOffset, int count)
        {
            int[] source = new int[4096];

            new Random(42).NextBytes(source.AsSpan().AsBytes());

            using ReadOnlyBuffer<int> readOnlyBuffer = Gpu.Default.AllocateReadOnlyBuffer(source);
            using ReadBackBuffer<int> readBackBuffer = Gpu.Default.AllocateReadBackBuffer<int>(readOnlyBuffer.Length);

            readOnlyBuffer.CopyTo(readBackBuffer, destinationOffset, bufferOffset, count);

            Assert.AreEqual(source.Length, readBackBuffer.Length);
            Assert.IsTrue(source.AsSpan(bufferOffset, count).SequenceEqual(readBackBuffer.Span.Slice(destinationOffset, count)));
        }
    }
}
