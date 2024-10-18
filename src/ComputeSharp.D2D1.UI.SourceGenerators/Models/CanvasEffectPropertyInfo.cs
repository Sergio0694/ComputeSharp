using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.SourceGenerators.Models;
#else
namespace ComputeSharp.D2D1.WinUI.SourceGenerators.Models;
#endif

/// <summary>
/// A model representing a generated canvas effect property.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="PropertyName">The generated property name.</param>
/// <param name="DeclaredAccessibility">The accessibility of the property, if available.</param>
/// <param name="GetterAccessibility">The accessibility of the <see langword="get"/> accessor, if available.</param>
/// <param name="SetterAccessibility">The accessibility of the <see langword="set"/> accessor, if available.</param>
/// <param name="TypeNameWithNullabilityAnnotations">The type name for the generated property, including nullability annotations.</param>
/// <param name="IsReferenceTypeOrUnconstraindTypeParameter">Indicates whether the property is of a reference type or an unconstrained type parameter.</param>
/// <param name="InvalidationType">The invalidation type to request.</param>
internal sealed record CanvasEffectPropertyInfo(
    HierarchyInfo Hierarchy,
    string PropertyName,
    Accessibility DeclaredAccessibility,
    Accessibility GetterAccessibility,
    Accessibility SetterAccessibility,
    string TypeNameWithNullabilityAnnotations,
    bool IsReferenceTypeOrUnconstraindTypeParameter,
    CanvasEffectInvalidationType InvalidationType);
