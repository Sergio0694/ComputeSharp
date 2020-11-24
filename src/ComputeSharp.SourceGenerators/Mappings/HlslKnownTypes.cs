using System.Collections.Generic;
using System.Numerics;

namespace ComputeSharp.SourceGenerators.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL type names to common .NET types.
    /// </summary>
    internal static class HlslKnownTypes
    {
        /// <summary>
        /// The mapping of supported known types to HLSL types.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> KnownTypes = new Dictionary<string, string>
        {
            [typeof(bool).FullName!] = "bool",
            ["ComputeSharp.Bool"] = "bool",
            ["ComputeSharp.Bool2"] = "bool2",
            ["ComputeSharp.Bool3"] = "bool3",
            ["ComputeSharp.Bool4"] = "bool4",
            [typeof(int).FullName!] = "int",
            ["ComputeSharp.Int2"] = "int2",
            ["ComputeSharp.Int3"] = "int3",
            ["ComputeSharp.Int4"] = "int4",
            [typeof(uint).FullName!] = "uint",
            ["ComputeSharp.UInt2"] = "uint2",
            ["ComputeSharp.UInt3"] = "uint3",
            ["ComputeSharp.UInt4"] = "uint4",
            [typeof(float).FullName!] = "float",
            ["ComputeSharp.Float2"] = "float2",
            ["ComputeSharp.Float3"] = "float3",
            ["ComputeSharp.Float4"] = "float4",
            [typeof(Vector2).FullName] = "float2",
            [typeof(Vector3).FullName] = "float3",
            [typeof(Vector4).FullName] = "float4",
            [typeof(double).FullName!] = "double",
            ["ComputeSharp.Double2"] = "double2",
            ["ComputeSharp.Double3"] = "double3",
            ["ComputeSharp.Double4"] = "double4",
            ["ComputeSharp.ThreadIds"] = "uint3",
            ["ComputeSharp.ReadOnlyBuffer<T>"] = "StructuredBuffer",
            ["ComputeSharp.ReadWriteBuffer<T>"] = "RWStructuredBuffer"
        };

        /// <summary>
        /// Gets the mapped HLSL-compatible type name for the input type name.
        /// </summary>
        /// <param name="name">The input type name to map.</param>
        /// <returns>The HLSL-compatible type name that can be used in an HLSL shader.</returns>
        public static bool TryGetMappedName(string originalName, out string? mappedName)
        {
            return KnownTypes.TryGetValue(originalName, out mappedName);
        }
    }
}
