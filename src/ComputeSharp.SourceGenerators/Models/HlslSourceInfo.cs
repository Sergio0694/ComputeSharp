using ComputeSharp.SourceGenerators.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL shader.
/// </summary>
/// <param name="HeaderAndThreadsX">The shader generated header and <c>threadsX</c> count declaration.</param>
/// <param name="ThreadsY">The <c>threadsY</c> count declaration.</param>
/// <param name="ThreadsZ">The <c>threadsZ</c> count declaration.</param>
/// <param name="Defines">The define statements, if any.</param>
/// <param name="StaticFieldsAndDeclaredTypes">The static fields and declared types, if any.</param>
/// <param name="CapturedFieldsAndResourcesAndForwardDeclarations">The captured fields, and method forward declarations.</param>
/// <param name="CapturedMethods">The captured method implementations.</param>
/// <param name="EntryPoint">The shader entry point.</param>
/// <param name="ImplicitTextureType">The type of the implicit texture type, if any.</param>
/// <param name="IsSamplerUsed">Whether or not the static sampler is used.</param>
/// <param name="DefinedTypes">The names of declared types.</param>
/// <param name="DefinedConstants">The names of defined constants.</param>
/// <param name="MethodSignatures">The signatures for captured methods.</param>
/// <param name="Delegates">The list of delegate fields.</param>
internal sealed record HlslSourceInfo(
    string HeaderAndThreadsX,
    string ThreadsY,
    string ThreadsZ,
    string Defines,
    string StaticFieldsAndDeclaredTypes,
    string CapturedFieldsAndResourcesAndForwardDeclarations,
    string CapturedMethods,
    string EntryPoint,
    string? ImplicitTextureType,
    bool IsSamplerUsed,
    ImmutableArray<string> DefinedTypes,
    ImmutableArray<string> DefinedConstants,
    ImmutableArray<string> MethodSignatures,
    ImmutableArray<string> Delegates)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="HlslSourceInfo"/>.
    /// </summary>
    public sealed class Comparer : IEqualityComparer<HlslSourceInfo>
    {
        /// <summary>
        /// The singleton <see cref="Comparer"/> instance.
        /// </summary>
        public static Comparer Default { get; } = new();

        /// <inheritdoc/>
        public bool Equals(HlslSourceInfo? x, HlslSourceInfo? y)
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
                x.HeaderAndThreadsX == y.HeaderAndThreadsX &&
                x.ThreadsY == y.ThreadsY &&
                x.ThreadsZ == y.ThreadsZ &&
                x.Defines == y.Defines &&
                x.StaticFieldsAndDeclaredTypes == y.StaticFieldsAndDeclaredTypes &&
                x.CapturedFieldsAndResourcesAndForwardDeclarations == y.CapturedFieldsAndResourcesAndForwardDeclarations &&
                x.CapturedMethods == y.CapturedMethods &&
                x.EntryPoint == y.EntryPoint &&
                x.ImplicitTextureType == y.ImplicitTextureType &&
                x.IsSamplerUsed == y.IsSamplerUsed &&
                x.DefinedTypes.SequenceEqual(y.DefinedTypes) &&
                x.DefinedConstants.SequenceEqual(y.DefinedTypes) &&
                x.MethodSignatures.SequenceEqual(y.MethodSignatures) &&
                x.Delegates.SequenceEqual(y.Delegates);
        }

        /// <inheritdoc/>
        public int GetHashCode(HlslSourceInfo obj)
        {
            HashCode hashCode = default;

            hashCode.Add(obj.HeaderAndThreadsX);
            hashCode.Add(obj.ThreadsY);
            hashCode.Add(obj.ThreadsZ);
            hashCode.Add(obj.Defines);
            hashCode.Add(obj.StaticFieldsAndDeclaredTypes);
            hashCode.Add(obj.CapturedFieldsAndResourcesAndForwardDeclarations);
            hashCode.Add(obj.CapturedMethods);
            hashCode.Add(obj.EntryPoint);
            hashCode.Add(obj.ImplicitTextureType);
            hashCode.Add(obj.IsSamplerUsed);
            hashCode.AddRange(obj.DefinedTypes);
            hashCode.AddRange(obj.DefinedConstants);
            hashCode.AddRange(obj.MethodSignatures);
            hashCode.AddRange(obj.Delegates);

            return hashCode.ToHashCode();
        }
    }
}
