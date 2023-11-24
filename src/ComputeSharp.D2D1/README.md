![](https://user-images.githubusercontent.com/10199417/

# Overview 📖

**ComputeSharp.D2D1** is a library to write D2D1 pixel shaders entirely with C# code, and to easily register and create `ID2D1Effect`-s from them. This shares the same base APIs (primitives, intrinsics, etc.) as **ComputeSharp**, but then adds D2D1 specific support, instead of using DX12 compute shaders like the main package. This means it offers the ability to implement D2D1 pixel shaders, and to then either load them manually, or to register a D2D1 effect using them, optionally with a custom draw transform as well.

# Quick start 🚀

Here's a simple D2D1 pixel shader written using **ComputeSharp.D2D1**:

```csharp
[D2DInputCount(1)]
[D2DInputSimple(0)]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
[D2DGeneratedPixelShaderDescriptor]
public readonly partial struct DifferenceEffect(float amount) : ID2D1PixelShader
{
    /// <inheritdoc/>
    public float4 Execute()
    {
        float4 color = D2D.GetInput(0);
        float3 rgb = Hlsl.Saturate(this.amount - color.RGB);

        return new(rgb, 1);
    }
}
```

This can then be used directly to get the shader bytecode and the buffer, like so:

```csharp
ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<DifferenceEffect>();
ReadOnlyMemory<byte> buffer = D2D1PixelShader.GetConstantBuffer(new DifferenceEffect(1));
```

There are also several other APIs to easily register a pixel shader effect from a shader written using **ComputeSharp.D2D1**, and to then create an `ID2D1Effect` instance from it (from the `D2D1PixelShaderEffect` type), as well as for reflecting into a shader and extract information about it, such as its HLSL source code (from the `D2D1ReflectionServices` type).

# There's more!
For a complete list of all features available in **ComputeSharp**, check the documentation in the [GitHub repo](https://github.com/Sergio0694/ComputeSharp).