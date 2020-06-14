using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("StaticFunctions")]
    public class StaticFunctionsTests
    {
        private struct ExternalStaticFunctionDirect_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticMethodsContainer.Square(4);
            }
        }

        [TestMethod]
        public void ExternalStaticFunctionDirect()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new ExternalStaticFunctionDirect_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 16) < 0.0001f);
        }

        private struct ExternalStaticFunctionWithAttributesDirect_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticMethodsContainer.SquareWithAttributes(4);
            }
        }

        [TestMethod]
        public void ExternalStaticFunctionWithAttributesDirect()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new ExternalStaticFunctionWithAttributesDirect_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 16) < 0.0001f);
        }

        private struct FloatToFloatFunc_Shader : IComputeShader
        {
            public Func<float, float> F;
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = F(3);
            }
        }

        [TestMethod]
        public void FloatToFloatFunc()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new FloatToFloatFunc_Shader
            {
                F = StaticMethodsContainer.Square,
                B = buffer
            };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        [TestMethod]
        public void InternalFloatToFloatFunc()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Func<float, float> square = StaticMethodsContainer.InternalSquare;

            var shader = new FloatToFloatFunc_Shader
            {
                F = square,
                B = buffer
            };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        [TestMethod]
        public void ChangingFuncsFromSameMethod()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new FloatToFloatFunc_Shader
            {
                F = StaticMethodsContainer.Square,
                B = buffer
            };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);

            shader.F = StaticMethodsContainer.Negate;

            Gpu.Default.For(1, shader);

            result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] + 3) < 0.0001f);
        }

        private struct ChangingFuncsFromExternalMethod_Shader : IComputeShader
        {
            public Func<float, float> F;
            public ReadWriteBuffer<float> B;
            public float I;

            public void Execute(ThreadIds ids)
            {
                B[0] = F(I);
            }
        }

        [TestMethod]
        public void ChangingFuncsFromExternalMethod()
        {
            MethodRunner(StaticMethodsContainer.Square, 3, 9);

            MethodRunner(StaticMethodsContainer.Negate, 3, -3);
        }

        private void MethodRunner(Func<float, float> func, float input, float expected)
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new ChangingFuncsFromExternalMethod_Shader
            {
                F = func,
                B = buffer,
                I = input
            };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - expected) < 0.0001f);
        }

        public delegate float Squarer(float x);

        private struct CustomStaticDelegate_Shader : IComputeShader
        {
            public Squarer F;
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = F(3);
            }
        }

        [TestMethod]
        public void CustomStaticDelegate()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Squarer square = StaticMethodsContainer.Square;

            var shader = new CustomStaticDelegate_Shader
            {
                F = square,
                B = buffer
            };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        [TestMethod]
        public void InlineStatelessDelegate()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Func<float, float> f = x => x * x;

            var shader = new FloatToFloatFunc_Shader
            {
                F = f,
                B = buffer
            };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        [TestMethod]
        public void StatelessLocalFunctionToDelegate()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            float Func(float x) => x * x;

            Func<float, float> f = Func;

            var shader = new FloatToFloatFunc_Shader
            {
                F = f,
                B = buffer
            };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        [TestMethod]
        public void StaticLocalFunction()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            static float f(float x) => x * x;

            var shader = new FloatToFloatFunc_Shader
            {
                F = f,
                B = buffer
            };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        [TestMethod]
        public void StatelessLocalFunction()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            float f(float x) => x * x;

            var shader = new FloatToFloatFunc_Shader
            {
                F = f,
                B = buffer
            };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        public static float Square(float x) => x * x;

        private struct StaticMethodInSameClass_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = Square(3);
            }
        }

        [TestMethod]
        public void StaticMethodInSameClass()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new StaticMethodInSameClass_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        public static Func<float, float> SquareFunc { get; } = x => x * x;

        private struct StaticFunctionInSameClass_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = SquareFunc(3);
            }
        }

        [TestMethod]
        public void StaticFunctionInSameClass()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new StaticFunctionInSameClass_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        private struct StaticFunctionInExternalClass_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticPropertiesContainer.SquareFunc(3);
            }
        }

        [TestMethod]
        public void StaticFunctionInExternalClass()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new StaticFunctionInExternalClass_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        private static float Sigmoid(float x) => 1 / (1 + Hlsl.Exp(-x));

        private struct ReadmeSample_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                int offset = ids.X + ids.Y * 1;
                float pow = Hlsl.Pow(B[offset], 2); // 9
                B[offset] = Sigmoid(pow); // 0.9998766
            }
        }

        [TestMethod]
        public void ReadmeSample()
        {
            float[] buffer = { 3 };
            using ReadWriteBuffer<float> xBuffer = Gpu.Default.AllocateReadWriteBuffer(buffer);

            var shader = new ReadmeSample_Shader { B = xBuffer };

            Gpu.Default.For(1, shader);

            float[] result = xBuffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 0.9998766f) < 0.0001f);
        }
    }
}
