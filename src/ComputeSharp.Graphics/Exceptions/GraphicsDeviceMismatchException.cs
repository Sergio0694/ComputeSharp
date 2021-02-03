using System;
using System.Diagnostics.Contracts;
using System.Text;
using ComputeSharp.Interop;
using ComputeSharp.Resources;

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
        /// <param name="resource">The input <see cref="NativeObject"/> that was used.</param>
        /// <param name="sourceDevice">The source <see cref="GraphicsDevice"/> instance tied to <paramref name="resource"/>.</param>
        /// <param name="destinationDevice">The target <see cref="GraphicsDevice"/> instance that was used.</param>
        /// <returns>A new <see cref="GraphicsDeviceMismatchException"/> instance with a formatted error message.</returns>
        /// <remarks>
        /// This method only takes a <see cref="NativeObject"/> instance and the associated <see cref="GraphicsDevice"/> instance as
        /// <see cref="object.GetType"/> will still be available, but without the unnecessary generic type specializations for the method.
        /// </remarks>
        [Pure]
        private static GraphicsDeviceMismatchException Create(NativeObject resource, GraphicsDevice sourceDevice, GraphicsDevice destinationDevice)
        {
            StringBuilder builder = new(512);

            builder.AppendLine("Invalid pairing of graphics devices used to run a compute shader and allocate memory buffers.");
            builder.AppendLine($"The target device to run the compute shader is \"{destinationDevice}\".");
            builder.AppendLine($"The buffer of type {resource.GetType()} was allocated on device \"{sourceDevice}\".");
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
        internal static void Throw<T>(Buffer<T> buffer, GraphicsDevice device)
            where T : unmanaged
        {
            throw Create(buffer, buffer.GraphicsDevice, device);
        }

        /// <summary>
        /// Throws a new <see cref="GraphicsDeviceMismatchException"/> instance from the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of values in the input buffer.</typeparam>
        /// <param name="texture">The input <see cref="Texture2D{T}"/> that was used.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance that was used.</param>
        internal static void Throw<T>(Texture2D<T> texture, GraphicsDevice device)
            where T : unmanaged
        {
            throw Create(texture, texture.GraphicsDevice, device);
        }

        /// <summary>
        /// Throws a new <see cref="GraphicsDeviceMismatchException"/> instance from the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of values in the input buffer.</typeparam>
        /// <param name="texture">The input <see cref="Texture3D{T}"/> that was used.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance that was used.</param>
        internal static void Throw<T>(Texture3D<T> texture, GraphicsDevice device)
            where T : unmanaged
        {
            throw Create(texture, texture.GraphicsDevice, device);
        }

        /// <summary>
        /// Throws a new <see cref="GraphicsDeviceMismatchException"/> instance from the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of values in the input buffer.</typeparam>
        /// <param name="buffer">The input <see cref="TransferBuffer{T}"/> that was used.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance that was used.</param>
        internal static void Throw<T>(TransferBuffer<T> buffer, GraphicsDevice device)
            where T : unmanaged
        {
            throw Create(buffer, buffer.GraphicsDevice, device);
        }

        /// <summary>
        /// Throws a new <see cref="GraphicsDeviceMismatchException"/> instance from the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of values in the input buffer.</typeparam>
        /// <param name="texture">The input <see cref="TransferTexture2D{T}"/> that was used.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance that was used.</param>
        internal static void Throw<T>(TransferTexture2D<T> texture, GraphicsDevice device)
            where T : unmanaged
        {
            throw Create(texture, texture.GraphicsDevice, device);
        }

        /// <summary>
        /// Throws a new <see cref="GraphicsDeviceMismatchException"/> instance from the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of values in the input buffer.</typeparam>
        /// <param name="texture">The input <see cref="TransferTexture3D{T}"/> that was used.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance that was used.</param>
        internal static void Throw<T>(TransferTexture3D<T> texture, GraphicsDevice device)
            where T : unmanaged
        {
            throw Create(texture, texture.GraphicsDevice, device);
        }
    }
}