using System;
using System.Linq;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("Buffer")]
    public partial class BufferTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ConstantBuffer<>))]
        [Resource(typeof(ReadOnlyBuffer<>))]
        [Resource(typeof(ReadWriteBuffer<>))]
        [Data(AllocationMode.Default)]
        [Data(AllocationMode.Clear)]
        public void Allocate_Uninitialized_Ok(Device device, Type bufferType, AllocationMode allocationMode)
        {
            using Buffer<float> buffer = device.Get().AllocateBuffer<float>(bufferType, 128, allocationMode);

            Assert.IsNotNull(buffer);
            Assert.AreEqual(buffer.Length, 128);
            Assert.AreSame(buffer.GraphicsDevice, device.Get());

            if (allocationMode == AllocationMode.Clear)
            {
                foreach (float x in buffer.ToArray())
                {
                    Assert.AreEqual(x, 0f);
                }
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ConstantBuffer<>))]
        [Resource(typeof(ReadOnlyBuffer<>))]
        [Resource(typeof(ReadWriteBuffer<>))]
        [Data(-247824)]
        [Data(-1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Device device, Type bufferType, int length)
        {
            using Buffer<float> buffer = device.Get().AllocateBuffer<float>(bufferType, length);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ConstantBuffer<>))]
        [Resource(typeof(ReadOnlyBuffer<>))]
        [Resource(typeof(ReadWriteBuffer<>))]
        public void Allocate_FromArray(Device device, Type bufferType)
        {
            float[] data = Enumerable.Range(0, 128).Select(static i => (float)i).ToArray();

            using Buffer<float> buffer = device.Get().AllocateBuffer(bufferType, data);

            Assert.IsNotNull(buffer);
            Assert.AreEqual(buffer.Length, 128);
            Assert.AreSame(buffer.GraphicsDevice, device.Get());

            float[] result = buffer.ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(data.Length, result.Length);
            Assert.IsTrue(data.SequenceEqual(result));
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ConstantBuffer<>))]
        [Resource(typeof(ReadOnlyBuffer<>))]
        [Resource(typeof(ReadWriteBuffer<>))]
        [Data(typeof(ConstantBuffer<>))]
        [Data(typeof(ReadOnlyBuffer<>))]
        [Data(typeof(ReadWriteBuffer<>))]
        public void Allocate_FromBuffer(Device device, Type sourceType, Type destinationType)
        {
            float[] data = Enumerable.Range(0, 128).Select(static i => (float)i).ToArray();

            using Buffer<float> source = device.Get().AllocateBuffer(sourceType, data);
            using Buffer<float> destination = device.Get().AllocateBuffer(destinationType, source);

            Assert.IsNotNull(destination);
            Assert.AreEqual(destination.Length, 128);
            Assert.AreSame(destination.GraphicsDevice, device.Get());

            float[] result = destination.ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(data.Length, result.Length);
            Assert.IsTrue(data.SequenceEqual(result));
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ConstantBuffer<>))]
        [Resource(typeof(ReadOnlyBuffer<>))]
        [Resource(typeof(ReadWriteBuffer<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Device device, Type bufferType)
        {
            using Buffer<float> buffer = device.Get().AllocateBuffer<float>(bufferType, 128);

            buffer.Dispose();

            _ = buffer.ToArray();
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ConstantBuffer<>))]
        [Resource(typeof(ReadOnlyBuffer<>))]
        [Resource(typeof(ReadWriteBuffer<>))]
        [Data(0, 4096)]
        [Data(128, 512)]
        [Data(2048, 2048)]
        public void CopyTo_RangeToArray_Ok(Device device, Type bufferType, int offset, int count)
        {
            float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

            using Buffer<float> buffer = device.Get().AllocateBuffer(bufferType, array);

            float[] result = buffer.ToArray(offset, count);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Length, count);
            Assert.IsTrue(array.AsSpan(offset, count).SequenceEqual(result));
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ConstantBuffer<>))]
        [Resource(typeof(ReadOnlyBuffer<>))]
        [Resource(typeof(ReadWriteBuffer<>))]
        [Data(-1, 128)]
        [Data(8192, 128)]
        [Data(512, 4096)]
        [Data(512, -128)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_RangeToArray_Fail(Device device, Type bufferType, int offset, int count)
        {
            float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

            using Buffer<float> buffer = device.Get().AllocateBuffer(bufferType, array);

            _ = buffer.ToArray(offset, count);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ConstantBuffer<>))]
        [Resource(typeof(ReadOnlyBuffer<>))]
        [Resource(typeof(ReadWriteBuffer<>))]
        [Data(0, 0, 4096)]
        [Data(512, 0, 2048)]
        [Data(0, 512, 2048)]
        [Data(127, 1024, 587)]
        public void CopyTo_RangeToVoid_Ok(Device device, Type bufferType, int destinationOffset, int bufferOffset, int count)
        {
            float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

            using Buffer<float> buffer = device.Get().AllocateBuffer(bufferType, array);

            float[] result = new float[4096];

            buffer.CopyTo(result, destinationOffset, bufferOffset, count);

            Assert.IsTrue(array.AsSpan(bufferOffset, count).SequenceEqual(result.AsSpan(destinationOffset, count)));
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(ConstantBuffer<>))]
        [Resource(typeof(ReadOnlyBuffer<>))]
        [Resource(typeof(ReadWriteBuffer<>))]
        [Data(0, 0, 8196)]
        [Data(-12, 0, 1024)]
        [Data(0, -56, 1024)]
        [Data(512, 0, 4096)]
        [Data(12, 1024, 3600)]
        [Data(12, 1024, -2096)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_RangeToVoid_Fail(Device device, Type bufferType, int destinationOffset, int bufferOffset, int count)
        {
            float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

            using Buffer<float> buffer = device.Get().AllocateBuffer(bufferType, array);

            float[] result = new float[4096];

            buffer.CopyTo(result, destinationOffset, bufferOffset, count);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Dispatch_ConstantBuffer(Device device)
        {
            int[] data = Enumerable.Range(0, 1024).ToArray();

            using ConstantBuffer<int> source = device.Get().AllocateConstantBuffer(data);
            using ReadWriteBuffer<int> destination = device.Get().AllocateReadWriteBuffer<int>(data.Length);

            device.Get().For(source.Length, new ConstantBufferKernel(source, destination));

            int[] result = destination.ToArray();

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        internal readonly partial struct ConstantBufferKernel : IComputeShader
        {
            public readonly ConstantBuffer<int> source;
            public readonly ReadWriteBuffer<int> destination;

            public void Execute()
            {
                destination[ThreadIds.X] = source[ThreadIds.X];
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Dispatch_ReadOnlyBuffer(Device device)
        {
            int[] data = Enumerable.Range(0, 1024).ToArray();

            using ReadOnlyBuffer<int> source = device.Get().AllocateReadOnlyBuffer(data);
            using ReadWriteBuffer<int> destination = device.Get().AllocateReadWriteBuffer<int>(data.Length);

            device.Get().For(source.Length, new ReadOnlyBufferKernel(source, destination));

            int[] result = destination.ToArray();

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        internal readonly partial struct ReadOnlyBufferKernel : IComputeShader
        {
            public readonly ReadOnlyBuffer<int> source;
            public readonly ReadWriteBuffer<int> destination;

            public void Execute()
            {
                destination[ThreadIds.X] = source[ThreadIds.X];
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Dispatch_ReadWriteBuffer(Device device)
        {
            int[] data = Enumerable.Range(0, 1024).ToArray();

            using ReadWriteBuffer<int> source = device.Get().AllocateReadWriteBuffer(data);
            using ReadWriteBuffer<int> destination = device.Get().AllocateReadWriteBuffer<int>(data.Length);

            device.Get().For(source.Length, new ReadWriteBufferKernel(source, destination));

            int[] result = destination.ToArray();

            CollectionAssert.AreEqual(data, result);
        }

        [AutoConstructor]
        internal readonly partial struct ReadWriteBufferKernel : IComputeShader
        {
            public readonly ReadWriteBuffer<int> source;
            public readonly ReadWriteBuffer<int> destination;

            public void Execute()
            {
                destination[ThreadIds.X] = source[ThreadIds.X];
            }
        }
    }
}
