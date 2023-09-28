using System;
using System.Globalization;
using System.Linq;
using System.Text;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadBytecode
    {
        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>ShaderProfile</c> property.
        /// </summary>
        /// <param name="shaderProfile">The input shader profile.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>ShaderProfile</c> property.</returns>
        public static PropertyDeclarationSyntax GetShaderProfileSyntax(D2D1ShaderProfile shaderProfile)
        {
            // This code produces a method declaration as follows:
            //
            // readonly ComputeSharp.D2D1.D2D1ShaderProfile global::ComputeSharp.D2D1.__Internals.ID2D1Shader.ShaderProfile => <SHADER_PROFILE>;
            return
                PropertyDeclaration(IdentifierName("ComputeSharp.D2D1.D2D1ShaderProfile"), Identifier(nameof(ID2D1Shader.ShaderProfile)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("global::ComputeSharp.D2D1.D2D1ShaderProfile"),
                        IdentifierName(shaderProfile.ToString(CultureInfo.InvariantCulture)))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>CompileOptions</c> property.
        /// </summary>
        /// <param name="compileOptions">The input compile options.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>CompileOptions</c> property.</returns>
        public static PropertyDeclarationSyntax GetCompileOptionsSyntax(D2D1CompileOptions compileOptions)
        {
            // Get a formatted representation of the compile options being used
            ExpressionSyntax compileOptionsExpression =
                ParseExpression(
                    compileOptions
                    .ToString(CultureInfo.InvariantCulture)
                    .Split(',')
                    .Select(static name => $"global::ComputeSharp.D2D1.D2D1CompileOptions.{name.Trim()}")
                    .Aggregate("", static (left, right) => left.Length > 0 ? $"{left} | {right}" : right));

            // This code produces a method declaration as follows:
            //
            // readonly ComputeSharp.D2D1.D2D1CompileOptions global::ComputeSharp.D2D1.__Internals.ID2D1Shader.CompileOptions => <COMPILE_OPTIONS_EXPRESSION>;
            return
                PropertyDeclaration(IdentifierName("ComputeSharp.D2D1.D2D1CompileOptions"), Identifier(nameof(ID2D1Shader.CompileOptions)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(compileOptionsExpression))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        /// <summary>
        /// Creates a <see cref="PropertyDeclarationSyntax"/> instance for the <c>HlslBytecode</c> property.
        /// </summary>
        /// <param name="bytecodeInfo">The input bytecode info.</param>
        /// <param name="fixup">An opaque <see cref="Func{TResult}"/> instance to transform the final tree into text.</param>
        /// <param name="additionalTypes">Any additional <see cref="TypeDeclarationSyntax"/> instances needed by the generated code, if needed.</param>
        /// <returns>The resulting <see cref="PropertyDeclarationSyntax"/> instance for the <c>HlslBytecode</c> property.</returns>
        public static PropertyDeclarationSyntax GetHlslBytecodeSyntax(
            HlslBytecodeInfo bytecodeInfo,
            out Func<SyntaxNode, SourceText> fixup,
            out TypeDeclarationSyntax[] additionalTypes)
        {
            ExpressionSyntax memoryExpression;

            // If there is no bytecode, simply return a default expression.
            // Otherwise, declare the memory manager and access it.
            if (bytecodeInfo is not HlslBytecodeInfo.Success success)
            {
                memoryExpression = LiteralExpression(SyntaxKind.DefaultLiteralExpression, Token(SyntaxKind.DefaultKeyword));
                fixup = static tree => tree.GetText(Encoding.UTF8);
                additionalTypes = Array.Empty<TypeDeclarationSyntax>();
            }
            else
            {
                // Create a ReadOnlyMemory<byte> instance from the memory manager:
                //
                // HlslBytecodeMemoryManager.Instance.Memory
                memoryExpression =
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("HlslBytecodeMemoryManager"),
                            IdentifierName("Instance")),
                        IdentifierName("Memory"));

                TypeDeclarationSyntax memoryManagerDeclaration = GetMemoryManagerDeclaration();
                string bytecodeLiterals = SyntaxFormattingHelper.BuildByteArrayInitializationExpressionString(success.Bytecode.AsSpan());

                additionalTypes = new TypeDeclarationSyntax[] { memoryManagerDeclaration };
                fixup = tree => SourceText.From(tree.ToFullString().Replace("__EMBEDDED_SHADER_BYTECODE", bytecodeLiterals), Encoding.UTF8);
            }

            // This code produces a method declaration as follows:
            //
            // readonly global::System.ReadOnlyMemory<byte> global::ComputeSharp.D2D1.__Internals.ID2D1Shader.HlslBytecode => <MEMORY_EXPRESSION>;
            return
                PropertyDeclaration(
                    GenericName(Identifier("global::System.ReadOnlyMemory"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))),
                    Identifier(nameof(ID2D1Shader.HlslBytecode)))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .WithExpressionBody(ArrowExpressionClause(memoryExpression))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        /// <summary>
        /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
        /// </summary>
        /// <returns>The <see cref="BlockSyntax"/> instance trying to retrieve the precompiled shader.</returns>
        private static unsafe TypeDeclarationSyntax GetMemoryManagerDeclaration()
        {
            // Create the MemoryManager<T> declaration:
            //
            // /// <summary>
            // /// <see cref="global::System.Buffers.MemoryManager{T}"/> implementation to get the HLSL bytecode.
            // /// </summary>
            // [global::System.CodeDom.Compiler.GeneratedCode("...", "...")]
            // [global::System.Diagnostics.DebuggerNonUserCode]
            // [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            // file sealed HlslBytecodeMemoryManager : global::System.Buffers.MemoryManager<byte>
            // {
            // }
            TypeDeclarationSyntax typeDeclaration =
                ClassDeclaration("HlslBytecodeMemoryManager")
                .AddModifiers(Token(SyntaxKind.FileKeyword), Token(SyntaxKind.SealedKeyword))
                .AddBaseListTypes(SimpleBaseType(
                    GenericName(Identifier("global::System.Buffers.MemoryManager"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword)))))
                .AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).FullName))),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).Assembly.GetName().Version.ToString())))))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage")))))
                .WithLeadingTrivia(
                    Comment("/// <summary>"),
                    Comment("""/// A <see cref="global::System.Buffers.MemoryManager{T}"/> implementation to get the HLSL bytecode."""),
                    Comment("/// </summary>"));

            using ImmutableArrayBuilder<MemberDeclarationSyntax> memberDeclarations = ImmutableArrayBuilder<MemberDeclarationSyntax>.Rent();

            // Declare the singleton property to get the memory manager:
            //
            // /// <summary>The singleton <see cref="HlslBytecodeMemoryManager"/> instance to use.</summary>
            // public static readonly HlslBytecodeMemoryManager Instance = new();
            memberDeclarations.Add(
                FieldDeclaration(
                    VariableDeclaration(IdentifierName("HlslBytecodeMemoryManager"))
                    .AddVariables(
                        VariableDeclarator(Identifier("Instance"))
                        .WithInitializer(EqualsValueClause(ImplicitObjectCreationExpression()))))
                .AddModifiers(
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.StaticKeyword),
                    Token(SyntaxKind.ReadOnlyKeyword))
                .WithLeadingTrivia(Comment("""/// <summary>The singleton <see cref="HlslBytecodeMemoryManager"/> instance to use.</summary>""")));

            // Construct the RVA span property:
            //
            // /// <summary>The RVA data with the HLSL bytecode.</summary>
            // private static global::System.ReadOnlySpan<byte> Data => new byte[] { __EMBEDDED_SHADER_BYTECODE };
            memberDeclarations.Add(
                PropertyDeclaration(
                    GenericName(Identifier("global::System.ReadOnlySpan"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))),
                    Identifier("Data"))
                .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.StaticKeyword))
                .WithExpressionBody(
                    ArrowExpressionClause(
                        ArrayCreationExpression(
                            ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                            .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))),
                            InitializerExpression(
                                SyntaxKind.ArrayInitializerExpression,
                                SingletonSeparatedList<ExpressionSyntax>(IdentifierName("__EMBEDDED_SHADER_BYTECODE"))))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                .WithLeadingTrivia(Comment("/// <summary>The RVA data with the HLSL bytecode.</summary>")));

            // Override the Memory<byte> property:
            //
            // /// <inheritdoc/>
            // public override global::System.Memory<byte> Memory
            // {
            //     [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            //     get => CreateMemory(Data.Length);
            // }
            memberDeclarations.Add(
                PropertyDeclaration(
                    GenericName(Identifier("global::System.Memory"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))),
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

            // Add the GetSpan() method:
            //
            // /// <inheritdoc/>
            // public override unsafe global::System.Span<byte> GetSpan
            // {
            //     return new(global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::System.Runtime.InteropServices.MemoryMarshal(Data)), Data.Length);
            // }
            memberDeclarations.Add(
                MethodDeclaration(
                    GenericName(Identifier("global::System.Span"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.ByteKeyword))),
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