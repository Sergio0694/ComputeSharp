using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL method.
/// </summary>
/// <param name="MetadataName">The metadata name for the method.</param>
/// <param name="EntryPoint">The meethod entry point.</param>
/// <param name="DefinedTypes">The discovered declared types.</param>
/// <param name="DefinedConstants">The discovered defined constants.</param>
/// <param name="DependentMethods">The dependent captured methods.</param>
internal sealed record HlslMethodSourceInfo(
    string MetadataName,
    string EntryPoint,
    ImmutableArray<(string Name, string Definition)> DefinedTypes,
    ImmutableArray<(string Name, string Value)> DefinedConstants,
    ImmutableArray<(string Signature, string Definition)> DependentMethods)
{
    /// <inheritdoc/>
    public bool Equals(HlslMethodSourceInfo? obj) => Comparer.Default.Equals(this, obj);

    /// <inheritdoc/>
    public override int GetHashCode() => Comparer.Default.GetHashCode(this);

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="HlslMethodSourceInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<HlslMethodSourceInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, HlslMethodSourceInfo obj)
        {
            hashCode.Add(obj.MetadataName);
            hashCode.Add(obj.EntryPoint);
            hashCode.AddRange(obj.DefinedTypes);
            hashCode.AddRange(obj.DefinedConstants);
            hashCode.AddRange(obj.DependentMethods);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(HlslMethodSourceInfo x, HlslMethodSourceInfo y)
        {
            return
                x.MetadataName == y.MetadataName &&
                x.EntryPoint == y.EntryPoint &&
                x.DefinedTypes.SequenceEqual(y.DefinedTypes) &&
                x.DefinedConstants.SequenceEqual(y.DefinedConstants) &&
                x.DependentMethods.SequenceEqual(y.DependentMethods);
        }
    }
}
