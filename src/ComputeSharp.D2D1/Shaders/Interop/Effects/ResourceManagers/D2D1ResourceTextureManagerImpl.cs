using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

/// <summary>
/// An implementation of the <see cref="ID2D1ResourceTextureManager"/> and <see cref="ID2D1ResourceTextureManagerInternal"/> interfaces.
/// </summary>
internal unsafe partial struct D2D1ResourceTextureManagerImpl
{
    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1ResourceTextureManagerImpl"/> instance, for <see cref="ID2D1ResourceTextureManager"/>.
    /// </summary>
    private static readonly void** VtblForID2D1ResourceTextureManager = InitVtblForID2D1ResourceTextureManagerAndID2D1ResourceTextureManagerInternal();

    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1ResourceTextureManagerImpl"/> instance, for <see cref="ID2D1ResourceTextureManagerInternal"/>.
    /// </summary>
    private static readonly void** VtblForID2D1ResourceTextureManagerInternal = &VtblForID2D1ResourceTextureManager[5];

    /// <summary>
    /// Initializes the combined vtable for <see cref="ID2D1ResourceTextureManager"/> and <see cref="ID2D1ResourceTextureManagerInternal"/>.
    /// </summary>
    /// <returns>The combined vtable for <see cref="ID2D1ResourceTextureManager"/> and <see cref="ID2D1ResourceTextureManagerInternal"/>.</returns>
    private static void** InitVtblForID2D1ResourceTextureManagerAndID2D1ResourceTextureManagerInternal()
    {
        void** lpVtbl = (void**)D2D1AssemblyAssociatedMemory.Allocate(sizeof(void*) * 10);

        // ID2D1ResourceTextureManager
        lpVtbl[0] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, Guid*, void**, int>)&ID2D1ResourceTextureManagerMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, uint>)&ID2D1ResourceTextureManagerMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, uint>)&ID2D1ResourceTextureManagerMethods.Release;
        lpVtbl[3] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, Guid*, D2D1_RESOURCE_TEXTURE_PROPERTIES*, byte*, uint*, uint, int>)&ID2D1ResourceTextureManagerMethods.Initialize;
        lpVtbl[4] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, uint*, uint*, uint*, uint, byte*, uint, int>)&ID2D1ResourceTextureManagerMethods.Update;

        // ID2D1ResourceTextureManagerInternal
        lpVtbl[5 + 0] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, Guid*, void**, int>)&ID2D1ResourceTextureManagerInternalMethods.QueryInterface;
        lpVtbl[5 + 1] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, uint>)&ID2D1ResourceTextureManagerInternalMethods.AddRef;
        lpVtbl[5 + 2] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, uint>)&ID2D1ResourceTextureManagerInternalMethods.Release;
        lpVtbl[5 + 3] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, ID2D1EffectContext*, uint*, int>)&ID2D1ResourceTextureManagerInternalMethods.Initialize;
        lpVtbl[5 + 4] = (delegate* unmanaged<D2D1ResourceTextureManagerImpl*, ID2D1ResourceTexture**, int>)&ID2D1ResourceTextureManagerInternalMethods.GetResourceTexture;

        return lpVtbl;
    }

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1ResourceTextureManager"/>.
    /// </summary>
    private void** lpVtblForID2D1ResourceTextureManager;

    /// <summary>
    /// The vtable pointer for the current instance, for <see cref="ID2D1ResourceTextureManagerInternal"/>.
    /// </summary>
    private void** lpVtblForID2D1ResourceTextureManagerInternal;

    /// <summary>
    /// The current reference count for the object (from <see cref="IUnknown"/>).
    /// </summary>
    private volatile int referenceCount;

    /// <summary>
    /// Indicates whether the current instance requires multithread support to work safely.
    /// </summary>
    /// <remarks>
    /// This is used when the object is wrapped in a RCW with a finalizer, which could run at any time.
    /// In that case, multithread support is required, and using a single thread factory is an error.
    /// </remarks>
    private int requiresMultithread;

    /// <summary>
    /// The <see cref="ID2D1EffectContext"/> object currently wrapped by the current instance.
    /// </summary>
    private ID2D1EffectContext* d2D1EffectContext;

    /// <summary>
    /// The <see cref="ID2D1Multithread"/> instance used to lock accesses to <see cref="d2D1EffectContext"/> and <see cref="d2D1ResourceTexture"/>.
    /// </summary>
    private ID2D1Multithread* d2D1Multithread;

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
    /// A <see cref="GCHandle"/> to a dummy <see langword="object"/> to use for locking.
    /// </summary>
    private GCHandle lockHandle;

    /// <summary>
    /// The expected dimensions for the slot the manager has been assigned to in an effect.
    /// </summary>
    /// <remarks>If <c>0</c>, it has not been set, so it can be ignored.</remarks>
    private uint expectedDimensions;

    /// <summary>
    /// The factory method for <see cref="D2D1ResourceTextureManagerImpl"/> instances.
    /// </summary>
    /// <param name="resourceTextureManager">The resulting resource texture manager instance.</param>
    /// <param name="requiresMultithread">Indicates whether the current instance requires multithread support to work safely.</param>
    public static void Factory(D2D1ResourceTextureManagerImpl** resourceTextureManager, bool requiresMultithread)
    {
        D2D1ResourceTextureManagerImpl* @this = (D2D1ResourceTextureManagerImpl*)NativeMemory.Alloc((nuint)sizeof(D2D1ResourceTextureManagerImpl));

        *@this = default;

        @this->lpVtblForID2D1ResourceTextureManager = VtblForID2D1ResourceTextureManager;
        @this->lpVtblForID2D1ResourceTextureManagerInternal = VtblForID2D1ResourceTextureManagerInternal;
        @this->referenceCount = 1;
        @this->d2D1EffectContext = null;
        @this->d2D1Multithread = null;
        @this->d2D1ResourceTexture = null;
        @this->requiresMultithread = requiresMultithread ? 1 : 0;
        @this->resourceId = null;
        @this->resourceTextureProperties = default;
        @this->data = null;
        @this->strides = null;
        @this->dataSize = 0;
        @this->lockHandle = GCHandle.Alloc(new object());
        @this->expectedDimensions = 0;

        *resourceTextureManager = @this;
    }

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    public int QueryInterface(Guid* riid, void** ppvObject)
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
            // Note: if this object is being destroyed, it means that no other caller can exist. As such, there
            // is no need to take the instance lock, as it's impossible for this logic to be racing against other
            // threads referencing this object. If someone else was doing this, this would be falling apart anyway.
            // As such, we can simply free this GC handle and run the rest of the logic without the additional lock.
            this.lockHandle.Free();

            // Synchronize on the ID2D1Multithread instance before touching D2D objects
            if (this.d2D1Multithread is not null)
            {
                // Enter the lock, and then free the effect context. That is guaranteed to
                // not be null here, as it is only ever set if ID2D1Multithread is retrieved.
                this.d2D1Multithread->Enter();

                // Release the resource too if it has been created
                ComPtr.Dispose(this.d2D1ResourceTexture);

                // Now that the resource, if any, has been released, the effect context can also
                // be released. This must be done only after any associated resource textures have
                // been released. Releasing an effect context first, in case it was the last reference
                // to it, will otherwise cause the Release() call on the resource texture to explode.
                _ = this.d2D1EffectContext->Release();

                this.d2D1Multithread->Leave();

                _ = this.d2D1Multithread->Release();
            }

            NativeMemory.Free(this.resourceId);
            NativeMemory.Free(this.resourceTextureProperties.extents);
            NativeMemory.Free(this.resourceTextureProperties.extendModes);
            NativeMemory.Free(this.data);
            NativeMemory.Free(this.strides);

            NativeMemory.Free(Unsafe.AsPointer(ref this));
        }

        return referenceCount;
    }
}