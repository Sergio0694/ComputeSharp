using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a shader dispatch id.
/// </summary>
/// <param name="Delegates">The list of delegate field names for the shader.</param>
internal sealed record DispatchIdInfo(EquatableArray<string> Delegates);