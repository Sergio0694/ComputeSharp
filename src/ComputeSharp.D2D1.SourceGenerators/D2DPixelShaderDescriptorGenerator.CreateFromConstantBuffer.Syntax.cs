using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class CreateFromConstantBuffer
    {
        /// <summary>
        /// Writes the <c>CreateFromConstantBuffer</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
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