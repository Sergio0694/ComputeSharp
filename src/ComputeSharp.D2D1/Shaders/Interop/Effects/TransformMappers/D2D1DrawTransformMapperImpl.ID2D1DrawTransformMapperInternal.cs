using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <inheritdoc/>
unsafe partial struct D2D1DrawTransformMapperImpl
{
    /// <summary>
    /// The implementation for <see cref="ID2D1DrawTransformMapperInternal"/>.
    /// </summary>
    private static class ID2D1DrawTransformMapperInternalMethods
    {
        /// <inheritdoc cref="D2D1DrawTransformMapperImpl.QueryInterface"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int QueryInterface(D2D1DrawTransformMapperImpl* @this, Guid* riid, void** ppvObject)
        {
            @this = (D2D1DrawTransformMapperImpl*)&((void**)@this)[-1];

            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="D2D1DrawTransformMapperImpl.AddRef"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static uint AddRef(D2D1DrawTransformMapperImpl* @this)
        {
            @this = (D2D1DrawTransformMapperImpl*)&((void**)@this)[-1];

            return @this->AddRef();
        }

        /// <inheritdoc cref="D2D1DrawTransformMapperImpl.Release"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static uint Release(D2D1DrawTransformMapperImpl* @this)
        {
            @this = (D2D1DrawTransformMapperImpl*)&((void**)@this)[-1];

            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1DrawTransformMapperInternal.GetManagedWrapperHandle"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int GetManagedWrapperHandle(D2D1DrawTransformMapperImpl* @this, void** ppvHandle)
        {
            @this = (D2D1DrawTransformMapperImpl*)&((void**)@this)[-1];

            if (ppvHandle is null)
            {
                return E.E_POINTER;
            }

            *ppvHandle = (void*)GCHandle.ToIntPtr(@this->transformMapperHandle);

            return S.S_OK;
        }
    }
}