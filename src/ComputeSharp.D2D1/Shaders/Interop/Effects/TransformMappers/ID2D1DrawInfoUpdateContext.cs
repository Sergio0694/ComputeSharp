using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// The updater for <see cref="ID2D1DrawInfo"/> type to use with built-in effects.
/// </summary>
internal unsafe struct ID2D1DrawInfoUpdateContext : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x40, 0x5B, 0x0C, 0x43,
                0x16, 0xAE,
                0x5F, 0x48,
                0x90, 0xE6,
                0x4F,
                0xA4,
                0x91,
                0x51,
                0x44,
                0xB6
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

    /// <summary>
    /// Retrieves the size of the constant buffer for the current effect.
    /// </summary>
    /// <param name="size">The resulting size of the constant buffer.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT GetConstantBufferSize(uint* size)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DrawInfoUpdateContext*, uint*, int>)this.lpVtbl[3])(
            (ID2D1DrawInfoUpdateContext*)Unsafe.AsPointer(ref this),
            size);
    }

    /// <summary>
    /// Retrieves the constant buffer data for the current effect.
    /// </summary>
    /// <param name="buffer">A pointer to the buffer to fill with the constant buffer data.</param>
    /// <param name="bufferCount">The size of the buffer provided through <paramref name="buffer"/>.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT GetConstantBuffer(byte* buffer, uint bufferCount)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DrawInfoUpdateContext*, byte*, uint, int>)this.lpVtbl[4])(
            (ID2D1DrawInfoUpdateContext*)Unsafe.AsPointer(ref this),
            buffer,
            bufferCount);
    }

    /// <summary>
    /// Sets the constant buffer data for the current effect.
    /// </summary>
    /// <param name="buffer">A pointer to the constant buffer data to use to update the effect.</param>
    /// <param name="bufferCount">The size of the buffer provided through <paramref name="buffer"/>.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT SetConstantBuffer(byte* buffer, uint bufferCount)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DrawInfoUpdateContext*, byte*, uint, int>)this.lpVtbl[5])(
            (ID2D1DrawInfoUpdateContext*)Unsafe.AsPointer(ref this),
            buffer,
            bufferCount);
    }
}