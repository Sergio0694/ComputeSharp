using System;
using ComputeSharp.SwapChain.D2D1.Backend;
using ComputeSharp.SwapChain.Shaders.D2D1;

namespace ComputeSharp.SwapChain;

class Program
{
    /// <summary>
    /// The mapping of available samples to choose from.
    /// </summary>
    private static readonly (string ShaderName, PixelShaderEffect Effect)[] Samples = new (string, PixelShaderEffect)[]
    {
        (nameof(HelloWorld), new PixelShaderEffect.For<HelloWorld>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        (nameof(ColorfulInfinity), new PixelShaderEffect.For<ColorfulInfinity>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        (nameof(FractalTiling), new PixelShaderEffect.For<FractalTiling>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        (nameof(TwoTiledTruchet), new PixelShaderEffect.For<TwoTiledTruchet>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        (nameof(MengerJourney), new PixelShaderEffect.For<MengerJourney>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        (nameof(Octagrams), new PixelShaderEffect.For<Octagrams>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        (nameof(ProteanClouds), new PixelShaderEffect.For<ProteanClouds>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        (nameof(PyramidPattern), new PixelShaderEffect.For<PyramidPattern>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        (nameof(TriangleGridContouring), new PixelShaderEffect.For<TriangleGridContouring>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height)))),
        (nameof(TerracedHills), new PixelShaderEffect.For<TerracedHills>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))))
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

                Win32Application win32Application = new();
                PixelShaderEffect effect = Samples[index].Effect;

                win32Application.Draw += (_, e) =>
                {
                    // Set the effect properties
                    effect.ElapsedTime = e.TotalTime;
                    effect.ScreenWidth = 1280;
                    effect.ScreenHeight = 720;

                    // Draw the effect
                    e.DrawingSession.DrawImage(effect);
                };

                _ = Win32ApplicationRunner.Run(win32Application, "ComputeSharp.SwapChain.D2D1", Samples[index].ShaderName);
            }
        }
        while (index >= 0 && index < Samples.Length);
    }
}