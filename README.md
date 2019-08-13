![](https://i.imgur.com/ufWcoO6.png)
[![NuGet](https://img.shields.io/nuget/v/ComputeSharp.svg)](https://www.nuget.org/packages/ComputeSharp/) [![NuGet](https://img.shields.io/nuget/dt/ComputeSharp.svg)](https://www.nuget.org/stats/packages/ComputeSharp?groupby=Version) [![Twitter Follow](https://img.shields.io/twitter/follow/Sergio0694.svg?style=flat&label=Follow)](https://twitter.com/SergioPedri)

# What is it?

**ComputeSharp** is a .NET Standard 2.0 library to run C# code in parallel on the GPU through DX12 and dynamically generated HLSL compute shaders. The available APIs let you allocate GPU buffers and write compute shaders as simple lambda expressions or local methods, with all the captured variables being handled automatically and passed to the running shader.

# Table of Contents

- [Installing from NuGet](#installing-from-nuget)
- [Quick start](#quick-start)
  - [Capturing variables](#capturing-variables) 
  - [Advanced usage](#advanced-usage)
- [Requirements](#requirements)

# Installing from NuGet

To install **ComputeSharp**, run the following command in the **Package Manager Console**

```
Install-Package ComputeSharp
```

More details available [here](https://www.nuget.org/packages/ComputeSharp/).

# Quick start

**ComputeSharp** exposes a `Gpu` class that acts as the entry point for all public APIs. It exposes the `Gpu.Default` property that let you access the main GPU device on the current machine, which can be used to allocate buffers and perform operations.

The following sample shows how to allocate a writeable buffer, populate it with a compute shader, and read it back.

```C#
// Allocate a writeable buffer on the GPU, with the contents of the array
using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(1000);

// Shader body
Action<ThreadIds> action = id => buffer[id.X] = id.X;

// Run the shader
Gpu.Default.For(1000, action);

// Get the data back
float[] array = buffer.GetData();
```

## Capturing variables

If the shader in C# is capturing some local variable, those will be automatically copied over to the GPU, so that the HLSL shader will be able to access them just like you'd expect. Just make sure to only capture valid HLSL types: either scalar types (`int`, `uint`, `float`, etc.), known HLSL structs (eg. `Vector3`) or unmanaged custom structs.

## Advanced usage

**ComputeSharp** lets you dispatch compute shaders over thread groups from 1 to 3 dimensions, includes supports for readonly buffers, and more. Here is a more advanced sample showcasing both these two features.

```C#
int height = 10, width = 10;
float[] x = new float[height * width]; // Array to sum to y
float[] y = new float[height * width]; // Result array (assume both had some values)

using ReadOnlyBuffer<float> xBuffer = Gpu.Default.AllocateConstantBuffer(x); 
using ReadWriteBuffer<float> yBuffer = Gpu.Default.AllocateReadWriteBuffer(y);

// Shader body
Action<ThreadIds> action = id =>
{
    uint offset = id.X + id.Y * (uint)width;
    yBuffer[offset] += xBuffer[offset];
};

// Run the shader
Gpu.Default.For(width, height, action);

// Get the data back and write it to the y array
yBuffer.GetData(y);
```

# Requirements

The **ComputeSharp** library requires .NET Standard 2.0 support, and it is available for applications targeting:
- .NET Framework >= 4.6.1
- .NET Core >= 2.0
- UWP (from SDK 10.0.16299, and only in `Debug` mode for now)

Additionally, you need an IDE with .NET Core 3.0 and C# 8.0 support to compile the library on your PC.
