using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using RuntimeHelpers = ComputeSharp.NetStandard.RuntimeHelpers;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// An implementation of the <see cref="ID2D1RenderInfoUpdateContext"/> and <see cref="ID2D1RenderInfoUpdateContextInternal"/> interfaces.
/// </summary>
internal unsafe partial struct D2D1RenderInfoUpdateContextImpl
{
#if !NET6_0_OR_GREATER
    /// <inheritdoc cref="QueryInterface"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int QueryInterfaceDelegate(D2D1RenderInfoUpdateContextImpl* @this, Guid* riid, void** ppvObject);

    /// <inheritdoc cref="AddRef"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint AddRefDelegate(D2D1RenderInfoUpdateContextImpl* @this);

    /// <inheritdoc cref="Release"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint ReleaseDelegate(D2D1RenderInfoUpdateContextImpl* @this);
#endif

    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1RenderInfoUpdateContextImpl"/> instance, for <see cref="ID2D1RenderInfoUpdateContext"/>.
    /// </summary>
    private static readonly void** VtblForID2D1RenderInfoUpdateContext = InitVtblForID2D1RenderInfoUpdateContextAndID2D1RenderInfoUpdateContextInternal();

    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1RenderInfoUpdateContextImpl"/> instance, for <see cref="ID2D1RenderInfoUpdateContextInternal"/>.
    /// </summary>
    private static readonly void** VtblForID2D1RenderInfoUpdateContextInternal = &VtblForID2D1RenderInfoUpdateContext[5];

    /// <summary>
    /// Initializes the combined vtable for <see cref="ID2D1RenderInfoUpdateContext"/> and <see cref="ID2D1RenderInfoUpdateContextInternal"/>.
    /// </summary>
    /// <returns>The combined vtable for <see cref="ID2D1RenderInfoUpdateContext"/> and <see cref="ID2D1RenderInfoUpdateContextInternal"/>.</returns>
    private static void** InitVtblForID2D1RenderInfoUpdateContextAndID2D1RenderInfoUpdateContextInternal()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(D2D1RenderInfoUpdateContextImpl), sizeof(void*) * 10);

        // ID2D1ResourceTextureManager
#if NET6_0_OR_GREATER
        lpVtbl[0] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, Guid*, void**, int>)&ID2D1RenderInfoUpdateContextMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, uint>)&ID2D1RenderInfoUpdateContextMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, uint>)&ID2D1RenderInfoUpdateContextMethods.Release;
        lpVtbl[3] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, uint*, int>)&ID2D1RenderInfoUpdateContextMethods.GetConstantBufferSize;
        lpVtbl[4] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, byte*, uint, int>)&ID2D1RenderInfoUpdateContextMethods.GetConstantBuffer;
        lpVtbl[5] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, byte*, uint, int>)&ID2D1RenderInfoUpdateContextMethods.SetConstantBuffer;
#else
        lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextMethods.QueryInterfaceWrapper);
        lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextMethods.AddRefWrapper);
        lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextMethods.ReleaseWrapper);
        lpVtbl[3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextMethods.GetConstantBufferSizeWrapper);
        lpVtbl[4] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextMethods.GetConstantBufferWrapper);
        lpVtbl[5] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextMethods.SetConstantBufferWrapper);
#endif

        // ID2D1ResourceTextureManagerInternal
#if NET6_0_OR_GREATER
        lpVtbl[6 + 0] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, Guid*, void**, int>)&ID2D1RenderInfoUpdateContextInternalMethods.QueryInterface;
        lpVtbl[6 + 1] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, uint>)&ID2D1RenderInfoUpdateContextInternalMethods.AddRef;
        lpVtbl[6 + 2] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, uint>)&ID2D1RenderInfoUpdateContextInternalMethods.Release;
        lpVtbl[6 + 3] = (delegate* unmanaged<D2D1RenderInfoUpdateContextImpl*, int>)&ID2D1RenderInfoUpdateContextInternalMethods.Close;
#else
        lpVtbl[6 + 0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextInternalMethods.QueryInterfaceWrapper);
        lpVtbl[6 + 1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextInternalMethods.AddRefWrapper);
        lpVtbl[6 + 2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextInternalMethods.ReleaseWrapper);
        lpVtbl[6 + 3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1RenderInfoUpdateContextInternalMethods.CloseWrapper);
#endif

        return lpVtbl;
    }

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1RenderInfoUpdateContext"/>.
    /// </summary>
    private void** lpVtblForID2D1RenderInfoUpdateContext;

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1RenderInfoUpdateContextInternal"/>.
    /// </summary>
    private void** lpVtblForID2D1RenderInfoUpdateContextInternal;

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
    /// The factory method for <see cref="D2D1RenderInfoUpdateContextImpl"/> instances.
    /// </summary>
    /// <param name="drawInfoUpdateContext">The resulting draw info update context instance.</param>
    /// <param name="constantBuffer">The constant buffer data, if set.</param>
    /// <param name="constantBufferSize">The size of <paramref name="constantBuffer"/>.</param>
    /// <param name="d2D1DrawInfo">The <see cref="ID2D1DrawInfo"/> instance currently in use.</param>
    /// <returns>This always returns <c>0</c>.</returns>
    public static HRESULT Factory(
        D2D1RenderInfoUpdateContextImpl** drawInfoUpdateContext,
        byte* constantBuffer,
        int constantBufferSize,
        ID2D1DrawInfo* d2D1DrawInfo)
    {
        D2D1RenderInfoUpdateContextImpl* @this;

        try
        {
            @this = (D2D1RenderInfoUpdateContextImpl*)NativeMemory.Alloc((nuint)sizeof(D2D1RenderInfoUpdateContextImpl));
        }
        catch (OutOfMemoryException)
        {
            *drawInfoUpdateContext = null;

            return E.E_OUTOFMEMORY;
        }

        @this->lpVtblForID2D1RenderInfoUpdateContext = VtblForID2D1RenderInfoUpdateContext;
        @this->lpVtblForID2D1RenderInfoUpdateContextInternal = VtblForID2D1RenderInfoUpdateContextInternal;
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

        // ID2D1RenderInfoUpdateContext
        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(ID2D1RenderInfoUpdateContext.Guid))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        // ID2D1RenderInfoUpdateContextInternal
        if (riid->Equals(ID2D1RenderInfoUpdateContextInternal.Guid))
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