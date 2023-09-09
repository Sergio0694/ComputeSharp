using System;
using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
unsafe partial struct ComputeShaderEffect
{
    /// <summary>
    /// The implementation for <see cref="ID2D1EffectImpl"/>.
    /// </summary>
    private static class ID2D1EffectImplMethods
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="Initialize"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int InitializeDelegate(ComputeShaderEffect* @this, ID2D1EffectContext* effectContext, ID2D1TransformGraph* transformGraph);

        /// <inheritdoc cref="PrepareForRender"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int PrepareForRenderDelegate(ComputeShaderEffect* @this, D2D1_CHANGE_TYPE changeType);

        /// <inheritdoc cref="SetGraph"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int SetGraphDelegate(ComputeShaderEffect* @this, ID2D1TransformGraph* transformGraph);

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
        /// A cached <see cref="InitializeDelegate"/> instance wrapping <see cref="Initialize"/>.
        /// </summary>
        public static readonly InitializeDelegate InitializeWrapper = Initialize;

        /// <summary>
        /// A cached <see cref="PrepareForRenderDelegate"/> instance wrapping <see cref="PrepareForRender"/>.
        /// </summary>
        public static readonly PrepareForRenderDelegate PrepareForRenderWrapper = PrepareForRender;

        /// <summary>
        /// A cached <see cref="SetGraphDelegate"/> instance wrapping <see cref="SetGraph"/>.
        /// </summary>
        public static readonly SetGraphDelegate SetGraphWrapper = SetGraph;
#endif

        /// <inheritdoc cref="ID2D1EffectImpl.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(ComputeShaderEffect* @this, Guid* riid, void** ppvObject)
        {
            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="ID2D1EffectImpl.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(ComputeShaderEffect* @this)
        {
            return @this->AddRef();
        }

        /// <inheritdoc cref="ID2D1EffectImpl.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(ComputeShaderEffect* @this)
        {
            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1EffectImpl.Initialize"/>
        [UnmanagedCallersOnly]
        public static int Initialize(ComputeShaderEffect* @this, ID2D1EffectContext* effectContext, ID2D1TransformGraph* transformGraph)
        {
            // TODO

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1EffectImpl.PrepareForRender"/>
        [UnmanagedCallersOnly]
        public static int PrepareForRender(ComputeShaderEffect* @this, D2D1_CHANGE_TYPE changeType)
        {
            // TODO

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1EffectImpl.SetGraph"/>
        [UnmanagedCallersOnly]
        public static int SetGraph(ComputeShaderEffect* @this, ID2D1TransformGraph* transformGraph)
        {
            return E.E_NOTIMPL;
        }
    }
}