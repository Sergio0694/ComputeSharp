#if SOURCE_GENERATOR
using static Windows.Win32.Graphics.Direct2D.D2D1_CHANNEL_DEPTH;
#else
using static TerraFX.Interop.DirectX.D2D1_CHANNEL_DEPTH;
#endif

namespace ComputeSharp.D2D1;

/// <summary>
/// Indicates the channel depth of a stage in a Direct2D rendering pipeline.
/// </summary>
/// <remarks>
/// <para>This type exposes the available values in <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/ne-d2d1effectauthor-d2d1_channel_depth"/>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct2d/precision-and-clipping-in-effect-graphs"/>.</para>
/// </remarks>
public enum D2D1ChannelDepth
{
    /// <summary>
    /// The channel depth is the default, and it will be inherited from the inputs.
    /// </summary>
    Default = (int)D2D1_CHANNEL_DEPTH_DEFAULT,

    /// <summary>
    /// The channel depth is 1.
    /// </summary>
    One = (int)D2D1_CHANNEL_DEPTH_1,

    /// <summary>
    /// The channel depth is 4.
    /// </summary>
    Four = (int)D2D1_CHANNEL_DEPTH_4
}