using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// An interface for a model providing info on constant buffer fields.
/// </summary>
internal interface IConstantBufferInfo
{
    /// <summary>
    /// The hierarchy info for the shader type.
    /// </summary>
    HierarchyInfo Hierarchy { get; }

    /// <summary>
    /// The size of the shader constant buffer.
    /// </summary>
    int ConstantBufferSizeInBytes { get; }

    /// <summary>
    /// The description on shader instance fields.
    /// </summary>
    EquatableArray<FieldInfo> Fields { get; }
}