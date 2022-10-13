using System;

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// Options for fine-tuning the behavior of ICanvasImageInternal::GetD2DImage.
/// </summary>
[Flags]
internal enum GetImageFlags
{
    /// <summary>
    /// No options specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Ignore the <c>targetDpi</c> parameter, read DPI from <c>deviceContext</c> instead.
    /// </summary>
    ReadDpiFromDeviceContext = 1,

    /// <summary>
    /// Ignore the <c>targetDpi</c> parameter, always insert DPI compensation.
    /// </summary>
    AlwaysInsertDpiCompensation = 2,

    /// <summary>
    /// Ignore the <c>targetDpi</c> parameter, never insert DPI compensation.
    /// </summary>
    NeverInsertDpiCompensation = 4,

    /// <summary>
    /// Do the bare minimum to get back an <c>ID2D1Image</c>, no validation or recursive realization.
    /// </summary>
    MinimalRealization = 8,

    /// <summary>
    /// Allow partially configured effect graphs where some inputs are <see langword="null"/>.
    /// </summary>
    AllowNullEffectInputs = 16,

    /// <summary>
    /// If an input is invalid, unrealize the effect and return <see langword="null"/> rather than throwing.
    /// </summary>
    UnrealizeOnFailure = 32
}