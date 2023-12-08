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
public partial class TransferTexture1DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture1D<>))]
    [Resource(typeof(ReadBackTexture1D<>))]
    [Data(AllocationMode.Default)]
    [Data(AllocationMode.Clear)]
    public unsafe void Allocate_Uninitialized_Ok(Device device, Type textureType, AllocationMode allocationMode)
    {
        using TransferTexture1D<float> texture = device.Get().AllocateTransferTexture1D<float>(textureType, 256, allocationMode);

        Assert.IsNotNull(texture);
        Assert.AreEqual(texture.Width, 256);
        Assert.AreEqual(texture.Memory.Length, 256);
        Assert.AreEqual(texture.Span.Length, 256);
        Assert.AreSame(texture.GraphicsDevice, device.Get());

        if (allocationMode == AllocationMode.Clear)
        {
            foreach (float x in texture.Memory.Span)
            {
                Assert.AreEqual(x, 0f);
            }

            foreach (float x in texture.Span)
            {
                Assert.AreEqual(x, 0f);
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture1D<>))]
    [Resource(typeof(ReadBackTexture1D<>))]
    [Data(-14253)]
    [Data(-1)]
    [Data(0)]
    [Data(-14)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Allocate_Uninitialized_Fail(Device device, Type textureType, int width)
    {
        using TransferTexture1D<float> texture = device.Get().AllocateTransferTexture1D<float>(textureType, width);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture1D<>))]
    [Resource(typeof(ReadBackTexture1D<>))]
    [ExpectedException(typeof(ObjectDisposedException))]
    public void UsageAfterDispose(Device device, Type textureType)
    {
        using TransferTexture1D<float> texture = device.Get().AllocateTransferTexture1D<float>(textureType, 32);

        texture.Dispose();

        _ = texture.Span;
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void Allocate_UploadTexture1D_Copy_Full(Device device)
    {
        using UploadTexture1D<int> uploadTexture1D = device.Get().AllocateUploadTexture1D<int>(256);

        new Random(42).NextBytes(uploadTexture1D.Span.AsBytes());

        using ReadOnlyTexture1D<int> readOnlyTexture1D = device.Get().AllocateReadOnlyTexture1D<int>(uploadTexture1D.Width);

        uploadTexture1D.CopyTo(readOnlyTexture1D);

        int[] result = readOnlyTexture1D.ToArray();

        Assert.AreEqual(uploadTexture1D.Width, result.Length);

        fixed (int* _ = result)
        {
            Assert.IsTrue(uploadTexture1D.Span.SequenceEqual(new Span<int>(Unsafe.AsPointer(ref result[0]), result.Length)));
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(0, 2048)]
    [Data(128, 1813)]
    [Data(0, 2000)]
    [Data(97, 512)]
    public unsafe void Allocate_UploadTexture1D_Copy_Range(Device device, int x, int width)
    {
        using UploadTexture1D<int> uploadTexture1D = device.Get().AllocateUploadTexture1D<int>(2048);

        new Random(42).NextBytes(uploadTexture1D.Span.AsBytes());

        using ReadOnlyTexture1D<int> readOnlyTexture1D = device.Get().AllocateReadOnlyTexture1D<int>(uploadTexture1D.Width);

        uploadTexture1D.CopyTo(readOnlyTexture1D, x, width);

        int[] result = readOnlyTexture1D.ToArray();

        fixed (void* _ = result)
        {
            Span<int> sourceRow = uploadTexture1D.Span.Slice(x, width);
            Span<int> destinationRow = new Span<int>(Unsafe.AsPointer(ref result[0]), result.Length).Slice(0, width);

            Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void Allocate_ReadBackTexture1D_Copy_Full(Device device)
    {
        int[] source = new int[256];

        fixed (int* p = source)
        {
            new Random(42).NextBytes(new Span<int>(p, source.Length).AsBytes());
        }

        using ReadOnlyTexture1D<int> readOnlyTexture1D = device.Get().AllocateReadOnlyTexture1D(source);
        using ReadBackTexture1D<int> readBackTexture1D = device.Get().AllocateReadBackTexture1D<int>(256);

        readOnlyTexture1D.CopyTo(readBackTexture1D);

        Assert.AreEqual(readBackTexture1D.Width, 256);

        fixed (int* p = source)
        {
            Assert.IsTrue(readBackTexture1D.Span.SequenceEqual(new Span<int>(Unsafe.AsPointer(ref source[0]), source.Length)));
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(0, 2048)]
    [Data(128, 1813)]
    [Data(0, 2000)]
    [Data(97, 512)]
    public unsafe void Allocate_ReadBackTexture2D_Copy_Range(Device device, int x, int width)
    {
        int[] source = new int[2048];

        fixed (int* p = source)
        {
            new Random(42).NextBytes(new Span<int>(p, source.Length).AsBytes());
        }

        using ReadOnlyTexture1D<int> readOnlyTexture1D = device.Get().AllocateReadOnlyTexture1D(source);
        using ReadBackTexture1D<int> readBackTexture1D = device.Get().AllocateReadBackTexture1D<int>(2048);

        readOnlyTexture1D.CopyTo(readBackTexture1D, x, width);

        fixed (int* p = source)
        {
            Span<int> sourceRow = new Span<int>(Unsafe.AsPointer(ref source[0]), source.Length).Slice(x, width);
            Span<int> destinationRow = readBackTexture1D.Span.Slice(0, width);

            Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
        }
    }
}