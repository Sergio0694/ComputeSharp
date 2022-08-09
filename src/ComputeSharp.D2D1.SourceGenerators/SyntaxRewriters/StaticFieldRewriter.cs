using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <inheritdoc/>
partial class StaticFieldRewriter
{
    /// <inheritdoc cref="ShaderSourceRewriter.NeedsD2DRequiresScenePositionAttribute"/>
    public bool NeedsD2DRequiresScenePositionAttribute { get; private set; }

    /// <inheritdoc/>
    private partial void TrackKnownPropertyAccess(IMemberReferenceOperation operation, MemberAccessExpressionSyntax node)
    {
        // No special tracking is needed for D2D1 shaders
    }

    /// <inheritdoc/>
    private partial void TrackKnownMethodInvocation(string metadataName)
    {
        // Track whether the method needs [D2DRequiresScenePosition]
        if (HlslKnownMethods.NeedsD2DRequiresScenePositionAttribute(metadataName))
        {
            NeedsD2DRequiresScenePositionAttribute = true;
        }
    }
}
