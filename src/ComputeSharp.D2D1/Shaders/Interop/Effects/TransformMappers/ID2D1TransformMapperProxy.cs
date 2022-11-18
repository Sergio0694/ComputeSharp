using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.D2D1.Interop.Effects;
using TerraFX.Interop.Windows;
#if NET6_0_OR_GREATER
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RuntimeHelpers = ComputeSharp.D2D1.NetStandard.System.Runtime.CompilerServices.RuntimeHelpers;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// An implementation of the <see cref="ID2D1TransformMapper"/> interface wrapping <see cref="ID2D1TransformMapperInterop"/>.
/// </summary>
internal unsafe partial struct ID2D1TransformMapperProxy
{
    /// <summary>
    /// The shared method table pointer for all <see cref="ID2D1TransformMapper"/> instances.
    /// </summary>
    private static readonly void** Vtbl = InitVtbl();

    /// <summary>
    /// Builds the custom method table pointer for <see cref="ID2D1TransformMapper"/>.
    /// </summary>
    /// <returns>The method table pointer for <see cref="ID2D1TransformMapper"/>.</returns>
    private static void** InitVtbl()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(ID2D1TransformMapperProxy), sizeof(void*) * 6);

#if NET6_0_OR_GREATER
        lpVtbl[0] = (delegate* unmanaged<ID2D1TransformMapperProxy*, Guid*, void**, int>)&QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<ID2D1TransformMapperProxy*, uint>)&AddRef;
        lpVtbl[2] = (delegate* unmanaged<ID2D1TransformMapperProxy*, uint>)&Release;
        lpVtbl[3] = (delegate* unmanaged<ID2D1TransformMapperProxy*, ID2D1DrawInfoUpdateContext*, RECT*, RECT*, uint, RECT*, RECT*, int>)&MapInputRectsToOutputRect;
        lpVtbl[4] = (delegate* unmanaged<ID2D1TransformMapperProxy*, RECT*, RECT*, uint, int>)&MapOutputRectToInputRects;
        lpVtbl[5] = (delegate* unmanaged<ID2D1TransformMapperProxy*, uint, RECT, RECT*, int>)&MapInvalidRect;
#else
        lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(QueryInterfaceWrapper);
        lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(AddRefWrapper);
        lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ReleaseWrapper);
        lpVtbl[3] = (void*)Marshal.GetFunctionPointerForDelegate(MapInputRectsToOutputRectWrapper);
        lpVtbl[4] = (void*)Marshal.GetFunctionPointerForDelegate(MapOutputRectToInputRectsWrapper);
        lpVtbl[5] = (void*)Marshal.GetFunctionPointerForDelegate(MapInvalidRectWrapper);
#endif

        return lpVtbl;
    }

    /// <summary>
    /// The vtable pointer for the current instance.
    /// </summary>
    private void** lpVtbl;

    /// <summary>
    /// The current reference count for the object (from <see cref="IUnknown"/>).
    /// </summary>
    private volatile int referenceCount;

    /// <summary>
    /// A <see cref="GCHandle"/> to the wrapped <see cref="ID2D1TransformMapperInterop"/> instance.
    /// </summary>
    private GCHandle transformMapperHandle;

    /// <summary>
    /// The factory method for <see cref="ID2D1TransformMapperProxy"/> instances.
    /// </summary>
    /// <param name="transformMapper">The input <see cref="ID2D1TransformMapperInterop"/> instance to wrap.</param>
    /// <param name="d2D1TransformMapperProxy">The resulting <see cref="ID2D1TransformMapperProxy"/> instance.</param>
    public static void Factory(
        ID2D1TransformMapperInterop transformMapper,
        ID2D1TransformMapperProxy** d2D1TransformMapperProxy)
    {
        ID2D1TransformMapperProxy* @this = (ID2D1TransformMapperProxy*)NativeMemory.Alloc((nuint)sizeof(ID2D1TransformMapperProxy));

        @this->lpVtbl = Vtbl;
        @this->referenceCount = 1;
        @this->transformMapperHandle = GCHandle.Alloc(transformMapper);

        *d2D1TransformMapperProxy = @this;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    public int QueryInterface(Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(ID2D1TransformMapper.Guid))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        *ppvObject = null;

        return E.E_NOINTERFACE;
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    public uint AddRef()
    {
        return (uint)Interlocked.Increment(ref this.referenceCount);
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    public uint Release()
    {
        uint referenceCount = (uint)Interlocked.Decrement(ref this.referenceCount);

        if (referenceCount == 0)
        {
            this.transformMapperHandle.Free();

            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }
}