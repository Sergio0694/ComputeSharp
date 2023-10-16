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
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(InvalidGeneratedPixelShaderDescriptorAttributeTarget);

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

                // If the type implements IComputeShader or IComputeShader<TPixel>, it is valid
                foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
                {
                    if (SymbolEqualityComparer.Default.Equals(interfaceSymbol, computeShaderSymbol) ||
                        SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, pixelShaderSymbol))
                    {
                        return;
                    }
                }

                // If we got here, the type is not valid, so we can emit a diagnostic
                context.ReportDiagnostic(Diagnostic.Create(
                    InvalidGeneratedPixelShaderDescriptorAttributeTarget,
                    attribute.GetLocation(),
                    typeSymbol));
            }, SymbolKind.NamedType);
        });
    }
}