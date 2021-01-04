using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using ShareX.HelpersLib;

namespace ComputeSharp.ShareX
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(">> Loading image");

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "city.jpg");

            using Bitmap sourceBitmap = new(path);

            Console.WriteLine(">> Blurring on CPU");

            using Bitmap resultBitmapCpu = ConvolutionMatrixManager.GaussianBlur(61, 61, 10).Apply(sourceBitmap);

            string resultPathCpu = Path.Combine(
                Path.GetRelativePath(Path.GetDirectoryName(path)!, @"..\..\..\"),
                $"{Path.GetFileNameWithoutExtension(path)}_cpu{Path.GetExtension(path)}");

            Console.WriteLine(">> Saving image");

            resultBitmapCpu.Save(resultPathCpu);

            Console.WriteLine(">> Blurring on GPU");

            using Bitmap resultBitmapGpu = HlslConvolutionManager.Create(61, 10).Apply(sourceBitmap);

            string resultPathGpu = Path.Combine(
                Path.GetRelativePath(Path.GetDirectoryName(path)!, @"..\..\..\"),
                $"{Path.GetFileNameWithoutExtension(path)}_gpu{Path.GetExtension(path)}");

            Console.WriteLine(">> Saving image");

            resultBitmapGpu.Save(resultPathGpu);
        }
    }
}
