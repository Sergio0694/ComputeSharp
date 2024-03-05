using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NotReadOnlyComputeShaderTypeWithFieldsAnalyzer : NotReadOnlyShaderTypeWithFieldsAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="NotReadOnlyComputeShaderTypeWithFieldsAnalyzer"/> instance.
    /// </summary>
    public NotReadOnlyComputeShaderTypeWithFieldsAnalyzer()
        : base(NotReadOnlyShaderType, ["ComputeSharp.IComputeShader", "ComputeSharp.IComputeShader`1"])
    {
    }
}