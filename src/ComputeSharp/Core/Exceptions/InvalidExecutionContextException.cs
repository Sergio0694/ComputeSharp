using System;

namespace ComputeSharp.Exceptions
{
    /// <summary>
    /// A custom <see cref="InvalidOperationException"/> that indicates when an HLSL-only API is called from C#
    /// </summary>
    public sealed class InvalidExecutionContextException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new <see cref="InvalidExecutionContextException"/> instance.
        /// </summary>
        /// <param name="name">The name of the caller API.</param>
        internal InvalidExecutionContextException(string name)
            : base($"The API {name} can only be used from within an HLSL shader.")
        {
        }
    }
}
