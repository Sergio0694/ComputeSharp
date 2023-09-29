using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class EffectMetadata
    {
        /// <summary>
        /// Writes the <c>EffectDisplayName</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteEffectDisplayNameSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            WriteEffectMetadataSyntax("EffectDisplayName", info.EffectDisplayName, writer);
        }

        /// <summary>
        /// Writes the <c>EffectDescription</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteEffectDescriptionSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            WriteEffectMetadataSyntax("EffectDescription", info.EffectDescription, writer);
        }

        /// <summary>
        /// Writes the <c>EffectCategory</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteEffectCategorySyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            WriteEffectMetadataSyntax("EffectCategory", info.EffectCategory, writer);
        }

        /// <summary>
        /// Writes the <c>EffectAuthor</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteEffectAuthorSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            WriteEffectMetadataSyntax("EffectAuthor", info.EffectAuthor, writer);
        }

        /// <summary>
        /// Writes a specified metadata property.
        /// </summary>
        /// <param name="propertyName">The property name to generate.</param>
        /// <param name="metadataValue">The input effect metadata value, if available.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        private static void WriteEffectMetadataSyntax(string propertyName, string? metadataValue, IndentedTextWriter writer)
        {
            writer.Write($"readonly string? global::ComputeSharp.D2D1.__Internals.ID2D1Shader.{propertyName} => ");

            // Append null or the metadata value as a string literal
            if (metadataValue is null)
            {
                writer.WriteLine("null;");
            }
            else
            {
                writer.WriteLine($"\"{metadataValue}\";");
            }
        }
    }
}