using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            [typeof(ThreadIds).FullName] = "uint3"
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
        /// Gets the mapped HLSL-compatible type name for the input type symbol.
        /// </summary>
        /// <param name="typeSymbol">The input type to map.</param>
        /// <returns>The HLSL-compatible type name that can be used in an HLSL shader.</returns>
        [Pure]
        public static string GetMappedName(INamedTypeSymbol typeSymbol)
        {
            // Delegate types just return an empty string, as they're not actually
            // used in the generated shaders, but just mapped to a function at runtime.
            if (typeSymbol.TypeKind == TypeKind.Delegate) return "";

            string typeName = typeSymbol.GetFullMetadataName();

            // Special case for the structured buffer types
            if (IsStructuredBufferType(typeName))
            {
                string genericArgumentName = ((INamedTypeSymbol)typeSymbol.TypeArguments[0]).GetFullMetadataName();

                // If the current type is a custom type, format it as needed
                if (!KnownTypes.TryGetValue(genericArgumentName, out string? mapped))
                {
                    mapped = genericArgumentName.Replace(".", "__");
                }

                // Construct the HLSL type name
                return typeName switch
                {
                    "ComputeSharp.ReadOnlyBuffer`1" => $"StructuredBuffer<{mapped}>",
                    "ComputeSharp.ReadWriteBuffer`1" => $"RWStructuredBuffer<{mapped}>",
                    _ => throw new ArgumentException()
                };
            }

            return KnownTypes[typeName];
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

            return OrderByDependency(customTypes);
        }

        /// <summary>
        /// Orders the input sequence of types so that they can be declared in HLSL successfully.
        /// </summary>
        /// <param name="types">The input collection of types to declare.</param>
        /// <returns>The same list as input, but in a valid HLSL declaration order.</returns>
        [Pure]
        private static IEnumerable<INamedTypeSymbol> OrderByDependency(IEnumerable<INamedTypeSymbol> types)
        {
            Queue<(INamedTypeSymbol Type, HashSet<INamedTypeSymbol> Fields)> queue = new();

            // Build a mapping of type dependencies for all the captured types. A type depends on another
            // when the latter is a field in the first type. HLSL requires custom types to be declared in
            // order of usage, so we need to ensure that types are declared in an order that guarantees
            // that no type will be referenced before being defined. To do so, we can create a mapping of
            // types and their dependencies, and iteratively remove items from the map when they have no
            // dependencies left. When one type is processed and removed, it is also removed from the list
            // of dependencies of all other remaining types in the map, until there is none left.
            foreach (var type in types)
            {
                HashSet<INamedTypeSymbol> dependencies = new(SymbolEqualityComparer.Default);

                // Only add other custom types as dependencies, and ignore HLSL types
                foreach (var field in type.GetMembers().OfType<IFieldSymbol>())
                {
                    INamedTypeSymbol fieldType = (INamedTypeSymbol)field.Type;

                    if (!KnownTypes.ContainsKey(fieldType.GetFullMetadataName()))
                    {
                        _ = dependencies.Add(fieldType);
                    }
                }

                queue.Enqueue((type, dependencies));
            }

            while (queue.Count > 0)
            {
                var entry = queue.Dequeue();

                // No dependencies left, we can declare this type
                if (entry.Fields.Count == 0)
                {
                    // Remove the current type from dependencies of others
                    foreach (var pair in queue)
                    {
                        _ = pair.Fields.Remove(entry.Type);
                    }

                    yield return entry.Type;
                }
                else queue.Enqueue(entry);
            }
        }
    }
}
