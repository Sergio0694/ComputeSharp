using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing gathered input descriptions for a shader.
/// </summary>
/// <param name="InputDescriptions">The input descriptions for a given shader.</param>
internal sealed record InputDescriptionsInfo(EquatableArray<InputDescription> InputDescriptions);