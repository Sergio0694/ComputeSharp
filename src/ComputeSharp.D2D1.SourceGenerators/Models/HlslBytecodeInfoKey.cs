namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model with info to be a unique key for <see cref="HlslBytecodeInfo"/> instances.
/// </summary>
/// <param name="HlslSource">The input HLSL source code.</param>
/// <param name="RequestedShaderProfile">The requested shader profile, if available.</param>
/// <param name="RequestedCompileOptions">The requested compile options, if available.</param>
/// <param name="EffectiveShaderProfile">The effective shader profile.</param>
/// <param name="EffectiveCompileOptions">The effective compile options.</param>
/// <param name="HasErrors">Whether any errors were produced when analyzing the shader.</param>
internal sealed record HlslBytecodeInfoKey(
    string HlslSource,
    D2D1ShaderProfile? RequestedShaderProfile,
    D2D1CompileOptions? RequestedCompileOptions,
    D2D1ShaderProfile EffectiveShaderProfile,
    D2D1CompileOptions EffectiveCompileOptions,
    bool HasErrors);