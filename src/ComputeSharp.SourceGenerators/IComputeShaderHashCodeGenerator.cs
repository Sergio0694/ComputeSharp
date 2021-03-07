using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
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

            // Type attributes
            AttributeListSyntax[] attributes = new[]
            {
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("EditorBrowsable")).AddArgumentListArguments(
                    AttributeArgument(ParseExpression("EditorBrowsableState.Never"))))),
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("Obsolete")).AddArgumentListArguments(
                    AttributeArgument(LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        Literal("This type is not intended to be used directly by user code"))))))
            };

            foreach (StructDeclarationSyntax structDeclaration in structDeclarations)
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(structDeclaration.SyntaxTree);
                INamedTypeSymbol structDeclarationSymbol = semanticModel.GetDeclaredSymbol(structDeclaration)!;

                try
                {
                    OnExecute(context, structDeclaration, structDeclarationSymbol, ref attributes);
                }
                catch
                {
                    context.ReportDiagnostic(IComputeShaderHashCodeGeneratorError, structDeclaration, structDeclarationSymbol);
                }
            }
        }

        /// <summary>
        /// Processes a given target type.
        /// </summary>
        /// <param name="context">The input <see cref="GeneratorExecutionContext"/> instance to use.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
        /// <param name="attributes">The list of <see cref="AttributeListSyntax"/> instances to append to the first copy of the partial class being generated.</param>
        private static void OnExecute(GeneratorExecutionContext context, StructDeclarationSyntax structDeclaration, INamedTypeSymbol structDeclarationSymbol, ref AttributeListSyntax[] attributes)
        {
            // Only process compute shader types
            if (!structDeclarationSymbol.Interfaces.Any(static interfaceSymbol => interfaceSymbol.Name == nameof(IComputeShader))) return;

            TypeSyntax shaderType = ParseTypeName(structDeclarationSymbol.ToDisplayString());
            BlockSyntax block = Block(GetDelegateHashCodeStatements(structDeclarationSymbol));

            if (block.Statements.Count == 1) return;

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
            //         [System.Obsolete("This method is not intended to be called directly by user code")]
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
                    Token(SyntaxKind.PartialKeyword)).AddAttributeLists(attributes).AddMembers(
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
                            Literal("This method is not intended to be called directly by user code"))))))).AddModifiers(
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.StaticKeyword)).AddParameterListParameters(
                    Parameter(Identifier("hash")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("shader")).WithModifiers(TokenList(Token(SyntaxKind.InKeyword))).WithType(shaderType))
                    .WithBody(block))))
                .NormalizeWhitespace()
                .ToFullString();

            // Clear the attribute list to avoid duplicates
            attributes = Array.Empty<AttributeListSyntax>();

            // Add the method source attribute
            context.AddSource(structDeclarationSymbol.GetGeneratedFileName<IComputeShaderHashCodeGenerator>(), SourceText.From(source, Encoding.UTF8));
        }

        /// <summary>
        /// Gets a sequence of statements to process the hashcode of the captured delegates.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to hash the captured delegates.</returns>
        [Pure]
        private static IEnumerable<StatementSyntax> GetDelegateHashCodeStatements(INamedTypeSymbol structDeclarationSymbol)
        {
            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (fieldSymbol.Type is not INamedTypeSymbol { TypeKind: TypeKind.Delegate, IsStatic: false } typeSymbol)
                {
                    continue;
                }

                // hash += hash << 5;
                yield return ExpressionStatement(ParseExpression($"hash += hash << 5"));

                // hash += shader.Field[#i].Method.GetHashCode();
                yield return ExpressionStatement(ParseExpression($"hash += shader.{fieldSymbol.Name}.Method.GetHashCode()"));
            }

            yield return ReturnStatement(IdentifierName("hash"));
        }
    }
}

