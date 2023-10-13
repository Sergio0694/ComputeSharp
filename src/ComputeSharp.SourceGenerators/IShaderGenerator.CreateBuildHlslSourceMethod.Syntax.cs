using ComputeSharp.__Internals;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <inheritdoc/>
    partial class BuildHlslSource
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslSource</c> method.
        /// </summary>
        /// <param name="hlslSourceInfo">The input <see cref="HlslShaderSourceInfo"/> instance to use.</param>
        /// <param name="hierarchyDepth">The depth of the hierarchy for this type (used to calculate the right indentation).</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslSource</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(HlslShaderSourceInfo hlslSourceInfo, int hierarchyDepth)
        {
            // The indentation level is the current depth + 2 (the method, and the return expression)
            SyntaxToken hlslSourceLiteralExpression = SyntaxTokenHelper.CreateRawMultilineStringLiteral(hlslSourceInfo.HlslSource, hierarchyDepth + 2);

            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.__Internals.IShader.BuildHlslSource(out string hlslSource)
            // {
            //     hlslSource = <HLSL_SOURCE>;
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("BuildHlslSource"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddParameterListParameters(
                    Parameter(Identifier("hlslSource")).AddModifiers(Token(SyntaxKind.OutKeyword)).WithType(PredefinedType(Token(SyntaxKind.StringKeyword))))
                .WithBody(Block(
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("hlslSource"),
                            LiteralExpression(SyntaxKind.StringLiteralExpression, hlslSourceLiteralExpression)))));
        }
    }
}