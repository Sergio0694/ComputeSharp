using System;
using ComputeSharp.Graphics.Buffers;
using ComputeSharp.Graphics.Buffers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.NetCore.Tests.Internals
{
    [TestClass]
    [TestCategory("Buffers")]
    public class BufferTests
    {
        [TestMethod]
        public void ReadWriteBufferGetSetDataFromSingleValueInt()
        {
            object value = 77;

            using ConstantBuffer<int> buffer = (ConstantBuffer<int>)Gpu.Default.AllocateConstantBufferFromReflectedSingleValue(value);
            int[] result = buffer.GetData();

            Assert.IsTrue(result.Length == 1);
            Assert.IsTrue((int)value == result[0]);
        }

        [TestMethod]
        public void ReadWriteBufferGetSetDataFromSingleValueFloat()
        {
            object value = 3.14f;

            using ConstantBuffer<float> buffer = (ConstantBuffer<float>)Gpu.Default.AllocateConstantBufferFromReflectedSingleValue(value); 
            float[] result = buffer.GetData();

            Assert.IsTrue(result.Length == 1);
            Assert.IsTrue(MathF.Abs(result[0] - (float)value) < 0.0001f);
        }
    }
}
