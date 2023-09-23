using System;
using System.Collections.Immutable;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.SourceGeneration.Helpers;
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
    partial class InputTypes
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>InputTypes</c> property.
        /// </summary>
        /// <param name="inputTypes">The input types for the shader.</param>
        /// <param name="additionalTypes">Any additional <see cref="TypeDeclarationSyntax"/> instances needed by the generated code, if needed.</param>
        /// <returns>The resulting <see cref="MemberDeclarationSyntax"/> instance for the <c>InputTypes</c> property.</returns>
        public static PropertyDeclarationSyntax GetSyntax(ImmutableArray<uint> inputTypes, out TypeDeclarationSyntax[] additionalTypes)
        {
            ExpressionSyntax memoryExpression;

            // If there are no inputs, simply return a default expression.
            // Otherwise, declare the memory manager and access it.
            if (inputTypes.Length == 0)
            {
                memoryExpression = LiteralExpression(SyntaxKind.DefaultLiteralExpression, Token(SyntaxKind.DefaultKeyword));
                additionalTypes = Array.Empty<TypeDeclarationSyntax>();
            }
            else
            {
                // Create a ReadOnlyMemory<D2D1PixelShaderInputType> instance from the memory manager:
                //
                // InputTypesMemoryManager.Instance.Memory
                memoryExpression =
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("InputTypesMemoryManager"),
                            IdentifierName("Instance")),
                        IdentifierName("Memory"));

                additionalTypes = new TypeDeclarationSyntax[] { GetMemoryManagerDeclaration(inputTypes) };
            }

            // This code produces a method declaration as follows:
            //
            // readonly global::System.ReadOnlyMemory<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> global::ComputeSharp.D2D1.__Internals.ID2D1Shader.InputTypes => <MEMORY_EXPRESSION>;
            return
                PropertyDeclaration(
                    GenericName(Identifier("global::System.ReadOnlyMemory"))
                    .AddTypeArgumentListArguments(IdentifierName("global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType")),
                    Identifier(nameof(InputTypes)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(memoryExpression))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        /// <summary>
        /// Gets the memory manager declaration for the input type data.
        /// </summary>
        /// <param name="inputTypes">The input types for the shader.</param>
        /// <returns>The memory manager declaration for the input type data.</returns>
        private static TypeDeclarationSyntax GetMemoryManagerDeclaration(ImmutableArray<uint> inputTypes)
        {
            // Create the MemoryManager<T> declaration:
            //
            // /// <summary>
            // /// A <see cref="global::System.Buffers.MemoryManager{T}"/> implementation to get the input types.
            // /// </summary>
            // [global::System.CodeDom.Compiler.GeneratedCode("...", "...")]
            // [global::System.Diagnostics.DebuggerNonUserCode]
            // [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            // file sealed InputTypesMemoryManager : global::System.Buffers.MemoryManager<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType>
            // {
            // }
            TypeDeclarationSyntax typeDeclaration =
                ClassDeclaration("InputTypesMemoryManager")
                .AddModifiers(Token(SyntaxKind.FileKeyword), Token(SyntaxKind.SealedKeyword))
                .AddBaseListTypes(SimpleBaseType(
                    GenericName(Identifier("global::System.Buffers.MemoryManager"))
                    .AddTypeArgumentListArguments(IdentifierName("global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType"))))
                .AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).FullName))),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).Assembly.GetName().Version.ToString())))))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage")))))
                .WithLeadingTrivia(
                    Comment("/// <summary>"),
                    Comment("""/// A <see cref="global::System.Buffers.MemoryManager{T}"/> implementation to get the input types."""),
                    Comment("/// </summary>"));

            using ImmutableArrayBuilder<MemberDeclarationSyntax> memberDeclarations = ImmutableArrayBuilder<MemberDeclarationSyntax>.Rent();

            // Declare the singleton property to get the memory manager:
            //
            // /// <summary>The singleton <see cref="InputTypesMemoryManager"/> instance to use.</summary>
            // public static readonly InputTypesMemoryManager Instance = new();
            memberDeclarations.Add(
                FieldDeclaration(
                    VariableDeclaration(IdentifierName("InputTypesMemoryManager"))
                    .AddVariables(
                        VariableDeclarator(Identifier("Instance"))
                        .WithInitializer(EqualsValueClause(ImplicitObjectCreationExpression()))))
                .AddModifiers(
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.StaticKeyword),
                    Token(SyntaxKind.ReadOnlyKeyword))
                .WithLeadingTrivia(Comment("""/// <summary>The singleton <see cref="InputTypesMemoryManager"/> instance to use.</summary>""")));

            using (ImmutableArrayBuilder<ExpressionSyntax> inputTypeExpressions = ImmutableArrayBuilder<ExpressionSyntax>.Rent())
            {
                // Build the sequence of expressions for all input types
                foreach (uint inputType in inputTypes)
                {
                    inputTypeExpressions.Add(
                         MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType"),
                                IdentifierName(inputType == 0 ? "Simple" : "Complex")));
                }

                // Construct the RVA span property:
                //
                // /// <summary>The RVA data with the input type info.</summary>
                // private static global::System.ReadOnlySpan<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> Data => new[] { <INPUT_TYPES> };
                memberDeclarations.Add(
                    PropertyDeclaration(
                        GenericName(Identifier("global::System.ReadOnlySpan"))
                        .AddTypeArgumentListArguments(IdentifierName("global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType")),
                        Identifier("Data"))
                    .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.StaticKeyword))
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            ImplicitArrayCreationExpression(
                                InitializerExpression(
                                    SyntaxKind.ArrayInitializerExpression,
                                    SeparatedList(inputTypeExpressions.ToArray())))))
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                    .WithLeadingTrivia(Comment("/// <summary>The RVA data with the input type info.</summary>")));
            }

            // Add the GetSpan() method:
            //
            // /// <inheritdoc/>
            // public override unsafe global::System.Span<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> GetSpan
            // {
            //     return new(global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::System.Runtime.InteropServices.MemoryMarshal(Data)), Data.Length);
            // }
            memberDeclarations.Add(
                MethodDeclaration(
                    GenericName(Identifier("global::System.Span"))
                    .AddTypeArgumentListArguments(IdentifierName("global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType")),
                    Identifier("GetSpan"))
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.OverrideKeyword), Token(SyntaxKind.UnsafeKeyword))
                .AddBodyStatements(
                    ReturnStatement(
                        ImplicitObjectCreationExpression()
                        .AddArgumentListArguments(
                            Argument(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("global::System.Runtime.CompilerServices.Unsafe"),
                                        IdentifierName("AsPointer")))
                                .AddArgumentListArguments(
                                    Argument(
                                        InvocationExpression(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName("global::System.Runtime.InteropServices.MemoryMarshal"),
                                                IdentifierName("GetReference")))
                                        .AddArgumentListArguments(Argument(IdentifierName("Data"))))
                                    .WithRefOrOutKeyword(Token(SyntaxKind.RefKeyword)))),
                            Argument(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("Data"),
                                    IdentifierName("Length"))))))
                .WithLeadingTrivia(Comment("/// <inheritdoc/>")));

            // Override the Memory<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> property:
            //
            // /// <inheritdoc/>
            // public override global::System.Memory<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> Memory
            // {
            //     [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            //     get => CreateMemory(Data.Length);
            // }
            memberDeclarations.Add(
                PropertyDeclaration(
                    GenericName(Identifier("global::System.Memory"))
                    .AddTypeArgumentListArguments(IdentifierName("global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType")),
                    Identifier("Memory"))
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.OverrideKeyword))
                .AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .AddAttributeLists(
                        AttributeList(
                            SingletonSeparatedList(
                                Attribute(IdentifierName("global::System.Runtime.CompilerServices.MethodImpl"))
                                .AddArgumentListArguments(
                                    AttributeArgument(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("global::System.Runtime.CompilerServices.MethodImplOptions"),
                                            IdentifierName("AggressiveInlining")))))))
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            InvocationExpression(IdentifierName("CreateMemory"))
                            .AddArgumentListArguments(
                                Argument(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("Data"),
                                        IdentifierName("Length"))))))
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)))
                .WithLeadingTrivia(Comment("/// <inheritdoc/>")));

            // Add the Pin(int elementIndex) method:
            //
            // /// <inheritdoc/>
            // public override unsafe global::System.Buffers.MemoryHandle Pin(int elementIndex)
            // {
            //     return new(Unsafe.AsPointer(ref Unsafe.AsRef(in Data[elementIndex])), pinnable: this);
            // }
            memberDeclarations.Add(
                MethodDeclaration(IdentifierName("global::System.Buffers.MemoryHandle"), Identifier("Pin"))
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.OverrideKeyword), Token(SyntaxKind.UnsafeKeyword))
                .AddParameterListParameters(Parameter(Identifier("elementIndex")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))))
                .AddBodyStatements(
                    ReturnStatement(
                        ImplicitObjectCreationExpression()
                        .AddArgumentListArguments(
                            Argument(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("global::System.Runtime.CompilerServices.Unsafe"),
                                        IdentifierName("AsPointer")))
                                .AddArgumentListArguments(
                                    Argument(
                                        InvocationExpression(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName("global::System.Runtime.CompilerServices.Unsafe"),
                                                IdentifierName("AsRef")))
                                        .AddArgumentListArguments(
                                            Argument(
                                                ElementAccessExpression(IdentifierName("Data"))
                                                .AddArgumentListArguments(Argument(IdentifierName("elementIndex"))))
                                            .WithRefOrOutKeyword(Token(SyntaxKind.InKeyword))))
                                    .WithRefOrOutKeyword(Token(SyntaxKind.RefKeyword)))),
                            Argument(ThisExpression()).WithNameColon(NameColon(IdentifierName("pinnable"))))))
                .WithLeadingTrivia(Comment("/// <inheritdoc/>")));

            // Add the empty Unpin() method
            memberDeclarations.Add(
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier("Unpin"))
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.OverrideKeyword))
                .WithBody(Block())
                .WithLeadingTrivia(Comment("/// <inheritdoc/>")));

            // Add the empty Dispose(bool disposing) method
            memberDeclarations.Add(
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier("Dispose"))
                .AddModifiers(Token(SyntaxKind.ProtectedKeyword), Token(SyntaxKind.OverrideKeyword))
                .AddParameterListParameters(
                    Parameter(Identifier("disposing")).WithType(PredefinedType(Token(SyntaxKind.BoolKeyword))))
                .WithBody(Block())
                .WithLeadingTrivia(Comment("/// <inheritdoc/>")));

            return typeDeclaration.AddMembers(memberDeclarations.ToArray());
        }
    }
}