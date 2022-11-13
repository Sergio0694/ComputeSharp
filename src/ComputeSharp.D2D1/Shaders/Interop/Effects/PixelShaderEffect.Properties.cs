using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
unsafe partial struct PixelShaderEffect
{
#if !NET6_0_OR_GREATER
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int PropertyGetFunctionDelegate(IUnknown* effect, byte* data, uint dataSize, uint* actualSize);

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.setFunction"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int PropertySetFunctionDelegate(IUnknown* effect, byte* data, uint dataSize);

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetConstantBufferImpl"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetConstantBufferWrapper = GetConstantBufferImpl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetConstantBufferImpl"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetConstantBufferWrapper = SetConstantBufferImpl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetTransformMapperImpl"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetTransformMapperWrapper = GetTransformMapperImpl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetTransformMapperImpl"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetTransformMapperWrapper = SetTransformMapperImpl;
#endif

    /// <summary>
    /// Gets the get accessor for the constant buffer.
    /// </summary>
#if NET6_0_OR_GREATER
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetConstantBuffer
#else
    public static void* GetConstantBuffer
#endif
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
        get => &GetConstantBufferImpl;
#else
        get => (void*)Marshal.GetFunctionPointerForDelegate(GetConstantBufferWrapper);
#endif
    }

    /// <summary>
    /// Gets the set accessor for the constant buffer.
    /// </summary>
#if NET6_0_OR_GREATER
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetConstantBuffer
#else
    public static void* SetConstantBuffer
#endif
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
        get => &SetConstantBufferImpl;
#else
        get => (void*)Marshal.GetFunctionPointerForDelegate(SetConstantBufferWrapper);
#endif
    }

    /// <summary>
    /// Gets the get accessor for the transform mapper.
    /// </summary>
#if NET6_0_OR_GREATER
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetTransformMapper
#else
    public static void* GetTransformMapper
#endif
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
        get => &GetTransformMapperImpl;
#else
        get => (void*)Marshal.GetFunctionPointerForDelegate(GetTransformMapperWrapper);
#endif
    }

    /// <summary>
    /// Gets the set accessor for the transform mapper.
    /// </summary>
#if NET6_0_OR_GREATER
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetTransformMapper
#else
    public static void* SetTransformMapper
#endif
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
        get => &SetTransformMapperImpl;
#else
        get => (void*)Marshal.GetFunctionPointerForDelegate(SetTransformMapperWrapper);
#endif
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetConstantBufferImpl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        // This is a call likely from ID2D1Effect::GetValueSize, to query the property size
        if (data is null &&
            dataSize == 0 &&
            actualSize is not null)
        {
            *actualSize = (uint)@this->constantBufferSize;

            return S.S_FALSE;
        }

        if (data is null)
        {
            return E.E_POINTER;
        }

        if (dataSize < @this->constantBufferSize)
        {
            return E.E_NOT_SUFFICIENT_BUFFER;
        }

        if (@this->constantBuffer is null)
        {
            return E.E_NOT_VALID_STATE;
        }

        uint writtenBytes = 0;

        if (@this->constantBufferSize > 0)
        {
            Buffer.MemoryCopy(@this->constantBuffer, data, dataSize, @this->constantBufferSize);

            writtenBytes = (uint)@this->constantBufferSize;
        }

        if (actualSize is not null)
        {
            *actualSize = writtenBytes;
        }

        return S.S_OK;
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetConstantBufferImpl(IUnknown* effect, byte* data, uint dataSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        if (data is null)
        {
            return E.E_POINTER;
        }

        if (dataSize != (uint)@this->constantBufferSize)
        {
            return E.E_INVALIDARG;
        }

        // Reuse the existing buffer if there is one, otherwise allocate a new one
        if (@this->constantBuffer is not null)
        {
            Buffer.MemoryCopy(data, @this->constantBuffer, dataSize, dataSize);
        }
        else
        {
            void* buffer = NativeMemory.Alloc(dataSize);

            Buffer.MemoryCopy(data, buffer, dataSize, dataSize);

            @this->constantBuffer = (byte*)buffer;
        }

        return S.S_OK;
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetTransformMapperImpl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        if (data is null)
        {
            return E.E_POINTER;
        }

        if (dataSize < sizeof(void*))
        {
            return E.E_INVALIDARG;
        }

        if (@this->d2D1TransformMapper is null)
        {
            *(void**)data = null;
        }
        else
        {
            _ = ((IUnknown*)@this->d2D1TransformMapper)->QueryInterface(Windows.__uuidof<ID2D1TransformMapper>(), (void**)data);
        }

        if (actualSize is not null)
        {
            *actualSize = (uint)sizeof(void*);
        }

        return S.S_OK;
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetTransformMapperImpl(IUnknown* effect, byte* data, uint dataSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

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
        if (@this->d2D1TransformMapper is not null)
        {
            _ = ((IUnknown*)@this->d2D1TransformMapper)->Release();
        }

        // Store the transform mapper manager into the effect
        @this->d2D1TransformMapper = transformMapper.Detach();

        return S.S_OK;
    }

    /// <summary>
    /// Gets the resource texture manager for a given index.
    /// </summary>
    /// <param name="resourceTextureIndex">The index of the resource texture to get the manager for.</param>
    /// <param name="data">A pointer to a variable that stores the data that this function retrieves on the property.</param>
    /// <param name="dataSize">The number of bytes in the property to retrieve.</param>
    /// <param name="actualSize">A optional pointer to a variable that stores the actual number of bytes retrieved on the property.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    private int GetResourceTextureManagerAtIndex(int resourceTextureIndex, byte* data, uint dataSize, uint* actualSize)
    {
        if (!IsResourceTextureManagerIndexValid(resourceTextureIndex, out _))
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

        using ComPtr<ID2D1ResourceTextureManager> resourceTextureManager = this.resourceTextureManagerBuffer[resourceTextureIndex];

        _ = resourceTextureManager.CopyTo((ID2D1ResourceTextureManager**)data);

        if (actualSize is not null)
        {
            *actualSize = (uint)sizeof(void*);
        }

        return S.S_OK;
    }

    /// <summary>
    /// Sets the resource texture manager for a given index.
    /// </summary>
    /// <param name="resourceTextureIndex">The index of the resource texture to set the manager for.</param>
    /// <param name="data">A pointer to the data to be set on the property.</param>
    /// <param name="dataSize">The number of bytes in the property set by the function.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    private int SetResourceTextureManagerAtIndex(int resourceTextureIndex, byte* data, uint dataSize)
    {
        if (!IsResourceTextureManagerIndexValid(resourceTextureIndex, out uint dimensions))
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
        if (this.d2D1EffectContext is not null)
        {
            result = resourceTextureManagerInternal.Get()->Initialize(this.d2D1EffectContext, &dimensions);

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

        ref ID2D1ResourceTextureManager* currentResourceTextureManager = ref this.resourceTextureManagerBuffer[resourceTextureIndex];

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
    /// <param name="resourceTextureIndex">The index of the resource texture to validate.</param>
    /// <param name="dimensions">The number of dimensions for the resource texture at the gven index.</param>
    /// <returns>Whether or not <paramref name="resourceTextureIndex"/> is valid for the current effect.</returns>
    private bool IsResourceTextureManagerIndexValid(int resourceTextureIndex, out uint dimensions)
    {
        foreach (ref readonly D2D1ResourceTextureDescription resourceTextureDescription in new ReadOnlySpan<D2D1ResourceTextureDescription>(this.resourceTextureDescriptions, this.resourceTextureDescriptionCount))
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