using System;
using System.ComponentModel;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.__Internals;

/// <summary>
/// A <see langword="class"/> that uses the DXC APIs to compile compute shaders.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This type is not intended to be used directly by user code")]
public static class ShaderCompiler
{
    /// <summary>
    /// Compiles a new HLSL shader from the input source code.
    /// </summary>
    /// <param name="builder">The <see cref="ArrayPoolStringBuilder"/> instance with the HLSL source code to compile.</param>
    /// <returns>The bytecode for the compiled shader.</returns>
    public static unsafe IntPtr CompileShader(ref ArrayPoolStringBuilder builder)
    {
        using ComPtr<IDxcBlob> shader = Shaders.Translation.ShaderCompiler.Instance.CompileShader(builder.WrittenSpan);

        builder.Dispose();

        _ = shader.Get()->AddRef();

        return (IntPtr)shader.Get();
    }
}
