using System;
using System.Collections.Generic;
using System.Reflection;
using ComputeSharp.Shaders.Translation.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A custom <see cref="CSharpSyntaxRewriter"/> <see langword="class"/> that processes C# methods to convert to HLSL
    /// </summary>
    internal class ShaderSyntaxRewriter : CSharpSyntaxRewriter
    {
        /// <summary>
        /// The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance to use to rewrite the decompiled code
        /// </summary>
        private readonly SemanticModel SemanticModel;

        /// <summary>
        /// The declaring type that hosts the root from which the current syntax tree is inspected
        /// </summary>
        private readonly Type DeclaringType;

        /// <summary>
        /// Creates a new <see cref="ShaderSyntaxRewriter"/> instance with the specified parameters
        /// </summary>
        /// <param name="semanticModel">The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance to use to rewrite the decompiled code</param>
        /// <param name="declaringType">The declaring type that hosts the root from which the current syntax tree is inspected</param>
        public ShaderSyntaxRewriter(SemanticModel semanticModel, Type declaringType)
        {
            SemanticModel = semanticModel;
            DeclaringType = declaringType;
        }

        private readonly Dictionary<string, ReadableMember> _StaticMembers = new Dictionary<string, ReadableMember>();

        /// <summary>
        /// Gets the mapping of captured static members used by the target code
        /// </summary>
        public IReadOnlyDictionary<string, ReadableMember> StaticMembers => _StaticMembers;

        private readonly Dictionary<string, MethodInfo> _StaticMethods = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// Gets the mapping of captured static methods used by the target code
        /// </summary>
        public IReadOnlyDictionary<string, MethodInfo> StaticMethods => _StaticMethods;

        /// <inheritdoc/>
        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            node = (IdentifierNameSyntax)base.VisitIdentifierName(node);
            node = node.ReplaceIdentifierName(DeclaringType, out var variable);

            // Register the captured member, if any
            if (variable.HasValue && !_StaticMembers.ContainsKey(variable.Value.Name))
            {
                _StaticMembers.Add(variable.Value.Name, variable.Value.MemberInfo);
            }

            return node;
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            node = (ParameterSyntax)base.VisitParameter(node);
            node = node.WithAttributeLists(default);
            node = node.ReplaceType(node.Type);

            return node;
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitArgument(ArgumentSyntax node)
        {
            node = (ArgumentSyntax)base.VisitArgument(node);

            if (node.RefKindKeyword.IsKind(SyntaxKind.RefKeyword))
            {
                node = node.WithRefKindKeyword(SyntaxFactory.Token(SyntaxKind.None));
            }

            return node;
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            node = (CastExpressionSyntax)base.VisitCastExpression(node);

            return node.ReplaceType(node.Type);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            node = (LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node);

            return node.ReplaceType(node.Declaration.Type);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            node = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node);
            node = node.ReplaceType(node.Type);

            if (node.ArgumentList.Arguments.Count == 0)
            {
                return SyntaxFactory.CastExpression(node.Type, SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0)));
            }

            return SyntaxFactory.InvocationExpression(node.Type, node.ArgumentList);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            node = (DefaultExpressionSyntax)base.VisitDefaultExpression(node);
            node = node.ReplaceType(node.Type);

            return SyntaxFactory.CastExpression(node.Type, SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0)));
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            node = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node);
            SyntaxNode syntaxNode = node.ReplaceMember(SemanticModel, out var variable, out var method);

            // Register the captured members, if any
            if (variable.HasValue && !_StaticMembers.ContainsKey(variable.Value.Name))
            {
                _StaticMembers.Add(variable.Value.Name, variable.Value.MemberInfo);
            }
            if (method.HasValue && !_StaticMethods.ContainsKey(method.Value.Name))
            {
                _StaticMethods.Add(method.Value.Name, method.Value.MethodInfo);
            }

            return syntaxNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            node = (InvocationExpressionSyntax)base.VisitInvocationExpression(node);
            node = node.ReplaceInvocation(DeclaringType, out var variable, out var method);

            // Register the captured members, if any
            if (variable.HasValue && !_StaticMembers.ContainsKey(variable.Value.Name))
            {
                _StaticMembers.Add(variable.Value.Name, variable.Value.MemberInfo);
            }
            if (method.HasValue && !_StaticMethods.ContainsKey(method.Value.Name))
            {
                _StaticMethods.Add(method.Value.Name, method.Value.MethodInfo);
            }

            return node;
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            node = (MethodDeclarationSyntax)base.VisitMethodDeclaration(node);

            // Replace the return type node, if needed
            if (!node.ReturnType.ToString().Equals("void"))
            {
                return node.ReplaceType(node.ReturnType);
            }

            return node;
        }
    }
}
