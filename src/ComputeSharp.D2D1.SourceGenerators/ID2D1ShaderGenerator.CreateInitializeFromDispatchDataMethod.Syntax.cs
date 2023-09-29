using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class InitializeFromDispatchData
    {
        /// <summary>
        /// Writes the <c>InitializeFromDispatchData</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(typeof(ID2D1ShaderGenerator));
            writer.WriteLine("readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.InitializeFromDispatchData(global::System.ReadOnlySpan<byte> data)");

            using (writer.WriteBlock())
            {
                // If there are no fields, the method body is just empty
                if (info.Fields.IsEmpty)
                {
                    return;
                }

                // Insert the fallback for empty shaders. This will generate the following code:
                writer.WriteLine("if (data.IsEmpty)");

                using (writer.WriteBlock())
                {
                    writer.WriteLine("return;");
                }

                // Get a reference to the data through the generated native layout type
                writer.WriteLine();
                writer.WriteLine("ref readonly ConstantBuffer buffer = ref global::System.Runtime.CompilerServices.Unsafe.As<byte, ConstantBuffer>(ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(data));");

                // Generate loading statements for each captured field
                foreach (FieldInfo fieldInfo in info.Fields)
                {
                    switch (fieldInfo)
                    {
                        case FieldInfo.Primitive primitive:

                            // Read a primitive value
                            writer.WriteLine($"global::System.Runtime.CompilerServices.Unsafe.AsRef(in this.{string.Join(".", primitive.FieldPath)}) = buffer.{string.Join("_", primitive.FieldPath)};");
                            break;

                        case FieldInfo.NonLinearMatrix matrix:
                            string fieldPath = string.Join(".", matrix.FieldPath);
                            string fieldNamePrefix = string.Join("_", matrix.FieldPath);

                            // Read all rows of a given matrix type:
                            //
                            // global::System.Runtime.CompilerServices.Unsafe.AsRef(in this.<FIELD_PATH>)[0] = buffer.<CONSTANT_BUFFER_ROW_0_PATH>;
                            // global::System.Runtime.CompilerServices.Unsafe.AsRef(in this.<FIELD_PATH>)[1] = buffer.<CONSTANT_BUFFER_ROW_1_PATH>;
                            // ...
                            // global::System.Runtime.CompilerServices.Unsafe.AsRef(in this.<FIELD_PATH>)[N] = buffer.<CONSTANT_BUFFER_ROW_N_PATH>;
                            for (int j = 0; j < matrix.Rows; j++)
                            {
                                writer.WriteLine($"global::System.Runtime.CompilerServices.Unsafe.AsRef(in this.{fieldPath})[0] = buffer.{fieldNamePrefix}_{j};");
                            }

                            break;
                    }
                }
            }
        }
    }
}