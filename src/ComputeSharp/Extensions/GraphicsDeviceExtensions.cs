using System;
using ComputeSharp.Graphics;
using ComputeSharp.Shaders;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that contains extension methods for the <see cref="GraphicsDevice"/> type, used to run compute shaders
    /// </summary>
    public static class GraphicsDeviceExtensions
    {
        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="x">The number of threads to run on the X axis</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void For(this GraphicsDevice device, int x, Action<ThreadIds> action) => ShaderRunner.Run(device, x, 1, 1, action);

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="x">The number of threads to run on the X axis</param>
        /// <param name="y">The number of threads to run on the Y axis</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void For(this GraphicsDevice device, int x, int y, Action<ThreadIds> action) => ShaderRunner.Run(device, x, y, 1, action);

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="x">The number of threads to run on the X axis</param>
        /// <param name="y">The number of threads to run on the Y axis</param>
        /// <param name="z">The number of threads to run on the Z axis</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void For(this GraphicsDevice device, int x, int y, int z, Action<ThreadIds> action) => ShaderRunner.Run(device, x, y, z, action);
    }
}
