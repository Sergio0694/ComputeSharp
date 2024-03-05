using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever invalid options are used in an assembly-level [D2D1CompileOptions] attribute.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidAssemblyLevelCompileOptionsAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidPackMatrixColumnMajorOption];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationAction(static context =>
        {
            IAssemblySymbol assemblySymbol = context.Compilation.Assembly;

            // In order to emit diagnostics for [D2D1CompileOptions] attributes at the assembly level, the following is needed:
            //   - The type symbol for the assembly, to get the AttributeData object for the [D2D1CompileOptions] attribute, if used.
            //   - The syntax node representing the attribute targeting the assembly, to get a location (this is retrieved from the AttributeData).
            //   - The input D2D1CompileOptions value, which can be retrieved from the constructor arguments of the AttributeData object.
            if (assemblySymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out AttributeData? attributeData))
            {
                D2D1CompileOptions options = (D2D1CompileOptions)attributeData.ConstructorArguments[0].Value!;

                if ((options & D2D1CompileOptions.PackMatrixColumnMajor) != 0)
                {
                    Diagnostic diagnostic = Diagnostic.Create(
                        InvalidPackMatrixColumnMajorOption,
                        attributeData.ApplicationSyntaxReference?.GetSyntax(context.CancellationToken).GetLocation(),
                        assemblySymbol);

                    context.ReportDiagnostic(diagnostic);
                }
            }
        });
    }
}