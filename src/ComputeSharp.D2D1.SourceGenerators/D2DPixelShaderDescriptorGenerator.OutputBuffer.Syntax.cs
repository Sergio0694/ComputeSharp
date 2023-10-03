using System;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class OutputBuffer
    {
        /// <summary>
        /// Writes the <c>BufferPrecision</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteBufferPrecisionSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            // Set the right expression if the buffer options are valid.
            // If they are not, just return default (the analyzer will emit a diagnostic).
            string bufferPrecisionExpression = Enum.IsDefined(typeof(D2D1BufferPrecision), info.BufferPrecision) switch
            {
                true => $"global::ComputeSharp.D2D1.D2D1BufferPrecision.{info.BufferPrecision}",
                false => "default"
            };

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly ComputeSharp.D2D1.D2D1BufferPrecision global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.BufferPrecision => {bufferPrecisionExpression};");
        }

        /// <summary>
        /// Writes the <c>ChannelDepth</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteChannelDepthSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            // Set the right expression if the buffer options are valid
            string channelDepthExpression = Enum.IsDefined(typeof(D2D1ChannelDepth), info.ChannelDepth) switch
            {
                true => $"global::ComputeSharp.D2D1.D2D1ChannelDepth.{info.ChannelDepth}",
                false => "default"
            };

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly ComputeSharp.D2D1.D2D1ChannelDepth global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.ChannelDepth => {channelDepthExpression};");
        }
    }
}