using System;
using System.IO;
using ComputeSharp.SwapChain.Shaders;
#if WINDOWS_UWP
using ComputeSharp.Uwp;
#else
using ComputeSharp.WinUI;
#endif
using Windows.ApplicationModel;

#nullable enable

namespace ComputeSharp.SwapChain.Core.Shaders.Runners;

/// <summary>
/// A specialized <see cref="IShaderRunner"/> for <see cref="ContouredLayers"/>.
/// </summary>
public sealed class ContouredLayersRunner : IShaderRunner
{
    /// <summary>
    /// A texture for <c>\Textures\RustyMetal.png</c>.
    /// </summary>
    private ReadOnlyTexture2D<Rgba32, Float4>? texture;

    /// <inheritdoc/>
    public void Execute(IReadWriteTexture2D<Float4> texture, TimeSpan timespan, object? parameter)
    {
        if (this.texture is null)
        {
            string filename = Path.Combine(Package.Current.InstalledLocation.Path, "Assets", "Textures", "RustyMetal.png");

            this.texture = GraphicsDevice.Default.LoadReadOnlyTexture2D<Rgba32, Float4>(filename);
        }

        GraphicsDevice.Default.ForEach(texture, new ContouredLayers((float)timespan.TotalSeconds, this.texture));
    }
}
