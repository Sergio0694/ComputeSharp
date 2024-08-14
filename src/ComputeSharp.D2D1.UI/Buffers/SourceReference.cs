using System;
using System.Runtime.CompilerServices;
using ABI.Microsoft.Graphics.Canvas;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp.Extensions;
using ComputeSharp.D2D1.Uwp.Helpers;
#else
using ComputeSharp.D2D1.WinUI.Extensions;
using ComputeSharp.D2D1.WinUI.Helpers;
#endif
using ComputeSharp.Win32;
using Windows.Graphics.Effects;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.Buffers;
#else
namespace ComputeSharp.D2D1.WinUI.Buffers;
#endif

/// <summary>
/// A wrapper type for an input <see cref="IGraphicsEffectSource"/> and its underlying native D2D images.
/// </summary>
/// <remarks>
/// <para>
/// This type is meant to support these two common operations:
/// <list type="bullet">
///     <item>Look up a D2D resource from a property of some other D2D interface.</item>
///     <item>Get a managed wrapper from a D2D interface.</item>
/// </list>
/// With this, the generated D2D objects can be associated with each original managed wrapper. Similarly,
/// when the other way around is used, this type can also store the generated wrapper for future use.
/// </para>
/// <para>
/// This gives the following improvements:
/// <list type="bullet">
///     <item>Improved performance for the common case of repeated calls using the same D2D instance.</item>
///     <item>Maintaining a reference to the wrapper avoids it being repeatedly released and recreated.</item>
/// </list>
/// </para>
/// <para>
/// Just like with all other members of an effect, values in this object are considered authoritative when the
/// effect is not realized, otherwise they just act as a cache to speedup the reverse managed wrapper lookups.
/// </para>
/// <para>
/// For realized effects, <see cref="SourceReference"/> tracks both the <see cref="ID2D1Image"/> resource and its
/// <see cref="IGraphicsEffectSource"/> wrapper (and optional <see cref="ID2D1Effect"/> being the DPI compensation effect,
/// if one is needed. For unrealized effects, the resource is <see langword="null"/> and only the wrapper part is used.
/// </para>
/// </remarks>
internal unsafe struct SourceReference : IDisposable
{
    /// <summary>
    /// The <see cref="ID2D1Image"/> produced by calling <see cref="ICanvasImageInterop.GetD2DImage"/>
    /// on the input managed wrapper used as source for the effect (ie. <see cref="graphicsEffectSourceWrapper"/>).
    /// </summary>
    private ComPtr<ID2D1Image> d2D1ImageSource;

    /// <summary>
    /// The <see cref="ID2D1Effect"/> for DPI compensation wrapping <see cref="d2D1ImageSource"/>, if needed.
    /// </summary>
    private ComPtr<ID2D1Effect> d2D1EffectDpiCompensation;

    /// <summary>
    /// The <see cref="IGraphicsEffectSource"/> instance being a managed wrapper for the current effect input.
    /// </summary>
    private IGraphicsEffectSource? graphicsEffectSourceWrapper;

