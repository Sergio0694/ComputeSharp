using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <inheritdoc/>
    partial class BuildHlslSource
    {
        /// <summary>
        /// Writes the <c>BuildHlslSource</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine("readonly void global::ComputeSharp.__Internals.IShader.BuildHlslSource(out string hlslSource)");

            using (writer.WriteBlock())
            {
                writer.WriteLine("hlslSource =");
                writer.IncreaseIndent();
                writer.WriteLine("\"\"\"");
                writer.Write(info.HlslShaderSource.HlslSource, isMultiline: true);
                writer.WriteLine("\"\"\";");
                writer.DecreaseIndent();
            }
        }
    }
}