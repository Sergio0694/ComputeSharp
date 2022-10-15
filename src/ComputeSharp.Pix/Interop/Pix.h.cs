/*==========================================================================;
 *
 *  Copyright (C) Microsoft Corporation.  All Rights Reserved.
 *
 *  File:       pix3.h
 *  Content:    PIX include file
 *
 ****************************************************************************/

// The following methods in this file have been ported from the
// pix3.h file in the winpixeventruntime.1.0.220124001 package.

using System.Runtime.CompilerServices;

namespace ComputeSharp.Interop;

/// <inheritdoc/>
partial class Pix
{
    /// <summary>
    /// Gets a <see cref="uint"/> value representing a color for a PIX event.
    /// </summary>
    /// <param name="r">The red channel value for the color.</param>
    /// <param name="g">The green channel value for the color.</param>
    /// <param name="b">The blue channel value for the color.</param>
    /// <returns>A <see cref="uint"/> value representing a color for a PIX event.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint PIX_COLOR(byte r, byte g, byte b)
    {
        return 0xFF000000 | ((uint)r << 16) | ((uint)g << 8) | b;
    }

    /// <summary>
    /// Gets a <see cref="uint"/> value representing an indexed color for a PIX event.
    /// </summary>
    /// <param name="i">The index of the event to get the color for.</param>
    /// <returns>A <see cref="uint"/> value representing an indexed color for a PIX event.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint PIX_COLOR_INDEX(byte i)
    {
        return i;
    }
}