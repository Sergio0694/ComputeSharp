using System;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable IDE0062

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Metaprogramming")]
    public partial class MetaprogrammingTests
    {
        [AutoConstructor]
        internal readonly partial struct ActivationWithDelegateShader : IComputeShader
        {
            public readonly ReadWriteBuffer<int> buffer;
            public readonly Func<int, int> activation;

            public void Execute()
            {
                buffer[ThreadIds.X] = activation(ThreadIds.X);
            }
        }

        public static class Activations
        {
            [ShaderMethod]
            public static int Square(int x) => x * x;

            [ShaderMethod]
            public static int AddOne(int x) => x + 1;

            // Missing attribute
            public static int Identity(int x) => x;

            [ShaderMethod]
            public static int FunctionWithConstants(int x) => (int)(x + FourAndAHalf);

            [ShaderMethod]
            public static Position FunctionWithTypes(int x)
            {
                Point point = default;
                point.A = x;
                point.B = x * x;

                Position position = default;
                position.X = point.A;
                position.Y = point.B;

                return position;
            }

            public struct Point
            {
                public int A;
                public int B;
            }

            [ShaderMethod]
            public static int FunctionWithLocalFunctionAndStaticMethod(int x)
            {
                static int AddTwo(int x) => x + 2;

                return Square(3 + AddOne(x) + AddTwo(x));
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public unsafe void ActivationWithDelegate(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(100);

            device.Get().For(100, new ActivationWithDelegateShader(buffer, Activations.Square));

            int[] result = buffer.ToArray();

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i], Activations.Square(i));
            }

            device.Get().For(100, new ActivationWithDelegateShader(buffer, Activations.AddOne));

            result = buffer.ToArray();

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i], Activations.AddOne(i));
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void DelegateWithMissingAttributeOnMethod(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(100);

            device.Get().For(100, new ActivationWithDelegateShader(buffer, Activations.Identity));
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void DelegateWrappingNonStaticMethod(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(100);

            [ShaderMethod]
            int Dummy(int x) => x;

            device.Get().For(100, new ActivationWithDelegateShader(buffer, Dummy));
        }

        [AutoConstructor]
        internal readonly partial struct ActivationWithMultipleDelegatesShader : IComputeShader
        {
            public readonly ReadWriteBuffer<int> buffer;
            public readonly Func<int, int> f1;
            public readonly Func<int, int> f2;

            public void Execute()
            {
                buffer[ThreadIds.X] = f2(f1(ThreadIds.X));
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public unsafe void ActivationWithMultipleDelegates(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(100);

            device.Get().For(100, new ActivationWithMultipleDelegatesShader(buffer, Activations.Square, Activations.AddOne));

            int[] result = buffer.ToArray();

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i], Activations.AddOne(Activations.Square(i)));
            }
        }

        public const float FourAndAHalf = 4.5f;
        public const int MagicNumber = 42;

        [AutoConstructor]
        internal readonly partial struct DelegateWithConstantsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<int> buffer;
            public readonly Func<int, int> activation;

            public void Execute()
            {
                buffer[ThreadIds.X] = MagicNumber + activation(ThreadIds.X);
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public unsafe void DelegateWithConstants(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(100);

            device.Get().For(100, new DelegateWithConstantsShader(buffer, Activations.FunctionWithConstants));

            int[] result = buffer.ToArray();

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i], MagicNumber + Activations.FunctionWithConstants(i));
            }
        }

        public struct Position
        {
            public int X;
            public int Y;
        }

        [AutoConstructor]
        internal readonly partial struct DelegateWithTypesShader : IComputeShader
        {
            public readonly ReadWriteBuffer<Position> buffer;
            public readonly Func<int, Position> activation;

            public void Execute()
            {
                buffer[ThreadIds.X] = activation(ThreadIds.X);
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public unsafe void DelegateWithTypes(Device device)
        {
            using ReadWriteBuffer<Position> buffer = device.Get().AllocateReadWriteBuffer<Position>(100);

            device.Get().For(100, new DelegateWithTypesShader(buffer, Activations.FunctionWithTypes));

            Position[] result = buffer.ToArray();

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i].X, Activations.FunctionWithTypes(i).X);
                Assert.AreEqual(result[i].Y, Activations.FunctionWithTypes(i).Y);
            }
        }

        [AutoConstructor]
        internal readonly partial struct DelegateWithLocalFunctionsAndStaticMethodsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<int> buffer;
            public readonly Func<int, int> activation;

            public void Execute()
            {
                buffer[ThreadIds.X] = Activations.AddOne(ThreadIds.X) + activation(ThreadIds.X);
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public unsafe void DelegateWithLocalFunctionsAndStaticMethods(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(100);

            device.Get().For(100, new DelegateWithLocalFunctionsAndStaticMethodsShader(buffer, Activations.FunctionWithLocalFunctionAndStaticMethod));

            int[] result = buffer.ToArray();

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(result[i], Activations.AddOne(i) + Activations.FunctionWithLocalFunctionAndStaticMethod(i));
            }
        }
    }
}
