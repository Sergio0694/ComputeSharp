using System;
using System.Collections.Generic;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownTypes
{
    /// <summary>
    /// Gets the known HLSL dispatch types.
    /// </summary>
    public static IReadOnlyCollection<Type> HlslDispatchTypes { get; } =
    [
        typeof(ThreadIds),
        typeof(GroupIds),
        typeof(GridIds)
    ];

    /// <summary>
    /// Checks whether or not a given type name matches a constant buffer type.
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a constant buffer type.</returns>
    public static bool IsConstantBufferType(string typeName)
    {
        return typeName == "ComputeSharp.ConstantBuffer`1";
    }

    /// <summary>
    /// Checks whether or not a given type name matches a read write buffer type.
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a read write buffer type.</returns>
    public static bool IsReadWriteBufferType(string typeName)
    {
        return typeName == "ComputeSharp.ReadWriteBuffer`1";
    }

    /// <summary>
    /// Checks whether or not a given type name matches a structured buffer type.
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a structured buffer type.</returns>
    public static bool IsStructuredBufferType(string typeName)
    {
        return typeName switch
        {
            "ComputeSharp.ConstantBuffer`1" or
            "ComputeSharp.ReadOnlyBuffer`1" or
            "ComputeSharp.ReadWriteBuffer`1" => true,
            _ => false
        };
    }

    /// <summary>
    /// Checks whether or not a given type name matches a readonly typed resource type.
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a readonly typed resource type.</returns>
    public static bool IsReadOnlyTypedResourceType(string typeName)
    {
        return typeName switch
        {
            "ComputeSharp.ReadOnlyBuffer`1" or
            "ComputeSharp.ReadOnlyTexture1D`1" or
            "ComputeSharp.ReadOnlyTexture1D`2" or
            "ComputeSharp.ReadOnlyTexture2D`1" or
            "ComputeSharp.ReadOnlyTexture2D`2" or
            "ComputeSharp.ReadOnlyTexture3D`1" or
            "ComputeSharp.ReadOnlyTexture3D`2" or
            "ComputeSharp.IReadOnlyTexture1D`1" or
            "ComputeSharp.IReadOnlyTexture2D`1" or
            "ComputeSharp.IReadOnlyTexture3D`1" => true,
            "ComputeSharp.IReadOnlyNormalizedTexture1D`1" or
            "ComputeSharp.IReadOnlyNormalizedTexture2D`1" or
            "ComputeSharp.IReadOnlyNormalizedTexture3D`1" => true,
            _ => false
        };
    }

    /// <summary>
    /// Checks whether or not a given type name matches a writeable typed resource type.
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a writeable typed resource type.</returns>
    public static bool IsReadWriteTypedResourceType(string typeName)
    {
        return typeName switch
        {
            "ComputeSharp.ReadWriteBuffer`1" or
            "ComputeSharp.ReadWriteTexture1D`1" or
            "ComputeSharp.ReadWriteTexture1D`2" or
            "ComputeSharp.ReadWriteTexture2D`1" or
            "ComputeSharp.ReadWriteTexture2D`2" or
            "ComputeSharp.ReadWriteTexture3D`1" or
            "ComputeSharp.ReadWriteTexture3D`2" or
            "ComputeSharp.IReadWriteNormalizedTexture1D`1" or
            "ComputeSharp.IReadWriteNormalizedTexture2D`1" or
            "ComputeSharp.IReadWriteNormalizedTexture3D`1" => true,
            _ => false
        };
    }

    /// <summary>
    /// Checks whether or not a given type name matches a typed resource type.
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a typed resource type.</returns>
    public static bool IsTypedResourceType(string typeName)
    {
        return typeName switch
        {
            "ComputeSharp.ConstantBuffer`1" or
            "ComputeSharp.ReadOnlyBuffer`1" or
            "ComputeSharp.ReadWriteBuffer`1" or
            "ComputeSharp.ReadOnlyTexture1D`1" or
            "ComputeSharp.ReadOnlyTexture1D`2" or
            "ComputeSharp.ReadOnlyTexture2D`1" or
            "ComputeSharp.ReadOnlyTexture2D`2" or
            "ComputeSharp.ReadWriteTexture1D`1" or
            "ComputeSharp.ReadWriteTexture1D`2" or
            "ComputeSharp.ReadWriteTexture2D`1" or
            "ComputeSharp.ReadWriteTexture2D`2" or
            "ComputeSharp.ReadOnlyTexture3D`1" or
            "ComputeSharp.ReadOnlyTexture3D`2" or
            "ComputeSharp.ReadWriteTexture3D`1" or
            "ComputeSharp.ReadWriteTexture3D`2" or
            "ComputeSharp.IReadOnlyTexture1D`1" or
            "ComputeSharp.IReadOnlyTexture2D`1" or
            "ComputeSharp.IReadOnlyTexture3D`1" or
            "ComputeSharp.IReadOnlyNormalizedTexture1D`1" or
            "ComputeSharp.IReadWriteNormalizedTexture1D`1" or
            "ComputeSharp.IReadOnlyNormalizedTexture2D`1" or
            "ComputeSharp.IReadWriteNormalizedTexture2D`1" or
            "ComputeSharp.IReadOnlyNormalizedTexture3D`1" or
            "ComputeSharp.IReadWriteNormalizedTexture3D`1" => true,
            _ => false
        };
    }

    /// <inheritdoc/>
    public static partial string GetMappedName(INamedTypeSymbol typeSymbol)
    {
        // Delegate types just return an empty string, as they're not actually
        // used in the generated shaders, but just mapped to a function at runtime.
        if (typeSymbol.TypeKind == TypeKind.Delegate)
        {
            return "";
        }

        string typeName = typeSymbol.GetFullyQualifiedMetadataName();

        // Special case for the resource types
        if (IsTypedResourceType(typeName))
        {
            string genericArgumentName = ((INamedTypeSymbol)typeSymbol.TypeArguments.Last()).GetFullyQualifiedMetadataName();

            // If the current type is a custom type, format it as needed
            if (!KnownHlslTypeMetadataNames.TryGetValue(genericArgumentName, out string? mappedElementType))
            {
                mappedElementType = genericArgumentName.ToHlslIdentifierName();
            }

            // Construct the HLSL type name
            return typeName switch
            {
                "ComputeSharp.ConstantBuffer`1" => mappedElementType,
                "ComputeSharp.ReadOnlyBuffer`1" => $"StructuredBuffer<{mappedElementType}>",
                "ComputeSharp.ReadWriteBuffer`1" => $"RWStructuredBuffer<{mappedElementType}>",
                "ComputeSharp.ReadOnlyTexture1D`1" => $"Texture1D<{mappedElementType}>",
                "ComputeSharp.ReadOnlyTexture1D`2" => $"Texture1D<unorm {mappedElementType}>",
                "ComputeSharp.ReadWriteTexture1D`1" => $"RWTexture1D<{mappedElementType}>",
                "ComputeSharp.ReadWriteTexture1D`2" => $"RWTexture1D<unorm {mappedElementType}>",
                "ComputeSharp.ReadOnlyTexture2D`1" => $"Texture2D<{mappedElementType}>",
                "ComputeSharp.ReadOnlyTexture2D`2" => $"Texture2D<unorm {mappedElementType}>",
                "ComputeSharp.ReadWriteTexture2D`1" => $"RWTexture2D<{mappedElementType}>",
                "ComputeSharp.ReadWriteTexture2D`2" => $"RWTexture2D<unorm {mappedElementType}>",
                "ComputeSharp.ReadOnlyTexture3D`1" => $"Texture3D<{mappedElementType}>",
                "ComputeSharp.ReadOnlyTexture3D`2" => $"Texture3D<unorm {mappedElementType}>",
                "ComputeSharp.ReadWriteTexture3D`1" => $"RWTexture3D<{mappedElementType}>",
                "ComputeSharp.ReadWriteTexture3D`2" => $"RWTexture3D<unorm {mappedElementType}>",
                "ComputeSharp.IReadOnlyTexture1D`1" => $"Texture1D<{mappedElementType}>",
                "ComputeSharp.IReadOnlyTexture2D`1" => $"Texture2D<{mappedElementType}>",
                "ComputeSharp.IReadOnlyTexture3D`1" => $"Texture3D<{mappedElementType}>",
                "ComputeSharp.IReadOnlyNormalizedTexture1D`1" => $"Texture1D<unorm {mappedElementType}>",
                "ComputeSharp.IReadWriteNormalizedTexture1D`1" => $"RWTexture1D<unorm {mappedElementType}>",
                "ComputeSharp.IReadOnlyNormalizedTexture2D`1" => $"Texture2D<unorm {mappedElementType}>",
                "ComputeSharp.IReadWriteNormalizedTexture2D`1" => $"RWTexture2D<unorm {mappedElementType}>",
                "ComputeSharp.IReadOnlyNormalizedTexture3D`1" => $"Texture3D<unorm {mappedElementType}>",
                "ComputeSharp.IReadWriteNormalizedTexture3D`1" => $"RWTexture3D<unorm {mappedElementType}>",
                _ => throw new ArgumentException()
            };
        }

        // The captured field is of an HLSL primitive type
        if (KnownHlslTypeMetadataNames.TryGetValue(typeName, out string? mappedType))
        {
            return mappedType;
        }

        // The captured field is of a custom struct type
        return typeName.ToHlslIdentifierName();
    }

    /// <summary>
    /// Gets the mapped HLSL-compatible type name for the output texture of a pixel shader.
    /// </summary>
    /// <param name="typeSymbol">The pixel shader type to map.</param>
    /// <returns>The HLSL-compatible type name that can be used in an HLSL shader.</returns>
    public static string GetMappedNameForPixelShaderType(INamedTypeSymbol typeSymbol)
    {
        string genericArgumentName = ((INamedTypeSymbol)typeSymbol.TypeArguments.First()).GetFullyQualifiedMetadataName();

        // If the current type is a custom type, format it as needed
        if (!KnownHlslTypeMetadataNames.TryGetValue(genericArgumentName, out string? mappedElementType))
        {
            mappedElementType = genericArgumentName.ToHlslIdentifierName();
        }

        // Construct the HLSL type name
        return $"RWTexture2D<unorm {mappedElementType}>";
    }
}