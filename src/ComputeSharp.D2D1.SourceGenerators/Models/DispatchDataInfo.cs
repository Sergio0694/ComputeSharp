using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader dispatch data.
/// </summary>
/// <param name="FieldInfos">The description on shader instance fields.</param>
/// <param name="ConstantBufferSizeInBytes">The size of the shader constant buffer.</param>
internal sealed record DispatchDataInfo(EquatableArray<FieldInfo> FieldInfos, int ConstantBufferSizeInBytes);