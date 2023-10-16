namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model describing a captured resource (either a buffer or a texture).
/// </summary>
/// <param name="FieldName">The name of the resource field.</param>
/// <param name="TypeName">The full metadata name for the resource type.</param>
/// <param name="Offset">The offset for the resource within the root signature.</param>
public sealed record ResourceInfo(string FieldName, string TypeName, int Offset);