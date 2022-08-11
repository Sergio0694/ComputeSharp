using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        if (@this->constantBufferSize == 0)
        {
            *actualSize = 0;
        }
        else
        {
            int bytesToCopy = Math.Min((int)dataSize, @this->constantBufferSize);

            Buffer.MemoryCopy(@this->constantBuffer, data, dataSize, bytesToCopy);

            *actualSize = (uint)bytesToCopy;
        }

        return S.S_OK;
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetConstantBufferImpl(IUnknown* effect, byte* data, uint dataSize)
    {
        PixelShaderEffect* @this = (PixelShaderEffect*)effect;

        if (@this->constantBuffer is not null)
        {
            NativeMemory.Free(@this->constantBuffer);
        }

        void* buffer = NativeMemory.Alloc(dataSize);

        Buffer.MemoryCopy(data, buffer, dataSize, dataSize);

        @this->constantBuffer = (byte*)buffer;
        @this->constantBufferSize = (int)dataSize;

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
        // TODO: implement GetResourceTextureIndex

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
        // TODO: implement SetResourceTextureIndex

        return S.S_OK;
    }
}