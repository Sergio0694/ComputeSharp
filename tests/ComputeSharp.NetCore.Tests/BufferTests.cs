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
        public void ReadWriteBufferGetSetData()
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer(array);
            float[] result = buffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(result));
        }

        [TestMethod]
        public void ReadOnlyBufferGetSetData()
        {
            float[] array = Enumerable.Range(0, 4096).Select(i => (float)i).ToArray();

            using ReadOnlyBuffer<float> buffer = Gpu.Default.AllocateReadOnlyBuffer(array);
            float[] result = buffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(result));
        }
    }
}
