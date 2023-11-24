using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// The native WinRT interface for <see cref="global::Microsoft.Graphics.Canvas.ICanvasResourceCreator"/> objects.
/// </summary>
[NativeTypeName("class ICanvasResourceCreator : IInspectable")]
[NativeInheritance("IInspectable")]
internal unsafe struct ICanvasResourceCreator : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xA8, 0x8A, 0x6D, 0x8F,
                0x2F, 0x49,
                0xC6, 0x4B,
                0xB3,
                0xD0,
                0xE7,
                0xF5,
                0xEA,
                0xE8,
                0x4B,
                0x11
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasResourceCreator*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasResourceCreator*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasResourceCreator*, uint>)this.lpVtbl[1])((ICanvasResourceCreator*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasResourceCreator*, uint>)this.lpVtbl[2])((ICanvasResourceCreator*)Unsafe.AsPointer(ref this));
    }
}