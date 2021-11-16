using System;
using System.IO;
using ComputeSharp.SwapChain.Shaders;
using ComputeSharp.Uwp;
using Windows.ApplicationModel;

#nullable enable

namespace ComputeSharp.SwapChain.Uwp.Shaders.Runners
{
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
        public void Execute(IReadWriteTexture2D<Float4> texture, TimeSpan timespan)
        {
            if (this.texture is null)
            {
                string filename = Path.Combine(Package.Current.InstalledLocation.Path, "Assets", "Textures", "RustyMetal.png");

                this.texture = Gpu.Default.LoadReadOnlyTexture2D<Rgba32, Float4>(filename.AsSpan());
            }

            Gpu.Default.ForEach(texture, new ContouredLayers((float)timespan.TotalSeconds, this.texture));
        }
    }
}