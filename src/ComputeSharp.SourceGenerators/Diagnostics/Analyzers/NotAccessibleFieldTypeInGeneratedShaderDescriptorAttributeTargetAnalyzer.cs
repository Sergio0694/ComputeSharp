using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NotAccessibleFieldTypeInGeneratedShaderDescriptorAttributeTargetAnalyzer : NotAccessibleFieldTypeInGeneratedShaderDescriptorAttributeTargetAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="NotAccessibleFieldTypeInGeneratedShaderDescriptorAttributeTargetAnalyzer"/> instance.
    /// </summary>
    public NotAccessibleFieldTypeInGeneratedShaderDescriptorAttributeTargetAnalyzer()
        : base(NotAccessibleFieldTypeInTargetTypeForGeneratedComputeShaderDescriptorAttribute, "ComputeSharp.GeneratedComputeShaderDescriptorAttribute")
    {
    }
}