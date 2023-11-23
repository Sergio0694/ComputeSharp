using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NotReadonlyPixelShaderTypeAnalyzer : NotReadonlyShaderTypeAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="NotReadonlyPixelShaderTypeAnalyzer"/> instance.
    /// </summary>
    public NotReadonlyPixelShaderTypeAnalyzer()
        : base(NotReadOnlyShaderType, ImmutableArray.Create("ComputeSharp.D2D1.ID2D1PixelShader"))
    {
    }
}