using System.Collections.Generic;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownProperties
{
    /// <inheritdoc/>
    private static partial IReadOnlyDictionary<string, string> BuildKnownResourceIndexers()
    {
        return new Dictionary<string, string>
        {
            [$"ComputeSharp.D2D1.D2D1ResourceTexture2D.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
            [$"ComputeSharp.D2D1.D2D1ResourceTexture3D.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3"
        };
    }

    /// <inheritdoc/>
    private static partial IReadOnlyDictionary<string, (int Rank, int Axis)> BuildKnownSizeAccessors()
    {
        return new Dictionary<string, (int, int)>
        {
            ["ComputeSharp.D2D1.D2D1ResourceTexture1D.Width"] = (1, 0),
            ["ComputeSharp.D2D1.D2D1ResourceTexture2D.Width"] = (2, 0),
            ["ComputeSharp.D2D1.D2D1ResourceTexture2D.Height"] = (2, 1),
            ["ComputeSharp.D2D1.D2D1ResourceTexture3D.Width"] = (3, 0),
            ["ComputeSharp.D2D1.D2D1ResourceTexture3D.Height"] = (3, 1),
            ["ComputeSharp.D2D1.D2D1ResourceTexture3D.Depth"] = (3, 2)
        };
    }
}
