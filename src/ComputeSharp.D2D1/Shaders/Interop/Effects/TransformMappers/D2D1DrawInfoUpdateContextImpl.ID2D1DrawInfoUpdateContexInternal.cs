using System;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <inheritdoc/>
unsafe partial struct D2D1DrawInfoUpdateContextImpl
{
    /// <inheritdoc cref="ID2D1DrawInfoUpdateContextInternal.Close"/>
    public int Close()
    {
        this.d2D1DrawInfo = null;

        return S.S_OK;
    }

    /// <summary>
    /// The implementation for <see cref="ID2D1DrawInfoUpdateContextInternal"/>.
    /// </summary>
    private static class ID2D1DrawInfoUpdateContextInternalMethods
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="Close"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int CloseDelegate(D2D1DrawInfoUpdateContextImpl* @this);

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
        /// A cached <see cref="CloseDelegate"/> instance wrapping <see cref="Close"/>.
        /// </summary>
        public static readonly CloseDelegate CloseWrapper = Close;
#endif

        /// <inheritdoc cref="D2D1DrawInfoUpdateContextImpl.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(D2D1DrawInfoUpdateContextImpl* @this, Guid* riid, void** ppvObject)
        {
            @this = (D2D1DrawInfoUpdateContextImpl*)&((void**)@this)[-1];

            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="D2D1DrawInfoUpdateContextImpl.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(D2D1DrawInfoUpdateContextImpl* @this)
        {
            @this = (D2D1DrawInfoUpdateContextImpl*)&((void**)@this)[-1];

            return @this->AddRef();
        }

        /// <inheritdoc cref="D2D1DrawInfoUpdateContextImpl.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(D2D1DrawInfoUpdateContextImpl* @this)
        {
            @this = (D2D1DrawInfoUpdateContextImpl*)&((void**)@this)[-1];

            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1DrawInfoUpdateContextInternal.Close"/>
        [UnmanagedCallersOnly]
        public static int Close(D2D1DrawInfoUpdateContextImpl* @this)
        {
            @this = (D2D1DrawInfoUpdateContextImpl*)&((void**)@this)[-1];

            return @this->Close();
        }
    }
}