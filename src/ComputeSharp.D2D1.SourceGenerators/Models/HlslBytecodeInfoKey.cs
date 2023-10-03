namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model with info to be a unique key for <see cref="HlslBytecodeInfo"/> instances.
/// </summary>
/// <param name="HlslSource">The input HLSL source code.</param>
/// <param name="ShaderProfile">The shader profile.</param>
/// <param name="CompileOptions">The compile options.</param>
/// <param name="IsCompilationEnabled">Whether compilation should be attempted for the current info.</param>
internal sealed record HlslBytecodeInfoKey(
    string HlslSource,
    D2D1ShaderProfile ShaderProfile,
    D2D1CompileOptions CompileOptions,
    bool IsCompilationEnabled);