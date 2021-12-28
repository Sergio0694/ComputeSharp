using System;
using System.Linq;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("ComputeContext")]
public partial class ComputeContextTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public void For_Batched(Device device)
    {
        int[] array = Enumerable.Range(0, 1024).ToArray();

        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer(array);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.For(512, new OffsetComputeShader(buffer, 0));
            context.For(64, new OffsetComputeShader(buffer, 512));
            context.For(299, new OffsetComputeShader(buffer, 576));
            context.For(149, new OffsetComputeShader(buffer, 875));
        }

        int[] result = buffer.ToArray();

        foreach (ref int x in array.AsSpan())
        {
            x *= 2;
        }

        CollectionAssert.AreEqual(array, result);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void ForEach_Batched(Device device)
    {
        using ReadWriteTexture2D<Rgba32, float4> texture = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(128, 128);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.ForEach(texture, default(ClearPixelShader));
            context.Barrier(texture);
            context.ForEach(texture, new ColorPixelShader(new float4(0.22f, 0.44f, 0.66f, 0.88f)));
        }

        Rgba32[,] result = texture.ToArray();

        foreach (Rgba32 pixel in result)
        {
            Assert.AreEqual(pixel.R, 56);
            Assert.AreEqual(pixel.G, 112);
            Assert.AreEqual(pixel.B, 168);
            Assert.AreEqual(pixel.A, 224);
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void ForAndForEach_Batched(Device device)
    {
        int[] array = Enumerable.Range(0, 1024).ToArray();

        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer(array);
        using ReadWriteTexture2D<Rgba32, float4> texture = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(128, 128);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.For(512, new OffsetComputeShader(buffer, 0));
            context.For(64, new OffsetComputeShader(buffer, 512));
            context.ForEach(texture, default(ClearPixelShader));
            context.For(299, new OffsetComputeShader(buffer, 576));
            context.Barrier(texture);
            context.ForEach(texture, new ColorPixelShader(new float4(0.22f, 0.44f, 0.66f, 0.88f)));
            context.For(149, new OffsetComputeShader(buffer, 875));
        }

        // Buffer test
        {
            int[] result = buffer.ToArray();

            foreach (ref int x in array.AsSpan())
            {
                x *= 2;
            }

            CollectionAssert.AreEqual(array, result);
        }

        // Texture test
        {
            Rgba32[,] result = texture.ToArray();

            foreach (Rgba32 pixel in result)
            {
                Assert.AreEqual(pixel.R, 56);
                Assert.AreEqual(pixel.G, 112);
                Assert.AreEqual(pixel.B, 168);
                Assert.AreEqual(pixel.A, 224);
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Barrier_WithoutPreviousDispatches_ThrowsException(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(64);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.Barrier(buffer);
        }

        Assert.Fail();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public void DefaultContext_Barrier_ThrowsException(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(64);

        using ComputeContext context = default;

        context.Barrier(buffer);

        Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void DefaultContext_For_ThrowsException()
    {
        using ComputeContext context = default;

        context.For(32, 1, 1, default(OffsetComputeShader));

        Assert.Fail();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException))]
    public void DefaultContext_ForEach_ThrowsException(Device device)
    {
        using ReadWriteTexture2D<Rgba32, float4> texture = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(128, 128);

        using ComputeContext context = default;

        context.ForEach(texture, default(ColorPixelShader));

        Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void DefaultContext_Dispose_ThrowsException()
    {
        using ComputeContext context = default;
    }

    [AutoConstructor]
    internal readonly partial struct OffsetComputeShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;
        public readonly int offset;

        /// <inheritdoc/>
        public void Execute()
        {
            buffer[offset + ThreadIds.X] *= 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct ClearPixelShader : IPixelShader<float4>
    {
        /// <inheritdoc/>
        public float4 Execute()
        {
            return default;
        }
    }

    [AutoConstructor]
    internal readonly partial struct ColorPixelShader : IPixelShader<float4>
    {
        public readonly float4 color;

        /// <inheritdoc/>
        public float4 Execute()
        {
            return color;
        }
    }
}
