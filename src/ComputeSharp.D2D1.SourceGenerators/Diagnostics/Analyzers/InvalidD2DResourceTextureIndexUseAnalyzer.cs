using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever [D2DResourceTextureIndex] is used incorrectly to annotate an invalid field.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2DResourceTextureIndexUseAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(InvalidD2DResourceTextureIndexAttributeUse);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterSymbolAction(static context =>
        {
            IFieldSymbol fieldSymbol = (IFieldSymbol)context.Symbol;

            if (fieldSymbol.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DResourceTextureIndexAttribute", out _))
            {
                string metadataName = fieldSymbol.Type.GetFullMetadataName();

                if (!HlslKnownTypes.IsResourceTextureType(metadataName))
                {
                    Diagnostic diagnostic = Diagnostic.Create(
                        InvalidD2DResourceTextureIndexAttributeUse,
                        fieldSymbol.Locations.First(),
                        fieldSymbol.Name,
                        fieldSymbol.ContainingType,
                        fieldSymbol.Type);

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }, SymbolKind.Field);
    }
}