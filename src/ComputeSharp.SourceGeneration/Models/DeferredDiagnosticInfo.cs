using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model for a serializeable diagnostic info to be reconstructed later in a pipeline.
/// </summary>
/// <param name="Descriptor">The wrapped <see cref="DiagnosticDescriptor"/> instance.</param>
/// <param name="Arguments">The diagnostic arguments.</param>
internal sealed record DeferredDiagnosticInfo(DiagnosticDescriptor Descriptor, ImmutableArray<string> Arguments)
{
    /// <inheritdoc/>
    public bool Equals(DeferredDiagnosticInfo? obj)
    {
        return Comparer.Default.Equals(this, obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Comparer.Default.GetHashCode(this);
    }

    /// <summary>
    /// Creates a new <see cref="DeferredDiagnosticInfo"/> instance with the specified parameters.
    /// </summary>
    /// <param name="descriptor">The input <see cref="DiagnosticDescriptor"/> for the diagnostics to create.</param>
    /// <param name="args">The optional arguments for the formatted message to include.</param>
    /// <returns>A new <see cref="DeferredDiagnosticInfo"/> instance with the specified parameters.</returns>
    public static DeferredDiagnosticInfo Create(DiagnosticDescriptor descriptor, params object[] args)
    {
        return new(descriptor, args.Select(static arg => arg.ToString()).ToImmutableArray());
    }

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="DeferredDiagnosticInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<DeferredDiagnosticInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, DeferredDiagnosticInfo obj)
        {
            hashCode.Add(obj.Descriptor);
            hashCode.AddRange(obj.Arguments);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(DeferredDiagnosticInfo x, DeferredDiagnosticInfo y)
        {
            return
                x.Descriptor.Equals(y.Descriptor) &&
                x.Arguments.SequenceEqual(y.Arguments);
        }
    }
}