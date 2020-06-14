using System;
using System.Linq;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("ShaderDispatch")]
    public class ShaderDispatch
    {
        private struct WriteToReadWriteBufferDispatch1D_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[ids.X] = ids.X;
            }
        }

        [TestMethod]
        public void WriteToReadWriteBufferDispatch1D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(100);

            var shader = new WriteToReadWriteBufferDispatch1D_Shader { B = buffer };

            Gpu.Default.For(100, shader);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }

        [TestMethod]
        public void WriteToReadWriteBufferManualDispatch1D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(100);

            var shader = new WriteToReadWriteBufferDispatch1D_Shader { B = buffer };

            Gpu.Default.For(100, 1, 1, 64, 1, 1, shader);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }

        private struct WriteToReadWriteBufferDispatch2D_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[ids.X + ids.Y * 10] = ids.X + ids.Y * 10;
            }
        }

        [TestMethod]
        public void WriteToReadWriteBufferDispatch2D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(100);

            var shader = new WriteToReadWriteBufferDispatch2D_Shader { B = buffer };

            Gpu.Default.For(10, 10, shader);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }

        private struct WriteToReadWriteBufferDispatch3D_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[ids.X + ids.Y * 10 + ids.Z * 100] = ids.X + ids.Y * 10 + ids.Z * 100;
            }
        }

        [TestMethod]
        public void WriteToReadWriteBufferDispatch3D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1000);

            var shader = new WriteToReadWriteBufferDispatch3D_Shader { B = buffer };

            Gpu.Default.For(10, 10, 10, shader);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 1000).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }

        [TestMethod]
        public void WriteToReadWriteBufferManualDispatch3D()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1000);

            var shader = new WriteToReadWriteBufferDispatch3D_Shader { B = buffer };

            Gpu.Default.For(10, 10, 10, 4, 4, 4, shader);

            float[] array = buffer.GetData();
            float[] expected = Enumerable.Range(0, 1000).Select(i => (float)i).ToArray();

            Assert.IsTrue(array.AsSpan().ContentEquals(expected));
        }
    }
}
