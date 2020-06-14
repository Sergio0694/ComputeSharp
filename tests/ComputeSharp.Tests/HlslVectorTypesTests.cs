using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("HlslVectorTypes")]
    public class HlslVectorTypesTests
    {
        private struct SequentialCompilation_Shader_1 : IComputeShader
        {
            public Float2 F2;
            public ReadWriteBuffer<Float2> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = F2.XY;
            }
        }

        private struct SequentialCompilation_Shader_2 : IComputeShader
        {
            public Float2 F2;
            public ReadWriteBuffer<Float2> B;

            public void Execute(ThreadIds ids)
            {
                B[1] = F2.XY;
            }
        }

        [TestMethod]
        public void SequentialCompilation()
        {
            Float2 f2 = new Float2(1, 2);
            using ReadWriteBuffer<Float2> buffer = Gpu.Default.AllocateReadWriteBuffer<Float2>(2);

            var shader1 = new SequentialCompilation_Shader_1 { F2 = f2, B = buffer };
            var shader2 = new SequentialCompilation_Shader_2 { F2 = f2, B = buffer };

            Gpu.Default.For(1, shader1);
            Gpu.Default.For(1, shader2);
        }

        private struct LocalFloatAssignToFloat2Buffer_Shader : IComputeShader
        {
            public Float2 F2;
            public ReadWriteBuffer<Float2> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = F2.YX;
            }
        }

        [TestMethod]
        public void LocalFloatAssignToFloat2Buffer()
        {
            Float2 f2 = new Float2(1, 2);
            using ReadWriteBuffer<Float2> buffer = Gpu.Default.AllocateReadWriteBuffer<Float2>(1);

            var shader = new LocalFloatAssignToFloat2Buffer_Shader { F2 = f2, B = buffer };

            Gpu.Default.For(1, shader);

            Float2[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0].X - f2.Y) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[0].Y - f2.X) < 0.0001f);
        }

        private struct Int2Properties_Shader : IComputeShader
        {
            public ReadWriteBuffer<Int2> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = 1;
            }
        }

        [TestMethod]
        public void Int2Properties()
        {
            using ReadWriteBuffer<Int2> buffer = Gpu.Default.AllocateReadWriteBuffer<Int2>(1);

            var shader = new Int2Properties_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            Int2[] result = buffer.GetData();

            Assert.IsTrue(result[0].X == 1);
        }

        private struct LocalBoolAssignToBool2Buffer_Shader : IComputeShader
        {
            public Bool2 B2;
            public ReadWriteBuffer<Bool2> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = B2;
            }
        }

        [TestMethod]
        public void LocalBoolAssignToBool2Buffer()
        {
            Bool2 b2 = Bool2.TrueY;
            using ReadWriteBuffer<Bool2> buffer = Gpu.Default.AllocateReadWriteBuffer<Bool2>(1);

            var shader = new LocalBoolAssignToBool2Buffer_Shader { B2 = b2, B = buffer };

            Gpu.Default.For(1, shader);

            Bool2[] result = buffer.GetData();

            Assert.IsTrue(result[0].X == b2.X);
            Assert.IsTrue(result[0].Y == b2.Y);
        }

        private struct Bool3Operations_Shader : IComputeShader
        {
            public ReadWriteBuffer<Bool3> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = Bool3.TrueX;
                B[1] = Bool3.True;
                B[2].YZ = Bool2.True;
            }
        }

        [TestMethod]
        public void Bool3Operations()
        {
            using ReadWriteBuffer<Bool3> buffer = Gpu.Default.AllocateReadWriteBuffer<Bool3>(3);

            var shader = new Bool3Operations_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            Bool3[] result = buffer.GetData();

            Assert.IsTrue(result[0].X && !result[0].Y && !result[0].Z);
            Assert.IsTrue(result[1].X && result[1].Y && result[1].Z);
            Assert.IsTrue(!result[2].X && result[2].Y && result[2].Z);
        }
    }
}
