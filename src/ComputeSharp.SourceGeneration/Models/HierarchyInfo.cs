﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model describing the hierarchy info for a specific type.
/// </summary>
/// <param name="FilenameHint">The filename hint for the current type.</param>
/// <param name="MetadataName">The metadata name for the current type.</param>
/// <param name="Namespace">Gets the namespace for the current type.</param>
/// <param name="Names">Gets the sequence of type definitions containing the current type.</param>
internal sealed record HierarchyInfo(string FilenameHint, string MetadataName, string Namespace, ImmutableArray<string> Names)
{
    /// <summary>
    /// Creates a new <see cref="HierarchyInfo"/> instance from a given <see cref="INamedTypeSymbol"/>.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to gather info for.</param>
    /// <returns>A <see cref="HierarchyInfo"/> instance describing <paramref name="typeSymbol"/>.</returns>
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
            typeSymbol.MetadataName,
            typeSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces)),
            names.ToImmutable());
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
            hashCode.AddRange(obj.Names);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(HierarchyInfo x, HierarchyInfo y)
        {
            return
                x.FilenameHint == y.FilenameHint &&
                x.MetadataName == y.MetadataName &&
                x.Namespace == y.Namespace &&
                x.Names.SequenceEqual(y.Names);
        }
    }
}
