using System;
using System.Linq;
using ComputeSharp.Graphics.Buffers;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("ShaderDispatch")]
    public class ShaderDispatch
    {
        [TestMethod]
        public void WriteToReadWriteBufferDispatch1D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(100);

            Action<ThreadIds> action = id => buffer[id.X] = id.X;

            Gpu.Default.For(100, action);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }

        [TestMethod]
        public void WriteToReadWriteBufferManualDispatch1D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(100);

            Action<ThreadIds> action = id => buffer[id.X] = id.X;

            Gpu.Default.For(100, 1, 1, 64, 1, 1, action);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }

        [TestMethod]
        public void WriteToReadWriteBufferDispatch2D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(100);

            Action<ThreadIds> action = id => buffer[id.X + id.Y * 10] = id.X + id.Y * 10;

            Gpu.Default.For(10, 10, action);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }

        [TestMethod]
        public void WriteToReadWriteBufferDispatch3D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1000);

            Action<ThreadIds> action = id => buffer[id.X + id.Y * 10 + id.Z * 100] = id.X + id.Y * 10 + id.Z * 100;

            Gpu.Default.For(10, 10, 10, action);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 1000).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }

        [TestMethod]
        public void WriteToReadWriteBufferManualDispatch3D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1000);

            Action<ThreadIds> action = id => buffer[id.X + id.Y * 10 + id.Z * 100] = id.X + id.Y * 10 + id.Z * 100;

            Gpu.Default.For(10, 10, 10, 4, 4, 4, action);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 1000).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }
    }
}
