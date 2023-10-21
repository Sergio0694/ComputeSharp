using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

#pragma warning disable IDE0053

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <inheritoc/>
    private static partial class InputDescriptions
    {
        /// <summary>
        /// Writes the <c>InputDescriptions</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.Write($"static global::System.ReadOnlyMemory<global::ComputeSharp.D2D1.Interop.D2D1InputDescription> global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.InputDescriptions => ");

            // If there are no input descriptions, just return a default expression.
            // Otherwise, return the shared array with cached input descriptions.
            if (info.InputDescriptions.IsEmpty)
            {
                writer.WriteLine("default;");
            }
            else
            {
                writer.WriteLine("global::ComputeSharp.D2D1.Generated.Data.InputDescriptions;");
            }
        }

        /// <summary>
        /// Registers a callback to generate additional types, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional types.</param>
        /// <param name="usingDirectives">The using directives needed by the generated code.</param>
        public static void RegisterAdditionalTypesSyntax(
            D2D1ShaderInfo info,
            ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> callbacks,
            ImmutableHashSetBuilder<string> usingDirectives)
        {
            // If there are no necessary data members, stop here
            if (info.InputDescriptions.IsEmpty &&
                info.ResourceTextureDescriptions.IsEmpty)
            {
                return;
            }

            _ = usingDirectives.Add("global::System.CodeDom.Compiler");
            _ = usingDirectives.Add("global::System.Diagnostics");
            _ = usingDirectives.Add("global::System.Diagnostics.CodeAnalysis");

            AddAdditionalDataMemberUsingDirectives(info, usingDirectives);
            ResourceTextureDescriptions.AddAdditionalDataMemberUsingDirectives(info, usingDirectives);

            // Declare the additional data type
            static void Callback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                writer.WriteLine($$"""/// <summary>""");
                writer.WriteLine($$"""/// A container type for additional data needed by the shader.""");
                writer.WriteLine($$"""/// </summary>""");
                writer.WriteGeneratedAttributes(GeneratorName, useFullyQualifiedTypeNames: false);
                writer.WriteLine($$"""file static class Data""");

                using (writer.WriteBlock())
                {
                    using ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> dataMembers = new();

                    RegisterAdditionalDataMemberSyntax(info, dataMembers);
                    ResourceTextureDescriptions.RegisterAdditionalDataMemberSyntax(info, dataMembers);

                    for (int i = 0; i < dataMembers.WrittenSpan.Length; i++)
                    {
                        dataMembers.WrittenSpan[i](info, writer);

                        if (i < dataMembers.WrittenSpan.Length - 1)
                        {
                            writer.WriteLine();
                        }
                    }
                }
            }

            callbacks.Add(Callback);
        }

        /// <summary>
        /// Adds any using directives for the additional data member, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="usingDirectives">The using directives needed by the generated code.</param>
        private static void AddAdditionalDataMemberUsingDirectives(D2D1ShaderInfo info, ImmutableHashSetBuilder<string> usingDirectives)
        {
            if (info.InputDescriptions.IsEmpty)
            {
                return;
            }

            _ = usingDirectives.Add("global::ComputeSharp.D2D1");
            _ = usingDirectives.Add("global::ComputeSharp.D2D1.Interop");
        }

        /// <summary>
        /// Registers a callback to generate an additional data member, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional data members.</param>
        private static void RegisterAdditionalDataMemberSyntax(D2D1ShaderInfo info, ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> callbacks)
        {
            // If there are no input descriptions, there is nothing to di
            if (info.InputDescriptions.IsEmpty)
            {
                return;
            }

            // Declare the shared array with input descriptions
            static void Callback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                writer.WriteLine("""/// <summary>The singleton <see cref="D2D1InputDescription"/> array instance.</summary>""");
                writer.WriteLine("""public static readonly D2D1InputDescription[] InputDescriptions =""");
                writer.WriteLine("""[""");
                writer.IncreaseIndent();

                // Initialize all input descriptions
                writer.WriteInitializationExpressions(info.InputDescriptions.AsSpan(), static (description, writer) =>
                {
                    writer.Write($$"""new D2D1InputDescription({{description.Index}}, D2D1Filter.{{description.Filter}}) { LevelOfDetailCount = {{description.LevelOfDetailCount}} }""");
                });

                writer.DecreaseIndent();
                writer.WriteLine();
                writer.WriteLine("];");
            }

            callbacks.Add(Callback);
        }
    }
}