using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever the [GeneratedComputeShaderDescriptor] attribute is used on an invalid target type.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidGeneratedComputeShaderDescriptorAttributeTargetAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidGeneratedComputeShaderDescriptorAttributeTarget];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the IComputeShader, IComputeShader<TPixel> and [GeneratedComputeShaderDescriptor] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader") is not { } computeShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader`1") is not { } pixelShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.GeneratedComputeShaderDescriptorAttribute") is not { } generatedComputeShaderDescriptorAttributeSymbol)
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

                // If the current type does not have [GeneratedComputeShaderDescriptor], there is nothing to do
                if (!typeSymbol.TryGetAttributeWithType(generatedComputeShaderDescriptorAttributeSymbol, out AttributeData? attribute))
                {
                    return;
                }

                // If the type is generic or it doesn't implement IComputeShader nor IComputeShader<TPixel>, we emit a diagnostic
                if (typeSymbol.IsGenericType ||
                    !MissingComputeShaderDescriptorOnComputeShaderAnalyzer.IsComputeShaderType(typeSymbol, computeShaderSymbol, pixelShaderSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidGeneratedComputeShaderDescriptorAttributeTarget,
                        attribute.GetLocation(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}