using System;
using System.Collections.Generic;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A resource descriptor for a captured resource in a shader.
/// </summary>
/// <param name="TypeId">The type id for the resource.</param>
/// <param name="RegisterOffset">The offset of the resource in its containing register.</param>
/// <param name="Offset">The offset of the resource descriptor in the shader root signature.</param>
internal sealed record ResourceDescriptor(int TypeId, int RegisterOffset, int Offset)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="ResourceDescriptor"/>.
    /// </summary>
    public sealed class Comparer : IEqualityComparer<ResourceDescriptor>
    {
        /// <summary>
        /// The singleton <see cref="Comparer"/> instance.
        /// </summary>
        public static Comparer Default { get; } = new();

        /// <inheritdoc/>
        public bool Equals(ResourceDescriptor? x, ResourceDescriptor? y)
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
                x.TypeId == y.TypeId &&
                x.RegisterOffset == y.RegisterOffset &&
                x.Offset == y.Offset;
        }

        /// <inheritdoc/>
        public int GetHashCode(ResourceDescriptor obj)
        {
            return HashCode.Combine(obj.TypeId, obj.RegisterOffset, obj.Offset);
        }
    }
}
