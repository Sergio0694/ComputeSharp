using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model containing info on shader dispatch metadata.
/// </summary>
/// <param name="Root32BitConstantCount">The size of the shader root signature, in 32 bit constants.</param>
/// <param name="IsSamplerUsed">Whether or not the static sampler is used.</param>
/// <param name="ResourceDescriptors">The sequence of resource descriptors for the shader.</param>
internal sealed record DispatchMetadataInfo(int Root32BitConstantCount, bool IsSamplerUsed, EquatableArray<ResourceDescriptor> ResourceDescriptors);