using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates diagnostics when [D2DPixelShaderSource] is used on a method with an incorrect return type.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2DPixelShaderSourceMethodReturnTypeAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidD2DPixelShaderSourceMethodReturnType];

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

            // Also get the constructed ReadOnlySpan<byte> type
            INamedTypeSymbol readOnlySpanSymbol = context.Compilation.GetTypeByMetadataName("System.ReadOnlySpan`1")!;
            INamedTypeSymbol byteSymbol = context.Compilation.GetSpecialType(SpecialType.System_Byte);
            INamedTypeSymbol readOnlySpanOfByteSymbol = readOnlySpanSymbol.Construct(byteSymbol);

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

                // Emit a diagnostic if the return type is not ReadOnlySpan<byte>
                if (!SymbolEqualityComparer.Default.Equals(methodSymbol.ReturnType, readOnlySpanOfByteSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidD2DPixelShaderSourceMethodReturnType,
                        methodSymbol.Locations.First(),
                        methodSymbol.Name,
                        methodSymbol.ContainingType,
                        methodSymbol.ReturnType));
                }
            }, SymbolKind.Method);
        });
    }
}