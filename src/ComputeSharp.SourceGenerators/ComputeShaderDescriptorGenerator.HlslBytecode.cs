using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>HlslBytecode</c> property.
    /// </summary>
    private static partial class HlslBytecode
    {
        /// <summary>
        /// Extracts the compile options for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The requested compile options to use to compile the shader, if present.</returns>
        public static CompileOptions GetCompileOptions(INamedTypeSymbol structDeclarationSymbol)
        {
            // If a [CompileOptions] annotation is present, return the explicit options
            if (structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.CompileOptionsAttribute", out AttributeData? attributeData) ||
                structDeclarationSymbol.ContainingAssembly.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.CompileOptionsAttribute", out attributeData))
            {
                return (CompileOptions)attributeData.ConstructorArguments[0].Value!;
            }

            return CompileOptions.Default;
        }
    }
}