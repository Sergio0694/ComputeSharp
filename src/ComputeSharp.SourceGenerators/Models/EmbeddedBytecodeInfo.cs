using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

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
    /// <inheritdoc/>
    public bool Equals(EmbeddedBytecodeInfo? obj) => Comparer.Default.Equals(this, obj);

    /// <inheritdoc/>
    public override int GetHashCode() => Comparer.Default.GetHashCode(this);

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="EmbeddedBytecodeInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<EmbeddedBytecodeInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, EmbeddedBytecodeInfo obj)
        {
            hashCode.Add(obj.X);
            hashCode.Add(obj.Y);
            hashCode.Add(obj.Z);
            hashCode.AddBytes(obj.Bytecode.AsSpan());
        }

        /// <inheritdoc/>
        protected override bool AreEqual(EmbeddedBytecodeInfo x, EmbeddedBytecodeInfo y)
        {
            return
                x.X == y.X &&
                x.Y == y.Y &&
                x.Z == y.Z &&
                x.Bytecode.SequenceEqual(y.Bytecode);
        }
    }
}