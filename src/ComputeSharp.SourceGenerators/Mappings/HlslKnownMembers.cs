using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using ComputeSharp.SourceGenerators.Mappings;

namespace ComputeSharp.Shaders.Mappings
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
                // ThreadIds
                ["ComputeSharp.ThreadIds.X"] = "x",
                ["ComputeSharp.ThreadIds.Y"] = "y",
                ["ComputeSharp.ThreadIds.Z"] = "z",

                // Vector2
                ["System.Numerics.Vector2.X"] = "x",
                ["System.Numerics.Vector2.Y"] = "y",
                ["System.Numerics.Vector2.Zero"] = "(float2)0",
                ["System.Numerics.Vector2.One"] = "float2(1.0f, 1.0f)",
                ["System.Numerics.Vector2.UnitX"] = "float2(1.0f, 0.0f)",
                ["System.Numerics.Vector2.UnitY"] = "float2(0.0f, 1.0f)",

                // Vector3
                ["System.Numerics.Vector3.X"] = "x",
                ["System.Numerics.Vector3.Y"] = "y",
                ["System.Numerics.Vector3.Z"] = "z",
                ["System.Numerics.Vector3.Zero"] = "(float3)0",
                ["System.Numerics.Vector3.One"] = "float3(1.0f, 1.0f, 1.0f)",
                ["System.Numerics.Vector3.UnitX"] = "float3(1.0f, 0.0f, 0.0f)",
                ["System.Numerics.Vector3.UnitY"] = "float3(0.0f, 1.0f, 0.0f)",
                ["System.Numerics.Vector3.UnitZ"] = "float3(0.0f, 0.0f, 1.0f)",

                // Vector4
                ["System.Numerics.Vector4.X"] = "x",
                ["System.Numerics.Vector4.Y"] = "y",
                ["System.Numerics.Vector4.Z"] = "z",
                ["System.Numerics.Vector4.W"] = "w",
                ["System.Numerics.Vector4.Zero"] = "(float4)0",
                ["System.Numerics.Vector4.One"] = "float4(1.0f, 1.0f, 1.0f, 1.0f)",

                // Bool2
                ["ComputeSharp.Bool2.False"] = "(bool2)0",
                ["ComputeSharp.Bool2.True"] = "bool2(true, true)",
                ["ComputeSharp.Bool2.TrueX"] = "bool2(true, false)",
                ["ComputeSharp.Bool2.TrueY"] = "bool2(false, true)",

                // Bool3
                ["ComputeSharp.Bool3.False"] = "(bool3)0",
                ["ComputeSharp.Bool3.True"] = "bool3(true, true, true)",
                ["ComputeSharp.Bool3.TrueX"] = "bool3(true, false, false)",
                ["ComputeSharp.Bool3.TrueY"] = "bool3(false, true, false)",
                ["ComputeSharp.Bool3.TrueZ"] = "bool3(false, false, true)",

                // Bool4
                ["ComputeSharp.Bool4.False"] = "(bool4)0",
                ["ComputeSharp.Bool4.True"] = "bool4(true, true, true, true)",
                ["ComputeSharp.Bool4.TrueX"] = "bool4(true, false, false, false)",
                ["ComputeSharp.Bool4.TrueY"] = "bool4(false, true, false, false)",
                ["ComputeSharp.Bool4.TrueZ"] = "bool4(false, false, true, false)",
                ["ComputeSharp.Bool4.TrueW"] = "bool4(false, false, false, true)",

                // Int2
                ["ComputeSharp.Int2.Zero"] = "(int2)0",
                ["ComputeSharp.Int2.One"] = "int2(1, 1)",
                ["ComputeSharp.Int2.UnitX"] = "int2(1, 0)",
                ["ComputeSharp.Int2.UnitY"] = "int2(0, 1)",

                // Int3
                ["ComputeSharp.Int3.Zero"] = "(int3)0",
                ["ComputeSharp.Int3.One"] = "int3(1, 1, 1)",
                ["ComputeSharp.Int3.UnitX"] = "int3(1, 0, 0)",
                ["ComputeSharp.Int3.UnitY"] = "int3(0, 1, 0)",
                ["ComputeSharp.Int3.UnitZ"] = "int3(0, 0, 1)",

                // Int4
                ["ComputeSharp.Int4.Zero"] = "(int4)0",
                ["ComputeSharp.Int4.One"] = "int4(1, 1, 1, 1)",
                ["ComputeSharp.Int4.UnitX"] = "int4(1, 0, 0, 0)",
                ["ComputeSharp.Int4.UnitY"] = "int4(0, 1, 0, 0)",
                ["ComputeSharp.Int4.UnitZ"] = "int4(0, 0, 1, 0)",
                ["ComputeSharp.Int4.UnitW"] = "int4(0, 0, 0, 1)",

                // UInt2
                ["ComputeSharp.UInt2.Zero"] = "(uint2)0",
                ["ComputeSharp.UInt2.One"] = "uint2(1u, 1u)",
                ["ComputeSharp.UInt2.UnitX"] = "uint2(1u, 0u)",
                ["ComputeSharp.UInt2.UnitY"] = "uint2(0u, 1u)",

                // UInt3
                ["ComputeSharp.UInt3.Zero"] = "(uint3)0",
                ["ComputeSharp.UInt3.One"] = "uint3(1u, 1u, 1u)",
                ["ComputeSharp.UInt3.UnitX"] = "uint3(1u, 0u, 0u)",
                ["ComputeSharp.UInt3.UnitY"] = "uint3(0u, 1u, 0u)",
                ["ComputeSharp.UInt3.UnitZ"] = "uint3(0u, 0u, 1u)",

                // UInt4
                ["ComputeSharp.UInt4.Zero"] = "(uint4)0",
                ["ComputeSharp.UInt4.One"] = "uint4(1u, 1u, 1u, 1u)",
                ["ComputeSharp.UInt4.UnitX"] = "uint4(1u, 0u, 0u, 0u)",
                ["ComputeSharp.UInt4.UnitY"] = "uint4(0u, 1u, 0u, 0u)",
                ["ComputeSharp.UInt4.UnitZ"] = "uint4(0u, 0u, 1u, 0u)",
                ["ComputeSharp.UInt4.UnitW"] = "uint4(0u, 0u, 0u, 1u)",

                // Float2
                ["ComputeSharp.Float2.Zero"] = "(float2)0",
                ["ComputeSharp.Float2.One"] = "float2(1.0f, 1.0f)",
                ["ComputeSharp.Float2.UnitX"] = "float2(1.0f, 0.0f)",
                ["ComputeSharp.Float2.UnitY"] = "float2(0.0f, 1.0f)",

                // Float3
                ["ComputeSharp.Float3.Zero"] = "(float3)0",
                ["ComputeSharp.Float3.One"] = "float3(1.0f, 1.0f, 1.0f)",
                ["ComputeSharp.Float3.UnitX"] = "float3(1.0f, 0.0f, 0.0f)",
                ["ComputeSharp.Float3.UnitY"] = "float3(0.0f, 1.0f, 0.0f)",
                ["ComputeSharp.Float3.UnitZ"] = "float3(0.0f, 0.0f, 1.0f)",

                // Float4
                ["ComputeSharp.Float4.Zero"] = "(float4)0",
                ["ComputeSharp.Float4.One"] = "float4(1.0f, 1.0f, 1.0f, 1.0f)",
                ["ComputeSharp.Float4.UnitX"] = "float4(1.0f, 0.0f, 0.0f, 0.0f)",
                ["ComputeSharp.Float4.UnitY"] = "float4(0.0f, 1.0f, 0.0f, 0.0f)",
                ["ComputeSharp.Float4.UnitZ"] = "float4(0.0f, 0.0f, 1.0f, 0.0f)",
                ["ComputeSharp.Float4.UnitW"] = "float4(0.0f, 0.0f, 0.0f, 1.0f)",

                // Double2
                ["ComputeSharp.Double2.Zero"] = "(double2)0",
                ["ComputeSharp.Double2.One"] = "double2(1.0, 1.0)",
                ["ComputeSharp.Double2.UnitX"] = "double2(1.0, 0.0)",
                ["ComputeSharp.Double2.UnitY"] = "double2(0.0, 1.0)",

                // Double3
                ["ComputeSharp.Double3.Zero"] = "(double3)0",
                ["ComputeSharp.Double3.One"] = "double3(1.0, 1.0, 1.0)",
                ["ComputeSharp.Double3.UnitX"] = "double3(1.0, 0.0, 0.0)",
                ["ComputeSharp.Double3.UnitY"] = "double3(0.0, 1.0, 0.0)",
                ["ComputeSharp.Double3.UnitZ"] = "double3(0.0, 0.0, 1.0)",

                // Double4
                ["ComputeSharp.Double4.Zero"] = "(double4)0",
                ["ComputeSharp.Double4.One"] = "double4(1.0, 1.0, 1.0, 1.0)",
                ["ComputeSharp.Double4.UnitX"] = "double4(1.0, 0.0, 0.0, 0.0)",
                ["ComputeSharp.Double4.UnitY"] = "double4(0.0, 1.0, 0.0, 0.0)",
                ["ComputeSharp.Double4.UnitZ"] = "double4(0.0, 0.0, 1.0, 0.0)",
                ["ComputeSharp.Double4.UnitW"] = "double4(0.0, 0.0, 0.0, 1.0)"
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
