using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.D2D1Interop.SourceGenerators.Extensions;

namespace ComputeSharp.D2D1Interop.SourceGenerators.Models;

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
    public sealed class Comparer : IEqualityComparer<DispatchMetadataInfo>
    {
        /// <summary>
        /// The singleton <see cref="Comparer"/> instance.
        /// </summary>
        public static Comparer Default { get; } = new();

        /// <inheritdoc/>
        public bool Equals(DispatchMetadataInfo? x, DispatchMetadataInfo? y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return
                x.Root32BitConstantCount == y.Root32BitConstantCount &&
                x.IsSamplerUsed == y.IsSamplerUsed &&
                x.ResourceDescriptors.SequenceEqual(y.ResourceDescriptors, ResourceDescriptor.Comparer.Default);
        }

        /// <inheritdoc/>
        public int GetHashCode(DispatchMetadataInfo obj)
        {
            HashCode hashCode = default;

            hashCode.Add(obj.Root32BitConstantCount);
            hashCode.Add(obj.IsSamplerUsed);
            hashCode.AddRange(obj.ResourceDescriptors, ResourceDescriptor.Comparer.Default);

            return hashCode.ToHashCode();
        }
    }
}