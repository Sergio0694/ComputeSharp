using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates diagnostics for invalid uses of [ThreadGroupSize].
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidThreadGroupSizeAttributeUseAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
    [
        MissingThreadGroupSizeAttribute,
        InvalidThreadGroupSizeAttributeDefaultThreadGroupSizes,
        InvalidThreadGroupSizeAttributeValues,
    ];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the IComputeShader, IComputeShader<TPixel> and [ThreadGroupSize] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader") is not { } computeShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader`1") is not { } pixelShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.ThreadGroupSizeAttribute") is not { } threadGroupSizeAttributeSymbol)
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
                if (!MissingComputeShaderDescriptorOnComputeShaderAnalyzer.IsComputeShaderType(typeSymbol, computeShaderSymbol, pixelShaderSymbol))
                {
                    return;
                }

                // Warn if the shader type is not using [ThreadGroupSize]
                if (!typeSymbol.TryGetAttributeWithType(threadGroupSizeAttributeSymbol, out AttributeData? attributeData))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        MissingThreadGroupSizeAttribute,
                        typeSymbol.Locations.First(),
                        typeSymbol));

                    return;
                }

                // If there is a single argument, validate that it is valid
                if (attributeData.ConstructorArguments is [{ Value: var defaultSize }])
                {
                    int? rawDefaultSize = defaultSize as int?;

                    if ((DefaultThreadGroupSizes?)rawDefaultSize is not
                        (DefaultThreadGroupSizes.X or
                         DefaultThreadGroupSizes.Y or
                         DefaultThreadGroupSizes.Z or
                         DefaultThreadGroupSizes.XY or
                         DefaultThreadGroupSizes.XZ or
                         DefaultThreadGroupSizes.YZ or
                         DefaultThreadGroupSizes.XYZ))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            InvalidThreadGroupSizeAttributeDefaultThreadGroupSizes,
                            attributeData.GetLocation(),
                            typeSymbol));
                    }

                    return;
                }

                // If there are three arguments, validate that they are also valid thread group sizes
                if (attributeData.ConstructorArguments is not [{ Value: int threadsX }, { Value: int threadsY }, { Value: int threadsZ }] ||
                    threadsX is < 1 or > 1024 ||
                    threadsY is < 1 or > 1024 ||
                    threadsZ is < 1 or > 64)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidThreadGroupSizeAttributeValues,
                        attributeData.GetLocation(),
                        typeSymbol));

                    return;
                }
            }, SymbolKind.NamedType);
        });
    }
}