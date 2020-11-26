using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace ComputeSharp.SourceGenerators.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL member names to common .NET members.
    /// </summary>
    internal static class HlslKnownMembers
    {
        /// <summary>
        /// The mapping of supported known members to HLSL names.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> KnownMembers = BuildKnownMembersMap();

        /// <summary>
        /// Builds the mapping of supported known members to HLSL names.
        /// </summary>
        [Pure]
        private static IReadOnlyDictionary<string, string> BuildKnownMembersMap()
        {
            Dictionary<string, string> knownMembers = new()
            {
                [$"{typeof(ThreadIds).FullName}.{nameof(ThreadIds.X)}"] = "x",
                [$"{typeof(ThreadIds).FullName}.{nameof(ThreadIds.Y)}"] = "y",
                [$"{typeof(ThreadIds).FullName}.{nameof(ThreadIds.Z)}"] = "z",

                [$"{typeof(Vector2).FullName}.{nameof(Vector2.X)}"] = "x",
                [$"{typeof(Vector2).FullName}.{nameof(Vector2.Y)}"] = "y",
                [$"{typeof(Vector2).FullName}.{nameof(Vector2.Zero)}"] = "(float2)0",
                [$"{typeof(Vector2).FullName}.{nameof(Vector2.One)}"] = "float2(1.0f, 1.0f)",
                [$"{typeof(Vector2).FullName}.{nameof(Vector2.UnitX)}"] = "float2(1.0f, 0.0f)",
                [$"{typeof(Vector2).FullName}.{nameof(Vector2.UnitY)}"] = "float2(0.0f, 1.0f)",

                [$"{typeof(Vector3).FullName}.{nameof(Vector3.X)}"] = "x",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.Y)}"] = "y",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.Z)}"] = "z",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.Zero)}"] = "(float3)0",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.One)}"] = "float3(1.0f, 1.0f, 1.0f)",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.UnitX)}"] = "float3(1.0f, 0.0f, 0.0f)",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.UnitY)}"] = "float3(0.0f, 1.0f, 0.0f)",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.UnitZ)}"] = "float3(0.0f, 0.0f, 1.0f)",

                [$"{typeof(Vector4).FullName}.{nameof(Vector4.X)}"] = "x",
                [$"{typeof(Vector4).FullName}.{nameof(Vector4.Y)}"] = "y",
                [$"{typeof(Vector4).FullName}.{nameof(Vector4.Z)}"] = "z",
                [$"{typeof(Vector4).FullName}.{nameof(Vector4.W)}"] = "w",
                [$"{typeof(Vector4).FullName}.{nameof(Vector4.Zero)}"] = "(float4)0",
                [$"{typeof(Vector4).FullName}.{nameof(Vector4.One)}"] = "float4(1.0f, 1.0f, 1.0f, 1.0f)",

                [$"{typeof(Bool2).FullName}.{nameof(Bool2.False)}"] = "(bool2)0",
                [$"{typeof(Bool2).FullName}.{nameof(Bool2.True)}"] = "bool2(true, true)",
                [$"{typeof(Bool2).FullName}.{nameof(Bool2.TrueX)}"] = "bool2(true, false)",
                [$"{typeof(Bool2).FullName}.{nameof(Bool2.TrueY)}"] = "bool2(false, true)",

                [$"{typeof(Bool3).FullName}.{nameof(Bool3.False)}"] = "(bool3)0",
                [$"{typeof(Bool3).FullName}.{nameof(Bool3.True)}"] = "bool3(true, true, true)",
                [$"{typeof(Bool3).FullName}.{nameof(Bool3.TrueX)}"] = "bool3(true, false, false)",
                [$"{typeof(Bool3).FullName}.{nameof(Bool3.TrueY)}"] = "bool3(false, true, false)",
                [$"{typeof(Bool3).FullName}.{nameof(Bool3.TrueZ)}"] = "bool3(false, false, true)",

                [$"{typeof(Bool4).FullName}.{nameof(Bool4.False)}"] = "(bool4)0",
                [$"{typeof(Bool4).FullName}.{nameof(Bool4.True)}"] = "bool4(true, true, true, true)",
                [$"{typeof(Bool4).FullName}.{nameof(Bool4.TrueX)}"] = "bool4(true, false, false, false)",
                [$"{typeof(Bool4).FullName}.{nameof(Bool4.TrueY)}"] = "bool4(false, true, false, false)",
                [$"{typeof(Bool4).FullName}.{nameof(Bool4.TrueZ)}"] = "bool4(false, false, true, false)",
                [$"{typeof(Bool4).FullName}.{nameof(Bool4.TrueW)}"] = "bool4(false, false, false, true)",

                [$"{typeof(Int2).FullName}.{nameof(Int2.Zero)}"] = "(int2)0",
                [$"{typeof(Int2).FullName}.{nameof(Int2.One)}"] = "int2(1, 1)",
                [$"{typeof(Int2).FullName}.{nameof(Int2.UnitX)}"] = "int2(1, 0)",
                [$"{typeof(Int2).FullName}.{nameof(Int2.UnitY)}"] = "int2(0, 1)",

                [$"{typeof(Int3).FullName}.{nameof(Int3.Zero)}"] = "(int3)0",
                [$"{typeof(Int3).FullName}.{nameof(Int3.One)}"] = "int3(1, 1, 1)",
                [$"{typeof(Int3).FullName}.{nameof(Int3.UnitX)}"] = "int3(1, 0, 0)",
                [$"{typeof(Int3).FullName}.{nameof(Int3.UnitY)}"] = "int3(0, 1, 0)",
                [$"{typeof(Int3).FullName}.{nameof(Int3.UnitZ)}"] = "int3(0, 0, 1)",

                [$"{typeof(Int4).FullName}.{nameof(Int4.Zero)}"] = "(int4)0",
                [$"{typeof(Int4).FullName}.{nameof(Int4.One)}"] = "int4(1, 1, 1, 1)",
                [$"{typeof(Int4).FullName}.{nameof(Int4.UnitX)}"] = "int4(1, 0, 0, 0)",
                [$"{typeof(Int4).FullName}.{nameof(Int4.UnitY)}"] = "int4(0, 1, 0, 0)",
                [$"{typeof(Int4).FullName}.{nameof(Int4.UnitZ)}"] = "int4(0, 0, 1, 0)",
                [$"{typeof(Int4).FullName}.{nameof(Int4.UnitW)}"] = "int4(0, 0, 0, 1)",

                [$"{typeof(UInt2).FullName}.{nameof(UInt2.Zero)}"] = "(uint2)0",
                [$"{typeof(UInt2).FullName}.{nameof(UInt2.One)}"] = "uint2(1u, 1u)",
                [$"{typeof(UInt2).FullName}.{nameof(UInt2.UnitX)}"] = "uint2(1u, 0u)",
                [$"{typeof(UInt2).FullName}.{nameof(UInt2.UnitY)}"] = "uint2(0u, 1u)",

                [$"{typeof(UInt3).FullName}.{nameof(UInt3.Zero)}"] = "(uint3)0",
                [$"{typeof(UInt3).FullName}.{nameof(UInt3.One)}"] = "uint3(1u, 1u, 1u)",
                [$"{typeof(UInt3).FullName}.{nameof(UInt3.UnitX)}"] = "uint3(1u, 0u, 0u)",
                [$"{typeof(UInt3).FullName}.{nameof(UInt3.UnitY)}"] = "uint3(0u, 1u, 0u)",
                [$"{typeof(UInt3).FullName}.{nameof(UInt3.UnitZ)}"] = "uint3(0u, 0u, 1u)",

                [$"{typeof(UInt4).FullName}.{nameof(UInt4.Zero)}"] = "(uint4)0",
                [$"{typeof(UInt4).FullName}.{nameof(UInt4.One)}"] = "uint4(1u, 1u, 1u, 1u)",
                [$"{typeof(UInt4).FullName}.{nameof(UInt4.UnitX)}"] = "uint4(1u, 0u, 0u, 0u)",
                [$"{typeof(UInt4).FullName}.{nameof(UInt4.UnitY)}"] = "uint4(0u, 1u, 0u, 0u)",
                [$"{typeof(UInt4).FullName}.{nameof(UInt4.UnitZ)}"] = "uint4(0u, 0u, 1u, 0u)",
                [$"{typeof(UInt4).FullName}.{nameof(UInt4.UnitW)}"] = "uint4(0u, 0u, 0u, 1u)",

                [$"{typeof(Float2).FullName}.{nameof(Float2.Zero)}"] = "(float2)0",
                [$"{typeof(Float2).FullName}.{nameof(Float2.One)}"] = "float2(1.0f, 1.0f)",
                [$"{typeof(Float2).FullName}.{nameof(Float2.UnitX)}"] = "float2(1.0f, 0.0f)",
                [$"{typeof(Float2).FullName}.{nameof(Float2.UnitY)}"] = "float2(0.0f, 1.0f)",

                [$"{typeof(Float3).FullName}.{nameof(Float3.Zero)}"] = "(float3)0",
                [$"{typeof(Float3).FullName}.{nameof(Float3.One)}"] = "float3(1.0f, 1.0f, 1.0f)",
                [$"{typeof(Float3).FullName}.{nameof(Float3.UnitX)}"] = "float3(1.0f, 0.0f, 0.0f)",
                [$"{typeof(Float3).FullName}.{nameof(Float3.UnitY)}"] = "float3(0.0f, 1.0f, 0.0f)",
                [$"{typeof(Float3).FullName}.{nameof(Float3.UnitZ)}"] = "float3(0.0f, 0.0f, 1.0f)",

                [$"{typeof(Float4).FullName}.{nameof(Float4.Zero)}"] = "(float4)0",
                [$"{typeof(Float4).FullName}.{nameof(Float4.One)}"] = "float4(1.0f, 1.0f, 1.0f, 1.0f)",
                [$"{typeof(Float4).FullName}.{nameof(Float4.UnitX)}"] = "float4(1.0f, 0.0f, 0.0f, 0.0f)",
                [$"{typeof(Float4).FullName}.{nameof(Float4.UnitY)}"] = "float4(0.0f, 1.0f, 0.0f, 0.0f)",
                [$"{typeof(Float4).FullName}.{nameof(Float4.UnitZ)}"] = "float4(0.0f, 0.0f, 1.0f, 0.0f)",
                [$"{typeof(Float4).FullName}.{nameof(Float4.UnitW)}"] = "float4(0.0f, 0.0f, 0.0f, 1.0f)",

                [$"{typeof(Double2).FullName}.{nameof(Double2.Zero)}"] = "(double2)0",
                [$"{typeof(Double2).FullName}.{nameof(Double2.One)}"] = "double2(1.0, 1.0)",
                [$"{typeof(Double2).FullName}.{nameof(Double2.UnitX)}"] = "double2(1.0, 0.0)",
                [$"{typeof(Double2).FullName}.{nameof(Double2.UnitY)}"] = "double2(0.0, 1.0)",

                [$"{typeof(Double3).FullName}.{nameof(Double3.Zero)}"] = "(double3)0",
                [$"{typeof(Double3).FullName}.{nameof(Double3.One)}"] = "double3(1.0, 1.0, 1.0)",
                [$"{typeof(Double3).FullName}.{nameof(Double3.UnitX)}"] = "double3(1.0, 0.0, 0.0)",
                [$"{typeof(Double3).FullName}.{nameof(Double3.UnitY)}"] = "double3(0.0, 1.0, 0.0)",
                [$"{typeof(Double3).FullName}.{nameof(Double3.UnitZ)}"] = "double3(0.0, 0.0, 1.0)",

                [$"{typeof(Double4).FullName}.{nameof(Double4.Zero)}"] = "(double4)0",
                [$"{typeof(Double4).FullName}.{nameof(Double4.One)}"] = "double4(1.0, 1.0, 1.0, 1.0)",
                [$"{typeof(Double4).FullName}.{nameof(Double4.UnitX)}"] = "double4(1.0, 0.0, 0.0, 0.0)",
                [$"{typeof(Double4).FullName}.{nameof(Double4.UnitY)}"] = "double4(0.0, 1.0, 0.0, 0.0)",
                [$"{typeof(Double4).FullName}.{nameof(Double4.UnitZ)}"] = "double4(0.0, 0.0, 1.0, 0.0)",
                [$"{typeof(Double4).FullName}.{nameof(Double4.UnitW)}"] = "double4(0.0, 0.0, 0.0, 1.0)"
            };

            // Programmatically load mappings for the instance members of the HLSL vector types
            foreach (var item in
                from type in HlslKnownTypes.HlslMappedVectorTypes
                from property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                select (Type: type, Property: property))
            {
                knownMembers.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $"{item.Property.Name.ToLower()}");
            }

            return knownMembers;
        }

        /// <summary>
        /// Tries to get the mapped HLSL-compatible member name for the input member name.
        /// </summary>
        /// <param name="name">The input fully qualified member name.</param>
        /// <param name="mapped">The mapped name, if one is found.</param>
        /// <returns>The HLSL-compatible member name that can be used in an HLSL shader.</returns>
        [Pure]
        public static bool TryGetMappedName(string name, out string? mapped)
        {
            return KnownMembers.TryGetValue(name, out mapped);
        }
    }
}
