using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.__Internals;

/// <summary>
/// A <see langword="class"/> that uses the DXC APIs to compile compute shaders.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This type is not intended to be used directly by user code")]
public static class D2D1ShaderCompiler
{
    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <typeparam name="TLoader">The type of bytecode loader being used.</typeparam>
    /// <typeparam name="T">The type of D2D1 shader being dispatched.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the bytecode.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the D2D1 shader to run.</param>
    /// <param name="shaderProfile">The shader profile to use to compile the shader.</param>
    /// <param name="options">The compiler options to use to compile the shader.</param>
    public static unsafe void LoadDynamicBytecode<TLoader, T>(ref TLoader loader, in T shader, D2D1ShaderProfile shaderProfile, D2D1CompileOptions options)
        where TLoader : struct, ID2D1BytecodeLoader
        where T : struct, ID2D1Shader
    {
        Unsafe.AsRef(in shader).BuildHlslSource(out string hlslSource);

        using ComPtr<ID3DBlob> d3DBlobBytecode = Shaders.Translation.D3DCompiler.Compile(
            hlslSource.AsSpan(),
            shaderProfile,
            options);

        loader.LoadDynamicBytecode((IntPtr)d3DBlobBytecode.Get());
    }
}