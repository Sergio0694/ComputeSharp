using System;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever [D2DEffectId] is used with an invalid value.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidEffectIdValueAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidD2DEffectIdAttributeValue];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DEffectId] symbol
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DEffectIdAttribute") is not { } d2DEffectIdAttributeSymbol)
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
                    // Look for the [D2DEffectId] use
                    if (!SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, d2DEffectIdAttributeSymbol))
                    {
                        continue;
                    }

                    // Validate the GUID text and emit a diagnostic if needed
                    if (attributeData.ConstructorArguments is not [{ Value: string value }] ||
                        !Guid.TryParse(value, out _))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            InvalidD2DEffectIdAttributeValue,
                            attributeData.GetLocation()));
                    }

                    return;
                }
            }, SymbolKind.NamedType);
        });
    }
}