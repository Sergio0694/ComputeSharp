using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    /// <typeparam name="TLoader">The type of bytecode loader being used.</typeparam>
    /// <typeparam name="T">The type of shader being dispatched.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the bytecode.</param>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    public static unsafe void LoadDynamicBytecode<TLoader, T>(ref TLoader loader, int threadsX, int threadsY, int threadsZ, in T shader)
        where TLoader : struct, IBytecodeLoader
        where T : struct, IShader
    {
        Unsafe.AsRef(in shader).BuildHlslSource(out ArrayPoolStringBuilder builder, threadsX, threadsY, threadsZ);

        using ComPtr<IDxcBlob> dxcBlobBytecode = Shaders.Translation.ShaderCompiler.Instance.Compile(builder.WrittenSpan);

        builder.Dispose();

        loader.LoadDynamicBytecode((IntPtr)dxcBlobBytecode.Get());
    }
}