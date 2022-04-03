﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.Core.SourceGenerators.Extensions;
using ComputeSharp.Core.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.Core.SourceGenerators;

/// <summary>
/// A source generator creating constructors for types annotated with <see cref="AutoConstructorAttribute"/>.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class AutoConstructorGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Get all declared struct symbols with the [AutoConstructor] attribute
        IncrementalValuesProvider<INamedTypeSymbol> structDeclarations =
            context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, token) => node is StructDeclarationSyntax structDeclaration,
                static (context, token) => (INamedTypeSymbol?)context.SemanticModel.GetDeclaredSymbol(context.Node, token))
            .Where(static symbol => symbol is not null &&
                                    symbol.GetAttributes().Any(static a => a.AttributeClass?.ToDisplayString() == typeof(AutoConstructorAttribute).FullName))!;

        // Get the type hierarchy and fields info
        IncrementalValuesProvider<(HierarchyInfo Left, ImmutableArray<ParameterInfo> Right)> constructorInfo =
            structDeclarations
            .Select(static (item, token) => (Hierarchy: HierarchyInfo.From(item), Parameters: Ctor.GetData(item)))
            .Where(static item => !item.Parameters.IsEmpty)
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<ParameterInfo>.Default.ForImmutableArray());

        // Generate the constructors
        context.RegisterSourceOutput(constructorInfo, static (context, item) =>
        {
            CompilationUnitSyntax compilationUnit = Ctor.GetSyntax(item.Left, item.Right);

            context.AddSource($"{item.Left.FilenameHint}.Ctor", compilationUnit.ToFullString());
        });
    }

    /// <summary>
    /// A helper with all logic to generate the constructor declarations.
    /// </summary>
    private static class Ctor
    {
        /// <summary>
        /// Gets the sequence of <see cref="ParameterInfo"/> items for all fields in the input type.
        /// </summary>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> being inspected.</param>
        /// <returns>The sequence of <see cref="ParameterInfo"/> items for all fields in <paramref name="structDeclarationSymbol"/>.</returns>
        public static ImmutableArray<ParameterInfo> GetData(INamedTypeSymbol structDeclarationSymbol)
        {
            return (
                from fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>()
                where fieldSymbol is { IsConst: false, IsStatic: false, IsFixedSizeBuffer: false }
                let typeName = fieldSymbol.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                select new ParameterInfo(typeName, fieldSymbol.Name)).ToImmutableArray();
        }

        /// <summary>
        /// Gets the <see cref="CompilationUnitSyntax"/> instance for a given type and sequence of parameters.
        /// </summary>
        /// <param name="hierarchyInfo">The type hierarchy info.</param>
        /// <param name="parameters">The sequence of parameters to initialize.</param>
        /// <returns>A <see cref="CompilationUnitSyntax"/> instance for a specified type.</returns>
        public static CompilationUnitSyntax GetSyntax(HierarchyInfo hierarchyInfo, ImmutableArray<ParameterInfo> parameters)
        {
            // Create the constructor declaration for the type. This will
            // produce a constructor with simple initialization of all variables:
            //
            // public MyType(Foo a, Bar b, Baz c, ...)
            // {
            //     this.a = a;
            //     this.b = b;
            //     this.c = c;
            //     ...
            // }
            ConstructorDeclarationSyntax constructorDeclaration =
                ConstructorDeclaration(hierarchyInfo.Names[0])
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(parameters.Select(field => Parameter(Identifier(field.Name)).WithType(IdentifierName(field.Type))).ToArray())
                .AddBodyStatements(parameters.Select(field => ParseStatement($"this.{field.Name} = {field.Name};")).ToArray());

            // Add the unsafe modifier, if needed
            if (parameters.Any(static param => param.Type.Contains("*")))
            {
                constructorDeclaration = constructorDeclaration.AddModifiers(Token(SyntaxKind.UnsafeKeyword));
            }

            // Constructor attributes
            AttributeListSyntax[] attributes =
            {
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(AutoConstructorGenerator).FullName))),
                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(AutoConstructorGenerator).Assembly.GetName().Version.ToString())))))),
                AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
                AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage"))))
            };

            // Create the partial shader type declaration with the given method implementation.
            // This code produces a struct declaration as follows:
            //
            // partial struct <SHADER_TYPE>
            // {
            //     <METHOD>
            // }
            StructDeclarationSyntax structDeclarationSyntax =
                StructDeclaration(hierarchyInfo.Names[0])
                .AddModifiers(Token(SyntaxKind.PartialKeyword))
                .AddMembers(constructorDeclaration.AddAttributeLists(attributes));

            TypeDeclarationSyntax typeDeclarationSyntax = structDeclarationSyntax;

            // Add all parent types in ascending order, if any
            foreach (string parentType in hierarchyInfo.Names.AsSpan().Slice(1))
            {
                typeDeclarationSyntax =
                    ClassDeclaration(parentType)
                    .AddModifiers(Token(SyntaxKind.PartialKeyword))
                    .AddMembers(typeDeclarationSyntax);
            }

            if (hierarchyInfo.Namespace is "")
            {
                // If there is no namespace, attach the pragma directly to the declared type,
                // and skip the namespace declaration. This will produce code as follows:
                //
                // #pragma warning disable
                // 
                // <TYPE_HIERARCHY>
                return
                    CompilationUnit().AddMembers(
                        typeDeclarationSyntax
                        .WithModifiers(TokenList(Token(
                            TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))),
                            SyntaxKind.PartialKeyword,
                            TriviaList()))))
                    .NormalizeWhitespace(eol: "\n");
            }

            // Create the compilation unit with disabled warnings, target namespace and generated type.
            // This will produce code as follows:
            //
            // #pragma warning disable
            //
            // namespace <NAMESPACE>;
            // 
            // <TYPE_HIERARCHY>
            return
                CompilationUnit().AddMembers(
                FileScopedNamespaceDeclaration(IdentifierName(hierarchyInfo.Namespace))
                .AddMembers(typeDeclarationSyntax)
                .WithNamespaceKeyword(Token(TriviaList(
                    Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))),
                    SyntaxKind.NamespaceKeyword,
                    TriviaList())))
                .NormalizeWhitespace(eol: "\n");
        }
    }
}
