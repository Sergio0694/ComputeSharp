using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// The native WinRT interface for <see cref="global::Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi"/> objects.
/// </summary>
[Guid("1A75B512-E9FA-49E6-A876-38CAE194013E")]
[NativeTypeName("class ICanvasResourceCreatorWithDpi : IInspectable")]
[NativeInheritance("IInspectable")]
internal unsafe struct ICanvasResourceCreatorWithDpi : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x12, 0xB5, 0x75, 0x1A,
                0xFA, 0xE9,
                0xE6, 0x49,
                0xA8,
                0x76,
                0x38,
                0xCA,
                0xE1,
                0x94,
                0x01,
                0x3E
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
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceCreatorWithDpi*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasResourceCreatorWithDpi*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceCreatorWithDpi*, uint>)this.lpVtbl[1])((ICanvasResourceCreatorWithDpi*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceCreatorWithDpi*, uint>)this.lpVtbl[2])((ICanvasResourceCreatorWithDpi*)Unsafe.AsPointer(ref this));
    }
}