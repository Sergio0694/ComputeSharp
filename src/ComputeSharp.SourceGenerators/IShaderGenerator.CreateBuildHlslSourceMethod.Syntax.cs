using System.Collections.Immutable;
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
            // Generate the necessary body statements depending on whether dynamic shaders are supported
            ImmutableArray<StatementSyntax> bodyStatements = GenerateRenderMethodBody(hlslSourceInfo, hierarchyDepth);

            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.__Internals.IShader.BuildHlslSource(out global::ComputeSharp.__Internals.ArrayPoolStringBuilder builder, int threadsX, int threadsY, int threadsZ)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("BuildHlslSource"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddParameterListParameters(
                    Parameter(Identifier("builder")).AddModifiers(Token(SyntaxKind.OutKeyword)).WithType(IdentifierName("global::ComputeSharp.__Internals.ArrayPoolStringBuilder")),
                    Parameter(Identifier("threadsX")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("threadsY")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("threadsZ")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))))
                .WithBody(Block(bodyStatements));
        }

        /// <summary>
        /// Produces the series of statements for the empty fallback method.
        /// </summary>
        /// <returns>A series of statements for when dynamic shaders are not supported.</returns>
        private static ImmutableArray<StatementSyntax> GenerateEmptyMethodBody()
        {
            // builder = global::ComputeSharp.__Internals.ArrayPoolStringBuilder.Create(0);
            return
                ImmutableArray.Create<StatementSyntax>(
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("builder"),
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("global::ComputeSharp.__Internals.ArrayPoolStringBuilder"),
                                    IdentifierName("Create")))
                            .AddArgumentListArguments(
                                Argument(LiteralExpression(
                                    SyntaxKind.NumericLiteralExpression,
                                    Literal(0)))))));
        }

        /// <summary>
        /// Produces the series of statements to build the current HLSL source.
        /// </summary>
        /// <param name="hlslSourceInfo">The input <see cref="HlslShaderSourceInfo"/> instance to use.</param>
        /// <param name="hierarchyDepth">The depth of the hierarchy for this type (used to calculate the right indentation).</param>
        /// <returns>The series of statements to build the HLSL source to compile to execute the current shader.</returns>
        private static ImmutableArray<StatementSyntax> GenerateRenderMethodBody(HlslShaderSourceInfo hlslSourceInfo, int hierarchyDepth)
        {
            using ImmutableArrayBuilder<StatementSyntax> statements = new();

            // builder = global::ComputeSharp.__Internals.ArrayPoolStringBuilder.Create(<SIZE_HINT>);
            statements.Add(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("builder"),
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("global::ComputeSharp.__Internals.ArrayPoolStringBuilder"),
                                IdentifierName("Create")))
                        .AddArgumentListArguments(
                            Argument(LiteralExpression(
                                SyntaxKind.NumericLiteralExpression,
                                Literal(hlslSourceInfo.HlslSource.Length)))))));

            // The indentation level is the current depth + 2 (the method, and the return expression)
            SyntaxToken hlslSourceLiteralExpression = SyntaxTokenHelper.CreateRawMultilineStringLiteral(hlslSourceInfo.HlslSource, hierarchyDepth + 2);

            statements.Add(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("builder"), IdentifierName("Append")))
                        .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, hlslSourceLiteralExpression)))));

            return statements.ToImmutable();
        }
    }
}