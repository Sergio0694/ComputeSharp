using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates a warning whenever a compute shader type does not have an associated descriptor.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MissingComputeShaderDescriptorOnComputeShaderAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [MissingComputeShaderDescriptorOnComputeShaderType];

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
                if (!IsComputeShaderType(typeSymbol, computeShaderSymbol, pixelShaderSymbol))
                {
                    return;
                }

                // Emit a diagnostic if the descriptor is missing for the shader type
                if (!HasComputeShaderDescriptorInterface(typeSymbol, computeShaderDescriptorSymbol) &&
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

    /// <summary>
    /// Checks whether a given type is a compute shader type.
    /// </summary>
    /// <param name="typeSymbol">The type to check.</param>
    /// <param name="computeShaderSymbol">The type symbol for <c>IComputeShader</c>.</param>
    /// <param name="pixelShaderSymbol">The type symbol for <c>IComputeShader&lt;TPixel&gt;</c>.</param>
    /// <returns></returns>
    internal static bool IsComputeShaderType(
        INamedTypeSymbol typeSymbol,
        INamedTypeSymbol computeShaderSymbol,
        INamedTypeSymbol pixelShaderSymbol)
    {
        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(interfaceSymbol, computeShaderSymbol) ||
                SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, pixelShaderSymbol))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks whether a given type implements the shader descriptor interface.
    /// </summary>
    /// <param name="typeSymbol">The type to check.</param>
    /// <param name="computeShaderDescriptorSymbol">The type symbol for <c>IComputeShaderDescriptor&lt;T&gt;</c>.</param>
    /// <returns></returns>
    private static bool HasComputeShaderDescriptorInterface(INamedTypeSymbol typeSymbol, INamedTypeSymbol computeShaderDescriptorSymbol)
    {
        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, computeShaderDescriptorSymbol))
            {
                return true;
            }
        }

        return false;
    }
}