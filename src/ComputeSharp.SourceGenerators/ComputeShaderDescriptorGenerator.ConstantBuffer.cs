using System.Collections.Immutable;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;

#pragma warning disable CS0419

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the constant buffer marshalling methods and additional types.
    /// </summary>
    private static class ConstantBuffer
    {
        /// <inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo"/>
        /// <param name="compilation"><inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo" path="/param[@name='compilation']/node()"/></param>
        /// <param name="structDeclarationSymbol"><inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo" path="/param[@name='structDeclarationSymbol']/node()"/></param>
        /// <param name="isPixelShaderLike">Whether the compute shader is "pixel shader like", ie. outputting a pixel into a target texture.</param>
        /// <param name="constantBufferSizeInBytes"><inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo" path="/param[@name='constantBufferSizeInBytes']/node()"/></param>
        /// <param name="fields"><inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo" path="/param[@name='fields']/node()"/></param>
        public static void GetInfo(
            Compilation compilation,
            ITypeSymbol structDeclarationSymbol,
            bool isPixelShaderLike,
            out int constantBufferSizeInBytes,
            out ImmutableArray<FieldInfo> fields)
        {
            // Setup the resource and byte offsets for tracking. Pixel shaders have only two
            // implicitly captured int values, as they're always dispatched over a 2D texture.
            constantBufferSizeInBytes = sizeof(int) * (isPixelShaderLike ? 2 : 3);

            ConstantBufferSyntaxProcessor.GetInfo(
                compilation,
                structDeclarationSymbol,
                ref constantBufferSizeInBytes,
                out fields);

            // After all the captured fields have been processed, ensure the reported byte size for
            // the local variables is padded to a multiple of a 32 bit value. This is necessary to
            // enable loading all the dispatch data after reinterpreting it to a sequence of values
            // of size 32 bits (via SetComputeRoot32BitConstants), without reading out of bounds.
            constantBufferSizeInBytes = AlignmentHelper.Pad(constantBufferSizeInBytes, sizeof(int));
        }

        /// <summary>
        /// Writes the <c>LoadConstantBuffer</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            string typeName = info.Hierarchy.Hierarchy[0].QualifiedName;

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine("[global::System.Runtime.CompilerServices.SkipLocalsInit]");
            writer.WriteLine($"static unsafe void global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{typeName}>.LoadConstantBuffer<TLoader>(in {typeName} shader, ref TLoader loader, int x, int y, int z)");

            using (writer.WriteBlock())
            {
                // If there are no fields (ie. there are only artificial ones), just get an uninitialized instance.
                // All fields (ie. the thread size values) will be assigned directly inline, with no marshalling.
                if (info.Fields.IsEmpty)
                {
                    writer.WriteLine("global::System.Runtime.CompilerServices.Unsafe.SkipInit(out global::ComputeSharp.Generated.ConstantBuffer buffer);");
                }
                else
                {
                    // Otherwise, let the generated marshaller create an initialized instance of the constant buffer type
                    writer.WriteLine("global::ComputeSharp.Generated.ConstantBufferMarshaller.FromManaged(in shader, out global::ComputeSharp.Generated.ConstantBuffer buffer);");
                }

                // Initialize the artificial fields and finally pass a span over the constant buffer
                writer.WriteLine();
                writer.WriteLine("buffer.__x = x;");
                writer.WriteLine("buffer.__y = y;");
                writer.WriteLineIf(!info.IsPixelShaderLike, "buffer.__z = z;");
                writer.WriteLineIf(!info.IsPixelShaderLike);
                writer.WriteLine("loader.LoadConstantBuffer(new global::System.ReadOnlySpan<byte>(&buffer, sizeof(global::ComputeSharp.Generated.ConstantBuffer)));");
            }
        }
    }
}