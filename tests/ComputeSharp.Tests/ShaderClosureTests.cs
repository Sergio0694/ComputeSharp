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
        private static void LocalScalarAssignToBuffer<T>(T value, Func<T, T, bool> equals) where T : unmanaged
        {
            using ReadWriteBuffer<T> buffer = Gpu.Default.AllocateReadWriteBuffer<T>(4);

            T zero = default;

            Action<ThreadIds> action = id =>
            {
                if (id.X % 2 == 0) buffer[id.X] = value;
                else buffer[id.X] = zero;
            };

            Gpu.Default.For(4, action);

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

        [TestMethod]
        public void ShaderWithBoolType()
        {
            using ReadWriteBuffer<Bool> bools = Gpu.Default.AllocateReadWriteBuffer(new Bool[] { true, false, true, true });
            using ReadWriteBuffer<int> ints = Gpu.Default.AllocateReadWriteBuffer<int>(4);

            void Foo(ThreadIds id)
            {
                if (bools[id.X]) ints[id.X] = id.X + 1;
                bools[id.X] = (id.X % 2) == 0;
            }

            Gpu.Default.For(4, Foo);

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

        [TestMethod]
        public void LocalScalarAssignToBufferWithinLocalMethod()
        {
            float value = 10;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            void Foo(ThreadIds id) => buffer[0] = value;

            Gpu.Default.For(1, Foo);

            float[] result = buffer.GetData();

            Assert.IsTrue((int)result[0] == (int)value);
        }

        [TestMethod]
        public void LocalKnownVectorAssignToBuffer()
        {
            Vector4 vector4 = new Vector4(1, 2, 3, 4);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(4);

            Action<ThreadIds> action = id =>
            {
                if (id.X == 0) buffer[0] = vector4.X;
                else if (id.X == 1) buffer[1] = vector4.Y;
                else if (id.X == 2) buffer[2] = vector4.Z;
                else if (id.X == 3) buffer[3] = vector4.W;
            };

            Gpu.Default.For(4, action);

            float[] result = buffer.GetData();
            float[] expected = { 1, 2, 3, 4 };

            Assert.IsTrue(result.AsSpan().ContentEquals(expected));
        }

        [TestMethod]
        public void CopyBetweenConstantAndWriteableBuffers()
        {
            float[] source = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            using ConstantBuffer<float> input = Gpu.Default.AllocateConstantBuffer(source);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(source.Length);

            Action<ThreadIds> action = id => buffer[id.X] = input[id.X];

            Gpu.Default.For(source.Length, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(result.AsSpan().ContentEquals(source));
        }

        [TestMethod]
        public void CopyBetweenReadOnlyAndWriteableBuffers()
        {
            float[] source = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            using ReadOnlyBuffer<float> input = Gpu.Default.AllocateReadOnlyBuffer(source);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(source.Length);

            Action<ThreadIds> action = id => buffer[id.X] = input[id.X];

            Gpu.Default.For(source.Length, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(result.AsSpan().ContentEquals(source));
        }

        [TestMethod]
        public void CopyBetweenWriteableBuffers()
        {
            float[] source = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

            using ReadWriteBuffer<float> input = Gpu.Default.AllocateReadWriteBuffer(source);
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(source.Length);

            Action<ThreadIds> action = id => buffer[id.X] = input[id.X];

            Gpu.Default.For(source.Length, action);

            float[] result = buffer.GetData();

            Assert.IsTrue(result.AsSpan().ContentEquals(source));
        }

        [TestMethod]
        public void CopyBetweenNestedClosuresEasy()
        {
            int value1 = 1;
            using (ReadWriteBuffer<int> buffer1 = Gpu.Default.AllocateReadWriteBuffer<int>(1))
            {
                int value2 = 2;
                Action<ThreadIds> action = id =>
                {
                    int sum = value1 + value2;
                    buffer1[0] = sum;
                };

                Gpu.Default.For(1, action);

                int[] result1 = buffer1.GetData();

                Assert.IsTrue(result1[0] == value1 + value2);
            }
        }

        [TestMethod]
        public void CopyBetweenNestedClosuresHard()
        {
            int value1 = 1;
            using (ReadWriteBuffer<int> buffer1 = Gpu.Default.AllocateReadWriteBuffer<int>(1))
            {
                int value2 = 2;
                using (ReadWriteBuffer<int> buffer2 = Gpu.Default.AllocateReadWriteBuffer<int>(1))
                {
                    int value3 = 3;
                    using (ReadWriteBuffer<int> buffer3 = Gpu.Default.AllocateReadWriteBuffer<int>(1))
                    {
                        int value4 = 4;
                        Action<ThreadIds> action = id =>
                        {
                            int sum = value1 + value2 + value3 + value4;
                            buffer1[0] = buffer2[0] = buffer3[0] = sum;
                        };

                        Gpu.Default.For(1, action);

                        int[] result1 = buffer1.GetData();
                        int[] result2 = buffer2.GetData();
                        int[] result3 = buffer3.GetData();

                        Assert.IsTrue(result1[0] == value1 + value2 + value3 + value4);
                        Assert.IsTrue(result1[0] == result2[0]);
                        Assert.IsTrue(result1[0] == result3[0]);
                    }
                }
            }
        }
    }
}
