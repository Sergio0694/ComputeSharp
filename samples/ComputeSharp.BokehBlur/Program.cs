using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ComputeSharp.Graphics.Buffers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using Vector4 = System.Numerics.Vector4;

namespace ComputeSharp.BokehBlur
{
    class Program
    {
        /// <summary>
        /// The radius of the bokeh blur effect to apply
        /// </summary>
        private const int Radius = 48;

        /// <summary>
        /// The gamma exposure value to use when applying the effect
        /// </summary>
        private const float Gamma = 3;

        static void Main()
        {
            // Load the input image
            Console.WriteLine(">> Loading image");
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "city.jpg");
            using Image<Rgba32> image = Image.Load(path);
            var (height, width) = (image.Height, image.Width);

            // Get the vector buffer
            Console.WriteLine(">> Getting pixel data");
            Vector4[] vectorArray = new Vector4[height * width];

            // Populate the buffer
            Parallel.For(0, height, i =>
            {
                ref Rgba32 rPixel = ref image.GetPixelRowSpan(i).GetPinnableReference();
                ref Vector4 r4 = ref vectorArray[i * width];

                for (int j = 0; j < width; j++)
                {
                    Vector4 v4 = Unsafe.Add(ref rPixel, j).ToVector4();
                    v4.X = MathF.Pow(v4.X, Gamma);
                    v4.Y = MathF.Pow(v4.Y, Gamma);
                    v4.Z = MathF.Pow(v4.Z, Gamma);
                    Unsafe.Add(ref r4, j) = v4;
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
            using ReadOnlyBuffer<Vector4> image_gpu = Gpu.Default.AllocateReadOnlyBuffer(vectorArray);
            using ReadOnlyBuffer<float> kernel_gpu = Gpu.Default.AllocateReadOnlyBuffer(kernel);
            using ReadWriteBuffer<Vector4> result_gpu = Gpu.Default.AllocateReadWriteBuffer<Vector4>(vectorArray.Length);
            Gpu.Default.For(height, width, id =>
            {
                Vector4 total = Vector4.Zero;

                for (int y = -Radius; y <= Radius; y++)
                {
                    for (int x = -Radius; x <= Radius; x++)
                    {
                        int iy = (int)id.X + y;
                        int jx = (int)id.Y + x;

                        if (iy < 0) iy = -iy;
                        else if (iy > height) iy = 2 * height - iy;
                        if (jx < 0) jx = -jx;
                        else if (jx > width) jx = 2 * width - jx;

                        int ki = Radius - y;
                        int kj = Radius - x;

                        total += image_gpu[(uint)(iy * width + jx)] * kernel_gpu[(uint)(ki * diameter + kj)];
                    }
                }

                result_gpu[(uint)(id.X * width + id.Y)] = total;
            });

            // Copy data back
            Console.WriteLine(">> Copying data back");
            result_gpu.GetData(vectorArray);

            // Copy the modified image back
            Console.WriteLine(">> Storing pixel data");
            Parallel.For(0, height, i =>
            {
                ref Rgba32 rPixel = ref image.GetPixelRowSpan(i).GetPinnableReference();
                ref Vector4 r4 = ref vectorArray[i * width];
                Vector4 low = Vector4.Zero;
                Vector4 high = new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                float invGamma = 1 / Gamma;

                for (int j = 0; j < width; j++)
                {
                    Vector4 v4 = Unsafe.Add(ref r4, j);
                    Vector4 clamp = Vector4.Clamp(v4, low, high);
                    v4.X = MathF.Pow(clamp.X, invGamma);
                    v4.Y = MathF.Pow(clamp.Y, invGamma);
                    v4.Z = MathF.Pow(clamp.Z, invGamma);

                    Unsafe.Add(ref rPixel, j).FromVector4(v4);
                }
            });

            // Save the resulting image
            Console.WriteLine(">> Saving to disk");
            string targetPath = Path.Combine(
                Path.GetRelativePath(Path.GetDirectoryName(path), @"..\..\..\"),
                $"{Path.GetFileNameWithoutExtension(path)}_bokeh{Path.GetExtension(path)}");
            image.Save(targetPath);
        }
    }
}
