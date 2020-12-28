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
    public class IComputeShaderDataLoaderGenerator : ISourceGenerator
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

                // Create a static method to create the combined hashcode for a given shader type.
                // This code takes a block syntax and produces a compilation unit as follows:
                //
                // using System;
                // using System.ComponentModel;
                //
                // namespace ComputeSharp.__Internals
                // {
                //     internal static partial class DispatchDataLoader
                //     {
                //         [System.ComponentModel.EditorBrowsable(EditorBrowsableState.Never)]
                //         [System.Obsolete("This method is intended for internal usage only")]
                //         public static void LoadDispatchData(GraphicsDevice device, in ShaderType shader, ref ulong r0, ref byte r1)
                //         {
                //             ...
                //         }
                //     }
                // }
                var source =
                    CompilationUnit().AddUsings(
                    UsingDirective(IdentifierName("System")),
                    UsingDirective(IdentifierName("System.ComponentModel"))).AddMembers(
                    NamespaceDeclaration(IdentifierName("ComputeSharp.__Internals")).AddMembers(
                    ClassDeclaration("DispatchDataLoader").AddModifiers(
                        Token(SyntaxKind.InternalKeyword),
                        Token(SyntaxKind.StaticKeyword),
                        Token(SyntaxKind.PartialKeyword)).AddMembers(
                    MethodDeclaration(
                        PredefinedType(Token(SyntaxKind.VoidKeyword)),
                        Identifier("LoadDispatchData")).AddAttributeLists(
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
                        Parameter(Identifier("device")).WithType(IdentifierName("ComputeSharp.Graphics.GraphicsDevice")),
                        Parameter(Identifier("shader")).WithModifiers(TokenList(Token(SyntaxKind.InKeyword))).WithType(shaderType),
                        Parameter(Identifier("r0")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(PredefinedType(Token(SyntaxKind.ULongKeyword))),
                        Parameter(Identifier("r1")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                        .WithBody(block))))
                    .NormalizeWhitespace()
                    .ToFullString();

                // Add the method source attribute
                context.AddSource(structDeclarationSymbol.GetGeneratedFileName<IComputeShaderDataLoaderGenerator>(), SourceText.From(source, Encoding.UTF8));
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

