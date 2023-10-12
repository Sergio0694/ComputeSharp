using System.Collections.Immutable;
using System.Text;
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
        /// <param name="supportsDynamicShaders">Indicates whether or not dynamic shaders are supported.</param>
        /// <param name="hierarchyDepth">The depth of the hierarchy for this type (used to calculate the right indentation).</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslSource</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(HlslShaderSourceInfo hlslSourceInfo, bool supportsDynamicShaders, int hierarchyDepth)
        {
            // Generate the necessary body statements depending on whether dynamic shaders are supported
            ImmutableArray<StatementSyntax> bodyStatements = supportsDynamicShaders
                ? GenerateRenderMethodBody(hlslSourceInfo, hierarchyDepth)
                : GenerateEmptyMethodBody();

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

            StringBuilder textBuilder = new();
            int prologueStatements = 0;
            int sizeHint = 64;

            void AppendLine(string text)
            {
                _ = textBuilder.Append(text);
            }

            void AppendParsedStatement(string text)
            {
                FlushText();

                statements.Add(ParseStatement(text));
            }

            void FlushText()
            {
                if (textBuilder.Length > 0)
                {
                    string hlslSource = textBuilder.ToString();

                    _ = textBuilder.Append(hlslSource);

                    sizeHint += textBuilder.Length;

                    // The indentation level is the current depth + 2 (the method, and the return expression)
                    SyntaxToken hlslSourceLiteralExpression = SyntaxTokenHelper.CreateRawMultilineStringLiteral(hlslSource, hierarchyDepth + 2);

                    statements.Add(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("builder"), IdentifierName("Append")))
                                .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, hlslSourceLiteralExpression)))));

                    _ = textBuilder.Clear();
                }
            }

            // Header and thread ids
            AppendLine(hlslSourceInfo.HeaderAndThreadsX);
            AppendParsedStatement("builder.Append(threadsX);");
            AppendLine(hlslSourceInfo.ThreadsY);
            AppendParsedStatement("builder.Append(threadsY);");
            AppendLine(hlslSourceInfo.ThreadsZ);
            AppendParsedStatement("builder.Append(threadsZ);");

            // Define declarations
            AppendLine(hlslSourceInfo.Defines);

            // Static fields and declared types
            AppendLine(hlslSourceInfo.StaticFieldsAndDeclaredTypes);

            // Captured variables
            AppendLine(hlslSourceInfo.CapturedFieldsAndResourcesAndForwardDeclarations);

            // Captured methods
            AppendLine(hlslSourceInfo.CapturedMethods);

            // Entry point
            AppendLine(hlslSourceInfo.EntryPoint);

            FlushText();

            // builder = global::ComputeSharp.__Internals.ArrayPoolStringBuilder.Create(<SIZE_HINT>);
            statements.Insert(
                prologueStatements,
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
                                Literal(sizeHint)))))));

            return statements.ToImmutable();
        }
    }
}