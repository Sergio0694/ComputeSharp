using System;
using System.Linq;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Internals;

[TestClass]
public partial class DebugViewTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ConstantBuffer<>))]
    [Resource(typeof(ReadOnlyBuffer<>))]
    [Resource(typeof(ReadWriteBuffer<>))]
    [Data(32)]
    [Data(256)]
    [Data(1024)]
    public void BufferDebugView_NotNull(Device device, Type bufferType, int size)
    {
        float[] data = Enumerable.Range(0, size).Select(static i => (float)i).ToArray();

        using Buffer<float> buffer = device.Get().AllocateBuffer(bufferType, data);

        BufferDebugView<float> view = new(buffer);

        Assert.IsNotNull(view.Items);

        CollectionAssert.AreEqual(view.Items, data);
    }

    [TestMethod]
    public void BufferDebugView_Null()
    {
        using Buffer<float> buffer = null!;

        BufferDebugView<float> view = new(buffer);

        Assert.IsNull(view.Items);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    [Data(32, 32)]
    [Data(1024, 1024)]
    [Data(667, 480)]
    public void Texture2DDebugView_NotNull(Device device, Type textureType, int width, int height)
    {
        float[] data = Enumerable.Range(0, width * height).Select(static i => (float)i).ToArray();

        using Texture2D<float> texture = device.Get().AllocateTexture2D(textureType, data, width, height);

        Texture2DDebugView<float> view = new(texture);

        Assert.IsNotNull(view.Items);
        Assert.AreEqual(view.Items.GetLength(0), height);
        Assert.AreEqual(view.Items.GetLength(1), width);

        CollectionAssert.AreEqual(view.Items, data);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<,>))]
    [Resource(typeof(ReadWriteTexture2D<,>))]
    [Data(32, 32)]
    [Data(512, 512)]
    [Data(667, 480)]
    public void Texture2DDebugView_WithTPixel_NotNull(Device device, Type textureType, int width, int height)
    {
        Random random = new(42);
        Rgba32[,] data = new Rgba32[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                data[i, j] = new Rgba32((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
            }
        }

        using Texture2D<Rgba32> texture = device.Get().AllocateTexture2D<Rgba32, float4>(textureType, width, height);

        texture.CopyFrom(data);

        Texture2DDebugView<Rgba32> view = new(texture);

        Assert.IsNotNull(view.Items);
        Assert.AreEqual(view.Items.GetLength(0), height);
        Assert.AreEqual(view.Items.GetLength(1), width);

        CollectionAssert.AreEqual(view.Items, data);
    }

    [TestMethod]
    public void Texture2DDebugView_Null()
    {
        using Texture2D<float> texture = null!;

        Texture2DDebugView<float> view = new(texture);

        Assert.IsNull(view.Items);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    [Data(32, 32, 4)]
    [Data(1024, 1024, 3)]
    [Data(667, 480, 7)]
    public void Texture3DDebugView_NotNull(Device device, Type textureType, int width, int height, int depth)
    {
        float[] data = Enumerable.Range(0, width * height * depth).Select(static i => (float)i).ToArray();

        using Texture3D<float> texture = device.Get().AllocateTexture3D(textureType, data, width, height, depth);

        Texture3DDebugView<float> view = new(texture);

        Assert.IsNotNull(view.Items);
        Assert.AreEqual(view.Items.GetLength(0), depth);
        Assert.AreEqual(view.Items.GetLength(1), height);
        Assert.AreEqual(view.Items.GetLength(2), width);

        CollectionAssert.AreEqual(view.Items, data);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<,>))]
    [Resource(typeof(ReadWriteTexture3D<,>))]
    [Data(32, 32, 4)]
    [Data(1024, 1024, 3)]
    [Data(667, 480, 7)]
    public void Texture3DDebugView_WithTPixel_NotNull(Device device, Type textureType, int width, int height, int depth)
    {
        Random random = new(42);
        Rgba32[,,] data = new Rgba32[depth, height, width];

        for (int k = 0; k < depth; k++)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[k, i, j] = new Rgba32((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
                }
            }
        }

        using Texture3D<Rgba32> texture = device.Get().AllocateTexture3D<Rgba32, float4>(textureType, width, height, depth);

        texture.CopyFrom(data);

        Texture3DDebugView<Rgba32> view = new(texture);

        Assert.IsNotNull(view.Items);
        Assert.AreEqual(view.Items.GetLength(0), depth);
        Assert.AreEqual(view.Items.GetLength(1), height);
        Assert.AreEqual(view.Items.GetLength(2), width);

        CollectionAssert.AreEqual(view.Items, data);
    }

    [TestMethod]
    public void Texture3DDebugView_Null()
    {
        using Texture3D<float> texture = null!;

        Texture3DDebugView<float> view = new(texture);

        Assert.IsNull(view.Items);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadBuffer<>))]
    [Resource(typeof(ReadBackBuffer<>))]
    [Data(32)]
    [Data(1024)]
    [Data(667)]
    public void TransferBufferDebugView_NotNull(Device device, Type bufferType, int size)
    {
        float[] data = Enumerable.Range(0, size).Select(static i => (float)i).ToArray();

        using TransferBuffer<float> buffer = device.Get().AllocateTransferBuffer<float>(bufferType, size);

        data.AsSpan().CopyTo(buffer.Span);

        TransferBufferDebugView<float> view = new(buffer);

        Assert.IsNotNull(view.Items);

        CollectionAssert.AreEqual(view.Items, data);
    }

    [TestMethod]
    public void TransferBufferDebugView_Null()
    {
        using TransferBuffer<float> buffer = null!;

        TransferBufferDebugView<float> view = new(buffer);

        Assert.IsNull(view.Items);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture2D<>))]
    [Resource(typeof(ReadBackTexture2D<>))]
    [Data(32, 32)]
    [Data(1024, 1024)]
    [Data(667, 480)]
    public void TransferTexture2DDebugView_NotNull(Device device, Type textureType, int width, int height)
    {
        Random random = new(42);
        float[,] data = new float[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                data[i, j] = (float)random.NextDouble();
            }
        }

        using TransferTexture2D<float> texture = device.Get().AllocateTransferTexture2D<float>(textureType, width, height);

        for (int i = 0; i < height; i++)
        {
            Span<float> row = texture.View.GetRowSpan(i);

            for (int j = 0; j < width; j++)
            {
                row[j] = data[i, j];
            }
        }

        TransferTexture2DDebugView<float> view = new(texture);

        Assert.IsNotNull(view.Items);
        Assert.AreEqual(view.Items.GetLength(0), height);
        Assert.AreEqual(view.Items.GetLength(1), width);

        CollectionAssert.AreEqual(view.Items, data);
    }

    [TestMethod]
    public void TransferTexture2DDebugView_Null()
    {
        using Texture2D<float> texture = null!;

        Texture2DDebugView<float> view = new(texture);

        Assert.IsNull(view.Items);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture3D<>))]
    [Resource(typeof(ReadBackTexture3D<>))]
    [Data(32, 32, 4)]
    [Data(1024, 1024, 3)]
    [Data(667, 480, 7)]
    public void TransferTexture3DDebugView_NotNull(Device device, Type textureType, int width, int height, int depth)
    {
        Random random = new(42);
        float[,,] data = new float[depth, height, width];

        for (int k = 0; k < depth; k++)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[k, i, j] = (float)random.NextDouble();
                }
            }
        }

        using TransferTexture3D<float> texture = device.Get().AllocateTransferTexture3D<float>(textureType, width, height, depth);

        for (int k = 0; k < depth; k++)
        {
            for (int i = 0; i < height; i++)
            {
                Span<float> row = texture.View.GetRowSpan(i, k);

                for (int j = 0; j < width; j++)
                {
                    row[j] = data[k, i, j];
                }
            }
        }

        TransferTexture3DDebugView<float> view = new(texture);

        Assert.IsNotNull(view.Items);
        Assert.AreEqual(view.Items.GetLength(0), depth);
        Assert.AreEqual(view.Items.GetLength(1), height);
        Assert.AreEqual(view.Items.GetLength(2), width);

        CollectionAssert.AreEqual(view.Items, data);
    }

    [TestMethod]
    public void TransferTexture3DDebugView_Null()
    {
        using Texture3D<float> texture = null!;

        Texture3DDebugView<float> view = new(texture);

        Assert.IsNull(view.Items);
    }
}