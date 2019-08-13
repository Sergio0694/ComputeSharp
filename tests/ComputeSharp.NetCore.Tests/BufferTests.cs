using System;
using System.Linq;
using ComputeSharp.Graphics.Buffers;
using ComputeSharp.NetCore.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.NetCore.Tests
{
    [TestClass]
    [TestCategory("Buffers")]
    public class BufferTests
    {
        [TestMethod]
        public void ReadWriteBufferGetSetDataFromArray()
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer(array);
            float[] result = buffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(result));
        }

        [TestMethod]
        public void ReadOnlyBufferGetSetDataFromArray()
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using ReadOnlyBuffer<float> buffer = Gpu.Default.AllocateReadOnlyBuffer(array);
            float[] result = buffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(result));
        }

        [TestMethod]
        public void ReadWriteBufferGetSetDataFromReadWriteBuffer()
        {
            float[] array = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            using ReadWriteBuffer<float> sourceBuffer = Gpu.Default.AllocateReadWriteBuffer(array);
            using ReadWriteBuffer<float> destinationBuffer = Gpu.Default.AllocateReadWriteBuffer(sourceBuffer);

            float[] sourceResult = sourceBuffer.GetData();
            float[] destinationResult = destinationBuffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(sourceResult));
            Assert.IsTrue(array.AsSpan().ContentEquals(destinationResult));
        }

        [TestMethod]
        public void ReadWriteBufferGetSetDataFromReadOnlyBuffer()
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using ReadOnlyBuffer<float> sourceBuffer = Gpu.Default.AllocateReadOnlyBuffer(array);
            using ReadWriteBuffer<float> destinationBuffer = Gpu.Default.AllocateReadWriteBuffer(sourceBuffer);

            float[] sourceResult = sourceBuffer.GetData();
            float[] destinationResult = destinationBuffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(sourceResult));
            Assert.IsTrue(array.AsSpan().ContentEquals(destinationResult));
        }

        [TestMethod]
        public void ReadOnlyBufferGetSetDataFromReadWriteBuffer()
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using ReadWriteBuffer<float> sourceBuffer = Gpu.Default.AllocateReadWriteBuffer(array);
            using ReadOnlyBuffer<float> destinationBuffer = Gpu.Default.AllocateReadOnlyBuffer(sourceBuffer);

            float[] sourceResult = sourceBuffer.GetData();
            float[] destinationResult = destinationBuffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(sourceResult));
            Assert.IsTrue(array.AsSpan().ContentEquals(destinationResult));
        }

        [TestMethod]
        public void ReadOnlyBufferGetSetDataFromReadOnlyBuffer()
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using ReadOnlyBuffer<float> sourceBuffer = Gpu.Default.AllocateReadOnlyBuffer(array);
            using ReadOnlyBuffer<float> destinationBuffer = Gpu.Default.AllocateReadOnlyBuffer(sourceBuffer);

            float[] sourceResult = sourceBuffer.GetData();
            float[] destinationResult = destinationBuffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(sourceResult));
            Assert.IsTrue(array.AsSpan().ContentEquals(destinationResult));
        }
    }
}
