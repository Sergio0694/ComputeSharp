using System;
using System.Collections.Immutable;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadDispatchData
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.
        /// </summary>
        /// <param name="hierarchyInfo">The hiararchy info of the shader type.</param>
        /// <param name="dispatchInfo">The dispatch info gathered for the current shader.</param>
        /// <param name="additionalTypes">Any additional <see cref="TypeDeclarationSyntax"/> instances needed by the generated code, if needed.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(HierarchyInfo hierarchyInfo, DispatchDataInfo dispatchInfo, out TypeDeclarationSyntax[] additionalTypes)
        {
            // Declare the mapping constant buffer type, if needed (ie. if the shader has at least one field)
            if (dispatchInfo.FieldInfos.Length == 0)
            {
                additionalTypes = Array.Empty<TypeDeclarationSyntax>();
            }
            else
            {
                additionalTypes = new[] { GetConstantBufferDeclaration(hierarchyInfo, dispatchInfo.FieldInfos, dispatchInfo.ConstantBufferSizeInBytes) };
            }

            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.LoadDispatchData<TLoader>(ref TLoader loader)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(nameof(LoadDispatchData)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(Parameter(Identifier("loader")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(IdentifierName("TLoader")))
                .WithBody(Block(GetDispatchDataLoadingStatements(dispatchInfo.FieldInfos, dispatchInfo.ConstantBufferSizeInBytes)));
        }

        /// <summary>
        /// Gets a type definition to map the constant buffer of a given shader type.
        /// </summary>
        /// <param name="hierarchyInfo">The hiararchy info of the shader type.</param>
        /// <param name="fieldInfos">The array of <see cref="FieldInfo"/> values for all captured fields.</param>
        /// <param name="constantBufferSizeInBytes">The size of the shader constant buffer.</param>
        /// <returns>The <see cref="TypeDeclarationSyntax"/> object for the mapped constant buffer for the current shader type.</returns>
        private static TypeDeclarationSyntax GetConstantBufferDeclaration(HierarchyInfo hierarchyInfo, ImmutableArray<FieldInfo> fieldInfos, int constantBufferSizeInBytes)
        {
            string fullyQualifiedTypeName = hierarchyInfo.GetFullyQualifiedTypeName();

            using ImmutableArrayBuilder<FieldDeclarationSyntax> fieldDeclarations = ImmutableArrayBuilder<FieldDeclarationSyntax>.Rent();

            // Appends a new field declaration for a constant buffer field:
            //
            // <COMMENT>
            // [global::System.Runtime.InteropServices.FieldOffset(<FIELD_OFFSET>)]
            // public <FIELD_TYPE> <FIELD_NAME>;
            void AppendFieldDeclaration(
                string comment,
                TypeSyntax typeIdentifier,
                string identifierName,
                int fieldOffset)
            {
                fieldDeclarations.Add(
                    FieldDeclaration(
                        VariableDeclaration(typeIdentifier)
                        .AddVariables(VariableDeclarator(Identifier(identifierName))))
                    .AddAttributeLists(AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("global::System.Runtime.InteropServices.FieldOffset"))
                        .AddArgumentListArguments(AttributeArgument(
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(fieldOffset)))))))
                    .AddModifiers(Token(SyntaxKind.PublicKeyword))
                    .WithLeadingTrivia(Comment(comment)));
            }

            // Declare fields for every mapped item from the shader layout
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                switch (fieldInfo)
                {
                    case FieldInfo.Primitive { TypeName: "System.Boolean" } primitive:

                        // Append a field as a global::ComputeSharp.Bool value (will use the implicit conversion from bool values)
                        AppendFieldDeclaration(
                            comment: $"""/// <inheritdoc cref="{fullyQualifiedTypeName}.{string.Join(".", primitive.FieldPath)}"/>""",
                            typeIdentifier: IdentifierName("global::ComputeSharp.Bool"),
                            identifierName: string.Join("_", primitive.FieldPath),
                            fieldOffset: primitive.Offset);
                        break;
                    case FieldInfo.Primitive primitive:

                        // Append primitive fields of other types with their mapped names
                        AppendFieldDeclaration(
                            comment: $"""/// <inheritdoc cref="{fullyQualifiedTypeName}.{string.Join(".", primitive.FieldPath)}"/>""",
                            typeIdentifier: IdentifierName(HlslKnownTypes.GetMappedName(primitive.TypeName)),
                            identifierName: string.Join("_", primitive.FieldPath),
                            fieldOffset: primitive.Offset);
                        break;

                    case FieldInfo.NonLinearMatrix matrix:
                        string rowTypeName = HlslKnownTypes.GetMappedName($"ComputeSharp.{matrix.ElementName}{matrix.Columns}");
                        string fieldNamePrefix = string.Join("_", matrix.FieldPath);

                        // Declare a field for every row of the matrix type
                        for (int j = 0; j < matrix.Rows; j++)
                        {
                            AppendFieldDeclaration(
                                comment: $"""/// <summary>Row {j} of <see cref="{fullyQualifiedTypeName}.{string.Join(".", matrix.FieldPath)}"/>.</summary>""",
                                typeIdentifier: IdentifierName(rowTypeName),
                                identifierName: $"{fieldNamePrefix}_{j}",
                                fieldOffset: matrix.Offsets[j]);
                        }

                        break;
                }
            }

            // Create the constant buffer type:
            //
            // /// <summary>
            // /// A type representing the constant buffer native layout for <see cref="<SHADER_TYPE"/>.
            // /// </summary>
            // [global::System.CodeDom.Compiler.GeneratedCode("...", "...")]
            // [global::System.Diagnostics.DebuggerNonUserCode]
            // [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            // [global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = <CONSTANT_BUFFER_SIZE>)]
            // file struct ConstantBuffer
            // {
            //     <FIELD_DECLARATIONS>
            // }
            return
                StructDeclaration("ConstantBuffer")
                .AddModifiers(Token(SyntaxKind.FileKeyword))
                .AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).FullName))),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).Assembly.GetName().Version.ToString())))))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage")))),
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("global::System.Runtime.InteropServices.StructLayout")).AddArgumentListArguments(
                            AttributeArgument(MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("global::System.Runtime.InteropServices.LayoutKind"),
                                IdentifierName("Explicit"))),
                            AttributeArgument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(constantBufferSizeInBytes)))
                            .WithNameEquals(NameEquals(IdentifierName("Size")))))))
                .AddMembers(fieldDeclarations.ToArray())
                .WithLeadingTrivia(
                    Comment("/// <summary>"),
                    Comment($"""/// A type representing the constant buffer native layout for <see cref="{fullyQualifiedTypeName}"/>."""),
                    Comment("/// </summary>"));
        }

        /// <summary>
        /// Gets a sequence of statements to load the dispatch data for a given shader.
        /// </summary>
        /// <param name="fieldInfos">The array of <see cref="FieldInfo"/> values for all captured fields.</param>
        /// <param name="constantBufferSizeInBytes">The size of the shader constant buffer.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load the shader dispatch data.</returns>
        private static ImmutableArray<StatementSyntax> GetDispatchDataLoadingStatements(ImmutableArray<FieldInfo> fieldInfos, int constantBufferSizeInBytes)
        {
            // If there are no fields, just load an empty buffer
            if (fieldInfos.IsEmpty)
            {
                // loader.LoadConstantBuffer(default);
                return
                    ImmutableArray.Create<StatementSyntax>(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("loader"),
                                    IdentifierName("LoadConstantBuffer")))
                            .AddArgumentListArguments(Argument(
                                LiteralExpression(
                                    SyntaxKind.DefaultLiteralExpression,
                                    Token(SyntaxKind.DefaultKeyword))))));
            }

            using ImmutableArrayBuilder<StatementSyntax> statements = ImmutableArrayBuilder<StatementSyntax>.Rent();

            // global::System.Span<byte> data = stackalloc byte[<CONSTANT_BUFFER_SIZE>];
            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(
                        GenericName(Identifier("global::System.Span"))
                        .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("data"))
                        .WithInitializer(EqualsValueClause(
                            StackAllocArrayCreationExpression(
                                ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                                .AddRankSpecifiers(
                                    ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(constantBufferSizeInBytes)))))))))));

            // ref byte r0 = ref data[0];
            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(RefType(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier("r0"))
                        .WithInitializer(EqualsValueClause(
                            RefExpression(
                                ElementAccessExpression(IdentifierName("data"))
                                .AddArgumentListArguments(Argument(
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(0))))))))));

            // Generate loading statements for each captured field
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                switch (fieldInfo)
                {
                    case FieldInfo.Primitive { TypeName: "System.Boolean" } primitive:

                        // Read a boolean value and cast it to Bool first, which will apply the correct size expansion. This will generate the following:
                        //
                        // global::System.Runtime.CompilerServices.Unsafe.As<byte, global::ComputeSharp.Bool>(
                        //     ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET>)) = (global::ComputeSharp.Bool)<FIELD_PATH>
                        statements.Add(ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, global::ComputeSharp.Bool>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){primitive.Offset}))"),
                                ParseExpression($"(global::ComputeSharp.Bool){string.Join(".", primitive.FieldPath)}"))));
                        break;
                    case FieldInfo.Primitive primitive:

                        // Read a primitive value and serialize it into the target buffer. This will generate:
                        //
                        // global::System.Runtime.CompilerServices.Unsafe.As<byte, global::<TYPE_NAME>>(
                        //     ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)<OFFSET>)) = <FIELD_PATH>
                        statements.Add(ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, global::{primitive.TypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){primitive.Offset}))"),
                                ParseExpression($"{string.Join(".", primitive.FieldPath)}"))));
                        break;

                    case FieldInfo.NonLinearMatrix matrix:
                        string rowTypeName = $"global::ComputeSharp.{matrix.ElementName}{matrix.Columns}";
                        string rowLocalName = $"__{string.Join("_", matrix.FieldPath)}__row0";

                        // Declare a local to index into individual rows. This will generate:
                        //
                        // ref <ROW_TYPE> <ROW_NAME> = ref global::System.Runtime.CompilerServices.Unsafe.As<global::<TYPE_NAME>, <ROW_TYPE_NAME>>(
                        //     ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in <FIELD_PATH>));
                        statements.Add(ParseStatement($"ref {rowTypeName} {rowLocalName} = ref global::System.Runtime.CompilerServices.Unsafe.As<global::{matrix.TypeName}, {rowTypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in {string.Join(".", matrix.FieldPath)}));"));

                        // Generate the loading code for each individual row, with proper alignment.
                        // This will result in the following (assuming Float2x3 m):
                        //
                        // ref global::ComputeSharp.Float3 __m__row0 = ref global::System.Runtime.CompilerServices.Unsafe.As<global::ComputeSharp.Float2x3, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in m));
                        // global::System.Runtime.CompilerServices.Unsafe.As<byte, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)rawDataOffset)) = global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 0);
                        // global::System.Runtime.CompilerServices.Unsafe.As<byte, global::ComputeSharp.Float3>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint)(rawDataOffset + 16))) = global::System.Runtime.CompilerServices.Unsafe.Add(ref __m__row0, 1);
                        for (int j = 0; j < matrix.Rows; j++)
                        {
                            statements.Add(ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.As<byte, {rowTypeName}>(ref global::System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref r0, (nint){matrix.Offsets[j]}))"),
                                    ParseExpression($"global::System.Runtime.CompilerServices.Unsafe.Add(ref {rowLocalName}, {j})"))));
                        }

                        break;
                }
            }

            // loader.LoadConstantBuffer(data);
            statements.Add(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("loader"),
                            IdentifierName("LoadConstantBuffer")))
                    .AddArgumentListArguments(Argument(IdentifierName("data")))));

            return statements.ToImmutable();
        }
    }
}