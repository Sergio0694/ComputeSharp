using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// Gets the shader type for a given shader, if any.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to check.</param>
    /// <param name="compilation">The <see cref="Compilation"/> instance currently in use.</param>
    /// <returns>Whether or not <paramref name="typeSymbol"/> is a D2D1 interface type.</returns>
    public static bool IsD2D1PixelShaderType(INamedTypeSymbol typeSymbol, Compilation compilation)
    {
        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            if (interfaceSymbol.Name == nameof(ID2D1PixelShader))
            {
                INamedTypeSymbol d2D1PixelShaderSymbol = compilation.GetTypeByMetadataName("ComputeSharp.D2D1.ID2D1PixelShader")!;

                if (SymbolEqualityComparer.Default.Equals(interfaceSymbol, d2D1PixelShaderSymbol))
                {
                    return true;
                }
            }
        }

        return false;
    }
}