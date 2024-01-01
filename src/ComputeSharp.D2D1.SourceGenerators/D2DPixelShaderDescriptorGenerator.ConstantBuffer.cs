using System.Collections.Immutable;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the constant buffer marshalling methods and additional types.
    /// </summary>
    private static class ConstantBuffer
    {
        /// <inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo"/>
        /// <param name="compilation"><inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo" path="/param[@name='compilation']/node()"/></param>
        /// <param name="structDeclarationSymbol"><inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo" path="/param[@name='structDeclarationSymbol']/node()"/></param>
        /// <param name="constantBufferSizeInBytes"><inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo" path="/param[@name='constantBufferSizeInBytes']/node()"/></param>
        /// <param name="fields"><inheritdoc cref="ConstantBufferSyntaxProcessor.GetInfo" path="/param[@name='fields']/node()"/></param>
        public static void GetInfo(
            Compilation compilation,
            ITypeSymbol structDeclarationSymbol,
            out int constantBufferSizeInBytes,
            out ImmutableArray<FieldInfo> fields)
        {
            constantBufferSizeInBytes = 0;

            ConstantBufferSyntaxProcessor.GetInfo(
                compilation,
                structDeclarationSymbol,
                ref constantBufferSizeInBytes,
                out fields);
        }

        /// <summary>
        /// Writes the <c>LoadConstantBuffer</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteLoadConstantBufferSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            string typeName = info.Hierarchy.Hierarchy[0].QualifiedName;

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine("[global::System.Runtime.CompilerServices.SkipLocalsInit]");
            writer.WriteLine($"static unsafe void global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{typeName}>.LoadConstantBuffer<TLoader>(in {typeName} shader, ref TLoader loader)");

            using (writer.WriteBlock())
            {
                // If there are no fields, just load an empty buffer
                if (info.Fields.IsEmpty)
                {
                    writer.WriteLine("loader.LoadConstantBuffer(default);");
                }
                else
                {
                    // Otherwise, pass a span into the marshalled native layout buffer
                    writer.WriteLine("global::ComputeSharp.D2D1.Generated.ConstantBufferMarshaller.FromManaged(in shader, out global::ComputeSharp.D2D1.Generated.ConstantBuffer buffer);");
                    writer.WriteLine();
                    writer.WriteLine("loader.LoadConstantBuffer(new global::System.ReadOnlySpan<byte>(&buffer, sizeof(global::ComputeSharp.D2D1.Generated.ConstantBuffer)));");
                }
            }
        }

        /// <summary>
        /// Writes the <c>CreateFromConstantBuffer</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteCreateFromConstantBufferSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            string typeName = info.Hierarchy.Hierarchy[0].QualifiedName;

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine("[global::System.Runtime.CompilerServices.SkipLocalsInit]");
            writer.WriteLine($"static unsafe {typeName} global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{typeName}>.CreateFromConstantBuffer(global::System.ReadOnlySpan<byte> data)");

            using (writer.WriteBlock())
            {
                // If there are no fields, just return an empty shader
                if (info.Fields.IsEmpty)
                {
                    writer.WriteLine("return default;");

                    return;
                }

                // Get a reference to the data through the generated native layout type and define the shader
                writer.WriteLine("ref readonly global::ComputeSharp.D2D1.Generated.ConstantBuffer buffer = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::ComputeSharp.D2D1.Generated.ConstantBuffer>(data);");
                writer.WriteLine();
                writer.WriteLine("return global::ComputeSharp.D2D1.Generated.ConstantBufferMarshaller.ToManaged(in buffer);");
            }
        }
    }
}