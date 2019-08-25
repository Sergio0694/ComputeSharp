![](https://i.imgur.com/ufWcoO6.png)

# What is it?

**ComputeSharp** is a .NET Standard 2.0 library to run C# code in parallel on the GPU through DX12 and dynamically generated HLSL compute shaders. The available APIs let you allocate GPU buffers and write compute shaders as simple lambda expressions or local methods, with all the captured variables being handled automatically and passed to the running shader.

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
// Allocate a writeable buffer on the GPU, with the contents of the array
using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1000);

// Run the shader
Gpu.Default.For(1000, id => buffer[id.X] = id.X);

// Get the data back
float[] array = buffer.GetData();
```

## Capturing variables

If the shader in C# is capturing some local variable, those will be automatically copied over to the GPU, so that the HLSL shader will be able to access them just like you'd expect. Additionally, **ComputeSharp** can also resolve static fields being used in a shader. The captured variables need to be convertible to valid HLSL types: either scalar types (`int`, `uint`, `float`, etc.) or known HLSL structs (eg. `Vector3`). Here is a list of the variable types currently supported by the library:

✅ Local scalar types: `bool`, `int`, `uint`, `float`, `double`

✅ Local vector types: `System.Numerics.Vector2`, `Vector3`, `Vector4`

✅ `static` fields of both scalar, vector or buffer types

✅ `static` properties, same as with fields

## Advanced usage

**ComputeSharp** lets you dispatch compute shaders over thread groups from 1 to 3 dimensions, includes supports for constant and readonly buffers, and more. The shader body can both be declared inline, as a separate `Action<ThreadIds>` or as a local method. Additionally, most of the [HLSL intrinsic functions](https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-intrinsic-functions) are available through the `Hlsl` class. Here is a more advanced sample showcasing all these features.

```C#
int height = 10, width = 10;
float[] x = new float[height * width]; // Array to sum to y
float[] y = new float[height * width]; // Result array (assume both had some values)

using ReadOnlyBuffer<float> xBuffer = Gpu.Default.AllocateReadOnlyBuffer(x); 
using ReadWriteBuffer<float> yBuffer = Gpu.Default.AllocateReadWriteBuffer(y);

// Shader body
void Kernel(ThreadIds id)
{
    int offset = id.X + id.Y * width;
    yBuffer[offset] = Hlsl.Pow(xBuffer[offset], 2);
}

// Run the shader
Gpu.Default.For(width, height, Kernel);

// Get the data back and write it to the y array
yBuffer.GetData(y);
```

# Requirements

The **ComputeSharp** library requires .NET Standard 2.0 support, and it is available for applications targeting:
- .NET Framework >= 4.6.1
- .NET Core >= 2.0
- UWP (from SDK 10.0.16299, and only in `Debug` mode for now)

Additionally, you need an IDE with .NET Core 3.0 and C# 8.0 support to compile the library on your PC.

# Special thanks

The core of the **ComputeSharp** library is based on some of the code from the [DX12GameEngine](https://github.com/Aminator/DirectX12GameEngine) repository from [Amin Delavar](https://github.com/Aminator). Additionally, **ComputeSharp** uses the following NuGet packages (excluding those from Microsoft):

- [SharpDX.Direct3D12](https://www.nuget.org/packages/SharpDX.Direct3D12)
- [SharpDX.D3DCompiler](https://www.nuget.org/packages/SharpDX.D3DCompiler)
- [ICSharpCode.Decompiler](https://www.nuget.org/packages/ICSharpCode.Decompiler)
- [Stubble.Core](https://www.nuget.org/packages/Stubble.Core)
