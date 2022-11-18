using System;
using System.Diagnostics.CodeAnalysis;
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
internal unsafe partial struct D2D1TransformMapperImpl
{
#if !NET6_0_OR_GREATER
    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int QueryInterfaceDelegate(D2D1TransformMapperImpl* @this, Guid* riid, void** ppvObject);

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint AddRefDelegate(D2D1TransformMapperImpl* @this);

    /// <inheritdoc cref="IUnknown.Release"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint ReleaseDelegate(D2D1TransformMapperImpl* @this);

    /// <inheritdoc cref="ID2D1TransformMapper.MapInputRectsToOutputRect"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int MapInputRectsToOutputRectDelegate(
        D2D1TransformMapperImpl* @this,
        ID2D1DrawInfoUpdateContext* d2D1DrawInfoUpdateContext,
        RECT* inputRects,
        RECT* inputOpaqueSubRects,
        uint inputRectCount,
        RECT* outputRect,
        RECT* outputOpaqueSubRect);

    /// <inheritdoc cref="ID2D1TransformMapper.MapOutputRectToInputRects"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int MapOutputRectToInputRectsDelegate(
        D2D1TransformMapperImpl* @this,
        RECT* outputRect,
        RECT* inputRects,
        uint inputRectsCount);

    /// <inheritdoc cref="ID2D1TransformMapper.MapInvalidRect"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int MapInvalidRectDelegate(
        D2D1TransformMapperImpl* @this,
        uint inputIndex,
        RECT invalidInputRect,
        RECT* invalidOutputRect);

    /// <summary>
    /// A cached <see cref="QueryInterfaceDelegate"/> instance wrapping <see cref="QueryInterface(Guid*, void**)"/>.
    /// </summary>
    private static readonly QueryInterfaceDelegate QueryInterfaceWrapper = QueryInterface;

    /// <summary>
    /// A cached <see cref="AddRefDelegate"/> instance wrapping <see cref="AddRef()"/>.
    /// </summary>
    private static readonly AddRefDelegate AddRefWrapper = AddRef;

    /// <summary>
    /// A cached <see cref="ReleaseDelegate"/> instance wrapping <see cref="Release()"/>.
    /// </summary>
    private static readonly ReleaseDelegate ReleaseWrapper = Release;

    /// <summary>
    /// A cached <see cref="MapInputRectsToOutputRectDelegate"/> instance wrapping <see cref="MapInputRectsToOutputRect"/>.
    /// </summary>
    private static readonly MapInputRectsToOutputRectDelegate MapInputRectsToOutputRectWrapper = MapInputRectsToOutputRect;

    /// <summary>
    /// A cached <see cref="MapOutputRectToInputRectsDelegate"/> instance wrapping <see cref="MapOutputRectToInputRects"/>.
    /// </summary>
    private static readonly MapOutputRectToInputRectsDelegate MapOutputRectToInputRectsWrapper = MapOutputRectToInputRects;

    /// <summary>
    /// A cached <see cref="MapInvalidRectDelegate"/> instance wrapping <see cref="MapInvalidRect"/>.
    /// </summary>
    private static readonly MapInvalidRectDelegate MapInvalidRectWrapper = MapInvalidRect;
#endif

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
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(D2D1TransformMapperImpl), sizeof(void*) * 6);

