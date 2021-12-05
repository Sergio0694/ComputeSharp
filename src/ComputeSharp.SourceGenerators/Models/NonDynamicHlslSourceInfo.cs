using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL shader, excluding dynamic information.
/// </summary>
/// <param name="HeaderAndThreadsX">The shader generated header and <c>threadsX</c> count declaration.</param>
/// <param name="ThreadsY">The <c>threadsY</c> count declaration.</param>
/// <param name="ThreadsZ">The <c>threadsZ</c> count declaration.</param>
/// <param name="Defines">The define statements, if any.</param>
/// <param name="StaticFieldsAndDeclaredTypes">The static fields and declared types, if any.</param>
/// <param name="CapturedFieldsAndResourcesAndForwardDeclarations">The captured fields, and method forward declarations.</param>
/// <param name="CapturedMethods">The captured method implementations.</param>
/// <param name="EntryPoint">The shader entry point.</param>
/// <param name="MethodSignatures">The signatures for captured methods.</param>
internal sealed record NonDynamicHlslSourceInfo(
    string HeaderAndThreadsX,
    string ThreadsY,
    string ThreadsZ,
    string Defines,
    string StaticFieldsAndDeclaredTypes,
    string CapturedFieldsAndResourcesAndForwardDeclarations,
    string CapturedMethods,
    string EntryPoint,
    ImmutableArray<string> MethodSignatures)
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="NonDynamicHlslSourceInfo"/>.
    /// </summary>
    public sealed class Comparer : IEqualityComparer<NonDynamicHlslSourceInfo>
    {
        /// <inheritdoc/>
        public bool Equals(NonDynamicHlslSourceInfo? x, NonDynamicHlslSourceInfo? y)
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
                x.MethodSignatures.AsSpan().SequenceEqual(y.MethodSignatures.AsSpan());
        }

        /// <inheritdoc/>
        public int GetHashCode(NonDynamicHlslSourceInfo obj)
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

            foreach (string signature in obj.MethodSignatures)
            {
                hashCode.Add(signature);
            }

            return hashCode.ToHashCode();
        }
    }
}
