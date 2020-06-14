using System;
using System.Linq;
using System.Numerics;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("ShaderClosure")]
    public class ShaderClosureTests
    {
        private struct LocalScalarAssignToBuffer_Shader<T> : IComputeShader where T : unmanaged
        {
            public T Value;
            public T Zero;
            public ReadWriteBuffer<T> B;

            public void Execute(ThreadIds ids)
            {
                if (ids.X % 2 == 0) B[ids.X] = Value;
                else B[ids.X] = Zero;
            }
        }

        private static void LocalScalarAssignToBuffer<T>(T value, Func<T, T, bool> equals) where T : unmanaged
        {
            using ReadWriteBuffer<T> buffer = Gpu.Default.AllocateReadWriteBuffer<T>(4);

            T zero = default;

            var shader = new LocalScalarAssignToBuffer_Shader<T>
            {
                Value = value,
                Zero = zero,
                B = buffer
            };

            Gpu.Default.For(4, shader);

            T[] result = buffer.GetData();

            Assert.IsTrue(equals(result[0], value));
            Assert.IsFalse(equals(result[1], value));
            Assert.IsTrue(equals(result[2], value));
            Assert.IsFalse(equals(result[3], value));
        }

        [TestMethod]
        public void LocalScalarAssignToBuffer()
        {
            LocalScalarAssignToBuffer(10, (a, b) => a == b);
            LocalScalarAssignToBuffer(3.14f, (a, b) => MathF.Abs(a - b) < 0.001f);
            LocalScalarAssignToBuffer(6.28, (a, b) => Math.Abs(a - b) < 0.001f);
            LocalScalarAssignToBuffer<Bool>(true, (a, b) => a == b);
        }

        private struct ShaderWithBoolType_Shader : IComputeShader
        {
            public ReadWriteBuffer<Bool> B;
            public ReadWriteBuffer<int> I;

            public void Execute(ThreadIds ids)
            {
                if (B[ids.X]) I[ids.X] = ids.X + 1;
                B[ids.X] = (ids.X % 2) == 0;
            }
        }

        [TestMethod]
        public void ShaderWithBoolType()
        {
            using ReadWriteBuffer<Bool> bools = Gpu.Default.AllocateReadWriteBuffer(new Bool[] { true, false, true, true });
            using ReadWriteBuffer<int> ints = Gpu.Default.AllocateReadWriteBuffer<int>(4);

            var shader = new ShaderWithBoolType_Shader { B = bools, I = ints };

            Gpu.Default.For(4, shader);

            Bool[] boolResults = bools.GetData();

            Assert.IsTrue(boolResults[0]);
            Assert.IsFalse(boolResults[1]);
            Assert.IsTrue(boolResults[2]);
            Assert.IsFalse(boolResults[3]);

            int[] intResults = ints.GetData();

            Assert.IsTrue(intResults[0] == 1);
            Assert.IsTrue(intResults[1] == 0);
            Assert.IsTrue(intResults[2] == 3);
            Assert.IsTrue(intResults[3] == 4);
        }

        private struct LocalKnownVectorAssignToBuffer_Shader : IComputeShader
        {
            public Vector4 V4;
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                if (ids.X == 0) B[0] = V4.X;
                else if (ids.X == 1) B[1] = V4.Y;
                else if (ids.X == 2) B[2] = V4.Z;
                else if (ids.X == 3) B[3] = V4.W;
            }
        }

        [TestMethod]
        public void LocalKnownVectorAssignToBuffer()
        {
            Vector4 vector4 = new Vector4(1, 2, 3, 4);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(4);

            var shader = new LocalKnownVectorAssignToBuffer_Shader { V4 = vector4, B = buffer };

            Gpu.Default.For(4, shader);

            float[] result = buffer.GetData();
            float[] expected = { 1, 2, 3, 4 };

            Assert.IsTrue(result.AsSpan().ContentEquals(expected));
        }

        private struct CopyBetweenConstantAndWriteableBuffers_Shader : IComputeShader
        {
            public ConstantBuffer<float> C;
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[ids.X] = C[ids.X];
            }
        }

        [TestMethod]
        public void CopyBetweenConstantAndWriteableBuffers()
        {
            float[] source = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            using ConstantBuffer<float> input = Gpu.Default.AllocateConstantBuffer(source);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(source.Length);

            var shader = new CopyBetweenConstantAndWriteableBuffers_Shader { C = input, B = buffer };

            Gpu.Default.For(source.Length, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(result.AsSpan().ContentEquals(source));
        }

        private struct CopyBetweenReadOnlyAndWriteableBuffers_Shader : IComputeShader
        {
            public ReadOnlyBuffer<float> IN;
            public ReadWriteBuffer<float> OUT;

            public void Execute(ThreadIds ids)
            {
                OUT[ids.X] = IN[ids.X];
            }
        }

        [TestMethod]
        public void CopyBetweenReadOnlyAndWriteableBuffers()
        {
            float[] source = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            using ReadOnlyBuffer<float> input = Gpu.Default.AllocateReadOnlyBuffer(source);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(source.Length);

            var shader = new CopyBetweenReadOnlyAndWriteableBuffers_Shader { IN = input, OUT = buffer };

            Gpu.Default.For(source.Length, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(result.AsSpan().ContentEquals(source));
        }

        private struct CopyBetweenWriteableBuffers_Shader : IComputeShader
        {
            public ReadWriteBuffer<float> IN;
            public ReadWriteBuffer<float> OUT;

            public void Execute(ThreadIds ids)
            {
                OUT[ids.X] = IN[ids.X];
            }
        }

        [TestMethod]
        public void CopyBetweenWriteableBuffers()
        {
            float[] source = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            using ReadWriteBuffer<float> input = Gpu.Default.AllocateReadWriteBuffer(source);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(source.Length);

            var shader = new CopyBetweenWriteableBuffers_Shader { IN = input, OUT = buffer };

            Gpu.Default.For(source.Length, shader);

            float[] result = buffer.GetData();

            Assert.IsTrue(result.AsSpan().ContentEquals(source));
        }
    }
}
