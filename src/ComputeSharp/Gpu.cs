using ComputeSharp.Graphics;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that acts as an entry-point for all the library APIs, exposing the available GPU devices
    /// </summary>
    public static class Gpu
    {
        private static GraphicsDevice _Default;

        /// <summary>
        /// Gets the default <see cref="GraphicsDevice"/> instance for the current machine
        /// </summary>
        public static GraphicsDevice Default => _Default ??= new GraphicsDevice(true);
    }
}
