using System;
using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using ComputeSharp.Shaders.Models;
using TerraFX.Interop.DirectX;

#pragma warning disable CS0618

namespace ComputeSharp.Shaders.Dispatching;

/// <summary>
/// A bytecode loader for compute shaders.
/// </summary>
internal struct ShaderBytecodeLoader : IBytecodeLoader
{
    /// <summary>
    /// The current cached shader instance.
    /// </summary>
    private ICachedShader? cachedShader;

    /// <summary>
    /// Gets the current cached shader instance.
    /// </summary>
    /// <returns>The current <see cref="ICachedShader"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the shader has not been initialized.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ICachedShader GetCachedShader()
    {
        default(InvalidOperationException).ThrowIf(this.cachedShader is null);

        return this.cachedShader;
    }

    /// <inheritdoc/>
    public unsafe void LoadDynamicBytecode(IntPtr handle)
    {
        default(InvalidOperationException).ThrowIf(this.cachedShader is not null);
        default(InvalidOperationException).ThrowIf(handle == IntPtr.Zero);

        this.cachedShader = new ICachedShader.Dynamic((IDxcBlob*)handle);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void LoadEmbeddedBytecode(ReadOnlySpan<byte> bytecode)
    {
        default(InvalidOperationException).ThrowIf(this.cachedShader is not null);

        this.cachedShader = new ICachedShader.Embedded(bytecode);
    }
}