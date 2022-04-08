using System;
using System.Collections.Generic;

namespace ComputeSharp.D2D1Interop.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL shader.
/// </summary>
/// <param name="Hierarchy">The hierarchy data for the shader.</param>
/// <param name="HlslSource">The HLSL source.</param>
internal sealed record HlslShaderSourceInfo(HierarchyInfo Hierarchy, string HlslSource)
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
                HierarchyInfo.Comparer.Default.Equals(x.Hierarchy, y.Hierarchy) &&
                x.HlslSource == y.HlslSource;
        }

        /// <inheritdoc/>
        public int GetHashCode(HlslShaderSourceInfo obj)
        {
            HashCode hashCode = default;

            hashCode.Add(HierarchyInfo.Comparer.Default.GetHashCode(obj.Hierarchy));
            hashCode.Add(obj.HlslSource);

            return hashCode.ToHashCode();
        }
    }
}
