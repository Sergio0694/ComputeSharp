using ComputeSharp.D2D1.WinUI.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
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

                    // Finally, get the hierarchy too
                    HierarchyInfo hierarchyInfo = HierarchyInfo.From(typeSymbol);

                    token.ThrowIfCancellationRequested();

                    return new CanvasEffectPropertyInfo(
                        Hierarchy: hierarchyInfo,
                        PropertyName: propertySymbol.Name,
                        TypeNameWithNullabilityAnnotations: typeNameWithNullabilityAnnotations,
                        IsOldPropertyValueDirectlyReferenced: isOldPropertyValueDirectlyReferenced);
                })
            .Where(static item => item is not null)!;
    }
}