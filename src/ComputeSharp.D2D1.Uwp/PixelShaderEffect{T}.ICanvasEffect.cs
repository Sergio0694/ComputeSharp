using System;
using System.Buffers;
using System.Runtime.InteropServices;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.Interop;
using TerraFX.Interop.Windows;
using Windows.Foundation;
using ICanvasEffect = Microsoft.Graphics.Canvas.Effects.ICanvasEffect;
using ICanvasResourceCreatorWithDpi = Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi;

namespace ComputeSharp.D2D1.Uwp;

/// <inheritdoc/>
unsafe partial class PixelShaderEffect<T>
{
    /// <inheritdoc/>
    public void InvalidateSourceRectangle(ICanvasResourceCreatorWithDpi resourceCreator, uint sourceIndex, Rect invalidRectangle)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        using ComPtr<ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi> resourceCreatorWithDpi = default;
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        GetCanvasResourceCreatorAndCanvasImageInteropUnderlyingObjects(
            resourceCreator,
            resourceCreatorWithDpi.GetAddressOf(),
            canvasImageInterop.GetAddressOf());

        Win2D.InvalidateSourceRectangleForICanvasImageInterop(
            resourceCreator: resourceCreatorWithDpi.Get(),
            image: canvasImageInterop.Get(),
            sourceIndex: sourceIndex,
            invalidRectangle: &invalidRectangle).Assert();
    }

    /// <inheritdoc/>
    public Rect[] GetInvalidRectangles(ICanvasResourceCreatorWithDpi resourceCreator)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        using ComPtr<ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi> resourceCreatorWithDpi = default;
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        GetCanvasResourceCreatorAndCanvasImageInteropUnderlyingObjects(
            resourceCreator,
            resourceCreatorWithDpi.GetAddressOf(),
            canvasImageInterop.GetAddressOf());

        uint valueCount;
        Rect* valueElements;

        Win2D.GetInvalidRectanglesForICanvasImageInterop(
            resourceCreator: resourceCreatorWithDpi.Get(),
            image: canvasImageInterop.Get(),
            valueCount: &valueCount,
            valueElements: &valueElements).Assert();

        try
        {
            // Copy the data back from the COM array that Win2D created and returned.
            // If no items were returned, this will automatically return an empty array.
            return new Span<Rect>(valueElements, checked((int)valueCount)).ToArray();
        }
        finally
        {
            // Make sure to release the callee-allocated COM array
            Marshal.FreeCoTaskMem((IntPtr)valueElements);
        }
    }

    /// <inheritdoc/>
    public Rect GetRequiredSourceRectangle(
        ICanvasResourceCreatorWithDpi resourceCreator,
        Rect outputRectangle,
        ICanvasEffect sourceEffect,
        uint sourceIndex,
        Rect sourceBounds)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        using ComPtr<ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi> resourceCreatorWithDpi = default;
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        GetCanvasResourceCreatorAndCanvasImageInteropUnderlyingObjects(
            resourceCreator,
            resourceCreatorWithDpi.GetAddressOf(),
            canvasImageInterop.GetAddressOf());

        Rect result;

        using ComPtr<IUnknown> canvasEffectUnknown = default;
        using ComPtr<ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect> canvasEffectAbi = default;

        // Get the ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect object from the input interface
        canvasEffectUnknown.Attach((IUnknown*)Marshal.GetIUnknownForObject(sourceEffect));
        canvasEffectUnknown.CopyTo(canvasEffectAbi.GetAddressOf()).Assert();

        Win2D.GetRequiredSourceRectanglesForICanvasImageInterop(
            resourceCreator: resourceCreatorWithDpi.Get(),
            image: canvasImageInterop.Get(),
            outputRectangle: &outputRectangle,
            sourceEffectCount: 1,
            sourceEffects: canvasEffectAbi.GetAddressOf(),
            sourceIndexCount: 1,
            sourceIndices: &sourceIndex,
            sourceBoundsCount: 1,
            sourceBounds: &sourceBounds,
            valueCount: 1,
            valueElements: &result).Assert();

        return result;
    }

    /// <inheritdoc/>
    public Rect[] GetRequiredSourceRectangles(
        ICanvasResourceCreatorWithDpi resourceCreator,
        Rect outputRectangle,
        ICanvasEffect[] sourceEffects,
        uint[] sourceIndices,
        Rect[] sourceBounds)
    {
        using ReferenceTracker.Lease _0 = GetReferenceTracker().GetLease();

        using ComPtr<ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi> resourceCreatorWithDpi = default;
        using ComPtr<ICanvasImageInterop> canvasImageInterop = default;

        GetCanvasResourceCreatorAndCanvasImageInteropUnderlyingObjects(
            resourceCreator,
            resourceCreatorWithDpi.GetAddressOf(),
            canvasImageInterop.GetAddressOf());

        // The array optionally rented from the pool is of IntPtr types to reduce the number of generic
        // instantiations. This increases the chances of the pooled arrays being shared with other code.
        IntPtr[]? canvasEffectsAbiArray = null;

        // Create the span and optional array of ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect objects
        Span<ComPtr<ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect>> canvasEffects = sourceEffects.Length <= 16
            ? stackalloc ComPtr<ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect>[16]
            : MemoryMarshal.Cast<IntPtr, ComPtr<ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect>>(
                canvasEffectsAbiArray = ArrayPool<IntPtr>.Shared.Rent(sourceEffects.Length));

        // Clear the span to ensure no false positives are accidentally disposed
        canvasEffects.Clear();

        try
        {
            // Get the underlying ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect object for each input effect
            for (int i = 0; i < sourceEffects.Length; i++)
            {
                using ComPtr<IUnknown> canvasEffectUnknown = default;

                canvasEffectUnknown.Attach((IUnknown*)Marshal.GetIUnknownForObject(sourceEffects[i]));
                canvasEffectUnknown.CopyTo(canvasEffects[i].GetAddressOf()).Assert();
            }

            Rect[] result = new Rect[sourceEffects.Length];

            fixed (ComPtr<ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect>* pCanvasEffects = canvasEffects)
            fixed (uint* pSourceIndices = sourceIndices)
            fixed (Rect* pSourceBounds = sourceBounds)
            fixed (Rect* pResult = result)
            {
                Win2D.GetRequiredSourceRectanglesForICanvasImageInterop(
                    resourceCreator: resourceCreatorWithDpi.Get(),
                    image: canvasImageInterop.Get(),
                    outputRectangle: &outputRectangle,
                    sourceEffectCount: (uint)sourceEffects.Length,
                    sourceEffects: (ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect**)pCanvasEffects,
                    sourceIndexCount: (uint)sourceIndices.Length,
                    sourceIndices: pSourceIndices,
                    sourceBoundsCount: (uint)sourceBounds.Length,
                    sourceBounds: pSourceBounds,
                    valueCount: (uint)sourceEffects.Length,
                    valueElements: pResult).Assert();
            }

            // Only return the array to the pool if no exceptions have been thrown
            if (canvasEffectsAbiArray is not null)
            {
                ArrayPool<IntPtr>.Shared.Return(canvasEffectsAbiArray);
            }

            return result;
        }
        finally
        {
            // Release all canvas effects that have been retrieved up until the exception
            foreach (ref ComPtr<ABI.Microsoft.Graphics.Canvas.Effects.ICanvasEffect> canvasEffect in canvasEffects)
            {
                canvasEffect.Dispose();
            }
        }
    }

    /// <summary>
    /// Gets the underlying <see cref="ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi"/> and <see cref="ICanvasImageInterop"/>
    /// objects from an input <see cref="ICanvasResourceCreatorWithDpi"/> object and the current <see cref="PixelShaderEffect{T}"/> instance.
    /// </summary>
    /// <param name="resourceCreator">The input <see cref="ICanvasResourceCreatorWithDpi"/> object.</param>
    /// <param name="resourceCreatorWithDpi">The resulting <see cref="ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi"/> object for <paramref name="resourceCreator"/>.</param>
    /// <param name="canvasImageInterop">The resulting <see cref="ICanvasImageInterop"/> object for the current <see cref="PixelShaderEffect{T}"/> instance.</param>
    private void GetCanvasResourceCreatorAndCanvasImageInteropUnderlyingObjects(
        ICanvasResourceCreatorWithDpi resourceCreator,
        ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi** resourceCreatorWithDpi,
        ICanvasImageInterop** canvasImageInterop)
    {
        using ComPtr<IUnknown> resourceCreatorUnknown = default;

        // Get the ABI.Microsoft.Graphics.Canvas.ICanvasResourceCreatorWithDpi object from the input interface
        resourceCreatorUnknown.Attach((IUnknown*)Marshal.GetIUnknownForObject(resourceCreator));
        resourceCreatorUnknown.CopyTo(resourceCreatorWithDpi).Assert();

        using ComPtr<IUnknown> canvasImageInteropUnknown = default;

        // Get the ICanvasImageInterop object from the current instance
        canvasImageInteropUnknown.Attach((IUnknown*)Marshal.GetIUnknownForObject(this));
        canvasImageInteropUnknown.CopyTo(canvasImageInterop).Assert();
    }
}