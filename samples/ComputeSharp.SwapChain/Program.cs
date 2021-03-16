using System;
using ComputeSharp.SwapChain.Backend;
using ComputeSharp.SwapChain.Shaders;

namespace ComputeSharp.SwapChain
{
    class Program
    {
        /// <summary>
        /// The mapping of available samples to choose from.
        /// </summary>
        private static readonly Win32Application[] Samples = new Win32Application[]
        {
            new SwapChainApplication<ColorfulInfinity>(static (texture, time) => new(texture, (float)time.TotalSeconds)),
            new SwapChainApplication<FractalTiling>(static (texture, time) => new(texture, (float)time.TotalSeconds)),
            new SwapChainApplication<TwoTiledTruchet>(static (texture, time) => new(texture, (float)time.TotalSeconds)),
            new SwapChainApplication<MengerJourney>(static (texture, time) => new(texture, (float)time.TotalSeconds)),
            new SwapChainApplication<Octagrams>(static (texture, time) => new(texture, (float)time.TotalSeconds)),
            new SwapChainApplication<ProteanClouds>(static (texture, time) => new(texture, (float)time.TotalSeconds)),
            new SwapChainApplication<ExtrudedTruchetPattern>(static (texture, time) => new(texture, (float)time.TotalSeconds))
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
    }
}
