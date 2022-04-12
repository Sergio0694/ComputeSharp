using System;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1Interop.Tests.Extensions;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1Interop.Tests.Helpers;

/// <summary>
/// A <see langword="class"/> that uses the WIC APIs to load and decode images.
/// </summary>
internal static class WICHelper
{
    /// <summary>
    /// Loads an <see cref="IWICBitmap"/> instance from a given path.
    /// </summary>
    /// <param name="filename">The path to load the image from.</param>
    /// <param name="width">The resulting image width.</param>
    /// <param name="height">The resulting image height.</param>
    /// <returns>The resulting <see cref="IWICBitmap"/> instance.</returns>
    public static unsafe ComPtr<IWICBitmap> LoadBitmapFromFile(string filename, out uint width, out uint height)
    {
        using ComPtr<IWICImagingFactory2> wicImagingFactory2 = default;

        // Create a Windows Imaging Component (WIC) factory
        Windows.CoCreateInstance(
            rclsid: (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_WICImagingFactory2)),
            pUnkOuter: null,
            dwClsContext: (uint)CLSCTX.CLSCTX_INPROC_SERVER,
            riid: Windows.__uuidof<IWICImagingFactory2>(),
            ppv: (void**)wicImagingFactory2.GetAddressOf()).Assert();

        using ComPtr<IWICBitmapDecoder> wicBitmapDecoder = default;

        // Get the bitmap decoder for the target file
        fixed (char* p = filename)
        {
            wicImagingFactory2.Get()->CreateDecoderFromFilename(
                wzFilename: (ushort*)p,
                pguidVendor: null,
                dwDesiredAccess: Windows.GENERIC_READ,
                metadataOptions: WICDecodeOptions.WICDecodeMetadataCacheOnDemand,
                ppIDecoder: wicBitmapDecoder.GetAddressOf()).Assert();
        }

        using ComPtr<IWICBitmapFrameDecode> wicBitmapFrameDecode = default;

        // Get the first frame of the loaded image (if more are present, they will be ignored)
        wicBitmapDecoder.Get()->GetFrame(0, wicBitmapFrameDecode.GetAddressOf()).Assert();

        fixed (uint* widthPtr = &width)
        fixed (uint* heightPtr = &height)
        {
            // Extract the image size info
            wicBitmapFrameDecode.Get()->GetSize(widthPtr, heightPtr).Assert();
        }

        Guid wicTargetPixelFormatGuid = GUID.GUID_WICPixelFormat32bppPBGRA;
        Guid wicActualPixelFormatGuid;

        // Get the current and target pixel format info
        wicBitmapFrameDecode.Get()->GetPixelFormat(&wicActualPixelFormatGuid).Assert();

        using ComPtr<IWICBitmap> wicBitmap = default;

        // Create the target bitmap
        wicImagingFactory2.Get()->CreateBitmap(
            uiWidth: width,
            uiHeight: height,
            pixelFormat: &wicTargetPixelFormatGuid,
            option: WICBitmapCreateCacheOption.WICBitmapCacheOnLoad,
            ppIBitmap: wicBitmap.GetAddressOf()).Assert();

        WICRect wicRect;
        wicRect.X = 0;
        wicRect.Y = 0;
        wicRect.Width = (int)width;
        wicRect.Height = (int)height;

