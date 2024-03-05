using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NotReadOnlyPixelShaderTypeWithFieldsAnalyzer : NotReadOnlyShaderTypeWithFieldsAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="NotReadOnlyPixelShaderTypeWithFieldsAnalyzer"/> instance.
    /// </summary>
    public NotReadOnlyPixelShaderTypeWithFieldsAnalyzer()
        : base(NotReadOnlyShaderType, ["ComputeSharp.D2D1.ID2D1PixelShader"])
    {
    }
}