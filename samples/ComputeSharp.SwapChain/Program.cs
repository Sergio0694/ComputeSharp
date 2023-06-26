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

    static void Main()
    {
        // Prepare the mapping of available samples to choose from
        (string ShaderName, Win32Application Application)[] samples = new[]
        {
            (nameof(HelloWorld), Win32ApplicationFactory<HelloWorld>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(FourColorGradient), Win32ApplicationFactory<FourColorGradient>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(ColorfulInfinity), Win32ApplicationFactory<ColorfulInfinity>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(FractalTiling), Win32ApplicationFactory<FractalTiling>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(TwoTiledTruchet), Win32ApplicationFactory<TwoTiledTruchet>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(MengerJourney), Win32ApplicationFactory<MengerJourney>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(Octagrams), Win32ApplicationFactory<Octagrams>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(ProteanClouds), Win32ApplicationFactory<ProteanClouds>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(ExtrudedTruchetPattern), Win32ApplicationFactory<ExtrudedTruchetPattern>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(PyramidPattern), Win32ApplicationFactory<PyramidPattern>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(TriangleGridContouring), Win32ApplicationFactory<TriangleGridContouring>.Create(static time => new((float)time.TotalSeconds))),
            (nameof(ContouredLayers), Win32ApplicationFactory<ContouredLayers>.Create(static time => new((float)time.TotalSeconds, RustyMetal.Value))),
            (nameof(TerracedHills), Win32ApplicationFactory<TerracedHills>.Create(static time => new((float)time.TotalSeconds))),
        };

        int index;

        do
        {
            Console.Clear();
            Console.WriteLine("Available samples:");
            Console.WriteLine();

            for (int i = 0; i < samples.Length; i++)
            {
                Console.WriteLine($"{i}: {samples[i].ShaderName}");
            }

            Console.WriteLine($"{samples.Length}+: Exit (Use Escape, 'Q', or Alt + F4 to exit a sample once chosen)");
            Console.WriteLine();

            do
            {
                Console.Write("Enter the index of the sample to run: ");
            }
            while (!int.TryParse(Console.ReadLine(), out index));

            if (index >= 0 && index < samples.Length)
            {
                Console.WriteLine();
                Console.WriteLine($"Starting {samples[index].ShaderName}...");

                _ = Win32ApplicationRunner.Run(samples[index].Application, "ComputeSharp.SwapChain", samples[index].ShaderName);
            }
        }
        while (index >= 0 && index < samples.Length);
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