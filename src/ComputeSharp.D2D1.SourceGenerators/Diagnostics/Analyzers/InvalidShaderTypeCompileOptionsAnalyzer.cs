using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever invalid options are used in a [D2D1CompileOptions] attribute over a shader type.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidShaderTypeCompileOptionsAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidPackMatrixColumnMajorOption];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DCompileOptions] attribute type
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute") is not { } d2DCompileOptionsAttributeSymbol)
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

                // Check that the type is using [D2DCompileOptions]
                if (!typeSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out AttributeData? attributeData))
                {
                    return;
                }

                D2D1CompileOptions options = (D2D1CompileOptions)attributeData.ConstructorArguments[0].Value!;

                // Same validation and diagnostic as in the assembly-level analyzer
                if ((options & D2D1CompileOptions.PackMatrixColumnMajor) != 0)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidPackMatrixColumnMajorOption,
                        attributeData.ApplicationSyntaxReference?.GetSyntax(context.CancellationToken).GetLocation(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }
}