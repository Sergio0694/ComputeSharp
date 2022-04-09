using System;
using System.Collections.Generic;

namespace ComputeSharp.D2D1Interop.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL shader.
/// </summary>
/// <param name="HlslSource">The HLSL source.</param>
/// <param name="ShaderProfile">The shader profile to use to compile the shader, if requested.</param>
/// <param name="IsLinkingSupported">Whether the shader supports linking.</param>
internal sealed record HlslShaderSourceInfo(string HlslSource, D2D1ShaderProfile? ShaderProfile, bool IsLinkingSupported)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="HlslShaderSourceInfo"/>.
    /// </summary>
    public sealed class Comparer : IEqualityComparer<HlslShaderSourceInfo>
    {
        /// <summary>
        /// The singleton <see cref="Comparer"/> instance.
        /// </summary>
        public static Comparer Default { get; } = new();

        /// <inheritdoc/>
        public bool Equals(HlslShaderSourceInfo? x, HlslShaderSourceInfo? y)
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
                x.ShaderProfile == y.ShaderProfile &&
                x.IsLinkingSupported == y.IsLinkingSupported;
        }

        /// <inheritdoc/>
        public int GetHashCode(HlslShaderSourceInfo obj)
        {
            HashCode hashCode = default;

            hashCode.Add(obj.HlslSource);
            hashCode.Add(obj.ShaderProfile);
            hashCode.Add(obj.IsLinkingSupported);

            return hashCode.ToHashCode();
        }
    }
}
