using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
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
        /// Gets the known HLSL dispatch types.
        /// </summary>
        public static IReadOnlyCollection<Type> HlslDispatchTypes { get; } = new[]
        {
            typeof(ThreadIds),
            typeof(GroupIds),
            typeof(GridIds)
        };

        /// <summary>
        /// Gets the set of HLSL vector types.
        /// </summary>
        public static IReadOnlyCollection<Type> KnownVectorTypes { get; } = new[]
        {
            typeof(Bool2), typeof(Bool3), typeof(Bool4),
            typeof(Int2), typeof(Int3), typeof(Int4),
            typeof(UInt2), typeof(UInt3), typeof(UInt4),
            typeof(Float2), typeof(Float3), typeof(Float4),
            typeof(Double2), typeof(Double3), typeof(Double4)
        };

        /// <summary>
        /// Gets the set of HLSL matrix types.
        /// </summary>
        public static IReadOnlyCollection<Type> KnownMatrixTypes { get; } = new[]
        {
            typeof(Bool1x1), typeof(Bool1x2), typeof(Bool1x3), typeof(Bool1x4),
            typeof(Bool2x1), typeof(Bool2x2), typeof(Bool2x3), typeof(Bool2x4),
            typeof(Bool3x1), typeof(Bool3x2), typeof(Bool3x3), typeof(Bool3x4),
            typeof(Bool4x1), typeof(Bool4x2), typeof(Bool4x3), typeof(Bool4x4),
            typeof(Int1x1), typeof(Int1x2), typeof(Int1x3), typeof(Int1x4),
            typeof(Int2x1), typeof(Int2x2), typeof(Int2x3), typeof(Int2x4),
            typeof(Int3x1), typeof(Int3x2), typeof(Int3x3), typeof(Int3x4),
            typeof(Int4x1), typeof(Int4x2), typeof(Int4x3), typeof(Int4x4),
            typeof(UInt1x1), typeof(UInt1x2), typeof(UInt1x3), typeof(UInt1x4),
            typeof(UInt2x1), typeof(UInt2x2), typeof(UInt2x3), typeof(UInt2x4),
            typeof(UInt3x1), typeof(UInt3x2), typeof(UInt3x3), typeof(UInt3x4),
            typeof(UInt4x1), typeof(UInt4x2), typeof(UInt4x3), typeof(UInt4x4),
            typeof(Float1x1), typeof(Float1x2), typeof(Float1x3), typeof(Float1x4),
            typeof(Float2x1), typeof(Float2x2), typeof(Float2x3), typeof(Float2x4),
            typeof(Float3x1), typeof(Float3x2), typeof(Float3x3), typeof(Float3x4),
            typeof(Float4x1), typeof(Float4x2), typeof(Float4x3), typeof(Float4x4),
            typeof(Double1x1), typeof(Double1x2), typeof(Double1x3), typeof(Double1x4),
            typeof(Double2x1), typeof(Double2x2), typeof(Double2x3), typeof(Double2x4),
            typeof(Double3x1), typeof(Double3x2), typeof(Double3x3), typeof(Double3x4),
            typeof(Double4x1), typeof(Double4x2), typeof(Double4x3), typeof(Double4x4)
        };

        /// <summary>
        /// The mapping of supported known types to HLSL types.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> KnownHlslTypes = BuildKnownHlslTypes();

        /// <summary>
        /// Builds the mapping of known primitive types.
        /// </summary>
        [Pure]
        private static IReadOnlyDictionary<string, string> BuildKnownHlslTypes()
        {
            Dictionary<string, string> knownTypes = new()
            {
                [typeof(bool).FullName] = "bool",
                [typeof(int).FullName] = "int",
                [typeof(uint).FullName] = "uint",
                [typeof(float).FullName] = "float",
                [typeof(Vector2).FullName] = "float2",
                [typeof(Vector3).FullName] = "float3",
                [typeof(Vector4).FullName] = "float4",
                [typeof(double).FullName] = "double"
            };

            // Add all the vector types
            foreach (var type in KnownVectorTypes)
            {
                knownTypes.Add(type.FullName, type.Name.ToLower());
            }

            // Add all the matrix types
            foreach (var type in KnownMatrixTypes)
            {
                knownTypes.Add(type.FullName, type.Name.ToLower());
            }

            return knownTypes;
        }

        /// <summary>
        /// Checks whether or not a given type name matches a structured buffer type.
        /// </summary>
        /// <param name="typeName">The input type name to check.</param>
        /// <returns>Whether or not <paramref name="typeName"/> represents a structured buffer type.</returns>
        public static bool IsStructuredBufferType(string typeName)
        {
            switch (typeName)
            {
                case "ComputeSharp.ConstantBuffer`1":
                case "ComputeSharp.ReadOnlyBuffer`1":
                case "ComputeSharp.ReadWriteBuffer`1":
                    return true;
                default: return false;
            };
        }

        /// <summary>
        /// Checks whether or not a given type name matches a typed resource type.
        /// </summary>
        /// <param name="typeName">The input type name to check.</param>
        /// <returns>Whether or not <paramref name="typeName"/> represents a typed resource type.</returns>
        public static bool IsTypedResourceType(string typeName)
        {
            switch (typeName)
            {
                case "ComputeSharp.ConstantBuffer`1":
                case "ComputeSharp.ReadOnlyBuffer`1":
                case "ComputeSharp.ReadWriteBuffer`1":
                case "ComputeSharp.ReadOnlyTexture2D`1":
                case "ComputeSharp.ReadOnlyTexture2D`2":
                case "ComputeSharp.ReadWriteTexture2D`1":
                case "ComputeSharp.ReadWriteTexture2D`2":
                case "ComputeSharp.ReadOnlyTexture3D`1":
                case "ComputeSharp.ReadOnlyTexture3D`2":
                case "ComputeSharp.ReadWriteTexture3D`1":
                case "ComputeSharp.ReadWriteTexture3D`2":
                case "ComputeSharp.IReadOnlyTexture2D`1":
                case "ComputeSharp.IReadWriteTexture2D`1":
                case "ComputeSharp.IReadOnlyTexture3D`1":
                case "ComputeSharp.IReadWriteTexture3D`1":
                    return true;
                default: return false;
            };
        }

        /// <summary>
        /// Checks whether or not a given type name matches a known HLSL primitive type (scalar, vector or matrix).
        /// </summary>
        /// <param name="typeName">The input type name to check.</param>
        /// <returns>Whether or not <paramref name="typeName"/> represents a scalar, vector or matrix type.</returns>
        public static bool IsKnownHlslType(string typeName)
        {
            return KnownHlslTypes.ContainsKey(typeName);
        }

        /// <summary>
        /// Checks whether or not a given type name matches a vector type.
        /// </summary>
        /// <param name="typeName">The input type name to check.</param>
        /// <returns>Whether or not <paramref name="typeName"/> represents a vector type.</returns>
        public static bool IsVectorType(string typeName)
        {
            return KnownVectorTypes.Any(type => type.FullName == typeName);
        }

        /// <summary>
        /// Checks whether or not a given type name matches a matrix type.
        /// </summary>
        /// <param name="typeName">The input type name to check.</param>
        /// <returns>Whether or not <paramref name="typeName"/> represents a matrix type.</returns>
        public static bool IsMatrixType(string typeName)
        {
            return KnownMatrixTypes.Any(type => type.FullName == typeName);
        }

        /// <summary>
        /// Checks whether or not a given type name is a non linear matrix type.
        /// That is, a matrix type with more than a single row (which affects the constant buffer alignment).
        /// </summary>
        /// <param name="typeName">The input type name to check.</param>
        /// <param name="elementName">The element name of the matrix type.</param>
        /// <param name="rows">The number of rows in the matrix type.</param>
        /// <param name="columns">The number of columns in the matrix type.</param>
        /// <returns>Whether or not <paramref name="typeName"/> represents a non linear matrix type.</returns>
        public static bool IsNonLinearMatrixType(string typeName, out string? elementName, out int rows, out int columns)
        {
            if (KnownMatrixTypes.Any(type => type.FullName == typeName))
            {
                var match = Regex.Match(typeName, @"^ComputeSharp\.(Bool|Int|UInt|Float|Double)([2-4])x([1-4])$");

                if (match.Success)
                {
                    elementName = match.Groups[1].Value;
                    rows = int.Parse(match.Groups[2].Value);
                    columns = int.Parse(match.Groups[3].Value);

                    return true;
                }
            }

            elementName = null;
            rows = columns = 0;

            return false;
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

            // Special case for the resource types
            if (IsTypedResourceType(typeName))
            {
                string genericArgumentName = ((INamedTypeSymbol)typeSymbol.TypeArguments.Last()).GetFullMetadataName();

                // If the current type is a custom type, format it as needed
                if (!KnownHlslTypes.TryGetValue(genericArgumentName, out string? mappedElementType))
                {
                    mappedElementType = genericArgumentName.ToHlslIdentifierName();
                }

                // Construct the HLSL type name
                return typeName switch
                {
                    "ComputeSharp.ConstantBuffer`1" => mappedElementType,
                    "ComputeSharp.ReadOnlyBuffer`1" => $"StructuredBuffer<{mappedElementType}>",
                    "ComputeSharp.ReadWriteBuffer`1" => $"RWStructuredBuffer<{mappedElementType}>",
                    "ComputeSharp.ReadOnlyTexture2D`1" => $"Texture2D<{mappedElementType}>",
                    "ComputeSharp.ReadOnlyTexture2D`2" => $"Texture2D<unorm {mappedElementType}>",
                    "ComputeSharp.ReadWriteTexture2D`1" => $"RWTexture2D<{mappedElementType}>",
                    "ComputeSharp.ReadWriteTexture2D`2" => $"RWTexture2D<unorm {mappedElementType}>",
                    "ComputeSharp.ReadOnlyTexture3D`1" => $"Texture3D<{mappedElementType}>",
                    "ComputeSharp.ReadOnlyTexture3D`2" => $"Texture3D<unorm {mappedElementType}>",
                    "ComputeSharp.ReadWriteTexture3D`1" => $"RWTexture3D<{mappedElementType}>",
                    "ComputeSharp.ReadWriteTexture3D`2" => $"RWTexture3D<unorm {mappedElementType}>",
                    "ComputeSharp.IReadOnlyTexture2D`1" => $"Texture2D<unorm {mappedElementType}>",
                    "ComputeSharp.IReadWriteTexture2D`1" => $"RWTexture2D<unorm {mappedElementType}>",
                    "ComputeSharp.IReadOnlyTexture3D`1" => $"Texture3D<unorm {mappedElementType}>",
                    "ComputeSharp.IReadWriteTexture3D`1" => $"RWTexture3D<unorm {mappedElementType}>",
                    _ => throw new ArgumentException()
                };
            }

            // The captured field is of an HLSL primitive type
            if (KnownHlslTypes.TryGetValue(typeName, out string? mappedType))
            {
                return mappedType;
            }

            // The captured field is of a custom struct type
            return typeName.ToHlslIdentifierName();
        }

        /// <summary>
        /// Gets the mapped HLSL-compatible type name for the input element type symbol.
        /// </summary>
        /// <param name="typeSymbol">The input type to map.</param>
        /// <returns>The HLSL-compatible type name that can be used in an HLSL shader.</returns>
        [Pure]
        public static string GetMappedElementName(IArrayTypeSymbol typeSymbol)
        {
            string elementTypeName = ((INamedTypeSymbol)typeSymbol.ElementType).GetFullMetadataName();

            if (KnownHlslTypes.TryGetValue(elementTypeName, out string? mapped))
            {
                return mapped;
            }

            return elementTypeName.ToHlslIdentifierName();
        }

        /// <summary>
        /// Gets the mapped HLSL-compatible type name for the input type name.
        /// </summary>
        /// <param name="name">The input type name to map.</param>
        /// <returns>The HLSL-compatible type name that can be used in an HLSL shader.</returns>
        public static bool TryGetMappedName(string originalName, out string? mappedName)
        {
            return KnownHlslTypes.TryGetValue(originalName, out mappedName);
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
                if (KnownHlslTypes.ContainsKey(type.GetFullMetadataName())) return;

                if (!customTypes.Add(type)) return;

                foreach (var field in type.GetMembers().OfType<IFieldSymbol>())
                {
                    if (field.IsStatic) continue;

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
                    if (field.IsStatic) continue;

                    INamedTypeSymbol fieldType = (INamedTypeSymbol)field.Type;

                    if (!KnownHlslTypes.ContainsKey(fieldType.GetFullMetadataName()))
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
