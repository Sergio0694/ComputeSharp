using System;
using System.Linq;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class TransferBufferTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadBuffer<>))]
    [Resource(typeof(ReadBackBuffer<>))]
    [Data(AllocationMode.Default)]
    [Data(AllocationMode.Clear)]
    public unsafe void Allocate_Uninitialized_Ok(Device device, Type bufferType, AllocationMode allocationMode)
    {
        using TransferBuffer<float> buffer = device.Get().AllocateTransferBuffer<float>(bufferType, 128, allocationMode);

        Assert.IsNotNull(buffer);
        Assert.AreEqual(buffer.Length, 128);
        Assert.AreEqual(buffer.Memory.Length, 128);
        Assert.AreEqual(buffer.Span.Length, 128);
        Assert.IsTrue(Unsafe.AreSame(ref buffer.Memory.Span[0], ref buffer.Span[0]));
        Assert.IsTrue(Unsafe.AreSame(ref *(float*)buffer.Memory.Pin().Pointer, ref buffer.Span[0]));
        Assert.AreSame(buffer.GraphicsDevice, device.Get());

        if (allocationMode == AllocationMode.Clear)
        {
            foreach (float x in buffer.Span)
            {
                Assert.AreEqual(x, 0f);
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadBuffer<>))]
    [Resource(typeof(ReadBackBuffer<>))]
    [Data(-247824)]
    [Data(-1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Allocate_Uninitialized_Fail(Device device, Type bufferType, int length)
    {
        using TransferBuffer<float> buffer = device.Get().AllocateTransferBuffer<float>(bufferType, length);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadBuffer<>))]
    [Resource(typeof(ReadBackBuffer<>))]
    [ExpectedException(typeof(ObjectDisposedException))]
    public void UsageAfterDispose(Device device, Type bufferType)
    {
        using TransferBuffer<float> buffer = device.Get().AllocateTransferBuffer<float>(bufferType, 128);

        buffer.Dispose();

        _ = buffer.Span;
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Allocate_UploadBuffer_CopyTo_Full(Device device)
    {
        using UploadBuffer<int> uploadBuffer = device.Get().AllocateUploadBuffer<int>(4096);

        new Random(42).NextBytes(uploadBuffer.Span.AsBytes());

        using ReadOnlyBuffer<int> readOnlyBuffer = device.Get().AllocateReadOnlyBuffer<int>(uploadBuffer.Length);

        uploadBuffer.CopyTo(readOnlyBuffer);

        int[] result = readOnlyBuffer.ToArray();

        Assert.AreEqual(uploadBuffer.Length, result.Length);
        Assert.IsTrue(uploadBuffer.Span.SequenceEqual(result));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(0, 0, 4096)]
    [Data(0, 128, 2048)]
    [Data(128, 0, 2048)]
    [Data(33, 97, 512)]
    public void Allocate_UploadBuffer_CopyTo_Range(Device device, int sourceOffset, int destinationOffset, int count)
    {
        using UploadBuffer<int> uploadBuffer = device.Get().AllocateUploadBuffer<int>(4096);

        new Random(42).NextBytes(uploadBuffer.Span.AsBytes());

        using ReadOnlyBuffer<int> readOnlyBuffer = device.Get().AllocateReadOnlyBuffer<int>(uploadBuffer.Length);

        uploadBuffer.CopyTo(readOnlyBuffer, sourceOffset, destinationOffset, count);

        int[] result = readOnlyBuffer.ToArray(destinationOffset, count);

        Assert.AreEqual(result.Length, count);
        Assert.IsTrue(uploadBuffer.Span.Slice(sourceOffset, count).SequenceEqual(result));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Allocate_ReadBackBuffer_CopyTo_Full(Device device)
    {
        int[] source = new int[4096];

        new Random(42).NextBytes(source.AsSpan().AsBytes());

        using ReadOnlyBuffer<int> readOnlyBuffer = device.Get().AllocateReadOnlyBuffer(source);
        using ReadBackBuffer<int> readBackBuffer = device.Get().AllocateReadBackBuffer<int>(readOnlyBuffer.Length);

        readOnlyBuffer.CopyTo(readBackBuffer);

        Assert.AreEqual(source.Length, readBackBuffer.Length);
        Assert.IsTrue(source.AsSpan().SequenceEqual(readBackBuffer.Span));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(0, 0, 4096)]
    [Data(0, 128, 2048)]
    [Data(128, 0, 2048)]
    [Data(33, 97, 512)]
    public void Allocate_ReadBackBuffer_CopyTo_Range(Device device, int sourceOffset, int destinationOffset, int count)
    {
        int[] source = new int[4096];

        new Random(42).NextBytes(source.AsSpan().AsBytes());

        using ReadOnlyBuffer<int> readOnlyBuffer = device.Get().AllocateReadOnlyBuffer(source);
        using ReadBackBuffer<int> readBackBuffer = device.Get().AllocateReadBackBuffer<int>(readOnlyBuffer.Length);

        readOnlyBuffer.CopyTo(readBackBuffer, sourceOffset, destinationOffset, count);

        Assert.AreEqual(source.Length, readBackBuffer.Length);
        Assert.IsTrue(source.AsSpan(sourceOffset, count).SequenceEqual(readBackBuffer.Span.Slice(destinationOffset, count)));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Allocate_UploadBuffer_CopyFrom_Full(Device device)
    {
        using UploadBuffer<int> uploadBuffer = device.Get().AllocateUploadBuffer<int>(4096);

        new Random(42).NextBytes(uploadBuffer.Span.AsBytes());

        using ReadOnlyBuffer<int> readOnlyBuffer = device.Get().AllocateReadOnlyBuffer<int>(uploadBuffer.Length);

        readOnlyBuffer.CopyFrom(uploadBuffer);

        int[] result = readOnlyBuffer.ToArray();

        Assert.AreEqual(uploadBuffer.Length, result.Length);
        Assert.IsTrue(uploadBuffer.Span.SequenceEqual(result));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(0, 0, 4096)]
    [Data(0, 128, 2048)]
    [Data(128, 0, 2048)]
    [Data(33, 97, 512)]
    public void Allocate_UploadBuffer_CopyFrom_Range(Device device, int sourceOffset, int destinationOffset, int count)
    {
        using UploadBuffer<int> uploadBuffer = device.Get().AllocateUploadBuffer<int>(4096);

        new Random(42).NextBytes(uploadBuffer.Span.AsBytes());

        using ReadOnlyBuffer<int> readOnlyBuffer = device.Get().AllocateReadOnlyBuffer<int>(uploadBuffer.Length);

        readOnlyBuffer.CopyFrom(uploadBuffer, sourceOffset, destinationOffset, count);

        int[] result = readOnlyBuffer.ToArray(destinationOffset, count);

        Assert.AreEqual(result.Length, count);
        Assert.IsTrue(uploadBuffer.Span.Slice(sourceOffset, count).SequenceEqual(result));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Allocate_ReadBackBuffer_CopyFrom_Full(Device device)
    {
        int[] source = new int[4096];

        new Random(42).NextBytes(source.AsSpan().AsBytes());

        using ReadOnlyBuffer<int> readOnlyBuffer = device.Get().AllocateReadOnlyBuffer(source);
        using ReadBackBuffer<int> readBackBuffer = device.Get().AllocateReadBackBuffer<int>(readOnlyBuffer.Length);

        readBackBuffer.CopyFrom(readOnlyBuffer);

        Assert.AreEqual(source.Length, readBackBuffer.Length);
        Assert.IsTrue(source.AsSpan().SequenceEqual(readBackBuffer.Span));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(0, 0, 4096)]
    [Data(0, 128, 2048)]
    [Data(128, 0, 2048)]
    [Data(33, 97, 512)]
    public void Allocate_ReadBackBuffer_CopyFrom_Range(Device device, int sourceOffset, int destinationOffset, int count)
    {
        int[] source = new int[4096];

        new Random(42).NextBytes(source.AsSpan().AsBytes());

        using ReadOnlyBuffer<int> readOnlyBuffer = device.Get().AllocateReadOnlyBuffer(source);
        using ReadBackBuffer<int> readBackBuffer = device.Get().AllocateReadBackBuffer<int>(readOnlyBuffer.Length);

        readBackBuffer.CopyFrom(readOnlyBuffer, sourceOffset, destinationOffset, count);

        Assert.AreEqual(source.Length, readBackBuffer.Length);
        Assert.IsTrue(source.AsSpan(sourceOffset, count).SequenceEqual(readBackBuffer.Span.Slice(destinationOffset, count)));
    }
}