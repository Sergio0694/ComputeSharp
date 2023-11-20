using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a D2D1 shader.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="EffectId">The GUID bytes for the effect id.</param>
/// <param name="EffectDisplayName">The effect display name for the shader type, if available.</param>
/// <param name="EffectDescription">The effect description for the shader type, if available.</param>
/// <param name="EffectCategory">The effect category for the shader type, if available.</param>
/// <param name="EffectAuthor">The effect author for the shader type, if available.</param>
/// <param name="ConstantBufferSizeInBytes">The size of the shader constant buffer.</param>
/// <param name="InputTypes">The gathered input types for the shader.</param>
/// <param name="InputDescriptions">The gathered input descriptions for the shader.</param>
/// <param name="ResourceTextureDescriptions">The gathered resource texture descriptions for the shader.</param>
/// <param name="Fields">The description on shader instance fields.</param>
/// <param name="BufferPrecision">The buffer precision for the resulting output buffer.</param>
/// <param name="ChannelDepth">The channel depth for the resulting output buffer.</param>
/// <param name="PixelOptions">The pixel options used by the shader.</param>
/// <param name="HlslInfoKey">The key with processed info on the shader.</param>
/// <param name="HlslInfo">The value with processed info on the shader.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record D2D1ShaderInfo(
    HierarchyInfo Hierarchy,
    EquatableArray<byte> EffectId,
    string? EffectDisplayName,
    string? EffectDescription,
    string? EffectCategory,
    string? EffectAuthor,
    int ConstantBufferSizeInBytes,
    EquatableArray<uint> InputTypes,
    EquatableArray<InputDescription> InputDescriptions,
    EquatableArray<ResourceTextureDescription> ResourceTextureDescriptions,
    EquatableArray<FieldInfo> Fields,
    D2D1BufferPrecision BufferPrecision,
    D2D1ChannelDepth ChannelDepth,
    D2D1PixelOptions PixelOptions,
    HlslBytecodeInfoKey HlslInfoKey,
    HlslBytecodeInfo HlslInfo,
    EquatableArray<DiagnosticInfo> Diagnostcs) : IConstantBufferInfo;