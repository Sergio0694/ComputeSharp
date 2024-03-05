using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever [D2DResourceTextureIndex] is used incorrectly to annotate a field.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2DResourceTextureIndexAttributeLocationAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidD2DResourceTextureIndexAttributeLocation];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DResourceTextureIndex] symbol, and the ones for the D2D resource texture types
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DResourceTextureIndexAttribute") is not { } d2DResourceTextureIndexAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2D1ResourceTexture1D`1") is not { } d2D1ResourceTexture1DSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2D1ResourceTexture2D`1") is not { } d2D1ResourceTexture2DSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2D1ResourceTexture3D`1") is not { } d2D1ResourceTexture3DSymbol)
            {
                return;
            }

            // Inspect all fields to make sure they're not using the attribute incorrectly
            context.RegisterSymbolAction(context =>
            {
                IFieldSymbol fieldSymbol = (IFieldSymbol)context.Symbol;

                if (fieldSymbol.HasAttributeWithType(d2DResourceTextureIndexAttributeSymbol))
                {
                    // If the field is using [D2DResourceTextureIndex], it must be a D2D resource texture. We can first
                    // just check that the field type is a generic named type (since that's the case if it's indeed of
                    // a D2D resource texture type). Past that, we just check that it's one of the three possible types.
                    if (fieldSymbol.Type is not INamedTypeSymbol { IsGenericType: true } typeSymbol ||
                        !(SymbolEqualityComparer.Default.Equals(typeSymbol.ConstructedFrom, d2D1ResourceTexture1DSymbol) ||
                          SymbolEqualityComparer.Default.Equals(typeSymbol.ConstructedFrom, d2D1ResourceTexture2DSymbol) ||
                          SymbolEqualityComparer.Default.Equals(typeSymbol.ConstructedFrom, d2D1ResourceTexture3DSymbol)))
                    {
                        Diagnostic diagnostic = Diagnostic.Create(
                            InvalidD2DResourceTextureIndexAttributeLocation,
                            fieldSymbol.Locations.First(),
                            fieldSymbol.Name,
                            fieldSymbol.ContainingType,
                            fieldSymbol.Type);

                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }, SymbolKind.Field);
        });
    }
}