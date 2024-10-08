using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <summary>
    /// Gets whether a given type is a compute shader type (ie. implements any of the interfaces).
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to check.</param>
    /// <param name="compilation">The <see cref="Compilation"/> instance currently in use.</param>
    /// <param name="shaderInterfaceType">The (constructed) shader interface type implemented by the shader type.</param>
    /// <param name="isPixelShaderLike">Whether <paramref name="typeSymbol"/> is a "pixel shader like" type.</param>
    /// <returns>Whether <paramref name="typeSymbol"/> is a compute shader type at all.</returns>
    private static bool TryGetIsPixelShaderLike(
        INamedTypeSymbol typeSymbol,
        Compilation compilation,
        [NotNullWhen(true)] out INamedTypeSymbol? shaderInterfaceType,
        out bool isPixelShaderLike)
    {
        INamedTypeSymbol computeShaderSymbol = compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader")!;
        INamedTypeSymbol pixelShaderSymbol = compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader`1")!;

        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(interfaceSymbol, computeShaderSymbol))
            {
                shaderInterfaceType = interfaceSymbol;
                isPixelShaderLike = false;

                return true;
            }
            else if (SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, pixelShaderSymbol))
            {
                shaderInterfaceType = interfaceSymbol;
                isPixelShaderLike = true;

                return true;
            }
        }

        shaderInterfaceType = null;
        isPixelShaderLike = false;

        return false;
    }
}