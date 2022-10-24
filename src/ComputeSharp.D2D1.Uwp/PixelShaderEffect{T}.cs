using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.Interop;
using Microsoft.Graphics.Canvas;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Uwp;

/// <summary>
/// A custom <see cref="ICanvasImage"/> implementation powered by a supplied shader type.
/// </summary>
/// <typeparam name="T">The type of shader to use to render frames.</typeparam>
public sealed partial class PixelShaderEffect<T> : IReferenceTrackedObject, ICanvasImage, ICanvasImageInterop
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// The <see cref="ReferenceTracker"/> value for the current instance.
    /// </summary>
    private ReferenceTracker referenceTracker;

    /// <summary>
    /// Lock object used to synchronize calls to <see cref="ICanvasImageInterop"/> APIs.
    /// </summary>
    private readonly object lockObject = new();

    /// <summary>
    /// Flag to track whether a given call is recursively invoked by <see cref="ICanvasImageInterop.GetD2DImage"/>, to avoid graph cycles.
    /// </summary>
    private volatile int isInsideGetD2DImage;

    /// <summary>
    /// The current device the effect is realized on, if one is available (this is the underlying COM object for <see cref="CanvasDevice"/>).
    /// </summary>
    private ComPtr<ICanvasDevice> canvasDevice;

    /// <summary>
    /// The underlying D2D1 device the effect is realized on, if one is available (this is the D2D device backing <see cref="canvasDevice"/>).
    /// </summary>
    private ComPtr<ID2D1Device1> d2D1RealizationDevice;

    /// <summary>
    /// The current realized <see cref="ID2D1Effect"/>, if one is available.
    /// </summary>
    private ComPtr<ID2D1Effect> d2D1Effect;

    /// <summary>
    /// The backing field for <see cref="CacheOutput"/>.
    /// </summary>
    private bool cacheOutput;

    /// <summary>
    /// The backing field for <see cref="BufferPrecision"/>.
    /// </summary>
    private D2D1_BUFFER_PRECISION d2D1BufferPrecision;
}