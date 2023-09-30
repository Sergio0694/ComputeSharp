using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadDispatchData
    {
        /// <summary>
        /// Writes the <c>LoadDispatchData</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(typeof(ID2D1ShaderGenerator));
            writer.WriteLine("readonly unsafe void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.LoadDispatchData<TLoader>(ref TLoader loader)");

            using (writer.WriteBlock())
            {
                // If there are no fields, just load an empty buffer
                if (info.Fields.IsEmpty)
                {
                    writer.WriteLine("loader.LoadConstantBuffer(default);");
                }
                else
                {
                    writer.WriteLine("global::ComputeSharp.D2D1.Generated.ConstantBuffer buffer;");

                    // Generate loading statements for each captured field
                    foreach (FieldInfo fieldInfo in info.Fields)
                    {
                        switch (fieldInfo)
                        {
                            case FieldInfo.Primitive primitive:

                                // Assign a primitive value
                                writer.WriteLine($"buffer.{string.Join("_", primitive.FieldPath)} = this.{string.Join(".", primitive.FieldPath)};");
                                break;

                            case FieldInfo.NonLinearMatrix matrix:
                                string fieldPath = string.Join(".", matrix.FieldPath);
                                string fieldNamePrefix = string.Join("_", matrix.FieldPath);

                                // Assign all rows of a given matrix type:
                                //
                                // buffer.<CONSTANT_BUFFER_ROW_0_PATH> = this.<FIELD_PATH>[0];
                                // buffer.<CONSTANT_BUFFER_ROW_1_PATH> = this.<FIELD_PATH>[1];
                                // ...
                                // buffer.<CONSTANT_BUFFER_ROW_N_PATH> = this.<FIELD_PATH>[N];
                                for (int j = 0; j < matrix.Rows; j++)
                                {
                                    writer.WriteLine($"buffer.{fieldNamePrefix}_{j} = this.{fieldPath}[{j}];");
                                }

                                break;
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine("loader.LoadConstantBuffer(new global::System.ReadOnlySpan<byte>(&buffer, sizeof(global::ComputeSharp.D2D1.Generated.ConstantBuffer)));");
                }
            }
        }

        /// <summary>
        /// Registers a callback to generate an additional type, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional types.</param>
        /// <param name="usingDirectives">The using directives needed by the generated code.</param>
        public static void RegisterAdditionalTypeSyntax(
            D2D1ShaderInfo info,
            ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> callbacks,
            ImmutableHashSetBuilder<string> usingDirectives)
        {
            // If there are no fields, there is no need for a constant buffer type
            if (info.Fields.IsEmpty)
            {
                return;
            }

            usingDirectives.Add("global::System.CodeDom.Compiler");
            usingDirectives.Add("global::System.Diagnostics");
            usingDirectives.Add("global::System.Diagnostics.CodeAnalysis");
            usingDirectives.Add("global::System.Runtime.InteropServices");

            // Declare the ConstantBuffer type
            static void Callback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                string fullyQualifiedTypeName = info.Hierarchy.GetFullyQualifiedTypeName();

                writer.WriteLine($$"""/// <summary>""");
                writer.WriteLine($$"""/// A type representing the constant buffer native layout for <see cref="{{fullyQualifiedTypeName}}"/>.""");
                writer.WriteLine($$"""/// </summary>""");
                writer.WriteGeneratedAttributes(typeof(ID2D1ShaderGenerator), useFullyQualifiedTypeNames: false);
                writer.WriteLine($$"""[StructLayout(LayoutKind.Explicit, Size = {{info.ConstantBufferSizeInBytes}})]""");
                writer.WriteLine($$"""file struct ConstantBuffer""");

                using (writer.WriteBlock())
                {
                    // Declare fields for every mapped item from the shader layout
                    writer.WriteLineSeparatedMembers(info.Fields.AsSpan(), (field, writer) =>
                    {
                        switch (field)
                        {
                            case FieldInfo.Primitive { TypeName: "System.Boolean" } primitive:

                                // Append a field as a global::ComputeSharp.Bool value (will use the implicit conversion from bool values)
                                writer.WriteLine($$"""/// <inheritdoc cref="{{fullyQualifiedTypeName}}.{{string.Join(".", primitive.FieldPath)}}"/>""");
                                writer.WriteLine($$"""[FieldOffset({{primitive.Offset}})]""");
                                writer.WriteLine($$"""public global::ComputeSharp.Bool {{string.Join("_", primitive.FieldPath)}};""");
                                break;
                            case FieldInfo.Primitive primitive:

                                // Append primitive fields of other types with their mapped names
                                writer.WriteLine($$"""/// <inheritdoc cref="{{fullyQualifiedTypeName}}.{{string.Join(".", primitive.FieldPath)}}"/>""");
                                writer.WriteLine($$"""[FieldOffset({{primitive.Offset}})]""");
                                writer.WriteLine($$"""public {{HlslKnownTypes.GetMappedName(primitive.TypeName)}} {{string.Join("_", primitive.FieldPath)}};""");
                                break;

                            case FieldInfo.NonLinearMatrix matrix:
                                string rowTypeName = HlslKnownTypes.GetMappedName($"ComputeSharp.{matrix.ElementName}{matrix.Columns}");
                                string fieldNamePrefix = string.Join("_", matrix.FieldPath);

                                // Declare a field for every row of the matrix type
                                for (int j = 0; j < matrix.Rows; j++)
                                {
                                    writer.WriteLineIf(j > 0);
                                    writer.WriteLine($$"""/// <summary>Row {j} of <see cref="{{fullyQualifiedTypeName}}.{{string.Join(".", matrix.FieldPath)}}"/>.</summary>""");
                                    writer.WriteLine($$"""[FieldOffset({{matrix.Offsets[j]}})]""");
                                    writer.WriteLine($$"""public {{rowTypeName}} {{fieldNamePrefix}}_{{j}};""");
                                }

                                break;
                        }
                    });
                }
            }

            callbacks.Add(Callback);
        }
    }
}