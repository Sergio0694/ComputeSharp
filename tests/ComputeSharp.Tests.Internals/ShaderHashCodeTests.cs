using System;
using ComputeSharp.Shaders.Translation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Internals
{
    [TestClass]
    [TestCategory("ShaderHashCode")]
    public class ShaderHashCodeTests
    {
        public struct Shader1 : IComputeShader
        {
            public float A;
            public ReadWriteBuffer<float> B;

            public void Execute()
            {
                B[0] = A;
            }
        }

        [TestMethod]
        public void ShaderWithNoCapturedDelegates()
        {
            float value = 10;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Shader1 shader1 = new() { A = value, B = buffer };

            int
                hash1 = ShaderHashCodeProvider.GetHashCode(shader1),
                hash2 = ShaderHashCodeProvider.GetHashCode(shader1);

            Assert.IsTrue(hash1 == hash2);

            Shader1 shader2 = new() { A = value, B = buffer };

            int hash3 = ShaderHashCodeProvider.GetHashCode(shader2);

            Assert.IsTrue(hash1 == hash3);
        }

        public struct Shader2 : IComputeShader
        {
            public float A;
            public ReadWriteBuffer<float> B;
            public Func<float, float> F;

            public void Execute()
            {
                B[0] = F(A);
            }
        }

        [TestMethod]
        public void ShaderWithCapturedDelegates()
        {
            Func<float, float> f = static x => x * x;

            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Shader2 shader1 = new() { A = 1, B = buffer, F = f };

            int
                hash1 = ShaderHashCodeProvider.GetHashCode(shader1),
                hash2 = ShaderHashCodeProvider.GetHashCode(shader1);

            Assert.IsTrue(hash1 == hash2);

            f = static x => x + 1;

            Shader2 shader2 = new() { A = 1, B = buffer, F = f };

            int hash3 = ShaderHashCodeProvider.GetHashCode(shader2);

            Assert.IsFalse(hash1 == hash3);
        }
    }
}
