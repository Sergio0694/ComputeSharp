using System;
using System.IO;
using System.Reflection;
using ComputeSharp.BokehBlur.Processors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.ImageProcessing;

class Program
{
    static void Main()
    {
        Console.WriteLine(">> Loading image");
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "city.jpg");
        using Image<ImageSharpRgba32> image = Image.Load<ImageSharpRgba32>(path);

        // Apply a series of processors and save the results
        foreach (var effect in new (string Name, IImageProcessor Processor)[]
        {
            ("bokeh", new HlslBokehBlurProcessor(80, 2)),
            ("gaussian", new HlslGaussianBlurProcessor(80))
        })
        {
            Console.WriteLine($">> Applying {effect.Name}");
            using Image copy = image.Clone(c => c.ApplyProcessor(effect.Processor));

            Console.WriteLine($">> Saving {effect.Name} to disk");

            string targetPath = Path.GetDirectoryName(path)!;

            while (Path.GetFileName(targetPath) != "ComputeSharp.ImageProcessing")
            {
                targetPath = Path.GetDirectoryName(targetPath)!;
            }

            targetPath = Path.Combine(targetPath, $"{Path.GetFileNameWithoutExtension(path)}_{effect.Name}{Path.GetExtension(path)}");

            copy.Save(targetPath);
        }
    }
}
