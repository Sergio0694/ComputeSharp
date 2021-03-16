using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace ComputeSharp.Exceptions
{
    /// <summary>
    /// A custom <see cref="InvalidOperationException"/> that indicates when a resource was attempted to be created with an unsupported type.
    /// </summary>
    public sealed class UnsupportedDoubleOperationsException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new <see cref="UnsupportedDoubleOperationsException"/> instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnsupportedDoubleOperationsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="UnsupportedTextureTypeException"/> instance from the specified parameters.
        /// </summary>
        /// <param name="type">The element type of resource that was being created.</param>
        /// <returns>A new <see cref="UnsupportedTextureTypeException"/> instance with a formatted error message.</returns>
        [Pure]
        private static UnsupportedDoubleOperationsException Create(Type type)
        {
            StringBuilder builder = new(256);

            builder.AppendLine($"The device in use does not support creating resources of type {type}, as it does not support using double precision floating point numbers.");
            builder.AppendLine("As a possible workaround, consider replacing usage of the double type with single precision floating point numbers.");
            builder.AppendLine("Note that double precision operations are only partially supported on GPU devices, and are far slower than single precision ones.");
            builder.ToString();

            return new(builder.ToString());
        }

        /// <summary>
        /// Throws a new <see cref="UnsupportedDoubleOperationsException"/> instance from the specified parameters.
        /// </summary>
        /// <typeparam name="T">The type of values in the resource that couldn't be created.</typeparam>
        internal static void Throw<T>()
            where T : unmanaged
        {
            throw Create(typeof(T));
        }
    }
}