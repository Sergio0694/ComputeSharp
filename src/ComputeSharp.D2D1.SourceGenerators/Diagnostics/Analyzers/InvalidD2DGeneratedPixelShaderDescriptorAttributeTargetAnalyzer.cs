using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever the [D2DGeneratedPixelShaderDescriptor] attribute is used on an invalid target type.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidD2DGeneratedPixelShaderDescriptorAttributeTarget];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the ID2D1PixelShader and [D2DGeneratedPixelShaderDescriptor] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.ID2D1PixelShader") is not { } d2D1PixelShaderSymbol ||
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

                // If the type does not have [D2DGeneratedPixelShaderDescriptor], we can stop here
                if (!typeSymbol.TryGetAttributeWithType(d2DGeneratedPixelShaderDescriptorAttributeSymbol, out AttributeData? attribute))
                {
                    return;
                }

                // Emit a diagnostic if the target type is generic or does not implement ID2D1PixelShader
                if (typeSymbol.IsGenericType || !typeSymbol.HasInterfaceWithType(d2D1PixelShaderSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidD2DGeneratedPixelShaderDescriptorAttributeTarget,
                        attribute.GetLocation(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}