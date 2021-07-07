using System;
using System.ComponentModel;

namespace ComputeSharp.__Internals
{
    /// <summary>
    /// A base <see langword="interface"/> representing a given shader that can be dispatched.
    /// </summary>
    /// <typeparam name="TSelf">The type of shader.</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This interface is not intended to be used directly by user code")]
    public interface IShader<TSelf>
        where TSelf : struct, IShader<TSelf>
    {
        /// <summary>
        /// Gets a unique dispatch identifier for the shader.
        /// </summary>
        /// <returns>The unique dispatch identifier for the shader.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This interface is not intended to be used directly by user code")]
        int GetDispatchId();
    }
}
