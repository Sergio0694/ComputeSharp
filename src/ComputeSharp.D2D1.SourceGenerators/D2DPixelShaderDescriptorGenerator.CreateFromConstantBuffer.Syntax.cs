using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;

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
            writer.WriteLine($"readonly unsafe {typeName} global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{typeName}>.CreateFromConstantBuffer(global::System.ReadOnlySpan<byte> data)");

            using (writer.WriteBlock())
            {
                // If there are no fields, just return an empty shader (TODO: add length check on .NET 8)
                if (info.Fields.IsEmpty)
                {
                    writer.WriteLine("return default;");

                    return;
                }

                // Get a reference to the data through the generated native layout type and define the shader
                writer.WriteLine($"ref readonly global::ComputeSharp.D2D1.Generated.ConstantBuffer buffer = ref global::System.Runtime.CompilerServices.Unsafe.As<byte, global::ComputeSharp.D2D1.Generated.ConstantBuffer>(ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(data));");
                writer.WriteLine();
                writer.WriteLine($"{typeName} shader;");
                writer.WriteLine();

                // Generate loading statements for each captured field
                foreach (FieldInfo fieldInfo in info.Fields)
                {
                    switch (fieldInfo)
                    {
                        case FieldInfo.Primitive primitive:

                            // Read a primitive value
                            writer.WriteLine($"*&shader.{string.Join(".", primitive.FieldPath)} = buffer.{string.Join("_", primitive.FieldPath)};");
                            break;

                        case FieldInfo.NonLinearMatrix matrix:
                            string primitiveTypeName = matrix.ElementName.ToLowerInvariant();
                            string rowTypeName = HlslKnownTypes.GetMappedName($"ComputeSharp.{matrix.ElementName}{matrix.Columns}");
                            string fieldPath = string.Join(".", matrix.FieldPath);
                            string fieldNamePrefix = string.Join("_", matrix.FieldPath);

                            // Read all rows of a given matrix type:
                            //
                            // Unsafe.As<<PRIMITIVE_TYPE_NAME>, <ROW_TYPE_NAME>>(ref (*&shader.<FIELD_PATH>).M11) = buffer.<CONSTANT_BUFFER_ROW_0_PATH>;
                            // Unsafe.As<<PRIMITIVE_TYPE_NAME>, <ROW_TYPE_NAME>>(ref (*&shader.<FIELD_PATH>).M21) = buffer.<CONSTANT_BUFFER_ROW_1_PATH>;
                            // ...
                            // Unsafe.As<<PRIMITIVE_TYPE_NAME>, <ROW_TYPE_NAME>>(ref (*&shader.<FIELD_PATH>).MN1) = buffer.<CONSTANT_BUFFER_ROW_N_PATH>;
                            for (int j = 0; j < matrix.Rows; j++)
                            {
                                writer.WriteLine($"global::System.Runtime.CompilerServices.Unsafe.As<{primitiveTypeName}, {rowTypeName}>(ref (*&shader.{fieldPath}).M{j + 1}1) = buffer.{fieldNamePrefix}_{j};");
                            }

                            break;
                    }
                }

                // Return the populated shader
                writer.WriteLine();
                writer.WriteLine("return shader;");
            }
        }
    }
}