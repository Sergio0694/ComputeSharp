﻿using System.Collections.Generic;
using ComputeSharp.D2D1Interop.__Internals;
using ComputeSharp.D2D1Interop.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1Interop.SourceGenerators;

/// <inheritdoc/>
public sealed partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// Gets the shader type for a given shader, if any.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to check.</param>
    /// <param name="d2D1ShaderSymbol">The <see cref="INamedTypeSymbol"/> instance for <see cref="ID2D1Shader"/>.</param>
    /// <returns>Whether or not <paramref name="typeSymbol"/> implements <paramref name="d2D1ShaderSymbol"/>.</returns>
    private static bool IsD2D1ShaderType(INamedTypeSymbol typeSymbol, INamedTypeSymbol d2D1ShaderSymbol)
    {
        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.Interfaces)
        {
            if (interfaceSymbol.Name == nameof(ID2D1Shader) &&
                 SymbolEqualityComparer.Default.Equals(interfaceSymbol, d2D1ShaderSymbol))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Creates a <see cref="CompilationUnitSyntax"/> instance wrapping the given method.
    /// </summary>
    /// <param name="hierarchyInfo">The <see cref="HierarchyInfo"/> instance for the current type.</param>
    /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> item to insert.</param>
    /// <param name="canUseSkipLocalsInit">Whether <c>[SkipLocalsInit]</c> can be used.</param>
    /// <returns>A <see cref="CompilationUnitSyntax"/> object wrapping <paramref name="methodDeclaration"/>.</returns>
    private static CompilationUnitSyntax GetCompilationUnitFromMethod(
        HierarchyInfo hierarchyInfo,
        MethodDeclarationSyntax methodDeclaration,
        bool canUseSkipLocalsInit)
    {
        // Method attributes
        List<AttributeListSyntax> attributes = new()
        {
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).FullName))),
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).Assembly.GetName().Version.ToString())))))),
            AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
            AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage")))),
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.ComponentModel.EditorBrowsable")).AddArgumentListArguments(
                AttributeArgument(ParseExpression("global::System.ComponentModel.EditorBrowsableState.Never"))))),
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.Obsolete")).AddArgumentListArguments(
                AttributeArgument(LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Literal("This method is not intended to be used directly by user code"))))))
        };

        // Add [SkipLocalsInit] if the target project allows it
        if (canUseSkipLocalsInit)
        {
            attributes.Add(AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Runtime.CompilerServices.SkipLocalsInit")))));
        }

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
            .AddMembers(methodDeclaration.AddAttributeLists(attributes.ToArray()));

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
            // and skip the namespace declaration. This is the same additional path also
            // used when generating custom constructors. This will produce code as follows:
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
