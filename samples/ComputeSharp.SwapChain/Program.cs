using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.SwapChain.Backend;
using ComputeSharp.SwapChain.Shaders;
using SixLabors.ImageSharp;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.SwapChain
{
    class Program
    {
        /// <summary>
        /// A texture for <c>\Textures\RustyMetal.png</c>.
        /// </summary>
        private static readonly IReadOnlyTexture2D<Float4> RustyMetal = LoadTexture();

        /// <summary>
        /// The mapping of available samples to choose from.
        /// </summary>
        private static readonly Win32Application[] Samples = new Win32Application[]
        {
            new SwapChainApplication<ColorfulInfinity>(static time => new((float)time.TotalSeconds)),
            new SwapChainApplication<FractalTiling>(static time => new((float)time.TotalSeconds)),
            new SwapChainApplication<TwoTiledTruchet>(static time => new((float)time.TotalSeconds)),
            new SwapChainApplication<MengerJourney>(static time => new((float)time.TotalSeconds)),
            new SwapChainApplication<Octagrams>(static time => new((float)time.TotalSeconds)),
            new SwapChainApplication<ProteanClouds>(static time => new((float)time.TotalSeconds)),
            new SwapChainApplication<ExtrudedTruchetPattern>(static time => new((float)time.TotalSeconds)),
            new SwapChainApplication<PyramidPattern>(static time => new((float)time.TotalSeconds)),
            new SwapChainApplication<TriangleGridContouring>(static time => new((float)time.TotalSeconds)),
            new SwapChainApplication<ContouredLayers>(static time => new((float)time.TotalSeconds, RustyMetal))
        };

        static void Main()
        {
            Console.WriteLine("Available samples:");
            Console.WriteLine();

            for (int i = 0; i < Samples.Length; i++)
            {
                Console.WriteLine($"{i}: {Samples[i].GetType().GenericTypeArguments[0].Name}");
            }

            Console.WriteLine();

            int index;

            do
            {
                Console.Write("Enter the index of the sample to run: ");
            }
            while (!int.TryParse(Console.ReadLine(), out index));

            Console.WriteLine();
            Console.WriteLine($"Starting {Samples[index].GetType().GenericTypeArguments[0].Name}...");

            Win32ApplicationRunner.Run(Samples[index]);
        }

        /// <summary>
        /// Allocates a new <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the specified texture data.
        /// </summary>
        /// <param name="name">The name of the texture to load.</param>
        /// <returns>A texture with the data from the image at the specified name.</returns>
        [Pure]
        private static ReadOnlyTexture2D<Rgba32, Float4> LoadTexture([CallerMemberName] string name = null!)
        {
            string filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Textures", $"{name}.png");

            using Image<ImageSharpRgba32> image = Image.Load<ImageSharpRgba32>(filename);

            ReadOnlyTexture2D<Rgba32, Float4> texture = Gpu.Default.AllocateReadOnlyTexture2D<Rgba32, Float4>(image.Width, image.Height);

            image.TryGetSinglePixelSpan(out Span<ImageSharpRgba32> span);

            texture.CopyFrom(MemoryMarshal.Cast<ImageSharpRgba32, Rgba32>(span));

            return texture;
        }
    }
}
