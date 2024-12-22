using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Enumerables;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class Texture3DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [Data(AllocationMode.Default)]
    [Data(AllocationMode.Clear)]
    public void Allocate_Uninitialized_Ok(Device device, Type textureType, AllocationMode allocationMode)
    {
        using Texture3D<float> texture = device.Get().AllocateTexture3D<float>(textureType, 64, 64, 12, allocationMode);

        Assert.IsNotNull(texture);
        Assert.AreEqual(texture.Width, 64);
        Assert.AreEqual(texture.Height, 64);
        Assert.AreEqual(texture.Depth, 12);
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
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [Data(128, -14253, 4)]
    [Data(128, -1, 4)]
    [Data(0, -4314, 4)]
    [Data(-14, -53, 4)]
    [Data(12, 12, -1)]
    [Data(2, 4, ushort.MaxValue + 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Allocate_Uninitialized_Fail(Device device, Type textureType, int width, int height, int depth)
    {
        using Texture3D<float> texture = device.Get().AllocateTexture3D<float>(textureType, width, height, depth);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [Data(12, 12, 4)]
    [Data(2, 12, 4)]
    [Data(64, 64, 1)]
    [Data(64, 27, 3)]
    public void Allocate_FromArray(Device device, Type textureType, int width, int height, int depth)
    {
        float[] data = Enumerable.Range(0, width * height * depth).Select(static i => (float)i).ToArray();

        using Texture3D<float> texture = device.Get().AllocateTexture3D(textureType, data, width, height, depth);

        Assert.IsNotNull(texture);
        Assert.AreEqual(texture.Width, width);
        Assert.AreEqual(texture.Height, height);
        Assert.AreEqual(texture.Depth, depth);
        Assert.AreSame(texture.GraphicsDevice, device.Get());

        float[,,] result = texture.ToArray();

        Assert.IsNotNull(result);
        Assert.AreEqual(result.GetLength(0), depth);
        Assert.AreEqual(result.GetLength(1), height);
        Assert.AreEqual(result.GetLength(2), width);
        Assert.AreEqual(data.Length, result.Length);
        Assert.IsTrue(data.SequenceEqual(result.Cast<float>()));

        Array.Clear(result, 0, result.Length);

        float[] flat = new float[result.Length];

        texture.CopyTo(flat);

        CollectionAssert.AreEqual(flat, data);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [ExpectedException(typeof(ObjectDisposedException))]
    public void UsageAfterDispose(Device device, Type textureType)
    {
        using Texture3D<float> texture = device.Get().AllocateTexture3D<float>(textureType, 10, 10, 4);

        texture.Dispose();

        _ = texture.ToArray();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [Data(0, 0, 0, 64, 64, 8)]
    [Data(0, 14, 0, 64, 50, 1)]
    [Data(14, 0, 0, 50, 64, 1)]
    [Data(0, 0, 3, 50, 64, 1)]
    [Data(10, 10, 1, 54, 54, 4)]
    [Data(20, 20, 3, 32, 27, 3)]
    [Data(40, 2, 4, 4, 62, 4)]
    [Data(0, 60, 1, 64, 4, 6)]
    [Data(63, 2, 2, 1, 60, 5)]
    [Data(2, 63, 7, 60, 1, 1)]
    public void GetData_RangeToArray_Ok(Device device, Type textureType, int x, int y, int z, int width, int height, int depth)
    {
        float[] array = Enumerable.Range(0, 64 * 64 * 8).Select(static i => (float)i).ToArray();

        using Texture3D<float> texture = device.Get().AllocateTexture3D(textureType, array, 64, 64, 8);

        float[,,] result = texture.ToArray(x, y, z, width, height, depth);

        Assert.AreEqual(depth, result.GetLength(0));
        Assert.AreEqual(height, result.GetLength(1));
        Assert.AreEqual(width, result.GetLength(2));

        for (int k = 0; k < depth; k++)
        {
            Span2D<float> expected = new Span2D<float>(array, 64 * 64 * (k + z), 64, 64, 0).Slice(y, x, height, width);
            Span2D<float> data = new(result, k);

            CollectionAssert.AreEqual(expected.ToArray(), data.ToArray());
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [Data(-1, 0, 0, 50, 50, 1)]
    [Data(0, -1, 0, 50, 50, 1)]
    [Data(0, 0, -1, 50, 50, 1)]
    [Data(12, 0, 0, -1, 50, 1)]
    [Data(12, 0, 0, 20, -1, 0)]
    [Data(12, 0, 0, 20, 2, -1)]
    [Data(12, 20, 0, 20, 50, 2)]
    [Data(12, 20, 0, 60, 20, 3)]
    [Data(12, 10, 6, 20, 20, 10)]
    [Data(80, 20, 0, 40, 20, 1)]
    [Data(0, 80, 0, 40, 20, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetData_RangeToArray_Fail(Device device, Type textureType, int x, int y, int z, int width, int height, int depth)
    {
        using Texture3D<float> texture = device.Get().AllocateTexture3D<float>(textureType, 64, 64, 8);

        _ = texture.ToArray(x, y, z, width, height, depth);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [Data(0, 0, 0, 64, 64, 3)]
    [Data(0, 14, 0, 64, 50, 3)]
    [Data(0, 14, 1, 64, 50, 2)]
    [Data(14, 0, 1, 50, 64, 2)]
    [Data(10, 10, 2, 54, 54, 1)]
    [Data(20, 20, 0, 32, 27, 3)]
    [Data(60, 0, 0, 4, 64, 3)]
    [Data(0, 60, 2, 64, 4, 1)]
    [Data(63, 2, 0, 1, 60, 2)]
    [Data(2, 63, 1, 60, 1, 2)]
    public unsafe void GetData_RangeToVoid_Ok(Device device, Type textureType, int x, int y, int z, int width, int height, int depth)
    {
        int[,,] array = new int[3, 64, 64];

        fixed (int* p = array)
        {
            foreach (SpanEnumerable<int>.Item item in new Span<int>(p, array.Length).Enumerate())
            {
                item.Value = item.Index;
            }
        }

        using Texture3D<int> texture = device.Get().AllocateTexture3D(textureType, array);

        int[,,] result = new int[depth, height, width];

        fixed (int* p = result)
        {
            texture.CopyTo(new Span<int>(p, result.Length), x, y, z, width, height, depth);
        }

        for (int k = 0; k < depth; k++)
        {
            Span2D<int> source = array.AsSpan2D(z + k).Slice(y, x, height, width);
            Span2D<int> destination = result.AsSpan2D(k);

            CollectionAssert.AreEqual(source.ToArray(), destination.ToArray());
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [Data(0, -1, 0, 50, 50, 2)]
    [Data(-1, 0, 0, 50, 50, 3)]
    [Data(12, 0, 0, -1, 50, 1)]
    [Data(12, 0, 0, 20, 50, 0)]
    [Data(12, 0, 1, 20, -1, 2)]
    [Data(12, 20, 2, 20, 50, 1)]
    [Data(12, 20, 0, 60, 20, 3)]
    [Data(80, 20, 2, 40, 20, 1)]
    [Data(0, 80, 2, 40, 20, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetData_RangeToVoid_Fail(Device device, Type textureType, int x, int y, int z, int width, int height, int depth)
    {
        float[] array = Enumerable.Range(0, 64 * 64 * 3).Select(static i => (float)i).ToArray();

        using Texture3D<float> texture = device.Get().AllocateTexture3D(textureType, array, 64, 64, 3);

        float[] result = new float[array.Length];

        texture.CopyTo(result, x, y, z, width, height, depth);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture3D<>))]
    [AdditionalResource(typeof(ReadWriteTexture3D<>))]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 512, 512, 3)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 512, 512, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 1, 0, 0, 0, 512, 512, 2)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 1, 512, 512, 2)]
    // [Data(512, 512, 4, 512, 512, 4, 0, 0, 1, 0, 0, 2, 512, 512, 2)]
    // This test fails, only on WARP devices, and only if not run on its own. It seems
    // to be possibly caused by a bug in ID3D12GraphicsCommandList::CopyTextureRegion.
    [Data(512, 1024, 3, 512, 512, 3, 127, 256, 1, 128, 128, 1, 128, 256, 1)]
    [Data(512, 1024, 3, 512, 512, 3, 127, 256, 1, 128, 128, 1, 128, 256, 2)]
    [Data(512, 1024, 3, 512, 512, 3, 122, 329, 1, 127, 256, 2, 125, 200, 1)]
    [Data(1024, 1024, 3, 512, 512, 3, 127, 256, 1, 128, 128, 1, 128, 256, 1)]
    [Data(1024, 1024, 3, 512, 512, 3, 127, 256, 1, 128, 128, 1, 128, 256, 2)]
    [Data(1024, 1024, 3, 512, 512, 3, 122, 329, 1, 127, 256, 2, 125, 200, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 0, 0, 0, 512, 512, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 512, 0, 0, 512, 512, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 0, 512, 0, 512, 512, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 512, 512, 0, 512, 512, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 0, 0, 0, 512, 512, 2)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 512, 0, 0, 512, 512, 2)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 0, 512, 0, 512, 512, 2)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 512, 512, 0, 512, 512, 2)]
    public void CopyTo_TextureToVoid_Ok(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int sourceHeight,
        int sourceDepth,
        int destinationWidth,
        int destinationHeight,
        int destinationDepth,
        int sourceOffsetX,
        int sourceOffsetY,
        int sourceOffsetZ,
        int destinationOffsetX,
        int destinationOffsetY,
        int destinationOffsetZ,
        int copyWidth,
        int copyHeight,
        int copyDepth)
    {
        float[] array = Enumerable.Range(0, sourceHeight * sourceWidth * sourceDepth).Select(static i => (float)i).ToArray();

        using Texture3D<float> source = device.Get().AllocateTexture3D(sourceType, array, sourceWidth, sourceHeight, sourceDepth);
        using Texture3D<float> destination = device.Get().AllocateTexture3D<float>(destinationType, destinationWidth, destinationHeight, destinationDepth, AllocationMode.Clear);

        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, copyWidth, copyHeight, copyDepth);

        float[,,] stack = destination.ToArray();

        for (int k = 0; k < destinationDepth; k++)
        {
            ReadOnlySpan2D<float> result = new(stack, k);

            for (int i = 0; i < destinationHeight; i++)
            {
                for (int j = 0; j < destinationWidth; j++)
                {
                    if (k >= destinationOffsetZ &&
                        k < destinationOffsetZ + copyDepth &&
                        i >= destinationOffsetY &&
                        i < destinationOffsetY + copyHeight &&
                        j >= destinationOffsetX &&
                        j < destinationOffsetX + copyWidth)
                    {
                        int sourceZ = k - destinationOffsetZ + sourceOffsetZ;
                        int sourceY = i - destinationOffsetY + sourceOffsetY;
                        int sourceX = j - destinationOffsetX + sourceOffsetX;

                        ReadOnlySpan2D<float> expected = new(array, sourceZ * sourceHeight * sourceWidth, sourceHeight, sourceWidth, 0);

                        Assert.AreEqual(expected[sourceY, sourceX], result[i, j]);
                    }
                    else
                    {
                        Assert.AreEqual(0, result[i, j]);
                    }
                }
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture3D<>))]
    [AdditionalResource(typeof(ReadWriteTexture3D<>))]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 2055, 1024, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, -1, 1024, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, int.MaxValue, 1024, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, 1025, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, -1, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, int.MaxValue, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, 128, 6)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, 128, -1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, 128, int.MaxValue)]
    [Data(512, 512, 3, 512, 512, 3, 450, 0, 0, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 450, 0, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 5, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 450, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 450, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 4, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, -1, 0, 0, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, -1, 0, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, -1, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, -1, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, -1, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, -1, 128, 128, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyTo_TextureToVoid_Fail(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int sourceHeight,
        int sourceDepth,
        int destinationWidth,
        int destinationHeight,
        int destinationDepth,
        int sourceOffsetX,
        int sourceOffsetY,
        int sourceOffsetZ,
        int destinationOffsetX,
        int destinationOffsetY,
        int destinationOffsetZ,
        int copyWidth,
        int copyHeight,
        int copyDepth)
    {
        using Texture3D<float> source = device.Get().AllocateTexture3D<float>(sourceType, sourceWidth, sourceHeight, sourceDepth);
        using Texture3D<float> destination = device.Get().AllocateTexture3D<float>(destinationType, destinationWidth, destinationHeight, destinationDepth, AllocationMode.Clear);

        source.CopyTo(destination, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, copyWidth, copyHeight, copyDepth);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture3D<>))]
    [AdditionalResource(typeof(ReadWriteTexture3D<>))]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 512, 512, 3)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 512, 512, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 1, 0, 0, 0, 512, 512, 2)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 1, 512, 512, 2)]
    // [Data(512, 512, 4, 512, 512, 4, 0, 0, 1, 0, 0, 2, 512, 512, 2)]
    // TODO: see explanation above in CopyTo_TextureToVoid_Ok
    [Data(512, 1024, 3, 512, 512, 3, 127, 256, 1, 128, 128, 1, 128, 256, 1)]
    [Data(512, 1024, 3, 512, 512, 3, 127, 256, 1, 128, 128, 1, 128, 256, 2)]
    [Data(512, 1024, 3, 512, 512, 3, 122, 329, 1, 127, 256, 2, 125, 200, 1)]
    [Data(1024, 1024, 3, 512, 512, 3, 127, 256, 1, 128, 128, 1, 128, 256, 1)]
    [Data(1024, 1024, 3, 512, 512, 3, 127, 256, 1, 128, 128, 1, 128, 256, 2)]
    [Data(1024, 1024, 3, 512, 512, 3, 122, 329, 1, 127, 256, 2, 125, 200, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 0, 0, 0, 512, 512, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 512, 0, 0, 512, 512, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 0, 512, 0, 512, 512, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 512, 512, 0, 512, 512, 1)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 0, 0, 0, 512, 512, 2)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 512, 0, 0, 512, 512, 2)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 0, 512, 0, 512, 512, 2)]
    [Data(512, 512, 2, 1024, 1024, 3, 0, 0, 0, 512, 512, 0, 512, 512, 2)]
    public void CopyFrom_TextureToVoid_Ok(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int sourceHeight,
        int sourceDepth,
        int destinationWidth,
        int destinationHeight,
        int destinationDepth,
        int sourceOffsetX,
        int sourceOffsetY,
        int sourceOffsetZ,
        int destinationOffsetX,
        int destinationOffsetY,
        int destinationOffsetZ,
        int copyWidth,
        int copyHeight,
        int copyDepth)
    {
        float[] array = Enumerable.Range(0, sourceHeight * sourceWidth * sourceDepth).Select(static i => (float)i).ToArray();

        using Texture3D<float> source = device.Get().AllocateTexture3D(sourceType, array, sourceWidth, sourceHeight, sourceDepth);
        using Texture3D<float> destination = device.Get().AllocateTexture3D<float>(destinationType, destinationWidth, destinationHeight, destinationDepth, AllocationMode.Clear);

        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, copyWidth, copyHeight, copyDepth);

        float[,,] stack = destination.ToArray();

        for (int k = 0; k < destinationDepth; k++)
        {
            ReadOnlySpan2D<float> result = new(stack, k);

            for (int i = 0; i < destinationHeight; i++)
            {
                for (int j = 0; j < destinationWidth; j++)
                {
                    if (k >= destinationOffsetZ &&
                        k < destinationOffsetZ + copyDepth &&
                        i >= destinationOffsetY &&
                        i < destinationOffsetY + copyHeight &&
                        j >= destinationOffsetX &&
                        j < destinationOffsetX + copyWidth)
                    {
                        int sourceZ = k - destinationOffsetZ + sourceOffsetZ;
                        int sourceY = i - destinationOffsetY + sourceOffsetY;
                        int sourceX = j - destinationOffsetX + sourceOffsetX;

                        ReadOnlySpan2D<float> expected = new(array, sourceZ * sourceHeight * sourceWidth, sourceHeight, sourceWidth, 0);

                        Assert.AreEqual(expected[sourceY, sourceX], result[i, j]);
                    }
                    else
                    {
                        Assert.AreEqual(0, result[i, j]);
                    }
                }
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture3D<>))]
    [AdditionalResource(typeof(ReadWriteTexture3D<>))]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 2055, 1024, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, -1, 1024, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, int.MaxValue, 1024, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, 1025, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, -1, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, int.MaxValue, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, 128, 6)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, 128, -1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 0, 256, 128, int.MaxValue)]
    [Data(512, 512, 3, 512, 512, 3, 450, 0, 0, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 450, 0, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 5, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 450, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 450, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, 4, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, -1, 0, 0, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, -1, 0, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, -1, 0, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, -1, 0, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, -1, 0, 128, 128, 1)]
    [Data(512, 512, 3, 512, 512, 3, 0, 0, 0, 0, 0, -1, 128, 128, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyFrom_TextureToVoid_Fail(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int sourceHeight,
        int sourceDepth,
        int destinationWidth,
        int destinationHeight,
        int destinationDepth,
        int sourceOffsetX,
        int sourceOffsetY,
        int sourceOffsetZ,
        int destinationOffsetX,
        int destinationOffsetY,
        int destinationOffsetZ,
        int copyWidth,
        int copyHeight,
        int copyDepth)
    {
        using Texture3D<float> source = device.Get().AllocateTexture3D<float>(sourceType, sourceWidth, sourceHeight, sourceDepth);
        using Texture3D<float> destination = device.Get().AllocateTexture3D<float>(destinationType, destinationWidth, destinationHeight, destinationDepth, AllocationMode.Clear);

        destination.CopyFrom(source, sourceOffsetX, sourceOffsetY, sourceOffsetZ, destinationOffsetX, destinationOffsetY, destinationOffsetZ, copyWidth, copyHeight, copyDepth);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadOnlyTexture3D(Device device)
    {
        int[] data = Enumerable.Range(0, 32 * 32 * 3).ToArray();

        using ReadOnlyTexture3D<int> source = device.Get().AllocateReadOnlyTexture3D(data, 32, 32, 3);
        using ReadWriteBuffer<int> destination = device.Get().AllocateReadWriteBuffer<int>(data.Length);

        device.Get().For(source.Width, source.Height, source.Depth, new ReadOnlyTexture3DKernel(source, destination));

        int[] result = destination.ToArray();

        CollectionAssert.AreEqual(data, result);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture3DKernel : IComputeShader
    {
        public readonly ReadOnlyTexture3D<int> source;
        public readonly ReadWriteBuffer<int> destination;

        public void Execute()
        {
            this.destination[(ThreadIds.Z * 32 * 32) + (ThreadIds.Y * 32) + ThreadIds.X] = this.source[ThreadIds.XYZ];
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadWriteTexture2D(Device device)
    {
        int[] data = Enumerable.Range(0, 32 * 32 * 3).ToArray();

        using ReadWriteTexture3D<int> source = device.Get().AllocateReadWriteTexture3D(data, 32, 32, 3);
        using ReadWriteTexture3D<int> destination = device.Get().AllocateReadWriteTexture3D<int>(32, 32, 3);

        device.Get().For(source.Width, source.Height, source.Depth, new ReadWriteTexture3DKernel(source, destination));

        int[] result = new int[data.Length];

        destination.CopyTo(result);

        CollectionAssert.AreEqual(data, result);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture3DKernel : IComputeShader
    {
        public readonly ReadWriteTexture3D<int> source;
        public readonly ReadWriteTexture3D<int> destination;

        public void Execute()
        {
            this.destination[ThreadIds.XYZ] = this.source[ThreadIds.XYZ];
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadWriteTexture3D_AsInterface_FromReadOnly(Device device)
    {
        float[] data = Enumerable.Range(0, 32 * 32 * 3).Select(static i => (float)i).ToArray();

        using ReadOnlyTexture3D<float> source = device.Get().AllocateReadOnlyTexture3D(data, 32, 32, 3);
        using ReadWriteTexture3D<float> texture1 = device.Get().AllocateReadWriteTexture3D<float>(32, 32, 3);
        using ReadWriteTexture3D<float> texture2 = device.Get().AllocateReadWriteTexture3D<float>(32, 32, 3);

        device.Get().For(source.Width, new InterfaceReadOnlyTexture3DKernel(source, texture1, texture2));

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
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct InterfaceReadOnlyTexture3DKernel : IComputeShader
    {
        public readonly IReadOnlyTexture3D<float> source;
        public readonly ReadWriteTexture3D<float> texture1;
        public readonly ReadWriteTexture3D<float> texture2;

        public void Execute()
        {
            this.texture1[ThreadIds.XYZ] = this.source[ThreadIds.XYZ];
            this.texture2[ThreadIds.XYZ] = this.source.Sample(ThreadIds.XYZ);
        }
    }
}