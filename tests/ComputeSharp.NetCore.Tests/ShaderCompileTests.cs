using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.NetCore.Tests
{
    [TestClass]
    [TestCategory("ShaderCompile")]
    public class ShaderTests
    {
        [TestMethod]
        public void EmptyShaderCompileError()
        {
            Action<ThreadIds> action = id => { };

            Assert.ThrowsException<InvalidOperationException>(() => Gpu.Default.For(10, action));
        }
    }
}
