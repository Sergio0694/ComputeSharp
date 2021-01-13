using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Initialization")]
    public partial class InitializationTests
    {
        [TestMethod]
        public void IsSupported()
        {
            Assert.IsTrue(Gpu.IsSupported);
        }

        [TestMethod]
        public void DefaultDeviceInfo()
        {
            Assert.IsTrue(Gpu.Default.Luid != default);
            Assert.IsTrue(Gpu.Default.Name is { Length: > 0 });

            if (Gpu.Default.IsHardwareAccelerated)
            {
                Assert.IsTrue(Gpu.Default.DedicatedMemorySize != 0);
            }
            else
            {
                Assert.IsTrue(Gpu.Default.SharedMemorySize != 0);
            }

            Assert.IsTrue(Gpu.Default.ComputeUnits != 0);
            Assert.IsTrue(Gpu.Default.WavefrontSize != 0);
        }

        [TestMethod]
        public void DisposeDefault()
        {
            using var before = Gpu.Default.AllocateReadOnlyBuffer<float>(128);

            Gpu.Default.Dispose();

            using var after = Gpu.Default.AllocateReadOnlyBuffer<float>(128);
        }

        [TestMethod]
        public void EnumerateDevices()
        {
            int i = 0;

            foreach (GraphicsDevice device in Gpu.EnumerateDevices())
            {
                if (i++ == 0) Assert.AreSame(Gpu.Default, device);

                using ReadWriteBuffer<int> buffer = device.AllocateReadWriteBuffer<int>(128);

                device.For(128, new SampleShader(buffer));

                int[] data = buffer.GetData();

                Assert.IsTrue(data.SequenceEqual(Enumerable.Range(0, 128)));
            }
        }

        [TestMethod]
        public void QueryDevices()
        {
            int i = 0;

            foreach (GraphicsDevice device in Gpu.QueryDevices(info => info.DedicatedMemorySize >= 1024))
            {
                if (i++ == 0) Assert.AreSame(Gpu.Default, device);

                using ReadWriteBuffer<int> buffer = device.AllocateReadWriteBuffer<int>(128);

                device.For(128, new SampleShader(buffer));

                int[] data = buffer.GetData();

                Assert.IsTrue(data.SequenceEqual(Enumerable.Range(0, 128)));
            }
        }

        [AutoConstructor]
        internal readonly partial struct SampleShader : IComputeShader
        {
            public readonly ReadWriteBuffer<int> buffer;

            public void Execute()
            {
                buffer[ThreadIds.X] = ThreadIds.X;
            }
        }
    }
}
