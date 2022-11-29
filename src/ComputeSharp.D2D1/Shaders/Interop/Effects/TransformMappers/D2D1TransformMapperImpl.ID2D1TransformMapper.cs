using System;
using System.Drawing;
using System.Runtime.CompilerServices;
#if NET6_0_OR_GREATER
using System.Runtime.InteropServices;
#endif
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Interop.Effects;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <inheritdoc/>
unsafe partial struct D2D1TransformMapperImpl
{
    /// <inheritdoc cref="QueryInterface(Guid*, void**)"/>
    [UnmanagedCallersOnly]
    private static int QueryInterface(D2D1TransformMapperImpl* @this, Guid* riid, void** ppvObject)
    {
        return @this->QueryInterface(riid, ppvObject);
    }

    /// <inheritdoc cref="AddRef()"/>
    [UnmanagedCallersOnly]
    private static uint AddRef(D2D1TransformMapperImpl* @this)
    {
        return @this->AddRef();
    }

    /// <inheritdoc cref="Release()"/>
    [UnmanagedCallersOnly]
    private static uint Release(D2D1TransformMapperImpl* @this)
    {
        return @this->Release();
    }

    /// <inheritdoc cref="ID2D1TransformMapper.MapInputRectsToOutputRect"/>
    [UnmanagedCallersOnly]
    private static int MapInputRectsToOutputRect(
        D2D1TransformMapperImpl* @this,
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
        D2D1TransformMapperImpl* @this,
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
        D2D1TransformMapperImpl* @this,
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