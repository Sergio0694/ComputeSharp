using System;
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
    public delegate int PropertyGetFunctionDelegate(IUnknown* effect, byte* data, uint dataSize, uint* actualSize);

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.setFunction"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int PropertySetBindingDelegate(IUnknown* effect, byte* data, uint dataSize);

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetConstantBuffer"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetConstantBufferWrapper = GetConstantBuffer;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetConstantBuffer"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetConstantBufferWrapper = SetConstantBuffer;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager0"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager0Wrapper = GetResourceTextureManager0;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager0"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager0Wrapper = SetResourceTextureManager0;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager1"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager1Wrapper = GetResourceTextureManager1;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager1"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager1Wrapper = SetResourceTextureManager1;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager2"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager2Wrapper = GetResourceTextureManager2;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager2"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager2Wrapper = SetResourceTextureManager2;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager3"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager3Wrapper = GetResourceTextureManager3;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager3"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager3Wrapper = SetResourceTextureManager3;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager4"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager4Wrapper = GetResourceTextureManager4;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager4"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager4Wrapper = SetResourceTextureManager4;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager5"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager5Wrapper = GetResourceTextureManager5;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager5"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager5Wrapper = SetResourceTextureManager5;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager6"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager6Wrapper = GetResourceTextureManager6;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager6"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager6Wrapper = SetResourceTextureManager6;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager7"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager7Wrapper = GetResourceTextureManager7;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager7"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager7Wrapper = SetResourceTextureManager7;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager8"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager8Wrapper = GetResourceTextureManager8;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager8"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager8Wrapper = SetResourceTextureManager8;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager9"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager9Wrapper = GetResourceTextureManager9;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager9"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager9Wrapper = SetResourceTextureManager9;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager10"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager10Wrapper = GetResourceTextureManager10;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager10"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager10Wrapper = SetResourceTextureManager10;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager11"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager11Wrapper = GetResourceTextureManager11;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager11"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager11Wrapper = SetResourceTextureManager11;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager12"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager12Wrapper = GetResourceTextureManager12;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager12"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager12Wrapper = SetResourceTextureManager12;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager13"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager13Wrapper = GetResourceTextureManager13;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager13"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager13Wrapper = SetResourceTextureManager13;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager14"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager14Wrapper = GetResourceTextureManager14;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager14"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager14Wrapper = SetResourceTextureManager14;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager15"/>.
    /// </summary>
    public static readonly PropertyGetFunctionDelegate GetResourceTextureManager15Wrapper = GetResourceTextureManager15;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager15"/>.
    /// </summary>
    public static readonly PropertySetBindingDelegate SetResourceTextureManager15Wrapper = SetResourceTextureManager15;
#endif

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetConstantBuffer(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
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
    [UnmanagedCallersOnly]
    public static int SetConstantBuffer(IUnknown* effect, byte* data, uint dataSize)
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

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager0(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(0, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager0(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(0, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager1(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(1, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager1(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(1, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager2(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(2, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager2(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(2, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager3(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(3, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager3(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(3, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager4(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(4, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager4(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(4, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager5(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(5, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager5(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(5, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager6(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(6, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager6(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(6, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager7(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(7, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager7(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(7, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager8(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(8, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager8(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(8, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager9(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(9, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager9(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(9, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager10(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(10, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager10(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(10, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager11(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(11, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager11(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(11, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager12(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(12, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager12(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(12, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager13(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(13, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager13(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(13, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager14(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(14, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager14(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(14, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int GetResourceTextureManager15(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(15, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    public static int SetResourceTextureManager15(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(15, data, dataSize);
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