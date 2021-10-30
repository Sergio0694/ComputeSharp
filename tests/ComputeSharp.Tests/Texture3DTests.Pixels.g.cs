using System;
using System.Numerics;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;

namespace ComputeSharp.Tests;

public partial class Texture3DTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Data(typeof(Bgra32), typeof(Vector4))]
    [Data(typeof(Bgra32), typeof(Float4))]
    [Data(typeof(R16), typeof(float))]
    [Data(typeof(R8), typeof(float))]
    [Data(typeof(Rg16), typeof(Vector2))]
    [Data(typeof(Rg16), typeof(Float2))]
    [Data(typeof(Rg32), typeof(Vector2))]
    [Data(typeof(Rg32), typeof(Float2))]
    [Data(typeof(Rgba32), typeof(Vector4))]
    [Data(typeof(Rgba32), typeof(Float4))]
    [Data(typeof(Rgba64), typeof(Vector4))]
    [Data(typeof(Rgba64), typeof(Float4))]
    public void Dispatch_NormalizedTexture3D(Device device, Type t, Type tPixel)
    {
        if (t == typeof(Bgra32) && tPixel == typeof(Vector4))
        {
            using ReadOnlyTexture3D<Bgra32, Vector4> source = device.Get().AllocateReadOnlyTexture3D<Bgra32, Vector4>(128, 128, 2);
            using ReadWriteTexture3D<Bgra32, Vector4> destination = device.Get().AllocateReadWriteTexture3D<Bgra32, Vector4>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Bgra32_Vector4(source, destination));
        }
        if (t == typeof(Bgra32) && tPixel == typeof(Float4))
        {
            using ReadOnlyTexture3D<Bgra32, Float4> source = device.Get().AllocateReadOnlyTexture3D<Bgra32, Float4>(128, 128, 2);
            using ReadWriteTexture3D<Bgra32, Float4> destination = device.Get().AllocateReadWriteTexture3D<Bgra32, Float4>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Bgra32_Float4(source, destination));
        }
        if (t == typeof(R16) && tPixel == typeof(float))
        {
            using ReadOnlyTexture3D<R16, float> source = device.Get().AllocateReadOnlyTexture3D<R16, float>(128, 128, 2);
            using ReadWriteTexture3D<R16, float> destination = device.Get().AllocateReadWriteTexture3D<R16, float>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_R16_float(source, destination));
        }
        if (t == typeof(R8) && tPixel == typeof(float))
        {
            using ReadOnlyTexture3D<R8, float> source = device.Get().AllocateReadOnlyTexture3D<R8, float>(128, 128, 2);
            using ReadWriteTexture3D<R8, float> destination = device.Get().AllocateReadWriteTexture3D<R8, float>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_R8_float(source, destination));
        }
        if (t == typeof(Rg16) && tPixel == typeof(Vector2))
        {
            using ReadOnlyTexture3D<Rg16, Vector2> source = device.Get().AllocateReadOnlyTexture3D<Rg16, Vector2>(128, 128, 2);
            using ReadWriteTexture3D<Rg16, Vector2> destination = device.Get().AllocateReadWriteTexture3D<Rg16, Vector2>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Rg16_Vector2(source, destination));
        }
        if (t == typeof(Rg16) && tPixel == typeof(Float2))
        {
            using ReadOnlyTexture3D<Rg16, Float2> source = device.Get().AllocateReadOnlyTexture3D<Rg16, Float2>(128, 128, 2);
            using ReadWriteTexture3D<Rg16, Float2> destination = device.Get().AllocateReadWriteTexture3D<Rg16, Float2>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Rg16_Float2(source, destination));
        }
        if (t == typeof(Rg32) && tPixel == typeof(Vector2))
        {
            using ReadOnlyTexture3D<Rg32, Vector2> source = device.Get().AllocateReadOnlyTexture3D<Rg32, Vector2>(128, 128, 2);
            using ReadWriteTexture3D<Rg32, Vector2> destination = device.Get().AllocateReadWriteTexture3D<Rg32, Vector2>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Rg32_Vector2(source, destination));
        }
        if (t == typeof(Rg32) && tPixel == typeof(Float2))
        {
            using ReadOnlyTexture3D<Rg32, Float2> source = device.Get().AllocateReadOnlyTexture3D<Rg32, Float2>(128, 128, 2);
            using ReadWriteTexture3D<Rg32, Float2> destination = device.Get().AllocateReadWriteTexture3D<Rg32, Float2>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Rg32_Float2(source, destination));
        }
        if (t == typeof(Rgba32) && tPixel == typeof(Vector4))
        {
            using ReadOnlyTexture3D<Rgba32, Vector4> source = device.Get().AllocateReadOnlyTexture3D<Rgba32, Vector4>(128, 128, 2);
            using ReadWriteTexture3D<Rgba32, Vector4> destination = device.Get().AllocateReadWriteTexture3D<Rgba32, Vector4>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Rgba32_Vector4(source, destination));
        }
        if (t == typeof(Rgba32) && tPixel == typeof(Float4))
        {
            using ReadOnlyTexture3D<Rgba32, Float4> source = device.Get().AllocateReadOnlyTexture3D<Rgba32, Float4>(128, 128, 2);
            using ReadWriteTexture3D<Rgba32, Float4> destination = device.Get().AllocateReadWriteTexture3D<Rgba32, Float4>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Rgba32_Float4(source, destination));
        }
        if (t == typeof(Rgba64) && tPixel == typeof(Vector4))
        {
            using ReadOnlyTexture3D<Rgba64, Vector4> source = device.Get().AllocateReadOnlyTexture3D<Rgba64, Vector4>(128, 128, 2);
            using ReadWriteTexture3D<Rgba64, Vector4> destination = device.Get().AllocateReadWriteTexture3D<Rgba64, Vector4>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Rgba64_Vector4(source, destination));
        }
        if (t == typeof(Rgba64) && tPixel == typeof(Float4))
        {
            using ReadOnlyTexture3D<Rgba64, Float4> source = device.Get().AllocateReadOnlyTexture3D<Rgba64, Float4>(128, 128, 2);
            using ReadWriteTexture3D<Rgba64, Float4> destination = device.Get().AllocateReadWriteTexture3D<Rgba64, Float4>(128, 128, 2);

            device.Get().For(128, 128, 2, new Shader_Unorm_Rgba64_Float4(source, destination));
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Bgra32_Vector4 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Bgra32, Vector4> source;
        public readonly ReadWriteTexture3D<Bgra32, Vector4> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Bgra32_Float4 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Bgra32, Float4> source;
        public readonly ReadWriteTexture3D<Bgra32, Float4> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_R16_float : IComputeShader
    {
        public readonly ReadOnlyTexture3D<R16, float> source;
        public readonly ReadWriteTexture3D<R16, float> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_R8_float : IComputeShader
    {
        public readonly ReadOnlyTexture3D<R8, float> source;
        public readonly ReadWriteTexture3D<R8, float> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rg16_Vector2 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Rg16, Vector2> source;
        public readonly ReadWriteTexture3D<Rg16, Vector2> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rg16_Float2 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Rg16, Float2> source;
        public readonly ReadWriteTexture3D<Rg16, Float2> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rg32_Vector2 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Rg32, Vector2> source;
        public readonly ReadWriteTexture3D<Rg32, Vector2> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rg32_Float2 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Rg32, Float2> source;
        public readonly ReadWriteTexture3D<Rg32, Float2> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rgba32_Vector4 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Rgba32, Vector4> source;
        public readonly ReadWriteTexture3D<Rgba32, Vector4> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rgba32_Float4 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Rgba32, Float4> source;
        public readonly ReadWriteTexture3D<Rgba32, Float4> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rgba64_Vector4 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Rgba64, Vector4> source;
        public readonly ReadWriteTexture3D<Rgba64, Vector4> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }

    [AutoConstructor]
    internal readonly partial struct Shader_Unorm_Rgba64_Float4 : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Rgba64, Float4> source;
        public readonly ReadWriteTexture3D<Rgba64, Float4> destination;

        public void Execute()
        {
            destination[ThreadIds.XYZ] = (source[ThreadIds.XYZ] + destination[ThreadIds.XYZ]) / 2;
        }
    }
}
