using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model for a serializeable diagnostic info to be reconstructed later in a pipeline.
/// </summary>
/// <param name="Descriptor">The wrapped <see cref="DiagnosticDescriptor"/> instance.</param>
/// <param name="Arguments">The diagnostic arguments.</param>
internal sealed record DeferredDiagnosticInfo(DiagnosticDescriptor Descriptor, EquatableArray<string> Arguments)
{
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
}