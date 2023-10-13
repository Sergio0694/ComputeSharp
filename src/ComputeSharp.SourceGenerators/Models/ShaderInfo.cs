using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a shader.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="DispatchData">The gathered shader dispatch data.</param>
/// <param name="DispatchMetadata">The gathered shader dispatch metadata.</param>
/// <param name="HlslShaderSource">The processed HLSL source for the shader.</param>
/// <param name="ThreadIds">The thread ids for the shader, if compilation is requested.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record ShaderInfo(
    HierarchyInfo Hierarchy,
    DispatchDataInfo DispatchData,
    DispatchMetadataInfo DispatchMetadata,
    HlslShaderSourceInfo HlslShaderSource,
    ThreadIdsInfo ThreadIds,
    EquatableArray<DiagnosticInfo> Diagnostcs);