using ComputeSharp.D2D1.WinUI.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.WinUI.SourceGenerators;

/// <summary>
/// A source generator creating implementations of effect properties.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class CanvasEffectPropertyGenerator : IIncrementalGenerator
{
    /// <summary>
    /// The name of generator to include in the generated code.
    /// </summary>
    private const string GeneratorName = "ComputeSharp.D2D1.WinUI.CanvasEffectPropertyGenerator";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<CanvasEffectPropertyInfo> propertyInfo =
            context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "ComputeSharp.D2D1.WinUI.GeneratedCanvasEffectPropertyAttribute",
                Execute.IsCandidatePropertyDeclaration,
                static (context, token) =>
                {
                    // Ensure we do have a property with a valid containing type
                    if (context.TargetSymbol is not IPropertySymbol { ContainingType: INamedTypeSymbol typeSymbol } propertySymbol)
                    {
                        return default;
                    }

                    // Ensure that the containing type derives from CanvasEffect
                    if (!typeSymbol.InheritsFromFullyQualifiedMetadataName("ComputeSharp.D2D1.WinUI.CanvasEffect"))
                    {
                        return default;
                    }

                    string typeNameWithNullabilityAnnotations = propertySymbol.Type.GetFullyQualifiedNameWithNullabilityAnnotations();
                    bool isOldPropertyValueDirectlyReferenced = Execute.IsOldPropertyValueDirectlyReferenced(propertySymbol);
                    CanvasEffectInvalidationType invalidationType = Execute.GetCanvasEffectInvalidationType(context.Attributes[0]);

                    // We're using IsValueType here and not IsReferenceType to also cover unconstrained type parameter cases.
                    // This will cover both reference types as well T when the constraints are not struct or unmanaged.
                    // If this is true, it means the field storage can potentially be in a null state (even if not annotated).
                    bool isReferenceTypeOrUnconstraindTypeParameter = !propertySymbol.Type.IsValueType;

                    // Finally, get the hierarchy too
                    HierarchyInfo hierarchyInfo = HierarchyInfo.From(typeSymbol);

                    token.ThrowIfCancellationRequested();

                    return new CanvasEffectPropertyInfo(
                        Hierarchy: hierarchyInfo,
                        PropertyName: propertySymbol.Name,
                        TypeNameWithNullabilityAnnotations: typeNameWithNullabilityAnnotations,
                        IsOldPropertyValueDirectlyReferenced: isOldPropertyValueDirectlyReferenced,
                        IsReferenceTypeOrUnconstraindTypeParameter: isReferenceTypeOrUnconstraindTypeParameter,
                        InvalidationType: invalidationType);
                })
            .Where(static item => item is not null)!;

        // Split and group by containing type
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EquatableArray<CanvasEffectPropertyInfo> Properties)> groupedPropertyInfo =
            propertyInfo
            .GroupBy(keySelector: static item => item.Hierarchy, elementSelector: static item => item);

        // Generate the source files, if any
        context.RegisterSourceOutput(groupedPropertyInfo, static (context, item) =>
        {
            using IndentedTextWriter writer = new();

            item.Hierarchy.WriteSyntax(
                state: item.Properties,
                writer: writer,
                baseTypes: [],
                memberCallbacks: [Execute.WritePropertyDeclarations]);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.g.cs", writer.ToString());
        });
    }
}