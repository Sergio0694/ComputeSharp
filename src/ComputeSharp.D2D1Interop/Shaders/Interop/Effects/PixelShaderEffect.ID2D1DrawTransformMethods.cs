using System;
using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1Interop.Interop.Effects;

/// <inheritdoc/>
partial struct PixelShaderEffect
{
    /// <summary>
    /// The implementation for <see cref="ID2D1DrawTransform"/>.
    /// </summary>
    private unsafe static class ID2D1DrawTransformMethods
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="GetInputCount"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate uint GetInputCountDelegate(PixelShaderEffect* @this);

        /// <inheritdoc cref="MapOutputRectToInputRects"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int MapOutputRectToInputRectsDelegate(PixelShaderEffect* @this, RECT* outputRect, RECT* inputRects, uint inputRectsCount);

        /// <inheritdoc cref="MapInputRectsToOutputRect"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int MapInputRectsToOutputRectDelegate(PixelShaderEffect* @this, RECT* inputRects, RECT* inputOpaqueSubRects, uint inputRectCount, RECT* outputRect, RECT* outputOpaqueSubRect);

        /// <inheritdoc cref="MapInvalidRect"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int MapInvalidRectDelegate(PixelShaderEffect* @this, uint inputIndex, RECT invalidInputRect, RECT* invalidOutputRect);

        /// <inheritdoc cref="SetDrawInfo"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int SetDrawInfoDelegate(PixelShaderEffect* @this, ID2D1DrawInfo* drawInfo);

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
        /// A cached <see cref="GetInputCountDelegate"/> instance wrapping <see cref="GetInputCount"/>.
        /// </summary>
        public static readonly GetInputCountDelegate GetInputCountWrapper = GetInputCount;

        /// <summary>
        /// A cached <see cref="MapOutputRectToInputRectsDelegate"/> instance wrapping <see cref="MapOutputRectToInputRects"/>.
        /// </summary>
        public static readonly MapOutputRectToInputRectsDelegate MapOutputRectToInputRectsWrapper = MapOutputRectToInputRects;

        /// <summary>
        /// A cached <see cref="MapInputRectsToOutputRectDelegate"/> instance wrapping <see cref="MapInputRectsToOutputRect"/>.
        /// </summary>
        public static readonly MapInputRectsToOutputRectDelegate MapInputRectsToOutputRectWrapper = MapInputRectsToOutputRect;

        /// <summary>
        /// A cached <see cref="MapInvalidRectDelegate"/> instance wrapping <see cref="MapInvalidRect"/>.
        /// </summary>
        public static readonly MapInvalidRectDelegate MapInvalidRectWrapper = MapInvalidRect;

        /// <summary>
        /// A cached <see cref="SetDrawInfoDelegate"/> instance wrapping <see cref="SetDrawInfo"/>.
        /// </summary>
        public static readonly SetDrawInfoDelegate SetDrawInfoWrapper = SetDrawInfo;
#endif

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

            if (inputRectsCount != @this->numberOfInputs)
            {
                return E.E_INVALIDARG;
            }

            for (int i = 0; i < @this->numberOfInputs; i++)
            {
                inputRects[i] = *outputRect;
            }

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInputRectsToOutputRect"/>
        [UnmanagedCallersOnly]
        public static int MapInputRectsToOutputRect(PixelShaderEffect* @this, RECT* inputRects, RECT* inputOpaqueSubRects, uint inputRectCount, RECT* outputRect, RECT* outputOpaqueSubRect)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            if (inputRectCount != @this->numberOfInputs)
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