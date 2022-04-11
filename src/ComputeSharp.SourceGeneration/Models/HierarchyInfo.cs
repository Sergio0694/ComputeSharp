using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model describing the hierarchy info for a specific type.
/// </summary>
/// <param name="FilenameHint">The filename hint for the current type.</param>
/// <param name="MetadataName">The metadata name for the current type.</param>
/// <param name="Namespace">Gets the namespace for the current type.</param>
/// <param name="Hierarchy">Gets the sequence of type definitions containing the current type.</param>
internal sealed partial record HierarchyInfo(string FilenameHint, string MetadataName, string Namespace, ImmutableArray<TypeInfo> Hierarchy)
{
    /// <summary>
    /// Creates a new <see cref="HierarchyInfo"/> instance from a given <see cref="INamedTypeSymbol"/>.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to gather info for.</param>
    /// <returns>A <see cref="HierarchyInfo"/> instance describing <paramref name="typeSymbol"/>.</returns>
    public static HierarchyInfo From(INamedTypeSymbol typeSymbol)
    {
        ImmutableArray<TypeInfo>.Builder hierarchy = ImmutableArray.CreateBuilder<TypeInfo>();

        for (INamedTypeSymbol? parent = typeSymbol;
             parent is not null;
             parent = parent.ContainingType)
        {
            hierarchy.Add(new TypeInfo(
                parent.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                parent.TypeKind,
                parent.IsRecord));
        }

        return new(
            typeSymbol.GetGeneratedFileName(),
            typeSymbol.MetadataName,
            typeSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces)),
            hierarchy.ToImmutable());
    }

    /// <summary>
    /// Creates a <see cref="CompilationUnitSyntax"/> instance for the current hierarchy.
    /// </summary>
    /// <param name="memberDeclarations">The member declarations to add to the generated type.</param>
    /// <returns>A <see cref="CompilationUnitSyntax"/> instance for the current hierarchy.</returns>
    public CompilationUnitSyntax GetSyntax(params MemberDeclarationSyntax[] memberDeclarations)
    {
        // Create the partial type declaration with for the current hierarchy.
        // This code produces a type declaration as follows:
        //
        // partial <TYPE_KIND> <TYPE_NAME>
        // {
        //     <MEMBER_DECLARATIONS>
        // }
        TypeDeclarationSyntax typeDeclarationSyntax =
            Hierarchy[0].GetSyntax()
            .AddModifiers(Token(SyntaxKind.PartialKeyword))
            .AddMembers(memberDeclarations);

        // Add all parent types in ascending order, if any
        foreach (TypeInfo parentType in Hierarchy.AsSpan().Slice(1))
        {
            typeDeclarationSyntax =
                parentType.GetSyntax()
                .AddModifiers(Token(SyntaxKind.PartialKeyword))
                .AddMembers(typeDeclarationSyntax);
        }

        if (Namespace is "")
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
            FileScopedNamespaceDeclaration(IdentifierName(Namespace))
            .AddMembers(typeDeclarationSyntax)
            .WithNamespaceKeyword(Token(TriviaList(
                Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))),
                SyntaxKind.NamespaceKeyword,
                TriviaList())))
            .NormalizeWhitespace(eol: "\n");
    }

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="HierarchyInfo"/>.
    /// </summary>
    public sealed class Comparer : Comparer<HierarchyInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, HierarchyInfo obj)
        {
            hashCode.Add(obj.FilenameHint);
            hashCode.Add(obj.MetadataName);
            hashCode.Add(obj.Namespace);
            hashCode.AddRange(obj.Hierarchy);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(HierarchyInfo x, HierarchyInfo y)
        {
            return
                x.FilenameHint == y.FilenameHint &&
                x.MetadataName == y.MetadataName &&
                x.Namespace == y.Namespace &&
                x.Hierarchy.SequenceEqual(y.Hierarchy);
        }
    }
}
