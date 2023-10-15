namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A resource descriptor for a captured resource in a shader.
/// </summary>
/// <param name="TypeId">The type id for the resource.</param>
/// <param name="Register">The register offset for the resource descriptor.</param>
internal sealed record ResourceDescriptor(int TypeId, int Register);