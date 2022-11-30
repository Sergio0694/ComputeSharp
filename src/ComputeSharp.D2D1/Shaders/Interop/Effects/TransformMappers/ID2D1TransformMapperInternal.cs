using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// The internal transform mapper manager interface.
/// </summary>
[Guid("C5D8FC65-FB86-4C2D-9EF5-95AEC639C952")]
internal unsafe struct ID2D1TransformMapperInternal
{
    /// <summary>
    /// Gets the <see cref="System.Guid"/> for <see cref="ID2D1TransformMapper"/> (<c>C5D8FC65-FB86-4C2D-9EF5-95AEC639C952</c>).
    /// </summary>
    public static ref readonly Guid Guid
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
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
            };

            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
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
        return ((delegate* unmanaged[Stdcall]<ID2D1TransformMapperInternal*, void**, int>)this.lpVtbl[3])(
            (ID2D1TransformMapperInternal*)Unsafe.AsPointer(ref this),
            ppvHandle);
    }
}