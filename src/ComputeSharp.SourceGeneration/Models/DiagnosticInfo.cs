using System;
using System.Collections.Generic;
using System.Linq;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model for a serializeable diagnostic provider.
/// </summary>
/// <param name="Descriptor">The wrapped <see cref="DiagnosticDescriptor"/> instance.</param>
/// <param name="Args">The diagnostic args.</param>
internal sealed record DiagnosticInfo(DiagnosticDescriptor Descriptor, params object[] Args)
{
    /// <inheritdoc/>
    public bool Equals(DiagnosticInfo? obj) => Comparer.Default.Equals(this, obj);

    /// <inheritdoc/>
    public override int GetHashCode() => Comparer.Default.GetHashCode(this);

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="DiagnosticInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<DiagnosticInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, DiagnosticInfo obj)
        {
            hashCode.Add(obj.Descriptor);

            foreach (object arg in obj.Args)
            {
                hashCode.Add(arg);
            }
        }

        /// <inheritdoc/>
        protected override bool AreEqual(DiagnosticInfo x, DiagnosticInfo y)
        {
            return
                x.Descriptor.Equals(y.Descriptor) &&
                x.Args.SequenceEqual(y.Args);
        }
    }
}
