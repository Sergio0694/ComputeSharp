using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// An internal version of <see cref="ID2D1DrawInfoUpdateContext"/> that supports being closed, to manage lifetime scopes.
/// </summary>
[Guid("CF2F5BB0-3F5E-4D6E-8FF1-D9A7EB6C0250")]
internal unsafe struct ID2D1DrawInfoUpdateContextInternal
{
    /// <summary>
    /// Gets the <see cref="System.Guid"/> for <see cref="ID2D1DrawInfoUpdateContextInternal"/> (<c>CF2F5BB0-3F5E-4D6E-8FF1-D9A7EB6C0250</c>).
    /// </summary>
    public static ref readonly Guid Guid
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
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
            };

            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
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