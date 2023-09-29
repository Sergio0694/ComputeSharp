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
        /// <param name="fields">The description on shader instance fields.</param>
        /// <param name="constantBufferSizeInBytes">The size of the shader constant buffer.</param>
        /// <param name="additionalTypes">Any additional <see cref="TypeDeclarationSyntax"/> instances needed by the generated code, if needed.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(
            HierarchyInfo hierarchyInfo,
            EquatableArray<FieldInfo> fields,
            int constantBufferSizeInBytes,
            out TypeDeclarationSyntax[] additionalTypes)
        {
            // Declare the mapping constant buffer type, if needed (ie. if the shader has at least one field)
            if (fields.Length == 0)
            {
                additionalTypes = Array.Empty<TypeDeclarationSyntax>();
            }
            else
            {
                additionalTypes = new[] { GetConstantBufferDeclaration(hierarchyInfo, fields, constantBufferSizeInBytes) };
            }

            // This code produces a method declaration as follows:
            //
            // readonly unsafe void global::ComputeSharp.D2D1.__Internals.ID2D1Shader.LoadDispatchData<TLoader>(ref TLoader loader)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(nameof(LoadDispatchData)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword), Token(SyntaxKind.UnsafeKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(Parameter(Identifier("loader")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(IdentifierName("TLoader")))
                .WithBody(Block(GetDispatchDataLoadingStatements(fields, constantBufferSizeInBytes)));
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
            // If there are no fields, just load an empty buffer:
            //
            // loader.LoadConstantBuffer(default);
            if (fieldInfos.IsEmpty)
            {
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

            // ConstantBuffer buffer;
            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(IdentifierName("ConstantBuffer"))
                    .AddVariables(VariableDeclarator(Identifier("buffer")))));

            // Generate loading statements for each captured field
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                switch (fieldInfo)
                {
                    case FieldInfo.Primitive primitive:

                        // Assign a primitive value:
                        //
                        // buffer.<CONSTANT_BUFFER_PATH> = this.<FIELD_PATH>;
                        statements.Add(
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("buffer"),
                                        IdentifierName(string.Join("_", primitive.FieldPath))),
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        ThisExpression(),
                                        IdentifierName(string.Join(".", primitive.FieldPath))))));
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
                            statements.Add(
                                ExpressionStatement(
                                    AssignmentExpression(
                                        SyntaxKind.SimpleAssignmentExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("buffer"),
                                            IdentifierName($"{fieldNamePrefix}_{j}")),
                                        ElementAccessExpression(
                                            MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            ThisExpression(),
                                            IdentifierName(fieldPath)))
                                        .AddArgumentListArguments(
                                            Argument(LiteralExpression(
                                                SyntaxKind.NumericLiteralExpression,
                                                Literal(j)))))));
                        }

                        break;
                }
            }

            // loader.LoadConstantBuffer(new global::System.ReadOnlySpan<byte>(&buffer, sizeof(ConstantBuffer)));
            statements.Add(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("loader"),
                            IdentifierName("LoadConstantBuffer")))
                    .AddArgumentListArguments(Argument(
                        ObjectCreationExpression(
                            GenericName(Identifier("global::System.ReadOnlySpan"))
                            .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                        .AddArgumentListArguments(
                            Argument(
                                PrefixUnaryExpression(
                                    SyntaxKind.AddressOfExpression,
                                    IdentifierName("buffer"))),
                            Argument(SizeOfExpression(IdentifierName("ConstantBuffer"))))))));

            return statements.ToImmutable();
        }
    }
}