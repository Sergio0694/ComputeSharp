using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever [D2DCompileOptions] is used on a shader type to request linking when not supported.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2D1CompileOptionsEnableLinkingOnShaderTypeAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidD2D1CompileOptionsEnableLinkingOnShaderType];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DCompileOptions], [D2DInputCount] and [D2DInputSimple] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute") is not { } d2DCompileOptionsAttributeSymbol ||
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

                // If the type is not using [D2DCompileOptions] with D2D1CompileOptions.EnableLinking, there's nothing to do
                if (!typeSymbol.TryGetAttributeWithType(d2DCompileOptionsAttributeSymbol, out AttributeData? compileOptionsAttribute) ||
                    !((D2D1CompileOptions)compileOptionsAttribute.ConstructorArguments[0].Value!).HasFlag(D2D1CompileOptions.EnableLinking))
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
                        InvalidD2D1CompileOptionsEnableLinkingOnShaderType,
                        compileOptionsAttribute.GetLocation(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}