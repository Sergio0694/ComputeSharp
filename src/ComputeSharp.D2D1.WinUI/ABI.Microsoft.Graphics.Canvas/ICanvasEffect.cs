using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055, IDE1006

namespace ABI.Microsoft.Graphics.Canvas.Effects;

/// <summary>
/// The native WinRT interface for <see cref="global::Microsoft.Graphics.Canvas.Effects.ICanvasEffect"/> objects.
/// </summary>
[Guid("0EF96F8C-9B5E-4BF0-A399-AAD8CE53DB55")]
[NativeTypeName("class ICanvasEffect : IInspectable")]
[NativeInheritance("IInspectable")]
internal unsafe struct ICanvasEffect : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x8C, 0x6F, 0xF9, 0x0E,
                0x5E, 0x9B,
                0xF0, 0x4B,
                0x99,
                0xA3,
                0xAA,
                0xD8,
                0xCE,
                0x53,
                0xDB,
                0x55
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
        return ((delegate* unmanaged[Stdcall]<ICanvasEffect*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasEffect*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffect*, uint>)this.lpVtbl[1])((ICanvasEffect*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffect*, uint>)this.lpVtbl[2])((ICanvasEffect*)Unsafe.AsPointer(ref this));
    }
}