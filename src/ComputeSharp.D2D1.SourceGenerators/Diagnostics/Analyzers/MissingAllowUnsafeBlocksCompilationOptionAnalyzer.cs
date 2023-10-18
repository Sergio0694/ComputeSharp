using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MissingAllowUnsafeBlocksCompilationOptionAnalyzer : MissingAllowUnsafeBlocksCompilationOptionAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="MissingAllowUnsafeBlocksCompilationOptionAnalyzer"/> instance.
    /// </summary>
    public MissingAllowUnsafeBlocksCompilationOptionAnalyzer()
        : base(MissingAllowUnsafeBlocksOption)
    {
    }
}