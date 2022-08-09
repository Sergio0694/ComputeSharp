using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <inheritdoc cref="HlslSourceRewriter"/>
partial class HlslSourceRewriter
{
    /// <summary>
    /// Gets whether or not the shader uses a texture sampler at least once.
    /// </summary>
    public bool IsSamplerUsed { get; private set; }

    /// <inheritdoc/>
    private partial SyntaxNode RewriteSampledTextureAccess(IPropertyReferenceOperation operation, ElementAccessExpressionSyntax node, string? mappedType)
    {
        IsSamplerUsed = true;

        // Get the syntax for the argument syntax transformation as described above
        ArgumentSyntax coordinateSyntax = mappedType switch
        {
            not null => Argument(InvocationExpression(IdentifierName(mappedType!), ArgumentList(node.ArgumentList.Arguments))),
            null => node.ArgumentList.Arguments[0]
        };

        // Transform an indexer syntax into a sampling call with the implicit static linear sampler.
        // For instance: texture[uv] will be rewritten as texture.SampleLevel(__sampler, uv, 0).
        return
            InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    node.Expression,
                    IdentifierName("SampleLevel")))
            .AddArgumentListArguments(
                Argument(IdentifierName("__sampler")),
                coordinateSyntax,
                Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))));
    }
}
