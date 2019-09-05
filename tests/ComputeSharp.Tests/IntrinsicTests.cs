using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Intrinsics")]
    public class IntrinsicTests
    {
        [TestMethod]
        public void IntrinsicWithInlineOutParamater()
        {
            float angle = 80;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(4);

            Action<ThreadIds> action = id =>
            {
                buffer[0] = Hlsl.Sin(angle);
                buffer[1] = Hlsl.Cos(angle);
                Hlsl.SinCos(angle, out float sine, out float cosine);
                buffer[2] = sine;
                buffer[3] = cosine;
            };

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - result[2]) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - result[3]) < 0.0001f);
        }

        [TestMethod]
        public void IntrinsicWithInlineOutParamaterAndDiscard()
        {
            float angle = 80;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            Action<ThreadIds> action = id =>
            {
                buffer[0] = Hlsl.Sin(angle);
                Hlsl.SinCos(angle, out float sine, out _);
                buffer[1] = sine;
            };

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - result[1]) < 0.0001f);
        }

        [TestMethod]
        public void IntrinsicWithOutParamaters()
        {
            float angle = 80;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(4);

            Action<ThreadIds> action = id =>
            {
                buffer[0] = Hlsl.Sin(angle);
                buffer[1] = Hlsl.Cos(angle);
                Hlsl.SinCos(angle, out buffer[2], out buffer[3]);
            };

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - result[2]) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - result[3]) < 0.0001f);
        }
    }
}
