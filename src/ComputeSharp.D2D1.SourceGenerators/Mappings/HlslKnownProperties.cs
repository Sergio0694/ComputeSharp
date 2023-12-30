using System.Collections.Generic;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownProperties
{
    /// <inheritdoc/>
    private static partial Dictionary<string, string> BuildKnownResourceIndexers()
    {
        return new()
        {
            [$"ComputeSharp.D2D1.D2D1ResourceTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
            [$"ComputeSharp.D2D1.D2D1ResourceTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3"
        };
    }

    /// <inheritdoc/>
    private static partial Dictionary<string, (int Rank, int Axis)> BuildKnownSizeAccessors()
    {
        return new()
        {
            ["ComputeSharp.D2D1.D2D1ResourceTexture1D`1.Width"] = (1, 0),
            ["ComputeSharp.D2D1.D2D1ResourceTexture2D`1.Width"] = (2, 0),
            ["ComputeSharp.D2D1.D2D1ResourceTexture2D`1.Height"] = (2, 1),
            ["ComputeSharp.D2D1.D2D1ResourceTexture3D`1.Width"] = (3, 0),
            ["ComputeSharp.D2D1.D2D1ResourceTexture3D`1.Height"] = (3, 1),
            ["ComputeSharp.D2D1.D2D1ResourceTexture3D`1.Depth"] = (3, 2)
        };
    }
}