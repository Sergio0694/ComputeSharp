using ComputeSharp.D2D1.__Internals;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class EffectDisplayName
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectDisplayName</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="HierarchyInfo"/> instance.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>EffectDisplayName</c> property.</returns>
        public static PropertyDeclarationSyntax GetSyntax(HierarchyInfo info)
        {
            // This code produces a method declaration as follows:
            //
            // readonly string global::ComputeSharp.D2D1.__Internals.ID2D1Shader.EffectDisplayName => "<FULLY_QUALIFIED_NAME>";
            return
                PropertyDeclaration(PredefinedType(Token(SyntaxKind.StringKeyword)), Identifier(nameof(EffectDisplayName)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(info.FullyQualifiedMetadataName))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }
    }
}