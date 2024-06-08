using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever [D2DPixelOptions] is used on a shader type to incorrectly request trivial sampling.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2D1PixelOptionsTrivialSamplingOnShaderTypeAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidD2D1PixelOptionsTrivialSamplingOnShaderType];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DPixelOptions], [D2DInputCount] and [D2DInputSimple] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DPixelOptionsAttribute") is not { } d2DPixelOptionsAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputCountAttribute") is not { } d2DInputCountAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputSimpleAttribute") is not { } d2DInputSimpleAttributeSymbol)
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

                // If the type is not using [D2DPixelOptions] with D2D1PixelOptions.TrivialSampling, there's nothing to do
                if (!typeSymbol.TryGetAttributeWithType(d2DPixelOptionsAttributeSymbol, out AttributeData? pixelOptionsAttribute) ||
                    !((D2D1PixelOptions)pixelOptionsAttribute.ConstructorArguments[0].Value!).HasFlag(D2D1PixelOptions.TrivialSampling))
                {
                    return;
                }

                // Make sure we have the [D2DInputCount] (if not present, the shader is invalid anyway) and with a valid value
                if (!typeSymbol.TryGetAttributeWithType(d2DInputCountAttributeSymbol, out AttributeData? inputCountAttribute) ||
                    inputCountAttribute.ConstructorArguments is not [{ Value: >= 0 and < 8 and int inputCount }])
                {
                    return;
                }

                // Emit a diagnostic if the compile options are not valid for the shader type
                if (!D2DPixelShaderDescriptorGenerator.HlslBytecode.IsSimpleInputShader(typeSymbol, d2DInputSimpleAttributeSymbol, inputCount))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidD2D1PixelOptionsTrivialSamplingOnShaderType,
                        pixelOptionsAttribute.GetLocation(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}