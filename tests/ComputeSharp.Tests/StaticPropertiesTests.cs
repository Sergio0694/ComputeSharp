using System;
using System.Numerics;
using ComputeSharp.Graphics.Buffers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    /// <summary>
    /// A container <see langword="class"/> for static properties to test
    /// </summary>
    public sealed class StaticPropertiesContainer
    {
        /// <summary>
        /// A <see langword="static"/> <see langword="readonly"/> <see langword="float"/> property
        /// </summary>
        public static float ReadonlyFloat => MathF.PI;

        /// <summary>
        /// A <see langword="static"/> <see langword="int"/> property
        /// </summary>
        public static float StaticFloat { get; set; } = MathF.PI;

        /// <summary>
        /// A <see langword="static"/> <see langword="readonly"/> <see langword="Vector2"/> property
        /// </summary>
        public static Vector2 ReadonlyVector2 { get; } = new Vector2(3.14f, 7.77f);

        /// <summary>
        /// A <see langword="static"/> <see langword="Vector2"/> property
        /// </summary>
        public static Vector2 StaticVector2 { get; set; } = new Vector2(3.14f, 7.77f);

        /// <summary>
        /// A <see langword="static"/> <see cref="ReadOnlyBuffer{T}"/> property
        /// </summary>
        public static ReadOnlyBuffer<float> StaticBuffer { get; set; } = Gpu.Default.AllocateReadOnlyBuffer(new[] { 3.14f, 7.77f });
    }

    [TestClass]
    [TestCategory("StaticProperties")]
    public class StaticPropertiesTests
    {
        [TestMethod]
        public void StaticReadonlyScalarPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Action<ThreadIds> action = id => buffer[0] = StaticPropertiesContainer.ReadonlyFloat;

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.ReadonlyFloat) < 0.0001f);
        }

        [TestMethod]
        public void StaticScalarPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Action<ThreadIds> action = id => buffer[0] = StaticPropertiesContainer.StaticFloat;

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.StaticFloat) < 0.0001f);
        }

        [TestMethod]
        public void StaticScalarPropertyRepeatedAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Action<ThreadIds> action = id => buffer[0] = StaticPropertiesContainer.StaticFloat + StaticPropertiesContainer.StaticFloat;

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.StaticFloat * 2) < 0.0001f);
        }

        [TestMethod]
        public void StaticReadonlyVectorPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            Action<ThreadIds> action = id =>
            {
                if (id.X == 0) buffer[0] = StaticPropertiesContainer.ReadonlyVector2.X;
                else buffer[1] = StaticPropertiesContainer.ReadonlyVector2.Y;
            };

            Gpu.Default.For(2, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.ReadonlyVector2.X) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - StaticPropertiesContainer.ReadonlyVector2.Y) < 0.0001f);
        }

        [TestMethod]
        public void StaticVectorPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            Action<ThreadIds> action = id =>
            {
                if (id.X == 0) buffer[0] = StaticPropertiesContainer.StaticVector2.X;
                else buffer[1] = StaticPropertiesContainer.StaticVector2.Y;
            };

            Gpu.Default.For(2, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.StaticVector2.X) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - StaticPropertiesContainer.StaticVector2.Y) < 0.0001f);
        }

        [TestMethod]
        public void StaticReadOnlyBufferPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            Action<ThreadIds> action = id =>
            {
                if (id.X == 0) buffer[0] = StaticPropertiesContainer.StaticBuffer[0];
                else buffer[1] = StaticPropertiesContainer.StaticBuffer[1];
            };

            Gpu.Default.For(2, action);

            float[] result = buffer.GetData();
            float[] expected = StaticPropertiesContainer.StaticBuffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - expected[0]) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - expected[1]) < 0.0001f);
        }
    }
}
