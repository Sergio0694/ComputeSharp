using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader dispatch data.
/// </summary>
/// <param name="IsPixelShaderLike">Whether the compute shader is "pixel shader like", ie. outputting a pixel into a target texture.</param>
/// <param name="FieldInfos">The description on shader instance fields.</param>
/// <param name="ResourceCount">The total number of captured resources.</param>
/// <param name="Root32BitConstantCount">The size of the shader root signature, in 32 bit constants.</param>
internal sealed record DispatchDataInfo(
    bool IsPixelShaderLike,
    EquatableArray<FieldInfo> FieldInfos,
    int ResourceCount,
    int Root32BitConstantCount);