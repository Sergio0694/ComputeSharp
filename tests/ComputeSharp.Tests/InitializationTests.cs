using System;
using System.Linq;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
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
            Assert.IsTrue(Gpu.Default is not null);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void DeviceInfo(Device device)
        {
            GraphicsDevice gpu = device.Get();

            Assert.IsTrue(gpu.Luid != default);
            Assert.IsTrue(gpu.Name is { Length: > 0 });
            Assert.IsTrue(gpu.ComputeUnits != 0);
            Assert.IsTrue(gpu.WavefrontSize != 0);
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

                int[] data = buffer.ToArray();

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

                int[] data = buffer.ToArray();

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
