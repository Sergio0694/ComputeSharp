using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

#pragma warning disable IDE0055, RS1024

namespace ComputeSharp.SourceGeneration.Mappings;

/// <summary>
/// A <see langword="class"/> that contains and maps known HLSL type names to common .NET types.
/// </summary>
internal static partial class HlslKnownTypes
{
    /// <summary>
    /// The set of HLSL vector types.
    /// </summary>
    private static readonly Type[] KnownVectorTypes =
    [
        typeof(Bool2), typeof(Bool3), typeof(Bool4),
        typeof(Int2), typeof(Int3), typeof(Int4),
        typeof(UInt2), typeof(UInt3), typeof(UInt4),
        typeof(Float2), typeof(Float3), typeof(Float4),
        typeof(Double2), typeof(Double3), typeof(Double4)
    ];

    /// <summary>
    /// The set of HLSL matrix types.
    /// </summary>
    private static readonly Type[] KnownMatrixTypes =
    [
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
    ];

    /// <summary>
    /// The mapping of supported known vector types to HLSL types.
    /// </summary>
    private static readonly Dictionary<string, string> KnownVectorTypeMetadataNames = BuildKnownVectorTypeMetadataNames();

    /// <summary>
    /// The mapping of supported known matrix types to HLSL types.
    /// </summary>
    private static readonly Dictionary<string, string> KnownMatrixTypesMetadataNames = BuildKnownMatrixTypeMetadataNames();

    /// <summary>
    /// The mapping of supported known types to HLSL types.
    /// </summary>
    private static readonly Dictionary<string, string> KnownHlslTypeMetadataNames = BuildKnownHlslTypeMetadataNames();

    /// <summary>
    /// The mapping of type info for all supported known matrix types to HLSL types.
    /// </summary>
    private static readonly Dictionary<string, NonLinearMatrixTypeInfo> KnownNonLinearMatrixTypeInfo = BuildKnownNonLinearMatrixTypeInfo();

    /// <summary>
    /// Builds the mapping of known HLSL vector types.
    /// </summary>
    /// <returns>The mapping of known HLSL vector types.</returns>
    private static Dictionary<string, string> BuildKnownVectorTypeMetadataNames()
    {
        return KnownVectorTypes.ToDictionary(
            keySelector: static type => type.FullName,
            elementSelector: static type => type.Name.ToLowerInvariant());
    }

    /// <summary>
    /// Builds the mapping of known HLSL matrix types.
    /// </summary>
    /// <returns>The mapping of known HLSL matrix types.</returns>
    private static Dictionary<string, string> BuildKnownMatrixTypeMetadataNames()
    {
        return KnownMatrixTypes.ToDictionary(
            keySelector: static type => type.FullName,
            elementSelector: static type => type.Name.ToLowerInvariant());
    }

    /// <summary>
    /// Builds the mapping of known primitive types.
    /// </summary>
    /// <returns>The mapping of known primitive types.</returns>
    private static Dictionary<string, string> BuildKnownHlslTypeMetadataNames()
    {
        Dictionary<string, string> knownTypes = new()
        {
            [typeof(bool).FullName] = "bool",
            [typeof(Bool).FullName] = "bool",
            [typeof(int).FullName] = "int",
            [typeof(uint).FullName] = "uint",
            [typeof(float).FullName] = "float",
            [typeof(double).FullName] = "double"
        };

        IEnumerable<KeyValuePair<string, string>> metadataNames =
            knownTypes
            .Concat(KnownVectorTypeMetadataNames)
            .Concat(KnownMatrixTypesMetadataNames);

        return metadataNames.ToDictionary(
            keySelector: static p => p.Key,
            elementSelector: static p => p.Value);
    }

