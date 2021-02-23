using ComputeSharp.Sample.SwapChain.Backend;
using ComputeSharp.Sample.SwapChain.Shaders;

namespace ComputeSharp.Sample.SwapChain
{
    class Program
    {
        static void Main(string[] args)
        {
            Win32ApplicationRunner.Run(new SwapChainApplication<TwoTiledTruchet>(static (texture, time) => new(texture, (float)time.TotalSeconds)));
        }
    }
}
