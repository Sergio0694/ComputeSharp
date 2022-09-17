using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A base type for a model describing a given captured field in a shader.
/// </summary>
internal abstract record FieldInfo
{
    /// <summary>
    /// A captured primitive value (either a scalar, a vector, or a linear matrix).
    /// </summary>
    /// <param name="FieldPath">The path of the field with respect to the shader instance.</param>
    /// <param name="TypeName">The full metadata name for the primitive type.</param>
    /// <param name="Offset">The byte offset of the value within the root signature.</param>
    public sealed record Primitive(ImmutableArray<string> FieldPath, string TypeName, int Offset) : FieldInfo
    {
        /// <inheritdoc/>
        public bool Equals(Primitive? obj) => Comparer.Default.Equals(this, obj);

        /// <inheritdoc/>
        public override int GetHashCode() => Comparer.Default.GetHashCode(this);

        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="Primitive"/>.
        /// </summary>
        private sealed class Comparer : Comparer<Primitive, Comparer>
        {
            /// <inheritdoc/>
            protected override void AddToHashCode(ref HashCode hashCode, Primitive obj)
            {
                hashCode.AddRange(obj.FieldPath);
                hashCode.Add(obj.TypeName);
                hashCode.Add(obj.Offset);
            }

            /// <inheritdoc/>
            protected override bool AreEqual(Primitive x, Primitive y)
            {
                return
                    x.FieldPath.SequenceEqual(y.FieldPath) &&
                    x.TypeName == y.TypeName &&
                    x.Offset == y.Offset;
            }
        }
    }

    /// <summary>
    /// A captured non linear matrix value.
    /// </summary>
    /// <param name="FieldPath">The path of the field with respect to the shader instance.</param>
    /// <param name="TypeName">The full metadata name for the matrix type.</param>
    /// <param name="ElementName">The name of each element in the matrix type.</param>
    /// <param name="Rows">The number of rows in the matrix.</param>
    /// <param name="Columns">The number of columns in the matrix.</param>
    /// <param name="Offsets">The sequence of byte offsets for each row within the root signature.</param>
    public sealed record NonLinearMatrix(
        ImmutableArray<string> FieldPath,
        string TypeName,
        string ElementName,
        int Rows,
        int Columns,
        ImmutableArray<int> Offsets) : FieldInfo
    {
        /// <inheritdoc/>
        public bool Equals(NonLinearMatrix? obj) => Comparer.Default.Equals(this, obj);

        /// <inheritdoc/>
        public override int GetHashCode() => Comparer.Default.GetHashCode(this);

        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="NonLinearMatrix"/>.
        /// </summary>
        private sealed class Comparer : Comparer<NonLinearMatrix, Comparer>
        {
            /// <inheritdoc/>
            protected override void AddToHashCode(ref HashCode hashCode, NonLinearMatrix obj)
            {
                hashCode.AddRange(obj.FieldPath);
                hashCode.Add(obj.TypeName);
                hashCode.Add(obj.ElementName);
                hashCode.Add(obj.Rows);
                hashCode.Add(obj.Columns);
                hashCode.AddRange(obj.Offsets);
            }

            /// <inheritdoc/>
            protected override bool AreEqual(NonLinearMatrix x, NonLinearMatrix y)
            {
                return
                    x.FieldPath.SequenceEqual(y.FieldPath) &&
                    x.TypeName == y.TypeName &&
                    x.ElementName == y.ElementName &&
                    x.Rows == y.Rows &&
                    x.Columns == y.Columns &&
                    x.Offsets.SequenceEqual(y.Offsets);
            }
        }
    }
}
