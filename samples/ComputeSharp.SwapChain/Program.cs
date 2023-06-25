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
        static HelloWorld HelloWorld(TimeSpan time) => new((float)time.TotalSeconds);
        static FourColorGradient FourColorGradient(TimeSpan time) => new((float)time.TotalSeconds);
        static ColorfulInfinity ColorfulInfinity(TimeSpan time) => new((float)time.TotalSeconds);
        static FractalTiling FractalTiling(TimeSpan time) => new((float)time.TotalSeconds);
        static TwoTiledTruchet TwoTiledTruchet(TimeSpan time) => new((float)time.TotalSeconds);
        static MengerJourney MengerJourney(TimeSpan time) => new((float)time.TotalSeconds);
        static Octagrams Octagrams(TimeSpan time) => new((float)time.TotalSeconds);
        static ProteanClouds ProteanClouds(TimeSpan time) => new((float)time.TotalSeconds);
        static ExtrudedTruchetPattern ExtrudedTruchetPattern(TimeSpan time) => new((float)time.TotalSeconds);
        static PyramidPattern PyramidPattern(TimeSpan time) => new((float)time.TotalSeconds);
        static TriangleGridContouring TriangleGridContouring(TimeSpan time) => new((float)time.TotalSeconds);
        static ContouredLayers ContouredLayers(TimeSpan time) => new((float)time.TotalSeconds, RustyMetal.Value);
        static TerracedHills TerracedHills(TimeSpan time) => new((float)time.TotalSeconds);

        (string ShaderName, Win32Application Application)[] samples;

        unsafe
        {
            // Prepare the mapping of available samples to choose from
            samples = new (string, Win32Application)[]
            {
                (nameof(HelloWorld), new SwapChainApplication<HelloWorld>(&HelloWorld)),
                (nameof(FourColorGradient), new SwapChainApplication<FourColorGradient>(&FourColorGradient)),
                (nameof(ColorfulInfinity), new SwapChainApplication<ColorfulInfinity>(&ColorfulInfinity)),
                (nameof(FractalTiling), new SwapChainApplication<FractalTiling>(&FractalTiling)),
                (nameof(TwoTiledTruchet), new SwapChainApplication<TwoTiledTruchet>(&TwoTiledTruchet)),
                (nameof(MengerJourney), new SwapChainApplication<MengerJourney>(&MengerJourney)),
                (nameof(Octagrams), new SwapChainApplication<Octagrams>(&Octagrams)),
                (nameof(ProteanClouds), new SwapChainApplication<ProteanClouds>(&ProteanClouds)),
                (nameof(ExtrudedTruchetPattern), new SwapChainApplication<ExtrudedTruchetPattern>(&ExtrudedTruchetPattern)),
                (nameof(PyramidPattern), new SwapChainApplication<PyramidPattern>(&PyramidPattern)),
                (nameof(TriangleGridContouring), new SwapChainApplication<TriangleGridContouring>(&TriangleGridContouring)),
                (nameof(ContouredLayers), new SwapChainApplication<ContouredLayers>(&ContouredLayers)),
                (nameof(TerracedHills), new SwapChainApplication<TerracedHills>(&TerracedHills))
            };
        }

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