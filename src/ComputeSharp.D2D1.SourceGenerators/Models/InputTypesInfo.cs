using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader input types.
/// </summary>
/// <param name="InputTypes">The input types for a given shader.</param>
internal sealed record InputTypesInfo(ImmutableArray<uint> InputTypes)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="InputTypesInfo"/>.
    /// </summary>
    public sealed class Comparer : Comparer<InputTypesInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, InputTypesInfo obj)
        {
            hashCode.AddRange(obj.InputTypes);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(InputTypesInfo x, InputTypesInfo y)
        {
            return x.InputTypes.SequenceEqual(y.InputTypes);
        }
    }
}
