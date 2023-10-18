using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// An implementation of the <see cref="ID2D1DrawInfoUpdateContext"/> and <see cref="ID2D1DrawInfoUpdateContextInternal"/> interfaces.
/// </summary>
internal unsafe partial struct D2D1DrawInfoUpdateContextImpl
{
    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1DrawInfoUpdateContextImpl"/> instance, for <see cref="ID2D1DrawInfoUpdateContext"/>.
    /// </summary>
    private static readonly void** VtblForID2D1DrawInfoUpdateContext = InitVtblForID2D1DrawInfoUpdateContextAndID2D1DrawInfoUpdateContextInternal();

    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1DrawInfoUpdateContextImpl"/> instance, for <see cref="ID2D1DrawInfoUpdateContextInternal"/>.
    /// </summary>
    private static readonly void** VtblForID2D1DrawInfoUpdateContextInternal = &VtblForID2D1DrawInfoUpdateContext[5];

    /// <summary>
    /// Initializes the combined vtable for <see cref="ID2D1DrawInfoUpdateContext"/> and <see cref="ID2D1DrawInfoUpdateContextInternal"/>.
    /// </summary>
    /// <returns>The combined vtable for <see cref="ID2D1DrawInfoUpdateContext"/> and <see cref="ID2D1DrawInfoUpdateContextInternal"/>.</returns>
    private static void** InitVtblForID2D1DrawInfoUpdateContextAndID2D1DrawInfoUpdateContextInternal()
    {
        void** lpVtbl = (void**)D2D1AssemblyAssociatedMemory.Allocate(sizeof(void*) * 10);

        // ID2D1ResourceTextureManager
        lpVtbl[0] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, Guid*, void**, int>)&ID2D1DrawInfoUpdateContextMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, uint>)&ID2D1DrawInfoUpdateContextMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, uint>)&ID2D1DrawInfoUpdateContextMethods.Release;
        lpVtbl[3] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, uint*, int>)&ID2D1DrawInfoUpdateContextMethods.GetConstantBufferSize;
        lpVtbl[4] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, byte*, uint, int>)&ID2D1DrawInfoUpdateContextMethods.GetConstantBuffer;
        lpVtbl[5] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, byte*, uint, int>)&ID2D1DrawInfoUpdateContextMethods.SetConstantBuffer;

        // ID2D1ResourceTextureManagerInternal
        lpVtbl[6 + 0] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, Guid*, void**, int>)&ID2D1DrawInfoUpdateContextInternalMethods.QueryInterface;
        lpVtbl[6 + 1] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, uint>)&ID2D1DrawInfoUpdateContextInternalMethods.AddRef;
        lpVtbl[6 + 2] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, uint>)&ID2D1DrawInfoUpdateContextInternalMethods.Release;
        lpVtbl[6 + 3] = (delegate* unmanaged<D2D1DrawInfoUpdateContextImpl*, int>)&ID2D1DrawInfoUpdateContextInternalMethods.Close;

        return lpVtbl;
    }

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1DrawInfoUpdateContext"/>.
    /// </summary>
    private void** lpVtblForID2D1DrawInfoUpdateContext;

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1DrawInfoUpdateContextInternal"/>.
    /// </summary>
    private void** lpVtblForID2D1DrawInfoUpdateContextInternal;

    /// <summary>
    /// The current reference count for the object (from <see cref="IUnknown"/>).
    /// </summary>
    private volatile int referenceCount;

    /// <summary>
    /// The constant buffer data, if set.
    /// </summary>
    private byte* constantBuffer;

    /// <summary>
    /// The size of <see cref="constantBuffer"/>.
    /// </summary>
    private int constantBufferSize;

    /// <summary>
    /// The <see cref="ID2D1DrawInfo"/> instance currently in use.
    /// </summary>
    private ID2D1DrawInfo* d2D1DrawInfo;

    /// <summary>
    /// The factory method for <see cref="D2D1DrawInfoUpdateContextImpl"/> instances.
    /// </summary>
    /// <param name="drawInfoUpdateContext">The resulting draw info update context instance.</param>
    /// <param name="constantBuffer">The constant buffer data, if set.</param>
    /// <param name="constantBufferSize">The size of <paramref name="constantBuffer"/>.</param>
    /// <param name="d2D1DrawInfo">The <see cref="ID2D1DrawInfo"/> instance currently in use.</param>
    /// <returns>This always returns <c>0</c>.</returns>
    public static HRESULT Factory(
        D2D1DrawInfoUpdateContextImpl** drawInfoUpdateContext,
        byte* constantBuffer,
        int constantBufferSize,
        ID2D1DrawInfo* d2D1DrawInfo)
    {
        D2D1DrawInfoUpdateContextImpl* @this;

        try
        {
            @this = (D2D1DrawInfoUpdateContextImpl*)NativeMemory.Alloc((nuint)sizeof(D2D1DrawInfoUpdateContextImpl));
        }
        catch (OutOfMemoryException)
        {
            *drawInfoUpdateContext = null;

            return E.E_OUTOFMEMORY;
        }

        @this->lpVtblForID2D1DrawInfoUpdateContext = VtblForID2D1DrawInfoUpdateContext;
        @this->lpVtblForID2D1DrawInfoUpdateContextInternal = VtblForID2D1DrawInfoUpdateContextInternal;
        @this->referenceCount = 1;
        @this->constantBuffer = constantBuffer;
        @this->constantBufferSize = constantBufferSize;
        @this->d2D1DrawInfo = d2D1DrawInfo;

        *drawInfoUpdateContext = @this;

        return S.S_OK;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    public int QueryInterface(Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        // ID2D1DrawInfoUpdateContext
        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(ID2D1DrawInfoUpdateContext.Guid))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        // ID2D1DrawInfoUpdateContextInternal
        if (riid->Equals(ID2D1DrawInfoUpdateContextInternal.Guid))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = (void**)Unsafe.AsPointer(ref this) + 1;

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
            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }
}