using System;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis.CSharp;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL shader method.
/// </summary>
/// <param name="Modifiers">The modifiers for the annotated method.</param>
/// <param name="MethodName">The name of the annotated method.</param>
/// <param name="InvalidReturnType">The fully qualified name of the return type, if invalid.</param>
/// <param name="HlslSource">The HLSL source.</param>
/// <param name="ShaderProfile">The shader profile to use to compile the shader.</param>
/// <param name="CompileOptions">The compile options to use to compile the shader.</param>
/// <param name="HasErrors">Whether any errors have been detected, and therefore the shader compilation should be skipped.</param>
internal sealed record HlslShaderMethodSourceInfo(
    ImmutableArray<SyntaxKind> Modifiers,
    string MethodName,
    string? InvalidReturnType,
    string HlslSource,
    D2D1ShaderProfile ShaderProfile,
    D2D1CompileOptions CompileOptions,
    bool HasErrors)
{
    /// <inheritdoc/>
    public bool Equals(HlslShaderMethodSourceInfo? obj) => Comparer.Default.Equals(this, obj);

    /// <inheritdoc/>
    public override int GetHashCode() => Comparer.Default.GetHashCode(this);

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="HlslShaderMethodSourceInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<HlslShaderMethodSourceInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, HlslShaderMethodSourceInfo obj)
        {
            hashCode.AddRange(obj.Modifiers);
            hashCode.Add(obj.MethodName);
            hashCode.Add(obj.InvalidReturnType);
            hashCode.Add(obj.HlslSource);
            hashCode.Add(obj.ShaderProfile);
            hashCode.Add(obj.CompileOptions);
            hashCode.Add(obj.HasErrors);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(HlslShaderMethodSourceInfo x, HlslShaderMethodSourceInfo y)
        {
            return
                MemoryMarshal.Cast<SyntaxKind, ushort>(x.Modifiers.AsSpan()).SequenceEqual(MemoryMarshal.Cast<SyntaxKind, ushort>(y.Modifiers.AsSpan())) &&
                x.MethodName == y.MethodName &&
                x.InvalidReturnType == y.InvalidReturnType &&
                x.HlslSource == y.HlslSource &&
                x.ShaderProfile == y.ShaderProfile &&
                x.CompileOptions == y.CompileOptions &&
                x.HasErrors == y.HasErrors;
        }
    }
}
