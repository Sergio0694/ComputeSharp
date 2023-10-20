using System.Linq;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class ConstantBuffer
    {
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

        /// <summary>
        /// Registers a callback to generate additional types, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional types.</param>
        /// <param name="usingDirectives">The using directives needed by the generated code.</param>
        public static void RegisterAdditionalTypesSyntax(
            D2D1ShaderInfo info,
            ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> callbacks,
            ImmutableHashSetBuilder<string> usingDirectives)
        {
            // If there are no fields, there is no need for a constant buffer type
            if (info.Fields.IsEmpty)
            {
                return;
            }

            _ = usingDirectives.Add("global::System.CodeDom.Compiler");
            _ = usingDirectives.Add("global::System.Diagnostics");
            _ = usingDirectives.Add("global::System.Diagnostics.CodeAnalysis");
            _ = usingDirectives.Add("global::System.Runtime.CompilerServices");
            _ = usingDirectives.Add("global::System.Runtime.InteropServices");

            // Declare the ConstantBuffer type
            static void ConstantBufferCallback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                string fullyQualifiedTypeName = info.Hierarchy.GetFullyQualifiedTypeName();

                writer.WriteLine($"""/// <summary>""");
                writer.WriteLine($"""/// A type representing the constant buffer native layout for <see cref="{fullyQualifiedTypeName}"/>.""");
                writer.WriteLine($"""/// </summary>""");
                writer.WriteGeneratedAttributes(GeneratorName, useFullyQualifiedTypeNames: false);
                writer.WriteLine($"""[StructLayout(LayoutKind.Explicit, Size = {info.ConstantBufferSizeInBytes})]""");
                writer.WriteLine($"""file struct ConstantBuffer""");

                using (writer.WriteBlock())
                {
                    // Declare fields for every mapped item from the shader layout
                    writer.WriteLineSeparatedMembers(info.Fields.AsSpan(), (field, writer) =>
                    {
                        switch (field)
                        {
                            case FieldInfo.Primitive { TypeName: "System.Boolean" } primitive:

                                // Append a field as a global::ComputeSharp.Bool value (will use the implicit conversion from bool values)
                                writer.WriteLine($"""/// <inheritdoc cref="{fullyQualifiedTypeName}.{string.Join(".", primitive.FieldPath.Select(static path => path.Name))}"/>""");
                                writer.WriteLine($"""[FieldOffset({primitive.Offset})]""");
                                writer.WriteLine($"""public global::ComputeSharp.Bool {string.Join("_", primitive.FieldPath.Select(static path => path.Name))};""");
                                break;
                            case FieldInfo.Primitive primitive:

                                // Append primitive fields of other types with their mapped names
                                writer.WriteLine($"""/// <inheritdoc cref="{fullyQualifiedTypeName}.{string.Join(".", primitive.FieldPath.Select(static path => path.Name))}"/>""");
                                writer.WriteLine($"""[FieldOffset({primitive.Offset})]""");
                                writer.WriteLine($"""public {HlslKnownTypes.GetMappedName(primitive.TypeName)} {string.Join("_", primitive.FieldPath.Select(static path => path.Name))};""");
                                break;

                            case FieldInfo.NonLinearMatrix matrix:
                                string rowTypeName = HlslKnownTypes.GetMappedName($"ComputeSharp.{matrix.ElementName}{matrix.Columns}");
                                string fieldNamePrefix = string.Join("_", matrix.FieldPath.Select(static path => path.Name));

                                // Declare a field for every row of the matrix type
                                for (int j = 0; j < matrix.Rows; j++)
                                {
                                    writer.WriteLineIf(j > 0);
                                    writer.WriteLine($"""/// <summary>Row {j} of <see cref="{fullyQualifiedTypeName}.{string.Join(".", matrix.FieldPath.Select(static path => path.Name))}"/>.</summary>""");
                                    writer.WriteLine($"""[FieldOffset({matrix.Offsets[j]})]""");
                                    writer.WriteLine($"""public {rowTypeName} {fieldNamePrefix}_{j};""");
                                }

                                break;
                        }
                    });
                }
            }

            // Declare the ConstantBufferMarshaller type
            static void ConstantBufferMarshallerCallback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                string fullyQualifiedTypeName = info.Hierarchy.GetFullyQualifiedTypeName();

                writer.WriteLine($"""/// <summary>""");
                writer.WriteLine($"""/// A type containing marshalling logic for shaders of type <see cref="{fullyQualifiedTypeName}"/>.""");
                writer.WriteLine($"""/// </summary>""");
                writer.WriteGeneratedAttributes(GeneratorName, useFullyQualifiedTypeNames: false);
                writer.WriteLine($"""file static class ConstantBufferMarshaller""");

                using (writer.WriteBlock())
                {
                    // Define the ToManaged method (native constant buffer to managed shader type)
                    writer.WriteLine($"""/// <summary>""");
                    writer.WriteLine($"""/// Marshals native constant buffer data to managed <see cref="{fullyQualifiedTypeName}"/> instances.""");
                    writer.WriteLine($"""/// </summary>""");
                    writer.WriteLine($"""/// <param name="buffer">The input native constant buffer.</param>""");
                    writer.WriteLine($"""/// <returns>The marshalled <see cref="{fullyQualifiedTypeName}"/> instance.</returns>""");
                    writer.WriteLine($"""[MethodImpl(MethodImplOptions.AggressiveInlining)]""");
                    writer.WriteLine($"""[SkipLocalsInit]""");
                    writer.WriteLine($"public static {fullyQualifiedTypeName} ToManaged(in ConstantBuffer buffer)");

                    using (writer.WriteBlock())
                    {
                        writer.WriteLine($"Unsafe.SkipInit(out {fullyQualifiedTypeName} shader);");
                        writer.WriteLine();

                        // Generate loading statements for each captured field
                        foreach (FieldInfo fieldInfo in info.Fields)
                        {
                            switch (fieldInfo)
                            {
                                case FieldInfo.Primitive primitive:

                                    // Read a nested primitive value
                                    writer.WriteLine($"{GetFieldAccessExpression(fieldInfo, true)} = buffer.{string.Join("_", primitive.FieldPath.Select(static path => path.Name))};");
                                    break;

                                case FieldInfo.NonLinearMatrix matrix:
                                    string primitiveTypeName = matrix.ElementName.ToLowerInvariant();
                                    string matrixHlslTypeName = $"{primitiveTypeName}{matrix.Rows}x{matrix.Columns}";
                                    string rowTypeName = HlslKnownTypes.GetMappedName($"ComputeSharp.{matrix.ElementName}{matrix.Columns}");
                                    string fieldNamePrefix = string.Join("_", matrix.FieldPath.Select(static path => path.Name));

                                    // Get a reference to the whole matrix field, just once for all rows
                                    writer.WriteLine(skipIfPresent: true);
                                    writer.WriteLine($"ref {matrixHlslTypeName} __{fieldNamePrefix} = ref {GetFieldAccessExpression(fieldInfo, true)};");
                                    writer.WriteLine();

                                    // Read all rows of a given matrix type
                                    for (int j = 0; j < matrix.Rows; j++)
                                    {
                                        writer.WriteLine($"Unsafe.As<{primitiveTypeName}, {rowTypeName}>(ref __{fieldNamePrefix}.M{j + 1}1) = buffer.{fieldNamePrefix}_{j};");
                                    }

                                    writer.WriteLine();
                                    break;
                            }
                        }

                        writer.WriteLine(skipIfPresent: true);
                        writer.WriteLine("return shader;");
                    }

                    // Define the FromManaged method (managed shader type to native constant buffer)
                    writer.WriteLine();
                    writer.WriteLine($"""/// <summary>""");
                    writer.WriteLine($"""/// Marshals managed <see cref="{fullyQualifiedTypeName}"/> instances to native constant buffer data.""");
                    writer.WriteLine($"""/// </summary>""");
                    writer.WriteLine($"""/// <param name="buffer">The input native constant buffer.</param>""");
                    writer.WriteLine($"""/// <returns>The marshalled <see cref="{fullyQualifiedTypeName}"/> instance.</returns>""");
                    writer.WriteLine($"""[MethodImpl(MethodImplOptions.AggressiveInlining)]""");
                    writer.WriteLine($"""[SkipLocalsInit]""");
                    writer.WriteLine($"public static void FromManaged(in {fullyQualifiedTypeName} shader, out ConstantBuffer buffer)");

                    using (writer.WriteBlock())
                    {
                        // Generate loading statements for each captured field
                        foreach (FieldInfo fieldInfo in info.Fields)
                        {
                            switch (fieldInfo)
                            {
                                case FieldInfo.Primitive primitive:

                                    // Assign a primitive value
                                    writer.WriteLine($"buffer.{string.Join("_", primitive.FieldPath.Select(static path => path.Name))} = {GetFieldAccessExpression(fieldInfo, false)};");
                                    break;

                                case FieldInfo.NonLinearMatrix matrix:
                                    string primitiveTypeName = matrix.ElementName.ToLowerInvariant();
                                    string matrixHlslTypeName = $"{primitiveTypeName}{matrix.Rows}x{matrix.Columns}";
                                    string rowTypeName = HlslKnownTypes.GetMappedName($"ComputeSharp.{matrix.ElementName}{matrix.Columns}");
                                    string fieldNamePrefix = string.Join("_", matrix.FieldPath.Select(static path => path.Name));

                                    // Get a reference to the whole matrix field, just once for all rows
                                    writer.WriteLine(skipIfPresent: true);
                                    writer.WriteLine($"ref {matrixHlslTypeName} __{fieldNamePrefix} = ref {GetFieldAccessExpression(fieldInfo, true)};");
                                    writer.WriteLine();

                                    // Assign all rows of a given matrix type
                                    for (int j = 0; j < matrix.Rows; j++)
                                    {
                                        writer.WriteLine($"buffer.{fieldNamePrefix}_{j} = Unsafe.As<{primitiveTypeName}, {rowTypeName}>(ref __{fieldNamePrefix}.M{j + 1}1);");
                                    }

                                    writer.WriteLine();
                                    break;
                            }
                        }
                    }

                    using ImmutableHashSetBuilder<(string ContainerType, string FieldName)> generatedAccessors = new();

                    // Define all field accessors
                    foreach (FieldInfo fieldInfo in info.Fields)
                    {
                        for (int i = 0; i < fieldInfo.FieldPath.Length; i++)
                        {
                            FieldPathPart pathPart = fieldInfo.FieldPath[i];

                            // Only generate field accessors for fields that are not accessible
                            if (pathPart.IsAccessible)
                            {
                                continue;
                            }

                            // Get the friendly type name for the field:
                            //   - If the part path is a nested struct, the type name is embedded
                            //   - Otherwise, get the correct HLSL mapped type name for the field
                            string typeName = pathPart switch
                            {
                                FieldPathPart.Nested nested => nested.TypeName,
                                FieldPathPart.Leaf when fieldInfo.TypeName is "System.Boolean" => "global::ComputeSharp.Bool",
                                _ => HlslKnownTypes.GetMappedName(fieldInfo.TypeName)
                            };

                            // Get the friendly type name for the field. If this nested field is the
                            // first path, then the parent is the shader type itself. Otherwise, the
                            // container path is the type of the previous containing type of the field.
                            string containingTypeName = i == 0
                                ? fullyQualifiedTypeName
                                : ((FieldPathPart.Nested)fieldInfo.FieldPath[i - 1]).TypeName;

                            // Make sure to skip repeated field accessors. It's possible for some accessors to end up
                            // being required, in case a shader type had multiple fields of the same nested struct type.
                            // To avoid so, we just add the containing type and name to an hashset that we update.
                            if (!generatedAccessors.Add((containingTypeName, pathPart.Name)))
                            {
                                continue;
                            }

                            writer.WriteLine();
                            writer.WriteLine($"""
                                /// <inheritdoc cref="{containingTypeName}.{pathPart.Name}"/>
                                /// <param name="shader">The input <see cref="{containingTypeName}"/> shader instance.</param>
                                /// <returns>A mutable reference to <see cref="{containingTypeName}.{pathPart.Name}"/>.</returns>
                                [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "{pathPart.Name}")]
                                private static extern ref readonly {typeName} {pathPart.Name}(ref readonly {containingTypeName} shader);
                                """, isMultiline: true);
                        }
                    }
                }
            }

            callbacks.Add(ConstantBufferCallback);
            callbacks.Add(ConstantBufferMarshallerCallback);
        }

        /// <summary>
        /// Gets an expression to access a given field.
        /// </summary>
        /// <param name="fieldInfo">The input field to generate the expression for.</param>
        /// <param name="isRef">Whether to return the expression as a mutable <see langword="ref"/>.</param>
        /// <returns>An expression to access a given field.</returns>
        private static string GetFieldAccessExpression(FieldInfo fieldInfo, bool isRef)
        {
            string fieldExpression = "shader";

            // Format the field access expression, there's two options:
            //   - If the field is accessible, just access it directly
            //   - If the field is not accessible, use the unsafe accessor
            foreach (FieldPathPart part in fieldInfo.FieldPath)
            {
                fieldExpression = part.IsAccessible
                    ? $"{fieldExpression}.{part.Name}"
                    : $"{part.Name}(in {fieldExpression})";
            }

            // Return a mutable ref if needed (the expression is always readonly here)
            if (isRef)
            {
                return $"Unsafe.AsRef(in {fieldExpression})";
            }

            return fieldExpression;
        }
    }
}