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

            using ConstantBuffer<float> buffer = Gpu.Default.AllocateConstantBuffer(array);
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

            using ConstantBuffer<float> sourceBuffer = Gpu.Default.AllocateConstantBuffer(array);
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
            using ConstantBuffer<float> destinationBuffer = Gpu.Default.AllocateConstantBuffer(sourceBuffer);

            float[] sourceResult = sourceBuffer.GetData();
            float[] destinationResult = destinationBuffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(sourceResult));
            Assert.IsTrue(array.AsSpan().ContentEquals(destinationResult));
        }

        [TestMethod]
        public void ReadOnlyBufferGetSetDataFromReadOnlyBuffer()
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using ConstantBuffer<float> sourceBuffer = Gpu.Default.AllocateConstantBuffer(array);
            using ConstantBuffer<float> destinationBuffer = Gpu.Default.AllocateConstantBuffer(sourceBuffer);

            float[] sourceResult = sourceBuffer.GetData();
            float[] destinationResult = destinationBuffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(sourceResult));
            Assert.IsTrue(array.AsSpan().ContentEquals(destinationResult));
        }
    }
}
