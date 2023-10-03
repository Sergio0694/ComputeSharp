using System;

namespace ComputeSharp.Exceptions;

/// <summary>
/// A custom <see cref="InvalidOperationException"/> that indicates when an HLSL-only API is called from C#.
/// </summary>
public sealed class InvalidExecutionContextException : InvalidOperationException
{
    /// <summary>
    /// Creates a new <see cref="InvalidExecutionContextException"/> instance.
    /// </summary>
    /// <param name="memberName">The member name of the invoked API.</param>
    internal InvalidExecutionContextException(string memberName)
        : base("The invoked API can only be used from within an HLSL shader.")
    {
        MemberName = memberName;
    }

    /// <summary>
    /// The name of the member that was incorrectly invoked.
    /// </summary>
    public string MemberName { get; }
}