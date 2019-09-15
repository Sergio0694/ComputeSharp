using System;
using ComputeSharp.Shaders.Translation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Internals
{
    [TestClass]
    [TestCategory("ShaderHashCodes")]
    public class ShaderHashCodeTests
    {
        [TestMethod]
        public void ShaderWithNoCapturedDelegates()
        {
            float value = 10;
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Action<ThreadIds> action1 = id => buffer[0] = value;

            int
                hash1 = ShaderLoader.GetHashCode(action1),
                hash2 = ShaderLoader.GetHashCode(action1);

            Assert.IsTrue(hash1 == hash2);

            Action<ThreadIds> action2 = id => buffer[0] = value;

            int hash3 = ShaderLoader.GetHashCode(action2);

            Assert.IsFalse(hash1 == hash3);
        }

        [TestMethod]
        public void ShaderWithCapturedDelegates()
        {
            Func<float, float> f = x => x * x;

            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1);

            Action<ThreadIds> action = id => buffer[0] = f(2);

            int
                hash1 = ShaderLoader.GetHashCode(action),
                hash2 = ShaderLoader.GetHashCode(action);

            Assert.IsTrue(hash1 == hash2);

            f = x => x + 1;

            int hash3 = ShaderLoader.GetHashCode(action);

            Assert.IsFalse(hash1 == hash3);
        }
    }
}
