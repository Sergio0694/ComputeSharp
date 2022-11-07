using System;
using System.IO;
using System.Runtime.CompilerServices;
using ComputeSharp.SwapChain.Backend;
using ComputeSharp.SwapChain.Shaders;

namespace ComputeSharp.SwapChain;

class Program
{
    /// <summary>
    /// A texture for <c>\Textures\RustyMetal.png</c>.
    /// </summary>
    private static readonly Lazy<IReadOnlyNormalizedTexture2D<float4>> RustyMetal = new(static () => LoadTexture());

    /// <summary>
    /// The mapping of available samples to choose from.
    /// </summary>
    private static readonly (string ShaderName, Win32Application Application)[] Samples = new (string, Win32Application)[]
    {
        (nameof(HelloWorld), new SwapChainApplication<HelloWorld>(static time => new((float)time.TotalSeconds))),
        (nameof(FourColorGradient), new SwapChainApplication<FourColorGradient>(static time => new((float)time.TotalSeconds))),
        (nameof(ColorfulInfinity), new SwapChainApplication<ColorfulInfinity>(static time => new((float)time.TotalSeconds))),
        (nameof(FractalTiling), new SwapChainApplication<FractalTiling>(static time => new((float)time.TotalSeconds))),
        (nameof(TwoTiledTruchet), new SwapChainApplication<TwoTiledTruchet>(static time => new((float)time.TotalSeconds))),
        (nameof(MengerJourney), new SwapChainApplication<MengerJourney>(static time => new((float)time.TotalSeconds))),
        (nameof(Octagrams), new SwapChainApplication<Octagrams>(static time => new((float)time.TotalSeconds))),
        (nameof(ProteanClouds), new SwapChainApplication<ProteanClouds>(static time => new((float)time.TotalSeconds))),
        (nameof(ExtrudedTruchetPattern), new SwapChainApplication<ExtrudedTruchetPattern>(static time => new((float)time.TotalSeconds))),
        (nameof(PyramidPattern), new SwapChainApplication<PyramidPattern>(static time => new((float)time.TotalSeconds))),
        (nameof(TriangleGridContouring), new SwapChainApplication<TriangleGridContouring>(static time => new((float)time.TotalSeconds))),
        (nameof(ContouredLayers), new SwapChainApplication<ContouredLayers>(static time => new((float)time.TotalSeconds, RustyMetal.Value)))
    };

    static void Main()
    {
        int index;

        do
        {
            Console.Clear();
            Console.WriteLine("Available samples:");
            Console.WriteLine();

            for (int i = 0; i < Samples.Length; i++)
            {
                Console.WriteLine($"{i}: {Samples[i].ShaderName}");
            }

            Console.WriteLine($"{Samples.Length}+: Exit (Use Escape, 'Q', or Alt + F4 to exit a sample once chosen)");
            Console.WriteLine();

            do
            {
                Console.Write("Enter the index of the sample to run: ");
            }
            while (!int.TryParse(Console.ReadLine(), out index));

            if (index >= 0 && index < Samples.Length)
            {
                Console.WriteLine();
                Console.WriteLine($"Starting {Samples[index].ShaderName}...");

                _ = Win32ApplicationRunner.Run(Samples[index].Application, "ComputeSharp.SwapChain", Samples[index].ShaderName);
            }
        }
        while (index >= 0 && index < Samples.Length);
    }

    /// <summary>
    /// Allocates a new <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the specified texture data.
    /// </summary>
    /// <param name="name">The name of the texture to load.</param>
    /// <returns>A texture with the data from the image at the specified name.</returns>
    private static ReadOnlyTexture2D<Rgba32, float4> LoadTexture([CallerMemberName] string name = null!)
    {
        string filename = Path.Combine(AppContext.BaseDirectory, "Textures", $"{name}.png");

        return GraphicsDevice.GetDefault().LoadReadOnlyTexture2D<Rgba32, float4>(filename);
    }
}