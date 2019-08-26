using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("HlslVectorTypes")]
    public class HlslVectorTypesTests
    {
        [TestMethod]
        public void LocalScalarAssignToBuffer()
        {
            Float2 f2 = new Float2(1, 2);
            using ReadWriteBuffer<Float2> buffer = Gpu.Default.AllocateReadWriteBuffer<Float2>(1);

            Action<ThreadIds> action = id => buffer[0] = f2.YX;

            Gpu.Default.For(1, action);

            Float2[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0].X - f2.Y) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[0].Y - f2.X) < 0.0001f);
        }
    }
}
