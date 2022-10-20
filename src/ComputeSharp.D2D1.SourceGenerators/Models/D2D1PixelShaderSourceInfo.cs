using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a [D2D1PixelShaderSource] attribute.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="HlslShaderMethodSource">The processed HLSL source for the shader.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record D2D1PixelShaderSourceInfo(
    HierarchyInfo Hierarchy,
    HlslShaderMethodSourceInfo HlslShaderMethodSource,
    EquatableArray<DiagnosticInfo> Diagnostcs);