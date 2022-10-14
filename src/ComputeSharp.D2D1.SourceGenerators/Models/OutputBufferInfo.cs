namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing the output buffer info for a shader.
/// </summary>
/// <param name="BufferPrecision">The buffer precision for the resulting output buffer.</param>
/// <param name="ChannelDepth">The channel depth for the resulting output buffer.</param>
internal sealed record OutputBufferInfo(D2D1BufferPrecision BufferPrecision, D2D1ChannelDepth ChannelDepth);