using System;
using System.Linq;
using ComputeSharp.Graphics.Buffers;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Buffers.Extensions;
using ComputeSharp.NetCore.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.NetCore.Tests
{
    [TestClass]
    [TestCategory("Buffers")]
    public class BufferTests
    {
        [TestMethod]
        public void ReadWriteBufferGetSetDataFromSingleValueInt()
        {
            object value = 77;

            using ReadOnlyBuffer<int> buffer = (ReadOnlyBuffer<int>)Gpu.Default.AllocateReadOnlyBufferFromReflectedSingleValue(value);
            int[] result = buffer.GetData();

            Assert.IsTrue(result.Length == 1);
            Assert.IsTrue((int)value == result[0]);
        }

        [TestMethod]
        public void ReadWriteBufferGetSetDataFromSingleValueFloat()
        {
            object value = 3.14f;

            using ReadOnlyBuffer<float> buffer = (ReadOnlyBuffer<float>)Gpu.Default.AllocateReadOnlyBufferFromReflectedSingleValue(value); 
            float[] result = buffer.GetData();

            Assert.IsTrue(result.Length == 1);
            Assert.IsTrue(MathF.Abs(result[0] - (float)value) < 0.0001f);
        }

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
