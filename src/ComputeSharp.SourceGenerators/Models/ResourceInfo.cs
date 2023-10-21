namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model describing a captured resource (either a buffer or a texture).
/// </summary>
/// <param name="FieldName">The name of the resource field.</param>
/// <param name="UnspeakableName">The unspeakable name of the field, if present.</param>
public sealed record ResourceInfo(string FieldName, string? UnspeakableName);