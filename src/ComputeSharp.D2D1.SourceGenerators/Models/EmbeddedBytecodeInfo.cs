using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing a shared and its compiled bytecode, if available.
/// </summary>
/// <param name="HlslSource">The HLSL source for the shader.</param>
/// <param name="ShaderProfile">The shader profile to use to compile the shader, if requested.</param>
/// <param name="IsLinkingSupported">Whether linking is supported for the current shader.</param>
/// <param name="Bytecode">The compiled shader bytecode, if available.</param>
internal sealed record EmbeddedBytecodeInfo(string HlslSource, D2D1ShaderProfile? ShaderProfile, bool IsLinkingSupported, ImmutableArray<byte> Bytecode)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="EmbeddedBytecodeInfo"/>.
    /// </summary>
    public sealed class Comparer : Comparer<EmbeddedBytecodeInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, EmbeddedBytecodeInfo obj)
        {
            hashCode.Add(obj.HlslSource);
            hashCode.Add(obj.ShaderProfile);
            hashCode.Add(obj.IsLinkingSupported);
            hashCode.AddBytes(obj.Bytecode.AsSpan());
        }

        /// <inheritdoc/>
        protected override bool AreEqual(EmbeddedBytecodeInfo x, EmbeddedBytecodeInfo y)
        {
            return
                x.HlslSource == y.HlslSource &&
                x.ShaderProfile == y.ShaderProfile &&
                x.IsLinkingSupported == y.IsLinkingSupported &&
                x.Bytecode.SequenceEqual(y.Bytecode);
        }
    }
}
