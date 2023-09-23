using ComputeSharp.D2D1.__Internals;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class HlslSource
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>HlslSource</c> property.
        /// </summary>
        /// <param name="hlslSource">The input HLSL source.</param>
        /// <param name="hierarchyDepth">The depth of the hierarchy for this type (used to calculate the right indentation).</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>HlslSource</c> property.</returns>
        public static PropertyDeclarationSyntax GetSyntax(string hlslSource, int hierarchyDepth)
        {
            SyntaxToken hlslSourceLiteralExpression = SyntaxTokenHelper.CreateRawMultilineStringLiteral(hlslSource, hierarchyDepth);

            // This code produces a property declaration as follows:
            //
            // readonly string global::ComputeSharp.D2D1.__Internals.ID2D1Shader.HlslSource => <HLSL_SOURCE>;
            return
                PropertyDeclaration(PredefinedType(Token(SyntaxKind.StringKeyword)), Identifier("HlslSource"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(LiteralExpression(SyntaxKind.StringLiteralExpression, hlslSourceLiteralExpression)))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }
    }
}