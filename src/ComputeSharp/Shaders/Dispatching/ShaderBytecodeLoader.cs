using System;
using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;
using ComputeSharp.Shaders.Models;

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
    /// <exception cref="NotSupportedException">Thrown if the HLSL bytecode is not available.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ICachedShader GetCachedShader()
    {
        default(NotSupportedException).ThrowIf(this.cachedShader is null);

        return this.cachedShader;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void LoadEmbeddedBytecode(ReadOnlySpan<byte> bytecode)
    {
        default(InvalidOperationException).ThrowIf(this.cachedShader is not null);

        this.cachedShader = new ICachedShader.Embedded(bytecode);
    }
}