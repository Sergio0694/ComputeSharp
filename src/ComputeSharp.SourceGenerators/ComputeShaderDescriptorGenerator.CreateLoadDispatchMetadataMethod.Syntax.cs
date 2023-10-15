using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class LoadDispatchMetadata
    {
        /// <summary>
        /// Writes the <c>ConstantBufferSize</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteConstantBufferSizeSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly int global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.ConstantBufferSize => {info.ConstantBufferSizeInBytes};");
        }

        /// <summary>
        /// Writes the <c>IsStaticSamplerRequired</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteIsStaticSamplerRequiredSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly bool global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.IsStaticSamplerRequired => {info.IsSamplerUsed.ToString().ToLowerInvariant()};");
        }

        /// <summary>
        /// Writes the <c>ResourceDescriptorRanges</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.Write($"readonly global::System.ReadOnlyMemory<global::ComputeSharp.Interop.ResourceDescriptorRange> global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.ResourceDescriptorRanges => ");

            // If there are no declared resources, just return an empty collection
            if (info.ResourceDescriptors.IsEmpty)
            {
                writer.WriteLine("default;");
            }
            else
            {
                writer.WriteLine("global::ComputeSharp.Generated.Data.ResourceDescriptorRanges;");
            }
        }

        /// <summary>
        /// Registers a callback to generate an additional data member, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional data members.</param>
        /// <param name="usingDirectives">The using directives needed by the generated code.</param>
        public static void RegisterAdditionalDataMemberSyntax(
            ShaderInfo info,
            ImmutableArrayBuilder<IndentedTextWriter.Callback<ShaderInfo>> callbacks,
            ImmutableHashSetBuilder<string> usingDirectives)
        {
            // If there are no declared resources, there is nothing to di
            if (info.ResourceDescriptors.IsEmpty)
            {
                return;
            }

            usingDirectives.Add("global::System.CodeDom.Compiler");
            usingDirectives.Add("global::System.Diagnostics");
            usingDirectives.Add("global::System.Diagnostics.CodeAnalysis");
            usingDirectives.Add("global::ComputeSharp.Interop");

            // Declare the additional data type and the shared array with resource descriptor ranges
            static void Callback(ShaderInfo info, IndentedTextWriter writer)
            {
                writer.WriteLine($$"""/// <summary>""");
                writer.WriteLine($$"""/// A container type for additional data needed by the shader.""");
                writer.WriteLine($$"""/// </summary>""");
                writer.WriteGeneratedAttributes(GeneratorName, useFullyQualifiedTypeNames: false);
                writer.WriteLine($$"""file static class Data""");

                using (writer.WriteBlock())
                {
                    writer.WriteLine("""/// <summary>The singleton <see cref="ResourceDescriptorRange"/> array instance.</summary>""");
                    writer.WriteLine("""public static readonly ResourceDescriptorRange[] ResourceDescriptorRanges =""");
                    writer.WriteLine("""{""");
                    writer.IncreaseIndent();

                    // Initialize all resource descriptor ranges
                    writer.WriteInitializationExpressions(info.ResourceDescriptors.AsSpan(), static (description, writer) =>
                    {
                        string rangeTypeName = description.TypeId switch
                        {
                            0 => "ShaderResourceView",
                            1 => "UnorderedAccessView",
                            _ => "ConstantBufferView"
                        };

                        writer.Write($"new ResourceDescriptorRange(ResourceDescriptorRangeType.{rangeTypeName}, {description.Register})");
                    });

                    writer.DecreaseIndent();
                    writer.WriteLine();
                    writer.WriteLine("};");
                }
            }

            callbacks.Add(Callback);
        }
    }
}