using System.Collections.Generic;
using System.Numerics;

namespace ComputeSharp.SourceGenerators.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL types to their alignment sizes.
    /// </summary>
    internal static class HlslKnownSizes
    {
        /// <summary>
        /// Gets the mapping of supported known sizes to HLSL type names.
        /// </summary>
        public static IReadOnlyDictionary<string, (int Size, int Pack)> KnownSizes { get; } = new Dictionary<string, (int, int)>
        {
            [typeof(bool).FullName] = (4, 4),
            [typeof(int).FullName] = (4, 4),
            [typeof(int).FullName] = (4, 4),
            [typeof(float).FullName] = (4, 4),
            [typeof(double).FullName] = (8, 8),

            [typeof(Vector2).FullName] = (8, 4),
            [typeof(Vector3).FullName] = (12, 4),
            [typeof(Vector4).FullName] = (16, 4),

            [typeof(Bool2).FullName] = (8, 4),
            [typeof(Bool3).FullName] = (12, 4),
            [typeof(Bool4).FullName] = (16, 4),

            [typeof(Int2).FullName] = (8, 4),
            [typeof(Int3).FullName] = (12, 4),
            [typeof(Int4).FullName] = (16, 4),

            [typeof(UInt2).FullName] = (8, 4),
            [typeof(UInt3).FullName] = (12, 4),
            [typeof(UInt4).FullName] = (16, 4),

            [typeof(Float2).FullName] = (8, 4),
            [typeof(Float3).FullName] = (12, 4),
            [typeof(Float4).FullName] = (16, 4),

            [typeof(Double2).FullName] = (16, 8),
            [typeof(Double3).FullName] = (24, 8),
            [typeof(Double3).FullName] = (32, 8)
        };
    }
}
