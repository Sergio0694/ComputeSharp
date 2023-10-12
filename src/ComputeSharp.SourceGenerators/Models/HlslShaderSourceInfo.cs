using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL shader.
/// </summary>
/// <param name="HeaderAndThreadsX">The shader generated header and <c>threadsX</c> count declaration.</param>
/// <param name="ThreadsY">The <c>threadsY</c> count declaration.</param>
/// <param name="ThreadsZ">The <c>threadsZ</c> count declaration.</param>
/// <param name="Defines">The define statements, if any.</param>
/// <param name="StaticFieldsAndDeclaredTypes">The static fields and declared types, if any.</param>
/// <param name="CapturedFieldsAndResourcesAndForwardDeclarations">The captured fields, and method forward declarations.</param>
/// <param name="CapturedMethods">The captured method implementations.</param>
/// <param name="EntryPoint">The shader entry point.</param>
/// <param name="ImplicitTextureType">The type of the implicit texture type, if any.</param>
/// <param name="IsSamplerUsed">Whether or not the static sampler is used.</param>
/// <param name="DefinedTypes">The names of declared types.</param>
/// <param name="DefinedConstants">The names of defined constants.</param>
/// <param name="MethodSignatures">The signatures for captured methods.</param>
internal sealed record HlslShaderSourceInfo(
    string HeaderAndThreadsX,
    string ThreadsY,
    string ThreadsZ,
    string Defines,
    string StaticFieldsAndDeclaredTypes,
    string CapturedFieldsAndResourcesAndForwardDeclarations,
    string CapturedMethods,
    string EntryPoint,
    string? ImplicitTextureType,
    bool IsSamplerUsed,
    EquatableArray<string> DefinedTypes,
    EquatableArray<string> DefinedConstants,
    EquatableArray<string> MethodSignatures);