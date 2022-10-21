using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing info on a shader that has requested to be precompiled at build time.
/// </summary>
/// <param name="HlslSource">The HLSL source for the shader.</param>
/// <param name="ShaderProfile">The shader profile to use to compile the shader, or the default one.</param>
/// <param name="CompileOptions">The compile options to use to compile the shader, or the default one.</param>
/// <param name="Bytecode">The compiled shader bytecode, if available.</param>
internal sealed record EmbeddedBytecodeInfo(string HlslSource, D2D1ShaderProfile ShaderProfile, D2D1CompileOptions CompileOptions, EquatableArray<byte> Bytecode);