namespace ComputeSharp;

/// <summary>
/// An <see langword="enum"/> representing a supported image format.
/// </summary>
public enum ImageFormat
{
    /// <summary>
    /// The BMP format.
    /// </summary>
    Bmp,

    /// <summary>
    /// The PNG format.
    /// </summary>
    Png,

    /// <summary>
    /// The JPEG format.
    /// </summary>
    Jpeg,

    /// <summary>
    /// The WMP format (also used for .jxr, .hdp, .wdp and .wmp files).
    /// </summary>
    Wmp,

    /// <summary>
    /// The TIFF format.
    /// </summary>
    Tiff,

    /// <summary>
    /// The DDS format.
    /// </summary>
    Dds
}