using System.Runtime.CompilerServices;
#if !NET6_OR_GREATER
using System.Runtime.InteropServices;
#endif
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
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager0Wrapper = GetResourceTextureManager0Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager0"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager0Wrapper = SetResourceTextureManager0Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager1"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager1Wrapper = GetResourceTextureManager1Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager1"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager1Wrapper = SetResourceTextureManager1Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager2"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager2Wrapper = GetResourceTextureManager2Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager2"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager2Wrapper = SetResourceTextureManager2Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager3"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager3Wrapper = GetResourceTextureManager3Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager3"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager3Wrapper = SetResourceTextureManager3Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager4"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager4Wrapper = GetResourceTextureManager4Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager4"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager4Wrapper = SetResourceTextureManager4Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager5"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager5Wrapper = GetResourceTextureManager5Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager5"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager5Wrapper = SetResourceTextureManager5Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager6"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager6Wrapper = GetResourceTextureManager6Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager6"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager6Wrapper = SetResourceTextureManager6Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager7"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager7Wrapper = GetResourceTextureManager7Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager7"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager7Wrapper = SetResourceTextureManager7Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager8"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager8Wrapper = GetResourceTextureManager8Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager8"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager8Wrapper = SetResourceTextureManager8Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager9"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager9Wrapper = GetResourceTextureManager9Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager9"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager9Wrapper = SetResourceTextureManager9Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager10"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager10Wrapper = GetResourceTextureManager10Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager10"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager10Wrapper = SetResourceTextureManager10Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager11"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager11Wrapper = GetResourceTextureManager11Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager11"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager11Wrapper = SetResourceTextureManager11Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager12"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager12Wrapper = GetResourceTextureManager12Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager12"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager12Wrapper = SetResourceTextureManager12Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager13"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager13Wrapper = GetResourceTextureManager13Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager13"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager13Wrapper = SetResourceTextureManager13Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager14"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager14Wrapper = GetResourceTextureManager14Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager14"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager14Wrapper = SetResourceTextureManager14Impl;

    /// <summary>
    /// A cached <see cref="PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetResourceTextureManager15"/>.
    /// </summary>
    private static readonly PropertyGetFunctionDelegate GetResourceTextureManager15Wrapper = GetResourceTextureManager15Impl;

    /// <summary>
    /// A cached <see cref="PropertySetFunctionDelegate"/> instance wrapping <see cref="SetResourceTextureManager15"/>.
    /// </summary>
    private static readonly PropertySetFunctionDelegate SetResourceTextureManager15Wrapper = SetResourceTextureManager15Impl;
#endif

    /// <summary>
    /// Gets the get accessor for the resource manager at index 0.
    /// </summary>
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetResourceTextureManager0
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager0Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager0Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager0Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager0Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager1Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager1Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager1Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager1Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager2Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager2Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager2Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager2Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager3Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager3Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager3Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager3Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager4Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager4Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager4Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager4Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager5Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager5Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager5Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager5Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager6Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager6Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager6Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager6Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager7Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager7Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager7Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager7Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager8Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager8Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager8Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager8Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager9Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager9Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager9Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager9Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager10Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager10Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager10Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager10Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager11Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager11Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager11Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager11Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager12Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager12Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager12Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager12Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager13Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager13Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager13Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager13Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager14Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager14Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager14Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager14Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &GetResourceTextureManager15Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)(void*)Marshal.GetFunctionPointerForDelegate(GetResourceTextureManager15Wrapper);
#endif
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
#if NET6_0_OR_GREATER
            return &SetResourceTextureManager15Impl;
#else
            return (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)(void*)Marshal.GetFunctionPointerForDelegate(SetResourceTextureManager15Wrapper);
#endif
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