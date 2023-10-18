using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class PixelOptions
    {
        /// <summary>
        /// Writes the <c>PixelOptions</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            // Set the right expression if the pixel options are valid
            string pixelOptionsExpression = (info.PixelOptions is D2D1PixelOptions.None or D2D1PixelOptions.TrivialSampling) switch
            {
                true => $"global::ComputeSharp.D2D1.D2D1PixelOptions.{info.PixelOptions}",
                false => "default"
            };

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"static ComputeSharp.D2D1.D2D1PixelOptions global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.PixelOptions => {pixelOptionsExpression};");
        }
    }
}