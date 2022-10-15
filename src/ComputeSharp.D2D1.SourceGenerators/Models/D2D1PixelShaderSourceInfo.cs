using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a [D2D1PixelShaderSource] attribute.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="HlslShaderMethodSource">The processed HLSL source for the shader.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record D2D1PixelShaderSourceInfo(
    HierarchyInfo Hierarchy,
    HlslShaderMethodSourceInfo HlslShaderMethodSource,
    ImmutableArray<DiagnosticInfo> Diagnostcs)
{
    /// <inheritdoc/>
    public bool Equals(D2D1PixelShaderSourceInfo? obj)
    {
        return Comparer.Default.Equals(this, obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Comparer.Default.GetHashCode(this);
    }

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="D2D1PixelShaderSourceInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<D2D1PixelShaderSourceInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, D2D1PixelShaderSourceInfo obj)
        {
            hashCode.Add(obj.Hierarchy);
            hashCode.Add(obj.HlslShaderMethodSource);
            hashCode.AddRange(obj.Diagnostcs);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(D2D1PixelShaderSourceInfo x, D2D1PixelShaderSourceInfo y)
        {
            return
                x.Hierarchy == y.Hierarchy &&
                x.HlslShaderMethodSource == y.HlslShaderMethodSource &&
                x.Diagnostcs.SequenceEqual(y.Diagnostcs);
        }
    }
}