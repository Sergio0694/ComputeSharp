using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadDispatchData
    {
        /// <summary>
        /// Writes the <c>LoadConstantBuffer</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteLoadConstantBufferSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            string typeName = info.Hierarchy.Hierarchy[0].QualifiedName;

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine("[global::System.Runtime.CompilerServices.SkipLocalsInit]");
            writer.WriteLine($"readonly void global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{typeName}>.LoadConstantBuffer<TLoader>(in {typeName} shader, ref TLoader loader, int x, int y, int z)");

            using (writer.WriteBlock())
            {
                writer.WriteLine($"global::System.Span<byte> span = stackalloc byte[{info.ConstantBufferSizeInBytes}];");

                // Append the statements for the dispatch ranges
                writer.WriteLine("global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref span[0]) = (uint)x;");
                writer.WriteLine("global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref span[4]) = (uint)y;");
                writer.WriteLineIf("global::System.Runtime.CompilerServices.Unsafe.As<byte, uint>(ref span[8]) = (uint)z;", !info.IsPixelShaderLike);

                // Generate loading statements for each captured field
                foreach (FieldInfo fieldInfo in info.Fields)
                {
                    switch (fieldInfo)
                    {
                        case FieldInfo.Primitive { TypeName: "System.Boolean" } primitive:

                            // Read a boolean value and cast it to Bool first, which will apply the correct size expansion
                            writer.WriteLine(
                                $"global::System.Runtime.CompilerServices.Unsafe.As<byte, global::ComputeSharp.Bool>(ref span[{primitive.Offset}]) = " +
                                $"(global::ComputeSharp.Bool)shader.{string.Join(".", primitive.FieldPath)};");
                            break;
                        case FieldInfo.Primitive primitive:

                            // Read a primitive value and serialize it into the target buffer
                            writer.WriteLine(
                                $"global::System.Runtime.CompilerServices.Unsafe.As<byte, global::{primitive.TypeName}>(ref span[{primitive.Offset}]) = " +
                                $"shader.{string.Join(".", primitive.FieldPath)};");
                            break;

                        case FieldInfo.NonLinearMatrix matrix:
                            string rowTypeName = $"global::ComputeSharp.{matrix.ElementName}{matrix.Columns}";
                            string rowLocalName = $"__{string.Join("_", matrix.FieldPath)}__row0";

                            // Declare a local to index into individual rows. This will generate:
                            //
                            // ref <ROW_TYPE> <ROW_NAME> = ref global::System.Runtime.CompilerServices.Unsafe.As<global::<TYPE_NAME>, <ROW_TYPE_NAME>>(
                            //     ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in <FIELD_PATH>));
                            writer.WriteLine(
                                $"ref {rowTypeName} {rowLocalName} = ref global::System.Runtime.CompilerServices.Unsafe.As<global::{matrix.TypeName}, {rowTypeName}>(" +
                                $"ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in shader.{string.Join(".", matrix.FieldPath)}));");

                            // Generate the loading code for each individual row, with proper alignment.
                            // This will result in the following (assuming Float2x3 m):
                            //
                            // ref global::ComputeSharp.Float3 __m__row0 = ref global::System.Runtime.CompilerServices.Unsafe.As<global::ComputeSharp.Float2x3, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in m));
                            // global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)rawDataOffset)) = global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 0);
                            // global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)(rawDataOffset + 16))) = global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 1);
                            for (int j = 0; j < matrix.Rows; j++)
                            {
                                writer.WriteLine(
                                    $"global::System.Runtime.CompilerServices.Unsafe.As<byte, {rowTypeName}>(ref span[{matrix.Offsets[j]}]) = " +
                                    $"global::System.Runtime.CompilerServices.Unsafe.Add(ref {rowLocalName}, {j});");
                            }

                            break;
                    }
                }

                // Load the prepared buffer
                writer.WriteLine("loader.LoadConstantBuffer(span);");
            }
        }

        /// <summary>
        /// Writes the <c>LoadGraphicsResources</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteLoadGraphicsResourcesSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            string typeName = info.Hierarchy.Hierarchy[0].QualifiedName;

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly void global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{typeName}>.LoadGraphicsResources<TLoader>(in {typeName} shader, ref TLoader loader)");

            using (writer.WriteBlock())
            {
                // Generate loading statements for each captured resource
                foreach (FieldInfo fieldInfo in info.Fields)
                {
                    if (fieldInfo is FieldInfo.Resource resource)
                    {
                        writer.WriteLine($"loader.LoadGraphicsResource(shader.{resource.FieldName}, {resource.Offset});");
                    }
                }
            }
        }
    }
}