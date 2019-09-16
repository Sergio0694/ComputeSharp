using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("HlslVectorTypes")]
    public class HlslVectorTypesTests
    {
        [TestMethod]
        public void SequentialCompilation()
        {
            Float2 f2 = new Float2(1, 2);
            using ReadWriteBuffer<Float2> buffer = Gpu.Default.AllocateReadWriteBuffer<Float2>(2);

            Action<ThreadIds> action1 = id => buffer[0] = f2.YX;

            Action<ThreadIds> action2 = id => buffer[1] = f2.YX;

            Gpu.Default.For(1, action1);
            Gpu.Default.For(1, action2);
        }

        [TestMethod]
        public void LocalFloatAssignToFloat2Buffer()
        {
            Float2 f2 = new Float2(1, 2);
            using ReadWriteBuffer<Float2> buffer = Gpu.Default.AllocateReadWriteBuffer<Float2>(1);

            Action<ThreadIds> action = id => buffer[0] = f2.YX;

            Gpu.Default.For(1, action);

            Float2[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0].X - f2.Y) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[0].Y - f2.X) < 0.0001f);
        }

        [TestMethod]
        public void Int2Properties()
        {
            using ReadWriteBuffer<Int2> buffer = Gpu.Default.AllocateReadWriteBuffer<Int2>(1);

            Action<ThreadIds> action = id => buffer[0].X = 1;

            Gpu.Default.For(1, action);

            Int2[] result = buffer.GetData();

            Assert.IsTrue(result[0].X == 1);
        }

        [TestMethod]
        public void LocalBoolAssignToBool2Buffer()
        {
            Bool2 b2 = Bool2.TrueY;
            using ReadWriteBuffer<Bool2> buffer = Gpu.Default.AllocateReadWriteBuffer<Bool2>(1);

            Action<ThreadIds> action = id => buffer[0] = b2;

            Gpu.Default.For(1, action);

            Bool2[] result = buffer.GetData();

            Assert.IsTrue(result[0].X == b2.X);
            Assert.IsTrue(result[0].Y == b2.Y);
        }

        [TestMethod]
        public void Bool3Operations()
        {
            using ReadWriteBuffer<Bool3> buffer = Gpu.Default.AllocateReadWriteBuffer<Bool3>(3);

            Action<ThreadIds> action = id =>
            {
                buffer[0] = Bool3.TrueX;
                buffer[1] = Bool3.True;
                buffer[2].YZ = Bool2.True;
            };

            Gpu.Default.For(1, action);

            Bool3[] result = buffer.GetData();

            Assert.IsTrue(result[0].X && !result[0].Y && !result[0].Z);
            Assert.IsTrue(result[1].X && result[1].Y && result[1].Z);
            Assert.IsTrue(!result[2].X && result[2].Y && result[2].Z);
        }
    }
}
