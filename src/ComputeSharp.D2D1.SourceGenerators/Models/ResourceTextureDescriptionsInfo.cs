using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing gathered resource texture descriptions for a shader.
/// </summary>
/// <param name="ResourceTextureDescriptions">The resource textures for the current shader.</param>
internal sealed record ResourceTextureDescriptionsInfo(EquatableArray<ResourceTextureDescription> ResourceTextureDescriptions);