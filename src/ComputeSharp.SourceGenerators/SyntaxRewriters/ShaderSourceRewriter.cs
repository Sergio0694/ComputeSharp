using System;
using System.Collections.Generic;
using System.Globalization;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
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
        /// The set of discovered custom types.
        /// </summary>
        private readonly HashSet<INamedTypeSymbol> discoveredTypes;

        /// <summary>
        /// The collection of processed local functions in the current tree.
        /// </summary>
        private readonly Dictionary<string, LocalFunctionStatementSyntax> localFunctions;

        /// <summary>
        /// The current <see cref="MethodDeclarationSyntax"/> tree being visited.
        /// </summary>
        private MethodDeclarationSyntax? currentMethod;

        /// <summary>
        /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the target syntax tree.</param>
        /// <param name="discoveredTypes">The set of discovered custom types.</param>
        public ShaderSourceRewriter(SemanticModel semanticModel, HashSet<INamedTypeSymbol> discoveredTypes)
        {
            this.semanticModel = semanticModel;
            this.discoveredTypes = discoveredTypes;
            this.localFunctions = new();
        }

        /// <summary>
        /// Gets the collection of processed local functions in the current tree.
        /// </summary>
        public IReadOnlyDictionary<string, LocalFunctionStatementSyntax> LocalFunctions => this.localFunctions;

        /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
        public MethodDeclarationSyntax? Visit(MethodDeclarationSyntax? node)
        {
            this.currentMethod = node;

            return (MethodDeclarationSyntax?)base.Visit(node);
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

            _ = this.discoveredTypes.Add((INamedTypeSymbol)this.semanticModel.GetTypeInfo(node.Type).Type!);

            return updatedNode.ReplaceType(updatedNode.Type, node.Type, this.semanticModel);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            var updatedNode = ((LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node)!);

            _ = this.discoveredTypes.Add((INamedTypeSymbol)this.semanticModel.GetTypeInfo(node.Declaration.Type).Type!);

            return updatedNode.ReplaceType(updatedNode.Declaration.Type, node.Declaration.Type, this.semanticModel);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var updatedNode = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node)!;

            _ = this.discoveredTypes.Add((INamedTypeSymbol)this.semanticModel.GetTypeInfo(node.Type).Type!);

            updatedNode = updatedNode.ReplaceType(updatedNode.Type, node.Type, this.semanticModel);

            // New objects use the default HLSL cast syntax, eg. (float4)0
            if (updatedNode.ArgumentList!.Arguments.Count == 0)
            {
                return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            return InvocationExpression(updatedNode.Type, updatedNode.ArgumentList);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
        {
            var updatedNode = (ImplicitObjectCreationExpressionSyntax)base.VisitImplicitObjectCreationExpression(node)!;

            _ = this.discoveredTypes.Add((INamedTypeSymbol)this.semanticModel.GetTypeInfo(node).Type!);

            TypeSyntax explicitType = IdentifierName("").ReplaceType(node, this.semanticModel);

            // Mutate the syntax like with explicit object creation expressions
            if (updatedNode.ArgumentList!.Arguments.Count == 0)
            {
                return CastExpression(explicitType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            return InvocationExpression(explicitType, updatedNode.ArgumentList);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            var updatedNode = (DefaultExpressionSyntax)base.VisitDefaultExpression(node)!;

            _ = this.discoveredTypes.Add((INamedTypeSymbol)this.semanticModel.GetTypeInfo(node.Type).Type!);

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
        public override SyntaxNode? VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
        {
            var updatedNode =
                ((LocalFunctionStatementSyntax)base.VisitLocalFunctionStatement(node)!)
                .WithBlockBody()
                .WithIdentifier(Identifier($"__{this.currentMethod!.Identifier.Text}__{node.Identifier.Text}"));

            // HLSL doesn't support local functions, so we first process them as usual and then remove
            // them from the current syntax tree completely. These will be added to the shader source
            // as external static method with a special name to avoid conflicts with other methods.
            // The name will simply be in the format: "__<MethodName>__<FunctionName>".
            this.localFunctions.Add(updatedNode.Identifier.Text, updatedNode);

            return null;
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var updatedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node)!;
            
            if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
                this.semanticModel.GetOperation(node) is IMemberReferenceOperation operation)
            {
                // If the current member access is to a constant, hardcode the value in HLSL
                if (operation.ConstantValue is { HasValue: true } and { Value: object value })
                {
                    return ParseExpression(value switch
                    {
                        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
                        _ => value.ToString()
                    });
                }

                // If the current member access is a field or property access, check the lookup table
                // to see if the member should be rewritten for HLSL compliance, and rewrite if needed.
                if (HlslKnownMembers.TryGetMappedName(operation.Member.ToDisplayString(), out string? mapping))
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
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var updatedNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node)!;

            if (this.semanticModel.GetOperation(node) is IInvocationOperation operation &&
                operation.TargetMethod is IMethodSymbol method &&
                method.IsStatic)
            {
                // If the invocation consists of invoking a static method that has a direct
                // mapping to HLSL, rewrite the expression in the current invocation node.
                // For instance: Math.Abs(expr) => abs(expr).
                if (HlslKnownMethods.TryGetMappedName(method.GetFullMetadataName(), out string? mapping))
                {
                    return updatedNode.WithExpression(ParseExpression(mapping!));
                }

                // Update the name if the target is a local function. The exact schema for the
                // updated name is detailed in the override handling the local function statement.
                if (method.MethodKind == MethodKind.LocalFunction)
                {
                    var functionIdentifier = $"__{this.currentMethod!.Identifier.Text}__{method.Name}";

                    return updatedNode.WithExpression(ParseExpression(functionIdentifier));
                }
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            SyntaxToken updatedToken = base.VisitToken(token);

            // Replace all identifier tokens when needed, to avoid colliding with HLSL keywords
            if (updatedToken.IsKind(SyntaxKind.IdentifierToken) &&
                HlslKnownKeywords.TryGetMappedName(updatedToken.Text, out string? mapped))
            {
                return ParseToken(mapped!);
            }

            return updatedToken;
        }
    }
}
