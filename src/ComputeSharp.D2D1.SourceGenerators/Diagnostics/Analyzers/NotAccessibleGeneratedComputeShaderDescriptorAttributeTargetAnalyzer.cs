using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NotAccessibleGeneratedComputeShaderDescriptorAttributeTargetAnalyzer : NotAccessibleGeneratedShaderDescriptorAttributeTargetAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="NotAccessibleGeneratedComputeShaderDescriptorAttributeTargetAnalyzer"/> instance.
    /// </summary>
    public NotAccessibleGeneratedComputeShaderDescriptorAttributeTargetAnalyzer()
        : base(NotAccessibleTargetTypeForD2DGeneratedPixelShaderDescriptorAttribute, "ComputeSharp.D2D1.D2DGeneratedPixelShaderDescriptorAttribute")
    {
    }
}