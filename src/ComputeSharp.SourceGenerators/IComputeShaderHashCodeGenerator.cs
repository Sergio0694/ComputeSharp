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
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

namespace ComputeSharp.SourceGenerators
{
    /// <summary>
    /// A source generator creating hash code factories for <see cref="IComputeShader"/> types.
    /// </summary>
    [Generator]
    public sealed partial class IComputeShaderHashCodeGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(static () => new SyntaxReceiver());
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            // Get the syntax receiver with the candidate nodes
            if (context.SyntaxContextReceiver is not SyntaxReceiver syntaxReceiver)
            {
                return;
            }

            // Method attributes
            AttributeListSyntax[] attributes = new[]
            {
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("GeneratedCode")).AddArgumentListArguments(
                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(IComputeShaderHashCodeGenerator).FullName))),
                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(IComputeShaderHashCodeGenerator).Assembly.GetName().Version.ToString())))))),
                AttributeList(SingletonSeparatedList(Attribute(IdentifierName("DebuggerNonUserCode")))),
                AttributeList(SingletonSeparatedList(Attribute(IdentifierName("ExcludeFromCodeCoverage")))),
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("EditorBrowsable")).AddArgumentListArguments(
                    AttributeArgument(ParseExpression("EditorBrowsableState.Never"))))),
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("Obsolete")).AddArgumentListArguments(
                    AttributeArgument(LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        Literal("This method is not intended to be used directly by user code"))))))
            };

            foreach (SyntaxReceiver.Item item in syntaxReceiver.GatheredInfo)
            {
                try
                {
                    OnExecute(context, item.StructDeclaration, item.StructSymbol, attributes);
                }
                catch
                {
                    context.ReportDiagnostic(IComputeShaderHashCodeGeneratorError, item.StructDeclaration, item.StructSymbol);
                }
            }
        }

        /// <summary>
        /// Processes a given target type.
        /// </summary>
        /// <param name="context">The input <see cref="GeneratorExecutionContext"/> instance to use.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
        /// <param name="attributes">The list of <see cref="AttributeListSyntax"/> instances to append to the generated method.</param>
        private static void OnExecute(GeneratorExecutionContext context, StructDeclarationSyntax structDeclaration, INamedTypeSymbol structDeclarationSymbol, AttributeListSyntax[] attributes)
        {
            string namespaceName = structDeclarationSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces));
            string structName = structDeclaration.Identifier.Text;
            SyntaxTokenList structModifiers = structDeclaration.Modifiers;

            // Create the partial shader type declaration with the hashcode interface method implementation.
            // This code produces a struct declaration as follows:
            //
            // public struct ShaderType : IShader<ShaderType>
            // {
            //     [GeneratedCode("...", "...")]
            //     [DebuggerNonUserCode]
            //     [ExcludeFromCodeCoverage]
            //     [EditorBrowsable(EditorBrowsableState.Never)]
            //     [Obsolete("This method is not intended to be called directly by user code")]
            //     public int GetDispatchId()
            //     {
            //         <BODY>
            //     }
            // }
            var structDeclarationSyntax =
                StructDeclaration(structName).WithModifiers(structModifiers)
                    .AddBaseListTypes(SimpleBaseType(
                        GenericName(Identifier("IShader"))
                        .AddTypeArgumentListArguments(IdentifierName(structName)))).AddMembers(
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.IntKeyword)),
                    Identifier("GetDispatchId"))
                .AddAttributeLists(attributes)
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.ReadOnlyKeyword))
                .WithBody(GetShaderHashCodeBody(structDeclarationSymbol)));

            TypeDeclarationSyntax typeDeclarationSyntax = structDeclarationSyntax;

            // Add all parent types in ascending order, if any
            foreach (var parentType in structDeclaration.Ancestors().OfType<TypeDeclarationSyntax>())
            {
                typeDeclarationSyntax = parentType
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(typeDeclarationSyntax))
                    .WithConstraintClauses(List<TypeParameterConstraintClauseSyntax>())
                    .WithBaseList(null)
                    .WithAttributeLists(List<AttributeListSyntax>())
                    .WithoutTrivia();
            }

            // Create a static method to create the combined hashcode for a given shader type.
            // This code takes a block syntax and produces a compilation unit as follows:
            //
            // using System;
            // using System.CodeDom.Compiler;
            // using System.ComponentModel;
            // using System.Diagnostics;
            // using System.Diagnostics.CodeAnalysis;
            // using ComputeSharp.__Internals;
            //
            // #pragma warning disable
            //
            // namespace <SHADER_NAMESPACE>
            // {
            //     <SHADER_DECLARATION>
            // }
            var source =
                CompilationUnit().AddUsings(
                UsingDirective(IdentifierName("System")),
                UsingDirective(IdentifierName("System.CodeDom.Compiler")),
                UsingDirective(IdentifierName("System.ComponentModel")),
                UsingDirective(IdentifierName("System.Diagnostics")),
                UsingDirective(IdentifierName("System.Diagnostics.CodeAnalysis")),
                UsingDirective(IdentifierName("ComputeSharp.__Internals"))).AddMembers(
                NamespaceDeclaration(IdentifierName(namespaceName)).AddMembers(typeDeclarationSyntax)
                .WithNamespaceKeyword(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.NamespaceKeyword, TriviaList())))
                .NormalizeWhitespace()
                .ToFullString();

            // Add the method source attribute
            context.AddSource(structDeclarationSymbol.GetGeneratedFileName(), SourceText.From(source, Encoding.UTF8));
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
                            IdentifierName("foo"),
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