        using (ComPtr<IWICBitmapLock> wicBitmapLock = default)
        {
            // Lock the bitmap
            wicBitmap.Get()->Lock(
                prcLock: &wicRect,
                flags: (uint)WICBitmapLockFlags.WICBitmapLockWrite,
                ppILock: wicBitmapLock.GetAddressOf()).Assert();

            uint wicBitmapStride;

            // Get the stride
            wicBitmapLock.Get()->GetStride(&wicBitmapStride).Assert();

            uint wicBitmapSize;
            byte* wicBitmapPointer;

            // Get the bitmap size and pointer
            wicBitmapLock.Get()->GetDataPointer(&wicBitmapSize, &wicBitmapPointer).Assert();

            // Check if a pixel format conversion is needed
            if (wicTargetPixelFormatGuid == wicActualPixelFormatGuid)
            {
                // No conversion is needed, just copy the pixels directly
                wicBitmapFrameDecode.Get()->CopyPixels(
                    prc: &wicRect,
                    cbStride: wicBitmapStride,
                    cbBufferSize: wicBitmapSize,
                    pbBuffer: wicBitmapPointer).Assert();
            }
            else
            {
                using ComPtr<IWICFormatConverter> wicFormatConverter = default;

                // Create a format converter
                wicImagingFactory2.Get()->CreateFormatConverter(wicFormatConverter.GetAddressOf()).Assert();

                // Get a format converter to decode the pixel data
                wicFormatConverter.Get()->Initialize(
                    pISource: (IWICBitmapSource*)wicBitmapFrameDecode.Get(),
                    dstFormat: &wicTargetPixelFormatGuid,
                    dither: WICBitmapDitherType.WICBitmapDitherTypeNone,
                    pIPalette: null,
                    alphaThresholdPercent: 0,
                    paletteTranslate: WICBitmapPaletteType.WICBitmapPaletteTypeCustom).Assert();

                // Decode the pixel data into the upload buffer
                wicFormatConverter.Get()->CopyPixels(
                    prc: &wicRect,
                    cbStride: wicBitmapStride,
                    cbBufferSize: wicBitmapSize,
                    pbBuffer: wicBitmapPointer).Assert();
            }
        }

        return wicBitmap.Move();
    }

    /// <summary>
    /// Saves a given image buffer to a target path.
    /// </summary>
    /// <param name="filename">The path to save the image to.</param>
    /// <param name="width">The image width.</param>
    /// <param name="height">The image height.</param>
    /// <param name="strideInBytes">The image stride in bytes.</param>
    /// <param name="buffer">The image pixel buffer.</param>
    public static unsafe void SaveBitmapToFile(string filename, uint width, uint height, uint strideInBytes, byte* buffer)
    {
        using ComPtr<IWICImagingFactory2> wicImagingFactory2 = default;

        // Create a Windows Imaging Component (WIC) factory
        Windows.CoCreateInstance(
            rclsid: (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_WICImagingFactory2)),
            pUnkOuter: null,
            dwClsContext: (uint)CLSCTX.CLSCTX_INPROC_SERVER,
            riid: Windows.__uuidof<IWICImagingFactory2>(),
            ppv: (void**)wicImagingFactory2.GetAddressOf()).Assert();

        using ComPtr<IWICBitmapEncoder> wicBitmapEncoder = default;

        // Create the image encoder
        wicImagingFactory2.Get()->CreateEncoder(
            guidContainerFormat: (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in GUID.GUID_ContainerFormatPng)),
            pguidVendor: null,
            ppIEncoder: wicBitmapEncoder.GetAddressOf()).Assert();

        using ComPtr<IWICStream> wicStream = default;

        // Create and initialize a stream to the target file
        wicImagingFactory2.Get()->CreateStream(wicStream.GetAddressOf()).Assert();

        fixed (char* p = filename)
        {
            // Initialize the stream to the target file
            wicStream.Get()->InitializeFromFilename((ushort*)p, Windows.GENERIC_WRITE);
        }

        // Initialize the encoder
        wicBitmapEncoder.Get()->Initialize(
            pIStream: (IStream*)wicStream.Get(),
            cacheOption: WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache).Assert();

        using ComPtr<IWICBitmapFrameEncode> wicBitmapFrameEncode = default;

        // Create the image frame and initialize it
        wicBitmapEncoder.Get()->CreateNewFrame(wicBitmapFrameEncode.GetAddressOf(), null).Assert();

        Guid wicTargetPixelFormatGuid = GUID.GUID_WICPixelFormat32bppPBGRA;

        // Set the encoding properties
        wicBitmapFrameEncode.Get()->Initialize(null).Assert();
        wicBitmapFrameEncode.Get()->SetSize(width, height).Assert();
        wicBitmapFrameEncode.Get()->SetPixelFormat(&wicTargetPixelFormatGuid).Assert();

        // Encode the target image
        wicBitmapFrameEncode.Get()->WritePixels(
            lineCount: height,
            cbStride: strideInBytes,
            cbBufferSize: strideInBytes * height,
            pbPixels: buffer).Assert();

        // Flush the changes
        wicBitmapFrameEncode.Get()->Commit();
        wicBitmapEncoder.Get()->Commit();
    }
}
