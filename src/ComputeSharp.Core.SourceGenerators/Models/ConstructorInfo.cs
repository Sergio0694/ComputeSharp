using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.Core.SourceGenerators.Models;

/// <summary>
/// A model containiing information on a constructor to generate.
/// </summary>
/// <param name="Parameters">The <see cref="ParameterInfo"/> values for the constructor.</param>
/// <param name="DefaultedFields">The names of ignored fields that should be defaulted.</param>
internal sealed record ConstructorInfo(ImmutableArray<ParameterInfo> Parameters, ImmutableArray<string> DefaultedFields)
{
    /// <inheritdoc/>
    public bool Equals(ConstructorInfo? obj)
    {
        return Comparer.Default.Equals(this, obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Comparer.Default.GetHashCode(this);
    }

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="ConstructorInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<ConstructorInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, ConstructorInfo obj)
        {
            hashCode.AddRange(obj.Parameters);
            hashCode.AddRange(obj.DefaultedFields);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(ConstructorInfo x, ConstructorInfo y)
        {
            return
                x.Parameters.SequenceEqual(y.Parameters) &&
                x.DefaultedFields.SequenceEqual(y.DefaultedFields);
        }
    }
}