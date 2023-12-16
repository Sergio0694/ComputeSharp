using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever a shader is not precompiled but runtime compilation is not enabled.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class D2DRuntimeCompilationDisabledAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(D2DRuntimeCompilationDisabled);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DShaderProfile], [D2DEnableRuntimeCompilation] and [D2DGeneratedPixelShaderDescriptor] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DShaderProfileAttribute") is not { } d2DShaderProfileAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DEnableRuntimeCompilationAttribute") is not { } d2DEnableRuntimeCompilationAttributeSymbol ||
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

                // Skip the type if it's not a generated shader descriptor target
                if (!typeSymbol.HasAttributeWithType(d2DGeneratedPixelShaderDescriptorAttributeSymbol))
                {
                    return;
                }

                // If the shader is being precompiled, we don't have anything else to do
                if (typeSymbol.HasAttributeWithType(d2DShaderProfileAttributeSymbol) ||
                    typeSymbol.ContainingAssembly.HasAttributeWithType(d2DShaderProfileAttributeSymbol))
                {
                    return;
                }

                // Emit a diagnostic if there is no [D2DEnableRuntimeCompilation] set anywhere
                if (!typeSymbol.HasAttributeWithType(d2DEnableRuntimeCompilationAttributeSymbol) &&
                    !typeSymbol.ContainingAssembly.HasAttributeWithType(d2DEnableRuntimeCompilationAttributeSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        D2DRuntimeCompilationDisabled,
                        typeSymbol.Locations.FirstOrDefault(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}