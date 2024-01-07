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
    /// <param name="isPixelShaderLike">Whether <paramref name="typeSymbol"/> is a "pixel shader like" type.</param>
    /// <returns>Whether <paramref name="typeSymbol"/> is a compute shader type at all.</returns>
    private static bool TryGetIsPixelShaderLike(INamedTypeSymbol typeSymbol, Compilation compilation, out bool isPixelShaderLike)
    {
        INamedTypeSymbol computeShaderSymbol = compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader")!;
        INamedTypeSymbol pixelShaderSymbol = compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader`1")!;

        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(interfaceSymbol, computeShaderSymbol))
            {
                isPixelShaderLike = false;

                return true;
            }
            else if (SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, pixelShaderSymbol))
            {
                isPixelShaderLike = true;

                return true;
            }
        }

        isPixelShaderLike = false;

        return false;
    }
}