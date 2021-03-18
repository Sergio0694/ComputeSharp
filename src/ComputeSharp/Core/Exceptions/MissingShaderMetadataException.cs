using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace ComputeSharp.Exceptions
{
    /// <summary>
    /// A custom <see cref="Exception"/> type that indicates when metadata is missing for a given shader.
    /// </summary>
    public sealed class MissingShaderMetadataException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="MissingShaderMetadataException"/> instance.
        /// </summary>
        /// <param name="error">The error message to include in the exception..</param>
        private MissingShaderMetadataException(string error)
            : base(error)
        {
        }

        /// <summary>
        /// Gets a formatted exception message for a given error.
        /// </summary>
        /// <param name="type">The type of shader that was failed to be loaded.</param>
        /// <returns>A new <see cref="MissingShaderMetadataException"/> instance with a formatted error message.</returns>
        [Pure]
        private static MissingShaderMetadataException Create(Type type)
        {
            StringBuilder builder = new(512);

            builder.AppendLine($"Failed to retrieve the shader metadata for type {type}.");
            builder.AppendLine("This usually indicates that the necessary source generator failed to run. Make sure to be using a IDE with full support " +
                               "for source generators, and that there are no build warnings in the errors window upon building your project. If a source " +
                               "generator fails to run correctly, please open an issue at https://github.com/Sergio0694/ComputeSharp and include a working repro along with it.");         

            return new(builder.ToString());
        }

        /// <summary>
        /// Throws a new <see cref="MissingShaderMetadataException"/> for a given delegate.
        /// </summary>
        internal static void Throw<T>()
        {
            throw Create(typeof(T));
        }
    }
}
