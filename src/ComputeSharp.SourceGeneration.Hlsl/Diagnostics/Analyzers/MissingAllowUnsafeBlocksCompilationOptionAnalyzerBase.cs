using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ComputeSharp.SourceGeneration.Diagnostics;

/// <summary>
/// A diagnostic analyzer that generates an error if the <c>AllowUnsafeBlocks</c> compilation option is not set.
/// </summary>
/// <param name="diagnosticDescriptor">The <see cref="DiagnosticDescriptor"/> instance to use.</param>
public abstract class MissingAllowUnsafeBlocksCompilationOptionAnalyzerBase(DiagnosticDescriptor diagnosticDescriptor) : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [diagnosticDescriptor];

    /// <inheritdoc/>
    public sealed override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationAction(context =>
        {
            // Check whether unsafe blocks are available, and emit an error if they are not
            if (!context.Compilation.IsAllowUnsafeBlocksEnabled())
            {
                context.ReportDiagnostic(Diagnostic.Create(diagnosticDescriptor, location: null));
            }
        });
    }
}