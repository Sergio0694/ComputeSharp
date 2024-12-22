using System;
using System.Collections.Generic;
using System.Linq;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class Texture1DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [Data(AllocationMode.Default)]
    [Data(AllocationMode.Clear)]
    public void Allocate_Uninitialized_Ok(Device device, Type textureType, AllocationMode allocationMode)
    {
        using Texture1D<float> texture = device.Get().AllocateTexture1D<float>(textureType, 128, allocationMode);

        Assert.IsNotNull(texture);
        Assert.AreEqual(texture.Width, 128);
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
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [Data(-14253)]
    [Data(-1)]
    [Data(0)]
    [Data(-14)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Allocate_Uninitialized_Fail(Device device, Type textureType, int width)
    {
        using Texture1D<float> texture = device.Get().AllocateTexture1D<float>(textureType, width);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    public void Allocate_FromArray(Device device, Type textureType)
    {
        float[] data = Enumerable.Range(0, 128).Select(static i => (float)i).ToArray();

        using Texture1D<float> texture = device.Get().AllocateTexture1D(textureType, data);

        Assert.IsNotNull(texture);
        Assert.AreEqual(texture.Width, 128);
        Assert.AreSame(texture.GraphicsDevice, device.Get());

        float[] result = texture.ToArray();

        Assert.IsNotNull(result);
        Assert.AreEqual(data.Length, result.Length);
        Assert.IsTrue(data.SequenceEqual(result));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [ExpectedException(typeof(ObjectDisposedException))]
    public void UsageAfterDispose(Device device, Type textureType)
    {
        using Texture1D<float> texture = device.Get().AllocateTexture1D<float>(textureType, 10);

        texture.Dispose();

        _ = texture.ToArray();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [Data(0, 64)]
    [Data(0, 64)]
    [Data(14, 50)]
    [Data(10, 54)]
    [Data(20, 32)]
    [Data(60, 4)]
    [Data(0, 64)]
    [Data(63, 1)]
    [Data(2, 60)]
    public void GetData_RangeToArray_Ok(Device device, Type textureType, int x, int width)
    {
        float[] array = Enumerable.Range(0, 256).Select(static i => (float)i).ToArray();

        using Texture1D<float> texture = device.Get().AllocateTexture1D(textureType, array);

        float[] result = texture.ToArray(x, width);

        Assert.AreEqual(width, result.Length);

        Span<float> expected = array.AsSpan(x, width);

        CollectionAssert.AreEqual(expected.ToArray(), result);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [Data(-1, 20)]
    [Data(20, -1)]
    [Data(64, 1)]
    [Data(12, 200)]
    [Data(80, 40)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetData_RangeToArray_Fail(Device device, Type textureType, int x, int width)
    {
        using Texture1D<float> texture = device.Get().AllocateTexture1D<float>(textureType, 64);

        _ = texture.ToArray(x, width);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [Data(0, 64)]
    [Data(0, 64)]
    [Data(14, 50)]
    [Data(10, 54)]
    [Data(20, 32)]
    [Data(60, 4)]
    [Data(0, 64)]
    [Data(63, 1)]
    [Data(2, 60)]
    public void GetData_RangeToVoid_Ok(Device device, Type textureType, int x, int width)
    {
        float[] array = Enumerable.Range(0, 512).Select(static i => (float)i).ToArray();

        using Texture1D<float> texture = device.Get().AllocateTexture1D(textureType, array);

        float[] result = new float[width];

        texture.CopyTo(result, x, width);

        Span<float> expected = array.AsSpan(x, width);

        CollectionAssert.AreEqual(expected.ToArray(), result);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [Data(0, 500)]
    [Data(-1, 50)]
    [Data(12, -1)]
    [Data(128, 1)]
    [Data(12, -20)]
    [Data(0, -40)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetData_RangeToVoid_Fail(Device device, Type textureType, int x, int width)
    {
        float[] array = Enumerable.Range(0, 128).Select(static i => (float)i).ToArray();

        using Texture1D<float> texture = device.Get().AllocateTexture1D(textureType, array);

        float[] result = new float[128];

        texture.CopyTo(result, x, width);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture1D<>))]
    [AdditionalResource(typeof(ReadWriteTexture1D<>))]
    [Data(1024, 1024, 0, 0, 1024)]
    [Data(1024, 1024, 0, 256, 480)]
    [Data(1024, 1024, 127, 256, 480)]
    [Data(1024, 1024, 256, 0, 480)]
    [Data(512, 1024, 29, 127, 320)]
    [Data(512, 256, 120, 0, 256)]
    public void CopyTo_TextureToVoid_Ok(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int destinationWidth,
        int sourceOffsetX,
        int destinationOffsetX,
        int copyWidth)
    {
        float[] array = Enumerable.Range(0, sourceWidth).Select(static i => (float)i).ToArray();

        using Texture1D<float> source = device.Get().AllocateTexture1D(sourceType, array);
        using Texture1D<float> destination = device.Get().AllocateTexture1D<float>(destinationType, destinationWidth, AllocationMode.Clear);

        source.CopyTo(destination, sourceOffsetX, destinationOffsetX, copyWidth);

        ReadOnlySpan<float> expected = array;
        ReadOnlySpan<float> result = destination.ToArray();

        for (int i = 0; i < destinationWidth; i++)
        {
            if (i >= destinationOffsetX &&
                i < destinationOffsetX + copyWidth)
            {
                int sourceX = i - destinationOffsetX + sourceOffsetX;

                Assert.AreEqual(expected[sourceX], result[i]);
            }
            else
            {
                Assert.AreEqual(0, result[i]);
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture1D<>))]
    [AdditionalResource(typeof(ReadWriteTexture1D<>))]
    [Data(1024, 1024, 0, 0, 2055)]
    [Data(1024, 1024, 0, 0, 3920)]
    [Data(1024, 1024, 0, 0, -1)]
    [Data(1024, 1024, 0, 0, -2)]
    [Data(1024, 1024, 0, 0, int.MaxValue)]
    [Data(512, 1024, 0, 768, 768)]
    [Data(512, 1024, 0, 256, 768)]
    [Data(512, 1024, 0, 620, 480)]
    [Data(512, 512, 450, 0, 128)]
    [Data(512, 512, 0, 450, 128)]
    [Data(512, 512, -1, 0, 128)]
    [Data(512, 512, 0, -1, 128)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyTo_TextureToVoid_Fail(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int destinationWidth,
        int sourceOffsetX,
        int destinationOffsetX,
        int copyWidth)
    {
        using Texture1D<float> source = device.Get().AllocateTexture1D<float>(sourceType, sourceWidth);
        using Texture1D<float> destination = device.Get().AllocateTexture1D<float>(destinationType, destinationWidth, AllocationMode.Clear);

        source.CopyTo(destination, sourceOffsetX, destinationOffsetX, copyWidth);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture1D<>))]
    [AdditionalResource(typeof(ReadWriteTexture1D<>))]
    [Data(1024, 1024, 0, 0, 1024)]
    [Data(1024, 1024, 127, 256, 480)]
    [Data(1024, 1024, 0, 127, 480)]
    [Data(1024, 1024, 122, 329, 480)]
    [Data(512, 1024, 122, 256, 320)]
    [Data(512, 256, 0, 0, 256)]
    [Data(512, 1024, 0, 512, 512)]
    [Data(512, 1024, 0, 0, 512)]
    public void CopyFrom_TextureToVoid_Ok(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int destinationWidth,
        int sourceOffsetX,
        int destinationOffsetX,
        int copyWidth)
    {
        float[] array = Enumerable.Range(0, sourceWidth).Select(static i => (float)i).ToArray();

        using Texture1D<float> source = device.Get().AllocateTexture1D(sourceType, array);
        using Texture1D<float> destination = device.Get().AllocateTexture1D<float>(destinationType, destinationWidth, AllocationMode.Clear);

        destination.CopyFrom(source, sourceOffsetX, destinationOffsetX, copyWidth);

        ReadOnlySpan<float> expected = array;
        ReadOnlySpan<float> result = destination.ToArray();

        for (int i = 0; i < destinationWidth; i++)
        {
            if (i >= destinationOffsetX &&
                i < destinationOffsetX + copyWidth)
            {
                int sourceX = i - destinationOffsetX + sourceOffsetX;

                Assert.AreEqual(expected[sourceX], result[i]);
            }
            else
            {
                Assert.AreEqual(0, result[i]);
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture1D<>))]
    [Resource(typeof(ReadWriteTexture1D<>))]
    [AdditionalResource(typeof(ReadOnlyTexture1D<>))]
    [AdditionalResource(typeof(ReadWriteTexture1D<>))]
    [Data(1024, 1024, 1024, 0, 2055)]
    [Data(1024, 1024, 1024, 0, 3920)]
    [Data(1024, 1024, 1024, 0, -1)]
    [Data(1024, 1024, 1024, 0, int.MaxValue)]
    [Data(512, 1024, 0, 0, 768)]
    [Data(512, 1024, 0, 0, 620)]
    [Data(1024, 512, 512, 0, 768)]
    [Data(1024, 512, 512, 0, 620)]
    [Data(512, 512, 450, 0, 128)]
    [Data(512, 512, 0, 450, 128)]
    [Data(512, 512, -1, 0, 128)]
    [Data(512, 512, 0, -1, 128)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyFrom_TextureToVoid_Fail(
        Device device,
        Type sourceType,
        Type destinationType,
        int sourceWidth,
        int destinationWidth,
        int sourceOffsetX,
        int destinationOffsetX,
        int copyWidth)
    {
        using Texture1D<float> source = device.Get().AllocateTexture1D<float>(sourceType, sourceWidth);
        using Texture1D<float> destination = device.Get().AllocateTexture1D<float>(destinationType, destinationWidth, AllocationMode.Clear);

        destination.CopyFrom(source, sourceOffsetX, destinationOffsetX, copyWidth);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadOnlyTexture1D(Device device)
    {
        int[] data = Enumerable.Range(0, 256).ToArray();

        using ReadOnlyTexture1D<int> source = device.Get().AllocateReadOnlyTexture1D(data);
        using ReadWriteBuffer<int> destination = device.Get().AllocateReadWriteBuffer<int>(data.Length);

        device.Get().For(source.Width, new ReadOnlyTexture1DKernel(source, destination));

        int[] result = destination.ToArray();

        CollectionAssert.AreEqual(data, result);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture1DKernel : IComputeShader
    {
        public readonly ReadOnlyTexture1D<int> source;
        public readonly ReadWriteBuffer<int> destination;

        public void Execute()
        {
            this.destination[ThreadIds.X] = this.source[ThreadIds.X];
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadWriteTexture1D(Device device)
    {
        int[] data = Enumerable.Range(0, 256).ToArray();

        using ReadWriteTexture1D<int> source = device.Get().AllocateReadWriteTexture1D(data);
        using ReadWriteTexture1D<int> destination = device.Get().AllocateReadWriteTexture1D<int>(256);

        device.Get().For(source.Width, new ReadWriteTexture1DKernel(source, destination));

        int[] result = new int[data.Length];

        destination.CopyTo(result);

        CollectionAssert.AreEqual(data, result);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture1DKernel : IComputeShader
    {
        public readonly ReadWriteTexture1D<int> source;
        public readonly ReadWriteTexture1D<int> destination;

        public void Execute()
        {
            this.destination[ThreadIds.X] = this.source[ThreadIds.X];
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Dispatch_ReadWriteTexture1D_AsInterface_FromReadOnly(Device device)
    {
        float[] data = Enumerable.Range(0, 256).Select(static i => (float)i).ToArray();

        using ReadOnlyTexture1D<float> source = device.Get().AllocateReadOnlyTexture1D(data);
        using ReadWriteBuffer<float> buffer1 = device.Get().AllocateReadWriteBuffer<float>(data.Length);
        using ReadWriteBuffer<float> buffer2 = device.Get().AllocateReadWriteBuffer<float>(data.Length);

        device.Get().For(source.Width, new InterfaceReadOnlyTexture1DKernel(source, buffer1, buffer2));

        float[] result1 = buffer1.ToArray();
        float[] result2 = buffer2.ToArray();

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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct InterfaceReadOnlyTexture1DKernel : IComputeShader
    {
        public readonly IReadOnlyTexture1D<float> source;
        public readonly ReadWriteBuffer<float> buffer1;
        public readonly ReadWriteBuffer<float> buffer2;

        public void Execute()
        {
            this.buffer1[ThreadIds.X] = this.source[ThreadIds.X];
            this.buffer2[ThreadIds.X] = this.source.Sample(ThreadIds.X);
        }
    }
}