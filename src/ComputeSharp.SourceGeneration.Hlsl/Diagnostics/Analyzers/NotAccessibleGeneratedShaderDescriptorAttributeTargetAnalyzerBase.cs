using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ComputeSharp.SourceGeneration.Diagnostics;

/// <summary>
/// A diagnostic analyzer that generates an error if a target shader type is not accessible from its containing assembly.
/// </summary>
/// <param name="diagnosticDescriptor">The <see cref="DiagnosticDescriptor"/> instance to use.</param>
/// <param name="generatedShaderDescriptorFullyQualifiedTypeName">The fully qualified type name of the target attribute.</param>
public abstract class NotAccessibleGeneratedShaderDescriptorAttributeTargetAnalyzerBase(
    DiagnosticDescriptor diagnosticDescriptor,
    string generatedShaderDescriptorFullyQualifiedTypeName) : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [diagnosticDescriptor];

    /// <inheritdoc/>
    public sealed override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(context =>
        {
            // Get the symbol for the target attribute type
            if (context.Compilation.GetTypeByMetadataName(generatedShaderDescriptorFullyQualifiedTypeName) is not { } generatedShaderDescriptorAttributeSymbol)
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

                // Emit a diagnostic if the target type is using the attribute but is not accessible
                if (typeSymbol.TryGetAttributeWithType(generatedShaderDescriptorAttributeSymbol, out AttributeData? attribute) &&
                    !typeSymbol.IsAccessibleFromContainingAssembly(context.Compilation))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        diagnosticDescriptor,
                        attribute.GetLocation(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}