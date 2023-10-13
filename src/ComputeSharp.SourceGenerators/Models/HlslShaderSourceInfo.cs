namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL shader.
/// </summary>
/// <param name="HlslSource">The HLSL source for the shader.</param>
/// <param name="IsSamplerUsed">Whether or not the static sampler is used.</param>
internal sealed record HlslShaderSourceInfo(string HlslSource, bool IsSamplerUsed);