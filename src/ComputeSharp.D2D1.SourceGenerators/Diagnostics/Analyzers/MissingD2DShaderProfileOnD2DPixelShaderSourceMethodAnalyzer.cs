using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates diagnostics when a method with [D2DPixelShaderSource] isn't using [D2DShaderProfile].
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MissingD2DShaderProfileOnD2DPixelShaderSourceMethodAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [MissingShaderProfileForD2DPixelShaderSource];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DPixelShaderSource] and [D2DShaderProfile] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DPixelShaderSourceAttribute") is not { } d2DPixelShaderSourceAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DShaderProfileAttribute") is not { } d2DShaderProfileAttributeSymbol)
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
                if (!methodSymbol.HasAttributeWithType(d2DPixelShaderSourceAttributeSymbol))
                {
                    return;
                }

                // Emit a diagnostic if there is no shader profile available at any level
                if (!methodSymbol.HasAttributeWithType(d2DShaderProfileAttributeSymbol) &&
                    !methodSymbol.ContainingAssembly.HasAttributeWithType(d2DShaderProfileAttributeSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        MissingShaderProfileForD2DPixelShaderSource,
                        methodSymbol.Locations.First(),
                        methodSymbol.Name,
                        methodSymbol.ContainingType));
                }
            }, SymbolKind.Method);
        });
    }
}