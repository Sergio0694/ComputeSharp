using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using Vector4 = System.Numerics.Vector4;

namespace ComputeSharp.BokehBlur
{
    class Program
    {
        /// <summary>
        /// The path of the image to process (the modified version will be saved as a copy)
        /// </summary>
        private const string ImagePath = @"";

        /// <summary>
        /// The radius of the bokeh blur effect to apply
        /// </summary>
        private const int Radius = 12;

        static void Main()
        {
            // Load the input image
            Console.WriteLine(">> Loading image");
            using Image<Rgb24> image = Image.Load<Rgb24>(ImagePath);
            var (height, width) = (image.Height, image.Width);

            // Get the vector buffer
            Console.WriteLine(">> Getting pixel data");
            Vector4[] vectorArray = new Vector4[height * width];

            // Populate the buffer
            Parallel.For(0, height, i =>
            {
                ref Rgb24 rPixel = ref image.GetPixelRowSpan(i).GetPinnableReference();
                ref Vector4 r4 = ref vectorArray[i * width];

                for (int j = 0; j < width; j++)
                {
                    Unsafe.Add(ref r4, j) = Unsafe.Add(ref rPixel, i).ToVector4();
                }
            });

            Console.WriteLine(">> Creating kernel");
            int diameter = Radius * 2 + 1;
            float[] kernel = new float[diameter * diameter];
            int ones = 0;
            for (int i = 0; i < diameter; i++)
            {
                for (int j = 0; j < diameter; j++)
                {
                    if (MathF.Sqrt(MathF.Pow(j - Radius, 2) + MathF.Pow(i - Radius, 2)) - 0.1f <= Radius)
                    {
                        kernel[i * diameter + j] = 1;
                        ones++;
                    }
                }
            }

            Console.WriteLine(">> Normalizing kernel");
            for (int i = 0; i < diameter; i++)
                for (int j = 0; j < diameter; j++)
                    kernel[i * diameter + j] /= ones;

            // Apply the effect
            Console.WriteLine(">> Applying effect");
            Vector4[] resultArray = new Vector4[height * width];
            Parallel.For(0, height * width, ij =>
            {
                int i = ij / width;
                int j = ij % width;
                ref Vector4 rin = ref vectorArray[0];
                ref Vector4 rout = ref resultArray[0];
                ref float rk = ref kernel[0];
                Vector4 total = Vector4.Zero;

                for (int y = -Radius; y <= Radius; y++)
                {
                    for (int x = -Radius; x <= Radius; x++)
                    {
                        int iy = i + y;
                        int jx = j + x;

                        if (iy < 0 || iy > height) continue;
                        if (jx < 0 || jx > width) continue;

                        int ki = Radius - y;
                        int kj = Radius - x;

                        total += Unsafe.Add(ref rin, iy * width + jx) * Unsafe.Add(ref rk, ki * diameter + kj);
                    }
                }

                Unsafe.Add(ref rout, i * width + j) = total;
            });

            // Copy the modified image back
            Console.WriteLine(">> Storing pixel data");
            Parallel.For(0, height, i =>
            {
                ref Rgb24 rPixel = ref image.GetPixelRowSpan(i).GetPinnableReference();
                ref Vector4 r4 = ref resultArray[i * width];

                for (int j = 0; j < width; j++)
                {
                    Unsafe.Add(ref rPixel, j).FromVector4(Unsafe.Add(ref r4, j));
                }
            });

            // Save the resulting image
            Console.WriteLine(">> Saving to disk");
            string targetPath = Path.Combine(
                Path.GetDirectoryName(ImagePath),
                $"{Path.GetFileNameWithoutExtension(ImagePath)}_bokeh{Path.GetExtension(ImagePath)}");
            image.Save(targetPath);
        }
    }
}
