namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model capturing the info needed to synthesize diagnostics for compiled HLSL bytecode.
/// This makes it possible to create such diagnostics after the transform node has completed,
/// which in turn allows deferring the bytecode compilation (so it can be parallelized).
/// </summary>
/// <param name="TypeName">The fully qualified name of the shader type.</param>
/// <param name="TypeLocation">The location of the shader type, if available.</param>
/// <param name="HasRequiresDoublePrecisionSupportAttribute">Whether the shader type is annotated to require double precision support.</param>
/// <param name="RequiresDoublePrecisionSupportAttributeLocation">The location of the attribute requiring double precision support, if present.</param>
internal sealed record HlslBytecodeDiagnosticsInfo(
    string TypeName,
    LocationInfo? TypeLocation,
    bool HasRequiresDoublePrecisionSupportAttribute,
    LocationInfo? RequiresDoublePrecisionSupportAttributeLocation);
