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
#endif

    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1TransformMapperImpl"/> instance, for <see cref="ID2D1TransformMapper"/>.
    /// </summary>
    private static readonly void** VtblForID2D1TransformMapper = InitVtblForID2D1TransformMapperAndID2D1TransformMapperInternal();

    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1TransformMapperImpl"/> instance, for <see cref="ID2D1TransformMapperInternal"/>.
    /// </summary>
    private static readonly void** VtblForID2D1TransformMapperInternal = &VtblForID2D1TransformMapper[6];

    /// <summary>
    /// Initializes the combined vtable for <see cref="ID2D1TransformMapper"/> and <see cref="ID2D1TransformMapperInternal"/>.
    /// </summary>
    /// <returns>The combined vtable for <see cref="ID2D1TransformMapper"/> and <see cref="ID2D1TransformMapperInternal"/>.</returns>
    private static void** InitVtblForID2D1TransformMapperAndID2D1TransformMapperInternal()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(D2D1TransformMapperImpl), sizeof(void*) * 10);

        // ID2D1ResourceTextureManager
#if NET6_0_OR_GREATER
        lpVtbl[0] = (delegate* unmanaged<D2D1TransformMapperImpl*, Guid*, void**, int>)&ID2D1TransformMapperMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<D2D1TransformMapperImpl*, uint>)&ID2D1TransformMapperMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<D2D1TransformMapperImpl*, uint>)&ID2D1TransformMapperMethods.Release;
        lpVtbl[3] = (delegate* unmanaged<D2D1TransformMapperImpl*, ID2D1DrawInfoUpdateContext*, RECT*, RECT*, uint, RECT*, RECT*, int>)&ID2D1TransformMapperMethods.MapInputRectsToOutputRect;
        lpVtbl[4] = (delegate* unmanaged<D2D1TransformMapperImpl*, RECT*, RECT*, uint, int>)&ID2D1TransformMapperMethods.MapOutputRectToInputRects;
        lpVtbl[5] = (delegate* unmanaged<D2D1TransformMapperImpl*, uint, RECT, RECT*, int>)&ID2D1TransformMapperMethods.MapInvalidRect;
#else
        lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperMethods.QueryInterfaceWrapper);
        lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperMethods.AddRefWrapper);
        lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperMethods.ReleaseWrapper);
        lpVtbl[3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperMethods.MapInputRectsToOutputRectWrapper);
        lpVtbl[4] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperMethods.MapOutputRectToInputRectsWrapper);
        lpVtbl[5] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperMethods.MapInvalidRectWrapper);
#endif

        // ID2D1TransformMapperInternal
#if NET6_0_OR_GREATER
        lpVtbl[6 + 0] = (delegate* unmanaged<D2D1TransformMapperImpl*, Guid*, void**, int>)&ID2D1TransformMapperInternalMethods.QueryInterface;
        lpVtbl[6 + 1] = (delegate* unmanaged<D2D1TransformMapperImpl*, uint>)&ID2D1TransformMapperInternalMethods.AddRef;
        lpVtbl[6 + 2] = (delegate* unmanaged<D2D1TransformMapperImpl*, uint>)&ID2D1TransformMapperInternalMethods.Release;
        lpVtbl[6 + 3] = (delegate* unmanaged<D2D1TransformMapperImpl*, void**, int>)&ID2D1TransformMapperInternalMethods.GetManagedWrapperHandle;
#else
        lpVtbl[6 + 0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperInternalMethods.QueryInterfaceWrapper);
        lpVtbl[6 + 1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperInternalMethods.AddRefWrapper);
        lpVtbl[6 + 2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperInternalMethods.ReleaseWrapper);
        lpVtbl[6 + 3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1TransformMapperInternalMethods.GetManagedWrapperHandleWrapper);
#endif

        return lpVtbl;
    }

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1TransformMapper"/>.
    /// </summary>
    private void** lpVtblForID2D1TransformMapper;

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1TransformMapperInternal"/>.
    /// </summary>
    private void** lpVtblForID2D1TransformMapperInternal;

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

        @this->lpVtblForID2D1TransformMapper = VtblForID2D1TransformMapper;
        @this->lpVtblForID2D1TransformMapperInternal = VtblForID2D1TransformMapperInternal;
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
    /// <remarks>This method must only be called if the caller has taken a lock on <see cref="SpinLock"/> already.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyToWithNoLock(ID2D1TransformMapper** transformMapper)
    {
        this.referenceCount++;

        *transformMapper = (ID2D1TransformMapper*)Unsafe.AsPointer(ref this);
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    /// <remarks>
    /// <para>This method is the same as <see cref="QueryInterface(Guid*, void**)"/> but skips locks.</para>
    /// <para>This method must only be called if the caller has taken a lock on <see cref="SpinLock"/> already.</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int QueryInterfaceWithNoLock(Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        // ID2D1TransformMapper
        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(ID2D1TransformMapper.Guid))
        {
            this.referenceCount++;

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        // ID2D1TransformMapperInternal
        if (riid->Equals(ID2D1TransformMapperInternal.Guid))
        {
            this.referenceCount++;

            *ppvObject = (void**)Unsafe.AsPointer(ref this) + 1;

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

        uint referenceCount;

        try
        {
            referenceCount = (uint)--this.referenceCount;

            // There are two special cases to consider here:
            //   - If the reference count is 1, it means that the D2D1TransformMapper<T> object is the only
            //     thing keeping this CCW alive. In that case, we can reset the GC handle pointing back to it,
            //     so that it can be collected if nobody else is referencing it. This breaks the reference cycle
            //     between the two object, which would have otherwise caused them to remain alive forever.
            //   - If the reference count is 0, the object is simply being destroyed. This is moved outside of
            //     this try/finally block, as otherwise accessing the lock from the finally block would write to
            //     out of bounds memory (as the field is in native memory that has just been freed).
            if (referenceCount == 1)
            {
                this.transformMapperHandle.Target = null;
            }
        }
        finally
        {
            this.spinLock.Exit();
        }

        // Destroy the object if this was the last reference left (see notes above as to why this is here)
        if (referenceCount == 0)
        {
            this.transformMapperHandle.Free();

            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }
}