    /// <summary>
    /// Builds the mapping of type info for all supported known matrix types to HLSL types.
    /// </summary>
    /// <returns>The mapping of type info for all supported known matrix types to HLSL types.</returns>
    private static Dictionary<string, NonLinearMatrixTypeInfo> BuildKnownNonLinearMatrixTypeInfo()
    {
        Type[] knownTypes =
        [
            typeof(Bool2x1), typeof(Bool2x2), typeof(Bool2x3), typeof(Bool2x4),
            typeof(Bool3x1), typeof(Bool3x2), typeof(Bool3x3), typeof(Bool3x4),
            typeof(Bool4x1), typeof(Bool4x2), typeof(Bool4x3), typeof(Bool4x4),
            typeof(Int2x1), typeof(Int2x2), typeof(Int2x3), typeof(Int2x4),
            typeof(Int3x1), typeof(Int3x2), typeof(Int3x3), typeof(Int3x4),
            typeof(Int4x1), typeof(Int4x2), typeof(Int4x3), typeof(Int4x4),
            typeof(UInt2x1), typeof(UInt2x2), typeof(UInt2x3), typeof(UInt2x4),
            typeof(UInt3x1), typeof(UInt3x2), typeof(UInt3x3), typeof(UInt3x4),
            typeof(UInt4x1), typeof(UInt4x2), typeof(UInt4x3), typeof(UInt4x4),
            typeof(Float2x1), typeof(Float2x2), typeof(Float2x3), typeof(Float2x4),
            typeof(Float3x1), typeof(Float3x2), typeof(Float3x3), typeof(Float3x4),
            typeof(Float4x1), typeof(Float4x2), typeof(Float4x3), typeof(Float4x4),
            typeof(Double2x1), typeof(Double2x2), typeof(Double2x3), typeof(Double2x4),
            typeof(Double3x1), typeof(Double3x2), typeof(Double3x3), typeof(Double3x4),
            typeof(Double4x1), typeof(Double4x2), typeof(Double4x3), typeof(Double4x4)
        ];

        static NonLinearMatrixTypeInfo CreateTypeInfo(Type type)
        {
            // Extract the info for each non linear matrix type:
            //   - The name is just the prefix without the NxM part
            //   - The number of rows is the first suffix character
            //   - The number of columns is the last suffix character
            return new(
                ElementName: type.Name[..^3],
                Rows: int.Parse(type.Name[^3].ToString(), CultureInfo.InvariantCulture),
                Columns: int.Parse(type.Name[^1].ToString(), CultureInfo.InvariantCulture));
        }

        return knownTypes.ToDictionary(
            keySelector: static type => type.FullName,
            elementSelector: CreateTypeInfo);
    }

    /// <summary>
    /// Enumerates the known HLSL vector types.
    /// </summary>
    /// <returns>The sequence of all known HLSL vector types.</returns>
    public static IEnumerable<Type> EnumerateKnownVectorTypes()
    {
        return KnownVectorTypes;
    }

    /// <summary>
    /// Enumerates the known HLSL matrix types.
    /// </summary>
    /// <returns>The sequence of all known HLSL matrix types.</returns>
    public static IEnumerable<Type> EnumerateKnownMatrixTypes()
    {
        return KnownMatrixTypes;
    }

    /// <summary>
    /// Checks whether or not a given type name matches a known HLSL primitive type (scalar, vector or matrix).
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a scalar, vector or matrix type.</returns>
    public static bool IsKnownHlslType(string typeName)
    {
        return KnownHlslTypeMetadataNames.ContainsKey(typeName);
    }

    /// <summary>
    /// Checks whether or not a given type name matches a vector type.
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a vector type.</returns>
    public static bool IsVectorType(string typeName)
    {
        return KnownVectorTypeMetadataNames.ContainsKey(typeName);
    }

    /// <summary>
    /// Checks whether or not a given type name matches a matrix type.
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a matrix type.</returns>
    public static bool IsMatrixType(string typeName)
    {
        return KnownMatrixTypesMetadataNames.ContainsKey(typeName);
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
        if (KnownNonLinearMatrixTypeInfo.TryGetValue(typeName, out NonLinearMatrixTypeInfo? value))
        {
            (elementName, rows, columns) = value;

            return true;
        }

        elementName = null;
        rows = 0;
        columns = 0;

        return false;
    }

