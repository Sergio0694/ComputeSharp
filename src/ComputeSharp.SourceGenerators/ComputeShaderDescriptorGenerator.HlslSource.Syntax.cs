using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class HlslSource
    {
        /// <summary>
        /// Writes the <c>HlslSource</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"static string global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.HlslSource =>");
            writer.IncreaseIndent();
            writer.WriteLine("\"\"\"");
            writer.Write(info.HlslInfoKey.HlslSource, isMultiline: true);
            writer.WriteLine(skipIfPresent: true);
            writer.WriteLine("\"\"\";");
            writer.DecreaseIndent();
        }
    }
}