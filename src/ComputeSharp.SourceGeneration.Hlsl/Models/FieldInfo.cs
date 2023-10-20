using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A base type for a model describing a given captured field in a shader.
/// </summary>
/// <param name="FieldPath">The path of the field with respect to the shader instance.</param>
/// <param name="TypeName">The full metadata name for the primitive type.</param>
internal abstract record FieldInfo(EquatableArray<FieldPathPart> FieldPath, string TypeName)
{
    /// <summary>
    /// A captured primitive value (either a scalar, a vector, or a linear matrix).
    /// </summary>
    /// <param name="FieldPath"><inheritdoc cref="FieldInfo(EquatableArray{string}, string)" path="/param[@name='FieldPath']/node()"/></param>
    /// <param name="TypeName"><inheritdoc cref="FieldInfo(EquatableArray{string}, string)" path="/param[@name='TypeName']/node()"/></param>
    /// <param name="Offset">The byte offset of the value within the root signature.</param>
    public sealed record Primitive(EquatableArray<FieldPathPart> FieldPath, string TypeName, int Offset) : FieldInfo(FieldPath, TypeName);

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
        EquatableArray<FieldPathPart> FieldPath,
        string TypeName,
        string ElementName,
        int Rows,
        int Columns,
        EquatableArray<int> Offsets) : FieldInfo(FieldPath, TypeName);
}

/// <summary>
/// A model for a part of a field path, ie. the name of the immediate parent field, and whether it's accessible.
/// </summary>
/// <param name="Name">The name of the immediate parent field to access (or self).</param>
internal abstract record FieldPathPart(string Name)
{
    /// <summary>
    /// A model for a leaf part of a field path.
    /// </summary>
    /// <param name="Name"><inheritdoc cref="FieldPathPart(string, bool)" path="/param[@name='Name']/node()"/></param>
    public sealed record Leaf(string Name) : FieldPathPart(Name);

    /// <summary>
    /// A model for a nested field part path (ie. going through a nested struct type).
    /// </summary>
    /// <param name="Name"><inheritdoc cref="FieldPathPart(string, bool)" path="/param[@name='Name']/node()"/></param>
    /// <param name="TypeName">The fully qualified type name of the containing type.</param>
    public sealed record Nested(string Name, string TypeName) : FieldPathPart(Name);
}