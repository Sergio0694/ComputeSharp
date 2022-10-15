using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader dispatch data.
/// </summary>
/// <param name="Type">The shader interface type.</param>
/// <param name="FieldInfos">The description on shader instance fields.</param>
/// <param name="ResourceCount">The total number of captured resources.</param>
/// <param name="Root32BitConstantCount">The size of the shader root signature, in 32 bit constants.</param>
internal sealed record DispatchDataInfo(
    ShaderType Type,
    ImmutableArray<FieldInfo> FieldInfos,
    int ResourceCount,
    int Root32BitConstantCount)
{
    /// <inheritdoc/>
    public bool Equals(DispatchDataInfo? obj)
    {
        return Comparer.Default.Equals(this, obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Comparer.Default.GetHashCode(this);
    }

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="DispatchDataInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<DispatchDataInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, DispatchDataInfo obj)
        {
            hashCode.Add(obj.Type);
            hashCode.AddRange(obj.FieldInfos);
            hashCode.Add(obj.ResourceCount);
            hashCode.Add(obj.Root32BitConstantCount);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(DispatchDataInfo x, DispatchDataInfo y)
        {
            return
                x.Type == y.Type &&
                x.FieldInfos.SequenceEqual(y.FieldInfos) &&
                x.ResourceCount == y.ResourceCount &&
                x.Root32BitConstantCount == y.Root32BitConstantCount;
        }
    }
}