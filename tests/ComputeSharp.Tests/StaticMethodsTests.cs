using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    /// <summary>
    /// A container <see langword="class"/> for static methods to test
    /// </summary>
    public static class StaticMethodsContainer
    {
        [Pure]
        public static float Square(float x) => x * x;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNull]
        public static float SquareWithAttributes(float x) => x * x;

        [Pure]
        public static float Negate(float x) => -x;

        [Pure]
        internal static float InternalSquare(float x) => x * x;

        [Pure]
        public static Float4 Range(float x) => new Float4(x, x + 1, x + 2, x + 3);

        [Pure]
        public static int Sum(Float4 x) => (int)(x.X + x.Y + x.Z + x.W);

        public static void Assign(int x, out int y) => y = x;

        public static void ReadAndSquare(ref int x) => x *= x;
    }

    [TestClass]
    [TestCategory("StaticMethods")]
    public class StaticMethodsTests
    {
        private struct FloatToFloatFunc_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticMethodsContainer.Square(3);
            }
        }

        [TestMethod]
        public void FloatToFloatFunc()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new FloatToFloatFunc_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        private struct InternalFloatToFloatFunc_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticMethodsContainer.InternalSquare(3);
            }
        }

        [TestMethod]
        public void InternalFloatToFloatFunc()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new InternalFloatToFloatFunc_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        private struct FloatToFloat4Func_Shader : IComputeShader
        {
            public ReadWriteBuffer<Float4> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticMethodsContainer.Range(3);
            }
        }

        [TestMethod]
        public void FloatToFloat4Func()
        {
            using ReadWriteBuffer<Float4> buffer = Gpu.Default.AllocateReadWriteBuffer<Float4>(1);

            var shader = new FloatToFloat4Func_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            Float4[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0].X - 3) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[0].Y - 4) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[0].Z - 5) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[0].W - 6) < 0.0001f);
        }

        private struct Float4ToIntFunc_Shader : IComputeShader
        {
            public ReadWriteBuffer<int> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticMethodsContainer.Sum(new Float4(1, 2, 3, 14));
            }
        }

        [TestMethod]
        public void Float4ToIntFunc()
        {
            using ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer<int>(1);

            var shader = new Float4ToIntFunc_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            int[] result = buffer.GetData();

            Assert.IsTrue(result[0] == 20);
        }

        private struct IntToOutIntFunc_Shader : IComputeShader
        {
            public ReadWriteBuffer<int> B;

            public void Execute(ThreadIds ids)
            {
                StaticMethodsContainer.Assign(7, out B[0]);
            }
        }

        [TestMethod]
        public void IntToOutIntFunc()
        {
            using ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer<int>(1);

            var shader = new IntToOutIntFunc_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            int[] result = buffer.GetData();

            Assert.IsTrue(result[0] == 7);
        }

        private struct IntToRefIntFunc_Shader : IComputeShader
        {
            public ReadWriteBuffer<int> B;

            public void Execute(ThreadIds ids)
            {
                StaticMethodsContainer.ReadAndSquare(ref B[0]);
            }
        }

        [TestMethod]
        public void IntToRefIntFunc()
        {
            int[] data = { 3 };
            using ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer(data);

            var shader = new IntToRefIntFunc_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            buffer.GetData(data);

            Assert.IsTrue(data[0] == 9);
        }
    }
}

