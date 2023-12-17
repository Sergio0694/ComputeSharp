using System;
using System.Linq;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class BufferTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ConstantBuffer<>))]
    [Resource(typeof(ReadOnlyBuffer<>))]
    [Resource(typeof(ReadWriteBuffer<>))]
    [Data(AllocationMode.Default)]
    [Data(AllocationMode.Clear)]
    [AdditionalData(128)]
    [AdditionalData(768)]
    [AdditionalData(1024)]
    [AdditionalData(443)]
    public void Allocate_Uninitialized_Ok(Device device, Type bufferType, AllocationMode allocationMode, int size)
    {
        using Buffer<float> buffer = device.Get().AllocateBuffer<float>(bufferType, size, allocationMode);

        Assert.IsNotNull(buffer);
        Assert.AreEqual(buffer.Length, size);
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
    [Data(128)]
    [Data(768)]
    [Data(1024)]
    [Data(443)]
    public void Allocate_FromArray(Device device, Type bufferType, int size)
    {
        float[] data = Enumerable.Range(0, size).Select(static i => (float)i).ToArray();

        using Buffer<float> buffer = device.Get().AllocateBuffer(bufferType, data);

        Assert.IsNotNull(buffer);
        Assert.AreEqual(buffer.Length, size);
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
    [AdditionalResource(typeof(ConstantBuffer<>))]
    [AdditionalResource(typeof(ReadOnlyBuffer<>))]
    [AdditionalResource(typeof(ReadWriteBuffer<>))]
    [Data(128)]
    [Data(768)]
    [Data(1024)]
    [Data(443)]
    public void Allocate_FromBuffer(Device device, Type sourceType, Type destinationType, int size)
    {
        float[] data = Enumerable.Range(0, size).Select(static i => (float)i).ToArray();

        using Buffer<float> source = device.Get().AllocateBuffer(sourceType, data);
        using Buffer<float> destination = device.Get().AllocateBuffer(destinationType, source);

        Assert.IsNotNull(destination);
        Assert.AreEqual(destination.Length, size);
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
    [Data(0, 512, 2048)]
    [Data(512, 0, 2048)]
    [Data(1024, 127, 587)]
    [Data(65, 127, 587)]
    public void CopyTo_RangeToVoid_Ok(Device device, Type bufferType, int sourceOffset, int destinationOffset, int count)
    {
        float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

        using Buffer<float> buffer = device.Get().AllocateBuffer(bufferType, array);

        float[] result = new float[4096];

        buffer.CopyTo(result, sourceOffset, destinationOffset, count);

        Assert.IsTrue(array.AsSpan(sourceOffset, count).SequenceEqual(result.AsSpan(destinationOffset, count)));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ConstantBuffer<>))]
    [Resource(typeof(ReadOnlyBuffer<>))]
    [Resource(typeof(ReadWriteBuffer<>))]
    [Data(0, 0, 8196)]
    [Data(0, -12, 1024)]
    [Data(-56, 0, 1024)]
    [Data(0, 512, 4096)]
    [Data(1024, 12, 3600)]
    [Data(1024, 12, -2096)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyTo_RangeToVoid_Fail(Device device, Type bufferType, int sourceOffset, int destinationOffset, int count)
    {
        float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

        using Buffer<float> buffer = device.Get().AllocateBuffer(bufferType, array);

        float[] result = new float[4096];

        buffer.CopyTo(result, sourceOffset, destinationOffset, count);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ConstantBuffer<>))]
    [Resource(typeof(ReadOnlyBuffer<>))]
    [Resource(typeof(ReadWriteBuffer<>))]
    [AdditionalResource(typeof(ConstantBuffer<>))]
    [AdditionalResource(typeof(ReadOnlyBuffer<>))]
    [AdditionalResource(typeof(ReadWriteBuffer<>))]
    [Data(0, 0, 4096)]
    [Data(0, 512, 2048)]
    [Data(512, 0, 2048)]
    [Data(1024, 127, 587)]
    [Data(65, 127, 587)]
    public void CopyTo_BufferToVoid_Ok(Device device, Type sourceType, Type destinationType, int sourceOffset, int destinationOffset, int count)
    {
        float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

        using Buffer<float> source = device.Get().AllocateBuffer(sourceType, array);
        using Buffer<float> destination = device.Get().AllocateBuffer<float>(destinationType, 4096);

        source.CopyTo(destination, sourceOffset, destinationOffset, count);

        float[] result = destination.ToArray();

        Assert.IsTrue(array.AsSpan(sourceOffset, count).SequenceEqual(result.AsSpan(destinationOffset, count)));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ConstantBuffer<>))]
    [Resource(typeof(ReadOnlyBuffer<>))]
    [Resource(typeof(ReadWriteBuffer<>))]
    [AdditionalResource(typeof(ConstantBuffer<>))]
    [AdditionalResource(typeof(ReadOnlyBuffer<>))]
    [AdditionalResource(typeof(ReadWriteBuffer<>))]
    [Data(0, 0, 8196)]
    [Data(0, -12, 1024)]
    [Data(-56, 0, 1024)]
    [Data(0, 512, 4096)]
    [Data(1024, 12, 3600)]
    [Data(1024, 12, -2096)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyTo_BufferToVoid_Fail(Device device, Type sourceType, Type destinationType, int sourceOffset, int destinationOffset, int count)
    {
        float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

        using Buffer<float> source = device.Get().AllocateBuffer(sourceType, array);
        using Buffer<float> destination = device.Get().AllocateBuffer<float>(destinationType, 4096);

        source.CopyTo(destination, sourceOffset, destinationOffset, count);
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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ConstantBufferKernel : IComputeShader
    {
        public readonly ConstantBuffer<int> source;
        public readonly ReadWriteBuffer<int> destination;

        public void Execute()
        {
            this.destination[ThreadIds.X] = this.source[ThreadIds.X];
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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyBufferKernel : IComputeShader
    {
        public readonly ReadOnlyBuffer<int> source;
        public readonly ReadWriteBuffer<int> destination;

        public void Execute()
        {
            this.destination[ThreadIds.X] = this.source[ThreadIds.X];
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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteBufferKernel : IComputeShader
    {
        public readonly ReadWriteBuffer<int> source;
        public readonly ReadWriteBuffer<int> destination;

        public void Execute()
        {
            this.destination[ThreadIds.X] = this.source[ThreadIds.X];
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadWriteBuffer_DoublePrecision(Device device)
    {
        if (!device.Get().IsDoublePrecisionSupportAvailable())
        {
            Assert.Inconclusive();
        }

        double[] array = Enumerable.Range(0, 128).Select(static i => (double)i).ToArray();

        using ReadWriteBuffer<double> buffer = device.Get().AllocateReadWriteBuffer(array);

        device.Get().For(128, new DoublePrecisionSupportShader(buffer, 2.0));

        double[] result = buffer.ToArray();

        for (int i = 0; i < 128; i++)
        {
            Assert.IsTrue(Math.Abs(result[i] - ((array[i] * 2.0) + 3.14)) < 0.00001);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [RequiresDoublePrecisionSupport]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct DoublePrecisionSupportShader : IComputeShader
    {
        public readonly ReadWriteBuffer<double> buffer;
        public readonly double factor;

        /// <inheritdoc/>
        public void Execute()
        {
            this.buffer[ThreadIds.X] = (this.buffer[ThreadIds.X] * this.factor) + 3.14;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ConstantBuffer<>))]
    [Resource(typeof(ReadOnlyBuffer<>))]
    [Resource(typeof(ReadWriteBuffer<>))]
    [ExpectedException(typeof(UnsupportedDoubleOperationException))]
    public void Dispatch_Buffer_DoublePrecision_ThrowsExceptionIfUnsupported(Device device, Type resourceType)
    {
        if (device.Get().IsDoublePrecisionSupportAvailable())
        {
            Assert.Inconclusive();
        }

        using Buffer<double> buffer = device.Get().AllocateBuffer<double>(resourceType, 32);

        Assert.Fail();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadBuffer<>))]
    [Resource(typeof(ReadBackBuffer<>))]
    [ExpectedException(typeof(UnsupportedDoubleOperationException))]
    public void Dispatch_TransferBuffer_DoublePrecision_ThrowsExceptionIfUnsupported(Device device, Type resourceType)
    {
        if (device.Get().IsDoublePrecisionSupportAvailable())
        {
            Assert.Inconclusive();
        }

        using TransferBuffer<double> buffer = device.Get().AllocateTransferBuffer<double>(resourceType, 32);

        Assert.Fail();
    }
}