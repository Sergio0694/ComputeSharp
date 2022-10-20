using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a shader method.
/// </summary>
/// <param name="HlslMethodSource">The processed HLSL source for the shader method.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record ShaderMethodInfo(HlslMethodSourceInfo HlslMethodSource, EquatableArray<DiagnosticInfo> Diagnostcs);