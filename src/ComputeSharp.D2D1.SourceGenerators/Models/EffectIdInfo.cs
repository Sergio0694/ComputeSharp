using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing info to use to generate an effect id for a shader.
/// </summary>
/// <param name="Bytes">The bytes for the processed effect id.</param>
internal sealed record EffectIdInfo(EquatableArray<byte> Bytes);