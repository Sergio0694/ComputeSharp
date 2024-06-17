using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ComputeSharp.SourceGeneration.Diagnostics;

/// <summary>
/// A diagnostic analyzer that generates an error if the <c>AllowUnsafeBlocks</c> compilation option is not set.
/// </summary>
/// <param name="diagnosticDescriptor">The <see cref="DiagnosticDescriptor"/> instance to use.</param>
/// <param name="generatedShaderDescriptorFullyQualifiedTypeName">The fully qualified type name of the target attribute.</param>
public abstract class MissingAllowUnsafeBlocksCompilationOptionAnalyzerBase(
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
            // If unsafe blocks are allowed, we'll never need to emit a diagnostic
            if (context.Compilation.IsAllowUnsafeBlocksEnabled())
            {
                return;
            }

            // Get the symbol for the target attribute type
            if (context.Compilation.GetTypeByMetadataName(generatedShaderDescriptorFullyQualifiedTypeName) is not { } generatedShaderDescriptorAttributeSymbol)
            {
                return;
            }

            // Check if any types in the compilation are using the trigger attribute
            context.RegisterSymbolAction(context =>
            {
                // Only struct types are possible targets
                if (context.Symbol is not INamedTypeSymbol { TypeKind: TypeKind.Struct } typeSymbol)
                {
                    return;
                }

                // If the target type is not using the attribute, there's nothing left to do
                if (!typeSymbol.TryGetAttributeWithType(generatedShaderDescriptorAttributeSymbol, out AttributeData? attribute))
                {
                    return;
                }

                // Emit the error on the attribute use
                context.ReportDiagnostic(Diagnostic.Create(diagnosticDescriptor, attribute.GetLocation()));
            }, SymbolKind.NamedType);
        });
    }
}