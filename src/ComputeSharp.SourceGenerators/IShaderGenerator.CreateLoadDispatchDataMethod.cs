using System;
using System.Collections.Generic;
using System.Linq;
using ComputeSharp.Core.Helpers;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators
{
    /// <inheritdoc/>
    public sealed partial class IShaderGenerator
    {
        /// <inheritdoc/>
        private static partial MethodDeclarationSyntax CreateLoadDispatchDataMethod(
            GeneratorExecutionContext context,
            INamedTypeSymbol structDeclarationSymbol,
            out IEnumerable<string> discoveredResources,
            out int root32BitConstantsCount)
        {
            // This code produces a method declaration as follows:
            //
            // public readonly void LoadDispatchData<TDataLoader>(ref TDataLoader loader, GraphicsDevice device, int x, int y, int z)
            //     where TDataLoader : struct, IDispatchDataLoader
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("LoadDispatchData"))
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TDataLoader")))
                .AddParameterListParameters(
                    Parameter(Identifier("loader"))
                        .AddModifiers(Token(SyntaxKind.RefKeyword))
                        .WithType(IdentifierName("TDataLoader")),
                    Parameter(Identifier("device")).WithType(IdentifierName("GraphicsDevice")),
                    Parameter(Identifier("x")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("y")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("z")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))))
                .AddConstraintClauses(
                    TypeParameterConstraintClause(IdentifierName("TDataLoader"))
                    .AddConstraints(
                        ClassOrStructConstraint(SyntaxKind.StructConstraint),
                        TypeConstraint(IdentifierName("IDispatchDataLoader"))))
                .WithBody(Block(GetDispatchDataLoadingStatements(context, structDeclarationSymbol, out discoveredResources, out root32BitConstantsCount)));
        }

        /// <summary>
        /// Gets a sequence of statements to load the dispatch data for a given shader
        /// </summary>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="discoveredResources">The sequence of discovered resources in the current shader type.</param>
        /// <param name="root32BitConstantsCount">The total number of 32 bit root constants being loaded for the current shader type.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load shader dispatch data.</returns>
        private static IEnumerable<StatementSyntax> GetDispatchDataLoadingStatements(GeneratorExecutionContext context, INamedTypeSymbol structDeclarationSymbol, out IEnumerable<string> discoveredResources, out int root32BitConstantsCount)
        {
            List<StatementSyntax> statements = new();
            List<string> resources = new();

            discoveredResources = resources;

            var pixelShaderSymbol = structDeclarationSymbol.AllInterfaces.FirstOrDefault(static interfaceSymbol => interfaceSymbol is { IsGenericType: true, Name: nameof(IPixelShader<byte>) });
            var isComputeShader = pixelShaderSymbol is null;
            int
                resourceOffset = 0,
                rawDataOffset = sizeof(int) * (isComputeShader ? 3 : 2);

            // Append the statements for the dispatch ranges:
            //
            // span0[0] = (uint)x;
            // span0[1] = (uint)y;
            // span0[2] = (uint)z;
            statements.Add(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ElementAccessExpression(IdentifierName("span0"))
                            .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)))),
                        CastExpression(PredefinedType(Token(SyntaxKind.UIntKeyword)), IdentifierName("x")))));

            statements.Add(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ElementAccessExpression(IdentifierName("span0"))
                            .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))),
                        CastExpression(PredefinedType(Token(SyntaxKind.UIntKeyword)), IdentifierName("y")))));

            if (isComputeShader)
            {
                statements.Add(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ElementAccessExpression(IdentifierName("span0"))
                            .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(2)))),
                        CastExpression(PredefinedType(Token(SyntaxKind.UIntKeyword)), IdentifierName("z")))));
            }

            // Append the statements to load all fields (both resources and other captured values)
            AppendFields(structDeclarationSymbol, Array.Empty<string>(), ref resourceOffset, ref rawDataOffset, resources, statements);

            // After all the captured fields have been processed, ansure the reported byte size for
            // the local variables is padded to a multiple of a 32 bit value. This is necessary to
            // enable loading all the dispatch data after reinterpreting it to a sequence of values
            // of size 32 bits (via SetComputeRoot32BitConstants), without reading out of bounds.
            rawDataOffset = AlignmentHelper.Pad(rawDataOffset, sizeof(int));
            root32BitConstantsCount = rawDataOffset / sizeof(int);

            // A shader root signature has a maximum size of 64 DWORDs, so 256 bytes.
            // Loaded values in the root signature have the following costs:
            //  - Root constants cost 1 DWORD each, since they are 32-bit values.
            //  - Descriptor tables cost 1 DWORD each.
            //  - Root descriptors (64-bit GPU virtual addresses) cost 2 DWORDs each.
            // So here we check whether the current signature respects that constraint,
            // and emit a build error otherwise. For more info on this, see the docs here:
            // https://docs.microsoft.com/windows/win32/direct3d12/root-signature-limits.
            int rootSignatureDwordSize = root32BitConstantsCount + resourceOffset;

            if (rootSignatureDwordSize > 64)
            {
                context.ReportDiagnostic(ShaderDispatchDataSizeExceeded, structDeclarationSymbol, structDeclarationSymbol);
            }

            // Span<uint> span0 = stackalloc uint[<VARIABLES>];
            statements.Insert(0,
                LocalDeclarationStatement(
                    VariableDeclaration(
                        GenericName(Identifier("Span"))
                        .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.UIntKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("span0"))
                        .WithInitializer(EqualsValueClause(
                            StackAllocArrayCreationExpression(
                                ArrayType(PredefinedType(Token(SyntaxKind.UIntKeyword)))
                                .AddRankSpecifiers(
                                    ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(root32BitConstantsCount)))))))))));

            // ref uint r0 = ref span0[0];
            statements.Insert(1,
                LocalDeclarationStatement(
                    VariableDeclaration(RefType(PredefinedType(Token(SyntaxKind.UIntKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("r0"))
                        .WithInitializer(EqualsValueClause(
                            RefExpression(
                                ElementAccessExpression(IdentifierName("span0"))
                                .AddArgumentListArguments(Argument(
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(0))))))))));

            // loader.LoadCapturedValues(span0);
            statements.Add(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("loader"),
                            IdentifierName("LoadCapturedValues")))
                    .AddArgumentListArguments(Argument(IdentifierName("span0")))));

            if (resourceOffset > 0)
            {
                // Span<ulong> span1 = stackalloc ulong[<RESOURCES>];
                statements.Insert(1,
                    LocalDeclarationStatement(
                        VariableDeclaration(
                            GenericName(Identifier("Span"))
                            .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ULongKeyword))))
                        .AddVariables(
                            VariableDeclarator(Identifier("span1"))
                            .WithInitializer(EqualsValueClause(
                                StackAllocArrayCreationExpression(
                                    ArrayType(PredefinedType(Token(SyntaxKind.ULongKeyword)))
                                    .AddRankSpecifiers(
                                        ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(resourceOffset)))))))))));

                // ref ulong r1 = ref span1[0];
                statements.Insert(3,
                    LocalDeclarationStatement(
                        VariableDeclaration(RefType(PredefinedType(Token(SyntaxKind.ULongKeyword))))
                        .AddVariables(
                            VariableDeclarator(Identifier("r1"))
                            .WithInitializer(EqualsValueClause(
                                RefExpression(
                                    ElementAccessExpression(IdentifierName("span1"))
                                    .AddArgumentListArguments(Argument(
                                        LiteralExpression(
                                            SyntaxKind.NumericLiteralExpression,
                                            Literal(0))))))))));

                // loader.LoadCapturedResources(span1);
                statements.Add(
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("loader"),
                                IdentifierName("LoadCapturedResources")))
                        .AddArgumentListArguments(Argument(IdentifierName("span1")))));
            }

            return statements;
        }

        /// <summary>
        /// Explores a given type hierarchy and generates statements to load fields.
        /// </summary>
        /// <param name="currentTypeSymbol">The current type being explored.</param>
        /// <param name="fieldPath">The current path of the field with respect to the shader instance.</param>
        /// <param name="resourceOffset">The current offset in the root table of loaded resources.</param>
        /// <param name="rawDataOffset">The current offset within the loaded data buffer.</param>
        /// <param name="resources">The target list of resources in the current shader type.</param>
        /// <param name="statements">The target list of statements being generated.</param>
        private static void AppendFields(ITypeSymbol currentTypeSymbol, IReadOnlyCollection<string> fieldPath, ref int resourceOffset, ref int rawDataOffset, ICollection<string> resources, ICollection<StatementSyntax> statements)
        {
            bool isFirstField = true;

            foreach (
               IFieldSymbol fieldSymbol in
               from fieldSymbol in currentTypeSymbol.GetMembers().OfType<IFieldSymbol>()
               where fieldSymbol.Type is INamedTypeSymbol { IsStatic: false } &&
                     !fieldSymbol.IsConst && !fieldSymbol.IsStatic && !fieldSymbol.IsFixedSizeBuffer
               select fieldSymbol)
            {
                string
                    fieldName = fieldSymbol.Name,
                    typeName = fieldSymbol.Type.GetFullMetadataName();

                // Disambiguates the name of target fields against the current input parameters
                if (fieldName is "loader" or "device" or "x" or "y" or "z")
                {
                    fieldName = $"this.{fieldName}";
                }

                // The first item in each nested struct needs to be aligned to 16 bytes
                if (isFirstField && fieldPath.Count > 0)
                {
                    rawDataOffset = AlignmentHelper.Pad(rawDataOffset, 16);

                    isFirstField = false;
                }

                if (HlslKnownTypes.IsTypedResourceType(typeName))
                {
                    resources.Add(typeName);

                    statements.Add(ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            ParseExpression($"Unsafe.Add(ref r1, {resourceOffset++})"),
                            ParseExpression($"GraphicsResourceHelper.ValidateAndGetGpuDescriptorHandle({fieldName}, device)"))));
                }
                else if (HlslKnownTypes.IsKnownHlslType(typeName))
                {
                    AppendHlslKnownTypeField(fieldPath.Concat(new[] { fieldName }), typeName, ref rawDataOffset, statements);
                }
                else if (fieldSymbol.Type.IsUnmanagedType)
                {
                    // Custom struct type defined by the user
                    AppendFields(fieldSymbol.Type, fieldPath.Concat(new[] { fieldName }).ToArray(), ref resourceOffset, ref rawDataOffset, resources, statements);
                }
            }
        }

        /// <summary>
        /// Generates a loading statement for a known HLSL primitive type field (scalar, vector or matrix).
        /// </summary>
        /// <param name="fieldPath">The current path of the field with respect to the shader instance.</param>
        /// <param name="typeName">The type name currently being read.</param>
        /// <param name="rawDataOffset">The current offset within the loaded data buffer.</param>
        /// <param name="statements">The target list of statements being generated.</param>
        private static void AppendHlslKnownTypeField(IEnumerable<string> fieldPath, string typeName, ref int rawDataOffset, ICollection<StatementSyntax> statements)
        {
            // For scalar, vector and linear matrix types, serialize the value normally.
            // Only the initial alignment needs to be considered, while data is packed.
            var (fieldSize, fieldPack) = HlslKnownSizes.GetTypeInfo(typeName);

            // Check if the current type is a matrix type with more than one row. In this
            // case, each row is aligned as if it was a separate array, so the start of
            // each row needs to be at a multiple of 16 bytes (a float4 register).
            if (HlslKnownTypes.IsNonLinearMatrixType(typeName, out string? elementName, out int rows, out int columns))
            {
                string
                    rowTypeName = $"ComputeSharp.{elementName}{columns}",
                    rowLocalName = $"__{string.Join("_", fieldPath)}__row0";

                statements.Add(ParseStatement($"ref {rowTypeName} {rowLocalName} = ref Unsafe.As<{typeName}, {rowTypeName}>(ref Unsafe.AsRef(in {string.Join(".", fieldPath)}));"));

                // Generate the loading code for each individual row, with proper alignment.
                // This will result in the following (assuming Float2x3 m):
                //
                // ref Float3 __m__row0 = ref Unsafe.As<Float2x3, Float3>(ref Unsafe.AsRef(in m));
                // Unsafe.As<uint, Float3>(ref Unsafe.AddByteOffset(ref r0, (nint)rawDataOffset)) =, Unsafe.Add(ref __m__row0, 0);
                // Unsafe.As<uint, Float3>(ref Unsafe.AddByteOffset(ref r0, (nint)(rawDataOffset + 16))) = Unsafe.Add(ref __m__row0, 1);
                for (int j = 0; j < rows; j++)
                {
                    rawDataOffset = AlignmentHelper.Pad(rawDataOffset, 16);

                    statements.Add(ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            ParseExpression($"Unsafe.As<uint, {rowTypeName}>(ref Unsafe.AddByteOffset(ref r0, (nint){rawDataOffset}))"),
                            ParseExpression($"Unsafe.Add(ref {rowLocalName}, {j})"))));

                    rawDataOffset += fieldPack * columns;
                }
            }
            else
            {
                // Calculate the right offset with 16-bytes padding (HLSL constant buffer).
                // Since we're in a constant buffer, we need to both pad the starting offset
                // to be aligned to the packing size of the type, and also to align the initial
                // offset to ensure that values do not cross 16 bytes boundaries either.
                rawDataOffset = AlignmentHelper.AlignToBoundary(
                    AlignmentHelper.Pad(rawDataOffset, fieldPack),
                    fieldSize,
                    16);

                statements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ParseExpression($"Unsafe.As<uint, {typeName}>(ref Unsafe.AddByteOffset(ref r0, (nint){rawDataOffset}))"),
                        ParseExpression($"{string.Join(".", fieldPath)}"))));

                rawDataOffset += fieldSize;
            }
        }
    }
}

