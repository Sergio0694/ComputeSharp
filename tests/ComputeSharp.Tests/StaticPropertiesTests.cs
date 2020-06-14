using System;
using System.Numerics;
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

        /// <summary>
        /// A <see langword="static"/> <see cref="Func{T,TResult}"/> property
        /// </summary>
        public static Func<float, float> SquareFunc { get; } = x => x * x;
    }

    [TestClass]
    [TestCategory("StaticProperties")]
    public class StaticPropertiesTests
    {
        private struct StaticReadonlyScalarPropertyAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticPropertiesContainer.ReadonlyFloat;
            }
        }

        [TestMethod]
        public void StaticReadonlyScalarPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new StaticReadonlyScalarPropertyAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.ReadonlyFloat) < 0.0001f);
        }

        private struct StaticScalarPropertyAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticPropertiesContainer.StaticFloat;
            }
        }

        [TestMethod]
        public void StaticScalarPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new StaticScalarPropertyAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.StaticFloat) < 0.0001f);
        }

        private struct StaticScalarPropertyRepeatedAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = StaticPropertiesContainer.StaticFloat + StaticPropertiesContainer.StaticFloat;
            }
        }

        [TestMethod]
        public void StaticScalarPropertyRepeatedAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            var shader = new StaticScalarPropertyRepeatedAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.StaticFloat * 2) < 0.0001f);
        }

        private struct StaticReadonlyVectorPropertyAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                if (ids.X == 0) B[0] = StaticPropertiesContainer.ReadonlyVector2.X;
                else B[1] = StaticPropertiesContainer.ReadonlyVector2.Y;
            }
        }

        [TestMethod]
        public void StaticReadonlyVectorPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            var shader = new StaticReadonlyVectorPropertyAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(2, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.ReadonlyVector2.X) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - StaticPropertiesContainer.ReadonlyVector2.Y) < 0.0001f);
        }

        private struct StaticVectorPropertyAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                if (ids.X == 0) B[0] = StaticPropertiesContainer.StaticVector2.X;
                else B[1] = StaticPropertiesContainer.StaticVector2.Y;
            }
        }

        [TestMethod]
        public void StaticVectorPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            var shader = new StaticVectorPropertyAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(2, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - StaticPropertiesContainer.StaticVector2.X) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - StaticPropertiesContainer.StaticVector2.Y) < 0.0001f);
        }

        private struct StaticReadOnlyBufferPropertyAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                if (ids.X == 0) B[0] = StaticPropertiesContainer.StaticBuffer[0];
                else B[1] = StaticPropertiesContainer.StaticBuffer[1];
            }
        }

        [TestMethod]
        public void StaticReadOnlyBufferPropertyAssignToBuffer()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(2);

            var shader = new StaticReadOnlyBufferPropertyAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(2, shader);

            float[] result = buffer.GetData();
            float[] expected = StaticPropertiesContainer.StaticBuffer.GetData();

            Assert.IsTrue(MathF.Abs(result[0] - expected[0]) < 0.0001f);
            Assert.IsTrue(MathF.Abs(result[1] - expected[1]) < 0.0001f);
        }

        public static int SomeNumber = 7;

        private struct StaticLocalScalarFieldAssignToBuffer_Shader : IComputeShader
        {
            public ReadWriteBuffer<int> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = SomeNumber;
            }
        }

        [TestMethod]
        public void StaticLocalScalarFieldAssignToBuffer()
        {
            using ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer<int>(1);

            var shader = new StaticLocalScalarFieldAssignToBuffer_Shader { B = buffer };

            Gpu.Default.For(1, shader);

            int[] result = buffer.GetData();

            Assert.IsTrue(result[0] == SomeNumber);
        }
    }
}
