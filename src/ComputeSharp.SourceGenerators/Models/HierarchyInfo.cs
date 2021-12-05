using System;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using ComputeSharp.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model describing the hierarchy info for a specific type.
/// </summary>
/// <param name="FilenameHint">The filename hint for the current type.</param>
/// <param name="Namespace">Gets the namespace for the current type.</param>
/// <param name="Names">Gets the sequence of type definitions containing the current type.</param>
internal sealed record HierarchyInfo(string FilenameHint, string Namespace, ImmutableArray<string> Names)
{
    /// <summary>
    /// Creates a new <see cref="HierarchyInfo"/> instance from a given <see cref="INamedTypeSymbol"/>.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to gather info for.</param>
    /// <returns>A <see cref="HierarchyInfo"/> instance describing <paramref name="typeSymbol"/>.</returns>
    [Pure]
    public static HierarchyInfo From(INamedTypeSymbol typeSymbol)
    {
        ImmutableArray<string>.Builder names = ImmutableArray.CreateBuilder<string>();

        for (INamedTypeSymbol? parent = typeSymbol;
             parent is not null;
             parent = parent.ContainingType)
        {
            names.Add(parent.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat));
        }

        return new(
            typeSymbol.GetGeneratedFileName(),
            typeSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces)),
            names.ToImmutable());
    }

    /// <inheritdoc/>
    [Pure]
    public bool Equals(HierarchyInfo? other)
    {
        return
            other is not null &&
            (ReferenceEquals(this, other) ||
             (Namespace == other.Namespace &&
              (Names == other.Names ||
               Names.AsSpan().SequenceEqual(other.Names.AsSpan()))));
    }

    /// <inheritdoc/>
    [Pure]
    public override int GetHashCode()
    {
        return HashCode.Combine(Namespace, Names, Names[0]);
    }
}
