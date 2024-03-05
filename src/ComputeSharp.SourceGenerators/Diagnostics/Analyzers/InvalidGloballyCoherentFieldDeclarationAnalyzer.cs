using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever the [GloballyCoherent] attribute is used on an invalid target field.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidGloballyCoherentFieldDeclarationAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidGloballyCoherentFieldDeclaration];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the IComputeShader, IComputeShader<TPixel>, ReadWriteBuffer<T> and [GloballyCoherent] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader") is not { } computeShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader`1") is not { } pixelShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.ReadWriteBuffer`1") is not { } readWriteBufferSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.GloballyCoherentAttribute") is not { } globallyCoherentAttributeSymbol)
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                if (context.Symbol is not IFieldSymbol { ContainingType: { } typeSymbol } fieldSymbol)
                {
                    return;
                }

                // If the current type does not have the [GloballyCoherent] attribute, there is nothing to do
                if (!fieldSymbol.TryGetAttributeWithType(globallyCoherentAttributeSymbol, out AttributeData? attribute))
                {
                    return;
                }

                // Emit a diagnostic if the field is not valid (either static, not of ReadWriteBuffer<T> type, or not within a compute shader type)
                if (fieldSymbol.IsStatic ||
                    fieldSymbol.Type is not INamedTypeSymbol { IsGenericType: true } fieldTypeSymbol ||
                    !SymbolEqualityComparer.Default.Equals(fieldTypeSymbol.ConstructedFrom, readWriteBufferSymbol) ||
                    !MissingComputeShaderDescriptorOnComputeShaderAnalyzer.IsComputeShaderType(typeSymbol, computeShaderSymbol, pixelShaderSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidGloballyCoherentFieldDeclaration,
                        attribute.GetLocation(),
                        fieldSymbol));
                }
            }, SymbolKind.Field);
        });
    }
}