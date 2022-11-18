using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Interop.Effects;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <inheritdoc/>
unsafe partial struct ID2D1TransformMapperProxy
{
#if !NET6_0_OR_GREATER
    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int QueryInterfaceDelegate(ID2D1TransformMapperProxy* @this, Guid* riid, void** ppvObject);

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint AddRefDelegate(ID2D1TransformMapperProxy* @this);

    /// <inheritdoc cref="IUnknown.Release"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint ReleaseDelegate(ID2D1TransformMapperProxy* @this);

    /// <inheritdoc cref="ID2D1TransformMapper.MapInputRectsToOutputRect"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int MapInputRectsToOutputRectDelegate(
        ID2D1TransformMapperProxy* @this,
        ID2D1DrawInfoUpdateContext* d2D1DrawInfoUpdateContext,
        RECT* inputRects,
        RECT* inputOpaqueSubRects,
        uint inputRectCount,
        RECT* outputRect,
        RECT* outputOpaqueSubRect);

    /// <inheritdoc cref="ID2D1TransformMapper.MapOutputRectToInputRects"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int MapOutputRectToInputRectsDelegate(
        ID2D1TransformMapperProxy* @this,
        RECT* outputRect,
        RECT* inputRects,
        uint inputRectsCount);

    /// <inheritdoc cref="ID2D1TransformMapper.MapInvalidRect"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int MapInvalidRectDelegate(
        ID2D1TransformMapperProxy* @this,
        uint inputIndex,
        RECT invalidInputRect,
        RECT* invalidOutputRect);

    /// <summary>
    /// A cached <see cref="QueryInterfaceDelegate"/> instance wrapping <see cref="QueryInterface(Guid*, void**)"/>.
    /// </summary>
    private static readonly QueryInterfaceDelegate QueryInterfaceWrapper = QueryInterface;

    /// <summary>
    /// A cached <see cref="AddRefDelegate"/> instance wrapping <see cref="AddRef()"/>.
    /// </summary>
    private static readonly AddRefDelegate AddRefWrapper = AddRef;

    /// <summary>
    /// A cached <see cref="ReleaseDelegate"/> instance wrapping <see cref="Release()"/>.
    /// </summary>
    private static readonly ReleaseDelegate ReleaseWrapper = Release;

    /// <summary>
    /// A cached <see cref="MapInputRectsToOutputRectDelegate"/> instance wrapping <see cref="MapInputRectsToOutputRect"/>.
    /// </summary>
    private static readonly MapInputRectsToOutputRectDelegate MapInputRectsToOutputRectWrapper = MapInputRectsToOutputRect;

    /// <summary>
    /// A cached <see cref="MapOutputRectToInputRectsDelegate"/> instance wrapping <see cref="MapOutputRectToInputRects"/>.
    /// </summary>
    private static readonly MapOutputRectToInputRectsDelegate MapOutputRectToInputRectsWrapper = MapOutputRectToInputRects;

    /// <summary>
    /// A cached <see cref="MapInvalidRectDelegate"/> instance wrapping <see cref="MapInvalidRect"/>.
    /// </summary>
    private static readonly MapInvalidRectDelegate MapInvalidRectWrapper = MapInvalidRect;
#endif

    /// <inheritdoc cref="QueryInterface(Guid*, void**)"/>
    [UnmanagedCallersOnly]
    private static int QueryInterface(ID2D1TransformMapperProxy* @this, Guid* riid, void** ppvObject)
    {
        return @this->QueryInterface(riid, ppvObject);
    }

    /// <inheritdoc cref="AddRef()"/>
    [UnmanagedCallersOnly]
    private static uint AddRef(ID2D1TransformMapperProxy* @this)
    {
        return @this->AddRef();
    }

    /// <inheritdoc cref="Release()"/>
    [UnmanagedCallersOnly]
    private static uint Release(ID2D1TransformMapperProxy* @this)
    {
        return @this->Release();
    }

    /// <inheritdoc cref="ID2D1TransformMapper.MapInputRectsToOutputRect"/>
    [UnmanagedCallersOnly]
    private static int MapInputRectsToOutputRect(
        ID2D1TransformMapperProxy* @this,
        ID2D1DrawInfoUpdateContext* d2D1DrawInfoUpdateContext,
        RECT* inputRects,
        RECT* inputOpaqueSubRects,
        uint inputRectCount,
        RECT* outputRect,
        RECT* outputOpaqueSubRect)
    {
        Span<Rectangle> inputs = stackalloc Rectangle[8].Slice(0, (int)inputRectCount);
        Span<Rectangle> opaqueInputs = stackalloc Rectangle[8].Slice(0, (int)inputRectCount);

        for (int i = 0; i < (int)inputRectCount; i++)
        {
            inputs[i] = inputRects[i].ToRectangle();
            opaqueInputs[i] = inputOpaqueSubRects[i].ToRectangle();
        }

        Rectangle output;
        Rectangle opaqueOutput;

        // Invoke MapInputsToOutput and handle exceptions so they don't cross the ABI boundary
        try
        {
            Unsafe.As<ID2D1TransformMapperInterop>(@this->transformMapperHandle.Target!).MapInputsToOutput(
                d2D1DrawInfoUpdateContext,
                inputs,
                opaqueInputs,
                out output,
                out opaqueOutput);
        }
        catch (Exception e)
        {
            return e.HResult;
        }

        *outputRect = output.ToRECT();
        *outputOpaqueSubRect = opaqueOutput.ToRECT();

        return S.S_OK;
    }

    /// <inheritdoc cref="ID2D1TransformMapper.MapOutputRectToInputRects"/>
    [UnmanagedCallersOnly]
    private static int MapOutputRectToInputRects(
        ID2D1TransformMapperProxy* @this,
        RECT* outputRect,
        RECT* inputRects,
        uint inputRectsCount)
    {
        Rectangle output = outputRect->ToRectangle();
        Span<Rectangle> inputs = stackalloc Rectangle[8].Slice(0, (int)inputRectsCount);

        for (int i = 0; i < (int)inputRectsCount; i++)
        {
            inputs[i] = inputRects[i].ToRectangle();
        }

        // Handle exceptions, as mentioned above
        try
        {
            Unsafe.As<ID2D1TransformMapperInterop>(@this->transformMapperHandle.Target!).MapOutputToInputs(in output, inputs);
        }
        catch (Exception e)
        {
            return e.HResult;
        }

        for (int i = 0; i < (int)inputRectsCount; i++)
        {
            inputRects[i] = inputs[i].ToRECT();
        }

        return S.S_OK;
    }

    /// <inheritdoc cref="ID2D1TransformMapper.MapInvalidRect"/>
    [UnmanagedCallersOnly]
    private static int MapInvalidRect(
        ID2D1TransformMapperProxy* @this,
        uint inputIndex,
        RECT invalidInputRect,
        RECT* invalidOutputRect)
    {
        Rectangle invalidInput = invalidInputRect.ToRectangle();
        Rectangle invalidOutput;

        // Handle exceptions once again
        try
        {
            Unsafe.As<ID2D1TransformMapperInterop>(@this->transformMapperHandle.Target!).MapInvalidOutput((int)inputIndex, invalidInput, out invalidOutput);
        }
        catch (Exception e)
        {
            return e.HResult;
        }

        *invalidOutputRect = invalidOutput.ToRECT();

        return S.S_OK;
    }
}