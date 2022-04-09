using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.D2D1Interop.SourceGenerators.Extensions;

namespace ComputeSharp.D2D1Interop.SourceGenerators.Models;

/// <summary>
/// A model representing a shared and its compiled bytecode, if available.
/// </summary>
/// <param name="HlslSource">The HLSL source for the shader.</param>
/// <param name="isLinkingSupported">Whether linking is supported for the current shader.</param>
/// <param name="Bytecode">The compiled shader bytecode, if available.</param>
internal sealed record EmbeddedBytecodeInfo(string HlslSource, bool isLinkingSupported, ImmutableArray<byte> Bytecode)
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
                x.HlslSource == y.HlslSource &&
                x.isLinkingSupported == y.isLinkingSupported &&
                x.Bytecode.SequenceEqual(y.Bytecode);
        }

        /// <inheritdoc/>
        public int GetHashCode(EmbeddedBytecodeInfo obj)
        {
            HashCode hashCode = default;

            hashCode.Add(obj.HlslSource);
            hashCode.Add(obj.isLinkingSupported);
            hashCode.AddBytes(obj.Bytecode.AsSpan());

            return hashCode.ToHashCode();
        }
    }
}
