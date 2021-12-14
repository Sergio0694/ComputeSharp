using System;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.__Internals;

/// <summary>
/// A <see langword="class"/> that uses the DXC APIs to compile compute shaders.
/// </summary>
public static class ShaderCompiler
{
    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="source">The HLSL source code to compile.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    public static unsafe IntPtr CompileShader(ReadOnlySpan<char> source)
    {
        using ComPtr<IDxcBlob> shader = Shaders.Translation.ShaderCompiler.Instance.CompileShader(source);

        _ = shader.Get()->AddRef();

        return (IntPtr)shader.Get();
    }
}
