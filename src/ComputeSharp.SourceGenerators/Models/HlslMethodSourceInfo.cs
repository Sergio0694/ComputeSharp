using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGenerators.Extensions;

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
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="HlslMethodSourceInfo"/>.
    /// </summary>
    public sealed class Comparer : IEqualityComparer<HlslMethodSourceInfo>
    {
        /// <summary>
        /// The singleton <see cref="Comparer"/> instance.
        /// </summary>
        public static Comparer Default { get; } = new();

        /// <inheritdoc/>
        public bool Equals(HlslMethodSourceInfo? x, HlslMethodSourceInfo? y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return
                x.MetadataName == y.MetadataName &&
                x.EntryPoint == y.EntryPoint &&
                x.DefinedTypes.SequenceEqual(y.DefinedTypes) &&
                x.DefinedConstants.SequenceEqual(y.DefinedConstants) &&
                x.DependentMethods.SequenceEqual(y.DependentMethods);
        }

        /// <inheritdoc/>
        public int GetHashCode(HlslMethodSourceInfo obj)
        {
            HashCode hashCode = default;

            hashCode.Add(obj.MetadataName);
            hashCode.Add(obj.EntryPoint);
            hashCode.AddRange(obj.DefinedTypes);
            hashCode.AddRange(obj.DefinedConstants);
            hashCode.AddRange(obj.DependentMethods);

            return hashCode.ToHashCode();
        }
    }
}
