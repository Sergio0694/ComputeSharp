using System;
using System.Drawing;
using TerraFX.Interop;

namespace ComputeSharp.Sample.SwapChain
{
    class Program
    {
        static void Main(string[] args)
        {
            Win32ApplicationRunner.Run<FractalTilesApplication>();
        }
    }

    internal sealed class FractalTilesApplication : Win32Application
    {
        public override string Title => "Fractal tiles";

        public override void Dispose()
        {
        }

        public override void OnInitialize(Size size, HWND hwnd)
        {
        }

        public override void OnResize(Size size)
        {
        }

        public override void OnUpdate(TimeSpan time)
        {
        }
    }
}
