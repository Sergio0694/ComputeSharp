using System;
using System.Diagnostics.Contracts;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Helpers;

/// <summary>
/// A <see langword="class"/> that uses the WIC APIs to load and decode images.
/// </summary>
internal sealed unsafe class WICHelper
{
    /// <summary>
    /// The <see cref="IWICImagingFactory2"/> instance to use to create decoders.
    /// </summary>
    private readonly ComPtr<IWICImagingFactory2> wicImagingFactory2;

    /// <summary>
    /// Creates a new <see cref="WICHelper"/> instance.
    /// </summary>
    private WICHelper()
    {
        using ComPtr<IWICImagingFactory2> wicImagingFactory2 = default;
        Guid wicImagingFactor2Clsid = FX.CLSID_WICImagingFactory2;

        FX.CoCreateInstance(
            &wicImagingFactor2Clsid,
            null,
            (uint)CLSCTX.CLSCTX_INPROC_SERVER,
            FX.__uuidof<IWICImagingFactory2>(),
            wicImagingFactory2.GetVoidAddressOf()).Assert();

        this.wicImagingFactory2 = wicImagingFactory2.Move();
    }

    /// <summary>
    /// Destroys the current <see cref="WICHelper"/> instance.
    /// </summary>
    ~WICHelper()
    {
        this.wicImagingFactory2.Dispose();
    }

    /// <summary>
    /// Gets a <see cref="WICHelper"/> instance to use.
    /// </summary>
    public static WICHelper Instance { get; } = new();

    /// <summary>
    /// Loads an <see cref="UploadTexture2D{T}"/> from a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="filename">The filename of the image file to load and decode into the texture.</param>
    /// <returns>An <see cref="UploadTexture2D{T}"/> instance with the contents of the specified file.</returns>
    [Pure]
    public UploadTexture2D<T> LoadTexture<T>(GraphicsDevice device, ReadOnlySpan<char> filename)
        where T : unmanaged
    {
        using ComPtr<IWICBitmapDecoder> wicBitmapDecoder = default;

        // Get the bitmap decoder for the target file
        fixed (char* p = filename)
        {
            this.wicImagingFactory2.Get()->CreateDecoderFromFilename(
                (ushort*)p,
                null,
                FX.GENERIC_READ,
                WICDecodeOptions.WICDecodeMetadataCacheOnDemand,
                wicBitmapDecoder.GetAddressOf()).Assert();
        }

        return LoadTexture<T>(device, wicBitmapDecoder.Get());
    }

    /// <summary>
    /// Loads an <see cref="UploadTexture2D{T}"/> from a specified buffer.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="span">The buffer with the image data to load and decode into the texture.</param>
    /// <returns>An <see cref="UploadTexture2D{T}"/> instance with the contents of the specified file.</returns>
    [Pure]
    public UploadTexture2D<T> LoadTexture<T>(GraphicsDevice device, ReadOnlySpan<byte> span)
        where T : unmanaged
    {
        using ComPtr<IWICBitmapDecoder> wicBitmapDecoder = default;
        using ComPtr<IWICStream> wicStream = default;

        // Get the bitmap decoder for the target buffer
        fixed (byte* p = span)
        {
            this.wicImagingFactory2.Get()->CreateStream(wicStream.GetAddressOf()).Assert();

            wicStream.Get()->InitializeFromMemory(p, (uint)span.Length).Assert();

            this.wicImagingFactory2.Get()->CreateDecoderFromStream(
                (IStream*)wicStream.Get(),
                null,
                WICDecodeOptions.WICDecodeMetadataCacheOnDemand,
                wicBitmapDecoder.GetAddressOf()).Assert();

            return LoadTexture<T>(device, wicBitmapDecoder.Get());
        }
    }

