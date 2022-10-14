namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing gathered info on a embedded thread ids.
/// </summary>
/// <param name="IsDefault">Whether the values are discarded.</param>
/// <param name="X">The thread ids value for the X axis.</param>
/// <param name="Y">The thread ids value for the Y axis.</param>
/// <param name="Z">The thread ids value for the Z axis.</param>
internal sealed record ThreadIdsInfo(bool IsDefault, int X, int Y, int Z);