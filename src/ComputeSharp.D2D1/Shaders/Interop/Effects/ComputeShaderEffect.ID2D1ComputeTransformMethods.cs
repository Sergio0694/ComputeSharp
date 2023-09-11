using System;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
partial struct ComputeShaderEffect
{
    /// <summary>
    /// The implementation for <see cref="ID2D1ComputeTransform"/>.
    /// </summary>
    private static unsafe class ID2D1ComputeTransformMethods
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="GetInputCount"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate uint GetInputCountDelegate(ComputeShaderEffect* @this);

        /// <inheritdoc cref="MapOutputRectToInputRects"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int MapOutputRectToInputRectsDelegate(ComputeShaderEffect* @this, RECT* outputRect, RECT* inputRects, uint inputRectsCount);

        /// <inheritdoc cref="MapInputRectsToOutputRect"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int MapInputRectsToOutputRectDelegate(ComputeShaderEffect* @this, RECT* inputRects, RECT* inputOpaqueSubRects, uint inputRectCount, RECT* outputRect, RECT* outputOpaqueSubRect);

        /// <inheritdoc cref="MapInvalidRect"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int MapInvalidRectDelegate(ComputeShaderEffect* @this, uint inputIndex, RECT invalidInputRect, RECT* invalidOutputRect);

        /// <inheritdoc cref="SetComputeInfo"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int SetComputeInfoDelegate(ComputeShaderEffect* @this, ID2D1ComputeInfo* computeInfo);

        /// <inheritdoc cref="CalculateThreadgroups"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int CalculateThreadgroupsDelegate(ComputeShaderEffect* @this, RECT* outputRect, uint* dimensionX, uint* dimensionY, uint* dimensionZ);

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
        /// A cached <see cref="SetComputeInfoDelegate"/> instance wrapping <see cref="SetComputeInfo"/>.
        /// </summary>
        public static readonly SetComputeInfoDelegate SetComputeInfoWrapper = SetComputeInfo;

        /// <summary>
        /// A cached <see cref="CalculateThreadgroupsDelegate"/> instance wrapping <see cref="CalculateThreadgroups"/>.
        /// </summary>
        public static readonly CalculateThreadgroupsDelegate CalculateThreadgroupsWrapper = CalculateThreadgroups;
#endif

