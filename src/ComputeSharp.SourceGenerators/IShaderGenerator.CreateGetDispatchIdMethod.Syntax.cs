using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using ComputeSharp.__Internals;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
public sealed partial class IShaderGenerator
{
    /// <summary>
    /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>GetDispatchId</c> method.
    /// </summary>
    ///<param name="delegateFieldNames">The names of all <see cref="System.Delegate"/> instance fields within the current shader type.</param>
    /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>GetDispatchId</c> method.</returns>
    private static MethodDeclarationSyntax CreateGetDispatchIdMethod(ImmutableArray<string> delegateFieldNames)
    {
        // This code produces a method declaration as follows:
        //
        // readonly int global::ComputeSharp.__Internals.IShader.GetDispatchId()
        // {
        //     <BODY>
        // }
        return
            MethodDeclaration(
                PredefinedType(Token(SyntaxKind.IntKeyword)),
                Identifier("GetDispatchId"))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
            .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
            .WithBody(GetShaderHashCodeBody(delegateFieldNames));
    }

    /// <summary>
    /// Gets a <see cref="BlockSyntax"/> instance with the logic to compute the hashcode of a given shader type.
    /// </summary>
    /// <param name="delegateFieldNames">The names of all <see cref="System.Delegate"/> instance fields within the current shader type.</param>
    /// <returns>The <see cref="BlockSyntax"/> instance to hash the input shader.</returns>
    [Pure]
    private static BlockSyntax GetShaderHashCodeBody(ImmutableArray<string> delegateFieldNames)
    {
        if (delegateFieldNames.Length == 0)
        {
            // return 0;
            return
                Block(ReturnStatement(
                LiteralExpression(
                    SyntaxKind.NumericLiteralExpression,
                    Literal(0))));
        }

        List<StatementSyntax> blockStatements = new(4);

        // global::System.HashCode hashCode = default;
        blockStatements.Add(LocalDeclarationStatement(
            VariableDeclaration(IdentifierName("global::System.HashCode"))
            .AddVariables(
                VariableDeclarator(Identifier("hashCode"))
                .WithInitializer(EqualsValueClause(
                    LiteralExpression(
                        SyntaxKind.DefaultLiteralExpression,
                        Token(SyntaxKind.DefaultKeyword)))))));

        foreach (string fieldName in delegateFieldNames)
        {
            // hashCode.Add(delegateField.Method);
            blockStatements.Add(ExpressionStatement(
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("hashCode"),
                        IdentifierName("Add")))
                .AddArgumentListArguments(Argument(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(fieldName),
                        IdentifierName("Method"))))));
        }

        // return hashCode.ToHashCode();
        blockStatements.Add(ReturnStatement(
            InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("hashCode"),
                    IdentifierName("ToHashCode")))));

        return Block(blockStatements);
    }
}
