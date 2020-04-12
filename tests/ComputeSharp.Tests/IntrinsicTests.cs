using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Intrinsics")]
    public class IntrinsicTests
    {
        private struct IntrinsicWithInlineOutParamater_Shader : IComputeShader
        {
            public float A;
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = Hlsl.Sin(A);
                B[1] = Hlsl.Cos(A);
                Hlsl.SinCos(A, out float sine, out float cosine);
                B[2] = sine;
                B[3] = cosine;
            }
        }

        [TestMethod]
        public void IntrinsicWithInlineOutParamater()
        {
            float angle = 80;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(4);

            var shader = new IntrinsicWithInlineOutParamater_Shader { A = angle, B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - result[2]) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - result[3]) < 0.0001f);
        }

        private struct IntrinsicWithInlineOutParamaterAndDiscard_Shader : IComputeShader
        {
            public float A;
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = Hlsl.Sin(A);
                Hlsl.SinCos(A, out float sine, out _);
                B[1] = sine;
            }
        }

        [TestMethod]
        public void IntrinsicWithInlineOutParamaterAndDiscard()
        {
            float angle = 80;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            var shader = new IntrinsicWithInlineOutParamaterAndDiscard_Shader { A = angle, B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - result[1]) < 0.0001f);
        }

        private struct IntrinsicWithOutParamaters_Shader : IComputeShader
        {
            public float A;
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = Hlsl.Sin(A);
                B[1] = Hlsl.Cos(A);
                Hlsl.SinCos(A, out B[2], out B[3]);
            }
        }

        [TestMethod]
        public void IntrinsicWithOutParamaters()
        {
            float angle = 80;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(4);

            var shader = new IntrinsicWithOutParamaters_Shader { A = angle, B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - result[2]) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - result[3]) < 0.0001f);
        }
    }
}
