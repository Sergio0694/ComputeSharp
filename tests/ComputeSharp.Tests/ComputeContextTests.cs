using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

#pragma warning disable IDE0047

namespace ComputeSharp.Tests;

[TestClass]
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
            where T : unmanaged, IPixel<T, TPixel>
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
            where T : unmanaged, IPixel<T, TPixel>
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
            where T : unmanaged, IPixel<T, TPixel>
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
                context.Clear((IReadWriteNormalizedTexture2D<TPixel>)texture);
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
            where T : unmanaged, IPixel<T, TPixel>
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
                context.Clear((IReadWriteNormalizedTexture3D<TPixel>)texture);
            }

            texture.CopyTo(array);

            foreach (byte value in array.AsSpan().AsBytes())
            {
                Assert.AreEqual(value, 0);
            }
        }

        TestHelper.Run(Test<Bgra32, Float4>, cpuType, gpuType, device);
    }

    /// <summary>
    /// Gets a sample pixel color of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of pixel to get.</typeparam>
    /// <returns>A sample pixel color of the specified type.</returns>
    /// <exception cref="ArgumentException">Thrown when <typeparamref name="T"/> isn't valid.</exception>
    private static T GetColor<T>()
    {
        if (typeof(T) == typeof(Bgra32))
        {
            return (T)(object)new Bgra32(0x6B, 0x6D, 0x9E, 0xC0);
        }

        if (typeof(T) == typeof(Rgba32))
        {
            return (T)(object)new Rgba32(0x6B, 0x6D, 0x9E, 0xC0);
        }

        if (typeof(T) == typeof(Rgba64))
        {
            return (T)(object)new Rgba64(0x6B, 0x6D05, 0x9E, 0xC0);
        }

        if (typeof(T) == typeof(R8))
        {
            return (T)(object)new R8(0x6B);
        }

        if (typeof(T) == typeof(R16))
        {
            return (T)(object)new R16(0x6D05);
        }

        if (typeof(T) == typeof(Rg16))
        {
            return (T)(object)new Rg16(0x6B, 0x6D);
        }

        if (typeof(T) == typeof(Rg32))
        {
            return (T)(object)new Rg32(0x6B, 0x6D05);
        }

        throw new ArgumentException("Invalid pixel type");
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
    public void Fill_ReadWriteTexture2D_WithPixel(Device device, Type cpuType, Type gpuType)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IPixel<T, TPixel>
            where TPixel : unmanaged
        {
            if (!device.Get().IsReadWriteTexture2DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T color = GetColor<T>();

            using ReadWriteTexture2D<T, TPixel> texture = device.Get().AllocateReadWriteTexture2D<T, TPixel>(128, 128);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Fill(texture, color);
            }

            T[,] result = texture.ToArray();

            foreach (T value in result)
            {
                Assert.AreEqual(value, color);
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
    public void Fill_ReadWriteTexture3D_WithPixel(Device device, Type cpuType, Type gpuType)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IPixel<T, TPixel>
            where TPixel : unmanaged
        {
            if (!device.Get().IsReadWriteTexture2DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T color = GetColor<T>();

            using ReadWriteTexture3D<T, TPixel> texture = device.Get().AllocateReadWriteTexture3D<T, TPixel>(128, 128, 3);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Fill(texture, color);
            }

            T[,,] result = texture.ToArray();

            foreach (T value in result)
            {
                Assert.AreEqual(value, color);
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
    public void Fill_ReadWriteTexture2D_WithPixel_AsNormalizedTexture(Device device, Type cpuType, Type gpuType)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IPixel<T, TPixel>
            where TPixel : unmanaged
        {
            if (!device.Get().IsReadWriteTexture2DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T color = GetColor<T>();

            using ReadWriteTexture2D<T, TPixel> texture = device.Get().AllocateReadWriteTexture2D<T, TPixel>(128, 128);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Fill((IReadWriteNormalizedTexture2D<TPixel>)texture, color.ToPixel());
            }

            T[,] result = texture.ToArray();

            foreach (T value in result)
            {
                Assert.AreEqual(value, color);
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
    public void Fill_ReadWriteTexture3D_WithPixel_AsNormalizedTexture(Device device, Type cpuType, Type gpuType)
    {
        static void Test<T, TPixel>(Device device)
            where T : unmanaged, IPixel<T, TPixel>
            where TPixel : unmanaged
        {
            if (!device.Get().IsReadWriteTexture2DSupportedForType<T>())
            {
                Assert.Inconclusive();
            }

            T color = GetColor<T>();

            using ReadWriteTexture3D<T, TPixel> texture = device.Get().AllocateReadWriteTexture3D<T, TPixel>(128, 128, 3);

            using (ComputeContext context = device.Get().CreateComputeContext())
            {
                context.Fill((IReadWriteNormalizedTexture3D<TPixel>)texture, color.ToPixel());
            }

            T[,,] result = texture.ToArray();

            foreach (T value in result)
            {
                Assert.AreEqual(value, color);
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

        _ = device.Get();

        _ = Parallel.For(0, 64, _ => Test(device));
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

        _ = device.Get();

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
        _ = device.Get();

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

        _ = device.Get();

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

        _ = device.Get();

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

        _ = device.Get();

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

        _ = device.Get();

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

        _ = device.Get();

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

        _ = device.Get();

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

        _ = device.Get();

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

        _ = device.Get();

        await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => Task.Run(() => TestAsync(device))));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Transition_ReadWriteTexture2D(Device device)
    {
        _ = device.Get();

        string imagingPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");

        using Image<ImageSharpRgba32> sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024Sampling.png"));

        using ReadOnlyTexture2D<Rgba32, float4> source = device.Get().LoadReadOnlyTexture2D<Rgba32, float4>(Path.Combine(imagingPath, "city.jpg"));
        using ReadWriteTexture2D<float4> sourceAsFloat4 = device.Get().AllocateReadWriteTexture2D<float4>(source.Width, source.Height);
        using ReadWriteTexture2D<Rgba32, float4> destination = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(sampled.Width, sampled.Height);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.For(source.Width, source.Height, new ConvertToNonNormalized2DShader(source, sourceAsFloat4));
            context.Transition(sourceAsFloat4, ResourceState.ReadOnly);
            context.ForEach(destination, new LinearSampling2DPixelShader(sourceAsFloat4.AsReadOnly()));
            context.Transition(sourceAsFloat4, ResourceState.ReadWrite);
        }

        using Image<ImageSharpRgba32> processed = destination.ToImage<Rgba32, ImageSharpRgba32>();

        TolerantImageComparer.AssertEqual(sampled, processed, 0.00000174f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Transition_ReadWriteTexture2D_Normalized(Device device)
    {
        _ = device.Get();

        string imagingPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");

        using Image<ImageSharpRgba32> sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024Sampling.png"));

        using ReadWriteTexture2D<Rgba32, float4> source = device.Get().LoadReadWriteTexture2D<Rgba32, float4>(Path.Combine(imagingPath, "city.jpg"));
        using ReadWriteTexture2D<Rgba32, float4> destination = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(sampled.Width, sampled.Height);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.Transition(source, ResourceState.ReadOnly);
            context.ForEach(destination, new LinearSamplingFromNormalized2DPixelShader(source.AsReadOnly()));
            context.Transition(source, ResourceState.ReadWrite);
        }

        using Image<ImageSharpRgba32> processed = destination.ToImage<Rgba32, ImageSharpRgba32>();

        TolerantImageComparer.AssertEqual(sampled, processed, 0.0000017f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Transition_ReadWriteTexture2D_Normalized_WithWriteToo(Device device)
    {
        _ = device.Get();

        string imagingPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");

        using Image<ImageSharpRgba32> sampled1 = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024Sampling.png"));
        using Image<ImageSharpRgba32> sampled2 = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024SamplingInverted.png"));

        using ReadOnlyTexture2D<Rgba32, float4> image = device.Get().LoadReadOnlyTexture2D<Rgba32, float4>(Path.Combine(imagingPath, "city.jpg"));
        using ReadWriteTexture2D<Rgba32, float4> source = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(image.Width, image.Height);
        using ReadWriteTexture2D<Rgba32, float4> destination1 = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(sampled1.Width, sampled1.Height);
        using ReadWriteTexture2D<Rgba32, float4> destination2 = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(sampled1.Width, sampled1.Height);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            // Dispatch before any transitions, and UAV barrier test
            context.ForEach<ClearPixelShader, float4>(source);
            context.Barrier(source);

            // Dispatch following a UAV barrier
            context.ForEach(source, new CopyToOutputPixelShader(image));

            // Transition to readonly and first use as readonly resource
            context.Transition(source, ResourceState.ReadOnly);
            context.ForEach(destination1, new LinearSamplingFromNormalized2DPixelShader(source.AsReadOnly()));

            // Transition back and write to it
            context.Transition(source, ResourceState.ReadWrite);
            context.ForEach(source, new InvertPixelShader(source));

            // Transition once again to readonly and final use as readonly resource
            context.Transition(source, ResourceState.ReadOnly);
            context.ForEach(destination2, new LinearSamplingFromNormalized2DPixelShader(source.AsReadOnly()));
        }

        using Image<ImageSharpRgba32> processed1 = destination1.ToImage<Rgba32, ImageSharpRgba32>();
        using Image<ImageSharpRgba32> processed2 = destination2.ToImage<Rgba32, ImageSharpRgba32>();

        TolerantImageComparer.AssertEqual(sampled1, processed1, 0.0000017f);
        TolerantImageComparer.AssertEqual(sampled2, processed2, 0.0000028f);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Transition_ReadWriteTexture3D(Device device)
    {
        _ = device.Get();

        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");
        string originalPath = Path.Combine(assetsPath, "CityWith1920x1280Resizing.png");
        string sampledPath = Path.Combine(assetsPath, "CityAfter1024x1024SamplingFrom1920x1080.png");

        ImageInfo imageInfo = Image.Identify(originalPath);

        using Image<ImageSharpRgba32> sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, sampledPath));

        using ReadWriteTexture3D<float4> sourceAsFloat4 = device.Get().AllocateReadWriteTexture3D<float4>(imageInfo.Width, imageInfo.Height, 3);

        using (UploadTexture2D<Rgba32> upload2D = device.Get().LoadUploadTexture2D<Rgba32>(originalPath))
        using (UploadTexture3D<float4> upload3D = device.Get().AllocateUploadTexture3D<float4>(upload2D.Width, upload2D.Height, 2))
        {
            for (int z = 0; z < upload3D.Depth; z++)
            {
                TextureView2D<float4> layer = upload3D.View.GetDepthView(z);

                for (int y = 0; y < layer.Height; y++)
                {
                    for (int x = 0; x < layer.Width; x++)
                    {
                        layer[x, y] = upload2D.View[x, y].ToPixel();
                    }
                }
            }

            upload3D.CopyTo(sourceAsFloat4);
        }

        using ReadWriteTexture3D<Rgba32, float4> destination = device.Get().AllocateReadWriteTexture3D<Rgba32, float4>(sampled.Width, sampled.Height, 2);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.Transition(sourceAsFloat4, ResourceState.ReadOnly);
            context.For(destination.Width, destination.Height, destination.Depth, new LinearSampling3DComputeShader(sourceAsFloat4.AsReadOnly(), destination));
            context.Transition(sourceAsFloat4, ResourceState.ReadWrite);
        }

        for (int z = 0; z < destination.Depth; z++)
        {
            using Image<ImageSharpRgba32> processed = destination.ToImage<Rgba32, ImageSharpRgba32>(depth: z);

            TolerantImageComparer.AssertEqual(sampled, processed, 0.000012f);
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Transition_ReadWriteTexture3D_Normalized(Device device)
    {
        _ = device.Get();

        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");
        string originalPath = Path.Combine(assetsPath, "CityWith1920x1280Resizing.png");
        string sampledPath = Path.Combine(assetsPath, "CityAfter1024x1024SamplingFrom1920x1080.png");

        ImageInfo imageInfo = Image.Identify(originalPath);

        using Image<ImageSharpRgba32> sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, sampledPath));

        using ReadWriteTexture3D<Rgba32, float4> source = device.Get().AllocateReadWriteTexture3D<Rgba32, float4>(imageInfo.Width, imageInfo.Height, 3);

        using (UploadTexture2D<Rgba32> upload2D = device.Get().LoadUploadTexture2D<Rgba32>(originalPath))
        using (UploadTexture3D<Rgba32> upload3D = device.Get().AllocateUploadTexture3D<Rgba32>(upload2D.Width, upload2D.Height, 2))
        {
            for (int z = 0; z < upload3D.Depth; z++)
            {
                upload2D.View.CopyTo(upload3D.View.GetDepthView(z));
            }

            upload3D.CopyTo(source);
        }

        using ReadWriteTexture3D<Rgba32, float4> destination = device.Get().AllocateReadWriteTexture3D<Rgba32, float4>(sampled.Width, sampled.Height, 2);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.Transition(source, ResourceState.ReadOnly);
            context.For(destination.Width, destination.Height, destination.Depth, new LinearSamplingFromNormalized3DComputeShader(source.AsReadOnly(), destination));
            context.Transition(source, ResourceState.ReadWrite);
        }

        for (int z = 0; z < destination.Depth; z++)
        {
            using Image<ImageSharpRgba32> processed = destination.ToImage<Rgba32, ImageSharpRgba32>(depth: z);

            TolerantImageComparer.AssertEqual(sampled, processed, 0.000012f);
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void TransitionAndWrites_ReadWriteTexture2D_Normalized(Device device)
    {
        _ = device.Get();

        string imagingPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging");
        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");

        using Image<ImageSharpRgba32> sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, "CityAfter1024x1024SamplingAndDashing.png"));

        using ReadWriteTexture2D<Rgba32, float4> source = device.Get().LoadReadWriteTexture2D<Rgba32, float4>(Path.Combine(imagingPath, "city.jpg"));
        using ReadWriteTexture2D<Rgba32, float4> destination = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(sampled.Width, sampled.Height);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.For(source.Width, source.Height, new Dotted2DPixelShader(source));
            context.Transition(source, ResourceState.ReadOnly);
            context.ForEach(destination, new LinearSamplingFromNormalized2DPixelShader(source.AsReadOnly()));
            context.Transition(source, ResourceState.ReadWrite);
            context.Fill(source, new Rgba32(0xFF, 0x6A, 0x00, 0x00));
        }

        using Image<ImageSharpRgba32> processed = destination.ToImage<Rgba32, ImageSharpRgba32>();

        TolerantImageComparer.AssertEqual(sampled, processed, 0.0000017f);

        Rgba32[,] clear = source.ToArray();

        foreach (Rgba32 value in clear)
        {
            Assert.AreEqual(value, new Rgba32(0xFF, 0x6A, 0x00, 0x00));
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void TransitionAndWrites_ReadWriteTexture3D_Normalized(Device device)
    {
        _ = device.Get();

        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");
        string originalPath = Path.Combine(assetsPath, "CityWith1920x1280Resizing.png");
        string sampledPath = Path.Combine(assetsPath, "CityAfter1024x1024SamplingFrom1920x1080AndDashing.png");

        ImageInfo imageInfo = Image.Identify(originalPath);

        using Image<ImageSharpRgba32> sampled = Image.Load<ImageSharpRgba32>(Path.Combine(assetsPath, sampledPath));

        using ReadWriteTexture3D<Rgba32, float4> source = device.Get().AllocateReadWriteTexture3D<Rgba32, float4>(imageInfo.Width, imageInfo.Height, 3);

        using (UploadTexture2D<Rgba32> upload2D = device.Get().LoadUploadTexture2D<Rgba32>(originalPath))
        using (UploadTexture3D<Rgba32> upload3D = device.Get().AllocateUploadTexture3D<Rgba32>(upload2D.Width, upload2D.Height, 2))
        {
            for (int z = 0; z < upload3D.Depth; z++)
            {
                upload2D.View.CopyTo(upload3D.View.GetDepthView(z));
            }

            upload3D.CopyTo(source);
        }

        using ReadWriteTexture3D<Rgba32, float4> destination = device.Get().AllocateReadWriteTexture3D<Rgba32, float4>(sampled.Width, sampled.Height, 2);

        using (ComputeContext context = device.Get().CreateComputeContext())
        {
            context.For(source.Width, source.Height, source.Depth, new Dotted3DPixelShader(source));
            context.Transition(source, ResourceState.ReadOnly);
            context.For(destination.Width, destination.Height, destination.Depth, new LinearSamplingFromNormalized3DComputeShader(source.AsReadOnly(), destination));
            context.Transition(source, ResourceState.ReadWrite);
            context.Fill(source, new Rgba32(0xFF, 0x6A, 0x00, 0x00));
        }

        for (int z = 0; z < destination.Depth; z++)
        {
            using Image<ImageSharpRgba32> processed = destination.ToImage<Rgba32, ImageSharpRgba32>(depth: z);

            TolerantImageComparer.AssertEqual(sampled, processed, 0.000012f);
        }

        Rgba32[,,] clear = source.ToArray();

        foreach (Rgba32 value in clear)
        {
            Assert.AreEqual(value, new Rgba32(0xFF, 0x6A, 0x00, 0x00));
        }
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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct OffsetComputeShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;
        public readonly int offset;

        /// <inheritdoc/>
        public void Execute()
        {
            this.buffer[this.offset + ThreadIds.X] *= 2;
        }
    }

    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ClearPixelShader : IComputeShader<float4>
    {
        /// <inheritdoc/>
        public float4 Execute()
        {
            return default;
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ColorPixelShader : IComputeShader<float4>
    {
        public readonly float4 color;

        /// <inheritdoc/>
        public float4 Execute()
        {
            return this.color;
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct LinearSampling2DPixelShader : IComputeShader<float4>
    {
        public readonly IReadOnlyTexture2D<float4> source;

        /// <inheritdoc/>
        public float4 Execute()
        {
            return this.source.Sample(ThreadIds.Normalized.XY);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct LinearSampling3DComputeShader : IComputeShader
    {
        public readonly IReadOnlyTexture3D<float4> source;
        public readonly IReadWriteNormalizedTexture3D<float4> destination;

        /// <inheritdoc/>
        public void Execute()
        {
            float3 xyz = ThreadIds.Normalized.XYZ;

            // The source has a depth of 3, but the destination has a depth of 2
            xyz.Z = Hlsl.Lerp(0, 0.5f, xyz.Z);

            this.destination[ThreadIds.XYZ] = this.source.Sample(xyz);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct LinearSamplingFromNormalized2DPixelShader : IComputeShader<float4>
    {
        public readonly IReadOnlyNormalizedTexture2D<float4> source;

        /// <inheritdoc/>
        public float4 Execute()
        {
            return this.source.Sample(ThreadIds.Normalized.XY);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct CopyToOutputPixelShader : IComputeShader<float4>
    {
        public readonly IReadOnlyNormalizedTexture2D<float4> source;

        /// <inheritdoc/>
        public float4 Execute()
        {
            return this.source[ThreadIds.XY];
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct InvertPixelShader : IComputeShader<float4>
    {
        public readonly IReadWriteNormalizedTexture2D<float4> source;

        /// <inheritdoc/>
        public float4 Execute()
        {
            return new(1.0f - this.source[ThreadIds.XY].XYZ, this.source[ThreadIds.XY].W);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct LinearSamplingFromNormalized3DComputeShader : IComputeShader
    {
        public readonly IReadOnlyNormalizedTexture3D<float4> source;
        public readonly IReadWriteNormalizedTexture3D<float4> destination;

        /// <inheritdoc/>
        public void Execute()
        {
            float3 xyz = ThreadIds.Normalized.XYZ;

            // See comment in LinearSampling3DComputeShader
            xyz.Z = Hlsl.Lerp(0, 0.5f, xyz.Z);

            this.destination[ThreadIds.XYZ] = this.source.Sample(xyz);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct Dotted2DPixelShader : IComputeShader
    {
        public readonly IReadWriteNormalizedTexture2D<float4> source;

        /// <inheritdoc/>
        public void Execute()
        {
            bool isHorizontal192CellEven = (ThreadIds.X / 192) % 2 == 0;
            bool isVertical128CellEven = (ThreadIds.Y / 128) % 2 == 0;

            if (isHorizontal192CellEven != isVertical128CellEven)
            {
                if ((ThreadIds.Normalized.X <= 0.5f &&
                     ThreadIds.Normalized.Y <= 0.5) ||
                    (ThreadIds.Normalized.X > 0.5f &&
                     ThreadIds.Normalized.Y > 0.5))
                {
                    this.source[ThreadIds.XY] = float4.UnitW;
                }
                else
                {
                    this.source[ThreadIds.XY] = float4.Zero;
                }
            }
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct Dotted3DPixelShader : IComputeShader
    {
        public readonly IReadWriteNormalizedTexture3D<float4> source;

        /// <inheritdoc/>
        public void Execute()
        {
            bool isHorizontal192CellEven = (ThreadIds.X / 192) % 2 == 0;
            bool isVertical128CellEven = (ThreadIds.Y / 128) % 2 == 0;

            if (isHorizontal192CellEven != isVertical128CellEven)
            {
                if ((ThreadIds.Normalized.X <= 0.5f &&
                     ThreadIds.Normalized.Y <= 0.5) ||
                    (ThreadIds.Normalized.X > 0.5f &&
                     ThreadIds.Normalized.Y > 0.5))
                {
                    this.source[ThreadIds.XYZ] = float4.UnitW;
                }
                else
                {
                    this.source[ThreadIds.XYZ] = float4.Zero;
                }
            }
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ConvertToNonNormalized2DShader : IComputeShader
    {
        public readonly IReadOnlyNormalizedTexture2D<float4> source;
        public readonly ReadWriteTexture2D<float4> destination;

        /// <inheritdoc/>
        public void Execute()
        {
            this.destination[ThreadIds.XY] = this.source[ThreadIds.XY];
        }
    }
}