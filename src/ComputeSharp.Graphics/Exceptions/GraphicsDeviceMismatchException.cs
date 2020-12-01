using System;
using System.Diagnostics.Contracts;
using System.Text;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;

namespace ComputeSharp.Exceptions
{
    /// <summary>
    /// A custom <see cref="InvalidOperationException"/> that indicates when mismatched devices are being used.
    /// </summary>
    public sealed class GraphicsDeviceMismatchException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new <see cref="GraphicsDeviceMismatchException"/> instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        private GraphicsDeviceMismatchException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="GraphicsDeviceMismatchException"/> instance from the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of values in the input buffer.</typeparam>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> that was used.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance that was used.</param>
        /// <returns>A new <see cref="GraphicsDeviceMismatchException"/> instance with a formatted error message.</returns>
        [Pure]
        private static GraphicsDeviceMismatchException Create<T>(Buffer<T> buffer, GraphicsDevice device)
            where T : unmanaged
        {
            StringBuilder builder = new(512);

            builder.AppendLine("Invalid pairing of graphics devices used to run a compute shader and allocate memory buffers.");
            builder.AppendLine($"The target device to run the compute shader is \"{device}\".");
            builder.AppendLine($"The buffer of type {buffer.GetType()} was allocated on device \"{buffer.GraphicsDevice}\".");
            builder.Append("Make sure to always allocate buffers on the same device used to actually run the code that accesses them.");
            builder.ToString();

            return new(builder.ToString());
        }

        /// <summary>
        /// Throws a new <see cref="GraphicsDeviceMismatchException"/> instance from the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of values in the input buffer.</typeparam>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> that was used.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance that was used.</param>
        [Pure]
        internal static void Throw<T>(Buffer<T> buffer, GraphicsDevice device)
            where T : unmanaged
        {
            throw Create(buffer, device);
        }
    }
}