using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class NumThreads
    {
        /// <summary>
        /// Writes the <c>ThreadsX</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteThreadsXSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"static int global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.ThreadsX => {info.ThreadsX};");
        }

        /// <summary>
        /// Writes the <c>ThreadsY</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteThreadsYSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"static int global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.ThreadsY => {info.ThreadsY};");
        }

        /// <summary>
        /// Writes the <c>ThreadsZ</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteThreadsZSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"static int global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.ThreadsZ => {info.ThreadsZ};");
        }
    }
}