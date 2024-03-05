using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.D2D1.SourceGenerators.Helpers;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever any shader metadata value attributes are used with an invalid value.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidEffectMetadataValueAnalyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// The set of type names for all metadata attributes.
    /// </summary>
    private static readonly ImmutableArray<string> MetadataAttributeTypeNames =
    [
        "ComputeSharp.D2D1.D2DEffectDisplayNameAttribute",
        "ComputeSharp.D2D1.D2DEffectDescriptionAttribute",
        "ComputeSharp.D2D1.D2DEffectCategoryAttribute",
        "ComputeSharp.D2D1.D2DEffectAuthorAttribute",
    ];

    /// <summary>
    /// The mapping of diagnostics to their attribute type names.
    /// </summary>
    private static readonly ImmutableDictionary<string, DiagnosticDescriptor> AttributeTypeNameToDiagnosticMapping = ImmutableDictionary.CreateRange(
    [
        new KeyValuePair<string, DiagnosticDescriptor>("D2DEffectDisplayNameAttribute", InvalidD2DEffectDisplayNameAttributeValue),
        new KeyValuePair<string, DiagnosticDescriptor>("D2DEffectDescriptionAttribute", InvalidD2DEffectDescriptionAttributeValue),
        new KeyValuePair<string, DiagnosticDescriptor>("D2DEffectCategoryAttribute", InvalidD2DEffectCategoryAttributeValue),
        new KeyValuePair<string, DiagnosticDescriptor>("D2DEffectAuthorAttribute", InvalidD2DEffectAuthorAttributeValue)
    ]);

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
    [
        InvalidD2DEffectDisplayNameAttributeValue,
        InvalidD2DEffectDescriptionAttributeValue,
        InvalidD2DEffectCategoryAttributeValue,
        InvalidD2DEffectAuthorAttributeValue,
    ];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the metadata attribute symbols
            if (!context.Compilation.TryBuildNamedTypeSymbolSet(MetadataAttributeTypeNames, out ImmutableHashSet<INamedTypeSymbol>? typeSymbols))
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // Only struct types and assemblies are possible targets
                if (context.Symbol is not (INamedTypeSymbol { TypeKind: TypeKind.Struct } or IAssemblySymbol))
                {
                    return;
                }

                foreach (AttributeData attributeData in context.Symbol.GetAttributes())
                {
                    // Only check attributes we care about
                    if (attributeData.AttributeClass is not INamedTypeSymbol attributeSymbol ||
                        !typeSymbols.Contains(attributeSymbol))
                    {
                        continue;
                    }

                    // Validate the effect display name
                    if (!D2D1EffectMetadataParser.IsValidEffectMetadataName(attributeData))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            AttributeTypeNameToDiagnosticMapping[attributeSymbol.MetadataName],
                            attributeData.GetLocation()));
                    }

                    return;
                }
            }, SymbolKind.NamedType | SymbolKind.Assembly);
        });
    }
}