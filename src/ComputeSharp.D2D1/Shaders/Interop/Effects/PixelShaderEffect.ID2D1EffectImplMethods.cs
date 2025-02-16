using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Interop.Extensions;
using ComputeSharp.Win32;

#pragma warning disable IDE0060

namespace ComputeSharp.D2D1.Interop.Effects;

/// <summary>
/// A simple <see cref="ID2D1EffectImpl"/> and <see cref="ID2D1DrawTransform"/> implementation for a given pixel shader.
/// </summary>
internal unsafe partial struct PixelShaderEffect
{
    /// <summary>
    /// The implementation for <see cref="ID2D1EffectImpl"/>.
    /// </summary>
    private static class ID2D1EffectImplMethods
    {
        /// <inheritdoc cref="ID2D1EffectImpl.QueryInterface"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int QueryInterface(PixelShaderEffect* @this, Guid* riid, void** ppvObject)
        {
            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="ID2D1EffectImpl.AddRef"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static uint AddRef(PixelShaderEffect* @this)
        {
            return @this->AddRef();
        }

        /// <inheritdoc cref="ID2D1EffectImpl.Release"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static uint Release(PixelShaderEffect* @this)
        {
            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1EffectImpl.Initialize"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int Initialize(PixelShaderEffect* @this, ID2D1EffectContext* effectContext, ID2D1TransformGraph* transformGraph)
        {
            try
            {
                ReadOnlySpan<byte> bytecode = @this->GetGlobals().HlslBytecode.Span;

                fixed (Guid* pShaderId = &@this->GetGlobals().EffectId)
                fixed (byte* pBytecode = bytecode)
                {
                    HRESULT hresult = effectContext->LoadPixelShader(
                        shaderId: pShaderId,
                        shaderBuffer: pBytecode,
                        shaderBufferCount: (uint)bytecode.Length);

                    // If E_INVALIDARG was returned, try to check whether double precision support was requested when not available. This
                    // is only done to provide a more helpful error message to callers. If no error was returned, the behavior is the same.
                    // If any error is detected while trying to check for shader support, ignore the value and propagate the current HRESULT.
                    if (hresult == E.E_INVALIDARG && effectContext->IsShaderSupported(pBytecode, bytecode.Length) == S.S_FALSE)
                    {
                        hresult = D2DERR.D2DERR_INSUFFICIENT_DEVICE_CAPABILITIES;
                    }

                    // Finally, assert that we did load the pixel shader correctly
                    hresult.Assert();
                }

                // If loading the bytecode succeeded, set the transform node
                transformGraph->SetSingleTransformNode((ID2D1TransformNode*)&@this->lpVtblForID2D1DrawTransform).Assert();

                // Store the new ID2D1EffectContext object
                @this->d2D1EffectContext.Attach(new ComPtr<ID2D1EffectContext>(effectContext).Get());

                return S.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        /// <inheritdoc cref="ID2D1EffectImpl.PrepareForRender"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int PrepareForRender(PixelShaderEffect* @this, D2D1_CHANGE_TYPE changeType)
        {
            try
            {
                // Validate the constant buffer. We either must have a stateless shader, in which case
                // the constant buffer might either be null or empty (both cases are allowed), or we
                // must have a shader with some state and a constant buffer being set. In that case
                // the code setting the constant buffer will have already validated its length.
                if (@this->GetGlobals().ConstantBufferSize > 0 &&
                    @this->constantBuffer is null)
                {
                    return E.E_NOT_VALID_STATE;
                }

                // First, set the constant buffer, if available
                if (@this->constantBuffer is not null)
                {
                    @this->d2D1DrawInfo.Get()->SetPixelShaderConstantBuffer(
                        buffer: @this->constantBuffer,
                        bufferCount: (uint)@this->GetGlobals().ConstantBufferSize).Assert();
                }

                ReadOnlySpan<D2D1ResourceTextureDescription> resourceTextureDescriptions = @this->GetGlobals().ResourceTextureDescriptions.Span;

                for (int i = 0; i < resourceTextureDescriptions.Length; i++)
                {
                    ref readonly ComPtr<ID2D1ResourceTextureManager> resourceTextureManager = ref @this->resourceTextureManagerBuffer[i];

                    // If the current resource texture manager is not set, we cannot render, as there's an unbound resource texture
                    if (resourceTextureManager.Get() is null)
                    {
                        return E.E_NOT_VALID_STATE;
                    }

                    using ComPtr<ID2D1ResourceTextureManagerInternal> resourceTextureManagerInternal = default;

                    // Get the ID2D1ResourceTextureManagerInternal object. This cast should always succeed, as when
                    // an input resource texture managers is set it's also checked for ID2D1ResourceTextureManagerInternal,
                    // but still validate for good measure.
                    resourceTextureManager.CopyTo(resourceTextureManagerInternal.GetAddressOf()).Assert();

                    using ComPtr<ID2D1ResourceTexture> d2D1ResourceTexture = default;

                    // Try to get the ID2D1ResourceTexture from the manager
                    resourceTextureManagerInternal.Get()->GetResourceTexture(d2D1ResourceTexture.GetAddressOf()).Assert();

                    ref readonly D2D1ResourceTextureDescription resourceTextureDescription = ref resourceTextureDescriptions[i];

                    // Set the ID2D1ResourceTexture object to the current index in the ID2D1DrawInfo object in use
                    @this->d2D1DrawInfo.Get()->SetResourceTexture(
                        textureIndex: (uint)resourceTextureDescription.Index,
                        resourceTexture: d2D1ResourceTexture.Get()).Assert();
                }

                return S.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        /// <inheritdoc cref="ID2D1EffectImpl.SetGraph"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int SetGraph(PixelShaderEffect* @this, ID2D1TransformGraph* transformGraph)
        {
            return E.E_NOTIMPL;
        }
    }
}