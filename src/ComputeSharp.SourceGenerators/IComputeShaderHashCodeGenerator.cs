using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators
{
    [Generator]
    public class IComputeShaderHashCodeGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            // Find all the struct declarations
            ImmutableArray<StructDeclarationSyntax> structDeclarations = (
                from tree in context.Compilation.SyntaxTrees
                from structDeclaration in tree.GetRoot().DescendantNodes().OfType<StructDeclarationSyntax>()
                select structDeclaration).ToImmutableArray();

            foreach (StructDeclarationSyntax structDeclaration in structDeclarations)
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(structDeclaration.SyntaxTree);
                INamedTypeSymbol structDeclarationSymbol = semanticModel.GetDeclaredSymbol(structDeclaration)!;

                // Only process compute shader types
                if (!structDeclarationSymbol.Interfaces.Any(static interfaceSymbol => interfaceSymbol.Name == nameof(IComputeShader))) continue;

                TypeSyntax shaderType = ParseTypeName(structDeclarationSymbol.ToDisplayString());

                BlockSyntax block = Block();

                // Process all the captured delegate types
                foreach (var fieldName in GetDelegateMemberNames(structDeclarationSymbol))
                {
                    // hash += hash << 5;
                    block = block.AddStatements(ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.AddAssignmentExpression,
                            IdentifierName("hash"),
                            BinaryExpression(SyntaxKind.LeftShiftExpression,
                                IdentifierName("hash"),
                                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(5))))));

                    // hash += shader.Field[#i].Method.GetHashCode();
                    block = block.AddStatements(ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.AddAssignmentExpression,
                            IdentifierName("hash"),
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("shader"),
                                            IdentifierName(fieldName)),
                                        IdentifierName("Method")),
                                    IdentifierName("GetHashCode"))))));
                }

                // return hash;
                block = block.AddStatements(ReturnStatement(IdentifierName("hash")));

                // Create a static method to create the combined hashcode for a given shader type.
                // This code takes a block syntax and produces a compilation unit as follows:
                //
                // using System;
                // using System.ComponentModel;
                //
                // namespace ComputeSharp.__Internals
                // {
                //     internal static partial class HashCodeProvider
                //     {
                //         [System.ComponentModel.EditorBrowsable(EditorBrowsableState.Never)]
                //         [System.Obsolete("This method is intended for internal usage only")]
                //         public static int CombineHashCode(int hash, in ShaderType shader)
                //         {
                //             return ...;
                //         }
                //     }
                // }
                var source =
                    CompilationUnit().AddUsings(
                    UsingDirective(IdentifierName("System")),
                    UsingDirective(IdentifierName("System.ComponentModel"))).AddMembers(
                    NamespaceDeclaration(IdentifierName("ComputeSharp.__Internals")).AddMembers(
                    ClassDeclaration("HashCodeProvider").AddModifiers(
                        Token(SyntaxKind.InternalKeyword),
                        Token(SyntaxKind.StaticKeyword),
                        Token(SyntaxKind.PartialKeyword)).AddMembers(
                    MethodDeclaration(
                        PredefinedType(Token(SyntaxKind.IntKeyword)),
                        Identifier("CombineHash")).AddAttributeLists(
                        AttributeList(SingletonSeparatedList(
                            Attribute(IdentifierName("EditorBrowsable")).AddArgumentListArguments(
                            AttributeArgument(ParseExpression("EditorBrowsableState.Never"))))),
                        AttributeList(SingletonSeparatedList(
                            Attribute(IdentifierName("Obsolete")).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(
                                SyntaxKind.StringLiteralExpression,
                                Literal("This method is intended for internal usage only"))))))).AddModifiers(
                        Token(SyntaxKind.PublicKeyword),
                        Token(SyntaxKind.StaticKeyword)).AddParameterListParameters(
                        Parameter(Identifier("hash")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                        Parameter(Identifier("shader")).WithModifiers(TokenList(Token(SyntaxKind.InKeyword))).WithType(shaderType))
                        .WithBody(block))))
                    .NormalizeWhitespace()
                    .ToFullString();

                // Add the method source attribute
                context.AddSource(structDeclarationSymbol.GetGeneratedFileName<IComputeShaderHashCodeGenerator>(), SourceText.From(source, Encoding.UTF8));
            }
        }

        /// <summary>
        /// Gets a sequence of captured delegate fields to process.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>A sequence of captured delegate members in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<string> GetDelegateMemberNames(INamedTypeSymbol structDeclarationSymbol)
        {
            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                INamedTypeSymbol typeSymbol = (INamedTypeSymbol)fieldSymbol.Type;

                if (typeSymbol.TypeKind != TypeKind.Delegate) continue;

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                yield return mapping ?? fieldSymbol.Name;
            }
        }
    }
}

