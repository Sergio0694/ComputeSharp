using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader dispatch id.
/// </summary>
/// <param name="Delegates">The list of delegate field names for the shader.</param>
internal sealed record DispatchIdInfo(ImmutableArray<string> Delegates)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="DispatchIdInfo"/>.
    /// </summary>
    public sealed class Comparer : IEqualityComparer<DispatchIdInfo>
    {
        /// <summary>
        /// The singleton <see cref="Comparer"/> instance.
        /// </summary>
        public static Comparer Default { get; } = new();

        /// <inheritdoc/>
        public bool Equals(DispatchIdInfo? x, DispatchIdInfo? y)
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

            return x.Delegates.SequenceEqual(y.Delegates);
        }

        /// <inheritdoc/>
        public int GetHashCode(DispatchIdInfo obj)
        {
            HashCode hashCode = default;

            hashCode.AddRange(obj.Delegates);

            return hashCode.ToHashCode();
        }
    }
}
