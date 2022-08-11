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
        /// <inheritdoc cref="Initialize"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int InitializeDelegate(ResourceTextureManager* @this, ID2D1EffectContext* effectContext);

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
        /// A cached <see cref="InitializeDelegate"/> instance wrapping <see cref="Initialize"/>.
        /// </summary>
        public static readonly InitializeDelegate InitializeWrapper = Initialize;

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
        /// Initializes the current <c>ID2D1ResourceTextureManagerInternal</c> instance.
        /// </summary>
        /// <param name="this">The current <c>ID2D1ResourceTextureManagerInternal</c> instance.</param>
        /// <param name="effectContext">The input <see cref="ID2D1EffectContext"/> for the manager.</param>
        /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
        [UnmanagedCallersOnly]
        private static int Initialize(ResourceTextureManager* @this, ID2D1EffectContext* effectContext)
        {
            return 0;
        }

        /// <summary>
        /// Gets the <see cref="ID2D1ResourceTexture"/> instance held by the manager.
        /// </summary>
        /// <param name="this">The current <c>ID2D1ResourceTextureManagerInternal</c> instance.</param>
        /// <param name="resourceTexture">The resulting <see cref="ID2D1ResourceTexture"/> instance.</param>
        /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
        [UnmanagedCallersOnly]
        private static int GetResourceTexture(ResourceTextureManager* @this, ID2D1ResourceTexture** resourceTexture)
        {
            return 0;
        }
    }
}
