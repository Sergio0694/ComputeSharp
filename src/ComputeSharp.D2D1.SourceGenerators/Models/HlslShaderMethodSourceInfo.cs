using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL shader method.
/// </summary>
/// <param name="Modifiers">The modifiers for the annotated method (these are <see cref="Microsoft.CodeAnalysis.CSharp.SyntaxKind"/> values, which can't be equated directly).</param>
/// <param name="MethodName">The name of the annotated method.</param>
/// <param name="InvalidReturnType">The fully qualified name of the return type, if invalid.</param>
/// <param name="HlslSource">The HLSL source.</param>
/// <param name="ShaderProfile">The shader profile to use to compile the shader.</param>
/// <param name="CompileOptions">The compile options to use to compile the shader.</param>
/// <param name="HasErrors">Whether any errors have been detected, and therefore the shader compilation should be skipped.</param>
internal sealed record HlslShaderMethodSourceInfo(
    EquatableArray<ushort> Modifiers,
    string MethodName,
    string? InvalidReturnType,
    string HlslSource,
    D2D1ShaderProfile ShaderProfile,
    D2D1CompileOptions CompileOptions,
    bool HasErrors);