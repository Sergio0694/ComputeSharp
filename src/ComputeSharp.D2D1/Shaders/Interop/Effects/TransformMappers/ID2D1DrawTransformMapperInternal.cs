using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// The internal transform mapper manager interface.
/// </summary>
internal unsafe struct ID2D1DrawTransformMapperInternal : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x65, 0xFC, 0xD8, 0xC5,
                0x86, 0xFB,
                0x2D, 0x4C,
                0x9E, 0xF5,
                0x95,
                0xAE,
                0xC6,
                0x39,
                0xC9,
                0x52
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

    /// <summary>
    /// Gets a <see cref="GCHandle"/> value for the managed wrapper associated with this CCW.
    /// </summary>
    /// <param name="ppvHandle">The <see cref="GCHandle"/> value for the managed wrapper associated with this CCW.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT GetManagedWrapperHandle(void** ppvHandle)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1DrawTransformMapperInternal*, void**, int>)this.lpVtbl[3])(
            (ID2D1DrawTransformMapperInternal*)Unsafe.AsPointer(ref this),
            ppvHandle);
    }
}