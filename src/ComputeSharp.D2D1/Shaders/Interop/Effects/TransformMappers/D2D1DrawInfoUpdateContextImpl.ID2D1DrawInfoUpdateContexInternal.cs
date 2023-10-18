using System;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

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