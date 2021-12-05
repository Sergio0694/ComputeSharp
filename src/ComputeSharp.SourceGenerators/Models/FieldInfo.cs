using System.Collections.Immutable;

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
}
