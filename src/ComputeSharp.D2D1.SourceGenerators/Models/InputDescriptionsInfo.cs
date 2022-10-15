using System;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing gathered input descriptions for a shader.
/// </summary>
/// <param name="InputDescriptions">The input descriptions for a given shader.</param>
internal sealed record InputDescriptionsInfo(ImmutableArray<InputDescription> InputDescriptions)
{
    /// <inheritdoc/>
    public bool Equals(InputDescriptionsInfo? obj)
    {
        return Comparer.Default.Equals(this, obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Comparer.Default.GetHashCode(this);
    }

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="InputDescriptionsInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<InputDescriptionsInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, InputDescriptionsInfo obj)
        {
            hashCode.AddRange(obj.InputDescriptions);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(InputDescriptionsInfo x, InputDescriptionsInfo y)
        {
            return x.InputDescriptions.SequenceEqual(y.InputDescriptions);
        }
    }
}