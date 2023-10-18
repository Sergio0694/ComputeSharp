using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
unsafe partial struct PixelShaderEffect
{
    /// <summary>
    /// Gets the get accessor for the resource manager at index 0.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager0
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager0Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 0.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager0
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager0Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 1.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager1
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager1Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 1.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager1
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager1Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 2.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager2
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager2Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 2.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager2
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager2Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 3.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager3
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager3Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 3.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager3
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager3Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 4.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager4
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager4Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 4.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager4
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager4Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 5.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager5
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager5Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 5.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager5
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager5Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 6.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager6
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager6Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 6.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager6
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager6Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 7.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager7
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager7Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 7.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager7
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager7Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 8.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager8
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager8Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 8.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager8
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager8Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 9.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager9
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager9Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 9.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager9
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager9Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 10.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager10
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager10Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 10.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager10
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager10Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 11.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager11
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager11Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 11.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager11
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager11Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 12.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager12
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager12Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 12.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager12
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager12Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 13.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager13
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager13Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 13.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager13
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager13Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 14.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager14
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager14Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 14.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager14
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager14Impl;
        }
    }

    /// <summary>
    /// Gets the get accessor for the resource manager at index 15.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager15
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &GetResourceTextureManager15Impl;
        }
    }

    /// <summary>
    /// Gets the set accessor for the resource manager at index 15.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetResourceTextureManager15
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return &SetResourceTextureManager15Impl;
        }
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager0Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(0, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager0Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(0, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager1Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(1, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager1Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(1, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager2Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(2, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager2Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(2, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager3Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(3, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager3Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(3, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager4Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(4, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager4Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(4, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager5Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(5, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager5Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(5, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager6Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(6, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager6Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(6, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager7Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(7, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager7Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(7, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager8Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(8, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager8Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(8, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager9Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(9, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager9Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(9, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager10Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(10, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager10Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(10, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager11Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(11, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager11Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(11, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager12Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(12, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager12Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(12, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager13Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(13, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager13Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(13, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager14Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(14, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager14Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(14, data, dataSize);
    }
    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetResourceTextureManager15Impl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        return ((PixelShaderEffect*)effect)->GetResourceTextureManagerAtIndex(15, data, dataSize, actualSize);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetResourceTextureManager15Impl(IUnknown* effect, byte* data, uint dataSize)
    {
        return ((PixelShaderEffect*)effect)->SetResourceTextureManagerAtIndex(15, data, dataSize);
    }
}