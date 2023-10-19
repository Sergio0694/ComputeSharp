using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NotAccessibleD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer : NotAccessibleGeneratedShaderDescriptorAttributeTargetAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="NotAccessibleD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer"/> instance.
    /// </summary>
    public NotAccessibleD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer()
        : base(NotAccessibleTargetTypeForD2DGeneratedPixelShaderDescriptorAttribute, "ComputeSharp.D2D1.D2DGeneratedPixelShaderDescriptorAttribute")
    {
    }
}