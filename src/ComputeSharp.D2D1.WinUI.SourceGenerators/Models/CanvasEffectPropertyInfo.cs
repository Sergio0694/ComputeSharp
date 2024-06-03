using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.D2D1.WinUI.SourceGenerators.Models;

/// <summary>
/// A model representing a generated canvas effect property.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="TypeNameWithNullabilityAnnotations">The type name for the generated property, including nullability annotations.</param>
/// <param name="PropertyName">The generated property name.</param>
/// <param name="IsReferenceTypeOrUnconstraindTypeParameter">Indicates whether the property is of a reference type or an unconstrained type parameter.</param>
/// <param name="InvalidationType">The invalidation type to request.</param>
internal sealed record CanvasEffectPropertyInfo(
    HierarchyInfo Hierarchy,
    string PropertyName,
    string TypeNameWithNullabilityAnnotations,
    bool IsReferenceTypeOrUnconstraindTypeParameter,
    CanvasEffectInvalidationType InvalidationType);
