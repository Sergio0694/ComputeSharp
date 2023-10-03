using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a [D2D1PixelShaderSource] attribute.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="Modifiers">The modifiers for the annotated method (these are <see cref="Microsoft.CodeAnalysis.CSharp.SyntaxKind"/> values, which can't be equated directly).</param>
/// <param name="MethodName">The name of the annotated method.</param>
/// <param name="InvalidReturnType">The fully qualified name of the return type, if invalid.</param>
/// <param name="HlslInfoKey">The key with processed info on the shader.</param>
/// <param name="HlslInfo">The value with processed info on the shader.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record D2D1PixelShaderSourceInfo(
    HierarchyInfo Hierarchy,
    EquatableArray<ushort> Modifiers,
    string MethodName,
    string? InvalidReturnType,
    HlslBytecodeInfoKey HlslInfoKey,
    HlslBytecodeInfo HlslInfo,
    EquatableArray<DiagnosticInfo> Diagnostcs);