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
        private struct EmptyShaderCompileError_Shader : IComputeShader
        {
            public void Execute(ThreadIds ids) { }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmptyShaderCompileError()
        {
            var shader = default(EmptyShaderCompileError_Shader);

            Gpu.Default.For(10, shader);
        }

        private struct DeviceMismatchException_Shader : IComputeShader
        {
            public ReadWriteBuffer<int> B;

            public void Execute(ThreadIds ids)
            {
                B[0] = 1;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(GraphicsDeviceMismatchException))]
        public void DeviceMismatchException()
        {
            ReadWriteBuffer<int> buffer = Gpu.Default.AllocateReadWriteBuffer<int>(1);

            GraphicsDevice device = Gpu.EnumerateDevices().First();

            var shader = new DeviceMismatchException_Shader { B = buffer };

            device.For(1, shader);
        }
    }
}

