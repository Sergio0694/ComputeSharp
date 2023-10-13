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
        /// Writes the <c>LoadDispatchData</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine("[global::System.Runtime.CompilerServices.SkipLocalsInit]");
            writer.WriteLine("readonly void global::ComputeSharp.__Internals.IShader.LoadDispatchData<TLoader>(ref TLoader loader, global::ComputeSharp.GraphicsDevice device, int x, int y, int z)");

            using (writer.WriteBlock())
            {
                writer.WriteLine($"global::System.Span<uint> span0 = stackalloc uint[{info.Root32BitConstantCount}];");
                writer.WriteLineIf($"global::System.Span<ulong> span1 = stackalloc ulong[{info.ResourceCount}];", info.ResourceCount > 0);
                writer.WriteLine("ref uint r0 = ref span0[0];");
                writer.WriteLineIf("ref ulong r1 = ref span1[0];", info.ResourceCount > 0);

                // Append the statements for the dispatch ranges
                writer.WriteLine("span0[0] = (uint)x;");
                writer.WriteLine("span0[1] = (uint)y;");
                writer.WriteLineIf("span0[2] = (uint)z;", !info.IsPixelShaderLike);

                // Generate loading statements for each captured field
                foreach (FieldInfo fieldInfo in info.Fields)
                {
                    switch (fieldInfo)
                    {
                        case FieldInfo.Resource resource:

                            // Validate the resource and get a handle for it
                            writer.WriteLine(
                                $"global::System.Runtime.CompilerServices.Unsafe.Add(ref r1, {resource.Offset}) = " +
                                $"global::ComputeSharp.__Internals.GraphicsResourceHelper.ValidateAndGetGpuDescriptorHandle({resource.FieldName}, device);");
                            break;
                        case FieldInfo.Primitive { TypeName: "System.Boolean" } primitive:

                            // Read a boolean value and cast it to Bool first, which will apply the correct size expansion
                            writer.WriteLine(
                                $"global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Bool>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){primitive.Offset})) = " +
                                $"(global::ComputeSharp.Bool){string.Join(".", primitive.FieldPath)};");
                            break;
                        case FieldInfo.Primitive primitive:

                            // Read a primitive value and serialize it into the target buffer
                            writer.WriteLine(
                                $"global::System.Runtime.CompilerServices.Unsafe.As<uint, global::{primitive.TypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){primitive.Offset})) = " +
                                $"{string.Join(".", primitive.FieldPath)};");
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
                                $"ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in {string.Join(".", matrix.FieldPath)}));");

                            // Generate the loading code for each individual row, with proper alignment.
                            // This will result in the following (assuming Float2x3 m):
                            //
                            // ref global::ComputeSharp.Float3 __m__row0 = ref global::System.Runtime.CompilerServices.Unsafe.As<global::ComputeSharp.Float2x3, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in m));
                            // global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)rawDataOffset)) = global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 0);
                            // global::System.Runtime.CompilerServices.Unsafe.As<uint, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)(rawDataOffset + 16))) = global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 1);
                            for (int j = 0; j < matrix.Rows; j++)
                            {
                                writer.WriteLine(
                                    $"global::System.Runtime.CompilerServices.Unsafe.As<uint, {rowTypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){matrix.Offsets[j]})) = " +
                                    $"global::System.Runtime.CompilerServices.Unsafe.Add(ref {rowLocalName}, {j});");
                            }

                            break;
                    }
                }

                // Load the prepared buffers
                writer.WriteLine("loader.LoadCapturedValues(span0);");
                writer.WriteLineIf("loader.LoadCapturedResources(span1);", info.ResourceCount > 0);
            }
        }
    }
}