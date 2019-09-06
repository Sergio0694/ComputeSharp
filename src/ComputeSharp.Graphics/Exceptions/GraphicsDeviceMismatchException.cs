using System;
using System.Diagnostics.Contracts;
using System.Text;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Extensions;

namespace ComputeSharp.Exceptions
{
    /// <summary>
    /// A custom <see cref="GraphicsDeviceMismatchException"/> that indicates when mismatched devices are being used
    /// </summary>
    public sealed class GraphicsDeviceMismatchException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new <see cref="GraphicsDeviceMismatchException"/> instance
        /// </summary>
        internal GraphicsDeviceMismatchException(GraphicsDevice device, GraphicsResource resource) : base(GetExceptionMessage(device, resource)) { }

        // Gets a proper exception message for a pair of device and resource
        [Pure]
        private static string GetExceptionMessage(GraphicsDevice device, GraphicsResource resource)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Invalid pairing of graphics devices used to run a compute shader and allocate memory buffers.");
            builder.AppendLine($"The target device to run the compute shader is \"{device.Name}\" (id: {device.NativeDevice.AdapterLuid}).");
            builder.AppendLine($"The buffer of type {resource.GetType().ToFriendlyString()} was allocated on device \"{resource.GraphicsDevice.Name}\" (id: {resource.GraphicsDevice.NativeDevice.AdapterLuid}).");
            builder.AppendLine("Make sure to always allocate buffers on the same device used to actually run the code that accesses them.");

            return builder.ToString();
        }
    }
}
