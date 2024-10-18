using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.D2D1.WinUI.SourceGenerators.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.WinUI.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates errors when a property using [GeneratedCanvasEffectProperty] is in an invalid type.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidGeneratedCanvasEffectPropertyContainingTypeAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidGeneratedCanvasEffectPropertyContainingType];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [GeneratedCanvasEffectProperty] and CanvasEffect symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.WinUI.GeneratedCanvasEffectPropertyAttribute") is not { } generatedCanvasEffectPropertyAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.WinUI.CanvasEffect") is not { } canvasEffectSymbol)
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // Validate that we have a valid containing type (also skip implementation parts)
                if (context.Symbol is not IPropertySymbol { ContainingType: { } containingTypeSymbol, PartialDefinitionPart: null })
                {
                    return;
                }

                // If the property is not using [GeneratedCanvasEffectProperty], there's nothing to do
                if (!context.Symbol.TryGetAttributeWithType(generatedCanvasEffectPropertyAttributeSymbol, out AttributeData? generatedCanvasEffectPropertyAttribute))
                {
                    return;
                }

                // Issue a diagnostic if the type doesn't derive from CanvasEffect
                if (!containingTypeSymbol.InheritsFromType(canvasEffectSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidGeneratedCanvasEffectPropertyContainingType,
                        generatedCanvasEffectPropertyAttribute.GetLocation(),
                        context.Symbol));
                }
            }, SymbolKind.Property);
        });
    }
}