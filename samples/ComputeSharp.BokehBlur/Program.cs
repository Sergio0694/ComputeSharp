using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputeSharp.BokehBlur
{
    class Program
    {
        /// <summary>
        /// The path of the image to process (the modified version will be saved as a copy)
        /// </summary>
        private const string ImagePath = @"";

        static void Main()
        {
            // Load the input image
            using Image<Rgb24> image = Image.Load<Rgb24>(ImagePath);
            var (height, width) = (image.Height, image.Width);

            // Get the vector buffer
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

            // Copy the modified image back
            Parallel.For(0, height, i =>
            {
                ref Rgb24 rPixel = ref image.GetPixelRowSpan(i).GetPinnableReference();
                ref Vector4 r4 = ref vectorArray[i * width];

                for (int j = 0; j < width; j++)
                {
                    Unsafe.Add(ref rPixel, i).FromVector4(Unsafe.Add(ref r4, i));
                }
            });

            // Save the resulting image
            string targetPath = Path.Combine(
                Path.GetDirectoryName(ImagePath),
                $"{Path.GetFileNameWithoutExtension(ImagePath)}_bokeh{Path.GetExtension(ImagePath)}");
            image.Save(targetPath);
        }
    }
}
