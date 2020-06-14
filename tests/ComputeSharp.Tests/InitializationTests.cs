using System;
using System.Linq;
using ComputeSharp.Graphics;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Initialization")]
    public class InitializationTests
    {
        [TestMethod]
        public void CheckIsSupportedAndGetDefaultDevice()
        {
            if (Gpu.IsSupported) Assert.IsNotNull(Gpu.Default);
            else Assert.ThrowsException<NotSupportedException>(() => Gpu.Default);
        }

        private struct RunShaderOnAllDevicesShader : IComputeShader
        {
            public ReadWriteBuffer<float> B;

            public void Execute(ThreadIds ids)
            {
                B[ids.X] = ids.X;
            }
        }

        [TestMethod]
        public void RunShaderOnAllDevices()
        {
            foreach (GraphicsDevice gpu in Gpu.EnumerateDevices())
            {
                using (ReadWriteBuffer<float> buffer = gpu.AllocateReadWriteBuffer<float>(100))
                {
                    var shader = new RunShaderOnAllDevicesShader { B = buffer };

                    gpu.For(100, shader);

                    float[] array = buffer.GetData();
                    float[] expected = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

                    Assert.IsTrue(array.AsSpan().ContentEquals(expected));
                }

                gpu.Dispose();
            }
        }
    }
}
