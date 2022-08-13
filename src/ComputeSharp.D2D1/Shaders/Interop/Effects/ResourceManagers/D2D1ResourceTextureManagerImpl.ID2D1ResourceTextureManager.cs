using System;
using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

/// <inheritdoc/>
unsafe partial struct D2D1ResourceTextureManagerImpl
{
    /// <inheritdoc cref="ID2D1ResourceTextureManager.Initialize"/>
    public static int Initialize(
        D2D1ResourceTextureManagerImpl* @this,
        Guid* resourceId,
        D2D1_RESOURCE_TEXTURE_PROPERTIES* resourceTextureProperties,
        byte* data,
        uint* strides,
        uint dataSize)
    {
        lock (@this->lockHandle.Target!)
        {
            // If the method has already been called, fail
            if (@this->d2D1ResourceTexture is not null ||
                @this->data is not null)
            {
                return E.E_NOT_VALID_STATE;
            }

            // If the method has already been called, just forward the call
            if (@this->d2D1EffectContext is not null)
            {
                @this->d2D1Multithread->Enter();

                // Create the resource after taking a D2D lock
                int hresult = @this->d2D1EffectContext->CreateResourceTexture(
                    resourceId: resourceId,
                    resourceTextureProperties: resourceTextureProperties,
                    data: data,
                    strides: strides,
                    dataSize: dataSize,
                    resourceTexture: &@this->d2D1ResourceTexture);

                @this->d2D1Multithread->Leave();

                return hresult;
            }

            // Initialize into a staging buffer
            return ID2D1ResourceTextureManagerMethods.InitializeWithStagingBuffer(
                @this: @this,
                resourceId: resourceId,
                resourceTextureProperties: resourceTextureProperties,
                data: data,
                strides: strides,
                dataSize: dataSize);
        }
    }

    /// <inheritdoc cref="ID2D1ResourceTextureManager.Update"/>
    public static int Update(
        D2D1ResourceTextureManagerImpl* @this,
        uint* minimumExtents,
        uint* maximimumExtents,
        uint* strides,
        uint dimensions,
        byte* data,
        uint dataCount)
    {
        lock (@this->lockHandle.Target!)
        {
            if (@this->d2D1ResourceTexture is null &&
                @this->data is null)
            {
                return E.E_NOT_VALID_STATE;
            }

            // If a resource texture already exists, just forward the call
            if (@this->d2D1ResourceTexture is not null)
            {
                @this->d2D1Multithread->Enter();

                // Take a D2D lock here too to ensure thread safety
                int hresult = @this->d2D1ResourceTexture->Update(
                    minimumExtents: minimumExtents,
                    maximimumExtents: maximimumExtents,
                    strides: strides,
                    dimensions: dimensions,
                    data: data,
                    dataCount: dataCount);

                @this->d2D1Multithread->Leave();

                return hresult;
            }

            // Otherwise update the staging buffer
            return ID2D1ResourceTextureManagerMethods.UpdateWithStagingBuffer(
                @this: @this,
                minimumExtents: minimumExtents,
                maximimumExtents: maximimumExtents,
                strides: strides,
                dimensions: dimensions,
                data: data,
                dataCount: dataCount);
        }
    }

    /// <summary>
    /// The implementation for <c>ID2D1ResourceTextureManager</c>.
    /// </summary>
    private static class ID2D1ResourceTextureManagerMethods
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="Initialize"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int InitializeDelegate(
            D2D1ResourceTextureManagerImpl* @this,
            Guid* resourceId,
            D2D1_RESOURCE_TEXTURE_PROPERTIES* resourceTextureProperties,
            byte* data,
            uint* strides,
            uint dataSize);

        /// <inheritdoc cref="Update"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int UpdateDelegate(
            D2D1ResourceTextureManagerImpl* @this,
            uint* minimumExtents,
            uint* maximimumExtents,
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
        /// A cached <see cref="InitializeDelegate"/> instance wrapping <see cref="Initialize"/>.
        /// </summary>
        public static readonly InitializeDelegate InitializeWrapper = Initialize;

        /// <summary>
        /// A cached <see cref="UpdateDelegate"/> instance wrapping <see cref="Update"/>.
        /// </summary>
        public static readonly UpdateDelegate UpdateWrapper = Update;
#endif

