using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Interop;

#pragma warning disable IDE0044, IDE0051

namespace ComputeSharp.D2D1.WinUI.Buffers;

/// <summary>
/// A fixed buffer type containing 16 <see cref="D2D1ResourceTextureManager"/> fields.
/// </summary>
[InlineArray(16)]
internal struct ResourceTextureManagerBuffer
{
    /// <summary>
    /// The <see cref="D2D1ResourceTextureManager"/> instance at index 0.
    /// </summary>
    private D2D1ResourceTextureManager? resourceTextureManager0;
}