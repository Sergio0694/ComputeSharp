using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates errors when a D2D pixel shader exceeds the maximum dispatch data size.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ExceededPixelShaderDispatchDataSizeAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [ExceededDispatchDataSize];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the ID2D1PixelShader symbol
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.ID2D1PixelShader") is not { } d2D1PixelShaderSymbol)
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // We're validating all struct types (since they're the possible shader types)
                if (context.Symbol is not INamedTypeSymbol { TypeKind: TypeKind.Struct } typeSymbol)
                {
                    return;
                }

                // Only validate fields for shader types (even without a generated descriptor)
                if (!typeSymbol.HasInterfaceWithType(d2D1PixelShaderSymbol))
                {
                    return;
                }

                int constantBufferSizeInBytes = 0;

                // Run the fast-path constant buffer processor logic.
                // This only extracts the constant buffer size.
                ConstantBufferSyntaxProcessor.GetInfo(
                    context.Compilation,
                    typeSymbol,
                    ref constantBufferSizeInBytes);

                // The maximum size for a constant buffer is 64KB
                const int D3D11_REQ_CONSTANT_BUFFER_ELEMENT_COUNT = 4096;
                const int MaximumConstantBufferSize = D3D11_REQ_CONSTANT_BUFFER_ELEMENT_COUNT * 4 * sizeof(float);

                // Emit a diagnostic if the shader constant buffer is too large
                if (constantBufferSizeInBytes > MaximumConstantBufferSize)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        ExceededDispatchDataSize,
                        typeSymbol.Locations.First(),
                        typeSymbol,
                        constantBufferSizeInBytes));
                }
            }, SymbolKind.NamedType);
        });
    }
}