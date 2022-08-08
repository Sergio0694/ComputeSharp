namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing a resource texture description for a shader.
/// </summary>
/// <param name="Index">The index of the resource texture resource the description is for.</param>
/// <param name="Rank">The rank of the resource texture.</param>
internal sealed record ResourceTextureDescription(uint Index, uint Rank);
