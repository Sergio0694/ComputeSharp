using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ComputeSharp.SourceGeneration.Diagnostics;

/// <summary>
/// A diagnostic analyzer that generates an error if the <c>AllowUnsafeBlocks</c> compilation option is not set.
/// </summary>
public abstract class MissingAllowUnsafeBlocksCompilationOptionAnalyzerBase : DiagnosticAnalyzer
{
    /// <summary>
    /// The <see cref="DiagnosticDescriptor"/> instance to use.
    /// </summary>
    private readonly DiagnosticDescriptor diagnosticDescriptor;

    /// <summary>
    /// Creates a new <see cref="MissingAllowUnsafeBlocksCompilationOptionAnalyzerBase"/> instance with the specified arguments.
    /// </summary>
    /// <param name="diagnosticDescriptor">The <see cref="DiagnosticDescriptor"/> instance to use.</param>
    protected MissingAllowUnsafeBlocksCompilationOptionAnalyzerBase(DiagnosticDescriptor diagnosticDescriptor)
    {
        this.diagnosticDescriptor = diagnosticDescriptor;

        SupportedDiagnostics = ImmutableArray.Create(diagnosticDescriptor);
    }

    /// <inheritdoc/>
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }

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
                context.ReportDiagnostic(Diagnostic.Create(this.diagnosticDescriptor, location: null));
            }
        });
    }
}