using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates diagnostics for the [D2DEnableRuntimeCompilation] attribute, when used on a type.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class D2DEnableRuntimeCompilationOnTypeAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [D2DRuntimeCompilationDisabled, D2DRuntimeCompilationAlreadyEnabled, D2DRuntimeCompilationOnTypeNotNecessary];

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

                // This analyzer will detect the following cases:
                //   1) Shader not precompiled, and missing [D2DEnableRuntimeCompilation] (D2DRuntimeCompilationDisabled)
                //   2) Shader with [D2DEnableRuntimeCompilation] on the assembly and also on the shader type (D2DRuntimeCompilationAlreadyEnabled)
                //   3) Shader being precompiled, but also with [D2DEnableRuntimeCompilation] (D2DRuntimeCompilationNotNecessary)
                bool hasD2DShaderProfileAttributeOnAssembly = typeSymbol.ContainingAssembly.HasAttributeWithType(d2DShaderProfileAttributeSymbol);
                bool hasD2DShaderProfileAttributeOnType = typeSymbol.HasAttributeWithType(d2DShaderProfileAttributeSymbol);
                bool hasD2DEnableRuntimeCompilationAttributeOnAssembly = typeSymbol.ContainingAssembly.HasAttributeWithType(d2DEnableRuntimeCompilationAttributeSymbol);
                bool hasD2DEnableRuntimeCompilationAttributeOnType = typeSymbol.TryGetAttributeWithType(
                    d2DEnableRuntimeCompilationAttributeSymbol,
                    out AttributeData? d2DEnableRuntimeCompilationAttributeData);

                // If [D2DEnableRuntimeCompilation] is present on the type and also on the assembly, emit a diagnostic (2)
                if (hasD2DEnableRuntimeCompilationAttributeOnAssembly && hasD2DEnableRuntimeCompilationAttributeOnType)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        D2DRuntimeCompilationAlreadyEnabled,
                        d2DEnableRuntimeCompilationAttributeData!.GetLocation(),
                        typeSymbol));
                }

                // Check if the shader is precompiled:
                //   - If it is, we should check that it's not annotated with [D2DEnableRuntimeCompilation]
                //   - If it is not, we should check that it is annotated with [D2DEnableRuntimeCompilation]
                if (hasD2DShaderProfileAttributeOnType || hasD2DShaderProfileAttributeOnAssembly)
                {
                    // Emit a diagnostic if [D2DEnableRuntimeCompilation] is present on the type (3)
                    if (hasD2DEnableRuntimeCompilationAttributeOnType)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            D2DRuntimeCompilationOnTypeNotNecessary,
                            d2DEnableRuntimeCompilationAttributeData!.GetLocation(),
                            typeSymbol));
                    }
                }
                else if (!hasD2DEnableRuntimeCompilationAttributeOnType && !hasD2DEnableRuntimeCompilationAttributeOnAssembly)
                {
                    // Emit a diagnostic if there is no [D2DEnableRuntimeCompilation] set anywhere (1)
                    context.ReportDiagnostic(Diagnostic.Create(
                        D2DRuntimeCompilationDisabled,
                        typeSymbol.Locations.FirstOrDefault(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}