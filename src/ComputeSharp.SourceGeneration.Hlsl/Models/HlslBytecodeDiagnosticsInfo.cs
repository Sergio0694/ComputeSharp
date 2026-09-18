using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model capturing the info needed to synthesize diagnostics for compiled HLSL bytecode. This makes it possible
/// to create such diagnostics after the transform node has completed (symbols cannot be used past that point), which
/// in turn allows deferring the bytecode compilation (so it can be parallelized). Note that this model holds source
/// locations (which reference syntax trees), so it is deliberately kept separate from the shader models: it only
/// travels alongside them up to the node that creates the diagnostics, and it is dropped from that point onwards.
/// </summary>
/// <param name="TypeName">The fully qualified name of the shader type.</param>
/// <param name="TypeLocation">The location of the shader type, if available.</param>
/// <param name="HasRequiresDoublePrecisionSupportAttribute">Whether the shader type is annotated to require double precision support.</param>
/// <param name="RequiresDoublePrecisionSupportAttributeLocation">The location of the attribute requiring double precision support, if available.</param>
internal sealed record HlslBytecodeDiagnosticsInfo(
    string TypeName,
    Location? TypeLocation,
    bool HasRequiresDoublePrecisionSupportAttribute,
    Location? RequiresDoublePrecisionSupportAttributeLocation);
