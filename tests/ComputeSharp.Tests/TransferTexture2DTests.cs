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
public partial class TransferTexture2DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture2D<>))]
    [Resource(typeof(ReadBackTexture2D<>))]
    [Data(AllocationMode.Default)]
    [Data(AllocationMode.Clear)]
    public unsafe void Allocate_Uninitialized_Ok(Device device, Type textureType, AllocationMode allocationMode)
    {
        using TransferTexture2D<float> texture = device.Get().AllocateTransferTexture2D<float>(textureType, 128, 256, allocationMode);

        Assert.IsNotNull(texture);
        Assert.AreEqual(texture.Width, 128);
        Assert.AreEqual(texture.Height, 256);
        Assert.AreEqual(texture.View.Width, 128);
        Assert.AreEqual(texture.View.Height, 256);
        Assert.AreSame(texture.GraphicsDevice, device.Get());

        if (allocationMode == AllocationMode.Clear)
        {
            for (int y = 0; y < texture.Height; y++)
            {
                foreach (float x in texture.View.GetRowSpan(y))
                {
                    Assert.AreEqual(x, 0f);
                }
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture2D<>))]
    [Resource(typeof(ReadBackTexture2D<>))]
    [Data(128, -14253)]
    [Data(128, -1)]
    [Data(0, -4314)]
    [Data(-14, -53)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Allocate_Uninitialized_Fail(Device device, Type textureType, int width, int height)
    {
        using TransferTexture2D<float> texture = device.Get().AllocateTransferTexture2D<float>(textureType, width, height);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture2D<>))]
    [Resource(typeof(ReadBackTexture2D<>))]
    [ExpectedException(typeof(ObjectDisposedException))]
    public void UsageAfterDispose(Device device, Type textureType)
    {
        using TransferTexture2D<float> texture = device.Get().AllocateTransferTexture2D<float>(textureType, 32, 32);

        texture.Dispose();

        _ = texture.View;
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void Allocate_UploadTexture2D_Copy_Full(Device device)
    {
        using UploadTexture2D<int> uploadTexture2D = device.Get().AllocateUploadTexture2D<int>(256, 256);

        for (int i = 0; i < uploadTexture2D.Height; i++)
        {
            new Random(i).NextBytes(uploadTexture2D.View.GetRowSpan(i).AsBytes());
        }

        using ReadOnlyTexture2D<int> readOnlyTexture2D = device.Get().AllocateReadOnlyTexture2D<int>(uploadTexture2D.Width, uploadTexture2D.Height);

        uploadTexture2D.CopyTo(readOnlyTexture2D);

        int[,] result = readOnlyTexture2D.ToArray();

        Assert.AreEqual(uploadTexture2D.Width, result.GetLength(1));
        Assert.AreEqual(uploadTexture2D.Height, result.GetLength(0));

        fixed (int* _ = result)
        {
            for (int i = 0; i < uploadTexture2D.Height; i++)
            {
                Assert.IsTrue(uploadTexture2D.View.GetRowSpan(i).SequenceEqual(new Span<int>(Unsafe.AsPointer(ref result[i, 0]), result.GetLength(1))));
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(0, 0, 2048, 2048)]
    [Data(128, 0, 1813, 2048)]
    [Data(0, 128, 2000, 944)]
    [Data(97, 33, 512, 794)]
    public unsafe void Allocate_UploadTexture2D_Copy_Range(Device device, int x, int y, int width, int height)
    {
        using UploadTexture2D<int> uploadTexture2D = device.Get().AllocateUploadTexture2D<int>(2048, 2048);

        for (int i = 0; i < uploadTexture2D.Height; i++)
        {
            new Random(i).NextBytes(uploadTexture2D.View.GetRowSpan(i).AsBytes());
        }

        using ReadOnlyTexture2D<int> readOnlyTexture2D = device.Get().AllocateReadOnlyTexture2D<int>(uploadTexture2D.Width, uploadTexture2D.Height);

        uploadTexture2D.CopyTo(readOnlyTexture2D, x, y, width, height);

        int[,] result = readOnlyTexture2D.ToArray();

        fixed (void* _ = result)
        {
            for (int i = 0; i < height; i++)
            {
                Span<int> sourceRow = uploadTexture2D.View.GetRowSpan(i + y).Slice(x, width);
                Span<int> destinationRow = new Span<int>(Unsafe.AsPointer(ref result[i, 0]), result.GetLength(1)).Slice(0, width);

                Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void Allocate_ReadBackTexture2D_Copy_Full(Device device)
    {
        int[,] source = new int[256, 256];

        fixed (int* p = source)
        {
            new Random(42).NextBytes(new Span<int>(p, source.Length).AsBytes());
        }

        using ReadOnlyTexture2D<int> readOnlyTexture2D = device.Get().AllocateReadOnlyTexture2D(source);
        using ReadBackTexture2D<int> readBackTexture2D = device.Get().AllocateReadBackTexture2D<int>(256, 256);

        readOnlyTexture2D.CopyTo(readBackTexture2D);

        Assert.AreEqual(readBackTexture2D.Width, 256);
        Assert.AreEqual(readBackTexture2D.Height, 256);

        fixed (int* p = source)
        {
            for (int i = 0; i < readBackTexture2D.Height; i++)
            {
                Assert.IsTrue(readBackTexture2D.View.GetRowSpan(i).SequenceEqual(new Span<int>(Unsafe.AsPointer(ref source[i, 0]), source.GetLength(1))));
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(0, 0, 2048, 2048)]
    [Data(128, 0, 1813, 2048)]
    [Data(0, 128, 2000, 944)]
    [Data(97, 33, 512, 794)]
    public unsafe void Allocate_ReadBackTexture2D_Copy_Range(Device device, int x, int y, int width, int height)
    {
        int[,] source = new int[2048, 2048];

        fixed (int* p = source)
        {
            new Random(42).NextBytes(new Span<int>(p, source.Length).AsBytes());
        }

        using ReadOnlyTexture2D<int> readOnlyTexture2D = device.Get().AllocateReadOnlyTexture2D(source);
        using ReadBackTexture2D<int> readBackTexture2D = device.Get().AllocateReadBackTexture2D<int>(2048, 2048);

        readOnlyTexture2D.CopyTo(readBackTexture2D, x, y, width, height);

        fixed (int* p = source)
        {
            for (int i = 0; i < height; i++)
            {
                Span<int> sourceRow = new Span<int>(Unsafe.AsPointer(ref source[i + y, 0]), source.GetLength(1)).Slice(x, width);
                Span<int> destinationRow = readBackTexture2D.View.GetRowSpan(i).Slice(0, width);

                Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
            }
        }
    }
}