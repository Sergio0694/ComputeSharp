using System;
using System.Linq;
using System.Threading.Tasks;
using ComputeSharp.__Internals;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using ComputeSharp.Tests.Helpers;
using Microsoft.Toolkit.HighPerformance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS0618

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("ComputeContext")]
public partial class ComputeContextTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(int))]
    [Data(typeof(Int2))]
    [Data(typeof(Int3))]
    [Data(typeof(Int4))]
    [Data(typeof(uint))]
    [Data(typeof(UInt2))]
    [Data(typeof(UInt3))]
    [Data(typeof(UInt4))]
    [Data(typeof(float))]
    [Data(typeof(Float2))]
    [Data(typeof(Float3))]
    [Data(typeof(Float4))]
    [Data(typeof(Bgra32))]
    [Data(typeof(Rgba32))]
    [Data(typeof(Rgba64))]
    [Data(typeof(R8))]
    [Data(typeof(R16))]
    [Data(typeof(Rg16))]
    [Data(typeof(Rg32))]
    public void Clear_ReadWriteBuffer(Device device, Type type)
    {
        static void Test<T>(Device device)
            where T : unmanaged
        {
            T[] array = new T[128];

            new Random().NextBytes(array.AsSpan().AsBytes());

            using ReadWriteBuffer<T> texture = device.Get().AllocateReadWriteBuffer<T>(array);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Clear(texture);
            }

            texture.CopyTo(array);

            foreach (byte value in array.AsSpan().AsBytes())
            {
                Assert.AreEqual(value, 0);
            }
        }

        TestHelper.Run(Test<int>, type, device);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(int))]
    [Data(typeof(Int2))]
    [Data(typeof(Int3))]
    [Data(typeof(Int4))]
    [Data(typeof(uint))]
    [Data(typeof(UInt2))]
    [Data(typeof(UInt3))]
    [Data(typeof(UInt4))]
    [Data(typeof(float))]
    [Data(typeof(Float2))]
    [Data(typeof(Float3))]
    [Data(typeof(Float4))]
    [Data(typeof(Bgra32))]
    [Data(typeof(Rgba32))]
    [Data(typeof(Rgba64))]
    [Data(typeof(R8))]
    [Data(typeof(R16))]
    [Data(typeof(Rg16))]
    [Data(typeof(Rg32))]
    public void Clear_ReadWriteTexture2D(Device device, Type type)
    {
        static void Test<T>(Device device)
            where T : unmanaged
        {
            if (!device.Get().IsReadWriteTexture2DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T[] array = new T[128 * 128];

            new Random().NextBytes(array.AsSpan().AsBytes());

            using ReadWriteTexture2D<T> texture = device.Get().AllocateReadWriteTexture2D<T>(array, 128, 128);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Clear(texture);
            }

            texture.CopyTo(array);

            foreach (byte value in array.AsSpan().AsBytes())
            {
                Assert.AreEqual(value, 0);
            }
        }

        TestHelper.Run(Test<int>, type, device);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(int))]
    [Data(typeof(Int2))]
    [Data(typeof(Int3))]
    [Data(typeof(Int4))]
    [Data(typeof(uint))]
    [Data(typeof(UInt2))]
    [Data(typeof(UInt3))]
    [Data(typeof(UInt4))]
    [Data(typeof(float))]
    [Data(typeof(Float2))]
    [Data(typeof(Float3))]
    [Data(typeof(Float4))]
    [Data(typeof(Bgra32))]
    [Data(typeof(Rgba32))]
    [Data(typeof(Rgba64))]
    [Data(typeof(R8))]
    [Data(typeof(R16))]
    [Data(typeof(Rg16))]
    [Data(typeof(Rg32))]
    public void Clear_ReadWriteTexture3D(Device device, Type type)
    {
        static void Test<T>(Device device)
            where T : unmanaged
        {
            if (!device.Get().IsReadWriteTexture3DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T[] array = new T[128 * 128 * 3];

            new Random().NextBytes(array.AsSpan().AsBytes());

            using ReadWriteTexture3D<T> texture = device.Get().AllocateReadWriteTexture3D<T>(array, 128, 128, 3);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Clear(texture);
            }

            texture.CopyTo(array);

            foreach (byte value in array.AsSpan().AsBytes())
            {
                Assert.AreEqual(value, 0);
            }
        }

        TestHelper.Run(Test<int>, type, device);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(Bgra32), typeof(Float4))]
    [Data(typeof(Rgba32), typeof(Float4))]
    [Data(typeof(Rgba64), typeof(Float4))]
    [Data(typeof(R8), typeof(float))]
    [Data(typeof(R16), typeof(float))]
    [Data(typeof(Rg16), typeof(Float2))]
    [Data(typeof(Rg32), typeof(Float2))]
    public void Clear_ReadWriteTexture2D_WithPixel(Device device, Type cpuType, Type gpuType)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            if (!device.Get().IsReadWriteTexture2DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T[] array = new T[128 * 128];

            new Random().NextBytes(array.AsSpan().AsBytes());

            using ReadWriteTexture2D<T, TPixel> texture = device.Get().AllocateReadWriteTexture2D<T, TPixel>(array, 128, 128);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Clear(texture);
            }

            texture.CopyTo(array);

            foreach (byte value in array.AsSpan().AsBytes())
            {
                Assert.AreEqual(value, 0);
            }
        }

        TestHelper.Run(Test<Bgra32, Float4>, cpuType, gpuType, device);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(Bgra32), typeof(Float4))]
    [Data(typeof(Rgba32), typeof(Float4))]
    [Data(typeof(Rgba64), typeof(Float4))]
    [Data(typeof(R8), typeof(float))]
    [Data(typeof(R16), typeof(float))]
    [Data(typeof(Rg16), typeof(Float2))]
    [Data(typeof(Rg32), typeof(Float2))]
    public void Clear_ReadWriteTexture3D_WithPixel(Device device, Type cpuType, Type gpuType)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            if (!device.Get().IsReadWriteTexture2DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T[] array = new T[128 * 128 * 3];

            new Random().NextBytes(array.AsSpan().AsBytes());

            using ReadWriteTexture3D<T, TPixel> texture = device.Get().AllocateReadWriteTexture3D<T, TPixel>(array, 128, 128, 3);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Clear(texture);
            }

            texture.CopyTo(array);

            foreach (byte value in array.AsSpan().AsBytes())
            {
                Assert.AreEqual(value, 0);
            }
        }

        TestHelper.Run(Test<Bgra32, Float4>, cpuType, gpuType, device);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(Bgra32), typeof(Float4))]
    [Data(typeof(Rgba32), typeof(Float4))]
    [Data(typeof(Rgba64), typeof(Float4))]
    [Data(typeof(R8), typeof(float))]
    [Data(typeof(R16), typeof(float))]
    [Data(typeof(Rg16), typeof(Float2))]
    [Data(typeof(Rg32), typeof(Float2))]
    public void Clear_ReadWriteTexture2D_WithPixel_AsNormalizedTexture(Device device, Type cpuType, Type gpuType)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            if (!device.Get().IsReadWriteTexture2DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T[] array = new T[128 * 128];

            new Random().NextBytes(array.AsSpan().AsBytes());

            using ReadWriteTexture2D<T, TPixel> texture = device.Get().AllocateReadWriteTexture2D<T, TPixel>(array, 128, 128);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Clear((IReadWriteTexture2D<TPixel>)texture);
            }

            texture.CopyTo(array);

            foreach (byte value in array.AsSpan().AsBytes())
            {
                Assert.AreEqual(value, 0);
            }
        }

        TestHelper.Run(Test<Bgra32, Float4>, cpuType, gpuType, device);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(Bgra32), typeof(Float4))]
    [Data(typeof(Rgba32), typeof(Float4))]
    [Data(typeof(Rgba64), typeof(Float4))]
    [Data(typeof(R8), typeof(float))]
    [Data(typeof(R16), typeof(float))]
    [Data(typeof(Rg16), typeof(Float2))]
    [Data(typeof(Rg32), typeof(Float2))]
    public void Clear_ReadWriteTexture3D_WithPixel_AsNormalizedTexture(Device device, Type cpuType, Type gpuType)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IUnorm<TPixel>
            where TPixel : unmanaged
        {
            if (!device.Get().IsReadWriteTexture2DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T[] array = new T[128 * 128 * 3];

            new Random().NextBytes(array.AsSpan().AsBytes());

            using ReadWriteTexture3D<T, TPixel> texture = device.Get().AllocateReadWriteTexture3D<T, TPixel>(array, 128, 128, 3);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Clear((IReadWriteTexture3D<TPixel>)texture);
            }

            texture.CopyTo(array);

            foreach (byte value in array.AsSpan().AsBytes())
            {
                Assert.AreEqual(value, 0);
            }
        }

        TestHelper.Run(Test<Bgra32, Float4>, cpuType, gpuType, device);
    }

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
    public void ForAndForEach_Batched_Parallel_WithParallelFor(Device device)
    {
        void Test(Device device)
        {
            for (int i = 0; i < 20; i++)
            {
                ForAndForEach_Batched(device);
            }
        }

        Parallel.For(0, 64, _ => Test(device));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Batched_Parallel_WithTaskRun(Device device)
    {
        void Test(Device device)
        {
            for (int i = 0; i < 20; i++)
            {
                ForAndForEach_Batched(device);
            }
        }

        await Task.Run(() => Test(device));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched(Device device)
    {
        int[] array = Enumerable.Range(0, 1024).ToArray();

        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer(array);
        using ReadWriteTexture2D<Rgba32, float4> texture = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(128, 128);

        await using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.For(512, new OffsetComputeShader(buffer, 0));
            context.For(64, new OffsetComputeShader(buffer, 512));
            context.ForEach(texture, default(ClearPixelShader));
            context.For(299, new OffsetComputeShader(buffer, 576));
            context.Barrier(texture);
            context.ForEach(texture, new ColorPixelShader(new float4(0.22f, 0.44f, 0.66f, 0.88f)));
            context.For(149, new OffsetComputeShader(buffer, 875));
        }

        void Validate()
        {
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

        Validate();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched_WithTaskRun(Device device)
    {
        await Task.Run(() => ForAndForEach_Async_Batched(device));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched_Parallel(Device device)
    {
        async Task TestAsync(Device device)
        {
            for (int i = 0; i < 20; i++)
            {
                await ForAndForEach_Async_Batched(device);
            }
        }

        await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => TestAsync(device)));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched_Parallel_WithConfigureAwait(Device device)
    {
        async Task TestAsync(Device device)
        {
            for (int i = 0; i < 20; i++)
            {
                await ForAndForEach_Async_Batched(device).ConfigureAwait(false);
            }
        }

        await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => TestAsync(device)));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched_Parallel_WithTaskRun(Device device)
    {
        async Task TestAsync(Device device)
        {
            for (int i = 0; i < 20; i++)
            {
                await ForAndForEach_Async_Batched(device);
            }
        }

        await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => Task.Run(() => TestAsync(device))));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched_Parallel_WithConfigureAwaitAndTaskRun(Device device)
    {
        async Task TestAsync(Device device)
        {
            for (int i = 0; i < 20; i++)
            {
                await ForAndForEach_Async_Batched(device).ConfigureAwait(false);
            }
        }

        await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => Task.Run(() => TestAsync(device))));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched_Parallel_Multiple(Device device)
    {
        async Task TestAsync(Device device)
        {
            for (int i = 0; i < 10; i++)
            {
                Task task1 = ForAndForEach_Async_Batched(device);
                Task task2 = ForAndForEach_Async_Batched(device);
                Task task3 = ForAndForEach_Async_Batched(device);

                await Task.WhenAll(task1, task2, task3);
            }
        }

        await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => TestAsync(device)));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched_Parallel_Multiple_WithConfigureAwait(Device device)
    {
        async Task TestAsync(Device device)
        {
            for (int i = 0; i < 10; i++)
            {
                Task task1 = ForAndForEach_Async_Batched(device);
                Task task2 = ForAndForEach_Async_Batched(device);
                Task task3 = ForAndForEach_Async_Batched(device);

                await Task.WhenAll(task1, task2, task3).ConfigureAwait(false);
            }
        }

        await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => TestAsync(device)));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched_Parallel_Multiple_WithTaskRun(Device device)
    {
        async Task TestAsync(Device device)
        {
            for (int i = 0; i < 10; i++)
            {
                Task task1 = ForAndForEach_Async_Batched(device);
                Task task2 = ForAndForEach_Async_Batched(device);
                Task task3 = ForAndForEach_Async_Batched(device);

                await Task.WhenAll(task1, task2, task3);
            }
        }

        await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => Task.Run(() => TestAsync(device))));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public async Task ForAndForEach_Async_Batched_Parallel_Multiple_WithConfigureAwaitAndTaskRun(Device device)
    {
        async Task TestAsync(Device device)
        {
            for (int i = 0; i < 10; i++)
            {
                Task task1 = ForAndForEach_Async_Batched(device);
                Task task2 = ForAndForEach_Async_Batched(device);
                Task task3 = ForAndForEach_Async_Batched(device);

                await Task.WhenAll(task1, task2, task3).ConfigureAwait(false);
            }
        }

        await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => Task.Run(() => TestAsync(device))));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
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
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
    public void For_AfterDispose_ThrowsException(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(128);

        ComputeContext context = device.Get().CreateComputeContext();

        context.Dispose();
        context.For(512, new OffsetComputeShader(buffer, 0));

        Assert.Fail();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
    public async Task For_AfterDisposeAsync_ThrowsException(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(128);

        ComputeContext context = device.Get().CreateComputeContext();

        await context.DisposeAsync();

        context.For(512, new OffsetComputeShader(buffer, 0));

        Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
    public void DefaultContext_GetGraphicsDevice_ThrowsException()
    {
        _ = default(ComputeContext).GraphicsDevice;
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
    public void DefaultContext_Barrier_ThrowsException(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(64);

        using ComputeContext context = default;

        context.Barrier(buffer);

        Assert.Fail();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
    public void DefaultContext_Clear_ThrowsException(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(64);

        using ComputeContext context = default;

        context.Clear(buffer);

        Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
    public void DefaultContext_For_ThrowsException()
    {
        using ComputeContext context = default;

        context.For(32, 1, 1, default(OffsetComputeShader));

        Assert.Fail();
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
    public void DefaultContext_ForEach_ThrowsException(Device device)
    {
        using ReadWriteTexture2D<Rgba32, float4> texture = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(128, 128);

        using ComputeContext context = default;

        context.ForEach(texture, default(ColorPixelShader));

        Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
    public void DefaultContext_Dispose_ThrowsException()
    {
        using ComputeContext context = default;
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
    public async Task DefaultContext_DisposeAsync_ThrowsException()
    {
        await using ComputeContext context = default;
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
