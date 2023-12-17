using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model describing a type info in a type hierarchy.
/// </summary>
/// <param name="QualifiedName">The qualified name for the type.</param>
/// <param name="Kind">The type of the type in the hierarchy.</param>
/// <param name="IsRecord">Whether the type is a record type.</param>
internal sealed record TypeInfo(string QualifiedName, TypeKind Kind, bool IsRecord)
{
    /// <summary>
    /// Gets the keyword for the current type kind.
    /// </summary>
    /// <returns>The keyword for the current type kind.</returns>
    public string GetTypeKeyword()
    {
        return Kind switch
        {
            TypeKind.Struct when IsRecord => "record struct",
            TypeKind.Struct => "struct",
            TypeKind.Interface => "interface",
            TypeKind.Class when IsRecord => "record",
            _ => "class"
        };
    }
}