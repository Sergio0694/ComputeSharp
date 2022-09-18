using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a shader method.
/// </summary>
/// <param name="HlslMethodSource">The processed HLSL source for the shader method.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record ShaderMethodInfo(HlslMethodSourceInfo HlslMethodSource, ImmutableArray<DiagnosticInfo> Diagnostcs)
{
    /// <inheritdoc/>
    public bool Equals(ShaderMethodInfo? obj) => Comparer.Default.Equals(this, obj);

    /// <inheritdoc/>
    public override int GetHashCode() => Comparer.Default.GetHashCode(this);

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="ShaderMethodInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<ShaderMethodInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, ShaderMethodInfo obj)
        {
            hashCode.Add(obj.HlslMethodSource);
            hashCode.AddRange(obj.Diagnostcs);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(ShaderMethodInfo x, ShaderMethodInfo y)
        {
            return
                x.HlslMethodSource == y.HlslMethodSource &&
                x.Diagnostcs.SequenceEqual(y.Diagnostcs);
        }
    }
}
