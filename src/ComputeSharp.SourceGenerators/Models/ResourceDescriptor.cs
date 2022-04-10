namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A resource descriptor for a captured resource in a shader.
/// </summary>
/// <param name="TypeId">The type id for the resource.</param>
/// <param name="RegisterOffset">The offset of the resource in its containing register.</param>
/// <param name="Offset">The offset of the resource descriptor in the shader root signature.</param>
internal sealed record ResourceDescriptor(int TypeId, int RegisterOffset, int Offset);
