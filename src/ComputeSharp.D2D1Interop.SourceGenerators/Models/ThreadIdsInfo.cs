using System;
using System.Collections.Generic;

namespace ComputeSharp.D2D1Interop.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a embedded thread ids.
/// </summary>
/// <param name="IsDefault">Whether the values are diascarded.</param>
/// <param name="X">The thread ids value for the X axis.</param>
/// <param name="Y">The thread ids value for the Y axis.</param>
/// <param name="Z">The thread ids value for the Z axis.</param>
internal sealed record ThreadIdsInfo(bool IsDefault, int X, int Y, int Z)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="ThreadIdsInfo"/>.
    /// </summary>
    public sealed class Comparer : IEqualityComparer<ThreadIdsInfo>
    {
        /// <summary>
        /// The singleton <see cref="Comparer"/> instance.
        /// </summary>
        public static Comparer Default { get; } = new();

        /// <inheritdoc/>
        public bool Equals(ThreadIdsInfo? x, ThreadIdsInfo? y)
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
                x.IsDefault == y.IsDefault &&
                x.X == y.X &&
                x.Y == y.Y &&
                x.Z == y.Y;
        }

        /// <inheritdoc/>
        public int GetHashCode(ThreadIdsInfo obj)
        {
            return HashCode.Combine(obj.IsDefault, obj.X, obj.Y, obj.Z);
        }
    }
}