        /// <inheritdoc cref="ComputeShaderEffect.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(ComputeShaderEffect* @this, Guid* riid, void** ppvObject)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="ComputeShaderEffect.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(ComputeShaderEffect* @this)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            return @this->AddRef();
        }

        /// <inheritdoc cref="ComputeShaderEffect.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(ComputeShaderEffect* @this)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            return @this->Release();
        }

        /// <inheritdoc cref="ID2D1ComputeTransform.GetInputCount"/>
        [UnmanagedCallersOnly]
        public static uint GetInputCount(ComputeShaderEffect* @this)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            return (uint)@this->inputCount;
        }

        /// <inheritdoc cref="ID2D1ComputeTransform.MapOutputRectToInputRects"/>
        [UnmanagedCallersOnly]
        public static int MapOutputRectToInputRects(ComputeShaderEffect* @this, RECT* outputRect, RECT* inputRects, uint inputRectsCount)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            if (inputRectsCount != @this->inputCount)
            {
                return E.E_INVALIDARG;
            }

            if (@this->d2D1TransformMapper is not null)
            {
                // Forward to the current ID2D1TransformMapper instance (same as with pixel shaders)
                HRESULT hresult = @this->d2D1TransformMapper->MapOutputRectToInputRects(outputRect, inputRects, inputRectsCount);

                if (!Windows.SUCCEEDED(hresult))
                {
                    return hresult;
                }
            }
            else
            {
                // If no custom transform is used, mark all input rects as infinite. This
                // is because we can't really make assumption, so we just treat them the
                // same way we would for a pixel shader only habing complex inputs.
                foreach (ref RECT rect in new Span<RECT>(inputRects, (int)inputRectsCount))
                {
                    rect.MakeD2D1Infinite();
                }
            }

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1ComputeTransform.MapInputRectsToOutputRect"/>
        [UnmanagedCallersOnly]
        public static int MapInputRectsToOutputRect(ComputeShaderEffect* @this, RECT* inputRects, RECT* inputOpaqueSubRects, uint inputRectCount, RECT* outputRect, RECT* outputOpaqueSubRect)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            if (inputRectCount != @this->inputCount)
            {
                return E.E_INVALIDARG;
            }

            if (@this->d2D1TransformMapper is not null)
            {
                using ComPtr<D2D1RenderInfoUpdateContextImpl> d2D1RenderInfoUpdateContext = default;

                // Create an ID2D1RenderInfoUpdateContext instance
                HRESULT hresult = D2D1RenderInfoUpdateContextImpl.Factory(
                    renderInfoUpdateContext: d2D1RenderInfoUpdateContext.GetAddressOf(),
                    constantBuffer: @this->constantBuffer,
                    constantBufferSize: @this->constantBufferSize,
                    d2D1ComputeInfo: @this->d2D1ComputeInfo);

                if (!Windows.SUCCEEDED(hresult))
                {
                    return hresult;
                }

                // Forward the call to the input ID2D1TransformMapper instance
                hresult = @this->d2D1TransformMapper->MapInputRectsToOutputRect(
                    updateContext: (ID2D1RenderInfoUpdateContext*)d2D1RenderInfoUpdateContext.Get(),
                    inputRects: inputRects,
                    inputOpaqueSubRects: inputOpaqueSubRects,
                    inputRectCount: inputRectCount,
                    outputRect: outputRect,
                    outputOpaqueSubRect: outputOpaqueSubRect);

                // Regardless of the operation result, always invalidate the context
                _ = d2D1RenderInfoUpdateContext.Get()->Close();

                if (!Windows.SUCCEEDED(hresult))
                {
                    return hresult;
                }
            }
            else if (inputRectCount == 0)
            {
                // If there are no inputs, make the output rectangle infinite
                outputRect->MakeD2D1Infinite();

                *outputOpaqueSubRect = default;
            }
            else
            {
                // If at least one input is present and no custom mapper is available, apply the default
                // mapping. Once again, same with pixel shaders with all inputs using complex sampling.
                outputRect->MakeD2D1Infinite();

                *outputOpaqueSubRect = default;
            }

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1ComputeTransform.MapInvalidRect"/>
        [UnmanagedCallersOnly]
        public static int MapInvalidRect(ComputeShaderEffect* @this, uint inputIndex, RECT invalidInputRect, RECT* invalidOutputRect)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            if (inputIndex >= (uint)@this->inputCount)
            {
                return E.E_INVALIDARG;
            }

            if (@this->d2D1TransformMapper is not null)
            {
                // Forward to the current ID2D1TransformMapper instance
                HRESULT hresult = @this->d2D1TransformMapper->MapInvalidRect(inputIndex, invalidInputRect, invalidOutputRect);

                if (!Windows.SUCCEEDED(hresult))
                {
                    return hresult;
                }
            }
            else
            {
                // Just mark the output as infinite, as we assume all inputs are using complex sampling
                invalidOutputRect->MakeD2D1Infinite();
            }

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1ComputeTransform.SetComputeInfo"/>
        [UnmanagedCallersOnly]
        public static int SetComputeInfo(ComputeShaderEffect* @this, ID2D1ComputeInfo* computeInfo)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            // Free the previous ID2D1ComputeInfo object, if present
            if (@this->d2D1ComputeInfo is not null)
            {
                _ = @this->d2D1ComputeInfo->Release();
            }

            // Store the new ID2D1ComputeInfo object
            _ = computeInfo->AddRef();

            @this->d2D1ComputeInfo = computeInfo;

            // Set the compute shader for the effect
            int hresult = computeInfo->SetComputeShader(&@this->shaderId);

            // Process any input descriptions
            if (Windows.SUCCEEDED(hresult))
            {
                D2D1ShaderEffect.SetInputDescriptions(
                    @this->inputDescriptionCount,
                    @this->inputDescriptions,
                    (ID2D1RenderInfo*)computeInfo,
                    ref hresult);
            }

            // Also set the output buffer info
            if (Windows.SUCCEEDED(hresult))
            {
                D2D1ShaderEffect.SetOutputBuffer(
                    @this->bufferPrecision,
                    @this->channelDepth,
                    (ID2D1RenderInfo*)computeInfo,
                    ref hresult);
            }

            return hresult;
        }

        /// <inheritdoc cref="ID2D1ComputeTransform.CalculateThreadgroups"/>
        [UnmanagedCallersOnly]
        public static int CalculateThreadgroups(ComputeShaderEffect* @this, RECT* outputRect, uint* dimensionX, uint* dimensionY, uint* dimensionZ)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            // TODO

            return S.S_OK;
        }
    }
}