using ComputeSharp.Sample.SwapChain.Backend;
using ComputeSharp.Sample.SwapChain.Shaders;

namespace ComputeSharp.Sample.SwapChain
{
    class Program
    {
        static void Main(string[] args)
        {
            Win32ApplicationRunner.Run(new SwapChainApplication<FractalTiling>(static (texture, time) => new FractalTiling(texture, (float)time.TotalSeconds)));
        }
    }
}
