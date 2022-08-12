using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
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
    private delegate int PropertySetBindingDelegate(IUnknown* effect, byte* data, uint dataSize);

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetConstantBufferImpl"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetConstantBufferWrapper = GetConstantBufferImpl;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetConstantBufferImpl"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetConstantBufferWrapper = SetConstantBufferImpl;
#endif

    /// <summary>
    /// Gets the get accessor for the constant buffer.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetConstantBuffer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
#if NET6_0_OR_GREATER
            return &GetConstantBufferImpl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetConstantBufferWrapper);
#endif
        }
    }

    /// <summary>
    /// Gets the set accessor for the constant buffer.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetConstantBuffer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
#if NET6_0_OR_GREATER
            return &SetConstantBufferImpl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetConstantBufferWrapper);
#endif
        }
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetConstantBufferImpl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        if (data is null || actualSize is null)
        {
            return E.E_POINTER;
        }

        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        if (dataSize < @this->constantBufferSize)
        {
            return E.E_NOT_SUFFICIENT_BUFFER;
        }

        if (@this->constantBuffer is null)
        {
            return E.E_NOT_VALID_STATE;
        }

        if (@this->constantBufferSize == 0)
        {
            *actualSize = 0;
        }
        else
        {
            Buffer.MemoryCopy(@this->constantBuffer, data, dataSize, @this->constantBufferSize);

            *actualSize = (uint)@this->constantBufferSize;
        }

        return S.S_OK;
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetConstantBufferImpl(IUnknown* effect, byte* data, uint dataSize)
    {
        if (data is null)
        {
            return E.E_POINTER;
        }

        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        if (dataSize != (uint)@this->constantBufferSize)
        {
            return E.E_INVALIDARG;
        }

        if (@this->constantBuffer is not null)
        {
            NativeMemory.Free(@this->constantBuffer);
        }

        void* buffer = NativeMemory.Alloc(dataSize);

        Buffer.MemoryCopy(data, buffer, dataSize, dataSize);

        @this->constantBuffer = (byte*)buffer;

        return S.S_OK;
    }

    /// <summary>
    /// Gets the resource texture manager for a given index.
    /// </summary>
    /// <param name="index">The index of the resource texture manager to get.</param>
    /// <param name="data">A pointer to a variable that stores the data that this function retrieves on the property.</param>
    /// <param name="dataSize">The number of bytes in the property to retrieve.</param>
    /// <param name="actualSize">A optional pointer to a variable that stores the actual number of bytes retrieved on the property.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    private int GetResourceTextureManagerAtIndex(int index, byte* data, uint dataSize, uint* actualSize)
    {
        if (!IsResourceTextureManagerIndexValid(index))
        {
            return E.E_INVALIDARG;
        }

        if (data is null || actualSize is null)
        {
            return E.E_POINTER;
        }

        if (dataSize < sizeof(void*))
        {
            return E.E_INVALIDARG;
        }

        using ComPtr<ID2D1ResourceTextureManager> resourceTextureManager = this.resourceTextureManagerBuffer[index];

        resourceTextureManager.CopyTo((ID2D1ResourceTextureManager**)data);

        *actualSize = (uint)sizeof(void*);

        return S.S_OK;
    }

    /// <summary>
    /// Sets the resource texture manager for a given index.
    /// </summary>
    /// <param name="index">The index of the resource texture manager to set.</param>
    /// <param name="data">A pointer to the data to be set on the property.</param>
    /// <param name="dataSize">The number of bytes in the property set by the function.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    private int SetResourceTextureManagerAtIndex(int index, byte* data, uint dataSize)
    {
        if (!IsResourceTextureManagerIndexValid(index))
        {
            return E.E_INVALIDARG;
        }

        if (data is null)
        {
            return E.E_POINTER;
        }

        if (dataSize != (uint)sizeof(void*))
        {
            return E.E_INVALIDARG;
        }

        using ComPtr<IUnknown> unknown = *(IUnknown**)data;
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
            resourceTextureManagerInternal.Get()->SetEffectContext(this.d2D1EffectContext);
        }

        // Store the resource texture manager into the buffer
        this.resourceTextureManagerBuffer[index] = resourceTextureManager.Detach();

        return S.S_OK;
    }

    /// <summary>
    /// Checks whether a given index for a resource texture manager is valid for the current effect.
    /// </summary>
    /// <param name="index">The resource texture manager index to validate.</param>
    /// <returns>Whether or not <paramref name="index"/> is valid for the current effect.</returns>
    private bool IsResourceTextureManagerIndexValid(int index)
    {
        foreach (ref readonly D2D1ResourceTextureDescription resourceTextureDescription in new ReadOnlySpan<D2D1ResourceTextureDescription>(this.resourceTextureDescriptions, this.resourceTextureDescriptionCount))
        {
            if (resourceTextureDescription.Index == index)
            {
                return true;
            }
        }

        return false;
    }
}