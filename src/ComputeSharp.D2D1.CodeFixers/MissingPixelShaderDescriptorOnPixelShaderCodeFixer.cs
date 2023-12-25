using System.Composition;
using ComputeSharp.CodeFixing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.CodeFixers;

/// <summary>
/// A code fixer that adds the <c>[D2DGeneratedPixelShaderDescriptor]</c> to D2D1 shader types with no descriptor.
/// </summary>
[ExportCodeFixProvider(LanguageNames.CSharp)]
[Shared]
public sealed class MissingPixelShaderDescriptorOnPixelShaderCodeFixer : MissingAttributeCodeFixer
{
    /// <summary>
    /// The set of type names for all D2D attributes that can be over shader types.
    /// </summary>
    private static readonly string[] D2DAttributeTypeNames =
    [
        "ComputeSharp.D2D1.D2DCompileOptionsAttribute",
        "ComputeSharp.D2D1.D2DEffectAuthorAttribute",
        "ComputeSharp.D2D1.D2DEffectCategoryAttribute",
        "ComputeSharp.D2D1.D2DEffectDescriptionAttribute",
        "ComputeSharp.D2D1.D2DEffectDisplayNameAttribute",
        "ComputeSharp.D2D1.D2DEffectIdAttribute",
        "ComputeSharp.D2D1.D2DInputComplexAttribute",
        "ComputeSharp.D2D1.D2DInputCountAttribute",
        "ComputeSharp.D2D1.D2DInputDescriptionAttribute",
        "ComputeSharp.D2D1.D2DInputSimpleAttribute",
        "ComputeSharp.D2D1.D2DOutputBufferAttribute",
        "ComputeSharp.D2D1.D2DPixelOptionsAttribute",
        "ComputeSharp.D2D1.D2DRequiresScenePositionAttribute",
        "ComputeSharp.D2D1.D2DShaderProfileAttribute"
    ];

    /// <summary>
    /// Creates a new <see cref="MissingAttributeCodeFixer"/> instance with the specified parameters.
    /// </summary>
    public MissingPixelShaderDescriptorOnPixelShaderCodeFixer()
        : base(
            diagnosticId: MissingPixelShaderDescriptorOnPixelShaderTypeId,
            codeActionTitle: "Add [D2DGeneratedPixelShaderDescriptor] attribute",
            attributeFullyQualifiedMetadataName: "ComputeSharp.D2D1.D2DGeneratedPixelShaderDescriptorAttribute",
            leadingAttributeFullyQualifiedMetadataNames: D2DAttributeTypeNames)
    {
    }
}