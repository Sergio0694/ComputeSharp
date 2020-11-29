using System;
using System.Collections.Generic;
using System.Numerics;
using ComputeSharp.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis;

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
        };

        /// <summary>
        /// Gets the known HLSL vector types available as mapped types.
        /// </summary>
        public static IReadOnlyCollection<Type> HlslMappedVectorTypes { get; } = new HashSet<Type>(new[]
        {
            typeof(Bool2), typeof(Bool3), typeof(Bool4),
            typeof(Int2), typeof(Int3), typeof(Int4),
            typeof(UInt2), typeof(UInt3), typeof(UInt4),
            typeof(Float2), typeof(Float3), typeof(Float4),
            typeof(Double2), typeof(Double3), typeof(Double4)
        });

        /// <summary>
        /// Checks whether or not a given type name matches a structured buffer type.
        /// </summary>
        /// <param name="typeName">The input type name to check.</param>
        /// <returns>Whether or not <paramref name="typeName"/> represents a structured buffer type.</returns>
        public static bool IsStructuredBufferType(string typeName)
        {
            return
                typeName == "ComputeSharp.ReadOnlyBuffer`1" ||
                typeName == "ComputeSharp.ReadWriteBuffer`1";
        }

        /// <summary>
        /// Gets the mapped HLSL-compatible type name for the input type name.
        /// </summary>
        /// <param name="name">The input type name to map.</param>
        /// <returns>The HLSL-compatible type name that can be used in an HLSL shader.</returns>
        public static bool TryGetMappedName(string originalName, out string? mappedName)
        {
            return KnownTypes.TryGetValue(originalName, out mappedName);
        }

        /// <summary>
        /// Gets the sequence of unique custom types from a collection of discovered types.
        /// </summary>
        /// <param name="discoveredTypes">The input collection of discovered types.</param>
        /// <returns>The list of unique custom types.</returns>
        public static IEnumerable<INamedTypeSymbol> GetCustomTypes(IEnumerable<INamedTypeSymbol> discoveredTypes)
        {
            // Local function to recursively gather nested types
            static void ExploreTypes(INamedTypeSymbol type, HashSet<INamedTypeSymbol> customTypes)
            {
                if (KnownTypes.ContainsKey(type.GetFullMetadataName())) return;

                customTypes.Add(type);

                foreach (var field in type.GetMembers().OfType<IFieldSymbol>())
                {
                    ExploreTypes((INamedTypeSymbol)field.Type, customTypes);
                }
            }

            HashSet<INamedTypeSymbol> customTypes = new(SymbolEqualityComparer.Default);

            // Explore all input types and their nested types
            foreach (INamedTypeSymbol type in discoveredTypes)
            {
                ExploreTypes(type, customTypes);
            }

            return customTypes;
        }
    }
}
