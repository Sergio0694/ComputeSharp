namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing an input description for a shader.
/// </summary>
/// <param name="Index">The index of the input resource the description is for.</param>
/// <param name="Filter">The input filter to use.</param>
/// <param name="LevelOfDetailCount">The level of detail to use.</param>
internal sealed record InputDescription(int Index, D2D1Filter Filter, int LevelOfDetailCount);