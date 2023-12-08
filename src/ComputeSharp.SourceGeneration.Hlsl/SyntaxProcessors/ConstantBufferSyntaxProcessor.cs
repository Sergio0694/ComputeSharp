using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <summary>
/// A processor responsible for extracting info and writing syntax for the constant buffer syntax for shader types.
/// </summary>
internal static partial class ConstantBufferSyntaxProcessor
{
    /// <summary>
    /// A <see cref="Regex"/> to extract the parameter name of a primary constructor capture field.
    /// </summary>
    /// <remarks>
    /// Temporary workaround until <see href="https://github.com/dotnet/roslyn/issues/70208"/> is available.
    /// </remarks>
    private static readonly Regex PrimaryConstructorNameRegex = new("^<([^>]+)>P$", RegexOptions.Compiled);

    /// <summary>
    /// Explores a given type hierarchy and generates statements to load fields.
    /// </summary>
    /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
    /// <param name="structDeclarationSymbol">The current shader type being explored.</param>
    /// <param name="constantBufferSizeInBytes">The size of the shader constant buffer.</param>
    /// <param name="fields">The sequence of <see cref="FieldInfo"/> instances for all captured values.</param>
    public static void GetInfo(
        Compilation compilation,
        ITypeSymbol structDeclarationSymbol,
        ref int constantBufferSizeInBytes,
        out ImmutableArray<FieldInfo> fields)
    {
        // Helper method to traverse the type hierarchy and append all valid fields
        static void GetInfo(
            Compilation compilation,
            ITypeSymbol currentTypeSymbol,
            ImmutableArray<FieldPathPart> fieldPath,
            ref int constantBufferSizeInBytes,
            ImmutableArrayBuilder<FieldInfo> fields)
        {
            bool isFirstField = true;

            foreach (ISymbol memberSymbol in currentTypeSymbol.GetMembers())
            {
                // Only process fields of a valid shape/type
                if (memberSymbol is not IFieldSymbol { Type: INamedTypeSymbol { IsStatic: false }, IsConst: false, IsStatic: false, IsFixedSizeBuffer: false } fieldSymbol)
                {
                    continue;
                }

                // Skip fields of not accessible types (the analyzer will handle this)
                if (!fieldSymbol.Type.IsAccessibleFromCompilationAssembly(compilation))
                {
                    continue;
                }

                // Try to get the name to use for the field and the accessor
                if (!TryGetFieldAccessorName(fieldSymbol, out string? fieldName, out string? unspeakableName))
                {
                    continue;
                }

                string typeName = fieldSymbol.Type.GetFullyQualifiedMetadataName();

                // The first item in each nested struct needs to be aligned to 16 bytes
                if (isFirstField && fieldPath.Length > 0)
                {
                    constantBufferSizeInBytes = AlignmentHelper.Pad(constantBufferSizeInBytes, 16);

                    isFirstField = false;
                }

                if (HlslKnownTypes.IsKnownHlslType(typeName))
                {
                    ImmutableArray<FieldPathPart> nestedFieldPath = fieldPath.Add(new FieldPathPart.Leaf(fieldName, unspeakableName));

                    fields.Add(GetHlslKnownTypeFieldInfo(nestedFieldPath, typeName, ref constantBufferSizeInBytes));
                }
                else if (fieldSymbol.Type.IsUnmanagedType)
                {
                    string nestedTypeName = fieldSymbol.Type.GetFullyQualifiedName(includeGlobal: true);
                    FieldPathPart fieldPathPart = new FieldPathPart.Nested(fieldName, unspeakableName, nestedTypeName);

                    // Custom struct type defined by the user
                    GetInfo(compilation, fieldSymbol.Type, fieldPath.Add(fieldPathPart), ref constantBufferSizeInBytes, fields);
                }
            }
        }

        using ImmutableArrayBuilder<FieldInfo> fieldBuilder = new();

        // Traverse all shader fields and gather info, and update the tracking offsets
        GetInfo(
            compilation,
            structDeclarationSymbol,
            ImmutableArray<FieldPathPart>.Empty,
            ref constantBufferSizeInBytes,
            fieldBuilder);

        fields = fieldBuilder.ToImmutable();
    }

    /// <summary>
    /// Tries to get the field name and the accessor name for a given field.
    /// </summary>
    /// <param name="fieldSymbol">The field symbol to analyze.</param>
    /// <param name="referencedName">The name of the field that's referenced in code by users.</param>
    /// <param name="unspeakableName">The real name of the field that should be accessed, when different.</param>
    /// <returns>Whether the field is supported and the necessary info could be retrieved.</returns>
    public static bool TryGetFieldAccessorName(
        IFieldSymbol fieldSymbol,
        [NotNullWhen(true)] out string? referencedName,
        out string? unspeakableName)
    {
        // If the field can be referenced by name, we can just access it directly
        if (fieldSymbol.CanBeReferencedByName)
        {
            referencedName = fieldSymbol.Name;
            unspeakableName = null;

            return true;
        }

        // If the field is a primary constructor capture field, get the original name
        if (PrimaryConstructorNameRegex.Match(fieldSymbol.Name) is { Success: true, Groups: [_, { Value: string parameterName }] })
        {
            referencedName = parameterName;
            unspeakableName = fieldSymbol.Name;

            return true;
        }

        // The field is not recognized, so just invalid
        referencedName = null;
        unspeakableName = null;

        return false;
    }

    /// <summary>
    /// Gets info on a given captured HLSL primitive type (either a scalar, a vector or a matrix).
    /// </summary>
    /// <param name="fieldPath">The current path of the field with respect to the shader instance.</param>
    /// <param name="typeName">The type name currently being read.</param>
    /// <param name="rawDataOffset">The current offset within the loaded data buffer.</param>
    private static FieldInfo GetHlslKnownTypeFieldInfo(
        ImmutableArray<FieldPathPart> fieldPath,
        string typeName,
        ref int rawDataOffset)
    {
        // For scalar, vector and linear matrix types, serialize the value normally.
        // Only the initial alignment needs to be considered, while data is packed.
        (int fieldSize, int fieldPack) = HlslKnownSizes.GetTypeInfo(typeName);

        // Check if the current type is a matrix type with more than one row. In this
        // case, each row is aligned as if it was a separate array, so the start of
        // each row needs to be at a multiple of 16 bytes (a float4 register).
        if (HlslKnownTypes.IsNonLinearMatrixType(typeName, out string? elementName, out int rows, out int columns))
        {
            using ImmutableArrayBuilder<int> builder = new();

            for (int j = 0; j < rows; j++)
            {
                int startingRawDataOffset = AlignmentHelper.Pad(rawDataOffset, 16);

                builder.Add(startingRawDataOffset);

                rawDataOffset = startingRawDataOffset + (fieldPack * columns);
            }

            return new FieldInfo.NonLinearMatrix(
                fieldPath,
                typeName,
                elementName!,
                rows,
                columns,
                builder.ToImmutable());
        }
        else
        {
            // Calculate the right offset with 16-bytes padding (HLSL constant buffer).
            // Since we're in a constant buffer, we need to both pad the starting offset
            // to be aligned to the packing size of the type, and also to align the initial
            // offset to ensure that values do not cross 16 bytes boundaries either.
            int startingRawDataOffset = AlignmentHelper.AlignToBoundary(
                AlignmentHelper.Pad(rawDataOffset, fieldPack),
                fieldSize,
                16);

            rawDataOffset = startingRawDataOffset + fieldSize;

            return new FieldInfo.Primitive(fieldPath, typeName, startingRawDataOffset);
        }
    }
}