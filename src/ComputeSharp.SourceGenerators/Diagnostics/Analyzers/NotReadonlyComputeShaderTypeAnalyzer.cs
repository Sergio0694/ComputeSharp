using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NotReadonlyComputeShaderTypeAnalyzer : NotReadonlyShaderTypeAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="NotReadonlyComputeShaderTypeAnalyzer"/> instance.
    /// </summary>
    public NotReadonlyComputeShaderTypeAnalyzer()
        : base(NotReadonlyShaderType, ImmutableArray.Create("ComputeSharp.IComputeShader", "ComputeSharp.IComputeShader`1"))
    {
    }
}