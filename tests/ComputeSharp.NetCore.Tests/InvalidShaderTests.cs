using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.NetCore.Tests
{
    [TestClass]
    [TestCategory("InvalidShader")]
    public class InvalidShaderTests
    {
        [TestMethod]
        public void EmptyShaderCompileError()
        {
            Action<ThreadIds> action = id => { };

            Assert.ThrowsException<InvalidOperationException>(() => Gpu.Default.For(10, action));
        }
    }
}

