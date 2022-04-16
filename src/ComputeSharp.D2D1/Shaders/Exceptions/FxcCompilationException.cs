using System;
using System.Text;

namespace ComputeSharp.D2D1.Exceptions;

/// <summary>
/// A custom <see cref="Exception"/> type that indicates when a shader compilation with the FXC compiler has failed.
/// </summary>
public sealed class FxcCompilationException : Exception
{
    /// <summary>
    /// Creates a new <see cref="FxcCompilationException"/> instance.
    /// </summary>
    /// <param name="error">The error message produced by the DXC compiler.</param>
    internal FxcCompilationException(string error)
        : base(GetExceptionMessage(error))
    {
    }

    /// <summary>
    /// Gets a formatted exception message for a given compilation error.
    /// </summary>
    /// <param name="error">The input compilatin error message from the FXC compiler.</param>
    /// <returns>A formatted error message for a new <see cref="FxcCompilationException"/> instance.</returns>
    private static string GetExceptionMessage(string error)
    {
        StringBuilder builder = new(512);

        builder.AppendLine("The FXC compiler encountered one or more errors while trying to compile the shader:");
        builder.AppendLine();
        builder.AppendLine(error.Trim());
        builder.AppendLine();
        builder.AppendLine("Make sure to only be using supported features by checking the README file in the ComputeSharp repository: https://github.com/Sergio0694/ComputeSharp.");
        builder.Append("If you're sure that your C# shader code is valid, please open an issue an include a working repro and this error message.");            

        return builder.ToString();
    }
}
