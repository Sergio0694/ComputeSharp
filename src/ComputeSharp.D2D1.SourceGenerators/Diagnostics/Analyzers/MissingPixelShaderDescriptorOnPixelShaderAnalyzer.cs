using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates a warning whenever a D2D1 shader type does not have an associated descriptor.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MissingPixelShaderDescriptorOnPixelShaderAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [MissingPixelShaderDescriptorOnPixelShaderType];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the ID2D1PixelShader, ID2D1PixelShaderDescriptor<T> and [D2DGeneratedPixelShaderDescriptor] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.ID2D1PixelShader") is not { } d2D1PixelShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor`1") is not { } d2D1PixelShaderDescriptorSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DGeneratedPixelShaderDescriptorAttribute") is not { } d2DGeneratedPixelShaderDescriptorAttributeSymbol)
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

                // If the type is not a pixel shader type, immediately bail out
                if (!typeSymbol.HasInterfaceWithType(d2D1PixelShaderSymbol))
                {
                    return;
                }

                // Emit a diagnostic if the descriptor is missing for the shader type
                if (!HasD2D1PixelShaderDescriptorInterface(typeSymbol, d2D1PixelShaderDescriptorSymbol) &&
                    !typeSymbol.HasAttributeWithType(d2DGeneratedPixelShaderDescriptorAttributeSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        MissingPixelShaderDescriptorOnPixelShaderType,
                        typeSymbol.Locations.FirstOrDefault(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }

    /// <summary>
    /// Checks whether a given type implements the shader descriptor interface.
    /// </summary>
    /// <param name="typeSymbol">The type to check.</param>
    /// <param name="d2D1PixelShaderDescriptorSymbol">The type symbol for <c>ID2D1PixelShaderDescriptor&lt;T&gt;</c>.</param>
    /// <returns></returns>
    private static bool HasD2D1PixelShaderDescriptorInterface(INamedTypeSymbol typeSymbol, INamedTypeSymbol d2D1PixelShaderDescriptorSymbol)
    {
        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, d2D1PixelShaderDescriptorSymbol))
            {
                return true;
            }
        }

        return false;
    }
}