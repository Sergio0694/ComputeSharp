using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// An implementation of the <see cref="ID2D1DrawTransformMapper"/> interface wrapping <see cref="ID2D1DrawTransformMapperInterop"/>.
/// </summary>
internal unsafe partial struct D2D1DrawTransformMapperImpl
{
    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1DrawTransformMapperImpl"/> instance, for <see cref="ID2D1DrawTransformMapper"/>.
    /// </summary>
    private static readonly void** VtblForID2D1DrawTransformMapper = InitVtblForID2D1DrawTransformMapperAndID2D1DrawTransformMapperInternal();

    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1DrawTransformMapperImpl"/> instance, for <see cref="ID2D1DrawTransformMapperInternal"/>.
    /// </summary>
    private static readonly void** VtblForID2D1DrawTransformMapperInternal = &VtblForID2D1DrawTransformMapper[6];

    /// <summary>
    /// Initializes the combined vtable for <see cref="ID2D1DrawTransformMapper"/> and <see cref="ID2D1DrawTransformMapperInternal"/>.
    /// </summary>
    /// <returns>The combined vtable for <see cref="ID2D1DrawTransformMapper"/> and <see cref="ID2D1DrawTransformMapperInternal"/>.</returns>
    private static void** InitVtblForID2D1DrawTransformMapperAndID2D1DrawTransformMapperInternal()
    {
        void** lpVtbl = (void**)D2D1AssemblyAssociatedMemory.Allocate(sizeof(void*) * 10);

        // ID2D1ResourceTextureManager
        lpVtbl[0] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, Guid*, void**, int>)&ID2D1DrawTransformMapperMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint>)&ID2D1DrawTransformMapperMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint>)&ID2D1DrawTransformMapperMethods.Release;
        lpVtbl[3] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, ID2D1DrawInfoUpdateContext*, RECT*, RECT*, uint, RECT*, RECT*, int>)&ID2D1DrawTransformMapperMethods.MapInputRectsToOutputRect;
        lpVtbl[4] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, RECT*, RECT*, uint, int>)&ID2D1DrawTransformMapperMethods.MapOutputRectToInputRects;
        lpVtbl[5] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint, RECT, RECT*, int>)&ID2D1DrawTransformMapperMethods.MapInvalidRect;

        // ID2D1DrawTransformMapperInternal
        lpVtbl[6 + 0] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, Guid*, void**, int>)&ID2D1DrawTransformMapperInternalMethods.QueryInterface;
        lpVtbl[6 + 1] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint>)&ID2D1DrawTransformMapperInternalMethods.AddRef;
        lpVtbl[6 + 2] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint>)&ID2D1DrawTransformMapperInternalMethods.Release;
        lpVtbl[6 + 3] = (delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, void**, int>)&ID2D1DrawTransformMapperInternalMethods.GetManagedWrapperHandle;

        return lpVtbl;
    }

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1DrawTransformMapper"/>.
    /// </summary>
    private void** lpVtblForID2D1DrawTransformMapper;

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1DrawTransformMapperInternal"/>.
    /// </summary>
    private void** lpVtblForID2D1DrawTransformMapperInternal;

    /// <summary>
    /// The current reference count for the object (from <see cref="IUnknown"/>).
    /// </summary>
    private volatile int referenceCount;

    /// <summary>
    /// A <see cref="GCHandle"/> to the wrapped <see cref="ID2D1DrawTransformMapperInterop"/> instance.
    /// </summary>
    private GCHandle transformMapperHandle;

    /// <summary>
    /// The <see cref="System.Threading.SpinLock"/> instance to synchronize accesses to this instance.
    /// </summary>
    private SpinLock spinLock;

    /// <summary>
    /// The factory method for <see cref="D2D1DrawTransformMapperImpl"/> instances.
    /// </summary>
    /// <param name="transformMapper">The input <see cref="ID2D1DrawTransformMapperInterop"/> instance to wrap.</param>
    /// <param name="d2D1TransformMapperProxy">The resulting <see cref="D2D1DrawTransformMapperImpl"/> instance.</param>
    public static void Factory(
        ID2D1DrawTransformMapperInterop transformMapper,
        D2D1DrawTransformMapperImpl** d2D1TransformMapperProxy)
    {
        D2D1DrawTransformMapperImpl* @this = (D2D1DrawTransformMapperImpl*)NativeMemory.Alloc((nuint)sizeof(D2D1DrawTransformMapperImpl));

        @this->lpVtblForID2D1DrawTransformMapper = VtblForID2D1DrawTransformMapper;
        @this->lpVtblForID2D1DrawTransformMapperInternal = VtblForID2D1DrawTransformMapperInternal;
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
    /// <param name="target">The associated <see cref="ID2D1DrawTransformMapperInterop"/> object to track.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnsureTargetIsTracked(ID2D1DrawTransformMapperInterop target)
    {
        this.transformMapperHandle.Target = target;
    }

    /// <summary>
    /// Copies the current object onto a target pointer.
    /// </summary>
    /// <param name="transformMapper">The target <see cref="ID2D1DrawTransformMapper"/> pointer.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    /// <remarks>This method must only be called if the caller has taken a lock on <see cref="SpinLock"/> already.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyToWithNoLock(ID2D1DrawTransformMapper** transformMapper)
    {
        this.referenceCount++;

        *transformMapper = (ID2D1DrawTransformMapper*)Unsafe.AsPointer(ref this);
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

        // ID2D1DrawTransformMapper
        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(Windows.__uuidof<ID2D1DrawTransformMapper>()))
        {
            this.referenceCount++;

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        // ID2D1DrawTransformMapperInternal
        if (riid->Equals(Windows.__uuidof<ID2D1DrawTransformMapperInternal>()))
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