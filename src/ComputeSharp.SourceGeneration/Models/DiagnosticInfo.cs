using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model for a serializeable diagnostic provider.
/// </summary>
/// <param name="Descriptor">The wrapped <see cref="DiagnosticDescriptor"/> instance.</param>
/// <param name="Args">The diagnostic args.</param>
internal sealed record DiagnosticInfo(DiagnosticDescriptor Descriptor, params object[] Args)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="DiagnosticInfo"/>.
    /// </summary>
    public sealed class Comparer : IEqualityComparer<DiagnosticInfo>
    {
        /// <summary>
        /// The singleton <see cref="Comparer"/> instance.
        /// </summary>
        public static Comparer Default { get; } = new();

        /// <inheritdoc/>
        public bool Equals(DiagnosticInfo? x, DiagnosticInfo? y)
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
                x.Descriptor.Equals(y.Descriptor) &&
                x.Args.SequenceEqual(y.Args);
        }

        /// <inheritdoc/>
        public int GetHashCode(DiagnosticInfo obj)
        {
            HashCode hashCode = default;

            hashCode.Add(obj.Descriptor);
            
            foreach (object arg in obj.Args)
            {
                hashCode.Add(arg);
            }

            return hashCode.ToHashCode();
        }
    }
}
