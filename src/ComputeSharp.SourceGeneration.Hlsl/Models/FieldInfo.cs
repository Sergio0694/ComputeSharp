using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A base type for a model describing a given captured field in a shader.
/// </summary>
/// <param name="FieldPath">The path of the field with respect to the shader instance.</param>
/// <param name="TypeName">The full metadata name for the primitive type.</param>
internal abstract record FieldInfo(EquatableArray<string> FieldPath, string TypeName)
{
    /// <summary>
    /// A captured primitive value (either a scalar, a vector, or a linear matrix).
    /// </summary>
    /// <param name="FieldPath"><inheritdoc cref="FieldInfo(EquatableArray{string}, string)" path="/param[@name='FieldPath']/node()"/></param>
    /// <param name="TypeName"><inheritdoc cref="FieldInfo(EquatableArray{string}, string)" path="/param[@name='TypeName']/node()"/></param>
    /// <param name="Offset">The byte offset of the value within the root signature.</param>
    public sealed record Primitive(EquatableArray<string> FieldPath, string TypeName, int Offset) : FieldInfo(FieldPath, TypeName);

    /// <summary>
    /// A captured non linear matrix value.
    /// </summary>
    /// <param name="FieldPath"><inheritdoc cref="FieldInfo(EquatableArray{string}, string)" path="/param[@name='FieldPath']/node()"/></param>
    /// <param name="TypeName"><inheritdoc cref="FieldInfo(EquatableArray{string}, string)" path="/param[@name='TypeName']/node()"/></param>
    /// <param name="ElementName">The name of each element in the matrix type.</param>
    /// <param name="Rows">The number of rows in the matrix.</param>
    /// <param name="Columns">The number of columns in the matrix.</param>
    /// <param name="Offsets">The sequence of byte offsets for each row within the root signature.</param>
    public sealed record NonLinearMatrix(
        EquatableArray<string> FieldPath,
        string TypeName,
        string ElementName,
        int Rows,
        int Columns,
        EquatableArray<int> Offsets) : FieldInfo(FieldPath, TypeName);
}