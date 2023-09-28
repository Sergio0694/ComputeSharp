using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing info on compiled HLSL bytecode, if available.
/// </summary>
internal abstract record HlslBytecodeInfo
{
    /// <summary>
    /// A successfully compiled HLSL shader.
    /// </summary>
    /// <param name="Bytecode">The resulting HLSL bytecode.</param>
    public sealed record Success(EquatableArray<byte> Bytecode) : HlslBytecodeInfo;

    /// <summary>
    /// An HLSL shader that failed to compile.
    /// </summary>
    /// <param name="Message">The error message from compiling the shader.</param>
    public sealed record Error(string Message) : HlslBytecodeInfo;

    /// <summary>
    /// No HLSL bytecode is available (ie. compilation was not requested).
    /// </summary>
    public sealed record Missing : HlslBytecodeInfo;
}