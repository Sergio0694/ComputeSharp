using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// An interop wrapper type for Win2D objects (see <see href="https://microsoft.github.io/Win2D/WinUI3/html/Interop.htm"/>).
/// </summary>
[NativeTypeName("class ICanvasResourceWrapperNative : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct ICanvasResourceWrapperNative : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x8D, 0x68, 0x10, 0x5F,
                0x55, 0xEA,
                0x55, 0x4D,
                0xA3,
                0xB0,
                0x4D,
                0xDB,
                0x55,
                0xC0,
                0xC2,
                0x0A
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
        return ((delegate* unmanaged[MemberFunction]<ICanvasResourceWrapperNative*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasResourceWrapperNative*, uint>)this.lpVtbl[1])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasResourceWrapperNative*, uint>)this.lpVtbl[2])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Interface provided by various Win2D objects that is able to retrieve the wrapped resource.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT GetNativeResource(ICanvasDevice* device, float dpi, [NativeTypeName("REFIID")] Guid* iid, void** resource)
    {
        return ((delegate* unmanaged[MemberFunction]<ICanvasResourceWrapperNative*, ICanvasDevice*, float, Guid*, void**, int>)this.lpVtbl[3])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this), device, dpi, iid, resource);
    }
}