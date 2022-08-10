using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis.CSharp;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing info on a shader method that has requested to be precompiled at build time.
/// </summary>
///<param name="Modifiers">The modifiers for the annotated method.</param>
/// <param name="MethodName">The name of the annotated method.</param>
/// <param name="HlslSource">The HLSL source.</param>
/// <param name="Bytecode">The compiled shader bytecode, if available.</param>
internal sealed record EmbeddedBytecodeMethodInfo(
    ImmutableArray<SyntaxKind> Modifiers,
    string MethodName,
    string HlslSource,
    ImmutableArray<byte> Bytecode)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="EmbeddedBytecodeMethodInfo"/>.
    /// </summary>
    public sealed class Comparer : Comparer<EmbeddedBytecodeMethodInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, EmbeddedBytecodeMethodInfo obj)
        {
            hashCode.AddRange(obj.Modifiers);
            hashCode.Add(obj.MethodName);
            hashCode.Add(obj.HlslSource);
            hashCode.AddBytes(obj.Bytecode.AsSpan());
        }

        /// <inheritdoc/>
        protected override bool AreEqual(EmbeddedBytecodeMethodInfo x, EmbeddedBytecodeMethodInfo y)
        {
            return
                MemoryMarshal.Cast<SyntaxKind, ushort>(x.Modifiers.AsSpan()).SequenceEqual(MemoryMarshal.Cast<SyntaxKind, ushort>(y.Modifiers.AsSpan())) &&
                x.MethodName == y.MethodName &&
                x.HlslSource == y.HlslSource &&
                x.Bytecode.SequenceEqual(y.Bytecode);
        }
    }
}
