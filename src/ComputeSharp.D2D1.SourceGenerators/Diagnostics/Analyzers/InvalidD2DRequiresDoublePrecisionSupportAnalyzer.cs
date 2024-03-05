using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates diagnostics when [D2DRequiresDoublePrecisionSupport] is used incorrectly.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2DRequiresDoublePrecisionSupportAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidD2DRequiresDoublePrecisionSupportAttribute];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DRequiresDoublePrecisionSupport], [D2DShaderProfile] and [D2DGeneratedPixelShaderDescriptor] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DRequiresDoublePrecisionSupportAttribute") is not { } d2DRequiresDoublePrecisionSupportAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DShaderProfileAttribute") is not { } d2DShaderProfileAttributeSymbol ||
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

                // If the shader is precompiled, there is nothing left to do
                if (typeSymbol.HasAttributeWithType(d2DShaderProfileAttributeSymbol) ||
                    typeSymbol.ContainingAssembly.HasAttributeWithType(d2DShaderProfileAttributeSymbol))
                {
                    return;
                }

                // Emit a diagnostic if the type is not precompiled and has [D2DRequiresDoublePrecisionSupport]
                if (typeSymbol.TryGetAttributeWithType(d2DRequiresDoublePrecisionSupportAttributeSymbol, out AttributeData? attributeData))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidD2DRequiresDoublePrecisionSupportAttribute,
                        attributeData.GetLocation(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}