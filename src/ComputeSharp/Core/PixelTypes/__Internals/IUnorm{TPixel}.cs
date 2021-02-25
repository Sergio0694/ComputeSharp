using System;
using System.ComponentModel;

namespace ComputeSharp.__Internals
{
    /// <summary>
    /// Indicates the supported HLSL pixel types to represent a given normalized pixel type.
    /// </summary>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This interface is not intended to be used directly by user code")]
    public interface IUnorm<TPixel>
        where TPixel : unmanaged
    {
    }
}
