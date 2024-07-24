using System;

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// Options for fine-tuning the behavior of <see cref="ICanvasImageInterop.GetD2DImage"/>.
/// </summary>
[Flags]
internal enum WIN2D_GET_D2D_IMAGE_FLAGS : uint
{
    /// <summary>
    /// No options specified.
    /// </summary>
    WIN2D_GET_D2D_IMAGE_FLAGS_NONE = 0,

    /// <summary>
    /// Ignore the <c>targetDpi</c> parameter, read DPI from <c>deviceContext</c> instead.
    /// </summary>
    WIN2D_GET_D2D_IMAGE_FLAGS_READ_DPI_FROM_DEVICE_CONTEXT = 1,

    /// <summary>
    /// Ignore the <c>targetDpi</c> parameter, always insert DPI compensation.
    /// </summary>
    WIN2D_GET_D2D_IMAGE_FLAGS_ALWAYS_INSERT_DPI_COMPENSATION = 2,

    /// <summary>
    /// Ignore the <c>targetDpi</c> parameter, never insert DPI compensation.
    /// </summary>
    WIN2D_GET_D2D_IMAGE_FLAGS_NEVER_INSERT_DPI_COMPENSATION = 4,

    /// <summary>
    /// Do the bare minimum to get back an <c>ID2D1Image</c>, no validation or recursive realization.
    /// </summary>
    WIN2D_GET_D2D_IMAGE_FLAGS_MINIMAL_REALIZATION = 8,

    /// <summary>
    /// Allow partially configured effect graphs where some inputs are <see langword="null"/>.
    /// </summary>
    WIN2D_GET_D2D_IMAGE_FLAGS_ALLOW_NULL_EFFECT_INPUTS = 16,

    /// <summary>
    /// If an input is invalid, unrealize the effect and set the output image to <see langword="null"/>.
    /// </summary>
    WIN2D_GET_D2D_IMAGE_FLAGS_UNREALIZE_ON_FAILURE = 32,

    /// <summary>
    /// Ignored.
    /// </summary>
    WIN2D_GET_D2D_IMAGE_FLAGS_FORCE_DWORD = 0xFFFFFFFF
}