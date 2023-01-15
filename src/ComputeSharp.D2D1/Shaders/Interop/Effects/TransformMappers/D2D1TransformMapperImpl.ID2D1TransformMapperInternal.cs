using System;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <inheritdoc/>
unsafe partial struct D2D1TransformMapperImpl
{
    /// <summary>
    /// The implementation for <see cref="ID2D1TransformMapperInternal"/>.
    /// </summary>
    private static class ID2D1TransformMapperInternalMethods
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="ID2D1TransformMapperInternal.GetManagedWrapperHandle"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int GetManagedWrapperHandleDelegate(D2D1TransformMapperImpl* @this, void** ppvHandle);

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
        /// A cached <see cref="GetManagedWrapperHandleDelegate"/> instance wrapping <see cref="GetManagedWrapperHandle"/>.
        /// </summary>
        public static readonly GetManagedWrapperHandleDelegate GetManagedWrapperHandleWrapper = GetManagedWrapperHandle;
#endif

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