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

        /// <summary>
        /// Loads the dispatch data for the shader.
        /// </summary>
        /// <typeparam name="TDataLoader">The type of data loader being used.</typeparam>
        /// <param name="dataLoader">The <typeparamref name="TDataLoader"/> instance to use to load the data.</param>
        /// <param name="device">The device the shader is being dispatched on.</param>
        /// <param name="x">The number of iterations to run on the X axis.</param>
        /// <param name="y">The number of iterations to run on the Y axis.</param>
        /// <param name="z">The number of iterations to run on the Z axis.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is not intended to be called directly by user code")]
        void LoadDispatchData<TDataLoader>(in TDataLoader dataLoader, GraphicsDevice device, int x, int y, int z)
            where TDataLoader : struct, IDispatchDataLoader;
    }
}
