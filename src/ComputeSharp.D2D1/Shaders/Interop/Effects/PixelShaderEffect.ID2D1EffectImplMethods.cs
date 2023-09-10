using System;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Shaders.Interop.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
unsafe partial struct PixelShaderEffect
{
    /// <summary>
    /// The implementation for <see cref="ID2D1EffectImpl"/>.
    /// </summary>
    private static class ID2D1EffectImplMethods
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="Initialize"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int InitializeDelegate(PixelShaderEffect* @this, ID2D1EffectContext* effectContext, ID2D1TransformGraph* transformGraph);

        /// <inheritdoc cref="PrepareForRender"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int PrepareForRenderDelegate(PixelShaderEffect* @this, D2D1_CHANGE_TYPE changeType);

        /// <inheritdoc cref="SetGraph"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int SetGraphDelegate(PixelShaderEffect* @this, ID2D1TransformGraph* transformGraph);

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
        public static int QueryInterface(PixelShaderEffect* @this, Guid* riid, void** ppvObject)
        {
            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="ID2D1EffectImpl.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(PixelShaderEffect* @this)
        {
            return @this->AddRef();
        }

        /// <inheritdoc cref="ID2D1EffectImpl.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(PixelShaderEffect* @this)
        {
            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1EffectImpl.Initialize"/>
        [UnmanagedCallersOnly]
        public static int Initialize(PixelShaderEffect* @this, ID2D1EffectContext* effectContext, ID2D1TransformGraph* transformGraph)
        {
            int hresult = effectContext->LoadPixelShader(
                shaderId: &@this->shaderId,
                shaderBuffer: @this->bytecode,
                shaderBufferCount: (uint)@this->bytecodeSize);

            // If E_INVALIDARG was returned, try to check whether double precision support was requested when not available. This
            // is only done to provide a more helpful error message to callers. If no error was returned, the behavior is the same.
            // If any error is detected while trying to check for shader support, ignore the value and propagate the current HRESULT.
            if (hresult == E.E_INVALIDARG && effectContext->IsShaderSupported(@this->bytecode, @this->bytecodeSize) == S.S_FALSE)
            {
                hresult = D2DERR.D2DERR_INSUFFICIENT_DEVICE_CAPABILITIES;
            }

            // If loading the bytecode succeeded, set the transform node
            if (Windows.SUCCEEDED(hresult))
            {
                hresult = transformGraph->SetSingleTransformNode((ID2D1TransformNode*)&@this->lpVtblForID2D1DrawTransform);
            }

            // If the transform node was set, also store the effect context
            if (Windows.SUCCEEDED(hresult))
            {
                _ = effectContext->AddRef();

                @this->d2D1EffectContext = effectContext;
            }

            return hresult;
        }

        /// <inheritdoc cref="ID2D1EffectImpl.PrepareForRender"/>
        [UnmanagedCallersOnly]
        public static int PrepareForRender(PixelShaderEffect* @this, D2D1_CHANGE_TYPE changeType)
        {
            // Validate the constant buffer
            if (@this->constantBufferSize > 0 &&
                @this->constantBuffer is null)
            {
                return E.E_NOT_VALID_STATE;
            }

            int hresult = S.S_OK;

            // First, set the constant buffer, if available
            if (@this->constantBuffer is not null)
            {
                hresult = @this->d2D1DrawInfo->SetPixelShaderConstantBuffer(
                    buffer: @this->constantBuffer,
                    bufferCount: (uint)@this->constantBufferSize);
            }

            // Set any resource texture managers, if needed
            if (Windows.SUCCEEDED(hresult))
            {
                D2D1ShaderEffect.SetResourceTextureManagers(
                    @this->resourceTextureDescriptionCount,
                    @this->resourceTextureDescriptions,
                    ref @this->resourceTextureManagerBuffer,
                    @this->d2D1DrawInfo,
#if NET6_0_OR_GREATER
                    &SetResourceTextureForD2D1DrawInfo,
#else
                    (delegate*<void*, uint, ID2D1ResourceTexture*, int>)&SetResourceTextureForD2D1DrawInfo,
#endif
                    ref hresult);
            }

            return hresult;
        }

        /// <inheritdoc cref="ID2D1EffectImpl.SetGraph"/>
        [UnmanagedCallersOnly]
        public static int SetGraph(PixelShaderEffect* @this, ID2D1TransformGraph* transformGraph)
        {
            return E.E_NOTIMPL;
        }

        /// <inheritdoc cref="ID2D1DrawInfo.SetResourceTexture"/>
        private static int SetResourceTextureForD2D1DrawInfo(
            void* d2D1DrawInfo,
            uint textureIndex,
            ID2D1ResourceTexture* resourceTexture)
        {
            return ((ID2D1DrawInfo*)d2D1DrawInfo)->SetResourceTexture(
                textureIndex: textureIndex,
                resourceTexture: resourceTexture);
        }
    }
}