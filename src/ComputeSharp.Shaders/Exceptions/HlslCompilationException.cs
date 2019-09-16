using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace ComputeSharp.Exceptions
{
    /// <summary>
    /// A custom <see cref="GraphicsDeviceMismatchException"/> that indicates when mismatched devices are being used
    /// </summary>
    public sealed class HlslCompilationException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="GraphicsDeviceMismatchException"/> instance
        /// </summary>
        /// <param name="error">The error message produced by the Dxc compiler</param>
        internal HlslCompilationException(string error) : base(GetExceptionMessage(error)) { }

        // Gets a proper exception message for a given compilation error
        [Pure]
        private static string GetExceptionMessage(string error)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("The Dxc compiler encountered an error while trying to compile the shader.");
            builder.AppendLine("Make sure to only be using supported features by checking the README file in the ComputeSharp repository: https://github.com/Sergio0694/ComputeSharp.");
            builder.AppendLine("If you're sure that your C# shader code is valid, please open an issue an include a working repro and the following error message:");
            builder.AppendLine(error.TrimEnd());

            return builder.ToString();
        }
    }
}
