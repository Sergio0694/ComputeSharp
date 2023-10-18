using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a shader.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="ThreadsX">The thread ids value for the X axis.</param>
/// <param name="ThreadsY">The thread ids value for the Y axis.</param>
/// <param name="ThreadsZ">The thread ids value for the Z axis.</param>
/// <param name="IsPixelShaderLike">Whether the compute shader is "pixel shader like", ie. outputting a pixel into a target texture.</param>
/// <param name="IsSamplerUsed">Whether or not the static sampler is used.</param>
/// <param name="ConstantBufferSizeInBytes">The size of the shader constant buffer.</param>
/// <param name="ResourceCount">The total number of captured resources in the shader.</param>
/// <param name="Fields">The description on shader instance fields.</param>
/// <param name="Resources">The description on shader captured resources.</param>
/// <param name="ResourceDescriptors">The sequence of resource descriptors for the shader.</param>
/// <param name="HlslInfoKey">The key with processed info on the shader.</param>
/// <param name="HlslInfo">The value with processed info on the shader.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record ShaderInfo(
    HierarchyInfo Hierarchy,
    int ThreadsX,
    int ThreadsY,
    int ThreadsZ,
    bool IsPixelShaderLike,
    bool IsSamplerUsed,
    int ConstantBufferSizeInBytes,
    int ResourceCount,
    EquatableArray<FieldInfo> Fields,
    EquatableArray<ResourceInfo> Resources,
    EquatableArray<ResourceDescriptor> ResourceDescriptors,
    HlslBytecodeInfoKey HlslInfoKey,
    HlslBytecodeInfo HlslInfo,
    EquatableArray<DiagnosticInfo> Diagnostcs) : IConstantBufferInfo;