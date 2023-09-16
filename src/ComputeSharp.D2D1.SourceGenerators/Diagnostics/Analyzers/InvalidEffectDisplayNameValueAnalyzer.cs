using System.Collections.Immutable;
using ComputeSharp.D2D1.SourceGenerators.Helpers;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever [D2DEffectName] is used with an invalid value.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidEffectDisplayNameValueAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(InvalidD2DEffectDisplayNameAttributeValue);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DEffectDisplayName] symbol
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DEffectDisplayNameAttribute") is not { } d2DEffectIdAttributeSymbol)
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // Only struct types are possible targets
                if (context.Symbol is not INamedTypeSymbol { TypeKind: TypeKind.Struct } typeSymbol)
                {
                    return;
                }

                foreach (AttributeData attributeData in typeSymbol.GetAttributes())
                {
                    // Look for the [D2DEffectDisplayName] use
                    if (!SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, d2DEffectIdAttributeSymbol))
                    {
                        continue;
                    }

                    // Validate the effect display name
                    if (!D2D1EffectMetadataParser.IsValidEffectDisplayName(attributeData))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            InvalidD2DEffectDisplayNameAttributeValue,
                            attributeData.GetLocation()));
                    }

                    return;
                }
            }, SymbolKind.NamedType);
        });
    }
}