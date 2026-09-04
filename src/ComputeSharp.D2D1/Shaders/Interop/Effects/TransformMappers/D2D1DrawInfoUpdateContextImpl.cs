using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// An implementation of the <see cref="ID2D1DrawInfoUpdateContext"/> and <see cref="ID2D1DrawInfoUpdateContextInternal"/> interfaces.
/// </summary>
internal unsafe partial struct D2D1DrawInfoUpdateContextImpl
{
    /// <summary>
    /// The shared vtable for <see cref="D2D1DrawInfoUpdateContextImpl"/> instances, for <see cref="ID2D1DrawInfoUpdateContext"/>.
    /// </summary>
    [FixedAddressValueType]
    private static readonly ID2D1DrawInfoUpdateContextVftbl VftblForID2D1DrawInfoUpdateContext;

    /// <summary>
    /// The shared vtable for <see cref="D2D1DrawInfoUpdateContextImpl"/> instances, for <see cref="ID2D1DrawInfoUpdateContextInternal"/>.
    /// </summary>
    [FixedAddressValueType]
    private static readonly ID2D1DrawInfoUpdateContextInternalVftbl VftblForID2D1DrawInfoUpdateContextInternal;

    /// <summary>
    /// Initializes <see cref="VftblForID2D1DrawInfoUpdateContext"/> and <see cref="VftblForID2D1DrawInfoUpdateContextInternal"/>.
    /// </summary>
    static D2D1DrawInfoUpdateContextImpl()
    {
        VftblForID2D1DrawInfoUpdateContext.QueryInterface = &ID2D1DrawInfoUpdateContextMethods.QueryInterface;
        VftblForID2D1DrawInfoUpdateContext.AddRef = &ID2D1DrawInfoUpdateContextMethods.AddRef;
        VftblForID2D1DrawInfoUpdateContext.Release = &ID2D1DrawInfoUpdateContextMethods.Release;
        VftblForID2D1DrawInfoUpdateContext.GetConstantBufferSize = &ID2D1DrawInfoUpdateContextMethods.GetConstantBufferSize;
        VftblForID2D1DrawInfoUpdateContext.GetConstantBuffer = &ID2D1DrawInfoUpdateContextMethods.GetConstantBuffer;
        VftblForID2D1DrawInfoUpdateContext.SetConstantBuffer = &ID2D1DrawInfoUpdateContextMethods.SetConstantBuffer;

        VftblForID2D1DrawInfoUpdateContextInternal.QueryInterface = &ID2D1DrawInfoUpdateContextInternalMethods.QueryInterface;
        VftblForID2D1DrawInfoUpdateContextInternal.AddRef = &ID2D1DrawInfoUpdateContextInternalMethods.AddRef;
        VftblForID2D1DrawInfoUpdateContextInternal.Release = &ID2D1DrawInfoUpdateContextInternalMethods.Release;
        VftblForID2D1DrawInfoUpdateContextInternal.Close = &ID2D1DrawInfoUpdateContextInternalMethods.Close;
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
    private ComPtr<ID2D1DrawInfo> d2D1DrawInfo;

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

        @this->lpVtblForID2D1DrawInfoUpdateContext = (void**)Unsafe.AsPointer(in VftblForID2D1DrawInfoUpdateContext);
        @this->lpVtblForID2D1DrawInfoUpdateContextInternal = (void**)Unsafe.AsPointer(in VftblForID2D1DrawInfoUpdateContextInternal);
        @this->referenceCount = 1;
        @this->constantBuffer = constantBuffer;
        @this->constantBufferSize = constantBufferSize;

        // This ID2D1DrawInfo manager instance is short lived, and the contract guarantees that it will not remain
        // alive after the underlying ID2D1DrawInfo instance has been invalidated. As such, we can optimize the
        // initialization by skipping the AddRef/Release calls on the ID2D1DrawInfo object, and simply storing the
        // pointer to the object. When this instance is closed, that will just be set to null again.
        @this->d2D1DrawInfo = default;
        @this->d2D1DrawInfo.Attach(d2D1DrawInfo);

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
            riid->Equals(Windows.__uuidof<ID2D1DrawInfoUpdateContext>()))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        // ID2D1DrawInfoUpdateContextInternal
        if (riid->Equals(Windows.__uuidof<ID2D1DrawInfoUpdateContextInternal>()))
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