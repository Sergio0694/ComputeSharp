using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
partial struct PixelShaderEffect
{
    /// <summary>
    /// The implementation for <see cref="ID2D1DrawTransform"/>.
    /// </summary>
    private static unsafe class ID2D1DrawTransformMethods
    {
        /// <inheritdoc cref="ID2D1DrawTransform.QueryInterface"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int QueryInterface(PixelShaderEffect* @this, Guid* riid, void** ppvObject)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="ID2D1DrawTransform.AddRef"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static uint AddRef(PixelShaderEffect* @this)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            return @this->AddRef();
        }

        /// <inheritdoc cref="ID2D1DrawTransform.Release"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static uint Release(PixelShaderEffect* @this)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1DrawTransform.GetInputCount"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static uint GetInputCount(PixelShaderEffect* @this)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            try
            {
                return (uint)@this->GetGlobals().InputCount;
            }
            catch
            {
                // GetInputCount doesn't respect COM, so it does not return an HRESULT.
                // The only thing we can do in this case is just to return 0 instead.
                return 0;
            }
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapOutputRectToInputRects"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int MapOutputRectToInputRects(PixelShaderEffect* @this, RECT* outputRect, RECT* inputRects, uint inputRectsCount)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            try
            {
                default(ArgumentOutOfRangeException).ThrowIfNotEqual((int)inputRectsCount, @this->GetGlobals().InputCount, nameof(inputRectsCount));

                if (@this->d2D1TransformMapper.Get() is not null)
                {
                    // Forward to the current ID2D1DrawTransformMapper instance
                    @this->d2D1TransformMapper.Get()->MapOutputRectToInputRects(outputRect, inputRects, inputRectsCount).Assert();
                }
                else
                {
                    ReadOnlySpan<D2D1PixelShaderInputType> inputTypes = @this->GetGlobals().InputTypes.Span;

                    // If no custom transform is used, apply the default mapping. In this case, the loop will
                    // automatically handle cases where no inputs are defined. If inputs are present instead,
                    // the default mapping will set the input rect for a simple input to be the same as the
                    // output rect, and the input rect for a complex rect to be infinite. In both cases, D2D
                    // will handle clipping the inputs to the actual output rect area, if needed.
                    for (uint i = 0; i < inputRectsCount; i++)
                    {
                        switch (inputTypes[(int)i])
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
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInputRectsToOutputRect"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int MapInputRectsToOutputRect(PixelShaderEffect* @this, RECT* inputRects, RECT* inputOpaqueSubRects, uint inputRectCount, RECT* outputRect, RECT* outputOpaqueSubRect)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            try
            {
                default(ArgumentOutOfRangeException).ThrowIfNotEqual((int)inputRectCount, @this->GetGlobals().InputCount, nameof(inputRectCount));

                if (@this->d2D1TransformMapper.Get() is not null)
                {
                    using ComPtr<D2D1DrawInfoUpdateContextImpl> d2D1DrawInfoUpdateContext = default;

                    // Create an ID2D1DrawInfoUpdateContext instance
                    D2D1DrawInfoUpdateContextImpl.Factory(
                        drawInfoUpdateContext: d2D1DrawInfoUpdateContext.GetAddressOf(),
                        constantBuffer: @this->constantBuffer,
                        constantBufferSize: @this->GetGlobals().ConstantBufferSize,
                        d2D1DrawInfo: @this->d2D1DrawInfo.Get()).Assert();

                    // Forward the call to the input ID2D1DrawTransformMapper instance
                    HRESULT hresult = @this->d2D1TransformMapper.Get()->MapInputRectsToOutputRect(
                        updateContext: (ID2D1DrawInfoUpdateContext*)d2D1DrawInfoUpdateContext.Get(),
                        inputRects: inputRects,
                        inputOpaqueSubRects: inputOpaqueSubRects,
                        inputRectCount: inputRectCount,
                        outputRect: outputRect,
                        outputOpaqueSubRect: outputOpaqueSubRect);

                    // Regardless of the operation result, always invalidate the context
                    d2D1DrawInfoUpdateContext.Get()->Close().Assert();

                    // Now we can validate that the call was in fact successful
                    hresult.Assert();
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
                    ReadOnlySpan<D2D1PixelShaderInputType> inputTypes = @this->GetGlobals().InputTypes.Span;

                    // If at least one input is present and no custom mapper is available, apply the default
                    // mapping. In this case, the output rect should be the union of the input rects for all
                    // the simple shader inputs, or infinite if there are no simple inputs. The input rects
                    // for complex inputs, if present, are not needed in this scenario, so they're ignored.
                    RECT unionOfSimpleRects = default;
                    bool isUnionOfSimpleRectsEmpty = true;

                    for (uint i = 0; i < inputRectCount; i++)
                    {
                        if (inputTypes[(int)i] == D2D1PixelShaderInputType.Simple)
                        {
                            if (isUnionOfSimpleRectsEmpty)
                            {
                                unionOfSimpleRects = inputRects[i];
                                isUnionOfSimpleRectsEmpty = false;
                            }
                            else
                            {
                                unionOfSimpleRects = unionOfSimpleRects.Union(inputRects[i]);
                            }
                        }
                    }

                    if (isUnionOfSimpleRectsEmpty)
                    {
                        outputRect->MakeD2D1Infinite();
                    }
                    else
                    {
                        *outputRect = unionOfSimpleRects;
                    }

                    *outputOpaqueSubRect = default;
                }

                return S.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInvalidRect"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int MapInvalidRect(PixelShaderEffect* @this, uint inputIndex, RECT invalidInputRect, RECT* invalidOutputRect)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            try
            {
                default(ArgumentOutOfRangeException).ThrowIfGreaterThanOrEqual((int)inputIndex, @this->GetGlobals().InputCount, nameof(inputIndex));

                if (@this->d2D1TransformMapper.Get() is not null)
                {
                    // Forward to the current ID2D1DrawTransformMapper instance
                    @this->d2D1TransformMapper.Get()->MapInvalidRect(inputIndex, invalidInputRect, invalidOutputRect).Assert();
                }
                else
                {
                    // The default mapping in this scenario just needs to set the invalid output rect for simple inputs to
                    // be the invalid input rect, and to set the invalid output rect for complex inputs to an infinite rect.
                    switch (@this->GetGlobals().InputTypes.Span[(int)inputIndex])
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
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }

        /// <inheritdoc cref="ID2D1DrawTransform.SetDrawInfo"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int SetDrawInfo(PixelShaderEffect* @this, ID2D1DrawInfo* drawInfo)
        {
            @this = (PixelShaderEffect*)&((void**)@this)[-1];

            try
            {
                // Store the new ID2D1DrawInfo object
                @this->d2D1DrawInfo.Attach(new ComPtr<ID2D1DrawInfo>(drawInfo).Get());

                Guid shaderId = @this->GetGlobals().EffectId;
                D2D1PixelOptions pixelOptions = @this->GetGlobals().PixelOptions;

                // Set the pixel shader for the effect
                drawInfo->SetPixelShader(&shaderId, (D2D1_PIXEL_OPTIONS)pixelOptions).Assert();

                // If any input descriptions are present, set them
                foreach (ref readonly D2D1InputDescription inputDescription in @this->GetGlobals().InputDescriptions.Span)
                {
                    D2D1_INPUT_DESCRIPTION d2D1InputDescription;
                    d2D1InputDescription.filter = (D2D1_FILTER)inputDescription.Filter;
                    d2D1InputDescription.levelOfDetailCount = (uint)inputDescription.LevelOfDetailCount;

                    drawInfo->SetInputDescription((uint)inputDescription.Index, d2D1InputDescription).Assert();
                }

                D2D1BufferPrecision bufferPrecision = @this->GetGlobals().BufferPrecision;
                D2D1ChannelDepth channelDepth = @this->GetGlobals().ChannelDepth;

                // If a custom buffer precision or channel depth is requested, set it
                if (bufferPrecision != D2D1BufferPrecision.Unknown ||
                    channelDepth != D2D1ChannelDepth.Default)
                {
                    drawInfo->SetOutputBuffer(
                        (D2D1_BUFFER_PRECISION)bufferPrecision,
                        (D2D1_CHANNEL_DEPTH)channelDepth).Assert();
                }

                return S.S_OK;
            }
            catch (Exception e)
            {
                return Marshal.GetHRForException(e);
            }
        }
    }
}