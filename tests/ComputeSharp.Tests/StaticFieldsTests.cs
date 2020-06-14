using System;
using System.Numerics;
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

    [TestClass]
    [TestCategory("StaticFields")]
    public class StaticFieldsTests
    {
        private struct StaticReadonlyScalarFieldAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticFieldsContainer.ReadonlyFloat;
            }
        }

        [TestMethod]
        public void StaticReadonlyScalarFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new StaticReadonlyScalarFieldAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.ReadonlyFloat) < 0.0001f);
        }

        private struct StaticScalarFieldAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticFieldsContainer.StaticFloat;
            }
        }

        [TestMethod]
        public void StaticScalarFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new StaticScalarFieldAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.StaticFloat) < 0.0001f);
        }

        private struct StaticScalarFieldRepeatedAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticFieldsContainer.StaticFloat + StaticFieldsContainer.StaticFloat;
            }
        }

        [TestMethod]
        public void StaticScalarFieldRepeatedAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new StaticScalarFieldRepeatedAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.StaticFloat * 2) < 0.0001f);
        }

        private struct StaticReadonlyVectorFieldAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                if (ids.X == 0) B[0] = StaticFieldsContainer.ReadonlyVector2.X;
                else B[1] = StaticFieldsContainer.ReadonlyVector2.Y;
            }
        }

        [TestMethod]
        public void StaticReadonlyVectorFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            var shader = new StaticReadonlyVectorFieldAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(2, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.ReadonlyVector2.X) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - StaticFieldsContainer.ReadonlyVector2.Y) < 0.0001f);
        }

        private struct StaticVectorFieldAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                if (ids.X == 0) B[0] = StaticFieldsContainer.StaticVector2.X;
                else B[1] = StaticFieldsContainer.StaticVector2.Y;
            }
        }

        [TestMethod]
        public void StaticVectorFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            var shader = new StaticVectorFieldAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(2, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticFieldsContainer.StaticVector2.X) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - StaticFieldsContainer.StaticVector2.Y) < 0.0001f);
        }

        private struct StaticReadOnlyBufferFieldAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                if (ids.X == 0) B[0] = StaticFieldsContainer.StaticBuffer[0];
                else B[1] = StaticFieldsContainer.StaticBuffer[1];
            }
        }

        [TestMethod]
        public void StaticReadOnlyBufferFieldAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            var shader = new StaticReadOnlyBufferFieldAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(2, shader);

            float[] result = buffer.GetData();
            float[] expected = StaticFieldsContainer.StaticBuffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - expected[0]) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - expected[1]) < 0.0001f);
        }
    }
}
