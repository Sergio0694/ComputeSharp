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

            return (uint)@this->inputCount;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapOutputRectToInputRects"/>
        [UnmanagedCallersOnly]
        public static int MapOutputRectToInputRects(PixelShaderEffect* @this, RECT* outputRect, RECT* inputRects, uint inputRectsCount)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            if (inputRectsCount != @this->inputCount)
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

                // Invoke MapOutputToInputs and handle exceptions so they don't cross the ABI boundary
                try
                {
                    d2D1TransformMapper.MapOutputToInputs(in output, inputs);
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
                // If no custom transform is used, apply the default mapping. In this case, the loop will
                // automatically handle cases where no inputs are defined. If inputs are present instead,
                // the default mapping will set the input rect for a simple input to be the same as the
                // output rect, and the input rect for a complex rect to be infinite. In both cases, D2D
                // will handle clipping the inputs to the actual output rect area, if needed.
                for (uint i = 0; i < inputRectsCount; i++)
                {
                    switch (@this->inputTypes[i])
                    {
                        case D2D1PixelShaderInputType.Simple:
                            inputRects[i] = *outputRect;
                            break;
                        case D2D1PixelShaderInputType.Complex:
                            inputRects[i].MakeD2D1Infinite();
                            break;
                        default:
                            return E.E_FAIL;
                    }
                }
            }

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInputRectsToOutputRect"/>
        [UnmanagedCallersOnly]
        public static int MapInputRectsToOutputRect(PixelShaderEffect* @this, RECT* inputRects, RECT* inputOpaqueSubRects, uint inputRectCount, RECT* outputRect, RECT* outputOpaqueSubRect)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            if (inputRectCount != @this->inputCount)
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
                // If there are no inputs, make the output rectangle infinite. This is useful
                // to make output-only effects work fine by default, without needing a custom
                // transform. In this case, the target area will be the output node anyway.
                outputRect->MakeD2D1Infinite();

                *outputOpaqueSubRect = default;
            }
            else
            {
                // If at least one input is present and no custom mapper is available, apply the default
                // mapping. In this case, the output rect should be the union of the input rects for all
                // the simple shader inputs, or infinite if there are no simple inputs. The input rects
                // for complex inputs, if present, are not needed in this scenario, so they're ignored.
                RECT? unionOfSimpleRects = null;

                for (uint i = 0; i < inputRectCount; i++)
                {
                    if (@this->inputTypes[i] == D2D1PixelShaderInputType.Simple)
                    {
                        unionOfSimpleRects = (unionOfSimpleRects ?? default).Union(inputRects[i]);
                    }
                }

                if (unionOfSimpleRects.HasValue)
                {
                    *outputRect = unionOfSimpleRects.Value;
                }
                else
                {
                    outputRect->MakeD2D1Infinite();
                }

                *outputOpaqueSubRect = default;
            }

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInvalidRect"/>
        [UnmanagedCallersOnly]
        public static int MapInvalidRect(PixelShaderEffect* @this, uint inputIndex, RECT invalidInputRect, RECT* invalidOutputRect)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            if (inputIndex >= (uint)@this->inputCount)
            {
                return E.E_INVALIDARG;
            }

            if (@this->d2D1TransformMapperHandle.Target is D2D1TransformMapper d2D1TransformMapper)
            {
                Rectangle invalidInput = invalidInputRect.ToRectangle();
                Rectangle invalidOutput;

                // Handle exceptions once again
                try
                {
                    d2D1TransformMapper.MapInvalidOutput((int)inputIndex, invalidInput, out invalidOutput);
                }
                catch (Exception e)
                {
                    return e.HResult;
                }

                *invalidOutputRect = invalidOutput.ToRECT();
            }
            else
            {
                // The default mapping in this scenario just needs to set the invalid output rect for simple inputs to
                // be the invalid input rect, and to set the invalid output rect for complex inputs to an infinite rect.
                switch (@this->inputTypes[inputIndex])
                {
                    case D2D1PixelShaderInputType.Simple:
                        *invalidOutputRect = invalidInputRect;
                        break;
                    case D2D1PixelShaderInputType.Complex:
                        invalidOutputRect->MakeD2D1Infinite();
                        break;
                    default:
                        return E.E_FAIL;
                }
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