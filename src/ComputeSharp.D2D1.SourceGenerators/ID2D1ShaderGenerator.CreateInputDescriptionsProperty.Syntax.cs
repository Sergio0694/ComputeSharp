using System;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.SourceGenerators.Models;
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
    /// <inheritoc/>
    private static partial class InputDescriptions
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>InputDescriptions</c> property.
        /// </summary>
        /// <param name="inputDescriptionsInfo">The input descriptions info gathered for the current shader.</param>
        /// <param name="additionalTypes">Any additional <see cref="TypeDeclarationSyntax"/> instances needed by the generated code, if needed.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>InputDescriptions</c> property.</returns>
        public static PropertyDeclarationSyntax GetSyntax(InputDescriptionsInfo inputDescriptionsInfo, out TypeDeclarationSyntax[] additionalTypes)
        {
            ExpressionSyntax memoryExpression;

            // If there are no input descriptions, just return a default expression.
            // Otherwise, declare the shared array and return it from the property.
            if (inputDescriptionsInfo.InputDescriptions.Length == 0)
            {
                memoryExpression = LiteralExpression(SyntaxKind.DefaultLiteralExpression, Token(SyntaxKind.DefaultKeyword));
                additionalTypes = Array.Empty<TypeDeclarationSyntax>();
            }
            else
            {
                memoryExpression = MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("Data"),
                    IdentifierName(nameof(InputDescriptions)));

                additionalTypes = new[] { GetArrayDeclaration(inputDescriptionsInfo) };
            }

            // This code produces a method declaration as follows:
            //
            // readonly global::System.ReadOnlyMemory<global::ComputeSharp.D2D1.Interop.D2D1InputDescription> global::ComputeSharp.D2D1.__Internals.ID2D1Shader.InputDescriptions => <EXPRESSION>;
            return
                PropertyDeclaration(
                    GenericName(Identifier("global::System.ReadOnlyMemory"))
                    .AddTypeArgumentListArguments(IdentifierName("global::ComputeSharp.D2D1.Interop.D2D1InputDescription")),
                    Identifier(nameof(InputDescriptions)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(memoryExpression))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        /// <summary>
        /// Gets the array declaration for the given input descriptions.
        /// </summary>
        /// <param name="inputDescriptionsInfo">The input descriptions info gathered for the current shader.</param>
        /// <returns>The array declaration for the given input descriptions.</returns>
        private static TypeDeclarationSyntax GetArrayDeclaration(InputDescriptionsInfo inputDescriptionsInfo)
        {
            using ImmutableArrayBuilder<ExpressionSyntax> inputDescriptionExpressions = ImmutableArrayBuilder<ExpressionSyntax>.Rent();

            foreach (InputDescription inputDescription in inputDescriptionsInfo.InputDescriptions)
            {
                // Create the description expression (excluding level of detail):
                //
                // new(<INDEX>, <FILTER>)
                ImplicitObjectCreationExpressionSyntax inputDescriptionExpression =
                    ImplicitObjectCreationExpression()
                    .AddArgumentListArguments(
                        Argument(LiteralExpression(
                            SyntaxKind.NumericLiteralExpression,
                            Literal(inputDescription.Index))),
                        Argument(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("global::ComputeSharp.D2D1.D2D1Filter"),
                                IdentifierName(inputDescription.Filter.ToString()))));

                // Add the level of detail, if needed:
                //
                // { LevelOfDetailCount = <LEVEL_OF_DETAIL_COUNT> }
                if (inputDescription.LevelOfDetailCount != 0)
                {
                    inputDescriptionExpression =
                        inputDescriptionExpression
                        .WithInitializer(
                            InitializerExpression(SyntaxKind.ObjectInitializerExpression)
                            .AddExpressions(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName("LevelOfDetailCount"),
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(inputDescription.LevelOfDetailCount)))));
                }

                inputDescriptionExpressions.Add(inputDescriptionExpression);
            }

            // Declare the singleton property to get the memory instance:
            //
            // /// <summary>The singleton <see cref="global::ComputeSharp.D2D1.Interop.D2D1InputDescription"/> array instance.</summary>
            // public static readonly global::ComputeSharp.D2D1.Interop.D2D1InputDescription[] InputDescriptions = { <INPUT_DESCRIPTIONS> };
            FieldDeclarationSyntax fieldDeclaration =
                FieldDeclaration(
                    VariableDeclaration(
                        ArrayType(IdentifierName("global::ComputeSharp.D2D1.Interop.D2D1InputDescription"))
                        .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                    .AddVariables(
                        VariableDeclarator(Identifier(nameof(InputDescriptions)))
                        .WithInitializer(EqualsValueClause(
                            InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                            .AddExpressions(inputDescriptionExpressions.ToArray())))))
                .AddModifiers(
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.StaticKeyword),
                    Token(SyntaxKind.ReadOnlyKeyword))
                .WithLeadingTrivia(Comment("""/// <summary>The singleton <see cref="global::ComputeSharp.D2D1.Interop.D2D1InputDescription"/> array instance.</summary>"""));

            // Create the container type declaration:
            //
            // /// <summary>
            // /// A container type for input descriptions.
            // /// </summary>
            // [global::System.CodeDom.Compiler.GeneratedCode("...", "...")]
            // [global::System.Diagnostics.DebuggerNonUserCode]
            // [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            // file static class Data
            // {
            //     <FIELD_DECLARATION>
            // }
            return
                ClassDeclaration("Data")
                .AddModifiers(Token(SyntaxKind.FileKeyword), Token(SyntaxKind.StaticKeyword))
                .AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).FullName))),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).Assembly.GetName().Version.ToString())))))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage")))))
                .AddMembers(fieldDeclaration)
                .WithLeadingTrivia(
                    Comment("/// <summary>"),
                    Comment("/// A container type for input descriptions."),
                    Comment("/// </summary>"));
        }
    }
}