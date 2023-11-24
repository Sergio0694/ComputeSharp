using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Extensions;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

/// <inheritdoc/>
partial struct D2D1ResourceTextureManagerImpl
{
    /// <summary>
    /// The implementation for <see cref="ID2D1ResourceTextureManagerInternal"/>.
    /// </summary>
    private static unsafe class ID2D1ResourceTextureManagerInternalMethods
    {
        /// <inheritdoc cref="D2D1ResourceTextureManagerImpl.QueryInterface"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int QueryInterface(D2D1ResourceTextureManagerImpl* @this, Guid* riid, void** ppvObject)
        {
            @this = (D2D1ResourceTextureManagerImpl*)&((void**)@this)[-1];

            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="D2D1ResourceTextureManagerImpl.AddRef"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static uint AddRef(D2D1ResourceTextureManagerImpl* @this)
        {
            @this = (D2D1ResourceTextureManagerImpl*)&((void**)@this)[-1];

            return @this->AddRef();
        }

        /// <inheritdoc cref="D2D1ResourceTextureManagerImpl.Release"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static uint Release(D2D1ResourceTextureManagerImpl* @this)
        {
            @this = (D2D1ResourceTextureManagerImpl*)&((void**)@this)[-1];

            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1ResourceTextureManagerInternal.Initialize"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int Initialize(D2D1ResourceTextureManagerImpl* @this, ID2D1EffectContext* effectContext, uint* dimensions)
        {
            @this = (D2D1ResourceTextureManagerImpl*)&((void**)@this)[-1];

            if (effectContext is null)
            {
                return E.E_POINTER;
            }

            lock (@this->lockHandle.Target!)
            {
                // If the dimensions are available, validate them
                if (dimensions is not null)
                {
                    uint expectedDimensions = *dimensions;

                    if (@this->resourceTextureProperties.extents is not null &&
                        @this->resourceTextureProperties.dimensions != expectedDimensions)
                    {
                        return E.E_INVALIDARG;
                    }

                    @this->expectedDimensions = expectedDimensions;
                }

                // If this is the first time the method is called, just store the context.
                // Before doing so, get an ID2D1Multithread instance, to ensure thread safety.
                // For additional info, see docs on ID2D1EffectFactoryExtensions.GetD2D1Multithread.
                if (@this->d2D1EffectContext.Get() is null)
                {
                    using ComPtr<ID2D1Multithread> d2D1Multithread = default;

                    HRESULT hresult = effectContext->GetD2D1Multithread(d2D1Multithread.GetAddressOf());

                    // If an ID2D1Multithread object is available, we can safely store the context. That
                    // is, under the condition that the required multithread support is also available.
                    if (Windows.SUCCEEDED(hresult))
                    {
                        if (@this->requiresMultithread > d2D1Multithread.Get()->GetMultithreadProtected())
                        {
                            return E.E_INVALIDARG;
                        }

                        // Now, the effect context can actually be stored safely while holding the lock.
                        // This is guaranteed to be the case here, as this method is only called (as per
                        // contact of the COM interface) from ID2D1EffectImpl, which holds the D2D lock.
                        // Also store the actual multithread object after storing the effect context.
                        @this->d2D1EffectContext.Attach(new ComPtr<ID2D1EffectContext>(effectContext).Get());
                        @this->d2D1Multithread.Attach(d2D1Multithread.Detach());
                    }

                    return hresult;
                }

                // Otherwise, just do nothing and return S_FALSE. This allows an existing resource texture
                // manager to be shared across effects. If the resource cannot be shared, it will just
                // return an error when effect is actually set, with ID2D1DrawInfo::SetResourceTexture.
                return S.S_FALSE;
            }
        }

        /// <inheritdoc cref="ID2D1ResourceTextureManagerInternal.GetResourceTexture"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int GetResourceTexture(D2D1ResourceTextureManagerImpl* @this, ID2D1ResourceTexture** resourceTexture)
        {
            @this = (D2D1ResourceTextureManagerImpl*)&((void**)@this)[-1];

            if (*resourceTexture is not null)
            {
                return E.E_POINTER;
            }

            lock (@this->lockHandle.Target!)
            {
                // If the effect context is null, it means Initialize has not been called yet
                if (@this->d2D1EffectContext.Get() is null)
                {
                    return E.E_NOT_VALID_STATE;
                }

                // If the texture has already been created, just return it
                if (@this->d2D1ResourceTexture.Get() is not null)
                {
                    @this->d2D1ResourceTexture.CopyTo(resourceTexture).Assert();

                    return S.S_OK;
                }

                // If the data is null at this point, it means CreateResourceTexture has not been called yet
                if (@this->data is null)
                {
                    return E.E_NOT_VALID_STATE;
                }

                using ComPtr<ID2D1ResourceTexture> d2D1ResourceTexture = default;

                // Create the resource now, as it hasn't been created yet
                int result = @this->d2D1EffectContext.Get()->CreateResourceTexture(
                    resourceId: @this->resourceId,
                    resourceTextureProperties: &@this->resourceTextureProperties,
                    data: @this->data,
                    strides: @this->strides,
                    dataSize: @this->dataSize,
                    resourceTexture: d2D1ResourceTexture.GetAddressOf());

                // If creation was successful, release the buffered data. Going forwards,
                // the resource texture will be used directly for all updates requested.
                if (result == S.S_OK)
                {
                    // Store the resource texture for later
                    _ = d2D1ResourceTexture.CopyTo(&@this->d2D1ResourceTexture);

                    // Also return it to callers
                    _ = d2D1ResourceTexture.CopyTo(resourceTexture);

                    // Free the staging buffers
                    NativeMemory.Free(@this->resourceTextureProperties.extents);
                    NativeMemory.Free(@this->resourceTextureProperties.extendModes);
                    NativeMemory.Free(@this->data);
                    NativeMemory.Free(@this->strides);

                    // Reset the stored pointers to avoid double frees from Release()
                    @this->resourceTextureProperties.extents = null;
                    @this->resourceTextureProperties.extendModes = null;
                    @this->data = null;
                    @this->strides = null;
                }

                return result;
            }
        }
    }
}