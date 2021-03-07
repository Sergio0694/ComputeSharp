using System;
using System.Diagnostics.Contracts;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.Core.Extensions;

#pragma warning disable CS0618

namespace ComputeSharp.Exceptions
{
    /// <summary>
    /// A custom <see cref="InvalidOperationException"/> that indicates when an HLSL-only API is called from C#
    /// </summary>
    public sealed class MissingMethodSourceException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new <see cref="MissingMethodSourceException"/> instance.
        /// </summary>
        /// <param name="message">The exception message to display.</param>
        private MissingMethodSourceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="MissingMethodSourceException"/> instance from the specified parameters.
        /// </summary>
        /// <param name="method">The target <see cref="Delegate"/> that couldn't be processed.</param>
        /// <returns>A new <see cref="MissingMethodSourceException"/> instance with a formatted error message.</returns>
        [Pure]
        private static MissingMethodSourceException Create(Delegate method)
        {
            StringBuilder builder = new(512);

            builder.AppendLine("The processed source for a method captured as a delegate in a shader was not found.");
            builder.AppendLine($"The delegate was of type {method.GetType()} and it was wrapping the method {method.Method.GetFullName()}.");
            builder.Append($"Make sure that the target method is annotated with [{nameof(ShaderMethodAttribute)}].");
            builder.ToString();

            return new(builder.ToString());
        }

        /// <summary>
        /// Throws a new <see cref="MissingMethodSourceException"/> for a given delegate.
        /// </summary>
        /// <param name="method">The target <see cref="Delegate"/> that couldn't be processed.</param>
        /// <returns>This method always throws and never actually returns anything.</returns>
        internal static ShaderMethodSourceAttribute Throw(Delegate method)
        {
            throw Create(method);
        }
    }
}
