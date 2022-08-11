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
    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager0"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager0Wrapper = GetResourceTextureManager0;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager0"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager0Wrapper = SetResourceTextureManager0;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager1"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager1Wrapper = GetResourceTextureManager1;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager1"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager1Wrapper = SetResourceTextureManager1;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager2"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager2Wrapper = GetResourceTextureManager2;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager2"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager2Wrapper = SetResourceTextureManager2;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager3"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager3Wrapper = GetResourceTextureManager3;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager3"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager3Wrapper = SetResourceTextureManager3;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager4"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager4Wrapper = GetResourceTextureManager4;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager4"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager4Wrapper = SetResourceTextureManager4;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager5"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager5Wrapper = GetResourceTextureManager5;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager5"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager5Wrapper = SetResourceTextureManager5;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager6"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager6Wrapper = GetResourceTextureManager6;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager6"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager6Wrapper = SetResourceTextureManager6;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager7"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager7Wrapper = GetResourceTextureManager7;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager7"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager7Wrapper = SetResourceTextureManager7;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager8"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager8Wrapper = GetResourceTextureManager8;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager8"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager8Wrapper = SetResourceTextureManager8;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager9"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager9Wrapper = GetResourceTextureManager9;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager9"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager9Wrapper = SetResourceTextureManager9;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager10"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager10Wrapper = GetResourceTextureManager10;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager10"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager10Wrapper = SetResourceTextureManager10;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager11"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager11Wrapper = GetResourceTextureManager11;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager11"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager11Wrapper = SetResourceTextureManager11;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager12"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager12Wrapper = GetResourceTextureManager12;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager12"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager12Wrapper = SetResourceTextureManager12;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager13"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager13Wrapper = GetResourceTextureManager13;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager13"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager13Wrapper = SetResourceTextureManager13;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager14"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager14Wrapper = GetResourceTextureManager14;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager14"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager14Wrapper = SetResourceTextureManager14;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager15"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager15Wrapper = GetResourceTextureManager15;

    /// <summary>
    /// A cached <see cref="PropertySetBindingDelegate"/> instance wrapping <see cref="SetResourceTextureManager15"/>.
    /// </summary>
    private static readonly PropertySetBindingDelegate SetResourceTextureManager15Wrapper = SetResourceTextureManager15;
#endif

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager0(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(0, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager0(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(0, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager1(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(1, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager1(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(1, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager2(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(2, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager2(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(2, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager3(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(3, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager3(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(3, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager4(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(4, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager4(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(4, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager5(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(5, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager5(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(5, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager6(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(6, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager6(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(6, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager7(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(7, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager7(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(7, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager8(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(8, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager8(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(8, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager9(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(9, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager9(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(9, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager10(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(10, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager10(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(10, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager11(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(11, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager11(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(11, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager12(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(12, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager12(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(12, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager13(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(13, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager13(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(13, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager14(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(14, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager14(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(14, data, dataSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int GetResourceTextureManager15(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(15, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly]
    private static int SetResourceTextureManager15(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(15, data, dataSize);
    }
}