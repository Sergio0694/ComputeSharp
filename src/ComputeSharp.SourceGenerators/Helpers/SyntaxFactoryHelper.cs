using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators.Helpers
{
    /// <summary>
    /// A class containing additional factory methods for constructing syntax nodes, tokens and trivia.
    /// </summary>
    internal static class SyntaxFactoryHelper
    {
        /// <summary>
        /// Creates a <see cref="string"/>[] array expression from the given sequence of <see cref="string"/> instances.
        /// <para>
        /// That it, it applies the following transformation:
        /// <code>
        /// { "S1", "S2" } => new string[] { "S1", "S2" }
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="values">The input sequence of <see cref="string"/> instances.</param>
        /// <returns>An <see cref="ArrayCreationExpression"/> instance with the described contents.</returns>
        [Pure]
        public static ArrayCreationExpressionSyntax ArrayExpression(IEnumerable<string> values)
        {
            return
                ArrayCreationExpression(
                ArrayType(PredefinedType(Token(SyntaxKind.StringKeyword)))
                .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                .WithInitializer(InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                .AddExpressions(values.Select(static value => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(value))).ToArray()));
        }

        /// <summary>
        /// Creates an <see cref="object"/>[] array expression with the nested groups in the input sequence.
        /// <para>
        /// That it, it applies the following transformation:
        /// <code>
        /// { ("K1", "V1"), ("K2", "V2") } => new object[] { new[] { "K1", "V1" }, new[] { "K2", "V2" } }
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="values">The input sequence of <see cref="string"/> groups.</param>
        /// <returns>An <see cref="ArrayCreationExpression"/> instance with the described contents.</returns>
        [Pure]
        public static ArrayCreationExpressionSyntax NestedArrayExpression(IEnumerable<IEnumerable<string?>> groups)
        {
            return
                ArrayCreationExpression(
                ArrayType(PredefinedType(Token(SyntaxKind.ObjectKeyword)))
                .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                .WithInitializer(InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                .AddExpressions(groups.Select(static group =>
                    ImplicitArrayCreationExpression(InitializerExpression(SyntaxKind.ArrayInitializerExpression).AddExpressions(
                        group.Select(static item => item is null
                            ? LiteralExpression(SyntaxKind.NullLiteralExpression)
                            : LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(item)))
                        .ToArray()))).ToArray()));
        }

        /// <summary>
        /// Creates an <see cref="object"/>[] array expression with the nested groups in the input sequence.
        /// <para>
        /// That it, it applies the following transformation:
        /// <code>
        /// { ("K1", "V1", I1), ("K2", "V2", null) } => new object[] { new object[] { "K1", "V1", I1 }, new object[] { "K2", "V2", null } }
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="values">The input sequence of <see cref="string"/> groups.</param>
        /// <returns>An <see cref="ArrayCreationExpression"/> instance with the described contents.</returns>
        [Pure]
        public static ArrayCreationExpressionSyntax NestedArrayExpression(IEnumerable<(string A, string B, int? C)> groups)
        {
            return
                ArrayCreationExpression(
                ArrayType(PredefinedType(Token(SyntaxKind.ObjectKeyword)))
                .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                .WithInitializer(InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                .AddExpressions(groups.Select(static group =>
                    ArrayCreationExpression(
                    ArrayType(PredefinedType(Token(SyntaxKind.ObjectKeyword)))
                    .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                    .WithInitializer(InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                    .AddExpressions(
                        LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(group.A)),
                        LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(group.B)),
                        group.C is int i
                            ? LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(i))
                            : LiteralExpression(SyntaxKind.NullLiteralExpression)))).ToArray()));
        }
    }
}
