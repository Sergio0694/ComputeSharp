![](https://i.imgur.com/ufWcoO6.png)
[![NuGet](https://img.shields.io/nuget/v/ComputeSharp.svg)](https://www.nuget.org/packages/ComputeSharp/) [![NuGet](https://img.shields.io/nuget/dt/ComputeSharp.svg)](https://www.nuget.org/stats/packages/ComputeSharp?groupby=Version)

# What is it?

**ComputeSharp** is a .NET Standard 2.1 library to run C# code in parallel on the GPU through DX12 and dynamically generated HLSL compute shaders. The available APIs let you allocate GPU buffers and write compute shaders as simple lambda expressions or local methods, with all the captured variables being handled automatically and passed to the running shader.

# Table of Contents

- [Installing from NuGet](#installing-from-nuget)
- [Quick start](#quick-start)
  - [Capturing variables](#capturing-variables) 
  - [Advanced usage](#advanced-usage)
- [Requirements](#requirements)
- [Special thanks](#special-thanks)

# Installing from NuGet

To install **ComputeSharp**, run the following command in the **Package Manager Console**

```
Install-Package ComputeSharp
```

More details available [here](https://www.nuget.org/packages/ComputeSharp/).

# Quick start

**ComputeSharp** exposes a `Gpu` class that acts as entry point for all public APIs. It exposes the `Gpu.Default` property that lets you access the main GPU device on the current machine, which can be used to allocate buffers and perform operations.

The following sample shows how to allocate a writeable buffer, populate it with a compute shader, and read it back.

```C#
// Define a simple shader
public readonly struct MyShader : IComputeShader
{
    public readonly ReadWriteBuffer<float> buffer;

    public MyShader(ReadWriteBuffer<float> buffer)
    {
        this.buffer = buffer;
    }

    public void Execute(ThreadIds ids)
    {
        buffer[ids.X] = ids.X;
    }
}

// Allocate a writeable buffer on the GPU, with the contents of the array
using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1000);

// Run the shader
Gpu.Default.For(1000, new MyShader(buffer));

// Get the data back
float[] array = buffer.GetData();
```

## Capturing variables

If the shader in C# is capturing some local variable, those will be automatically copied over to the GPU, so that the HLSL shader will be able to access them just like you'd expect. Additionally, **ComputeSharp** can also resolve static fields being used in a shader. The captured variables need to be convertible to valid HLSL types: either scalar types (`int`, `uint`, `float`, etc.) or known HLSL structs (eg. `Vector3`). Here is a list of the variable types currently supported by the library:

✅ .NET scalar types: `bool`, `int`, `uint`, `float`, `double`

✅ .NET vector types: `System.Numerics.Vector2`, `Vector3`, `Vector4`

✅ HLSL types: `Bool`, `Bool2`, `Bool3`, `Bool4`, `Float2`, `Float3`, `Float4`, `Int2`, `Int3`, `Int4`, `UInt2`, `Uint3`, etc.

✅ `static` fields of both scalar, vector or buffer types

✅ `static` properties, same as with fields

✅ `Func<T>`s or delegates with a valid HLSL signature, with the target method being `static`

✅ `Func<T>`s, local methods or delegates with no captured variables

✅ `static` fields and properties with a supported delegate type

## Allocating GPU buffers

There are a number of extension APIs for the `GraphicsDevice` class that can be used to allocate GPU buffers of three types: `ConstantBuffer<T>`, `ReadOnlyBuffer<T>` and `ReadWriteBuffer<T>`. The first is packed to 16 bytes and provides the fastest possible access for buffer elements, but it has a limited maximum size (around 64KB) and requires additional overhead when copying data to and from it if the size of each element is not a multiple of 16. The other buffer types are tightly packed and work great for all kinds of operations, and can be thought of the HLSL equivalent of `T[]` arrays in C#. If you're in doubt about which buffer type to use, just use either `ReadOnlyBuffer<T>` or `ReadWriteBuffer<T>`, depending on whether or not you also need write access to that buffer on the GPU side.

**NOTE:** although the APIs to allocate buffers are simply generic methods with a `T : unmanaged` constrain, they should only be used with C# types that are fully mapped to HLSL types. That means either `int`, `uint`, `float`, `double`, .NET vector types or HLSL types. The `bool` type should not be used in buffers due to C#/HLSL differences: use the `Bool` type instead.

## Advanced usage

**ComputeSharp** lets you dispatch compute shaders over thread groups from 1 to 3 dimensions, includes supports for constant and readonly buffers, and more. Additionally, most of the [HLSL intrinsic functions](https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-intrinsic-functions) are available through the `Hlsl` class. Here is a more advanced sample showcasing all these features.

```C#
// Define a class with some utility static functions
public static class Activations
{
    public static float Sigmoid(float x) => 1 / (1 + Hlsl.Exp(-x));
}

// Define the shader
public readonly struct ActivationShader : IComputeShader
{
    private readonly int width;

    public readonly ReadOnlyBuffer<float> x;
    public readonly ReadWriteBuffer<float> y;

    public ActivationShader(
        int width,
        ReadOnlyBuffer<float> x,
        ReadWriteBuffer<float> y)
    {
        this.width = width;
        this.x = x;
        this.y = y;
    }

    public void Execute(ThreadIds ids)
    {
        int offset = ids.X + ids.Y * width;
        float pow = Hlsl.Pow(x[offset], 2);
        y[offset] = Activations.Sigmoid(pow);
    }
}

int height = 10, width = 10;
float[] x = new float[height * width]; // Array to sum to y
float[] y = new float[height * width]; // Result array (assume both had some values)

using ReadOnlyBuffer<float> xBuffer = Gpu.Default.AllocateReadOnlyBuffer(x); 
using ReadWriteBuffer<float> yBuffer = Gpu.Default.AllocateReadWriteBuffer(y);

// Run the shader
Gpu.Default.For(width, height, new ActivationShader(width, xBuffer, yBuffer));

// Get the data back and write it to the y array
yBuffer.GetData(y);
```

# Requirements

The **ComputeSharp** library requires .NET Standard 2.1 support, and it is available for applications targeting:
- .NET Core >= 3.0
- Windows (x86 or x64)

Additionally, you need an IDE with .NET Core 3.1 and C# 8.0 support to compile the library and samples on your PC.

# Special thanks

The **ComputeSharp** library is based on some of the code from the [DX12GameEngine](https://github.com/Aminator/DirectX12GameEngine) repository by [Amin Delavar](https://github.com/Aminator). Additionally, **ComputeSharp** uses NuGet packages from the following repositories (excluding those from Microsoft):

- [Vortice.Windows](https://github.com/amerkoleci/Vortice.Windows)
- [ILSpy](https://github.com/icsharpcode/ILSpy)
- [Stubble](https://github.com/StubbleOrg/Stubble)