    /// <summary>
    /// Gets the mapped HLSL-compatible type name for the input type symbol.
    /// </summary>
    /// <param name="typeSymbol">The input type to map.</param>
    /// <returns>The HLSL-compatible type name that can be used in an HLSL shader.</returns>
    public static partial string GetMappedName(INamedTypeSymbol typeSymbol);

    /// <summary>
    /// Gets the mapped HLSL-compatible type name for the input element type symbol.
    /// </summary>
    /// <param name="typeSymbol">The input type to map.</param>
    /// <returns>The HLSL-compatible type name that can be used in an HLSL shader.</returns>
    public static string GetMappedElementName(IArrayTypeSymbol typeSymbol)
    {
        string elementTypeName = ((INamedTypeSymbol)typeSymbol.ElementType).GetFullyQualifiedMetadataName();

        if (KnownHlslTypeMetadataNames.TryGetValue(elementTypeName, out string? mapped))
        {
            return mapped;
        }

        return elementTypeName.ToHlslIdentifierName();
    }

    /// <summary>
    /// Gets the mapped HLSL-compatible type name for the input type name.
    /// </summary>
    /// <param name="originalName">The input type name to map.</param>
    /// <returns>The HLSL-compatible type name that can be used in an HLSL shader.</returns>
    public static string GetMappedName(string originalName)
    {
        return KnownHlslTypeMetadataNames[originalName];
    }

    /// <summary>
    /// Tries to get the mapped HLSL-compatible type name for the input type name.
    /// </summary>
    /// <param name="originalName">The input type name to map.</param>
    /// <param name="mappedName">The resulting mapped type name, if found.</param>
    /// <returns>Whether a mapped name was available.</returns>
    public static bool TryGetMappedName(string originalName, [NotNullWhen(true)] out string? mappedName)
    {
        return KnownHlslTypeMetadataNames.TryGetValue(originalName, out mappedName);
    }

    /// <summary>
    /// Gets the sequence of unique custom types from a collection of discovered types.
    /// </summary>
    /// <param name="discoveredTypes">The input collection of discovered types.</param>
    /// <param name="invalidTypes">The collection of discovered invalid types, if any.</param>
    /// <returns>The list of unique custom types.</returns>
    public static IEnumerable<INamedTypeSymbol> GetCustomTypes(IEnumerable<INamedTypeSymbol> discoveredTypes, out IReadOnlyCollection<INamedTypeSymbol> invalidTypes)
    {
        // Local function to recursively gather nested types
        static void ExploreTypes(INamedTypeSymbol type, HashSet<INamedTypeSymbol> customTypes, HashSet<INamedTypeSymbol> invalidTypes)
        {
            // Explicitly prevent bool from being a field in a custom struct
            if (type.SpecialType == SpecialType.System_Boolean)
            {
                _ = invalidTypes.Add(type);

                return;
            }

            if (KnownHlslTypeMetadataNames.ContainsKey(type.GetFullyQualifiedMetadataName()))
            {
                return;
            }

            // Check if the type is unsupported
            if (!type.IsUnmanagedType ||
                type.TypeKind is TypeKind.Enum ||
                type.IsGenericType ||
                type.IsRefLikeType ||
                type.GetFullyQualifiedName().StartsWith("System.", StringComparison.InvariantCulture))
            {
                _ = invalidTypes.Add(type);

                return;
            }

            if (!customTypes.Add(type))
            {
                return;
            }

            foreach (IFieldSymbol field in type.GetMembers().OfType<IFieldSymbol>())
            {
                if (field.IsStatic)
                {
                    continue;
                }

                ExploreTypes((INamedTypeSymbol)field.Type, customTypes, invalidTypes);
            }
        }

        HashSet<INamedTypeSymbol> customTypes = new(SymbolEqualityComparer.Default);
        HashSet<INamedTypeSymbol> invalidTypes2 = new(SymbolEqualityComparer.Default);

        // Explore all input types and their nested types
        foreach (INamedTypeSymbol type in discoveredTypes)
        {
            // Special case for bool types. These types are blocked if they appear as fields in custom struct types,
            // but are otherwise allowed. For instance, it is fine to use them in captured values for a shader (as
            // the dispatch data loader will perform the correct marshalling) as well as in locals/parameters. This
            // branch prevents crawling a processed type if it's just bool at the top level (ie. not a custom struct).
            if (type.SpecialType == SpecialType.System_Boolean)
            {
                continue;
            }

            ExploreTypes(type, customTypes, invalidTypes2);
        }

        invalidTypes = invalidTypes2;

        return OrderByDependency(customTypes, invalidTypes2);
    }

