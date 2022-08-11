using System;
using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

/// <inheritdoc/>
partial struct ResourceTextureManager
{
    /// <summary>
    /// The implementation for <c>ID2D1ResourceTextureManagerInternal</c>.
    /// </summary>
    private unsafe static class ID2D1ResourceTextureManagerInternalMethods
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="SetEffectContext"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int SetEffectContextDelegate(ResourceTextureManager* @this, ID2D1EffectContext* effectContext);

        /// <inheritdoc cref="GetResourceTexture"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int GetResourceTextureDelegate(ResourceTextureManager* @this, ID2D1ResourceTexture** resourceTexture);

        /// <summary>
        /// A cached <see cref="QueryInterfaceDelegate"/> instance wrapping <see cref="QueryInterface"/>.
        /// </summary>
        public static readonly QueryInterfaceDelegate QueryInterfaceWrapper = QueryInterface;

        /// <summary>
        /// A cached <see cref="AddRefDelegate"/> instance wrapping <see cref="AddRef"/>.
        /// </summary>
        public static readonly AddRefDelegate AddRefWrapper = AddRef;

        /// <summary>
        /// A cached <see cref="ReleaseDelegate"/> instance wrapping <see cref="Release"/>.
        /// </summary>
        public static readonly ReleaseDelegate ReleaseWrapper = Release;

        /// <summary>
        /// A cached <see cref="SetEffectContextDelegate"/> instance wrapping <see cref="SetEffectContext"/>.
        /// </summary>
        public static readonly SetEffectContextDelegate SetEffectContextWrapper = SetEffectContext;

        /// <summary>
        /// A cached <see cref="GetResourceTextureDelegate"/> instance wrapping <see cref="GetResourceTexture"/>.
        /// </summary>
        public static readonly GetResourceTextureDelegate GetResourceTextureWrapper = GetResourceTexture;
#endif

        /// <inheritdoc cref="ResourceTextureManager.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(ResourceTextureManager* @this, Guid* riid, void** ppvObject)
        {
            @this = (ResourceTextureManager*)&((void**)@this)[-1];

            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="ResourceTextureManager.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(ResourceTextureManager* @this)
        {
            @this = (ResourceTextureManager*)&((void**)@this)[-1];

            return @this->AddRef();
        }

        /// <inheritdoc cref="ResourceTextureManager.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(ResourceTextureManager* @this)
        {
            @this = (ResourceTextureManager*)&((void**)@this)[-1];

            return @this->Release();
        }

        /// <summary>
        /// Sets the <see cref="ID2D1EffectContext"/> for the current <c>ID2D1ResourceTextureManagerInternal</c> instance.
        /// </summary>
        /// <param name="this">The current <c>ID2D1ResourceTextureManagerInternal</c> instance.</param>
        /// <param name="effectContext">The input <see cref="ID2D1EffectContext"/> for the manager.</param>
        /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
        [UnmanagedCallersOnly]
        public static int SetEffectContext(ResourceTextureManager* @this, ID2D1EffectContext* effectContext)
        {
            @this = (ResourceTextureManager*)&((void**)@this)[-1];

            if (effectContext is null)
            {
                return E.E_POINTER;
            }

            effectContext->AddRef();

            // If this is the first time the method is called, just store the context
            if (@this->d2D1EffectContext is null)
            {
                @this->d2D1EffectContext = effectContext;

                return S.S_OK;
            }

            // Otherwise, just do nothing and return S_FALSE. This allows an existing resource texture
            // manager to be shared across effects. If the resource cannot be shared, it will just
            // return an error when effect is actually set, with ID2D1DrawInfo::SetResourceTexture.
            return S.S_FALSE;
        }

        /// <summary>
        /// Gets the <see cref="ID2D1ResourceTexture"/> instance held by the manager.
        /// </summary>
        /// <param name="this">The current <c>ID2D1ResourceTextureManagerInternal</c> instance.</param>
        /// <param name="resourceTexture">The resulting <see cref="ID2D1ResourceTexture"/> instance.</param>
        /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
        [UnmanagedCallersOnly]
        public static int GetResourceTexture(ResourceTextureManager* @this, ID2D1ResourceTexture** resourceTexture)
        {
            @this = (ResourceTextureManager*)&((void**)@this)[-1];

            if (*resourceTexture is not null)
            {
                return E.E_POINTER;
            }

            // If the effect context is null, it means Initialize has not been called yet
            if (@this->d2D1EffectContext is null)
            {
                return E.E_NOT_VALID_STATE;
            }

            // If the texture has already been created, just return it
            if (@this->d2D1ResourceTexture is not null)
            {
                @this->d2D1ResourceTexture->AddRef();

                *resourceTexture = @this->d2D1ResourceTexture;
            }

            // If the data is null at this point, it means CreateResourceTexture has not been called yet
            if (@this->data is null)
            {
                return E.E_NOT_VALID_STATE;
            }

            // Create the resource now, as it hasn't been created yet
            int result = @this->d2D1EffectContext->CreateResourceTexture(
                resourceId: @this->resourceId,
                resourceTextureProperties: &@this->resourceTextureProperties,
                data: @this->data,
                strides: @this->strides,
                dataSize: @this->dataSize,
                resourceTexture: resourceTexture);

            // If creation was successful, release the buffered data
            if (result == S.S_OK)
            {
                NativeMemory.Free(@this->resourceTextureProperties.extents);
                NativeMemory.Free(@this->resourceTextureProperties.extendModes);
                NativeMemory.Free(@this->data);
                NativeMemory.Free(@this->strides);
            }

            return S.S_OK;
        }
    }
}
