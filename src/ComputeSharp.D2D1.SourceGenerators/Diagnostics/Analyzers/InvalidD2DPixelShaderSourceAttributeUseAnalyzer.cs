using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates diagnostics when a method is using [D2DPixelShaderSource] with an invalid argument.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2DPixelShaderSourceAttributeUseAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidD2DPixelShaderSource];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DPixelShaderSource] symbol
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DPixelShaderSourceAttribute") is not { } d2DPixelShaderSourceAttributeSymbol)
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // Only partial definition methods are possible targets
                if (context.Symbol is not IMethodSymbol { IsPartialDefinition: true } methodSymbol)
                {
                    return;
                }

                // Ignore methods without [D2DPixelShaderSource]
                if (!methodSymbol.TryGetAttributeWithType(d2DPixelShaderSourceAttributeSymbol, out AttributeData? attributeData))
                {
                    return;
                }

                // Emit a diagnostic if there is no valid HLSL source argument
                if (!attributeData.TryGetConstructorArgument(0, out string? hlslSource))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidD2DPixelShaderSource,
                        methodSymbol.Locations.First(),
                        methodSymbol.Name,
                        methodSymbol.ContainingType));
                }
            }, SymbolKind.Method);
        });
    }
}