    /// <summary>
    /// Saves a texture to a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The texture data to save to an image.</param>
    /// <param name="filename">The filename of the image file to save.</param>
    public void SaveTexture<T>(TextureView2D<T> texture, ReadOnlySpan<char> filename)
        where T : unmanaged
    {
        using ComPtr<IWICBitmapEncoder> wicBitmapEncoder = default;
        Guid containerGuid = WICFormatHelper.GetForFilename(filename);

        // Create the image encoder
        this.wicImagingFactory2.Get()->CreateEncoder(
            &containerGuid,
            null,
            wicBitmapEncoder.GetAddressOf()).Assert();

        using ComPtr<IWICStream> wicStream = default;

        // Create and initialize a stream to the target file
        this.wicImagingFactory2.Get()->CreateStream(wicStream.GetAddressOf()).Assert();

        fixed (char* p = filename)
        {
            wicStream.Get()->InitializeFromFilename(
                (ushort*)p,
                FX.GENERIC_WRITE);
        }

        // Initialize the encoder
        wicBitmapEncoder.Get()->Initialize(
            (IStream*)wicStream.Get(),
            WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache).Assert();

        using ComPtr<IWICBitmapFrameEncode> wicBitmapFrameEncode = default;

        // Create the image frame and initialize it
        wicBitmapEncoder.Get()->CreateNewFrame(wicBitmapFrameEncode.GetAddressOf(), null).Assert();

        wicBitmapFrameEncode.Get()->Initialize(null).Assert();
        wicBitmapFrameEncode.Get()->SetSize((uint)texture.Width, (uint)texture.Height).Assert();

        // Depending on the target format and the current pixel type, we need to check whether
        // an intermediate encoding step is necessary. This is because not all pixel formats
        // are directly supported by the native image encoders present in Windows.
        if (WICFormatHelper.TryGetIntermediateFormatForType<T>(containerGuid, out Guid intermediateGuid))
        {
            T* data = texture.DangerousGetAddressAndByteStride(out int strideInBytes);

            using ComPtr<IWICBitmap> wicBitmap = default;
            Guid initialGuid = WICFormatHelper.GetForType<T>();

            // Create a bitmap wrapping the input texture
            this.wicImagingFactory2.Get()->CreateBitmapFromMemory(
                (uint)texture.Width,
                (uint)texture.Height,
                &initialGuid,
                (uint)strideInBytes,
                (uint)(strideInBytes * texture.Height),
                (byte*)data,
                wicBitmap.GetAddressOf()).Assert();

            using ComPtr<IWICFormatConverter> wicFormatConverter = default;

            this.wicImagingFactory2.Get()->CreateFormatConverter(wicFormatConverter.GetAddressOf()).Assert();

            // Get a format converter to encode the pixel data
            wicFormatConverter.Get()->Initialize(
                (IWICBitmapSource*)wicBitmap.Get(),
                &intermediateGuid,
                WICBitmapDitherType.WICBitmapDitherTypeNone,
                null,
                0,
                WICBitmapPaletteType.WICBitmapPaletteTypeMedianCut).Assert();

            // Write the encoded image data to the frame
            wicBitmapFrameEncode.Get()->SetPixelFormat(&intermediateGuid).Assert();
            wicBitmapFrameEncode.Get()->WriteSource(
                (IWICBitmapSource*)wicBitmap.Get(),
                null).Assert();
        }
        else
        {
            T* data = texture.DangerousGetAddressAndByteStride(out int strideInBytes);
            Guid initialGuid = WICFormatHelper.GetForType<T>();

            // Write the image data to the frame
            wicBitmapFrameEncode.Get()->SetPixelFormat(&initialGuid).Assert();
            wicBitmapFrameEncode.Get()->WritePixels(
                lineCount: (uint)texture.Height,
                cbStride: (uint)strideInBytes,
                cbBufferSize: (uint)(strideInBytes * texture.Height),
                pbPixels: (byte*)data).Assert();
        }            

        wicBitmapFrameEncode.Get()->Commit();
        wicBitmapEncoder.Get()->Commit();
    }

    /// <summary>
    /// Loads an <see cref="UploadTexture2D{T}"/> from a specified <see cref="IWICBitmapDecoder"/> object.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="wicBitmapDecoder">The <see cref="IWICBitmapDecoder"/> object in use.</param>
    /// <returns>An <see cref="UploadTexture2D{T}"/> instance with the contents of the specified file.</returns>
    [Pure]
    private UploadTexture2D<T> LoadTexture<T>(GraphicsDevice device, IWICBitmapDecoder* wicBitmapDecoder)
        where T : unmanaged
    {
        using ComPtr<IWICBitmapFrameDecode> wicBitmapFrameDecode = default;

        // Get the first frame of the loaded image (if more are present, they will be ignored)
        wicBitmapDecoder->GetFrame(0, wicBitmapFrameDecode.GetAddressOf()).Assert();

        uint width, height;

        // Extract the image size info
        wicBitmapFrameDecode.Get()->GetSize(&width, &height).Assert();

        Guid
            wicTargetPixelFormatGuid = WICFormatHelper.GetForType<T>(),
            wicActualPixelFormatGuid;

        // Get the current and target pixel format info
        wicBitmapFrameDecode.Get()->GetPixelFormat(&wicActualPixelFormatGuid).Assert();

        // If the current pixel format is the same as the target one, we can just read the
        // decoded pixel data directly. Otherwise, we need a pixel format conversion step.
        if (wicTargetPixelFormatGuid == wicActualPixelFormatGuid)
        {
            // Allocate an upload texture to transfer the decoded pixel data
            UploadTexture2D<T> upload = device.AllocateUploadTexture2D<T>((int)width, (int)height);

            T* data = upload.View.DangerousGetAddressAndByteStride(out int strideInBytes);

            // Copy the decoded pixels directly from the loaded image
            wicBitmapFrameDecode.Get()->CopyPixels(
                prc: null,
                cbStride: (uint)strideInBytes,
                cbBufferSize: (uint)strideInBytes * height,
                pbBuffer: (byte*)data).Assert();

            return upload;
        }
        else
        {
            using ComPtr<IWICFormatConverter> wicFormatConverter = default;

            this.wicImagingFactory2.Get()->CreateFormatConverter(wicFormatConverter.GetAddressOf()).Assert();

            // Get a format converter to decode the pixel data
            wicFormatConverter.Get()->Initialize(
                (IWICBitmapSource*)wicBitmapFrameDecode.Get(),
                &wicTargetPixelFormatGuid,
                WICBitmapDitherType.WICBitmapDitherTypeNone,
                null,
                0,
                WICBitmapPaletteType.WICBitmapPaletteTypeMedianCut).Assert();

            // Allocate an upload texture to transfer the converted pixel data
            UploadTexture2D<T> upload = device.AllocateUploadTexture2D<T>((int)width, (int)height);

            T* data = upload.View.DangerousGetAddressAndByteStride(out int strideInBytes);

            // Decode the pixel data into the upload buffer
            wicFormatConverter.Get()->CopyPixels(
                prc: null,
                cbStride: (uint)strideInBytes,
                cbBufferSize: (uint)strideInBytes * height,
                pbBuffer: (byte*)data).Assert();

            return upload;
        }
    }
}
