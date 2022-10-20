using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing info on a shader method that has requested to be precompiled at build time.
/// </summary>
/// <param name="Modifiers">The modifiers for the annotated method (these are <see cref="Microsoft.CodeAnalysis.CSharp.SyntaxKind"/> values, which can't be equated directly).</param>
/// <param name="MethodName">The name of the annotated method.</param>
/// <param name="InvalidReturnType">The fully qualified name of the return type, if invalid.</param>
/// <param name="HlslSource">The HLSL source.</param>
/// <param name="Bytecode">The compiled shader bytecode, if available.</param>
internal sealed record EmbeddedBytecodeMethodInfo(
    EquatableArray<ushort> Modifiers,
    string MethodName,
    string? InvalidReturnType,
    string HlslSource,
    EquatableArray<byte> Bytecode);