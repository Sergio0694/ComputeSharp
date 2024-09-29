using System;
using System.IO;
using ComputeSharp.SwapChain.Shaders;
using ComputeSharp.WinUI;
using Windows.ApplicationModel;

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
    public bool TryExecute(IReadWriteNormalizedTexture2D<Float4> texture, TimeSpan timespan, object? parameter)
    {
        if (this.texture is null ||
            this.texture.GraphicsDevice != texture.GraphicsDevice)
        {
            string filename = Path.Combine(Package.Current.InstalledLocation.Path, "Assets", "Textures", "RustyMetal.png");

            this.texture?.Dispose();

            this.texture = texture.GraphicsDevice.LoadReadOnlyTexture2D<Rgba32, Float4>(filename);
        }

        texture.GraphicsDevice.ForEach(texture, new ContouredLayers((float)timespan.TotalSeconds, this.texture));

        return true;
    }
}