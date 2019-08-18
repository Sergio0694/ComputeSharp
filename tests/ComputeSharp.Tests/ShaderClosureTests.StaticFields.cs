using System;
using System.Numerics;
using ComputeSharp.Graphics.Buffers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    /// <summary>
    /// A container <see langword="class"/> for static fields to test
    /// </summary>
    public sealed class StaticFieldsContainer
    {
        /// <summary>
        /// A <see langword="static"/> <see langword="readonly"/> <see langword="float"/> field
        /// </summary>
        public static readonly float ReadonlyFloat = MathF.PI;

        /// <summary>
        /// A <see langword="static"/> <see langword="int"/> field
        /// </summary>
        public static float StaticFloat = MathF.PI;

        /// <summary>
        /// A <see langword="static"/> <see langword="readonly"/> <see langword="Vector2"/> field
        /// </summary>
        public static readonly Vector2 ReadonlyVector2 = new Vector2(3.14f, 7.77f);

        /// <summary>
        /// A <see langword="static"/> <see langword="Vector2"/> field
        /// </summary>
        public static Vector2 StaticVector2 = new Vector2(3.14f, 7.77f);

        /// <summary>
        /// A <see langword="static"/> <see cref="ReadOnlyBuffer{T}"/> field
        /// </summary>
        public static ReadOnlyBuffer<float> StaticBuffer = Gpu.Default.AllocateReadOnlyBuffer(new[] { 3.14f, 7.77f });
    }

    public partial class ShaderClosureTests
    {
        [TestMethod]
        public void StaticReadonlyScalarFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Action<ThreadIds> action = id => buffer[0] = StaticFieldsContainer.ReadonlyFloat;

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.ReadonlyFloat) < 0.0001f);
        }

        [TestMethod]
        public void StaticScalarFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Action<ThreadIds> action = id => buffer[0] = StaticFieldsContainer.StaticFloat;

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.StaticFloat) < 0.0001f);
        }

        [TestMethod]
        public void StaticScalarFieldRepeatedAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Action<ThreadIds> action = id => buffer[0] = StaticFieldsContainer.StaticFloat + StaticFieldsContainer.StaticFloat;

            Gpu.Default.For(1, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.StaticFloat * 2) < 0.0001f);
        }

        [TestMethod]
        public void StaticReadonlyVectorFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            Action<ThreadIds> action = id =>
            {
                if (id.X == 0) buffer[0] = StaticFieldsContainer.ReadonlyVector2.X;
                else buffer[1] = StaticFieldsContainer.ReadonlyVector2.Y;
            };

            Gpu.Default.For(2, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.ReadonlyVector2.X) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - StaticFieldsContainer.ReadonlyVector2.Y) < 0.0001f);
        }

        [TestMethod]
        public void StaticVectorFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            Action<ThreadIds> action = id =>
            {
                if (id.X == 0) buffer[0] = StaticFieldsContainer.StaticVector2.X;
                else buffer[1] = StaticFieldsContainer.StaticVector2.Y;
            };

            Gpu.Default.For(2, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.StaticVector2.X) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - StaticFieldsContainer.StaticVector2.Y) < 0.0001f);
        }

        [TestMethod]
        public void StaticReadOnlyBufferFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            Action<ThreadIds> action = id =>
            {
                if (id.X == 0) buffer[0] = StaticFieldsContainer.StaticBuffer[0];
                else buffer[1] = StaticFieldsContainer.StaticBuffer[1];
            };

            Gpu.Default.For(2, action);

            float[] result = buffer.GetData();
            float[] expected = StaticFieldsContainer.StaticBuffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - expected[0]) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - expected[1]) < 0.0001f);
        }
    }
}
