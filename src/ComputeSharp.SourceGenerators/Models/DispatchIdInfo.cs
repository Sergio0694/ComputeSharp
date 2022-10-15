using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader dispatch id.
/// </summary>
/// <param name="Delegates">The list of delegate field names for the shader.</param>
internal sealed record DispatchIdInfo(ImmutableArray<string> Delegates)
{
    /// <inheritdoc/>
    public bool Equals(DispatchIdInfo? obj)
    {
        return Comparer.Default.Equals(this, obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Comparer.Default.GetHashCode(this);
    }

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="DispatchIdInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<DispatchIdInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, DispatchIdInfo obj)
        {
            hashCode.AddRange(obj.Delegates);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(DispatchIdInfo x, DispatchIdInfo y)
        {
            return x.Delegates.SequenceEqual(y.Delegates);
        }
    }
}