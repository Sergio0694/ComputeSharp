using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the available numeric properties.
    /// </summary>
    private static partial class NumericProperties
    {
        /// <summary>
        /// Writes the <c>ConstantBufferSize</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteConstantBufferSizeSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(typeof(ID2D1ShaderGenerator));
            writer.WriteLine($"readonly int global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.ConstantBufferSize => {info.ConstantBufferSizeInBytes};");
        }

        /// <summary>
        /// Writes the <c>InputCount</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteInputCountSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(typeof(ID2D1ShaderGenerator));
            writer.WriteLine($"readonly int global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.InputCount => {info.InputTypes.Length};");
        }

        /// <summary>
        /// Writes the <c>ResourceTextureCount</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteResourceTextureCountSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(typeof(ID2D1ShaderGenerator));
            writer.WriteLine($"readonly int global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.ResourceTextureCount => {info.ResourceTextureDescriptions.Length};");
        }
    }
}