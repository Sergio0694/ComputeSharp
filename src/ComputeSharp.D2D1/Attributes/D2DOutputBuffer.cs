using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader with a description on the resulting output buffer.
/// Using this attribute is optional when defining a D2D1 shader.
/// </summary>
/// <remarks>
/// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct2d/precision-and-clipping-in-effect-graphs"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DOutputBufferAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="D2DOutputBufferAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="bufferPrecision">The buffer precision for the resulting output buffer.</param>
    public D2DOutputBufferAttribute(D2D1BufferPrecision bufferPrecision)
    {
        BufferPrecision = bufferPrecision;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="D2DOutputBufferAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="channelDepth">The channel depth for the resulting output buffer.</param>
    public D2DOutputBufferAttribute(D2D1ChannelDepth channelDepth)
    {
        ChannelDepth = channelDepth;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="D2DOutputBufferAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="bufferPrecision">The buffer precision for the resulting output buffer.</param>
    /// <param name="channelDepth">The channel depth for the resulting output buffer.</param>
    public D2DOutputBufferAttribute(D2D1BufferPrecision bufferPrecision, D2D1ChannelDepth channelDepth)
    {
        BufferPrecision = bufferPrecision;
        ChannelDepth = channelDepth;
    }

    /// <summary>
    /// Gets the buffer precision for the resulting output buffer.
    /// </summary>
    public D2D1BufferPrecision BufferPrecision { get; }

    /// <summary>
    /// Gets the channel depth for the resulting output buffer.
    /// </summary>
    public D2D1ChannelDepth ChannelDepth { get; }
}
