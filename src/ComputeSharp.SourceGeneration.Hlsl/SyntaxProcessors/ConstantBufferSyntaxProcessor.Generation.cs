using System.ComponentModel;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <inheritdoc/>
partial class ConstantBufferSyntaxProcessor
{
    /// <summary>
    /// The name of generator to include in the generated code.
    /// </summary>
    private static string? generatorName;

    /// <summary>
    /// The direction to generate marshalling code for.
    /// </summary>
    private static BindingDirection direction;

    /// <summary>
    /// Registers a callback to generate additional types, if needed.
    /// </summary>
    /// <typeparam name="TInfo">The type of model providing the necessary info.</typeparam>
    /// <param name="generatorName">The name of generator to include in the generated code.</param>
    /// <param name="direction">The direction to generate marshalling code for.</param>
    /// <param name="info">The input <typeparamref name="TInfo"/> instance with gathered shader info.</param>
    /// <param name="callbacks">The registered callbacks to generate additional types.</param>
    /// <param name="usingDirectives">The using directives needed by the generated code.</param>
    public static void RegisterAdditionalTypesSyntax<TInfo>(
        string generatorName,
        BindingDirection direction,
        TInfo info,
        ImmutableArrayBuilder<IndentedTextWriter.Callback<TInfo>> callbacks,
        ImmutableHashSetBuilder<string> usingDirectives)
        where TInfo : class, IConstantBufferInfo
    {
        // Store the generator name and marshalling direction for later. This avoids needing closures for each
        // callback below. It is assumed that all calls to this method will have the same arguments in each assembly.
        ConstantBufferSyntaxProcessor.generatorName = generatorName;
        ConstantBufferSyntaxProcessor.direction = direction;

        // If there are no fields (including artificial ones), there is no need for a constant buffer type
        if (info.Fields.IsEmpty && !HasArtificialFields())
        {
            return;
        }

        _ = usingDirectives.Add("global::System.CodeDom.Compiler");
        _ = usingDirectives.Add("global::System.Diagnostics");
        _ = usingDirectives.Add("global::System.Diagnostics.CodeAnalysis");
        _ = usingDirectives.Add("global::System.Runtime.InteropServices");

        // Only add this directive if the marshaller is also present, as it's only needed there
        if (!info.Fields.IsEmpty)
        {
            _ = usingDirectives.Add("global::System.Runtime.CompilerServices");
        }

        // Declare the ConstantBuffer type
        static void ConstantBufferCallback(TInfo info, IndentedTextWriter writer)
        {
            string fullyQualifiedTypeName = info.Hierarchy.GetFullyQualifiedTypeName();

            writer.WriteLine($"""/// <summary>""");
            writer.WriteLine($"""/// A type representing the constant buffer native layout for <see cref="{fullyQualifiedTypeName}"/>.""");
            writer.WriteLine($"""/// </summary>""");
            writer.WriteGeneratedAttributes(ConstantBufferSyntaxProcessor.generatorName!, useFullyQualifiedTypeNames: false);
            writer.WriteLine($"""[StructLayout(LayoutKind.Explicit, Size = {info.ConstantBufferSizeInBytes})]""");
            writer.WriteLine($"""file struct ConstantBuffer""");

            using (writer.WriteBlock())
            {
                // Append any artificial fields, if needed
                AppendArtificialFields(info, writer);

                // Declare fields for every mapped item from the shader layout
                writer.WriteLineSeparatedMembers(info.Fields.AsSpan(), (field, writer) =>
                {
                    switch (field)
                    {
                        case FieldInfo.Primitive { TypeName: "System.Boolean" or "ComputeSharp.Bool" } primitive:

                            // Append a field as a Bool value for the native constant buffer layout. This is the case both for
                            // fields of type System.Boolean (they will use the implicit conversion from bool values), as well
                            // as for fields that were originally declared as Bool from the start (not recommended, but supported).
                            writer.WriteFieldXmlSummary(field, fullyQualifiedTypeName);
                            writer.WriteFieldXmlRemarks(field);
                            writer.WriteLine($"""[FieldOffset({primitive.Offset})]""");
                            writer.WriteLine($"""public Bool {string.Join("_", primitive.FieldPath.Select(static path => path.Name))};""");
                            break;
                        case FieldInfo.Primitive primitive:

                            // Append primitive fields of other types with their mapped names
                            writer.WriteFieldXmlSummary(field, fullyQualifiedTypeName);
                            writer.WriteFieldXmlRemarks(field);
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
                                writer.WriteFieldXmlSummary(field, fullyQualifiedTypeName);
                                writer.WriteFieldXmlRemarks(field, row: j);
                                writer.WriteLine($"""[FieldOffset({matrix.Offsets[j]})]""");
                                writer.WriteLine($"""public {rowTypeName} {fieldNamePrefix}_{j};""");
                            }

                            break;
                    }
                });
            }
        }

