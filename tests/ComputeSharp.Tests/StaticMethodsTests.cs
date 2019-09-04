using System;
using System.Diagnostics.Contracts;
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
        internal static float InternalSquare(float x) => x * x;

        [Pure]
        public static Float4 Range(float x) => new Float4(x, x + 1, x + 2, x + 3);

        [Pure]
        public static int Sum(Float4 x) => (int)(x.X + x.Y + x.Z + x.W);

        public static void Assign(int x, out int y) => y = x;

        public static void ReadAndSquare(ref int x) => x *= x;
    }

    [TestClass]
    [TestCategory("StaticMethodsTests")]
    public class StaticMethodsTests
    {
        [TestMethod]
        public void FloatToFloatFunc()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Gpu.Default.For(1, id => buffer[0] = StaticMethodsContainer.Square(3));

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        [TestMethod]
        public void InternalFloatToFloatFunc()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Gpu.Default.For(1, id => buffer[0] = StaticMethodsContainer.InternalSquare(3));

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - 9) < 0.0001f);
        }

        [TestMethod]
        public void FloatToFloat4Func()
        {
            using ReadWriteBuffer<Float4> buffer = Gpu.Default.AllocateReadWriteBuffer<Float4>(1);

            Gpu.Default.For(1, id => buffer[0] = StaticMethodsContainer.Range(3));

            Float4[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0].X - 3) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[0].Y - 4) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[0].Z - 5) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[0].W - 6) < 0.0001f);
        }

        [TestMethod]
        public void Float4ToIntFunc()
        {
            using ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer<int>(1);

            Gpu.Default.For(1, id => buffer[0] = StaticMethodsContainer.Sum(new Float4(1, 2, 3, 14)));

            int[] result = buffer.GetData();

            Assert.IsTrue(result[0] == 20);
        }

        [TestMethod]
        public void IntToOutIntFunc()
        {
            using ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer<int>(1);

            Gpu.Default.For(1, id => StaticMethodsContainer.Assign(7, out buffer[0]));

            int[] result = buffer.GetData();

            Assert.IsTrue(result[0] == 7);
        }

        [TestMethod]
        public void IntToRefIntFunc()
        {
            int[] data = { 3 };
            using ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer(data);

            Gpu.Default.For(1, id => StaticMethodsContainer.ReadAndSquare(ref buffer[0]));

            buffer.GetData(data);

            Assert.IsTrue(data[0] == 9);
        }
    }
}

