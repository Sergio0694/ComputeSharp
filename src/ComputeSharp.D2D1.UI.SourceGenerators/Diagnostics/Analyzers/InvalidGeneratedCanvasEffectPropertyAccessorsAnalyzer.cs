using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
#if WINDOWS_UWP
using static ComputeSharp.D2D1.Uwp.SourceGenerators.DiagnosticDescriptors;
#else
using static ComputeSharp.D2D1.WinUI.SourceGenerators.DiagnosticDescriptors;
#endif

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.SourceGenerators;
#else
namespace ComputeSharp.D2D1.WinUI.SourceGenerators;
#endif

/// <summary>
/// A diagnostic analyzer that generates errors when a property using [GeneratedCanvasEffectProperty] has invalid accessors.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidGeneratedCanvasEffectPropertyAccessorsAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidGeneratedCanvasEffectPropertyAccessors];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [GeneratedCanvasEffectProperty] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.WinUI.GeneratedCanvasEffectPropertyAttribute") is not { } generatedCanvasEffectPropertyAttributeSymbol)
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // Check that the property doesn't have 'get' and 'set' accessors
                if (context.Symbol is IPropertySymbol { GetMethod: { }, SetMethod.IsInitOnly: false })
                {
                    return;
                }

                // Skip implementation parts to avoid duplicate errors
                if (context.Symbol is IPropertySymbol { PartialDefinitionPart: not null })
                {
                    return;
                }

                // If the property is using [GeneratedCanvasEffectProperty], emit the diagnostic
                if (context.Symbol.TryGetAttributeWithType(generatedCanvasEffectPropertyAttributeSymbol, out AttributeData? generatedCanvasEffectPropertyAttribute))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidGeneratedCanvasEffectPropertyAccessors,
                        generatedCanvasEffectPropertyAttribute.GetLocation(),
                        context.Symbol));
                }
            }, SymbolKind.Property);
        });
    }
}