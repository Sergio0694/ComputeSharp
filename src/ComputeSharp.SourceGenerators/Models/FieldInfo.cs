using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A base type for a model describing a given captured field in a shader.
/// </summary>
internal abstract record FieldInfo
{
    /// <summary>
    /// A captured resource (either a buffer or a texture).
    /// </summary>
    /// <param name="FieldName">The name of the resource field.</param>
    /// <param name="TypeName">The full metadata name for the resource type.</param>
    /// <param name="Offset">The offset for the resource within the root signature.</param>
    public sealed record Resource(string FieldName, string TypeName, int Offset) : FieldInfo;

    /// <summary>
    /// A captured primitive value (either a scalar, a vector, or a linear matrix).
    /// </summary>
    /// <param name="FieldPath">The path of the field with respect to the shader instance.</param>
    /// <param name="TypeName">The full metadata name for the primitive type.</param>
    /// <param name="Offset">The byte offset of the value within the root signature.</param>
    public sealed record Primitive(ImmutableArray<string> FieldPath, string TypeName, int Offset) : FieldInfo;

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
        ImmutableArray<int> Offsets) : FieldInfo;

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="FieldInfo"/>.
    /// </summary>
    public sealed class Comparer : Comparer<FieldInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, FieldInfo obj)
        {
            switch (obj)
            {
                case Resource resource:
                    hashCode.Add(resource.FieldName);
                    hashCode.Add(resource.TypeName);
                    hashCode.Add(resource.Offset);
                    break;
                case Primitive primitive:
                    hashCode.AddRange(primitive.FieldPath);
                    hashCode.Add(primitive.TypeName);
                    hashCode.Add(primitive.Offset);
                    break;
                case NonLinearMatrix matrix:
                    hashCode.AddRange(matrix.FieldPath);
                    hashCode.Add(matrix.TypeName);
                    hashCode.Add(matrix.ElementName);
                    hashCode.Add(matrix.Rows);
                    hashCode.Add(matrix.Columns);
                    hashCode.AddRange(matrix.Offsets);
                    break;
                default:
                    break;
            }
        }

        /// <inheritdoc/>
        protected override bool AreEqual(FieldInfo x, FieldInfo y)
        {
            if (x is Resource resourceX && y is Resource resourceY)
            {
                return
                    resourceX.FieldName == resourceY.FieldName &&
                    resourceX.TypeName == resourceY.TypeName &&
                    resourceX.Offset == resourceY.Offset;
            }
            else if (x is Primitive primitiveX && y is Primitive primitiveY)
            {
                return
                    primitiveX.FieldPath.SequenceEqual(primitiveY.FieldPath) &&
                    primitiveX.TypeName == primitiveY.TypeName &&
                    primitiveX.Offset == primitiveY.Offset;
            }
            else if (x is NonLinearMatrix matrixX && y is NonLinearMatrix matrixY)
            {
                return
                    matrixX.FieldPath.SequenceEqual(matrixY.FieldPath) &&
                    matrixX.TypeName == matrixY.TypeName &&
                    matrixX.ElementName == matrixY.ElementName &&
                    matrixX.Rows == matrixY.Rows &&
                    matrixY.Columns == matrixY.Columns &&
                    matrixX.Offsets.SequenceEqual(matrixY.Offsets);
            }

            return false;
        }
    }
}
