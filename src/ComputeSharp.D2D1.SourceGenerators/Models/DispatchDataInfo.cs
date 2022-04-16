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
/// <param name="Root32BitConstantCount">The size of the shader root signature, in 32 bit constants.</param>
internal sealed record DispatchDataInfo(ImmutableArray<FieldInfo> FieldInfos, int Root32BitConstantCount)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="DispatchDataInfo"/>.
    /// </summary>
    public sealed class Comparer : Comparer<DispatchDataInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, DispatchDataInfo obj)
        {
            hashCode.AddRange(obj.FieldInfos, FieldInfo.Comparer.Default);
            hashCode.Add(obj.Root32BitConstantCount);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(DispatchDataInfo x, DispatchDataInfo y)
        {
            return
                x.FieldInfos.SequenceEqual(y.FieldInfos, FieldInfo.Comparer.Default) &&
                x.Root32BitConstantCount == y.Root32BitConstantCount;
        }
    }
}
