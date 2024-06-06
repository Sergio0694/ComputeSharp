using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the HLSL bytecode properties.
    /// </summary>
    internal static partial class HlslBytecode
    {
        /// <summary>
        /// Extracts the requested shader profile for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The requested shader profile to use to compile the shader, if present.</returns>
        public static D2D1ShaderProfile? GetRequestedShaderProfile(INamedTypeSymbol structDeclarationSymbol)
        {
            if (structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DShaderProfileAttribute", out AttributeData? attributeData))
            {
                return (D2D1ShaderProfile)attributeData.ConstructorArguments[0].Value!;
            }

            if (structDeclarationSymbol.ContainingAssembly.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DShaderProfileAttribute", out attributeData))
            {
                return (D2D1ShaderProfile)attributeData.ConstructorArguments[0].Value!;
            }

            return null;
        }

        /// <summary>
        /// Extracts the requested compile options for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The requested compile options to use to compile the shader, if present.</returns>
        public static D2D1CompileOptions? GetRequestedCompileOptions(INamedTypeSymbol structDeclarationSymbol)
        {
            if (structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out AttributeData? attributeData))
            {
                D2D1CompileOptions options = (D2D1CompileOptions)attributeData.ConstructorArguments[0].Value!;

                // PackMatrixRowMajor is always automatically enabled. If by any chance the attribute is requesting
                // column major packing, the analyzer will emit a diagnostic (same as for the assembly-level case).
                return options | D2D1CompileOptions.PackMatrixRowMajor;
            }

            if (structDeclarationSymbol.ContainingAssembly.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out attributeData))
            {
                // No need to validate against PackMatrixColumnMajor as that's checked separately
                return (D2D1CompileOptions)attributeData.ConstructorArguments[0].Value! | D2D1CompileOptions.PackMatrixRowMajor;
            }

            return null;
        }

        /// <summary>
        /// Gets the effective shader profile to use.
        /// </summary>
        /// <param name="shaderProfile">The requested shader profile.</param>
        /// <param name="isCompilationEnabled">Whether compilation should be performed with the input profile.</param>
        /// <returns>The effective shader profile.</returns>
        public static D2D1ShaderProfile GetEffectiveShaderProfile(D2D1ShaderProfile? shaderProfile, out bool isCompilationEnabled)
        {
            // Compilation is only enabled if the user explicitly selected a shader profile
            isCompilationEnabled = shaderProfile is not null;

            // The effective shader profile is either be the requested one, or the default value (which maps to PS5.0)
            return shaderProfile ?? D2D1ShaderProfile.PixelShader50;
        }

        /// <summary>
        /// Gets the effective compile options to use.
        /// </summary>
        /// <param name="compileOptions">The requested compile options.</param>
        /// <returns>The effective compile options.</returns>
        public static D2D1CompileOptions GetEffectiveCompileOptions(D2D1CompileOptions? compileOptions)
        {
            return compileOptions ?? D2D1CompileOptions.Default;
        }
    }
}