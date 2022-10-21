using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader input types.
/// </summary>
/// <param name="InputTypes">The input types for a given shader.</param>
internal sealed record InputTypesInfo(EquatableArray<uint> InputTypes);