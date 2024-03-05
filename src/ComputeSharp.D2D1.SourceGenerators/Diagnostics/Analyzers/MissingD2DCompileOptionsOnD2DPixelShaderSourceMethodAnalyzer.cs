using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates diagnostics when a method with [D2DPixelShaderSource] isn't using [D2DCompileOptions].
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MissingD2DCompileOptionsOnD2DPixelShaderSourceMethodAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [MissingCompileOptionsForD2DPixelShaderSource];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DPixelShaderSource] and [D2DShaderProfile] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DPixelShaderSourceAttribute") is not { } d2DPixelShaderSourceAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute") is not { } d2DCompileOptionsAttributeSymbol)
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

                // Emit a diagnostic if there is no compile options available at any level
                if (!methodSymbol.HasAttributeWithType(d2DCompileOptionsAttributeSymbol) &&
                    !methodSymbol.ContainingAssembly.HasAttributeWithType(d2DCompileOptionsAttributeSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        MissingCompileOptionsForD2DPixelShaderSource,
                        methodSymbol.Locations.First(),
                        methodSymbol.Name,
                        methodSymbol.ContainingType));
                }
            }, SymbolKind.Method);
        });
    }
}