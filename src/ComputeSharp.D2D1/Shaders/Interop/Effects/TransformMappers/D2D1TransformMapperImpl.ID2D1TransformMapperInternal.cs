using System;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <inheritdoc/>
unsafe partial struct D2D1TransformMapperImpl
{
    /// <summary>
    /// The implementation for <see cref="ID2D1TransformMapperInternal"/>.
    /// </summary>
    private static class ID2D1TransformMapperInternalMethods
    {
        /// <inheritdoc cref="D2D1TransformMapperImpl.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(D2D1TransformMapperImpl* @this, Guid* riid, void** ppvObject)
        {
            @this = (D2D1TransformMapperImpl*)&((void**)@this)[-1];

            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="D2D1TransformMapperImpl.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(D2D1TransformMapperImpl* @this)
        {
            @this = (D2D1TransformMapperImpl*)&((void**)@this)[-1];

            return @this->AddRef();
        }

        /// <inheritdoc cref="D2D1TransformMapperImpl.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(D2D1TransformMapperImpl* @this)
        {
            @this = (D2D1TransformMapperImpl*)&((void**)@this)[-1];

            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1TransformMapperInternal.GetManagedWrapperHandle"/>
        [UnmanagedCallersOnly]
        public static int GetManagedWrapperHandle(D2D1TransformMapperImpl* @this, void** ppvHandle)
        {
            @this = (D2D1TransformMapperImpl*)&((void**)@this)[-1];

            if (ppvHandle is null)
            {
                return E.E_POINTER;
            }

            *ppvHandle = (void*)GCHandle.ToIntPtr(@this->transformMapperHandle);

            return S.S_OK;
        }
    }
}