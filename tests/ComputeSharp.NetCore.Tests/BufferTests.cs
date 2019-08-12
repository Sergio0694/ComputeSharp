using System;
using ComputeSharp.Graphics.Buffers;
using ComputeSharp.NetCore.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.NetCore.Sample
{
    [TestClass]
    [TestCategory("Buffers")]
    public class BufferTests
    {
        [TestMethod]
        public void ReadWriteBufferGetSetData()
        {
            float[] array = new float[4096];
            array.AsSpan().FillRandom();

            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer(array);
            float[] result = buffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(result));
        }

        [TestMethod]
        public void ReadOnlyBufferGetSetData()
        {
            float[] array = new float[4096];
            array.AsSpan().FillRandom();

            using ReadOnlyBuffer<float> buffer = Gpu.Default.AllocateConstantBuffer(array);
            float[] result = buffer.GetData();

            Assert.IsTrue(array.AsSpan().ContentEquals(result));
        }
    }
}
