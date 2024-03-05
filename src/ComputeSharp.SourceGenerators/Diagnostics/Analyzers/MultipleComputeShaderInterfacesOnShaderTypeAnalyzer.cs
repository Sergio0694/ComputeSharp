using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error when a shader type using [GeneratedComputeShaderDescriptor] is implementing multiple shader interfaces.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MultipleComputeShaderInterfacesOnShaderTypeAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [MultipleShaderTypesImplemented];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the IComputeShader, IComputeShader<TPixel>, [GeneratedComputeShaderDescriptor] symbols
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

                // We only want to check types using [GeneratedComputeShaderDescriptor]
                if (!typeSymbol.HasAttributeWithType(generatedComputeShaderDescriptorAttributeSymbol))
                {
                    return;
                }

                int shaderInferfaceCount = 0;

                // Count how many shader interfaces are implemented
                foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
                {
                    if (SymbolEqualityComparer.Default.Equals(interfaceSymbol, computeShaderSymbol) ||
                        SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, pixelShaderSymbol))
                    {
                        shaderInferfaceCount++;
                    }
                }

                // Emit a diagnostic if there's more than one
                if (shaderInferfaceCount > 1)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        MultipleShaderTypesImplemented,
                        typeSymbol.Locations.FirstOrDefault(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}