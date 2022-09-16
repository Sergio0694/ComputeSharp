using ComputeSharp.D2D1.__Internals;
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
    partial class BuildHlslSource
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslSource</c> method.
        /// </summary>
        /// <param name="hlslSource">The input HLSL source.</param>
        /// <param name="hierarchyDepth">The depth of the hierarchy for this type (used to calculate the right indentation).</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslSource</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(string hlslSource, int hierarchyDepth)
        {
            // Create a token to represent the raw multiline string literal expression with the HLSL source. Here some spaces are
            // also added to properly align the resulting text with one indentation below the declaring string constant.
            // The spaces are: 4 for each containing type, 4 for the containing method, and 4 for the one additional indentation.
            string indentation = new(' ', 4 * hierarchyDepth + 4 + 4);
            SyntaxToken hlslSourceLiteralToken = Token(
                TriviaList(),
                SyntaxKind.MultiLineRawStringLiteralToken,
                $"\"\"\"\n{indentation}{hlslSource.Replace("\n", $"\n{indentation}")}\"\"\"",
                hlslSource,
                TriviaList());

            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.BuildHlslSource(out string hlslSource)
            // {
            //     hlslSource = <HLSL_SOURCE>;
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(nameof(BuildHlslSource)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddParameterListParameters(
                    Parameter(Identifier("hlslSource")).AddModifiers(Token(SyntaxKind.OutKeyword)).WithType(PredefinedType(Token(SyntaxKind.StringKeyword))))
                .WithBody(Block(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("hlslSource"),
                        LiteralExpression(SyntaxKind.StringLiteralExpression, hlslSourceLiteralToken)))));
        }
    }
}
