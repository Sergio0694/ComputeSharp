using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using ComputeSharp.__Internals;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators
{
    /// <inheritdoc/>
    public sealed partial class IShaderGenerator
    {
        /// <inheritdoc/>
        private static partial MethodDeclarationSyntax CreateGetDispatchIdMethod(INamedTypeSymbol structDeclarationSymbol)
        {
            // This code produces a method declaration as follows:
            //
            // readonly int IShader.GetDispatchId()
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.IntKeyword)),
                    Identifier("GetDispatchId"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName(nameof(IShader))))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithBody(GetShaderHashCodeBody(structDeclarationSymbol));
        }

        /// <summary>
        /// Gets a <see cref="BlockSyntax"/> instance with the logic to compute the hashcode of a given shader type.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The <see cref="BlockSyntax"/> instance to hash the input shader.</returns>
        [Pure]
        private static BlockSyntax GetShaderHashCodeBody(INamedTypeSymbol structDeclarationSymbol)
        {
            var delegateFields = structDeclarationSymbol
                .GetMembers()
                .OfType<IFieldSymbol>()
                .Where(static m => m.Type is INamedTypeSymbol { TypeKind: TypeKind.Delegate, IsStatic: false })
                .ToImmutableArray();

            if (delegateFields.Length == 0)
            {
                // return 0;
                return
                    Block(ReturnStatement(
                    LiteralExpression(
                        SyntaxKind.NumericLiteralExpression,
                        Literal(0))));
            }

            List<StatementSyntax> blockStatements = new(4);

            // HashCode hashCode = default;
            blockStatements.Add(LocalDeclarationStatement(
                VariableDeclaration(IdentifierName("HashCode"))
                .AddVariables(
                    VariableDeclarator(Identifier("hashCode"))
                    .WithInitializer(EqualsValueClause(
                        LiteralExpression(
                            SyntaxKind.DefaultLiteralExpression,
                            Token(SyntaxKind.DefaultKeyword)))))));

            foreach (IFieldSymbol fieldSymbol in delegateFields)
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
                            IdentifierName(fieldSymbol.Name),
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
}

