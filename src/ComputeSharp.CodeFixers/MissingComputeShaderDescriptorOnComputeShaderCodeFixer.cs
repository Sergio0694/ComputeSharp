using System.Composition;
using ComputeSharp.CodeFixing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.CodeFixers;

/// <summary>
/// A code fixer that adds the <c>[GeneratedComputeShaderDescriptor]</c> to compute shader types with no descriptor.
/// </summary>
[ExportCodeFixProvider(LanguageNames.CSharp)]
[Shared]
public sealed class MissingComputeShaderDescriptorOnComputeShaderCodeFixer : MissingAttributeCodeFixer
{
    /// <summary>
    /// The set of type names for all attributes that can be over shader types.
    /// </summary>
    private static readonly string[] AttributeTypeNames =
    [
        "ComputeSharp.ThreadGroupSizeAttribute",
        "ComputeSharp.GroupSharedAttribute"
    ];

    /// <summary>
    /// Creates a new <see cref="MissingAttributeCodeFixer"/> instance with the specified parameters.
    /// </summary>
    public MissingComputeShaderDescriptorOnComputeShaderCodeFixer()
        : base(
            diagnosticId: MissingComputeShaderDescriptorOnComputeShaderTypeId,
            codeActionTitle: "Add [GeneratedComputeShaderDescriptor] attribute",
            attributeFullyQualifiedMetadataName: "ComputeSharp.GeneratedComputeShaderDescriptorAttribute",
            leadingAttributeFullyQualifiedMetadataNames: AttributeTypeNames)
    {
    }
}