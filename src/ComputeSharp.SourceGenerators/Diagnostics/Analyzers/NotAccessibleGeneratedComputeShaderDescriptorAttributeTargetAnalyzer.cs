using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NotAccessibleGeneratedComputeShaderDescriptorAttributeTargetAnalyzer : NotAccessibleGeneratedShaderDescriptorAttributeTargetAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="NotAccessibleGeneratedComputeShaderDescriptorAttributeTargetAnalyzer"/> instance.
    /// </summary>
    public NotAccessibleGeneratedComputeShaderDescriptorAttributeTargetAnalyzer()
        : base(NotAccessibleTargetTypeForGeneratedComputeShaderDescriptorAttribute, "ComputeSharp.GeneratedComputeShaderDescriptorAttribute")
    {
    }
}