        // Declare the ConstantBufferMarshaller type
        static void ConstantBufferMarshallerCallback(TInfo info, IndentedTextWriter writer)
        {
            string fullyQualifiedTypeName = info.Hierarchy.GetFullyQualifiedTypeName();

            writer.WriteLine($"""/// <summary>""");
            writer.WriteLine($"""/// A type containing marshalling logic for the constant buffer in shaders of type <see cref="{fullyQualifiedTypeName}"/>.""");
            writer.WriteLine($"""/// </summary>""");
            writer.WriteGeneratedAttributes(ConstantBufferSyntaxProcessor.generatorName!, useFullyQualifiedTypeNames: false);
            writer.WriteLine($"""file static class ConstantBufferMarshaller""");

            using (writer.WriteBlock())
            {
                // Define the FromManaged method (managed shader type to native constant buffer)
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
                    // Initialize any artificial fields, if needed. This is necessary to avoid compiler errors due to
                    // the resulting constant buffer not being fully initialized, in case it had some extra fields.
                    InitializeArtificialFields(info, writer);

                    // Generate loading statements for each captured field
                    foreach (FieldInfo fieldInfo in info.Fields)
                    {
                        switch (fieldInfo)
                        {
                            case FieldInfo.Primitive primitive:

                                // Assign a primitive value
                                writer.Write($"buffer.{string.Join("_", primitive.FieldPath.Select(static path => path.Name))} = ");
                                writer.WriteFieldAccessExpression(fieldInfo);
                                writer.WriteLine(";");
                                break;

                            case FieldInfo.NonLinearMatrix matrix:
                                string primitiveTypeName = matrix.ElementName.ToLowerInvariant();
                                string matrixHlslTypeName = $"{primitiveTypeName}{matrix.Rows}x{matrix.Columns}";
                                string rowTypeName = HlslKnownTypes.GetMappedName($"ComputeSharp.{matrix.ElementName}{matrix.Columns}");
                                string fieldNamePrefix = string.Join("_", matrix.FieldPath.Select(static path => path.Name));

                                // Get a reference to the whole matrix field, just once for all rows
                                writer.WriteLine(skipIfPresent: true);
                                writer.Write($"ref {matrixHlslTypeName} __{fieldNamePrefix} = ref ");
                                writer.WriteFieldAccessExpression(fieldInfo);
                                writer.WriteLine(";");
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

                // Only generate the other direction if requested
                if (ConstantBufferSyntaxProcessor.direction == BindingDirection.TwoWay)
                {
                    // Define the ToManaged method (native constant buffer to managed shader type)
                    writer.WriteLine();
                    writer.WriteLine($"""/// <summary>""");
                    writer.WriteLine($"""/// Marshals native constant buffer data to managed <see cref="{fullyQualifiedTypeName}"/> instances.""");
                    writer.WriteLine($"""/// </summary>""");
                    writer.WriteLine($"""/// <param name="buffer">The input native constant buffer.</param>""");
                    writer.WriteLine($"""/// <returns>The marshalled <see cref="{fullyQualifiedTypeName}"/> instance.</returns>""");
                    writer.WriteLine($"""[MethodImpl(MethodImplOptions.AggressiveInlining)]""");
                    writer.WriteLine($"""[SkipLocalsInit]""");
                    writer.WriteLine($"public static {fullyQualifiedTypeName} ToManaged(ref readonly ConstantBuffer buffer)");

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
                                    writer.WriteFieldAccessExpression(fieldInfo);
                                    writer.WriteLine($" = buffer.{string.Join("_", primitive.FieldPath.Select(static path => path.Name))};");
                                    break;

                                case FieldInfo.NonLinearMatrix matrix:
                                    string primitiveTypeName = matrix.ElementName.ToLowerInvariant();
                                    string matrixHlslTypeName = $"{primitiveTypeName}{matrix.Rows}x{matrix.Columns}";
                                    string rowTypeName = HlslKnownTypes.GetMappedName($"ComputeSharp.{matrix.ElementName}{matrix.Columns}");
                                    string fieldNamePrefix = string.Join("_", matrix.FieldPath.Select(static path => path.Name));

                                    // Get a reference to the whole matrix field, just once for all rows
                                    writer.WriteLine(skipIfPresent: true);
                                    writer.Write($"ref {matrixHlslTypeName} __{fieldNamePrefix} = ref ");
                                    writer.WriteFieldAccessExpression(fieldInfo);
                                    writer.WriteLine(";");
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
                }

                using ImmutableHashSetBuilder<(string ContainerType, string FieldName)> generatedAccessors = new();

                // The return value of the field accessors only needs to be mutable if we're doing two-way binding.
                // Otherwise, we're only ever reading the field values, but we don't need to write back to them.
                string refModifier = ConstantBufferSyntaxProcessor.direction == BindingDirection.TwoWay ? "ref" : "ref readonly";

                // Define all field accessors. Note that these are generated regardless of the accessibility
                // of the field they're getting the value for. This simplifies the rest of the code, and allows
                // stripping the readonly-ness of the field references, and has no binary size impact anyway.
                // This is because the fields are compiled to be exactly equivalent to direct field references.
                foreach (FieldInfo fieldInfo in info.Fields)
                {
                    for (int i = 0; i < fieldInfo.FieldPath.Length; i++)
                    {
                        FieldPathPart pathPart = fieldInfo.FieldPath[i];

                        // Get the friendly type name for the field:
                        //   - If the part path is a nested struct, the type name is embedded
                        //   - Otherwise, get the correct HLSL mapped type name for the field
                        string typeName = pathPart switch
                        {
                            FieldPathPart.Nested nested => nested.TypeName,
                            _ => HlslKnownTypes.GetMappedName(fieldInfo.TypeName)
                        };

                        // Special case: if the field was actually of type ComputeSharp.Bool, and not System.Boolean,
                        // we need to preserve the original type name, and we cannot use the mapped type name (bool),
                        // as it would cause the generated field accessor to fail (the field type would not match).
                        if (fieldInfo.TypeName == "ComputeSharp.Bool")
                        {
                            typeName = "Bool";
                        }

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

                        // If there is no unspeakable name, we can reference the field directly. This means we can both
                        // use that in XML docs to refer to it (and inherit the docs), and we don't need an explicit name.
                        if (pathPart.UnspeakableName is null)
                        {
                            writer.WriteLine($"""
                                /// <inheritdoc cref="{containingTypeName}.{pathPart.Name}"/>
                                /// <param name="value">The input <see cref="{containingTypeName}"/> value.</param>
                                /// <returns>A mutable reference to <see cref="{containingTypeName}.{pathPart.Name}"/>.</returns>
                                [UnsafeAccessor(UnsafeAccessorKind.Field)]
                                private static extern {refModifier} {typeName} {pathPart.Name}(this ref readonly {containingTypeName} value);
                                """, isMultiline: true);
                        }
                        else
                        {
                            writer.WriteLine($"""
                                /// <summary>Gets a reference to the unspeakable field "{pathPart.Name}" of type <see cref="{containingTypeName}"/>.</summary>
                                /// <param name="value">The input <see cref="{containingTypeName}"/> value.</param>
                                /// <returns>A mutable reference to the unspeakable field "{pathPart.Name}".</returns>
                                [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "{pathPart.UnspeakableName}")]
                                private static extern {refModifier} {typeName} {pathPart.Name}(this ref readonly {containingTypeName} value);
                                """, isMultiline: true);
                        }
                    }
                }
            }
        }

        callbacks.Add(ConstantBufferCallback);

        // Only emit the marshaller type if there are any non artificial fields.
        // Otherwise, it's not needed, as there are either just no fields at all,
        // or only artificial ones, which would be assigned directly by callers.
        if (!info.Fields.IsEmpty)
        {
            callbacks.Add(ConstantBufferMarshallerCallback);
        }
    }

    /// <summary>
    /// Checks whether any artificial fields are present.
    /// </summary>
    /// <returns>Whether any artificial fields are present.</returns>
    private static partial bool HasArtificialFields();

    /// <summary>
    /// Appends any artificial fields to the generated constant buffer type.
    /// </summary>
    /// <param name="info">The <see cref="IConstantBufferInfo"/> instance to use to extract info.</param>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <remarks>This method is responsible for leaving trailing blanklines, if it adds any fields.</remarks>
    static partial void AppendArtificialFields(IConstantBufferInfo info, IndentedTextWriter writer);

    /// <summary>
    /// Initializes any artificial fields that have been generated into the constant buffer.
    /// </summary>
    /// <param name="info">The <see cref="IConstantBufferInfo"/> instance to use to extract info.</param>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <remarks>This method is responsible for leaving trailing blanklines, if it emits any statements.</remarks>
    static partial void InitializeArtificialFields(IConstantBufferInfo info, IndentedTextWriter writer);
}

/// <inheritdoc cref="Extensions.IndentedTextWriterExtensions"/>
file static class IndentedTextWriterExtensions
{
    /// <summary>
    /// Writes the XML summary for a given field.
    /// </summary>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="fieldInfo">The input field to write the XML summary for.</param>
    /// <param name="fullyQualifiedTypeName">The fully qualified type name of the containing type.</param>
    public static void WriteFieldXmlSummary(this IndentedTextWriter writer, FieldInfo fieldInfo, string fullyQualifiedTypeName)
    {
        if (fieldInfo.FieldPath[0].UnspeakableName is null)
        {
            writer.WriteLine($"""/// <inheritdoc cref="{fullyQualifiedTypeName}.{fieldInfo.FieldPath[0].Name}"/>""");
        }
        else
        {
            writer.WriteLine($"""/// <summary>The unspeakable field "{fieldInfo.FieldPath[0].Name}" of <see cref="{fullyQualifiedTypeName}"/>.</summary>""");
        }
    }

    /// <summary>
    /// Writes the XML remarks for a given field, if needed.
    /// </summary>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="fieldInfo">The input field to write the XML remarks for, if needed.</param>
    /// <param name="row">The row for the field, if this is a matrix type.</param>
    public static void WriteFieldXmlRemarks(this IndentedTextWriter writer, FieldInfo fieldInfo, int? row = null)
    {
        // The remarks are only needed if the field is nested
        if (fieldInfo.FieldPath is not [.., FieldPathPart.Nested nested, FieldPathPart.Leaf leaf])
        {
            return;
        }

        // Write the correct remarks depending on whether the field can be referenced directly
        if (leaf.UnspeakableName is null)
        {
            writer.Write($"""/// <remarks>Serialized field mapping to""");
            writer.WriteIf(row is not null, $""" row {row} of""");
            writer.WriteLine($""" <see cref="{nested.TypeName}.{leaf.Name}"/>.</remarks>""");
        }
        else
        {
            writer.Write($"""/// <remarks>Serialized field mapping to""");
            writer.WriteIf(row is not null, $""" row {row} of""");
            writer.WriteLine($""" the unspeakable field "{leaf.Name}" of <see cref="{nested.TypeName}"/>.</remarks>""");
        }
    }

    /// <summary>
    /// Writes an expression to access a given field.
    /// </summary>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="fieldInfo">The input field to write the expression for.</param>
    public static void WriteFieldAccessExpression(this IndentedTextWriter writer, FieldInfo fieldInfo)
    {
        writer.Write("shader");

        // Invoke all generated field accessors to get to the target field.
        // Because they're extension methods, we can chain them sequentially.
        // This simplifies the formatting and makes the code easier to read.
        foreach (FieldPathPart part in fieldInfo.FieldPath)
        {
            writer.Write($".{part.Name}()");
        }
    }
}