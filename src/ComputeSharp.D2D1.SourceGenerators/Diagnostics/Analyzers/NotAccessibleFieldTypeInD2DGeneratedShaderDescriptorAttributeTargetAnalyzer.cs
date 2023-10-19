using ComputeSharp.SourceGeneration.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NotAccessibleFieldTypeInD2DGeneratedShaderDescriptorAttributeTargetAnalyzer : NotAccessibleFieldTypeInGeneratedShaderDescriptorAttributeTargetAnalyzerBase
{
    /// <summary>
    /// Creates a new <see cref="NotAccessibleFieldTypeInD2DGeneratedShaderDescriptorAttributeTargetAnalyzer"/> instance.
    /// </summary>
    public NotAccessibleFieldTypeInD2DGeneratedShaderDescriptorAttributeTargetAnalyzer()
        : base(NotAccessibleFieldTypeInTargetTypeForD2DGeneratedPixelShaderDescriptorAttribute, "ComputeSharp.D2D1.D2DGeneratedPixelShaderDescriptorAttribute")
    {
    }
}