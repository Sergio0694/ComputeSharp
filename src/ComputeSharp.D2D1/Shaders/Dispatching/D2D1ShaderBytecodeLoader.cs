using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Dispatching;

/// <summary>
/// A bytecode loader for D2D1 shaders.
/// </summary>
internal unsafe struct D2D1ShaderBytecodeLoader : ID2D1BytecodeLoader
{
    /// <summary>
    /// A pointer to the precompiled shader, if available.
    /// </summary>
    private byte* embeddedBytecodePtr;

    /// <summary>
    /// The length of the precompiled shader buffer, if available.
    /// </summary>
    private int embeddedBytecodeSize;

    /// <summary>
    /// The dynamically compiled shader bytecode blob, if available.
    /// </summary>
    private ComPtr<ID3DBlob> d3DBlob;

    /// <summary>
    /// Gets the resulting shader bytecode that was available.
    /// </summary>
    /// <param name="precompiledBytecode">The precompiled bytecode, if available.</param>
    /// <returns>An <see cref="ID3DBlob"/> with the shader bytecode, if dynamically compiled.</returns>
    /// <remarks>Either <paramref name="precompiledBytecode"/> is empty, or the returned pointer is <see langword="null"/>.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComPtr<ID3DBlob> GetResultingShaderBytecode(out ReadOnlySpan<byte> precompiledBytecode)
    {
        precompiledBytecode = new ReadOnlySpan<byte>(this.embeddedBytecodePtr, this.embeddedBytecodeSize);

        return this.d3DBlob.Move();
    }

    /// <inheritdoc/>
    public unsafe void LoadDynamicBytecode(IntPtr handle)
    {
        if (this.embeddedBytecodePtr is not null ||
            this.d3DBlob.Get() is not null)
        {
            ThrowHelper.ThrowInvalidOperationException("The shader has already been initialized.");
        }

        if (handle == IntPtr.Zero)
        {
            ThrowHelper.ThrowNotSupportedException("Runtime shader compilation is not supported by the current configuration.");
        }

        this.d3DBlob = new ComPtr<ID3DBlob>((ID3DBlob*)handle);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void LoadEmbeddedBytecode(ReadOnlySpan<byte> bytecode)
    {
        if (this.embeddedBytecodePtr is not null ||
            this.d3DBlob.Get() is not null)
        {
            ThrowHelper.ThrowInvalidOperationException("The shader has already been initialized.");
        }

        this.embeddedBytecodePtr = (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(bytecode));
        this.embeddedBytecodeSize = bytecode.Length;
    }
}
