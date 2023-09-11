using System;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Shaders.Interop.Buffers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Shaders.Interop.Helpers;

/// <inheritdoc/>
unsafe partial class D2D1ShaderEffect
{
    /// <summary>
    /// Gets the constant buffer from a given D2D shader effect implementation.
    /// </summary>
    /// <param name="data">A pointer to a variable that stores the data that this function retrieves on the property.</param>
    /// <param name="dataSize">The number of bytes in the property to retrieve.</param>
    /// <param name="actualSize">A optional pointer to a variable that stores the actual number of bytes retrieved on the property.</param>
    /// <param name="constantBufferSize">The size of <paramref name="constantBuffer"/>.</param>
    /// <param name="constantBuffer">The constant buffer data, if set.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static int GetConstantBuffer(
        byte* data,
        uint dataSize,
        uint* actualSize,
        uint constantBufferSize,
        byte* constantBuffer)
    {
        // This is a call likely from ID2D1Effect::GetValueSize, to query the property size
        if (data is null &&
            dataSize == 0 &&
            actualSize is not null)
        {
            *actualSize = constantBufferSize;

            return S.S_FALSE;
        }

        if (data is null)
        {
            return E.E_POINTER;
        }

        if (dataSize < constantBufferSize)
        {
            return E.E_NOT_SUFFICIENT_BUFFER;
        }

        if (constantBuffer is null)
        {
            return E.E_NOT_VALID_STATE;
        }

        uint writtenBytes = 0;

        if (constantBufferSize > 0)
        {
            Buffer.MemoryCopy(constantBuffer, data, dataSize, constantBufferSize);

            writtenBytes = constantBufferSize;
        }

        if (actualSize is not null)
        {
            *actualSize = writtenBytes;
        }

        return S.S_OK;
    }

    /// <summary>
    /// Sets the constant buffer from a given D2D shader effect implementation.
    /// </summary>
    /// <param name="data">A pointer to the data to be set on the property.</param>
    /// <param name="dataSize">The number of bytes in the property set by the function.</param>
    /// <param name="constantBufferSize">The size of <paramref name="constantBuffer"/>.</param>
    /// <param name="constantBuffer">A reference to the constant buffer data storage.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static int SetConstantBuffer(
        byte* data,
        uint dataSize,
        uint constantBufferSize,
        ref byte* constantBuffer)
    {
        if (data is null)
        {
            return E.E_POINTER;
        }

        if (dataSize != constantBufferSize)
        {
            return E.E_INVALIDARG;
        }

        // Reuse the existing buffer if there is one, otherwise allocate a new one
        if (constantBuffer is not null)
        {
            Buffer.MemoryCopy(data, constantBuffer, dataSize, dataSize);
        }
        else
        {
            void* buffer = NativeMemory.Alloc(dataSize);

            Buffer.MemoryCopy(data, buffer, dataSize, dataSize);

            constantBuffer = (byte*)buffer;
        }

        return S.S_OK;
    }

    /// <summary>
    /// Gets the transform mapper from a given D2D shader effect implementation.
    /// </summary>
    /// <param name="data">A pointer to a variable that stores the data that this function retrieves on the property.</param>
    /// <param name="dataSize">The number of bytes in the property to retrieve.</param>
    /// <param name="actualSize">A optional pointer to a variable that stores the actual number of bytes retrieved on the property.</param>
    /// <param name="d2D1TransformMapper">The current <see cref="ID2D1TransformMapper"/> instance in use.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static int GetTransformMapper(
        byte* data,
        uint dataSize,
        uint* actualSize,
        ID2D1TransformMapper* d2D1TransformMapper)
    {
        if (data is null)
        {
            return E.E_POINTER;
        }

        if (dataSize < sizeof(void*))
        {
            return E.E_INVALIDARG;
        }

        if (d2D1TransformMapper is null)
        {
            *(void**)data = null;
        }
        else
        {
            HRESULT hresult = ((IUnknown*)d2D1TransformMapper)->QueryInterface(Windows.__uuidof<ID2D1TransformMapper>(), (void**)data);

            if (!Windows.SUCCEEDED(hresult))
            {
                return hresult;
            }
        }

        if (actualSize is not null)
        {
            *actualSize = (uint)sizeof(void*);
        }

        return S.S_OK;
    }

    /// <summary>
    /// Sets the transform mapper from a given D2D shader effect implementation.
    /// </summary>
    /// <param name="data">A pointer to the data to be set on the property.</param>
    /// <param name="dataSize">The number of bytes in the property set by the function.</param>
    /// <param name="d2D1TransformMapper">A reference to the <see cref="ID2D1TransformMapper"/> instance storage.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static int SetTransformMapper(
        byte* data,
        uint dataSize,
        ref ID2D1TransformMapper* d2D1TransformMapper)
    {
        if (data is null)
        {
            return E.E_POINTER;
        }

        void* value = *(void**)data;

        if (value is null)
        {
            return E.E_POINTER;
        }

        if (dataSize != (uint)sizeof(void*))
        {
            return E.E_INVALIDARG;
        }

        using ComPtr<IUnknown> unknown = (IUnknown*)value;
        using ComPtr<ID2D1TransformMapper> transformMapper = default;

        // Check that the input object implements ID2D1TransformMapper
        int result = unknown.CopyTo(transformMapper.GetAddressOf());

        if (result != S.S_OK)
        {
            return result;
        }

        // If there's already an existing manager, release it
        if (d2D1TransformMapper is not null)
        {
            _ = ((IUnknown*)d2D1TransformMapper)->Release();
        }

        // Store the transform mapper manager into the effect
        d2D1TransformMapper = transformMapper.Detach();

        return S.S_OK;
    }

    /// <summary>
    /// Gets the resource texture manager at a given index from a given D2D shader effect implementation.
    /// </summary>
    /// <param name="data">A pointer to a variable that stores the data that this function retrieves on the property.</param>
    /// <param name="dataSize">The number of bytes in the property to retrieve.</param>
    /// <param name="actualSize">A optional pointer to a variable that stores the actual number of bytes retrieved on the property.</param>
    /// <param name="resourceTextureDescriptionCount">The number of available resource texture descriptions.</param>
    /// <param name="resourceTextureDescriptions">The buffer with the available resource texture descriptions for the shader.</param>
    /// <param name="resourceTextureManagerBuffer">The resource texture managers in use.</param>
    /// <param name="resourceTextureIndex">The index of the resource texture to get.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static int GetResourceTextureManagerAtIndex(
        byte* data,
        uint dataSize,
        uint* actualSize,
        int resourceTextureDescriptionCount,
        D2D1ResourceTextureDescription* resourceTextureDescriptions,
        ref ResourceTextureManagerBuffer resourceTextureManagerBuffer,
        int resourceTextureIndex)
    {
        if (!IsResourceTextureManagerIndexValid(
            resourceTextureDescriptionCount,
            resourceTextureDescriptions,
            resourceTextureIndex,
            out _))
        {
            return E.E_INVALIDARG;
        }

        if (data is null)
        {
            return E.E_POINTER;
        }

        if (dataSize < sizeof(void*))
        {
            return E.E_INVALIDARG;
        }

        using ComPtr<ID2D1ResourceTextureManager> resourceTextureManager = resourceTextureManagerBuffer[resourceTextureIndex];

        HRESULT hresult = resourceTextureManager.CopyTo((ID2D1ResourceTextureManager**)data);

        if (!Windows.SUCCEEDED(hresult))
        {
            return hresult;
        }

        if (actualSize is not null)
        {
            *actualSize = (uint)sizeof(void*);
        }

        return S.S_OK;
    }

    /// <summary>
    /// Sets the resource texture manager at a given index from a given D2D shader effect implementation.
    /// </summary>
    /// <param name="data">A pointer to the data to be set on the property.</param>
    /// <param name="dataSize">The number of bytes in the property set by the function.</param>
    /// <param name="resourceTextureDescriptionCount">The number of available resource texture descriptions.</param>
    /// <param name="resourceTextureDescriptions">The buffer with the available resource texture descriptions for the shader.</param>
    /// <param name="resourceTextureManagerBuffer">The resource texture managers in use.</param>
    /// <param name="d2D1EffectContext">The <see cref="ID2D1EffectContext"/> instance in use.</param>
    /// <param name="resourceTextureIndex">The index of the resource texture to get.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static int SetResourceTextureManagerAtIndex(
        byte* data,
        uint dataSize,
        int resourceTextureDescriptionCount,
        D2D1ResourceTextureDescription* resourceTextureDescriptions,
        ref ResourceTextureManagerBuffer resourceTextureManagerBuffer,
        ID2D1EffectContext* d2D1EffectContext,
        int resourceTextureIndex)
    {
        if (!IsResourceTextureManagerIndexValid(
            resourceTextureDescriptionCount,
            resourceTextureDescriptions,
            resourceTextureIndex,
            out uint dimensions))
        {
            return E.E_INVALIDARG;
        }

        if (data is null)
        {
            return E.E_POINTER;
        }

        void* value = *(void**)data;

        if (value is null)
        {
            return E.E_POINTER;
        }

        if (dataSize != (uint)sizeof(void*))
        {
            return E.E_INVALIDARG;
        }

        using ComPtr<IUnknown> unknown = (IUnknown*)value;
        using ComPtr<ID2D1ResourceTextureManager> resourceTextureManager = default;

        // Check that the input object implements ID2D1ResourceTextureManager
        int result = unknown.CopyTo(resourceTextureManager.GetAddressOf());

        if (result != S.S_OK)
        {
            return result;
        }

        using ComPtr<ID2D1ResourceTextureManagerInternal> resourceTextureManagerInternal = default;

        // Then, also check that it implements ID2D1ResourceTextureManagerInternal
        result = unknown.CopyTo(resourceTextureManagerInternal.GetAddressOf());

        if (result != S.S_OK)
        {
            return result;
        }

        // Initialize the resource texture manager, if an effect context is available
        if (d2D1EffectContext is not null)
        {
            result = resourceTextureManagerInternal.Get()->Initialize(d2D1EffectContext, &dimensions);

            // ID2D1ResourceTextureManager::Initialize should generally return either S_OK for first
            // initialization, S_FALSE if an ID2D1EffectContext is already present (which is allowed, to
            // enable sharing resource texture managers across different source textures and effects), or
            // E_INVALIDARG if the manager has already been initialized through the public interface,
            // and the stored dimensions for that don't match the ones for this resource texture index.
            if (!Windows.SUCCEEDED(result))
            {
                return result;
            }
        }

        ref ID2D1ResourceTextureManager* currentResourceTextureManager = ref resourceTextureManagerBuffer[resourceTextureIndex];

        // If there's already an existing manager at this index, release it
        if (currentResourceTextureManager is not null)
        {
            _ = ((IUnknown*)currentResourceTextureManager)->Release();
        }

        // Store the resource texture manager into the buffer
        currentResourceTextureManager = resourceTextureManager.Detach();

        return S.S_OK;
    }

    /// <summary>
    /// Checks whether a given index for a resource texture manager is valid for the current effect.
    /// </summary>
    /// <param name="resourceTextureDescriptionCount">The number of available resource texture descriptions.</param>
    /// <param name="resourceTextureDescriptions">The buffer with the available resource texture descriptions for the shader.</param>
    /// <param name="resourceTextureIndex">The index of the resource texture to validate.</param>
    /// <param name="dimensions">The number of dimensions for the resource texture at the gven index.</param>
    /// <returns>Whether or not <paramref name="resourceTextureIndex"/> is valid for the current effect.</returns>
    private static bool IsResourceTextureManagerIndexValid(
        int resourceTextureDescriptionCount,
        D2D1ResourceTextureDescription* resourceTextureDescriptions,
        int resourceTextureIndex,
        out uint dimensions)
    {
        ReadOnlySpan<D2D1ResourceTextureDescription> d2D1ResourceTextureDescriptionRange = new(resourceTextureDescriptions, resourceTextureDescriptionCount);

        foreach (ref readonly D2D1ResourceTextureDescription resourceTextureDescription in d2D1ResourceTextureDescriptionRange)
        {
            if (resourceTextureDescription.Index == resourceTextureIndex)
            {
                dimensions = (uint)resourceTextureDescription.Dimensions;

                return true;
            }
        }

        dimensions = 0;

        return false;
    }
}
