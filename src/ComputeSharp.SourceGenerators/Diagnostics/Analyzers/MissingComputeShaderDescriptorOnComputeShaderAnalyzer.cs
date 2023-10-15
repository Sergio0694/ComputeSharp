using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates a warning whenever a compute shader type does not have an associated descriptor.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MissingComputeShaderDescriptorOnComputeShaderAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(MissingComputeShaderDescriptorOnComputeShaderType);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the IComputeShader, IComputeShader<TPixel>, IComputeShaderDescriptor<T> and [GeneratedComputeShaderDescriptor] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader") is not { } computeShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader`1") is not { } pixelShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.Descriptors.IComputeShaderDescriptor`1") is not { } computeShaderDescriptorSymbol ||
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

                // If the type is not a compute shader type, immediately bail out
                if (!typeSymbol.HasInterfaceWithType(computeShaderSymbol) &&
                    !typeSymbol.HasInterfaceWithType(pixelShaderSymbol))
                {
                    return;
                }

                // Emit a diagnostic if the descriptor is missing for the shader type
                if (!typeSymbol.HasInterfaceWithType(computeShaderDescriptorSymbol) &&
                    !typeSymbol.HasAttributeWithType(generatedComputeShaderDescriptorAttributeSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        MissingComputeShaderDescriptorOnComputeShaderType,
                        typeSymbol.Locations.FirstOrDefault(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}