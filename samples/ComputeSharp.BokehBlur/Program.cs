using System;
using System.IO;
using System.Reflection;
using ComputeSharp.BokehBlur.Processor;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ComputeSharp.BokehBlur
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(">> Loading image");
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "city.jpg");
            using Image<Rgba32> image = Image.Load<Rgba32>(path);

            Console.WriteLine(">> Applying blur");
            image.Mutate(c => c.ApplyProcessor(new HlslBokehBlurProcessor()));

            Console.WriteLine(">> Saving to disk");
            string targetPath = Path.Combine(
                Path.GetRelativePath(Path.GetDirectoryName(path), @"..\..\..\"),
                $"{Path.GetFileNameWithoutExtension(path)}_bokeh{Path.GetExtension(path)}");
            image.Save(targetPath);
        }
    }
}
