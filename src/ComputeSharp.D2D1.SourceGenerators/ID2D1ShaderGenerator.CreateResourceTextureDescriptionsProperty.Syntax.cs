using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

#pragma warning disable IDE0053

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritoc/>
    private static partial class ResourceTextureDescriptions
    {
        /// <summary>
        /// Writes the <c>ResourceTextureDescriptions</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(typeof(ID2D1ShaderGenerator));
            writer.Write("readonly global::System.ReadOnlyMemory<global::ComputeSharp.D2D1.Interop.D2D1ResourceTextureDescription> global::ComputeSharp.D2D1.__Internals.ID2D1Shader.ResourceTextureDescriptions => ");

            // If there are no resource texture descriptions, just return a default expression.
            // Otherwise, return a memory instance from the generated static readonly array.
            if (info.ResourceTextureDescriptions.IsEmpty)
            {
                writer.WriteLine("default;");
            }
            else
            {
                writer.WriteLine("global::ComputeSharp.D2D1.Generated.Data.ResourceTextureDescriptions;");
            }
        }

        /// <summary>
        /// Adds any using directives for the additional data member, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="usingDirectives">The using directives needed by the generated code.</param>
        public static void AddAdditionalDataMemberUsingDirectives(D2D1ShaderInfo info, ImmutableHashSetBuilder<string> usingDirectives)
        {
            if (info.ResourceTextureDescriptions.IsEmpty)
            {
                return;
            }

            usingDirectives.Add("global::ComputeSharp.D2D1.Interop");
        }

        /// <summary>
        /// Registers a callback to generate an additional data member, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional data members.</param>
        public static void RegisterAdditionalDataMemberSyntax(D2D1ShaderInfo info, ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> callbacks)
        {
            // If there are no resource texture descriptions, there is nothing to di
            if (info.ResourceTextureDescriptions.IsEmpty)
            {
                return;
            }

            // Declare the shared array with resource texture descriptions
            static void Callback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                writer.WriteLine("""/// <summary>The singleton <see cref="D2D1ResourceTextureDescription"/> array instance.</summary>""");
                writer.WriteLine("""public static readonly D2D1ResourceTextureDescription[] ResourceTextureDescriptions =""");
                writer.WriteLine("""{""");
                writer.IncreaseIndent();

                // Initialize all resource texture descriptions
                writer.WriteInitializationExpressions(info.ResourceTextureDescriptions.AsSpan(), static (description, writer) =>
                {
                    writer.Write($"new D2D1ResourceTextureDescription({description.Index}, {description.Rank})");
                });

                writer.DecreaseIndent();
                writer.WriteLine();
                writer.WriteLine("};");
            }

            callbacks.Add(Callback);
        }
    }
}