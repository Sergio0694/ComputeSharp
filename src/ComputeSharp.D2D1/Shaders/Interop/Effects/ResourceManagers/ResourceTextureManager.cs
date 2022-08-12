using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if NET6_0_OR_GREATER
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RuntimeHelpers = ComputeSharp.D2D1.NetStandard.System.Runtime.CompilerServices.RuntimeHelpers;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

/// <summary>
/// An implementation of the <c>ID2D1ResourceTextureManager</c> and <c>ID2D1ResourceTextureManagerInternal</c> interfaces.
/// </summary>
internal unsafe partial struct ResourceTextureManager
{
    /// <inheritdoc cref="QueryInterface"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int QueryInterfaceDelegate(ResourceTextureManager* @this, Guid* riid, void** ppvObject);

    /// <inheritdoc cref="AddRef"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint AddRefDelegate(ResourceTextureManager* @this);

    /// <inheritdoc cref="Release"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint ReleaseDelegate(ResourceTextureManager* @this);

    /// <summary>
    /// The shared vtable pointer for <see cref="ResourceTextureManager"/> instance, for <c>ID2D1ResourceTextureManager</c>.
    /// </summary>
    private static readonly void** VtblForID2D1ResourceTextureManager;

    /// <summary>
    /// The shared vtable pointer for <see cref="ResourceTextureManager"/> instance, for <c>ID2D1ResourceTextureManagerInternal</c>.
    /// </summary>
    private static readonly void** VtblForID2D1ResourceTextureManagerInternal;

    /// <summary>
    /// Initializes the shared state for <see cref="ResourceTextureManager"/>.
    /// </summary>
    static ResourceTextureManager()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(ResourceTextureManager), sizeof(void*) * 10);

        // ID2D1ResourceTextureManager
#if NET6_0_OR_GREATER
        lpVtbl[0] = (delegate* unmanaged<ResourceTextureManager*, Guid*, void**, int>)&ID2D1ResourceTextureManagerMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<ResourceTextureManager*, uint>)&ID2D1ResourceTextureManagerMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<ResourceTextureManager*, uint>)&ID2D1ResourceTextureManagerMethods.Release;
        lpVtbl[3] = (delegate* unmanaged<ResourceTextureManager*, Guid*, D2D1_RESOURCE_TEXTURE_PROPERTIES*, byte*, uint*, uint, int>)&ID2D1ResourceTextureManagerMethods.Initialize;
        lpVtbl[4] = (delegate* unmanaged<ResourceTextureManager*, uint*, uint*, uint*, uint, byte*, uint, int>)&ID2D1ResourceTextureManagerMethods.Update;
#else
        lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerMethods.QueryInterfaceWrapper);
        lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerMethods.AddRefWrapper);
        lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerMethods.ReleaseWrapper);
        lpVtbl[3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerMethods.InitializeWrapper);
        lpVtbl[4] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerMethods.UpdateWrapper);
#endif

        // ID2D1ResourceTextureManagerInternal
#if NET6_0_OR_GREATER
        lpVtbl[5 + 0] = (delegate* unmanaged<ResourceTextureManager*, Guid*, void**, int>)&ID2D1ResourceTextureManagerInternalMethods.QueryInterface;
        lpVtbl[5 + 1] = (delegate* unmanaged<ResourceTextureManager*, uint>)&ID2D1ResourceTextureManagerInternalMethods.AddRef;
        lpVtbl[5 + 2] = (delegate* unmanaged<ResourceTextureManager*, uint>)&ID2D1ResourceTextureManagerInternalMethods.Release;
        lpVtbl[5 + 3] = (delegate* unmanaged<ResourceTextureManager*, ID2D1EffectContext*, int>)&ID2D1ResourceTextureManagerInternalMethods.SetEffectContext;
        lpVtbl[5 + 4] = (delegate* unmanaged<ResourceTextureManager*, ID2D1ResourceTexture**, int>)&ID2D1ResourceTextureManagerInternalMethods.GetResourceTexture;
#else
        lpVtbl[5 + 0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerInternalMethods.QueryInterfaceWrapper);
        lpVtbl[5 + 1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerInternalMethods.AddRefWrapper);
        lpVtbl[5 + 2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerInternalMethods.ReleaseWrapper);
        lpVtbl[5 + 3] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerInternalMethods.SetEffectContextWrapper);
        lpVtbl[5 + 4] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerInternalMethods.GetResourceTextureWrapper);
