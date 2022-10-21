using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader dispatch data.
/// </summary>
/// <param name="Type">The shader interface type.</param>
/// <param name="FieldInfos">The description on shader instance fields.</param>
/// <param name="ResourceCount">The total number of captured resources.</param>
/// <param name="Root32BitConstantCount">The size of the shader root signature, in 32 bit constants.</param>
internal sealed record DispatchDataInfo(
    ShaderType Type,
    EquatableArray<FieldInfo> FieldInfos,
    int ResourceCount,
    int Root32BitConstantCount);