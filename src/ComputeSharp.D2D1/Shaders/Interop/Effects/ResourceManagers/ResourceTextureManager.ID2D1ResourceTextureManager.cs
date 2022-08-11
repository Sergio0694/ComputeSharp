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
    /// The implementation for <c>ID2D1ResourceTextureManager</c>.
    /// </summary>
    private static unsafe class ID2D1ResourceTextureManagerMethods
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="CreateResourceTexture"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int CreateResourceTextureDelegate(
            ResourceTextureManager* @this,
            Guid* resourceId,
            D2D1_RESOURCE_TEXTURE_PROPERTIES* resourceTextureProperties,
            byte* data,
            uint* strides,
            uint dataSize);

        /// <inheritdoc cref="Update"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int UpdateDelegate(
            ResourceTextureManager* @this,
            uint* minimumExtents,
            uint* maximumExtents,
            uint* strides,
            uint dimensions,
            byte* data,
            uint dataCount);

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
        /// A cached <see cref="CreateResourceTextureDelegate"/> instance wrapping <see cref="CreateResourceTexture"/>.
        /// </summary>
        public static readonly CreateResourceTextureDelegate CreateResourceTextureWrapper = CreateResourceTexture;

        /// <summary>
        /// A cached <see cref="UpdateDelegate"/> instance wrapping <see cref="Update"/>.
        /// </summary>
        public static readonly UpdateDelegate UpdateWrapper = Update;
#endif

        /// <inheritdoc cref="ResourceTextureManager.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(ResourceTextureManager* @this, Guid* riid, void** ppvObject)
        {
            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="ResourceTextureManager.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(ResourceTextureManager* @this)
        {
            return @this->AddRef();
        }

        /// <inheritdoc cref="ResourceTextureManager.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(ResourceTextureManager* @this)
        {
            return @this->Release();
        }

        /// <summary>
        /// Creates or finds the given resource texture, depending on whether a resource id is specified.
        /// It also optionally initializes the texture with the specified data.
        /// </summary>
        /// <param name="this">The current <c>ID2D1ResourceTextureManager</c> instance.</param>
        /// <param name="resourceId">An optional pointer to the unique id that identifies the resource texture.</param>
        /// <param name="resourceTextureProperties">The properties used to create the resource texture.</param>
        /// <param name="data">The optional data to be loaded into the resource texture.</param>
        /// <param name="strides">An optional pointer to the stride to advance through the resource texture, according to dimension.</param>
        /// <param name="dataSize">The size, in bytes, of the data.</param>
        /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
        [UnmanagedCallersOnly]
        public static int CreateResourceTexture(
            ResourceTextureManager* @this,
            Guid* resourceId,
            D2D1_RESOURCE_TEXTURE_PROPERTIES* resourceTextureProperties,
            byte* data,
            uint* strides,
            uint dataSize)
        {
            // Return E_POINTER if any input pointer is null
            if (resourceTextureProperties is null ||
                data is null ||
                strides is null ||
                resourceTextureProperties->extents is null ||
                resourceTextureProperties->extendModes is null)
            {
                return E.E_POINTER;
            }

            // If the method has already been called, fail
            if (@this->d2D1ResourceTexture is not null ||
                @this->data is not null)
            {
                return E.E_NOT_VALID_STATE;
            }

            // If Initialize has already been called, just forward the call
            if (@this->d2D1EffectContext is not null)
            {
                return @this->d2D1EffectContext->CreateResourceTexture(
                    resourceId: resourceId,
                    resourceTextureProperties: resourceTextureProperties,
                    data: data,
                    strides: strides,
                    dataSize: dataSize,
                    resourceTexture: &@this->d2D1ResourceTexture);
            }

            @this->resourceId = (Guid*)NativeMemory.Alloc((nuint)sizeof(Guid));

            *@this->resourceId = *resourceId;

            uint* extents = (uint*)NativeMemory.Alloc(sizeof(uint) * resourceTextureProperties->dimensions);
            D2D1_EXTEND_MODE* extendModes = (D2D1_EXTEND_MODE*)NativeMemory.Alloc(sizeof(D2D1_EXTEND_MODE) * resourceTextureProperties->dimensions);

            Buffer.MemoryCopy(
                source: resourceTextureProperties->extents,
                destination: extents,
                destinationSizeInBytes: sizeof(uint) * resourceTextureProperties->dimensions,
                sourceBytesToCopy: sizeof(uint) * resourceTextureProperties->dimensions);

            Buffer.MemoryCopy(
                source: resourceTextureProperties->extendModes,
                destination: extendModes,
                destinationSizeInBytes: sizeof(D2D1_EXTEND_MODE) * resourceTextureProperties->dimensions,
                sourceBytesToCopy: sizeof(D2D1_EXTEND_MODE) * resourceTextureProperties->dimensions);

            @this->resourceTextureProperties.extents = extents;
            @this->resourceTextureProperties.dimensions = resourceTextureProperties->dimensions;
            @this->resourceTextureProperties.bufferPrecision = resourceTextureProperties->bufferPrecision;
            @this->resourceTextureProperties.channelDepth = resourceTextureProperties->channelDepth;
            @this->resourceTextureProperties.filter = resourceTextureProperties->filter;
            @this->resourceTextureProperties.extendModes = extendModes;

            @this->data = (byte*)NativeMemory.Alloc(dataSize);

            Buffer.MemoryCopy(data, @this->data, dataSize, dataSize);

            @this->strides = (uint*)NativeMemory.Alloc(sizeof(uint) * resourceTextureProperties->dimensions);

            Buffer.MemoryCopy(
                source: strides,
                destination: @this->strides,
                destinationSizeInBytes: sizeof(uint) * resourceTextureProperties->dimensions,
                sourceBytesToCopy: sizeof(uint) * resourceTextureProperties->dimensions);

            return 0;
        }

        /// <summary>
        /// Updates the specific resource texture inside the specific range or box using the supplied data.
        /// </summary>
        /// <param name="this">The current <c>ID2D1ResourceTextureManager</c> instance.</param>
        /// <param name="minimumExtents">The "left" extent of the updates if specified. If <see langword="null"/>, the entire texture is updated.</param>
        /// <param name="maximumExtents">The "right" extent of the updates if specified. If <see langword="null"/>, the entire texture is updated.</param>
        /// <param name="strides">The stride to advance through the input data, according to dimension.</param>
        /// <param name="dimensions">The number of dimensions in the resource texture. This must match the number used to load the texture.</param>
        /// <param name="data">The data to be placed into the resource texture.</param>
        /// <param name="dataCount">The size of the data buffer to be used to update the resource texture.</param>
        /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
        [UnmanagedCallersOnly]
        public static int Update(
            ResourceTextureManager* @this,
            uint* minimumExtents,
            uint* maximumExtents,
            uint* strides,
            uint dimensions,
            byte* data,
            uint dataCount)
        {
            // If a resource texture already exists, just forward the call
            if (@this->d2D1ResourceTexture is not null)
            {
                return @this->d2D1ResourceTexture->Update(
                    minimumExtents: minimumExtents,
                    maximimumExtents: maximumExtents,
                    strides: strides,
                    dimensions: dimensions,
                    data: data,
                    dataCount: dataCount);
            }

            // TODO: implement Update over buffered data

            return 0;
        }
    }
}
