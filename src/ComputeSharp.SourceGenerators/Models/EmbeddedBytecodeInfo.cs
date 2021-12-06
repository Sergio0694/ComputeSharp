using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGenerators.Extensions;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing a compiled shader bytecode, or none.
/// </summary>
/// <param name="X">The thread ids value for the X axis.</param>
/// <param name="Y">The thread ids value for the Y axis.</param>
/// <param name="Z">The thread ids value for the Z axis.</param>
/// <param name="Bytecode">The compiled shader bytecode, if available.</param>
internal sealed record EmbeddedBytecodeInfo(int X, int Y, int Z, ImmutableArray<byte> Bytecode)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="EmbeddedBytecodeInfo"/>.
    /// </summary>
    public sealed class Comparer : IEqualityComparer<EmbeddedBytecodeInfo>
    {
        /// <summary>
        /// The singleton <see cref="Comparer"/> instance.
        /// </summary>
        public static Comparer Default { get; } = new();

        /// <inheritdoc/>
        public bool Equals(EmbeddedBytecodeInfo? x, EmbeddedBytecodeInfo? y)
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
                x.X == y.X &&
                x.Y == y.Y &&
                x.Z == y.Z &&
                x.Bytecode.SequenceEqual(y.Bytecode);
        }

        /// <inheritdoc/>
        public int GetHashCode(EmbeddedBytecodeInfo obj)
        {
            HashCode hashCode = default;

            hashCode.Add(obj.X);
            hashCode.Add(obj.Y);
            hashCode.Add(obj.Z);
            hashCode.AddBytes(obj.Bytecode.AsSpan());

            return hashCode.ToHashCode();
        }
    }
}