        /// <inheritdoc cref="D2D1ResourceTextureManagerImpl.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(D2D1ResourceTextureManagerImpl* @this, Guid* riid, void** ppvObject)
        {
            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="D2D1ResourceTextureManagerImpl.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(D2D1ResourceTextureManagerImpl* @this)
        {
            return @this->AddRef();
        }

        /// <inheritdoc cref="D2D1ResourceTextureManagerImpl.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(D2D1ResourceTextureManagerImpl* @this)
        {
            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1ResourceTextureManager.Initialize"/>
        [UnmanagedCallersOnly]
        public static int Initialize(
            D2D1ResourceTextureManagerImpl* @this,
            Guid* resourceId,
            D2D1_RESOURCE_TEXTURE_PROPERTIES* resourceTextureProperties,
            byte* data,
            uint* strides,
            uint dataSize)
        {
            return D2D1ResourceTextureManagerImpl.Initialize(
                @this,
                resourceId,
                resourceTextureProperties,
                data,
                strides,
                dataSize);
        }

        /// <inheritdoc cref="ID2D1ResourceTextureManager.Update"/>
        [UnmanagedCallersOnly]
        public static int Update(
            D2D1ResourceTextureManagerImpl* @this,
            uint* minimumExtents,
            uint* maximimumExtents,
            uint* strides,
            uint dimensions,
            byte* data,
            uint dataCount)
        {
            return D2D1ResourceTextureManagerImpl.Update(
                @this,
                minimumExtents,
                maximimumExtents,
                strides,
                dimensions,
                data,
                dataCount);
        }

        /// <inheritdoc cref="Initialize"/>
        public static int InitializeWithStagingBuffer(
            D2D1ResourceTextureManagerImpl* @this,
            Guid* resourceId,
            D2D1_RESOURCE_TEXTURE_PROPERTIES* resourceTextureProperties,
            byte* data,
            uint* strides,
            uint dataSize)
        {
            // Return E_POINTER if any input pointer is null. In particular, the data pointer
            // must not be null if the data size is not 0, and the strides pointer must not be
            // null if some data is supplied, and the number of dimensions is greater than 1.
            if (resourceTextureProperties is null ||
                (dataSize > 0 && data is null) ||
                resourceTextureProperties->extents is null ||
                resourceTextureProperties->extendModes is null ||
                (resourceTextureProperties->dimensions > 1 && dataSize > 0 && strides is null))
            {
                return E.E_POINTER;
            }

            // Textures can only have between 1 and 3 dimensions
            if (resourceTextureProperties->dimensions is 0 or > 3)
            {
                return E.E_INVALIDARG;
            }

            // No extent can be 0
            foreach (uint extent in new ReadOnlySpan<uint>(resourceTextureProperties->extents, (int)resourceTextureProperties->dimensions))
            {
                if (extent == 0)
                {
                    return E.E_INVALIDARG;
                }
            }

            // The buffer precision can't be unknown, and the channel depth must be explicit
            if (resourceTextureProperties->bufferPrecision == D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_UNKNOWN ||
                resourceTextureProperties->channelDepth == D2D1_CHANNEL_DEPTH.D2D1_CHANNEL_DEPTH_DEFAULT)
            {
                return E.E_INVALIDARG;
            }

            // Validate the data size
            if (dataSize > 0)
            {
                uint channelSizeInBytes = resourceTextureProperties->bufferPrecision switch
                {
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_8BPC_UNORM => 1,
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_8BPC_UNORM_SRGB => 1,
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_16BPC_UNORM => 2,
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_16BPC_FLOAT => 2,
                    D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_32BPC_FLOAT => 4,
                    _ => 0
                };

                uint elementSizeInBytes = resourceTextureProperties->channelDepth switch
                {
                    D2D1_CHANNEL_DEPTH.D2D1_CHANNEL_DEPTH_1 => channelSizeInBytes,
                    D2D1_CHANNEL_DEPTH.D2D1_CHANNEL_DEPTH_4 => channelSizeInBytes * 4,
                    _ => 0
                };

                if (elementSizeInBytes == 0)
                {
                    return E.E_INVALIDARG;
                }

                uint extent0SizeInBytes;

                try
                {
                    extent0SizeInBytes = checked(resourceTextureProperties->extents[0] * elementSizeInBytes);
                }
                catch (OverflowException)
                {
                    return E.E_INVALIDARG;
                }

                // For 1D textures, the row byte size must match the data size (no strides to consider)
                if (resourceTextureProperties->dimensions == 1)
                {
                    if (extent0SizeInBytes != dataSize)
                    {
                        return E.E_NOT_SUFFICIENT_BUFFER;
                    }                    
                }
                else
                {
                    // Validate the 0th stride is not smaller than the byte size of each row
                    if (extent0SizeInBytes > strides[0])
                    {
                        return E.E_INVALIDARG;
                    }

                    uint extent01SizeInBytes;

                    try
                    {
                        extent01SizeInBytes = checked((resourceTextureProperties->extents[1] - 1) * strides[0] + extent0SizeInBytes);
                    }
                    catch (OverflowException)
                    {
                        return E.E_INVALIDARG;
                    }

                    // For 2D textures, the size must match the rows * stride, plus the byte size of the last row (no trailing padding)
                    if (resourceTextureProperties->dimensions == 2)
                    {
                        if (extent01SizeInBytes != dataSize)
                        {
                            return E.E_NOT_SUFFICIENT_BUFFER;
                        }
                    }
                    else
                    {
                        uint stridedExtent01SizeInBytes;

                        try
                        {
                            stridedExtent01SizeInBytes = checked(resourceTextureProperties->extents[1] * strides[1]);
                        }
                        catch (OverflowException)
                        {
                            return E.E_INVALIDARG;
                        }

                        // Validate the 1th stride is not smaller than the byte size of a 2D slice, with padding
                        if (stridedExtent01SizeInBytes > strides[2])
                        {
                            return E.E_INVALIDARG;
                        }

                        uint extent012SizeInBytes;

                        try
                        {
                            extent012SizeInBytes = (resourceTextureProperties->extents[2] - 1) * strides[1] + extent01SizeInBytes;
                        }
                        catch (OverflowException)
                        {
                            return E.E_INVALIDARG;
                        }

                        if (extent012SizeInBytes != dataSize)
                        {
                            return E.E_NOT_SUFFICIENT_BUFFER;
                        }
                    }
                }
            }

            // Allocate a buffer for the resource id, if there is one
            if (resourceId is not null)
            {
                try
                {
                    @this->resourceId = (Guid*)NativeMemory.Alloc((nuint)sizeof(Guid));
                }
                catch (OutOfMemoryException)
                {
                    return E.E_OUTOFMEMORY;
                }

                *@this->resourceId = *resourceId;
            }

            uint* extents;

            // Allocate a buffer for the extents (one per dimension)
            try
            {
                extents = (uint*)NativeMemory.Alloc(sizeof(uint) * resourceTextureProperties->dimensions);
            }
            catch (OutOfMemoryException)
            {
                return E.E_OUTOFMEMORY;
            }

            D2D1_EXTEND_MODE* extendModes;

            // Allocate a buffer for the extend modes (one per dimension)
            try
            {
                extendModes = (D2D1_EXTEND_MODE*)NativeMemory.Alloc(sizeof(D2D1_EXTEND_MODE) * resourceTextureProperties->dimensions);
            }
            catch (OutOfMemoryException)
            {
                return E.E_OUTOFMEMORY;
            }

            // Copy the extents into the buffer
            Buffer.MemoryCopy(
                source: resourceTextureProperties->extents,
                destination: extents,
                destinationSizeInBytes: sizeof(uint) * resourceTextureProperties->dimensions,
                sourceBytesToCopy: sizeof(uint) * resourceTextureProperties->dimensions);

            // Copy the extend modes into the buffer
            Buffer.MemoryCopy(
                source: resourceTextureProperties->extendModes,
                destination: extendModes,
                destinationSizeInBytes: sizeof(D2D1_EXTEND_MODE) * resourceTextureProperties->dimensions,
                sourceBytesToCopy: sizeof(D2D1_EXTEND_MODE) * resourceTextureProperties->dimensions);

            // Initialize the other resource texture properties
            @this->resourceTextureProperties.extents = extents;
            @this->resourceTextureProperties.dimensions = resourceTextureProperties->dimensions;
            @this->resourceTextureProperties.bufferPrecision = resourceTextureProperties->bufferPrecision;
            @this->resourceTextureProperties.channelDepth = resourceTextureProperties->channelDepth;
            @this->resourceTextureProperties.filter = resourceTextureProperties->filter;
            @this->resourceTextureProperties.extendModes = extendModes;

            // Allocate the data buffer (this is the one most likely to potentially throw)
            try
            {
                @this->data = (byte*)NativeMemory.Alloc(dataSize);
            }
            catch (OutOfMemoryException)
            {
                return E.E_OUTOFMEMORY;
            }

            // Copy the data buffer
            Buffer.MemoryCopy(data, @this->data, dataSize, dataSize);

            @this->dataSize = dataSize;

            // If there are strides (ie. the texture has dimension greater than 1), initialize them as well
            if (strides is not null)
            {
                try
                {
                    @this->strides = (uint*)NativeMemory.Alloc(sizeof(uint) * (resourceTextureProperties->dimensions - 1));
                }
                catch (OutOfMemoryException)
                {
                    return E.E_OUTOFMEMORY;
                }

                Buffer.MemoryCopy(
                    source: strides,
                    destination: @this->strides,
                    destinationSizeInBytes: sizeof(uint) * (resourceTextureProperties->dimensions - 1),
                    sourceBytesToCopy: sizeof(uint) * (resourceTextureProperties->dimensions - 1));
            }

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1ResourceTextureManager.Update"/>
        public static int UpdateWithStagingBuffer(
            D2D1ResourceTextureManagerImpl* @this,
            uint* minimumExtents,
            uint* maximimumExtents,
            uint* strides,
            uint dimensions,
            byte* data,
            uint dataCount)
        {
            return E.E_NOTIMPL;
        }
    }
}
