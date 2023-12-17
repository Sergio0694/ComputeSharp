using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model representing info on compiled HLSL bytecode, if available.
/// </summary>
internal abstract record HlslBytecodeInfo
{
    /// <summary>
    /// A successfully compiled HLSL shader.
    /// </summary>
    /// <param name="Bytecode">The resulting HLSL bytecode.</param>
    /// <param name="RequiresDoublePrecisionSupport">Whether the shader requires support for double precision operations.</param>
    public sealed record Success(EquatableArray<byte> Bytecode, bool RequiresDoublePrecisionSupport) : HlslBytecodeInfo;

    /// <summary>
    /// An HLSL shader that failed to compile due to a Win32 error.
    /// </summary>
    /// <param name="HResult">The HRESULT for the error.</param>
    /// <param name="Message">The error message from compiling the shader.</param>
    public sealed record Win32Error(int HResult, string Message) : HlslBytecodeInfo;

    /// <summary>
    /// An HLSL shader that failed to compile due to a compiler error.
    /// </summary>
    /// <param name="Message">The error message from compiling the shader.</param>
    public sealed record CompilerError(string Message) : HlslBytecodeInfo;

    /// <summary>
    /// No HLSL bytecode is available (ie. compilation was not requested or some other error was detected).
    /// </summary>
    public sealed record Missing : HlslBytecodeInfo
    {
        /// <summary>
        /// Gets the shared <see cref="Missing"/> instance.
        /// </summary>
        public static Missing Instance { get; } = new();
    }
}