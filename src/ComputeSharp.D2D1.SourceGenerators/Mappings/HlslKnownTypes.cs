using System;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownTypes
{
    /// <summary>
    /// Checks whether or not a given type name matches a resource texture type.
    /// </summary>
    /// <param name="typeName">The input type name to check.</param>
    /// <returns>Whether or not <paramref name="typeName"/> represents a readonly typed resource type.</returns>
    public static bool IsResourceTextureType(string typeName)
    {
        return typeName switch
        {
            "ComputeSharp.D2D1.D2D1ResourceTexture1D" or
            "ComputeSharp.D2D1.D2D1ResourceTexture2D" or
            "ComputeSharp.D2D1.D2D1ResourceTexture3D" => true,
            _ => false
        };
    }

    /// <inheritdoc/>
    public static partial string GetMappedName(INamedTypeSymbol typeSymbol)
    {
        string typeName = typeSymbol.GetFullMetadataName();

        // Special case for resource texture types
        if (IsResourceTextureType(typeName))
        {
            return typeName switch
            {
                "ComputeSharp.D2D1.D2D1ResourceTexture1D" => "Texture1D",
                "ComputeSharp.D2D1.D2D1ResourceTexture2D" => "Texture2D",
                "ComputeSharp.D2D1.D2D1ResourceTexture3D" => "Texture3D",
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
}
