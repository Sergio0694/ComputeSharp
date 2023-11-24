![ComputeSharp cover image](https://user-images.githubusercontent.com/10199417/108635546-3512ea00-7480-11eb-8172-99bc59f4eb6f.png)

# Overview 📖

**ComputeSharp** is a library to write DX12 compute shaders entirely with C# code. The available APIs let you access GPU devices, allocate GPU buffers and textures, move data between them and the RAM, write compute shaders entirely in C# and have them run on the GPU. The goal of this project is to make GPU computing easy to use for all .NET developers!

# Quick start 🚀

**ComputeSharp** exposes a `GraphicsDevice` class that acts as entry point for all public APIs. The available `GraphicsDevice.GetDefault()` method that lets you access the main GPU device on the current machine, which can be used to allocate buffers and perform operations. If your machine doesn't have a supported GPU (or if it doesn't have a GPU at all), **ComputeSharp** will automatically create a [WARP device](https://docs.microsoft.com/windows/win32/direct3darticles/directx-warp) instead, which will still let you use the library normally, with shaders running on the CPU instead through an emulation layer. This means that you don't need to manually write a fallback path in case no GPU is available: **ComputeSharp** will automatically handle this for you.

Let's suppose we want to run a simple compute shader that multiplies all items in a target buffer by two. The first step is to create the GPU buffer and copy our data to it:

```csharp
// Get some sample data
int[] array = [.. Enumerable.Range(1, 100)];

// Allocate a GPU buffer and copy the data to it.
// We want the shader to modify the items in-place, so we
// can allocate a single read-write buffer to work on.
using ReadWriteBuffer<int> buffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer(array);
```

The `AllocateReadWriteBuffer` extension takes care of creating a `ReadWriteBuffer<T>` instance with the same size as the input array and copying its contents to the allocated GPU buffer. There are a number of overloads available as well, to create buffers of different types and with custom length.

Next, we need to define the GPU shader to run. To do this, we'll need to define a `partial struct` type implementing the `IComputeShader` interface (note that the `partial` modifier is necessary for **ComputeSharp** to generate additional code to run the shader). This type will contain the code we want to run on the GPU, as well as fields representing the values we want to capture and pass to the GPU (such as GPU resources, or arbitrary values we need). Next, we need to add the `[ThreadGroupSize]` attribute to configure the dispatching configuration for the shader. This shader only operates on a 1D buffer, so we can use `DefaultThreadGroupSizes.X` for this. Lastly, we also need to add the `[GeneratedComputeShaderDescriptor]` attribute, to let the source generator bundled with **ComputeSharp** do its magic. In this case, we only need to capture the buffer to work on, so the shader type will look like this:

```C#
[ThreadGroupSize(DefaultThreadGroupSizes.X)]
[GeneratedComputeShaderDescriptor]
public readonly partial struct MultiplyByTwo(ReadWriteBuffer<int> buffer) : IComputeShader
{
    public void Execute()
    {
        buffer[ThreadIds.X] *= 2;
    }
}
```

In this example, we're using a primary constructor (but you can also explicitly declare fields and set them via a constructor). The shader body is also using a special `ThreadIds` class, which is one of the available special classes to access dispatch parameters from within a shader body. In this case, `ThreadIds` lets us access the current invocation index for the shader, just like if we were accessing the classic `i` variable from within a `for` loop.

We can now finally run the GPU shader and copy the data back to our array:

```csharp
// Launch the shader
GraphicsDevice.GetDefault().For(buffer.Length, new MultiplyByTwo(buffer));

// Get the data back
buffer.CopyTo(array);
```

# There's more!
For a complete list of all features available in **ComputeSharp**, check the documentation in the [GitHub repo](https://github.com/Sergio0694/ComputeSharp).