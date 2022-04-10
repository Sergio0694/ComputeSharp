using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model containing info on shader dispatch metadata.
/// </summary>
/// <param name="Root32BitConstantCount">The size of the shader root signature, in 32 bit constants.</param>
/// <param name="IsSamplerUsed">Whether or not the static sampler is used.</param>
/// <param name="ResourceDescriptors">The sequence of resource descriptors for the shader.</param>
internal sealed record DispatchMetadataInfo(
    int Root32BitConstantCount,
    bool IsSamplerUsed,
    ImmutableArray<ResourceDescriptor> ResourceDescriptors)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="DispatchMetadataInfo"/>.
    /// </summary>
    public sealed class Comparer : Comparer<DispatchMetadataInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, DispatchMetadataInfo obj)
        {
            hashCode.Add(obj.Root32BitConstantCount);
            hashCode.Add(obj.IsSamplerUsed);
            hashCode.AddRange(obj.ResourceDescriptors);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(DispatchMetadataInfo x, DispatchMetadataInfo y)
        {
            return
                x.Root32BitConstantCount == y.Root32BitConstantCount &&
                x.IsSamplerUsed == y.IsSamplerUsed &&
                x.ResourceDescriptors.SequenceEqual(y.ResourceDescriptors);
        }
    }
}