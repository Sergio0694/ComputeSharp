![](https://user-images.githubusercontent.com/10199417/108635546-3512ea00-7480-11eb-8172-99bc59f4eb6f.png)
[![.NET](https://github.com/Sergio0694/ComputeSharp/workflows/.NET/badge.svg)](https://github.com/Sergio0694/ComputeSharp/actions) [![NuGet](https://img.shields.io/nuget/dt/ComputeSharp.svg)](https://www.nuget.org/stats/packages/ComputeSharp?groupby=Version)

# What is it?

**ComputeSharp** is a .NET library to run C# code in parallel on the GPU through DX12 and dynamically generated HLSL compute shaders. The available APIs let you access GPU devices, allocate GPU buffers and textures, move data between them and the RAM, write compute shaders entirely in C# and have them run on the GPU. The goal of this project is to make GPU computing easy to use for all .NET developers! 🚀

# What can it actually do?

Since a video is worth a thousand words, here's a showcase of some pixel shaders (originally from [shadertoy.com](https://www.shadertoy.com/)), ported from GLSL to C# and running with **ComputeSharp** in a WinUI 3 sample app. You can use this library to create all sorts of things from scientific simulations, to animated backgrounds, audio visualizers and more! 🙌

https://user-images.githubusercontent.com/10199417/126792509-c11feb11-a609-4fab-86b3-426d43df6d90.mp4

# Try out the sample app!

The sample app is available in the Microsoft Store, showcasing several pixel shaders all powered by **ComputeSharp**!

<a href="https://www.microsoft.com/store/apps/9PDC095X3PKV"><img src="https://developer.microsoft.com/en-us/store/badges/images/English_get-it-from-MS.png" alt="Get it from Microsoft" width='280' /></a>

# Table of Contents

- [Available packages](#available-packages)
- [Installing from NuGet](#installing-from-nuget)
- [Quick start](#quick-start)
  - [Capturing variables](#capturing-variables)
  - [GPU resource types](#gpu-resource-types)
  - [HLSL vector and matrix types](#hlsl-vector-and-matrix-types)
  - [HLSL intrinsics](#hlsl-intrinsics)
  - [Shader constants and globals](#shader-constants-and-globals)
  - [Dispatch info](#dispatch-info)
  - [Working with images](#working-with-images)
  - [Shader metaprogramming](#shader-metaprogramming)
  - [Inspecting shaders](#inspecting-shaders)
  - [DirectX interop](#directx-interop)
  - [Precompiled shaders](#precompiled-shaders)
- [Requirements](#requirements)
- [F# support](#f-support)
- [Sponsors](#sponsors)
- [Special thanks](#special-thanks)

# Available packages

| Name | Description | Latest version |
| ------ | ------  | ------ |
| **ComputeSharp** | The core library, with compiled shaders support | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.svg)](https://www.nuget.org/packages/ComputeSharp/) |
| **ComputeSharp.Dynamic** | An extension for **ComputeSharp**, enabling dynamic shader compilation at runtime and shader metaprogramming | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.Dynamic.svg)](https://www.nuget.org/packages/ComputeSharp.Dynamic/) |
| **ComputeSharp.Uwp** | A UWP library (targeting .NET Standard 2.0) with controls to render DX12 shaders powered by **ComputeSharp** | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.Uwp.svg)](https://www.nuget.org/packages/ComputeSharp.Uwp/) |
| **ComputeSharp.WinUI** | A WinUI 3 library (targeting .NET 6) with controls to render DX12 shaders powered by **ComputeSharp** | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.WinUI.svg)](https://www.nuget.org/packages/ComputeSharp.WinUI/) |
| **ComputeSharp.D2D1** | A library to write D2D1 pixel shaders entirely with C# code, and to easily register and create ID2D1Effect-s from them | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.D2D1.svg)](https://www.nuget.org/packages/ComputeSharp.D2D1/) |

# Installing from NuGet

To install **ComputeSharp**, run the following command in the **Package Manager Console**

```
Install-Package ComputeSharp
```

Alternatively, use the NuGet package manager wizard in Visual Studio or your IDE of choice.

More details available [here](https://www.nuget.org/packages/ComputeSharp/).

# Quick start

**ComputeSharp** exposes a `GraphicsDevice` class that acts as entry point for all public APIs. The available `GraphicsDevice.Default` property that lets you access the main GPU device on the current machine, which can be used to allocate buffers and perform operations. If your machine doesn't have a supported GPU (or if it doesn't have a GPU at all), **ComputeSharp** will automatically create a [WARP device](https://docs.microsoft.com/windows/win32/direct3darticles/directx-warp) instead, which will still let you use the library normally, with shaders running on the CPU instead through an emulation layer. This means that you don't need to manually write a fallback path in case no GPU is available - **ComputeSharp** will automatically handle this for you.

Let's suppose we want to run a simple compute shader that multiplies all items in a target buffer by two. The first step is to create the GPU buffer and copy our data to it:

```csharp
// Get some sample data
int[] array = Enumerable.Range(1, 100).ToArray();

// Allocate a GPU buffer and copy the data to it.
// We want the shader to modify the items in-place, so we
// can allocate a single read-write buffer to work on.
using ReadWriteBuffer<int> buffer = GraphicsDevice.Default.AllocateReadWriteBuffer(array);
```

The `AllocateReadWriteBuffer` extension takes care of creating a `ReadWriteBuffer<T>` instance with the same size as the input array and copying its contents to the allocated GPU buffer. There are a number of overloads available as well, to create buffers of different types and with custom length.

Next, we need to define the GPU shader to run. To do this, we'll need to define a `partial struct` type implementing the `IComputeShader` interface (note that the `partial` modifier is necessary for **ComputeSharp** to generate additional code to run the shader). This type will contain the code we want to run on the GPU, as well as fields representing the values we want to capture and pass to the GPU (such as GPU resources, or arbitrary values we need). In this case, we only need to capture the buffer to work on, so the shader type will look like this:

```C#
[AutoConstructor]
public readonly partial struct MultiplyByTwo : IComputeShader
{
    public readonly ReadWriteBuffer<int> buffer;

    public void Execute()
    {
        buffer[ThreadIds.X] *= 2;
    }
}
```

We're using the `[AutoConstructor]` attribute included in **ComputeSharp**, which creates a constructor for our type automatically. The shader body is also using a special `ThreadIds` class, which is one of the available special classes to access dispatch parameters from within a shader body. In this case, `ThreadIds` lets us access the current invocation index for the shader, just like if we were accessing the classic `i` variable from within a `for` loop.

We can now finally run the GPU shader and copy the data back to our array:

```csharp
// Launch the shader
GraphicsDevice.Default.For(buffer.Length, new MultiplyByTwo(buffer));

// Get the data back
buffer.CopyTo(array);
```

> **NOTE:** all these samples assume that **ComputeSharp.Dynamic** is also installed. For info on how to only use precompiled shaders with the base **ComputeSharp** package, see the [precompiled shaders](#precompiled-shaders) paragraph.

## Capturing variables

Shaders can store either GPU resources or custom values in their fields, so that they can be accessed when running on the GPU as well. This can be useful to pass some extra parameters to a shader (eg. some factor to multiply values by), that don't belong to a GPU buffer of their own. The captured variables need to be of a supported scalar or vector type so that they can be correctly used by the GPU shader in HLSL. Here is a list of the variable types currently supported:

✅ .NET scalar types: `bool`, `int`, `uint`, `float`, `double`

✅ .NET vector types: `System.Numerics.Vector2`, `Vector3`, `Vector4`

✅ HLSL types: `Bool`, `Bool2`, `Bool3`, `Bool4`, `Float2`, `Float3`, `Float4`, `Int2`, `Int3`, `Int4`, `UInt2`, `Uint3`, etc.

✅ HLSL matrix types: `Float2x2`, `Float3x3`, `Float3x4`, `Float4x4`, `Float1x4`, `Float4x`, etc.

✅ Custom `struct` types containing any of the types above, as well as other valid custom `struct` types

> **NOTE:** since **ComputeSharp** generates global type aliases for all the HLSL types in consuming projects, it's possible to refer to them via the same name they use in HLSL, for simplicity. For instance, `Float4` can just be referred to as `float4`, `Float4x4` will just be `float4x4`, etc. This also allows the type names to look more consistent with other C# primitive types.

## GPU resource types

There are a number of extension APIs for the `GraphicsDevice` class that can be used to allocate GPU resources. Here is a breakdown of the main resource types that are available:

- `ReadWriteBuffer<T>`: this type can be viewed as the equivalent of the `T[]` type, and can contain a writeable sequence of any of the supported types mentioned above. It is very flexible and works well in most situations. If you're just getting started and are not sure about what kind of buffer to use, this is usually a good choice.
- `ReadOnlyBuffer<T>`: this type represents a sequence of items that the GPU cannot write to. It is particularly useful to declare intent from within a compute shader and to avoid accidentally writing to data that is not supposed to change during the execution of a shader.
- `ConstantBuffer<T>`: this type is meant to be used for small sequences of items that never change during the execution of a shader. Compared to `ReadOnlyBuffer<T>` is has more constraints, but can benefit from better caching on the GPU side (it is recommended to verify with proper benchmarking that this type is appropriate to use). Items within a `ConstantBuffer<T>` instance are packed to 16 bytes, which helps the GPU to have a particularly fast access time to them, but the total size of the buffer is limited to around 64KB. Copying to and from this buffer can also have additional overhead, as the GPU needs to account for the possible padding for each item (as the 16 bytes alignment is not present on the CPU side). If you're in doubt about which buffer type to use, just use either `ReadOnlyBuffer<T>` or `ReadWriteBuffer<T>`, depending on whether or not you also need write access to that buffer on the GPU side.
- `ReadOnlyTexture2D<T>` and `ReadWriteTexture2D<T>`: these types represent a 2D texture with elements of a specific type. Note that textures are not just 2D arrays, and have additional characteristics and limitations. Items in a texture are stored with a tiled layout instead of with the classic row-major order that .NET `T[,]` arrays have, and this allows them to be extremely fast when accessing small areas of neighbouring items (due to better cache locality). This can offer a big performance speedup in operations that have a similar memory access pattern, such as blur effect or convolutions in general. Textures also have limitations in the type of items they can contain (eg. custom `struct` types are not supported), and you can check if a specific type is supported at runtime with `GraphicsDevice.IsReadOnlyTexture2DSupportedForType<T>()` and `IsReadWriteTexture2DSupportedForType<T>()`.
- `ReadOnlyTexture3D<T>` and `ReadWriteTexture3D<T>`: these are just like 2D textures, but in 3 dimensions. The same characteristics and limitations apply, with the addition of the fact that the depth axis has a much smaller limit on the size it can have. The `GraphicsDevice.IsReadOnlyTexture3DSupportedForType<T>()` and `IsReadWriteTexture3DSupportedForType<T>()` methods can be used to check for type support at runtime.
- `ReadOnlyTexture2D<T, TPixel>` and `ReadWriteTexture2D<T, TPixel>`: these texture types are particularly useful when doing image processing from a compute shader, as they allow the CPU to perform pixel format conversion automatically. The two type parameters indicate the type of items in a texture when exposed on the CPU side (as `T` items) or on the GPU side (as `TPixel` items). The type parameter `T` can be any of the existing pixel formats available in **ComputeSharp** (such as `Rgba32` and `Bgra32`), while the `TPixel` parameter represents the type of elements the GPU will be working with (for instance, `Float4` for `Rgba32`). These texture types have a number of benefits such as lower memory usage and reduced overhead on the CPU (as there is no need to manually do the pixel format conversion when copying data back and forth from the GPU).
- `ReadOnlyTexture3D<T, TPixel>` and `ReadWriteTexture3D<T, TPixel>`: these types are just like their 2D equivalent types, with the same support for automatic pixel format conversion on the GPU side.
- `UploadBuffer<T>`: this type can be used in more advanced scenarios where performance is particularly important. It represents a buffer that can be copied directly to a GPU structured buffer (either `ReadWriteBuffer<T>` or `ReadOnlyBuffer<T>`), without the need to first create a temporary transfer buffer to do so. If there is a large number of copy operations being performed to GPU buffers, it can be beneficial to create a single `UploadBuffer<T>` instance to load data to copy to the GPU, and execute a single copy from there.
- `ReadBackBuffer<T>`: this type is another advanced buffer type that is analogous to `UploadBuffer<T>`, but that can be used to copy data back from a GPU buffer. In particular, this buffer can also be accessed quickly by the GPU when reading or writing data from it, so it can also be used for further processing of data on the CPU side, without the need to copy the data onto another buffer first (such as a .NET array).
- `UploadTexture2D<T>`, `UploadTexture3D<T>`, `ReadBackTexture2D<T>` and `ReadBackTexture3D<T>`: these types are conceptually similar to `UploadBuffer<T>` and `ReadBackBuffer<T>`, with the main difference being that they can be used to copy data back and forth from 2D and 3D textures respectively.

> **NOTE:** although the various APIs to allocate buffers are simply generic methods with a `T : unmanaged` constrain, they should only be used with C# types that are supported (see notes above). Additionally, the `bool` type should not be used in buffers due to C#/HLSL differences: use the `Bool` type instead (or just an `int` buffer).

## HLSL vector and matrix types

As mentioned in the [Capturing variables](#capturing-variables) paragraph, **ComputeSharp** also exposes matrix types that can be used in compute shaders. These values store individual component values in row-major order (for consistency with .NET arrays) and can be indexed in several ways just like with HLSL vector types. One noticeable difference compared to HLSL vector types though is the lack of explicit properties to extract swizzled vectors (eg. `Float4.XZY` returns a `Float3` value with the `X`, `Z` and `Y` components). Due to the number of possible combinations being simply too high in the case of matrix types (eg. `Float4x4` alone would have had over 160k properties), the ability to extract swizzled vectors ([see here](https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-per-component-math)) can be achieved through the use of a special indexer property and values from the `MatrixIndex` type. Here is how it can be used:

```csharp
float4x4 matrix = default;

// Standard indexer for rows and individual items
float4 row = matrix[0];
float item = matrix[0][1];

// Swizzled indexers, which can be made less verbose to
// write by adding this using static directive to the file
using static ComputeSharp.MatrixIndex;

float4 diagonal = matrix[M11, M22, M33, M44];
float4 vertices = matrix[M11, M14, M44, M41];
```

Matrix types also include a number of built-in operators to work with vector types, and the `Hlsl` class detailed below (see [HLSL intrinsics](#hlsl-intrinsics)) also includes several overloads for the available methods to work on both matrix and vector types at the same time (eg. for row/matrix multiplication and other common linear algebra operations).

> **NOTE:** in order to make all the available properties and indexers usable when declaring shader constants and globals (see [Shader constants and globals](#shader-constants-and-globals)), they will return undefined data if used on the CPU instead of throwing an exception, and in this case their behavior is considered undefined. Refer to the XML docs for each API for further info, as properties and operators that are only meant to be used in a shader are clearly marked as undefined behavior if used on the CPU side. The only APIs guaranteed to be usable on the GPU are constructors, static properties and properties to access individual elements (eg. `Float4.X`).

## HLSL intrinsics

**ComputeSharp** offers support for all HLSL intrinsics that can be used from compute shaders. These are special functions (usually representing mathematical operations) that are optimized on the GPU and can execute very efficiently. Some of them can be mapped automatically from methods in the `System.Math` type, but if you want to have direct access to all of them you can just use the methods in the `Hlsl` class from a compute shader, and **ComputeSharp** will rewrite all those calls to use the native intrinsics in the compute shaders where those are used.

Here's an example of a shader that applies the [softmax function](https://en.wikipedia.org/wiki/Rectifier_(neural_networks)) to all the items in a given buffer:

```csharp
[AutoConstructor]
public readonly partial struct SoftmaxActivation : IComputeShader
{
    public readonly ReadWriteBuffer<float> buffer;
    public readonly float k;

    public void Execute()
    {
        float exp = Hlsl.Exp(k * buffer[ThreadIds.X]);
        float log = Hlsl.Log(1 + exp);

        buffer[ThreadIds.X] = log / k;
    }
}
```

> **NOTE:** in order to make all the intrinsics usable when declaring shader constants and globals (see [Shader constants and globals](#shader-constants-and-globals)), most intrinsics will just return a default value if used on the CPU instead of throwing an exception, and in this case their behavior is considered undefined. Make sure to only ever use these methods in a shader, as their results will not be correct in other execution contexts.

## Shader constants and globals

One common approach to make shaders easier to modify and experiment with is to separate the parameters being used from the code using them, by defining them as constants. **ComputeSharp** has special handling for this, and will rewrite static fields as global variables in the generated shaders (marking them as constants if needed), which are optimized by the compiler for frequent access during execution. The advantage of using constants is that they're directly embedded into a compiled shader, so they don't need to be loaded in memory whenever a shader is dispatched. Non constant shader global variables, on the other hand, can make code easier to write as they remove the need to pass multiple values around in each function that is invoked. Static fields can be of any of the supported HLSL primitive types, including vector and matrix types. Furthermore, the initialization of these constants can also use any of the available HLSL intrinsics. If the same result is being used multiple times while a shader is executed, moving values to a shader constant is a good way to avoid repeatedly computing the same value over and over.

Here is an example of how shader constants and mutable globals can be declared and used:

```csharp
[AutoConstructor]
public readonly partial struct SampleShaderWithConstants : IComputeShader
{
    public readonly ReadWriteBuffer<float> buffer;

    private const int iterations = 10;
    private const float pi = 3.14f;
    private static readonly float2 sinCosPi = new(Hlsl.Sin(Pi), Hlsl.Cos(Pi));
    private static float sum;

    public void Execute()
    {
        for (int i = 0; i < iterations; i++)
        {
            sum += pi + sinCosPi.X * sinCosPi.Y;
        }

        buffer[ThreadIds.X] = sum;
    }
}
```

As shown above, there are two ways to declare a constant value in a shader: either by using `const` in C# (which is perfect for when a value is just a primitive scalar type initialized with a constant expression), or by using `static readonly`. The latter has the advantage that it allows the type to be of a vector and matrix type as well, and the initialization can also use any of the available HLSL intrinsics. Lastly, mutable globals can be declared by just using the `static` modifier, and they can also optionally have an initializer (otherwise their default value will be used).

## Dispatch info

As shown in the [Quick start](#quick-start) paragraph, **ComputeSharp** includes a number of special types that allow shaders to access a number of useful dispatch info values, such as the current iteration coordinate or the target dispatch range. Here is a list of all the available types in this category:

- `ThreadIds`: indicates the ids of a given GPU thread running a compute shader. That is, it enables a shader to access info on the current iteration index along each axis.
- `ThreadIds.Normalized`: indicates the normalized ids of a given GPU thread running a compute shader. These ids represent equivalent info to those from `ThreadIds`, but normalized in the [0, 1] range. The range used for the normalization is the one given by the target dispatch size.
- `DispatchSize`: indicates the size of the current shader dispatch being executed. That is, it enables a shader to access info on the targeted number of invocations along each axis.
- `GroupIds`: indicates the ids of a given GPU thread running a compute shader within a dispatch group. That is, it enables a shader to access info on the index of the current thread with respect to the currently running group.
- `GroupSize`: indicates the size info of a given GPU thread group running a compute shader. That is, it enables a shader to access info on the size of the thread groups being used.
- `GridIds`: indicates the ids of the current thread group within the dispatch grid. That is, it enables a shader to access info on the index of the current thread group with respect to the dispatch grid.

For more info on how all these values relate to the corresponding HLSL inputs, see the `[numthreads]` docs [here](https://docs.microsoft.com/windows/win32/direct3dhlsl/sm5-attributes-numthreads). Note that `ThreadIds`, `GroupIds` and `GridIds` map to `SV_DispatchThreadID`, `SV_GroupThreadID` and `SV_GroupID` respectively, and the `GroupIds.Index` property maps to `SV_GroupIndex`.

## Working with images

As mentioned in the [GPU resource types](#gpu-resource-types) paragraph, there are several texture types that are specialized to work on image pixels, such as `ReadWriteTexture2D<T, TPixel>`. Let's imagine we want to write a compute shader that applies some image processing filter. We will need to load an image (in this example we will use the integrated APIs to do so, but external libraries such as [ImageSharp](https://github.com/SixLabors/ImageSharp) or just the `System.Drawing` can also be used) and then process it on the GPU, but without spending time on the CPU to convert pixels from a format such as BGRA32 to the normalized `float` values we want our shader to work on. We can do this by utilizing the `ReadWriteTexture2D<T, TPixel>` type as follows:

```csharp
// Load a texture from a specified image, and decode it in the BGRA32 format
using var texture = GraphicsDevice.Default.LoadReadWriteTexture2D<Bgra32, float4>("myImage.jpg");

// Run our shader on the texture we just loaded
GraphicsDevice.Default.For(texture.Width, texture.Height, new GrayscaleEffect(texture));

// Save the processed image by overwriting the original image
texture.Save("myImage.jpg");
```

With the compute shader being like this:

```csharp
[AutoConstructor]
public readonly partial struct GrayscaleEffect : IComputeShader
{
    public readonly IReadWriteNormalizedTexture2D<float4> texture;

    // Other captured resources or values here...

    public void Execute()
    {
        // Our image processing logic here. In this example, we are just
        // applying a naive grayscale effect to all pixels in the image.
        float3 rgb = texture[ThreadIds.XY].RGB;
        float avg = Hlsl.Dot(rgb, new(0.0722f, 0.7152f, 0.2126f));

        texture[ThreadIds.XY].RGB = avg;
    }
}
```

> **NOTE:** this is just an example to illustrate how these texture types can help with automatic pixel format conversion. You're free to use any library of choice to load and save image data, as well as to how to structure your compute shaders representing image effects. This is just one of the infinite possible effects that could be achieved by using **ComputeSharp**.

You can also use a similar technique to create a blank texture, render a single frame of a pixel shader, and save the output to a file:

```csharp
// Create a blank 1280x720 surface for us to render to
using var texture = GraphicsDevice.Default.AllocateReadWriteTexture2D<Bgra32, float4>(1280, 720);

// Using an existing shader from our samples, we can render a specific time frame
GraphicsDevice.Default.ForEach(texture, new FourColorGradient(0));

// Save the result to a file on disk
texture.Save("output.png");
```

## Shader metaprogramming

One of the reasons why **ComputeSharp** compiles shaders at runtime is that it allows it to support a number of dynamic scenarios, including shader metaprogramming. What this means is that it's possible to have a shader that captures an instance of a `Delegate` type (provided the signature matches the supported types mentioned above), and reuse it with different methods. The library will automatically run different variations of the shaders depending on what methods they are capturing. Here is an example:

```csharp
// Define a function we want to use in our shader. Captured functions
// need to be static methods annotated with [ShaderAttribute].
[ShaderMethod]
public static float Square(float x) => x * x;

[AutoConstructor]
public readonly partial struct ApplyFunction : IComputeShader
{
    public readonly ReadWriteBuffer<float> buffer;
    public readonly Func<float, float> function;

    public void Execute()
    {
        buffer[ThreadIds.X] = function(buffer[ThreadIds.X]);
    }
}
```

Then we can simply create a new `Func<float, float>` instance and use that to construct a new instance of the `ApplyFunction` type, to execute that shader on a target buffer. **ComputeSharp** will build or reuse a version of that shader with the supplied captured function and execute it on the GPU:

```csharp
float[] array = LoadSampleData();

// Allocate the buffer and copy data to it
using ReadWriteBuffer<float> buffer = GraphicsDevice.Default.AllocateReadWriteBuffer(array);

// Run the shader
GraphicsDevice.Default.For(width, height, new ApplyFunction(buffer, Square));

// Copy the processed data back as usual
buffer.CopyTo(array);
```

## Inspecting shaders

For users that are familiar with the HLSL language and might want to access more info on a given shader generated by **ComputeSharp**, such as the compiled HLSL code or the statistics exposed by the [DirectX 12 reflection APIs](https://docs.microsoft.com/windows/win32/api/d3d12shader/nn-d3d12shader-id3d12shaderreflection), the library includes a `ReflectionServices` class that allows to easily gather all these details on a given shader type.

Assuming we have the same shader defined in [Quick start](#quick-start), here is how this class can be used:

```csharp
ReflectionServices.GetShaderInfo<MainKernel>(out ShaderInfo shaderInfo);

// Access info here, for instance...
string hlslSource = shaderInfo.HlslSource;
uint numberOfResources = shaderInfo.BoundResourceCount;
uint instructionCount = shaderInfo.InstructionCount;
```

## DirectX interop

There are cases where it might be needed to manually interop with other DirectX APIs, to perform operations that are not included in the public API surface exposed by **ComputeSharp**. For instance, to copy the contents of a `ReadWriteTextre2D<T, TPixel>` to the backbuffer of a swap chain object, so that it can be rendered in a window. For these scenarios, the `InteropServices` class exposes APIs to easily retrieve the underlying COM pointers for any of the wrapping types in the library, and to perform a `QueryInterface` call on them to retrieve a COM pointer of a specified interface type. Here is how these APIs can be used:

```csharp
using ComPtr<ID3D12Device> d3D12Device = default;
using ComPtr<ID3D12Resource> d3D12Resource = default;

using ReadWriteBuffer<float> buffer = GraphicsDevice.Default.AllocateReadWriteBuffer<float>(128);

// Get the underlying ID3D12Device object
InteropServices.GetID3D12Device(GraphicsDevice.Default, __uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());

// Get the underlying ID3D12Resource object
InteropServices.GetID3D12Resource(buffer, __uuidof<ID3D12Resource>(), (void**)d3D12Resource.GetAddressOf());

// Now the COM objects can be used directly, eg. to display images with a swap chain
```

The `InteropServices` class is also used by the swap chain sample to access the underlying COM objects and use them the rendered frames in a Win32 window. The full source code is available [here](https://github.com/Sergio0694/ComputeSharp/blob/main/samples/ComputeSharp.SwapChain/Backend/SwapChainApplication%7BT%7D.cs), and it provides reference implementation of how these APIs can be used to interop with other DirectX objects.

## Precompiled shaders

When only using the main **ComputeSharp** project, dynamic shader compilation is not available. This makes the library have a much smaller impact on binary size, as it will not bundle the native DirectX compiler dependencies (which add about 20MB per CPU architecture). Additionally, precompiling shaders makes the code less error prone and the startup time much faster. In order to precompile a shader, it must not have dynamic features (ie. it cannot capture delegates, see the [shader metaprogramming](#shader-metaprogramming) paragraph) and it must be annotated with the `[EmbeddedBytecode]` attribute:

```csharp
[AutoConstructor]
[EmbeddedBytecode(8, 8, 1)]
public readonly partial struct MultiplyByTwo : IPixelShader<float4>
{
    public float4 Execute()
    {
        float2 uv = ThreadIds.Normalized.XY;
        float3 col = 0.5f + 0.5f * Hlsl.Cos(new float3(uv, uv.X) + new float3(0, 2, 4));

        return new(col, 1f);
    }
}
```

The values in the `[EmbeddedBytecode]` attribute map to the [`[numthreads]` HLSL attribute](https://docs.microsoft.com/windows/win32/direct3dhlsl/sm5-attributes-numthreads) for the shader entry point. Make sure to use the same values when dispatching the shader at runtime (either using the `GraphicsDevice.For` overload with explicit parameters for the tread numbers, or by relying on `GraphicsDevice.ForEach` to always use `(8, 8, 1)` for thread numbers, as it will be dispatching shaders on a 2D texture).

# Requirements

Building and running **ComputeSharp** from the .NET solution filter has the following prerequisites:
- Visual Studio 2022
- .NET 6 SDK
- .NET Core 3.1 SDK
- .NET Framework 4.7.2 targeting pack
- Windows x64 or ARM64

Additionally, in order to also build and run all UWP and WinUI 3 projects, these components need to be installed:

- UWP development workload
- Windows 10 SDKs 17763 and 19041
- Windows App SDK extension for VS2022

# F# support

If you're wondering whether it is possible to use **ComputeSharp** from an F# project, it is!

There is a caveat though: since the HLSL shader rewriter specifically works on C# syntax, it is necessary to write the actual shaders in C#. A simple way to do this is to have a small C# project with just the shader types, and then reference it from an F# project that will contain all the actual logic: every API to create GPU devices, allocate buffers and invoke shaders will work perfectly fine from F# as well.

Assuming we had the `MultiplyByTwo` shader defined in the [Quick start](#quick-start) paragraph referenced from a C# project, here is how we can actually use it directly from our F# project, with the same sample described above:

```fsharp
let array = [| for i in 1 .. 100 -> (float32)i |]

// Create the graphics buffer
use buffer = GraphicsDevice.Default.AllocateReadWriteBuffer(array)

let shader = new MultiplyByTwo(buffer)

// Run the shader (passed by reference)
GraphicsDevice.Default.For(100, &shader)

// Get the data back
buffer.CopyTo array
```

For a complete example, check out the F# sample [here](https://github.com/Sergio0694/ComputeSharp/blob/main/samples/ComputeSharp.Sample.FSharp/Program.fs).

# Sponsors

Huge thanks to these sponsors for directly supporting my work on **ComputeSharp**, it means a lot! 🙌

<a href="https://github.com/dgellow"><img src="https://avatars.githubusercontent.com/u/2451004" height="auto" width="60" style="border-radius:50%"></a>
<a href="https://github.com/Gavin-Williams"><img src="https://avatars.githubusercontent.com/u/4374176" height="auto" width="60" style="border-radius:50%"></a>
<a href="https://github.com/ptasev"><img src="https://avatars.githubusercontent.com/u/23424044" height="auto" width="60" style="border-radius:50%"></a>
<a href="https://github.com/xoofx"><img src="https://avatars.githubusercontent.com/u/715038" height="auto" width="60" style="border-radius:50%"></a>
<a href="https://github.com/hawkerm"><img src="https://avatars.githubusercontent.com/u/8959496" height="auto" width="60" style="border-radius:50%"></a>

# Special thanks

**ComputeSharp** was originally based on some of the code from the [DX12GameEngine](https://github.com/Aminator/DirectX12GameEngine) project by [Amin Delavar](https://github.com/Aminator).

Additionally, **ComputeSharp** uses NuGet packages from the following repositories:

- [.NET Community Toolkit](https://aka.ms/toolkit/dotnet)
- [Windows Community Toolkit](https://aka.ms/toolkit/windows)
- [TerraFX.Interop.Windows](https://github.com/terrafx/terrafx.interop.windows)
- [TerraFX.Interop.D3D12MemoryAllocator](https://github.com/terrafx/terrafx.interop.d3d12memoryallocator)
