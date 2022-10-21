using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing a compiled shader bytecode, or none.
/// </summary>
/// <param name="X">The thread ids value for the X axis.</param>
/// <param name="Y">The thread ids value for the Y axis.</param>
/// <param name="Z">The thread ids value for the Z axis.</param>
/// <param name="Bytecode">The compiled shader bytecode, if available.</param>
internal sealed record EmbeddedBytecodeInfo(int X, int Y, int Z, EquatableArray<byte> Bytecode);