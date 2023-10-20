using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class DispatchDataLoading
    {
        /// <summary>
        /// Writes the <c>LoadGraphicsResources</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteLoadGraphicsResourcesSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            string typeName = info.Hierarchy.Hierarchy[0].QualifiedName;

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"static void global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{typeName}>.LoadGraphicsResources<TLoader>(in {typeName} shader, ref TLoader loader)");

            using (writer.WriteBlock())
            {
                // Generate loading statements for each captured resource
                foreach (ResourceInfo resource in info.Resources)
                {
                    writer.WriteLine($"loader.LoadGraphicsResource(shader.{resource.FieldName}, {resource.Offset});");
                }
            }
        }
    }
}