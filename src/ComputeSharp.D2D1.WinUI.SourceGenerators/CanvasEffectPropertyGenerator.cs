using ComputeSharp.D2D1.WinUI.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Constants;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

                    // Ensure that the property declaration is a partial definition with no implementation
                    if (propertySymbol is not { IsPartialDefinition: true, PartialImplementationPart: null })
                    {
                        return default;
                    }

                    // Also ignore all properties that have an invalid declaration
                    if (propertySymbol.IsStatic || propertySymbol.ReturnsByRef || propertySymbol.ReturnsByRefReadonly || propertySymbol.Type.IsRefLikeType)
                    {
                        return default;
                    }

                    // Ensure that the containing type derives from CanvasEffect
                    if (!typeSymbol.InheritsFromFullyQualifiedMetadataName("ComputeSharp.D2D1.WinUI.CanvasEffect"))
                    {
                        return default;
                    }

                    token.ThrowIfCancellationRequested();

                    // Get the accessibility values, if the property is valid
                    if (!Execute.TryGetAccessibilityModifiers(
                        node: (PropertyDeclarationSyntax)context.TargetNode,
                        symbol: propertySymbol,
                        out Accessibility declaredAccessibility,
                        out Accessibility getterAccessibility,
                        out Accessibility setterAccessibility))
                    {
                        return default;
                    }

                    token.ThrowIfCancellationRequested();

                    string typeNameWithNullabilityAnnotations = propertySymbol.Type.GetFullyQualifiedNameWithNullabilityAnnotations();

                    token.ThrowIfCancellationRequested();

                    CanvasEffectInvalidationType invalidationType = Execute.GetCanvasEffectInvalidationType(context.Attributes[0]);

                    token.ThrowIfCancellationRequested();

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
                        DeclaredAccessibility: declaredAccessibility,
                        GetterAccessibility: getterAccessibility,
                        SetterAccessibility: setterAccessibility,
                        TypeNameWithNullabilityAnnotations: typeNameWithNullabilityAnnotations,
                        IsReferenceTypeOrUnconstraindTypeParameter: isReferenceTypeOrUnconstraindTypeParameter,
                        InvalidationType: invalidationType);
                })
            .WithTrackingName(WellKnownTrackingNames.Execute)
            .Where(static item => item is not null)!;

        // Split and group by containing type
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EquatableArray<CanvasEffectPropertyInfo> Properties)> groupedPropertyInfo =
            propertyInfo
            .GroupBy(keySelector: static item => item.Hierarchy, elementSelector: static item => item)
            .WithTrackingName(WellKnownTrackingNames.Output);

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