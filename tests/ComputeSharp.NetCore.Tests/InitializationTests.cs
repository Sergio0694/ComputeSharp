using System;
using System.Linq;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers;
using ComputeSharp.NetCore.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.NetCore.Tests
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

        [TestMethod]
        public void RunShaderOnAllDevices()
        {
            foreach (GraphicsDevice gpu in Gpu.EnumerateDevices())
            {
                using (ReadWriteBuffer<float> buffer = gpu.AllocateReadWriteBuffer<float>(100))
                {
                    Action<ThreadIds> action = id => buffer[id.X] = id.X;

                    gpu.For(100, action);

                    float[] array = buffer.GetData();
                    float[] expected = Enumerable.Range(0, 100).Select(i => (float)i).ToArray();

                    Assert.IsTrue(array.AsSpan().ContentEquals(expected));
                }

                gpu.Dispose();
            }
        }
    }
}
