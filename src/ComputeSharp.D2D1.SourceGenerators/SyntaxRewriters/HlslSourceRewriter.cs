using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <inheritdoc cref="HlslSourceRewriter"/>
partial class HlslSourceRewriter
{
    /// <inheritdoc/>
    private partial SyntaxNode RewriteSampledTextureAccess(IPropertyReferenceOperation operation, ElementAccessExpressionSyntax node, string? mappedType)
    {
        // Get the syntax for the argument syntax transformation as described above
        ArgumentSyntax coordinateSyntax = mappedType switch
        {
            not null => Argument(InvocationExpression(IdentifierName(mappedType!), ArgumentList(node.ArgumentList.Arguments))),
            null => node.ArgumentList.Arguments[0]
        };

        string fieldName = (operation.Instance as IFieldReferenceOperation)?.Member.Name ?? "";

        _ = HlslKnownKeywords.TryGetMappedName(fieldName, out string? mapped);

        // Transform an indexer syntax into a sampling call with the implicit static linear sampler.
        // For instance: texture[uv] will be rewritten as texture.Sample(__sampler__texture, uv).
        return
            InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    node.Expression,
                    IdentifierName("Sample")))
            .AddArgumentListArguments(Argument(IdentifierName($"__sampler__{mapped ?? fieldName}")), coordinateSyntax);
    }
}
