using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.Interop;
using ComputeSharp.Win32;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using ICanvasImageInterop = Microsoft.Graphics.Canvas.ICanvasImageInterop;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <summary>
/// A custom <see cref="ICanvasEffect"/> implementation powered by a supplied shader type.
/// </summary>
/// <typeparam name="T">The type of shader to use to render frames.</typeparam>
public sealed partial class PixelShaderEffect<T> : IReferenceTrackedObject, ICanvasEffect, ICanvasImageInterop
    where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
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
    /// The backing field for <see cref="ConstantBuffer"/>.
    /// </summary>
    private T constantBuffer;

    /// <summary>
    /// The backing field for <see cref="TransformMapper"/>.
    /// </summary>
    private D2D1DrawTransformMapper<T>? transformMapper;

    /// <summary>
    /// The backing field for <see cref="CacheOutput"/>.
    /// </summary>
    private bool cacheOutput;

    /// <summary>
    /// The backing field for <see cref="BufferPrecision"/>.
    /// </summary>
    private D2D1_BUFFER_PRECISION d2D1BufferPrecision;

    /// <summary>
    /// Creates a new <see cref="PixelShaderEffect{T}"/> instance.
    /// </summary>
    public PixelShaderEffect()
    {
        using ReferenceTracker.Lease _0 = ReferenceTracker.Create(this, out this.referenceTracker);

        Sources = new SourceCollection(this);
        ResourceTextureManagers = new ResourceTextureManagerCollection(this);
    }
}