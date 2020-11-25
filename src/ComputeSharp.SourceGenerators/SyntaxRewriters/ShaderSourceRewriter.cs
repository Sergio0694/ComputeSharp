using System.Numerics;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators.SyntaxRewriters
{
    /// <summary>
    /// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# methods to convert to HLSL compliant code.
    /// </summary>
    internal sealed class ShaderSourceRewriter : CSharpSyntaxRewriter
    {
        /// <summary>
        /// The <see cref="SemanticModel"/> instance with semantic info on the target syntax tree.
        /// </summary>
        private readonly SemanticModel semanticModel;

        /// <summary>
        /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the target syntax tree.</param>
        public ShaderSourceRewriter(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
        public TNode? Visit<TNode>(TNode? node)
            where TNode : SyntaxNode
        {
            return (TNode?)base.Visit(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            var updatedNode = (ParameterSyntax)base.VisitParameter(node)!;

            return updatedNode
                .WithAttributeLists(default)
                .ReplaceType(updatedNode.Type!, node.Type!, this.semanticModel);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            var updatedNode = (CastExpressionSyntax)base.VisitCastExpression(node)!;

            return updatedNode.ReplaceType(updatedNode.Type, node.Type, this.semanticModel);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            var updatedNode = ((LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node)!);

            return updatedNode.ReplaceType(updatedNode.Declaration.Type, node.Declaration.Type, this.semanticModel);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var updatedNode = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node)!;

            updatedNode = updatedNode.ReplaceType(updatedNode.Type, node.Type, this.semanticModel);

            // New objects use the default HLSL cast syntax, eg. (float4)0
            if (updatedNode.ArgumentList!.Arguments.Count == 0)
            {
                return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            return InvocationExpression(updatedNode.Type, updatedNode.ArgumentList);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            var updatedNode = (DefaultExpressionSyntax)base.VisitDefaultExpression(node)!;

            updatedNode = updatedNode.ReplaceType(updatedNode.Type, node.Type, this.semanticModel);

            // A default expression becomes (T)0 in HLSL
            return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            node = ((LiteralExpressionSyntax)base.VisitLiteralExpression(node)!);

            if (node.IsKind(SyntaxKind.DefaultLiteralExpression))
            {
                // Same HLSL-style expression in the form (T)0
                return CastExpression(node.ReplaceType(this.semanticModel), LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            return node;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            return ((MethodDeclarationSyntax)base.VisitMethodDeclaration(node)!).WithBlockBody();
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var updatedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node)!;

            // If the current member access is a field or property access, check the lookup table
            // to see if the member should be rewritten for HLSL compliance, and rewrite if needed.
            if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
                this.semanticModel.GetOperation(node) is IMemberReferenceOperation operation &&
                HlslKnownMembers.TryGetMappedName(operation.Member.ToDisplayString(), out string? mapping))
            {
                // Static and instance members are handled differently, with static members being
                // converted into a literal expression, and instance getting an updated identifier.
                // For instance, consider these two cases:
                //   - Vector3.One (static) => float3(1.0f, 1.0f, 1.0f)
                //   - ThredIds.X (instance) => [arg].x
                return operation.Member.IsStatic switch
                {
                    true => ParseExpression(mapping!),
                    false => updatedNode.WithName(IdentifierName(mapping!))
                };
            }

            return updatedNode;
        }
    }
}