#if NET6_0_OR_GREATER
        lpVtbl[0] = (delegate* unmanaged<D2D1TransformMapperImpl*, Guid*, void**, int>)&QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<D2D1TransformMapperImpl*, uint>)&AddRef;
        lpVtbl[2] = (delegate* unmanaged<D2D1TransformMapperImpl*, uint>)&Release;
        lpVtbl[3] = (delegate* unmanaged<D2D1TransformMapperImpl*, ID2D1DrawInfoUpdateContext*, RECT*, RECT*, uint, RECT*, RECT*, int>)&MapInputRectsToOutputRect;
        lpVtbl[4] = (delegate* unmanaged<D2D1TransformMapperImpl*, RECT*, RECT*, uint, int>)&MapOutputRectToInputRects;
        lpVtbl[5] = (delegate* unmanaged<D2D1TransformMapperImpl*, uint, RECT, RECT*, int>)&MapInvalidRect;
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
    /// The <see cref="System.Threading.SpinLock"/> instance to synchronize accesses to this instance.
    /// </summary>
    private SpinLock spinLock;

    /// <summary>
    /// The factory method for <see cref="D2D1TransformMapperImpl"/> instances.
    /// </summary>
    /// <param name="transformMapper">The input <see cref="ID2D1TransformMapperInterop"/> instance to wrap.</param>
    /// <param name="d2D1TransformMapperProxy">The resulting <see cref="D2D1TransformMapperImpl"/> instance.</param>
    public static void Factory(
        ID2D1TransformMapperInterop transformMapper,
        D2D1TransformMapperImpl** d2D1TransformMapperProxy)
    {
        D2D1TransformMapperImpl* @this = (D2D1TransformMapperImpl*)NativeMemory.Alloc((nuint)sizeof(D2D1TransformMapperImpl));

        @this->lpVtbl = Vtbl;
        @this->referenceCount = 1;
        @this->transformMapperHandle = GCHandle.Alloc(transformMapper);
        @this->spinLock = new SpinLock(enableThreadOwnerTracking: false);

        *d2D1TransformMapperProxy = @this;
    }

    /// <summary>
    /// Gets a reference to the <see cref="System.Threading.SpinLock"/> instance used to synchronize reference counts.
    /// </summary>
    [UnscopedRef]
    public ref SpinLock SpinLock => ref this.spinLock;

    /// <summary>
    /// Ensures the target of the current CCW is tracked and is keeping it alive.
    /// </summary>
    /// <param name="target">The associated <see cref="ID2D1TransformMapperInterop"/> object to track.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnsureTargetIsTracked(ID2D1TransformMapperInterop target)
    {
        this.transformMapperHandle.Target = target;
    }

    /// <summary>
    /// Copies the current object onto a target pointer.
    /// </summary>
    /// <param name="transformMapper">The target <see cref="ID2D1TransformMapper"/> pointer.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyToWithNoLock(ID2D1TransformMapper** transformMapper)
    {
        this.referenceCount++;

        *transformMapper = (ID2D1TransformMapper*)Unsafe.AsPointer(ref this);
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    ///<remarks>This method is the same as <see cref="QueryInterface(Guid*, void**)"/> but skips locks.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int QueryInterfaceWithNoLock(Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(ID2D1TransformMapper.Guid))
        {
            this.referenceCount++;

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        *ppvObject = null;

        return E.E_NOINTERFACE;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    private int QueryInterface(Guid* riid, void** ppvObject)
    {
        bool lockTaken = false;

        this.spinLock.Enter(ref lockTaken);

        try
        {
            return QueryInterfaceWithNoLock(riid, ppvObject);
        }
        finally
        {
            this.spinLock.Exit();
        }
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    private uint AddRef()
    {
        bool lockTaken = false;

        this.spinLock.Enter(ref lockTaken);

        try
        {
            return (uint)++this.referenceCount;
        }
        finally
        {
            this.spinLock.Exit();
        }
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    private uint Release()
    {
        bool lockTaken = false;

        this.spinLock.Enter(ref lockTaken);

        try
        {
            uint referenceCount = (uint)--this.referenceCount;

            // There are two special cases to consider here:
            //   - If the reference count is 1, it means that the D2D1TransformMapper<T> object is the only
            //     thing keeping this CCW alive. In that case, we can reset the GC handle pointing back to it,
            //     so that it can be collected if nobody else is referencing it. This breaks the reference cycle
            //     between the two object, which would have otherwise caused them to remain alive forever.
            //   - If the reference count is 0, the object is simply being destroyed.
            if (referenceCount == 1)
            {
                this.transformMapperHandle.Target = null;
            }
            else if (referenceCount == 0)
            {
                this.transformMapperHandle.Free();

                NativeMemory.Free(Unsafe.AsPointer(ref this));
            }

            return referenceCount;
        }
        finally
        {
            this.spinLock.Exit();
        }
    }
}