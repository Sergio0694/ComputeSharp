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
    }
}