    /// <summary>
    /// Orders the input sequence of types so that they can be declared in HLSL successfully.
    /// </summary>
    /// <param name="types">The input collection of types to declare.</param>
    /// <param name="invalidTypes">The collection of discovered invalid types, if any.</param>
    /// <returns>The same list as input, but in a valid HLSL declaration order.</returns>
    private static IEnumerable<INamedTypeSymbol> OrderByDependency(IEnumerable<INamedTypeSymbol> types, IReadOnlyCollection<INamedTypeSymbol> invalidTypes)
    {
        Queue<(INamedTypeSymbol Type, HashSet<INamedTypeSymbol> Fields)> queue = [];

        // Build a mapping of type dependencies for all the captured types. A type depends on another
        // when the latter is a field in the first type. HLSL requires custom types to be declared in
        // order of usage, so we need to ensure that types are declared in an order that guarantees
        // that no type will be referenced before being defined. To do so, we can create a mapping of
        // types and their dependencies, and iteratively remove items from the map when they have no
        // dependencies left. When one type is processed and removed, it is also removed from the list
        // of dependencies of all other remaining types in the map, until there is none left.
        foreach (INamedTypeSymbol type in types)
        {
            HashSet<INamedTypeSymbol> dependencies = new(SymbolEqualityComparer.Default);

            // Only add other custom types as dependencies, and ignore HLSL types
            foreach (IFieldSymbol field in type.GetMembers().OfType<IFieldSymbol>())
            {
                if (field.IsStatic)
                {
                    continue;
                }

                INamedTypeSymbol fieldType = (INamedTypeSymbol)field.Type;

                if (!KnownHlslTypeMetadataNames.ContainsKey(fieldType.GetFullyQualifiedMetadataName()) &&
                    !invalidTypes.Contains(fieldType))
                {
                    _ = dependencies.Add(fieldType);
                }
            }

            queue.Enqueue((type, dependencies));
        }

        while (queue.Count > 0)
        {
            (INamedTypeSymbol Type, HashSet<INamedTypeSymbol> Fields) entry = queue.Dequeue();

            // No dependencies left, we can declare this type
            if (entry.Fields.Count == 0)
            {
                // Remove the current type from dependencies of others
                foreach ((INamedTypeSymbol Type, HashSet<INamedTypeSymbol> Fields) pair in queue)
                {
                    _ = pair.Fields.Remove(entry.Type);
                }

                yield return entry.Type;
            }
            else
            {
                queue.Enqueue(entry);
            }
        }
    }

    /// <summary>
    /// A simple model with info on a non linear matrix type.
    /// </summary>
    /// <param name="ElementName">The element name of the matrix type.</param>
    /// <param name="Rows">The number of rows in the matrix type.</param>
    /// <param name="Columns">The number of columns in the matrix type.</param>
    private sealed record NonLinearMatrixTypeInfo(string ElementName, int Rows, int Columns);
}