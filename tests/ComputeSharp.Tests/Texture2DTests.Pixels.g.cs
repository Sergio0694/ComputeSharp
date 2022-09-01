using System;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;

namespace ComputeSharp.Tests;

partial class Texture2DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(Bgra32), typeof(Float4))]
    [Data(typeof(R16), typeof(float))]
    [Data(typeof(R8), typeof(float))]
    [Data(typeof(Rg16), typeof(Float2))]
    [Data(typeof(Rg32), typeof(Float2))]
    [Data(typeof(Rgba32), typeof(Float4))]
    [Data(typeof(Rgba64), typeof(Float4))]
    public void Dispatch_NormalizedTexture2D(Device device, Type t, Type tPixel)
    {
        if (t == typeof(Bgra32) && tPixel == typeof(Float4))
        {
            using ReadOnlyTexture2D<Bgra32, Float4> source = device.Get().AllocateReadOnlyTexture2D<Bgra32, Float4>(128, 128);
            using ReadWriteTexture2D<Bgra32, Float4> destination = device.Get().AllocateReadWriteTexture2D<Bgra32, Float4>(128, 128);

            device.Get().For(128, 128, new Shader_Unorm_Bgra32_Float4(source, destination));
        }
        if (t == typeof(R16) && tPixel == typeof(float))
        {
            using ReadOnlyTexture2D<R16, float> source = device.Get().AllocateReadOnlyTexture2D<R16, float>(128, 128);
            using ReadWriteTexture2D<R16, float> destination = device.Get().AllocateReadWriteTexture2D<R16, float>(128, 128);

            device.Get().For(128, 128, new Shader_Unorm_R16_float(source, destination));
        }
        if (t == typeof(R8) && tPixel == typeof(float))
        {
            using ReadOnlyTexture2D<R8, float> source = device.Get().AllocateReadOnlyTexture2D<R8, float>(128, 128);
            using ReadWriteTexture2D<R8, float> destination = device.Get().AllocateReadWriteTexture2D<R8, float>(128, 128);

            device.Get().For(128, 128, new Shader_Unorm_R8_float(source, destination));
        }
        if (t == typeof(Rg16) && tPixel == typeof(Float2))
        {
            using ReadOnlyTexture2D<Rg16, Float2> source = device.Get().AllocateReadOnlyTexture2D<Rg16, Float2>(128, 128);
            using ReadWriteTexture2D<Rg16, Float2> destination = device.Get().AllocateReadWriteTexture2D<Rg16, Float2>(128, 128);

            device.Get().For(128, 128, new Shader_Unorm_Rg16_Float2(source, destination));
        }
        if (t == typeof(Rg32) && tPixel == typeof(Float2))
        {
            using ReadOnlyTexture2D<Rg32, Float2> source = device.Get().AllocateReadOnlyTexture2D<Rg32, Float2>(128, 128);
            using ReadWriteTexture2D<Rg32, Float2> destination = device.Get().AllocateReadWriteTexture2D<Rg32, Float2>(128, 128);

            device.Get().For(128, 128, new Shader_Unorm_Rg32_Float2(source, destination));
        }
        if (t == typeof(Rgba32) && tPixel == typeof(Float4))
        {
            using ReadOnlyTexture2D<Rgba32, Float4> source = device.Get().AllocateReadOnlyTexture2D<Rgba32, Float4>(128, 128);
            using ReadWriteTexture2D<Rgba32, Float4> destination = device.Get().AllocateReadWriteTexture2D<Rgba32, Float4>(128, 128);

            device.Get().For(128, 128, new Shader_Unorm_Rgba32_Float4(source, destination));
        }
        if (t == typeof(Rgba64) && tPixel == typeof(Float4))
        {
            using ReadOnlyTexture2D<Rgba64, Float4> source = device.Get().AllocateReadOnlyTexture2D<Rgba64, Float4>(128, 128);
            using ReadWriteTexture2D<Rgba64, Float4> destination = device.Get().AllocateReadWriteTexture2D<Rgba64, Float4>(128, 128);

            device.Get().For(128, 128, new Shader_Unorm_Rgba64_Float4(source, destination));
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Bgra32_Float4 : IComputeShader
    {
        public readonly ReadOnlyTexture2D<Bgra32, Float4> source;
        public readonly ReadWriteTexture2D<Bgra32, Float4> destination;

        public void Execute()
        {
            destination[ThreadIds.XY] = (source[ThreadIds.XY] + destination[ThreadIds.XY]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_R16_float : IComputeShader
    {
        public readonly ReadOnlyTexture2D<R16, float> source;
        public readonly ReadWriteTexture2D<R16, float> destination;

        public void Execute()
        {
            destination[ThreadIds.XY] = (source[ThreadIds.XY] + destination[ThreadIds.XY]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_R8_float : IComputeShader
    {
        public readonly ReadOnlyTexture2D<R8, float> source;
        public readonly ReadWriteTexture2D<R8, float> destination;

        public void Execute()
        {
            destination[ThreadIds.XY] = (source[ThreadIds.XY] + destination[ThreadIds.XY]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rg16_Float2 : IComputeShader
    {
        public readonly ReadOnlyTexture2D<Rg16, Float2> source;
        public readonly ReadWriteTexture2D<Rg16, Float2> destination;

        public void Execute()
        {
            destination[ThreadIds.XY] = (source[ThreadIds.XY] + destination[ThreadIds.XY]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rg32_Float2 : IComputeShader
    {
        public readonly ReadOnlyTexture2D<Rg32, Float2> source;
        public readonly ReadWriteTexture2D<Rg32, Float2> destination;

        public void Execute()
        {
            destination[ThreadIds.XY] = (source[ThreadIds.XY] + destination[ThreadIds.XY]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rgba32_Float4 : IComputeShader
    {
        public readonly ReadOnlyTexture2D<Rgba32, Float4> source;
        public readonly ReadWriteTexture2D<Rgba32, Float4> destination;

        public void Execute()
        {
            destination[ThreadIds.XY] = (source[ThreadIds.XY] + destination[ThreadIds.XY]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rgba64_Float4 : IComputeShader
    {
        public readonly ReadOnlyTexture2D<Rgba64, Float4> source;
        public readonly ReadWriteTexture2D<Rgba64, Float4> destination;

        public void Execute()
        {
            destination[ThreadIds.XY] = (source[ThreadIds.XY] + destination[ThreadIds.XY]) / 2;
        }
    }
}
