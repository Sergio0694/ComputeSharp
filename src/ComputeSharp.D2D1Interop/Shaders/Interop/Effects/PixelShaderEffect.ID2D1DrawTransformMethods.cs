using System;
using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1Interop.Tests.Helpers;

/// <inheritdoc/>
partial struct PixelShaderEffect
{
    /// <summary>
    /// The implementation for <see cref="ID2D1DrawTransform"/>.
    /// </summary>
    private unsafe static class ID2D1DrawTransformMethods
    {
        /// <inheritdoc cref="ID2D1DrawTransform.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(PixelShaderEffect* @this, Guid* riid, void** ppvObject)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="ID2D1DrawTransform.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(PixelShaderEffect* @this)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            return @this->AddRef();
        }

        /// <inheritdoc cref="ID2D1DrawTransform.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(PixelShaderEffect* @this)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1DrawTransform.GetInputCount"/>
        [UnmanagedCallersOnly]
        public static uint GetInputCount(PixelShaderEffect* @this)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            return (uint)@this->numberOfInputs;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapOutputRectToInputRects"/>
        [UnmanagedCallersOnly]
        public static int MapOutputRectToInputRects(PixelShaderEffect* @this, RECT* outputRect, RECT* inputRects, uint inputRectsCount)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            if (inputRectsCount != 1)
            {
                return E.E_INVALIDARG;
            }

            inputRects[0] = *outputRect;

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInputRectsToOutputRect"/>
        [UnmanagedCallersOnly]
        public static int MapInputRectsToOutputRect(PixelShaderEffect* @this, RECT* inputRects, RECT* inputOpaqueSubRects, uint inputRectCount, RECT* outputRect, RECT* outputOpaqueSubRect)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            if (inputRectCount != 1)
            {
                return E.E_INVALIDARG;
            }

            *outputRect = inputRects[0];

            @this->inputRect = inputRects[0];

            *outputOpaqueSubRect = default;

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInvalidRect"/>
        [UnmanagedCallersOnly]
        public static int MapInvalidRect(PixelShaderEffect* @this, uint inputIndex, RECT invalidInputRect, RECT* invalidOutputRect)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            *invalidOutputRect = @this->inputRect;

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.SetDrawInfo"/>
        [UnmanagedCallersOnly]
        public static int SetDrawInfo(PixelShaderEffect* @this, ID2D1DrawInfo* drawInfo)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            @this->d2D1DrawInfo = drawInfo;

            return drawInfo->SetPixelShader(&@this->shaderId);
        }
    }
}