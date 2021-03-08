using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ComputeSharp.SourceGenerators.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL member names to common .NET members.
    /// </summary>
    internal static class HlslKnownMembers
    {
        /// <summary>
        /// The mapping of supported known indexers to HLSL resource type names.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> KnownResourceIndexers = new Dictionary<string, string>
        {
            [$"ComputeSharp.ReadOnlyTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
            [$"ComputeSharp.ReadOnlyTexture2D`2.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
            [$"ComputeSharp.ReadWriteTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
            [$"ComputeSharp.ReadWriteTexture2D`2.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
            [$"ComputeSharp.IReadOnlyTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
            [$"ComputeSharp.IReadWriteTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
            [$"ComputeSharp.ReadOnlyTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
            [$"ComputeSharp.ReadOnlyTexture3D`2.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
            [$"ComputeSharp.ReadWriteTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
            [$"ComputeSharp.ReadWriteTexture3D`2.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
            [$"ComputeSharp.IReadOnlyTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
            [$"ComputeSharp.IReadWriteTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3"
        };

        /// <summary>
        /// The mapping of supported known size accessors for HLSL resource types.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, (int Rank, int Axis)> KnownSizeAccessors = new Dictionary<string, (int, int)>
        {
            ["ComputeSharp.Resources.Buffer`1.Length"] = (2, 0),
            ["ComputeSharp.Resources.Texture2D`1.Width"] = (2, 0),
            ["ComputeSharp.Resources.Texture2D`1.Height"] = (2, 1),
            ["ComputeSharp.Resources.Texture3D`1.Width"] = (3, 0),
            ["ComputeSharp.Resources.Texture3D`1.Height"] = (3, 1),
            ["ComputeSharp.Resources.Texture3D`1.Depth"] = (3, 2),
            ["ComputeSharp.IReadOnlyTexture2D`1.Width"] = (2, 0),
            ["ComputeSharp.IReadOnlyTexture2D`1.Height"] = (2, 1),
            ["ComputeSharp.IReadOnlyTexture3D`1.Width"] = (3, 0),
            ["ComputeSharp.IReadOnlyTexture3D`1.Height"] = (3, 1),
            ["ComputeSharp.IReadOnlyTexture3D`1.Depth"] = (3, 2),
            ["ComputeSharp.IReadWriteTexture2D`1.Width"] = (2, 0),
            ["ComputeSharp.IReadWriteTexture2D`1.Height"] = (2, 1),
            ["ComputeSharp.IReadWriteTexture3D`1.Width"] = (3, 0),
            ["ComputeSharp.IReadWriteTexture3D`1.Height"] = (3, 1),
            ["ComputeSharp.IReadWriteTexture3D`1.Depth"] = (3, 2)
        };

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
                from type in HlslKnownTypes.KnownVectorTypes
                from property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                select (Type: type, Property: property))
            {
                knownMembers.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $"{item.Property.Name.ToLower()}");
            }

            // Load mappings for the matrix properties as well
            foreach (var item in
                from type in HlslKnownTypes.KnownMatrixTypes
                from property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                where Regex.IsMatch(property.Name, "^M[1-4]{2}$")
                select (Type: type, Property: property))
            {
                char
                    row = (char)(item.Property.Name[1] - 1),
                    column = (char)(item.Property.Name[2] - 1);

                knownMembers.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $"_m{row}{column}");
            }

            // Store GroupIds.Index for a quicker comparison afterwards
            PropertyInfo groupindexProperty = typeof(GroupIds).GetProperty(nameof(GroupIds.Index), BindingFlags.Static | BindingFlags.Public);

            // Programmatically load mappings for the dispatch types
            foreach (var item in
                from type in HlslKnownTypes.HlslDispatchTypes
                from property in type.GetProperties(BindingFlags.Static | BindingFlags.Public)
                select (Type: type, Property: property))
            {
                if (item.Property == groupindexProperty)
                {
                    // The thread group index is a standalone parameter, so if this property is used, we
                    // just map the access directly to that implicit and hidden parameter name instead.
                    knownMembers.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $"__{nameof(GroupIds)}__get_Index");
                }
                else
                {
                    knownMembers.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $"{item.Type.Name}.{item.Property.Name.ToLower()}");
                }
            }

            // Programmatically load mappings for the normalized thread ids
            foreach (var property in typeof(ThreadIds.Normalized).GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                string key = $"{typeof(ThreadIds).FullName}{Type.Delimiter}{typeof(ThreadIds.Normalized).Name}{Type.Delimiter}{property.Name}";

                switch (property.Name)
                {
                    case string name when name.Length == 1:
                        knownMembers.Add(key, $"{typeof(ThreadIds).Name}.{char.ToLower(name[0])} / (float)__{char.ToLower(name[0])}");
                        break;
                    case string name when name.Length == 2:
                    {
                        string
                            numerator = $"float2({typeof(ThreadIds).Name}.{char.ToLower(name[0])}, {typeof(ThreadIds).Name}.{char.ToLower(name[1])})",
                            denominator = $"float2(__{char.ToLower(name[0])}, __{char.ToLower(name[1])})";

                        knownMembers.Add(key, $"{numerator} / {denominator}");
                        break;
                    }
                    case string name when name.Length == 3:
                    {
                        string
                            numerator = $"float3({typeof(ThreadIds).Name}.{char.ToLower(name[0])}, {typeof(ThreadIds).Name}.{char.ToLower(name[1])}, {typeof(ThreadIds).Name}.{char.ToLower(name[2])})",
                            denominator = $"float3(__{char.ToLower(name[0])}, __{char.ToLower(name[1])}, __{char.ToLower(name[2])})";

                        knownMembers.Add(key, $"{numerator} / {denominator}");
                        break;
                    }
                }
            }

            // Programmatically load mappings for the group size
            foreach (var property in typeof(GroupSize).GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                string key = $"{typeof(GroupSize).FullName}{Type.Delimiter}{property.Name}";

                switch (property.Name)
                {
                    case nameof(GroupSize.Count):
                        knownMembers.Add(key, "__GroupSize__get_X * __GroupSize__get_Y * __GroupSize__get_Z");
                        break;
                    case string name when name.Length == 1:
                        knownMembers.Add(key, $"__GroupSize__get_{name}");
                        break;
                    case string name when name.Length == 2:
                        knownMembers.Add(key, $"int2(__GroupSize__get_{name[0]}, __GroupSize__get_{name[1]})");
                        break;
                    case string name when name.Length == 3:
                        knownMembers.Add(key, $"int3(__GroupSize__get_{name[0]}, __GroupSize__get_{name[1]}, __GroupSize__get_{name[2]})");
                        break;
                }
            }

            // Programmatically load mappings for the dispatch size
            foreach (var property in typeof(DispatchSize).GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                string key = $"{typeof(DispatchSize).FullName}{Type.Delimiter}{property.Name}";

                switch (property.Name)
                {
                    case nameof(DispatchSize.Count):
                        knownMembers.Add(key, "__x * __y * __z");
                        break;
                    case string name when name.Length == 1:
                        knownMembers.Add(key, $"__{char.ToLower(name[0])}");
                        break;
                    case string name when name.Length == 2:
                        knownMembers.Add(key, $"int2(__{char.ToLower(name[0])}, __{char.ToLower(name[1])})");
                        break;
                    case string name when name.Length == 3:
                        knownMembers.Add(key, $"int3(__{char.ToLower(name[0])}, __{char.ToLower(name[1])}, __{char.ToLower(name[2])})");
                        break;
                }
            }

            return knownMembers;
        }

        /// <summary>
        /// The mapping of supported known members to HLSL names.
        /// </summary>
        private static readonly IReadOnlyCollection<string> KnownMatrixIndexers = BuildKnownMatrixIndexers();

        /// <summary>
        /// Builds the mapping of swizzled matrix indexer properties.
        /// </summary>
        [Pure]
        private static IReadOnlyCollection<string> BuildKnownMatrixIndexers()
        {
            return (
                from type in Assembly.GetExecutingAssembly().ExportedTypes
                where Regex.IsMatch(type.FullName, @"ComputeSharp\.(Bool|Double|Float|Int|UInt)[1-4]x[1-4]")
                from property in type.GetProperties()
                let indices = property.GetIndexParameters()
                where indices.Length > 0 &&
                      indices.All(static index => index.ParameterType == typeof(MatrixIndex))
                let metadataName = $"{type.FullName}.this[{string.Join(", ", indices.Select(index => index.ParameterType))}]"
                select metadataName).ToImmutableHashSet();
        }

        /// <summary>
        /// The mapping of supported known members to HLSL names.
        /// </summary>
        private static readonly IReadOnlyCollection<string> KnownMatrixIndices = BuildKnownMatrixIndices();

        /// <summary>
        /// Builds the mapping of swizzled matrix indices.
        /// </summary>
        [Pure]
        private static IReadOnlyCollection<string> BuildKnownMatrixIndices()
        {
            return (
                from name in Enum.GetNames(typeof(MatrixIndex))
                select $"{typeof(MatrixIndex).FullName}.{name}").ToImmutableHashSet();
        }

        /// <summary>
        /// Checks whether or not a given property fullname matches a matrix swizzled indexer.
        /// </summary>
        /// <param name="name">The input fully qualified member name.</param>
        /// <returns>Whether or not <paramref name="name"/> is a matrix swizzled indexer.</returns>
        [Pure]
        public static bool IsKnownMatrixIndexer(string name)
        {
            return KnownMatrixIndexers.Contains(name);
        }

        /// <summary>
        /// Checks whether or not a given property fullname matches a matrix swizzled index.
        /// </summary>
        /// <param name="name">The input fully qualified member name.</param>
        /// <returns>Whether or not <paramref name="name"/> is a matrix swizzled index.</returns>
        [Pure]
        public static bool IsKnownMatrixIndex(string name)
        {
            return KnownMatrixIndices.Contains(name);
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

        /// <summary>
        /// Tries to get the mapped HLSL-compatible indexer resource type name for the input indexer name.
        /// </summary>
        /// <param name="name">The input fully qualified indexer name.</param>
        /// <param name="mapped">The mapped type name, if one is found.</param>
        /// <returns>The HLSL-compatible type name that can be used in an HLSL shader for the given indexer.</returns>
        [Pure]
        public static bool TryGetMappedResourceIndexerTypeName(string name, out string? mapped)
        {
            return KnownResourceIndexers.TryGetValue(name, out mapped);
        }

        /// <summary>
        /// Tries to get the mapped HLSL-compatible indexer vector type name for the input indexer name.
        /// </summary>
        /// <param name="name">The input fully qualified indexer name.</param>
        /// <param name="mapped">The mapped type name, if one is found.</param>
        /// <returns>The HLSL-compatible type name that can be used in an HLSL shader for the given indexer.</returns>
        [Pure]
        public static bool TryGetAccessorRankAndAxis(string name, out int rank, out int axis)
        {
            if (KnownSizeAccessors.TryGetValue(name, out var info))
            {
                (rank, axis) = info;

                return true;
            }

            (rank, axis) = default((int, int));

            return false;
        }
    }
}
