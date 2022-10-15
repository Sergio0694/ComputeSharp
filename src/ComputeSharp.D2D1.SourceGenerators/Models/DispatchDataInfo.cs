using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader dispatch data.
/// </summary>
/// <param name="FieldInfos">The description on shader instance fields.</param>
/// <param name="ConstantBufferSizeInBytes">The size of the shader constant buffer.</param>
internal sealed record DispatchDataInfo(ImmutableArray<FieldInfo> FieldInfos, int ConstantBufferSizeInBytes)
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
            hashCode.AddRange(obj.FieldInfos);
            hashCode.Add(obj.ConstantBufferSizeInBytes);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(DispatchDataInfo x, DispatchDataInfo y)
        {
            return
                x.FieldInfos.SequenceEqual(y.FieldInfos) &&
                x.ConstantBufferSizeInBytes == y.ConstantBufferSizeInBytes;
        }
    }
}