#endif

        VtblForID2D1ResourceTextureManager = lpVtbl;
        VtblForID2D1ResourceTextureManagerInternal = &lpVtbl[5];
    }

    /// <summary>
    /// The vtable pointer for the current instance, for <c>ID2D1ResourceTextureManager</c>.
    /// </summary>
    private void** lpVtblForID2D1ResourceTextureManager;

    /// <summary>
    /// The vtable pointer for the current instance, for <c>ID2D1ResourceTextureManagerInternal</c>.
    /// </summary>
    private void** lpVtblForID2D1ResourceTextureManagerInternal;

    /// <summary>
    /// The current reference count for the object (from <c>IUnknown</c>).
    /// </summary>
    private volatile int referenceCount;

    /// <summary>
    /// The <see cref="ID2D1EffectContext"/> object currently wrapped by the current instance.
    /// </summary>
    private ID2D1EffectContext* d2D1EffectContext;

    /// <summary>
    /// The <see cref="ID2D1ResourceTexture"/> object, if one was created already.
    /// </summary>
    private ID2D1ResourceTexture* d2D1ResourceTexture;

    /// <summary>
    /// The <see cref="Guid"/> to identify the resource, if available.
    /// </summary>
    private Guid* resourceId;

    /// <summary>
    /// The <see cref="D2D1_RESOURCE_TEXTURE_PROPERTIES"/> for the resource texture.
    /// </summary>
    /// <remarks>
    /// If <see cref="D2D1_RESOURCE_TEXTURE_PROPERTIES.extents"/> is <see langword="null"/>, it has not been set.
    /// </remarks>
    private D2D1_RESOURCE_TEXTURE_PROPERTIES resourceTextureProperties;

    /// <summary>
    /// The texture data, if available.
    /// </summary>
    /// <remarks>
    /// Once the texture has been created, this will no longer be necessary.
    /// </remarks>
    private byte* data;

    /// <summary>
    /// An optional pointer to the stride to advance through the resource texture, according to dimension.
    /// </summary>
    private uint* strides;

    /// <summary>
    /// The size, in bytes, of the data.
    /// </summary>
    private uint dataSize;

    /// <summary>
    /// The factory method for <see cref="ResourceTextureManager"/> instances.
    /// </summary>
    /// <param name="resourceTextureManager">The resulting resource texture manager instance.</param>
    public static void Factory(ResourceTextureManager** resourceTextureManager)
    {
        ResourceTextureManager* @this = (ResourceTextureManager*)NativeMemory.Alloc((nuint)sizeof(ResourceTextureManager));

        *@this = default;

        @this->lpVtblForID2D1ResourceTextureManager = VtblForID2D1ResourceTextureManager;
        @this->lpVtblForID2D1ResourceTextureManagerInternal = VtblForID2D1ResourceTextureManagerInternal;
        @this->referenceCount = 1;
        @this->d2D1EffectContext = null;
        @this->d2D1ResourceTexture = null;
        @this->resourceId = null;
        @this->resourceTextureProperties = default;
        @this->data = null;
        @this->strides = null;
        @this->dataSize = 0;

        *resourceTextureManager = @this;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    private int QueryInterface(Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        // ID2D1ResourceTextureManager
        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(ID2D1ResourceTextureManager.Guid))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        // ID2D1ResourceTextureManagerInternal
        if (riid->Equals(ID2D1ResourceTextureManagerInternal.Guid))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = (void**)Unsafe.AsPointer(ref this) + 1;

            return S.S_OK;
        }

        *ppvObject = null;

        return E.E_NOINTERFACE;
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    private uint AddRef()
    {
        return (uint)Interlocked.Increment(ref this.referenceCount);
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    private uint Release()
    {
        uint referenceCount = (uint)Interlocked.Decrement(ref this.referenceCount);

        if (referenceCount == 0)
        {
            if (this.d2D1EffectContext is not null)
            {
                this.d2D1EffectContext->Release();
            }

            if (this.d2D1ResourceTexture is not null)
            {
                this.d2D1ResourceTexture->Release();
            }

            if (this.resourceId is not null)
            {
                NativeMemory.Free(this.resourceId);
            }

            if (this.resourceTextureProperties.extents is not null)
            {
                NativeMemory.Free(this.resourceTextureProperties.extents);
            }

            if (this.resourceTextureProperties.extendModes is not null)
            {
                NativeMemory.Free(this.resourceTextureProperties.extendModes);
            }

            if (this.data is not null)
            {
                NativeMemory.Free(this.data);
            }

            if (this.strides is not null)
            {
                NativeMemory.Free(this.strides);
            }
        }

        return referenceCount;
    }
}
