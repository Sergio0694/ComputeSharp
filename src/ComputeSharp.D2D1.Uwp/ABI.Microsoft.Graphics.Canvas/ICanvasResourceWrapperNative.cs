using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// An interop wrapper type for Win2D objects (see <see href="https://microsoft.github.io/Win2D/WinUI3/html/Interop.htm"/>).
/// </summary>
[Guid("5F10688D-EA55-4D55-A3B0-4DDB55C0C20A")]
[NativeTypeName("class ICanvasResourceWrapperNative : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct ICanvasResourceWrapperNative
{
    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, uint>)this.lpVtbl[1])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, uint>)this.lpVtbl[2])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Interface provided by various Win2D objects that is able to retrieve the wrapped resource.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    [return: NativeTypeName("HRESULT")]
    public HRESULT GetNativeResource([NativeTypeName("ICanvasDevice*")] void* device, float dpi, [NativeTypeName("REFIID")] Guid* iid, void** resource)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasResourceWrapperNative*, void*, float, Guid*, void**, int>)this.lpVtbl[3])((ICanvasResourceWrapperNative*)Unsafe.AsPointer(ref this), device, dpi, iid, resource);
    }
}