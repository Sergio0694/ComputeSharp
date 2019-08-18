using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers;
using ComputeSharp.Graphics.Buffers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Internals
{
    [TestClass]
    [TestCategory("Buffers")]
    public class BufferTests
    {
        [TestMethod]
        public void ConstantBufferGetSetFromGenericValues()
        {
            object[] values = { 7, 3.14, Vector2.UnitX, 3.14f, new Vector4(7.14f, 7.15f, 7.77f, 2.55f) };

            using ConstantBuffer<Vector4> buffer = (ConstantBuffer<Vector4>)Gpu.Default.AllocateConstantBufferFromReflectedValues(values);
            Vector4[] data = buffer.GetData();
            ref byte r0 = ref Unsafe.As<Vector4, byte>(ref data[0]);

            int i = Unsafe.As<byte, int>(ref Unsafe.Add(ref r0, 0));            // 0 -> 3, sizeof(int) == 4
            double d = Unsafe.As<byte, double>(ref Unsafe.Add(ref r0, 4));      // 4 -> 11, sizeof(double) == 8
            Vector2 v2 = Unsafe.As<byte, Vector2>(ref Unsafe.Add(ref r0, 16));  // 16 -> 23, sizeof(Vector2) == 8
            float f = Unsafe.As<byte, float>(ref Unsafe.Add(ref r0, 24));       // 24 -> 27, sizeof(float) == 4
            Vector4 v4 = Unsafe.As<byte, Vector4>(ref Unsafe.Add(ref r0, 32));  // 32 -> 47, sizeof(Vector4) == 16

            Assert.IsTrue((int)values[0] == i);
            Assert.IsTrue(Math.Abs((double)values[1] - d) < 0.0001);
            Assert.IsTrue((Vector2)values[2] == v2);
            Assert.IsTrue(MathF.Abs((float)values[3] - f) < 0.0001);
            Assert.IsTrue((Vector4)values[4] == v4);
        }
    }
}
