using System;
using System.Linq;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
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

        [TestMethod]
        public void DeviceMismatchException()
        {
            ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer<int>(1);

            GraphicsDevice device = Gpu.EnumerateDevices().First();

            Action<ThreadIds> action = id => buffer[0] = 1;

            Assert.ThrowsException<GraphicsDeviceMismatchException>(() => device.For(1, action));
        }
    }
}

