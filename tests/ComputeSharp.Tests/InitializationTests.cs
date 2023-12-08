using System;
using System.Linq;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class InitializationTests
{
    [TestMethod]
    public void IsSupported()
    {
        Assert.IsTrue(GraphicsDevice.GetDefault() is not null);
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
    public void EnumerateDevices()
    {
        int i = 0;

        foreach (GraphicsDevice device in GraphicsDevice.EnumerateDevices())
        {
            if (i++ == 0)
            {
                Assert.AreSame(GraphicsDevice.GetDefault(), device);
            }

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

        foreach (GraphicsDevice device in GraphicsDevice.QueryDevices(info => info.DedicatedMemorySize >= 1024))
        {
            if (i++ == 0)
            {
                Assert.AreSame(GraphicsDevice.GetDefault(), device);
            }

            using ReadWriteBuffer<int> buffer = device.AllocateReadWriteBuffer<int>(128);

            device.For(128, new SampleShader(buffer));

            int[] data = buffer.ToArray();

            Assert.IsTrue(data.SequenceEqual(Enumerable.Range(0, 128)));
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct SampleShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.X] = ThreadIds.X;
        }
    }
}