using System;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
unsafe partial struct PixelShaderEffect
{
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static HRESULT GetConstantBufferImpl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        try
        {
            // This is a call likely from ID2D1Effect::GetValueSize, to query the property size.
            // In that case we explicitly allow these inputs, and return the size of the buffer.
            if (data is null &&
                dataSize == 0 &&
                actualSize is not null)
            {
                *actualSize = (uint)@this->GetGlobals().ConstantBufferSize;

                return S.S_FALSE;
            }

            default(ArgumentNullException).ThrowIfNull(data);

            if (dataSize < @this->GetGlobals().ConstantBufferSize)
            {
                return E.E_NOT_SUFFICIENT_BUFFER;
            }

            if (@this->constantBuffer is null)
            {
                return E.E_NOT_VALID_STATE;
            }

            uint writtenBytes = 0;

            if (@this->GetGlobals().ConstantBufferSize > 0)
            {
                Buffer.MemoryCopy(@this->constantBuffer, data, dataSize, @this->GetGlobals().ConstantBufferSize);

                writtenBytes = (uint)@this->GetGlobals().ConstantBufferSize;
            }

            if (actualSize is not null)
            {
                *actualSize = writtenBytes;
            }

            return S.S_OK;
        }
        catch (Exception e)
        {
            return Marshal.GetHRForException(e);
        }
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static HRESULT SetConstantBufferImpl(IUnknown* effect, byte* data, uint dataSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        try
        {
            default(ArgumentNullException).ThrowIfNull(data);
            default(ArgumentOutOfRangeException).ThrowIfNotEqual((int)dataSize, @this->GetGlobals().ConstantBufferSize, nameof(dataSize));

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
        catch (Exception e)
        {
            return Marshal.GetHRForException(e);
        }
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static HRESULT GetTransformMapperImpl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        try
        {
            default(ArgumentNullException).ThrowIfNull(data);
            default(ArgumentOutOfRangeException).ThrowIfLessThan((int)dataSize, sizeof(void*), nameof(dataSize));

            @this->d2D1TransformMapper.CopyTo((ID2D1DrawTransformMapper**)data).Assert();

            if (actualSize is not null)
            {
                *actualSize = (uint)sizeof(void*);
            }

            return S.S_OK;
        }
        catch (Exception e)
        {
            return Marshal.GetHRForException(e);
        }
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static HRESULT SetTransformMapperImpl(IUnknown* effect, byte* data, uint dataSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        try
        {
            default(ArgumentNullException).ThrowIfNull(data);
            default(ArgumentOutOfRangeException).ThrowIfLessThan((int)dataSize, sizeof(void*), nameof(dataSize));

            void* value = *(void**)data;

            default(ArgumentNullException).ThrowIfNull(value);

            using ComPtr<IUnknown> unknown = new((IUnknown*)value);
            using ComPtr<ID2D1DrawTransformMapper> transformMapper = default;

            // Check that the input object implements ID2D1DrawTransformMapper
            unknown.CopyTo(transformMapper.GetAddressOf()).Assert();

            // Store the transform mapper manager into the effect
            @this->d2D1TransformMapper.Attach(transformMapper.Detach());

            return S.S_OK;
        }
        catch (Exception e)
        {
            return Marshal.GetHRForException(e);
        }
    }

    /// <summary>
    /// Gets the resource texture manager for a given index.
    /// </summary>
    /// <param name="index">The index of the resource texture manager to assign the resource texture manager to (this might not match the resource texture index).</param>
    /// <param name="data">A pointer to a variable that stores the data that this function retrieves on the property.</param>
    /// <param name="dataSize">The number of bytes in the property to retrieve.</param>
    /// <param name="actualSize">A optional pointer to a variable that stores the actual number of bytes retrieved on the property.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    private int GetResourceTextureManagerAtIndex(int index, byte* data, uint dataSize, uint* actualSize)
    {
        try
        {
            default(ArgumentNullException).ThrowIfNull(data);
            default(ArgumentOutOfRangeException).ThrowIfLessThan((int)dataSize, sizeof(void*), nameof(dataSize));
            default(ArgumentOutOfRangeException).ThrowIfGreaterThanOrEqual(index, GetGlobals().ResourceTextureCount);

            this.resourceTextureManagerBuffer[index].CopyTo((ID2D1ResourceTextureManager**)data).Assert();

            if (actualSize is not null)
            {
                *actualSize = (uint)sizeof(void*);
            }

            return S.S_OK;
        }
        catch (Exception e)
        {
            return Marshal.GetHRForException(e);
        }
    }

    /// <summary>
    /// Sets the resource texture manager for a given index.
    /// </summary>
    /// <param name="index">The index of the resource texture manager to assign the resource texture manager to (this might not match the resource texture index).</param>
    /// <param name="data">A pointer to the data to be set on the property.</param>
    /// <param name="dataSize">The number of bytes in the property set by the function.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    private int SetResourceTextureManagerAtIndex(int index, byte* data, uint dataSize)
    {
        try
        {
            default(ArgumentNullException).ThrowIfNull(data);
            default(ArgumentOutOfRangeException).ThrowIfLessThan((int)dataSize, sizeof(void*), nameof(dataSize));
            default(ArgumentOutOfRangeException).ThrowIfGreaterThanOrEqual(index, GetGlobals().ResourceTextureCount);

            void* value = *(void**)data;

            default(ArgumentNullException).ThrowIfNull(value);

            using ComPtr<IUnknown> unknown = new((IUnknown*)value);
            using ComPtr<ID2D1ResourceTextureManager> resourceTextureManager = default;

            // Check that the input object implements ID2D1ResourceTextureManager
            unknown.CopyTo(resourceTextureManager.GetAddressOf()).Assert();

            using ComPtr<ID2D1ResourceTextureManagerInternal> resourceTextureManagerInternal = default;

            // Then, also check that it implements ID2D1ResourceTextureManagerInternal
            unknown.CopyTo(resourceTextureManagerInternal.GetAddressOf()).Assert();

            // Initialize the resource texture manager, if an effect context is available
            if (this.d2D1EffectContext.Get() is not null)
            {
                uint dimensions = (uint)GetGlobals().ResourceTextureDescriptions.Span[index].Dimensions;

                // ID2D1ResourceTextureManager::Initialize should generally return either S_OK for first
                // initialization, S_FALSE if an ID2D1EffectContext is already present (which is allowed, to
                // enable sharing resource texture managers across different source textures and effects), or
                // E_INVALIDARG if the manager has already been initialized through the public interface,
                // and the stored dimensions for that don't match the ones for this resource texture index.
                resourceTextureManagerInternal.Get()->Initialize(this.d2D1EffectContext.Get(), &dimensions).Assert();
            }

            // Store the resource texture manager into the buffer
            this.resourceTextureManagerBuffer[index].Attach(resourceTextureManager.Detach());

            return S.S_OK;
        }
        catch (Exception e)
        {
            return Marshal.GetHRForException(e);
        }
    }
}