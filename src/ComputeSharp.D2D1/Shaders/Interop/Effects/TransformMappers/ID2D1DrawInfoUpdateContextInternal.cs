using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// An internal version of <see cref="ID2D1DrawInfoUpdateContext"/> that supports being closed, to manage lifetime scopes.
/// </summary>
internal unsafe struct ID2D1DrawInfoUpdateContextInternal : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xB0, 0x5B, 0x2F, 0xCF,
                0x5E, 0x3F,
                0x6E, 0x4D,
                0x8F, 0xF1,
                0xD9,
                0xA7,
                0xEB,
                0x6C,
                0x02,
                0x50
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

    /// <summary>
    /// Closes the current instance.
    /// </summary>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT Close()
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1DrawInfoUpdateContext*, int>)this.lpVtbl[3])((ID2D1DrawInfoUpdateContext*)Unsafe.AsPointer(ref this));
    }
}