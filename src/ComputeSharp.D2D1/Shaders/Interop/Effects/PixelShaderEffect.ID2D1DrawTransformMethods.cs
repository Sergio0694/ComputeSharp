using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1.Interop.Effects;

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

            if (@this->d2D1TransformMapperHandle.Target is D2D1TransformMapper d2D1TransformMapper)
            {
                Rectangle output = outputRect->ToRectangle();
                Span<Rectangle> inputs = stackalloc Rectangle[8].Slice(0, (int)inputRectsCount);

                for (int i = 0; i < (int)inputRectsCount; i++)
                {
                    inputs[i] = inputRects[i].ToRectangle();
                }

                ReadOnlySpan<byte> buffer = new(@this->constantBuffer, @this->constantBufferSize);

                // Invoke MapOutputToInputs and handle exceptions so they don't cross the ABI boundary
                try
                {
                    d2D1TransformMapper.MapOutputToInputs(buffer, in output, inputs);
                }
                catch (Exception e)
                {
                    return e.HResult;
                }

                for (int i = 0; i < (int)inputRectsCount; i++)
                {
                    inputRects[i] = inputs[i].ToRECT();
                }
            }
            else
            {
                // Default mapping
                for (int i = 0; i < (int)inputRectsCount; i++)
                {
                    inputRects[i] = *outputRect;
                }
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

            if (@this->d2D1TransformMapperHandle.Target is D2D1TransformMapper d2D1TransformMapper)
            {
                Span<Rectangle> inputs = stackalloc Rectangle[8].Slice(0, (int)inputRectCount);
                Span<Rectangle> opaqueInputs = stackalloc Rectangle[8].Slice(0, (int)inputRectCount);

                for (int i = 0; i < (int)inputRectCount; i++)
                {
                    inputs[i] = inputRects[i].ToRectangle();
                    opaqueInputs[i] = inputOpaqueSubRects[i].ToRectangle();
                }

                ReadOnlySpan<byte> buffer = new(@this->constantBuffer, @this->constantBufferSize);

                Rectangle output;
                Rectangle opaqueOutput;

                // Handle exceptions, as mentioned above
                try
                {
                    d2D1TransformMapper.MapInputsToOutput(buffer, inputs, opaqueInputs, out output, out opaqueOutput);
                }
                catch (Exception e)
                {
                    return e.HResult;
                }

                *outputRect = output.ToRECT();
                *outputOpaqueSubRect = opaqueOutput.ToRECT();
            }
            else if (inputRectCount == 0)
            {
                // If there are no inputs, just ignore (users will need a custom mapping)
                *outputRect = default;

                @this->inputRect = default;

                *outputOpaqueSubRect = default;
            }
            else
            {
                // Default mapping
                *outputRect = inputRects[0];

                @this->inputRect = inputRects[0];

                *outputOpaqueSubRect = default;
            }

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInvalidRect"/>
        [UnmanagedCallersOnly]
        public static int MapInvalidRect(PixelShaderEffect* @this, uint inputIndex, RECT invalidInputRect, RECT* invalidOutputRect)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            if (@this->d2D1TransformMapperHandle.Target is D2D1TransformMapper d2D1TransformMapper)
            {
                Rectangle invalidInput = invalidInputRect.ToRectangle();
                Rectangle invalidOutput;

                ReadOnlySpan<byte> buffer = new(@this->constantBuffer, @this->constantBufferSize);

                // Handle exceptions once again
                try
                {
                    d2D1TransformMapper.MapInvalidOutput(buffer, (int)inputIndex, invalidInput, out invalidOutput);
                }
                catch (Exception e)
                {
                    return e.HResult;
                }

                *invalidOutputRect = invalidOutput.ToRECT();
            }
            else
            {
                // Default mapping
                *invalidOutputRect = @this->inputRect;
            }

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