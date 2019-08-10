using System;
using System.IO;
using System.Threading.Tasks;
using SharpDX.DXGI;
using Windows.Graphics.Imaging;

namespace DirectX12GameEngine.Graphics
{
    public sealed class Image : IDisposable
    {
        public Memory<byte> Data { get; }

        public ImageDescription Description { get; }

        public int Width => Description.Width;

        public int Height => Description.Height;

        internal Image()
        {
        }

        internal Image(ImageDescription description, Memory<byte> data)
        {
            Description = description;
            Data = data;
        }

        public static async Task<Image> LoadAsync(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await LoadAsync(stream);
        }

        public static async Task<Image> LoadAsync(Stream stream)
        {
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());

            PixelDataProvider pixelDataProvider = await decoder.GetPixelDataAsync(
                decoder.BitmapPixelFormat, decoder.BitmapAlphaMode, new BitmapTransform(),
                ExifOrientationMode.RespectExifOrientation, ColorManagementMode.DoNotColorManage);

            byte[] imageBuffer = pixelDataProvider.DetachPixelData();

            PixelFormat pixelFormat = decoder.BitmapPixelFormat switch
            {
                BitmapPixelFormat.Rgba8 => PixelFormat.R8G8B8A8_UNorm,
                BitmapPixelFormat.Bgra8 => PixelFormat.B8G8R8A8_UNorm,
                _ => throw new NotSupportedException("This format is not supported.")
            };

            ImageDescription description = ImageDescription.New2D((int)decoder.PixelWidth, (int)decoder.OrientedPixelHeight, pixelFormat);

            return new Image(description, imageBuffer);
        }

        public void Dispose()
        {
        }
    }
}