    /// <summary>
    /// Gets whether or not this source ference has a DPI compensation effect.
    /// </summary>
    public readonly bool HasDpiCompensationEffect
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.d2D1EffectDpiCompensation.Get() is not null;
    }

    /// <summary>
    /// Gets the <see cref="ID2D1Effect"/> object for the DPI compensation effect used, if any.
    /// </summary>
    /// <returns>The <see cref="ID2D1Effect"/> object for the DPI compensation effect used, if any.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ID2D1Effect* GetDpiCompensationEffect()
    {
        return this.d2D1EffectDpiCompensation.Get();
    }

    /// <summary>
    /// Sets the DPI compensation effet being used,
    /// </summary>
    /// <param name="d2D1Effect"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetDpiCompensationEffect(ID2D1Effect* d2D1Effect)
    {
        this.d2D1EffectDpiCompensation.Dispose();

        if (d2D1Effect is not null)
        {
            this.d2D1EffectDpiCompensation = new ComPtr<ID2D1Effect>(d2D1Effect);
        }
    }

    /// <summary>
    /// Gets the <see cref="IGraphicsEffectSource"/> instance being a managed wrapper for the current effect input.
    /// </summary>
    /// <returns>The <see cref="IGraphicsEffectSource"/> instance being a managed wrapper for the current effect input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly IGraphicsEffectSource? GetWrapper()
    {
        return this.graphicsEffectSourceWrapper;
    }

    /// <summary>
    /// Sets the <see cref="IGraphicsEffectSource"/> instance being a managed wrapper for the current effect input.
    /// </summary>
    /// <param name="wrapper">The <see cref="IGraphicsEffectSource"/> instance being a managed wrapper for the current effect input.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetWrapper(IGraphicsEffectSource? wrapper)
    {
        this.d2D1ImageSource.Dispose();

        this.graphicsEffectSourceWrapper = wrapper;
    }

    /// <summary>
    /// Sets the wrapper and underlying realized resource for the current source reference.
    /// </summary>
    /// <param name="wrapper">The <see cref="IGraphicsEffectSource"/> instance being a managed wrapper for the current effect input.</param>
    /// <param name="d2D1Image">The underlying relized <see cref="ID2D1Image"/> instance for <paramref name="wrapper"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetWrapperAndResource(IGraphicsEffectSource? wrapper, ID2D1Image* d2D1Image)
    {
        this.d2D1ImageSource.Dispose();

        this.d2D1ImageSource = new ComPtr<ID2D1Image>(d2D1Image);
        this.graphicsEffectSourceWrapper = wrapper;
    }

    /// <summary>
    /// Gets or creates an <see cref="IGraphicsEffectSource"/> wrapper for a given <see cref="ID2D1Image"/> object.
    /// </summary>
    /// <param name="canvasDevice">The input <see cref="ICanvasDevice"/> object the source effect is realized on.</param>
    /// <param name="d2D1Image">The input <see cref="ID2D1Image"/> object to get or create a wrapper for.</param>
    /// <returns>An <see cref="IGraphicsEffectSource"/> wrapper for a given <see cref="ID2D1Image"/> object.</returns>
    public IGraphicsEffectSource? GetOrCreateWrapper(ICanvasDevice* canvasDevice, ID2D1Image* d2D1Image)
    {
        // Check if there already is a matching managed wrapper for the input image
        if (!this.d2D1ImageSource.Get()->IsSameInstance(d2D1Image))
        {
            // If that's not the case, create a new wrapper if the input image is not null
            if (d2D1Image is not null)
            {
                this.graphicsEffectSourceWrapper = ResourceManager.GetOrCreate(
                    device: canvasDevice,
                    resource: (IUnknown*)d2D1Image,
                    dpi: 0);
            }
            else
            {
                this.graphicsEffectSourceWrapper = null;
            }

            this.d2D1ImageSource = new ComPtr<ID2D1Image>(d2D1Image);
        }

        return this.graphicsEffectSourceWrapper;
    }

    /// <summary>
    /// Updates the underlying <see cref="ID2D1Image"/> resource, if needed.
    /// This is used after a potential re-realization of a lazily bound effect.
    /// </summary>
    /// <param name="d2D1Image">The input <see cref="ID2D1Image"/> object.</param>
    /// <returns>Whether the resource has changed or not.</returns>
    public bool UpdateResource(ID2D1Image* d2D1Image)
    {
        if (d2D1Image->IsSameInstance(this.d2D1ImageSource.Get()))
        {
            return false;
        }

        this.d2D1ImageSource.Dispose();
        this.d2D1ImageSource = new ComPtr<ID2D1Image>(d2D1Image);

        return true;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.d2D1ImageSource.Dispose();
        this.d2D1EffectDpiCompensation.Dispose();
        this.graphicsEffectSourceWrapper = null;
    }
}