using System;
using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop.DirectX;
using ComputeSharp.Shaders.Translation.Models;

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
        if (this.cachedShader is not ICachedShader cachedShader)
        {
            return ThrowHelper.ThrowInvalidOperationException<ICachedShader>("The shader has not been initialized");
        }

        return cachedShader;
    }

    /// <inheritdoc/>
    public unsafe void LoadDynamicBytecode(IntPtr handle)
    {
        if (this.cachedShader is not null)
        {
            ThrowHelper.ThrowInvalidOperationException("The shader has already been initialized");
        }

        this.cachedShader = new ICachedShader.Dynamic((IDxcBlob*)handle);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void LoadEmbeddedBytecode(ReadOnlySpan<byte> bytecode)
    {
        if (this.cachedShader is not null)
        {
            ThrowHelper.ThrowInvalidOperationException("The shader has already been initialized");
        }

        this.cachedShader = new ICachedShader.Embedded(bytecode);
    }
}
