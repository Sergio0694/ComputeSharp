using System;
using System.Runtime.InteropServices;
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

            // TODO

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1ComputeTransform.MapInputRectsToOutputRect"/>
        [UnmanagedCallersOnly]
        public static int MapInputRectsToOutputRect(ComputeShaderEffect* @this, RECT* inputRects, RECT* inputOpaqueSubRects, uint inputRectCount, RECT* outputRect, RECT* outputOpaqueSubRect)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            // TODO

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1ComputeTransform.MapInvalidRect"/>
        [UnmanagedCallersOnly]
        public static int MapInvalidRect(ComputeShaderEffect* @this, uint inputIndex, RECT invalidInputRect, RECT* invalidOutputRect)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            // TODO

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1ComputeTransform.SetComputeInfo"/>
        [UnmanagedCallersOnly]
        public static int SetComputeInfo(ComputeShaderEffect* @this, ID2D1ComputeInfo* computeInfo)
        {
            @this = (ComputeShaderEffect*)&((void**)@this)[-1];

            // TODO

            return S.S_OK;
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