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
        public static void For(this GraphicsDevice device, int x, Action<ThreadIds> action) => device.For(x, 1, 1, action);

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="x">The number of threads to run on the X axis</param>
        /// <param name="y">The number of threads to run on the Y axis</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void For(this GraphicsDevice device, int x, int y, Action<ThreadIds> action) => device.For(x, y, 1, action);

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="x">The number of threads to run on the X axis</param>
        /// <param name="y">The number of threads to run on the Y axis</param>
        /// <param name="z">The number of threads to run on the Z axis</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void For(this GraphicsDevice device, int x, int y, int z, Action<ThreadIds> action)
        {
            int
                threadsX = device.WavefrontSize,
                threadsY = y > 1 ? device.WavefrontSize : 1,
                threadsZ = z > 1 ? device.WavefrontSize : 1,
                groupsX = (x - 1) / device.WavefrontSize + 1,
                groupsY = (y - 1) / device.WavefrontSize + 1,
                groupsZ = (z - 1) / device.WavefrontSize + 1;

            ShaderRunner.Run(device, (threadsX, threadsY, threadsZ), (groupsX, groupsY, groupsZ), action);
        }

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="numThreads">The <see cref="NumThreads"/> value that indicates the number of threads to run in each thread group</param>
        /// <param name="numGroups">The <see cref="NumThreads"/> value that indicates the number of thread groups to dispatch</param>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        public static void For(this GraphicsDevice device, NumThreads numThreads, NumThreads numGroups, Action<ThreadIds> action)
        {
            ShaderRunner.Run(device, numThreads, numGroups, action);
        }
    }
}
