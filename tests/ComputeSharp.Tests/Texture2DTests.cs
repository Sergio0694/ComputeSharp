using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.HighPerformance;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class Texture2DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [Data(AllocationMode.Default)]
    [Data(AllocationMode.Clear)]
    public void Allocate_Uninitialized_Ok(Device device, Type textureType, AllocationMode allocationMode)
    {
        using Texture2D<float> texture = device.Get().AllocateTexture2D<float>(textureType, 128, 128, allocationMode);

        Assert.IsNotNull(texture);
        Assert.AreEqual(texture.Width, 128);
        Assert.AreEqual(texture.Height, 128);
        Assert.AreSame(texture.GraphicsDevice, device.Get());

        if (allocationMode == AllocationMode.Clear)
        {
            foreach (float x in texture.ToArray())
            {
                Assert.AreEqual(x, 0f);
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [Data(128, -14253)]
    [Data(128, -1)]
    [Data(0, -4314)]
    [Data(-14, -53)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Allocate_Uninitialized_Fail(Device device, Type textureType, int width, int height)
    {
        using Texture2D<float> texture = device.Get().AllocateTexture2D<float>(textureType, width, height);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    public void Allocate_FromArray(Device device, Type textureType)
    {
        float[] data = Enumerable.Range(0, 128 * 128).Select(static i => (float)i).ToArray();

        using Texture2D<float> texture = device.Get().AllocateTexture2D(textureType, data, 128, 128);

        Assert.IsNotNull(texture);
        Assert.AreEqual(texture.Width, 128);
        Assert.AreEqual(texture.Height, 128);
        Assert.AreSame(texture.GraphicsDevice, device.Get());

        float[,] result = texture.ToArray();

        Assert.IsNotNull(result);
        Assert.AreEqual(data.Length, result.Length);
        Assert.IsTrue(data.SequenceEqual(result.Cast<float>()));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [ExpectedException(typeof(ObjectDisposedException))]
    public void UsageAfterDispose(Device device, Type textureType)
    {
        using Texture2D<float> texture = device.Get().AllocateTexture2D<float>(textureType, 10, 10);

        texture.Dispose();

        _ = texture.ToArray();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [Data(0, 0, 64, 64)]
    [Data(0, 14, 64, 50)]
    [Data(14, 0, 50, 64)]
    [Data(10, 10, 54, 54)]
    [Data(20, 20, 32, 27)]
    [Data(60, 0, 4, 64)]
    [Data(0, 60, 64, 4)]
    [Data(63, 2, 1, 60)]
    [Data(2, 63, 60, 1)]
    public void GetData_RangeToArray_Ok(Device device, Type textureType, int x, int y, int width, int height)
    {
        float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

        using Texture2D<float> texture = device.Get().AllocateTexture2D(textureType, array, 64, 64);

        float[,] result = texture.ToArray(x, y, width, height);

        Assert.AreEqual(height, result.GetLength(0));
        Assert.AreEqual(width, result.GetLength(1));

        Span2D<float> expected = new Span2D<float>(array, 64, 64).Slice(y, x, height, width);
        Span2D<float> data = new(result);

        CollectionAssert.AreEqual(expected.ToArray(), data.ToArray());
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [Data(-1, 0, 50, 50)]
    [Data(0, -1, 50, 50)]
    [Data(12, 0, -1, 50)]
    [Data(12, 0, 20, -1)]
    [Data(12, 20, 20, 50)]
    [Data(12, 20, 60, 20)]
    [Data(80, 20, 40, 20)]
    [Data(0, 80, 40, 20)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetData_RangeToArray_Fail(Device device, Type textureType, int x, int y, int width, int height)
    {
        using Texture2D<float> texture = device.Get().AllocateTexture2D<float>(textureType, 64, 64);

        _ = texture.ToArray(x, y, width, height);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [Data(0, 0, 64, 64)]
    [Data(0, 14, 64, 50)]
    [Data(14, 0, 50, 64)]
    [Data(10, 10, 54, 54)]
    [Data(20, 20, 32, 27)]
    [Data(60, 0, 4, 64)]
    [Data(0, 60, 64, 4)]
    [Data(63, 2, 1, 60)]
    [Data(2, 63, 60, 1)]
    public void GetData_RangeToVoid_Ok(Device device, Type textureType, int x, int y, int width, int height)
    {
        float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

        using Texture2D<float> texture = device.Get().AllocateTexture2D(textureType, array, 64, 64);

        float[] result = new float[width * height];

        texture.CopyTo(result, x, y, width, height);

        Span2D<float> expected = new Span2D<float>(array, 64, 64).Slice(y, x, height, width);
        Span2D<float> data = new(result, height, width);

        CollectionAssert.AreEqual(expected.ToArray(), data.ToArray());
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [Data(0, -1, 50, 50)]
    [Data(-1, 0, 50, 50)]
    [Data(12, 0, -1, 50)]
    [Data(12, 0, 20, -1)]
    [Data(12, 20, 20, 50)]
    [Data(12, 20, 60, 20)]
    [Data(80, 20, 40, 20)]
    [Data(0, 80, 40, 20)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetData_RangeToVoid_Fail(Device device, Type textureType, int x, int y, int width, int height)
    {
        float[] array = Enumerable.Range(0, 4096).Select(static i => (float)i).ToArray();

        using Texture2D<float> texture = device.Get().AllocateTexture2D(textureType, array, 64, 64);

        float[] result = new float[4096];

        texture.CopyTo(result, x, y, width, height);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture2D<>))]
    [AdditionalResource(typeof(ReadWriteTexture2D<>))]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 1024, 1024)]
    [Data(1024, 1024, 1024, 1024, 127, 256, 0, 0, 512, 480)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 127, 256, 512, 480)]
    [Data(1024, 1024, 1024, 1024, 122, 329, 127, 256, 512, 480)]
    [Data(512, 480, 1024, 768, 122, 29, 127, 256, 256, 320)]
    [Data(512, 480, 256, 256, 122, 120, 0, 0, 256, 256)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 0, 512, 512)]
    [Data(512, 512, 1024, 1024, 0, 0, 512, 0, 512, 512)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 512, 512, 512)]
    [Data(512, 512, 1024, 1024, 0, 0, 512, 512, 512, 512)]
    [Data(512, 512, 1024, 1024, 0, 0, 256, 256, 512, 512)]
    public void CopyTo_TextureToVoid_Ok(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int sourceHeight,
        int destinationWidth,
        int destinationHeight,
        int sourceOffsetX,
        int sourceOffsetY,
        int destinationOffsetX,
        int destinationOffsetY,
        int copyWidth,
        int copyHeight)
    {
        float[] array = Enumerable.Range(0, sourceHeight * sourceWidth).Select(static i => (float)i).ToArray();

        using Texture2D<float> source = device.Get().AllocateTexture2D(sourceType, array, sourceWidth, sourceHeight);
        using Texture2D<float> destination = device.Get().AllocateTexture2D<float>(destinationType, destinationWidth, destinationHeight, AllocationMode.Clear);

        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, copyWidth, copyHeight);

        ReadOnlySpan2D<float> expected = new(array, sourceHeight, sourceWidth);
        ReadOnlySpan2D<float> result = destination.ToArray();

        for (int i = 0; i < destinationHeight; i++)
        {
            for (int j = 0; j < destinationWidth; j++)
            {
                if (i >= destinationOffsetY &&
                    i < destinationOffsetY + copyHeight &&
                    j >= destinationOffsetX &&
                    j < destinationOffsetX + copyWidth)
                {
                    int sourceY = i - destinationOffsetY + sourceOffsetY;
                    int sourceX = j - destinationOffsetX + sourceOffsetX;

                    Assert.AreEqual(expected[sourceY, sourceX], result[i, j]);
                }
                else
                {
                    Assert.AreEqual(0, result[i, j]);
                }
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture2D<>))]
    [AdditionalResource(typeof(ReadWriteTexture2D<>))]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 2055, 1024)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 1024, 3920)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, -1, 1024)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 512, -2)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 512, int.MaxValue)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 0, 768, 768)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 0, 256, 768)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 0, 620, 256)]
    [Data(1024, 1024, 512, 512, 0, 0, 0, 0, 768, 768)]
    [Data(1024, 1024, 512, 512, 0, 0, 0, 0, 256, 768)]
    [Data(1024, 1024, 512, 512, 0, 0, 0, 0, 620, 256)]
    [Data(512, 512, 512, 512, 450, 0, 0, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 450, 0, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 0, 450, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 0, 0, 450, 128, 128)]
    [Data(512, 512, 512, 512, -1, 0, 0, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, -1, 0, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 0, -1, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 0, 0, -1, 128, 128)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyTo_TextureToVoid_Fail(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int sourceHeight,
        int destinationWidth,
        int destinationHeight,
        int sourceOffsetX,
        int sourceOffsetY,
        int destinationOffsetX,
        int destinationOffsetY,
        int copyWidth,
        int copyHeight)
    {
        using Texture2D<float> source = device.Get().AllocateTexture2D<float>(sourceType, sourceWidth, sourceHeight);
        using Texture2D<float> destination = device.Get().AllocateTexture2D<float>(destinationType, destinationWidth, destinationHeight, AllocationMode.Clear);

        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, copyWidth, copyHeight);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture2D<>))]
    [AdditionalResource(typeof(ReadWriteTexture2D<>))]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 1024, 1024)]
    [Data(1024, 1024, 1024, 1024, 127, 256, 0, 0, 512, 480)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 127, 256, 512, 480)]
    [Data(1024, 1024, 1024, 1024, 122, 329, 127, 256, 512, 480)]
    [Data(512, 480, 1024, 768, 122, 29, 127, 256, 256, 320)]
    [Data(512, 480, 256, 256, 122, 120, 0, 0, 256, 256)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 0, 512, 512)]
    [Data(512, 512, 1024, 1024, 0, 0, 512, 0, 512, 512)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 512, 512, 512)]
    [Data(512, 512, 1024, 1024, 0, 0, 512, 512, 512, 512)]
    [Data(512, 512, 1024, 1024, 0, 0, 256, 256, 512, 512)]
    public void CopyFrom_TextureToVoid_Ok(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int sourceHeight,
        int destinationWidth,
        int destinationHeight,
        int sourceOffsetX,
        int sourceOffsetY,
        int destinationOffsetX,
        int destinationOffsetY,
        int copyWidth,
        int copyHeight)
    {
        float[] array = Enumerable.Range(0, sourceHeight * sourceWidth).Select(static i => (float)i).ToArray();

        using Texture2D<float> source = device.Get().AllocateTexture2D(sourceType, array, sourceWidth, sourceHeight);
        using Texture2D<float> destination = device.Get().AllocateTexture2D<float>(destinationType, destinationWidth, destinationHeight, AllocationMode.Clear);

        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, copyWidth, copyHeight);

        ReadOnlySpan2D<float> expected = new(array, sourceHeight, sourceWidth);
        ReadOnlySpan2D<float> result = destination.ToArray();

        for (int i = 0; i < destinationHeight; i++)
        {
            for (int j = 0; j < destinationWidth; j++)
            {
                if (i >= destinationOffsetY &&
                    i < destinationOffsetY + copyHeight &&
                    j >= destinationOffsetX &&
                    j < destinationOffsetX + copyWidth)
                {
                    int sourceY = i - destinationOffsetY + sourceOffsetY;
                    int sourceX = j - destinationOffsetX + sourceOffsetX;

                    Assert.AreEqual(expected[sourceY, sourceX], result[i, j]);
                }
                else
                {
                    Assert.AreEqual(0, result[i, j]);
                }
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture2D<>))]
    [AdditionalResource(typeof(ReadWriteTexture2D<>))]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 2055, 1024)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 1024, 3920)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, -1, 1024)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 512, -2)]
    [Data(1024, 1024, 1024, 1024, 0, 0, 0, 0, 512, int.MaxValue)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 0, 768, 768)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 0, 256, 768)]
    [Data(512, 512, 1024, 1024, 0, 0, 0, 0, 620, 256)]
    [Data(1024, 1024, 512, 512, 0, 0, 0, 0, 768, 768)]
    [Data(1024, 1024, 512, 512, 0, 0, 0, 0, 256, 768)]
    [Data(1024, 1024, 512, 512, 0, 0, 0, 0, 620, 256)]
    [Data(512, 512, 512, 512, 450, 0, 0, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 450, 0, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 0, 450, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 0, 0, 450, 128, 128)]
    [Data(512, 512, 512, 512, -1, 0, 0, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, -1, 0, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 0, -1, 0, 128, 128)]
    [Data(512, 512, 512, 512, 0, 0, 0, -1, 128, 128)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyFrom_TextureToVoid_Fail(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int sourceHeight,
        int destinationWidth,
        int destinationHeight,
        int sourceOffsetX,
        int sourceOffsetY,
        int destinationOffsetX,
        int destinationOffsetY,
        int copyWidth,
        int copyHeight)
    {
        using Texture2D<float> source = device.Get().AllocateTexture2D<float>(sourceType, sourceWidth, sourceHeight);
        using Texture2D<float> destination = device.Get().AllocateTexture2D<float>(destinationType, destinationWidth, destinationHeight, AllocationMode.Clear);

        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, destinationOffsetX, destinationOffsetY, copyWidth, copyHeight);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadOnlyTexture2D(Device device)
    {
        int[] data = Enumerable.Range(0, 32 * 32).ToArray();

        using ReadOnlyTexture2D<int> source = device.Get().AllocateReadOnlyTexture2D(data, 32, 32);
        using ReadWriteBuffer<int> destination = device.Get().AllocateReadWriteBuffer<int>(data.Length);

        device.Get().For(source.Width, source.Height, new ReadOnlyTexture2DKernel(source, destination));

        int[] result = destination.ToArray();

        CollectionAssert.AreEqual(data, result);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture2DKernel : IComputeShader
    {
        public readonly ReadOnlyTexture2D<int> source;
        public readonly ReadWriteBuffer<int> destination;

        public void Execute()
        {
            this.destination[(ThreadIds.Y * 32) + ThreadIds.X] = this.source[ThreadIds.XY];
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadWriteTexture2D(Device device)
    {
        int[] data = Enumerable.Range(0, 32 * 32).ToArray();

        using ReadWriteTexture2D<int> source = device.Get().AllocateReadWriteTexture2D(data, 32, 32);
        using ReadWriteTexture2D<int> destination = device.Get().AllocateReadWriteTexture2D<int>(32, 32);

        device.Get().For(source.Width, source.Height, new ReadWriteTexture2DKernel(source, destination));

        int[] result = new int[data.Length];

        destination.CopyTo(result);

        CollectionAssert.AreEqual(data, result);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture2DKernel : IComputeShader
    {
        public readonly ReadWriteTexture2D<int> source;
        public readonly ReadWriteTexture2D<int> destination;

        public void Execute()
        {
            this.destination[ThreadIds.XY] = this.source[ThreadIds.XY];
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadWriteTexture2D_AsInterface_FromReadOnly(Device device)
    {
        float[] data = Enumerable.Range(0, 32 * 32).Select(static i => (float)i).ToArray();

        using ReadOnlyTexture2D<float> source = device.Get().AllocateReadOnlyTexture2D(data, 32, 32);
        using ReadWriteTexture2D<float> texture1 = device.Get().AllocateReadWriteTexture2D<float>(32, 32);
        using ReadWriteTexture2D<float> texture2 = device.Get().AllocateReadWriteTexture2D<float>(32, 32);

        device.Get().For(source.Width, new InterfaceReadOnlyTexture2DKernel(source, texture1, texture2));

        float[] result1 = texture1.ToArray().AsSpan().ToArray();
        float[] result2 = texture2.ToArray().AsSpan().ToArray();

        CollectionAssert.AreEqual(
            expected: data,
            actual: result1,
            comparer: Comparer<float>.Create(static (x, y) => Math.Abs(x - y) < 0.000001f ? 0 : x.CompareTo(y)));

        CollectionAssert.AreEqual(
            expected: data,
            actual: result2,
            comparer: Comparer<float>.Create(static (x, y) => Math.Abs(x - y) < 0.000001f ? 0 : x.CompareTo(y)));
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct InterfaceReadOnlyTexture2DKernel : IComputeShader
    {
        public readonly IReadOnlyTexture2D<float> source;
        public readonly ReadWriteTexture2D<float> texture1;
        public readonly ReadWriteTexture2D<float> texture2;

        public void Execute()
        {
            this.texture1[ThreadIds.XY] = this.source[ThreadIds.XY];
            this.texture2[ThreadIds.XY] = this.source.Sample(ThreadIds.XY);
        }
    }
}