using System;
using System.Linq;
using System.Numerics;
using ComputeSharp.Graphics.Buffers;
using ComputeSharp.NetCore.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.NetCore.Tests
{
    [TestClass]
    [TestCategory("ShaderClosure")]
    public class ShaderClosureTests
    {
        [TestMethod]
        public void LocalScalarAssignToBuffer()
        {
            float value = 10;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Action<ThreadIds> action = id => buffer[0] = value;

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - value) < 0.0001f);
        }

        [TestMethod]
        public void LocalScalarAssignToBufferWithinLocalMethod()
        {
            float value = 10;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            void Foo(ThreadIds id) => buffer[0] = value;

            Gpu.Default.For(1, Foo);

            float[] result = buffer.GetData();

            Assert.IsTrue((int)result[0] == (int)value);
        }

        [TestMethod]
        public void LocalKnownVectorAssignToBuffer()
        {
            Vector4 vector4 = new Vector4(1, 2, 3, 4);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(4);

            Action<ThreadIds> action = id =>
            {
                if (id.X == 0) buffer[0] = vector4.X;
                else if (id.X == 1) buffer[1] = vector4.Y;
                else if (id.X == 2) buffer[2] = vector4.Z;
                else if (id.X == 3) buffer[3] = vector4.W;
            };

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();
            float[] expected = { 1, 2, 3, 4 };

            Assert.IsTrue(result.AsSpan().ContentEquals(expected));
        }

        [TestMethod]
        public void CopyBetweenWriteableBuffers()
        {
            float[] source = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            using ReadWriteBuffer<float> input = Gpu.Default.AllocateReadWriteBuffer(source); 
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(source.Length);

            Action<ThreadIds> action = id => buffer[id.X] = input[id.X];

            Gpu.Default.For(source.Length, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(result.AsSpan().ContentEquals(source));
        }

        [TestMethod]
        public void CopyBetweenReadOnlyAndWriteableBuffers()
        {
            float[] source = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            using ConstantBuffer<float> input = Gpu.Default.AllocateConstantBuffer(source);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(source.Length);

            Action<ThreadIds> action = id => buffer[id.X] = input[id.X];

            Gpu.Default.For(source.Length, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(result.AsSpan().ContentEquals(source));
        }
    }
}
