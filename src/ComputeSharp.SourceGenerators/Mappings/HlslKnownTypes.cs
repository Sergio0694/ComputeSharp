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
            [typeof(bool).FullName] = "bool",
            [typeof(Bool).FullName] = "bool",
            [typeof(Bool2).FullName] = "bool2",
            [typeof(Bool3).FullName] = "bool3",
            [typeof(Bool4).FullName] = "bool4",
            [typeof(int).FullName] = "int",
            [typeof(Int2).FullName] = "int2",
            [typeof(Int3).FullName] = "int3",
            [typeof(Int4).FullName] = "int4",
            [typeof(uint).FullName] = "uint",
            [typeof(UInt2).FullName] = "uint2",
            [typeof(UInt3).FullName] = "uint3",
            [typeof(UInt4).FullName] = "uint4",
            [typeof(float).FullName] = "float",
            [typeof(Float2).FullName] = "float2",
            [typeof(Float3).FullName] = "float3",
            [typeof(Float4).FullName] = "float4",
            [typeof(Vector2).FullName] = "float2",
            [typeof(Vector3).FullName] = "float3",
            [typeof(Vector4).FullName] = "float4",
            [typeof(double).FullName] = "double",
            [typeof(Double2).FullName] = "double2",
            [typeof(Double3).FullName] = "double3",
            [typeof(Double4).FullName] = "double4",
            [typeof(ThreadIds).FullName] = "uint3",
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
