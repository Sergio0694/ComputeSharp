using ComputeSharp.D2D1Interop.__Internals;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1Interop.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadBytecode
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>TryGetBytecode</c> method.
        /// </summary>
        /// <param name="hlslSource">The input HLSL source.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslString</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(string hlslSource)
        {
            BlockSyntax block = GetShaderBytecodeBody(hlslSource);

            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.D2D1Interop.__Internals.ID2D1Shader.LoadBytecode<TLoader>(ref TLoader loader, global::ComputeSharp.D2D1Interop.D2D1ShaderProfile? shaderProfile, out string hlslSource)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("LoadBytecode"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.D2D1Interop.__Internals.{nameof(ID2D1Shader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddTypeParameterListParameters(TypeParameter(Identifier("TLoader")))
                .AddParameterListParameters(
                    Parameter(Identifier("loader")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(IdentifierName("TLoader")),
                    Parameter(Identifier("shaderProfile")).WithType(NullableType(IdentifierName("global::ComputeSharp.D2D1Interop.D2D1ShaderProfile"))),
                    Parameter(Identifier("hlslSource")).AddModifiers(Token(SyntaxKind.OutKeyword)).WithType(PredefinedType(Token(SyntaxKind.StringKeyword))))
                .WithBody(block);
        }

        /// <summary>
        /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
        /// </summary>
        /// <param name="hlslSource">The input HLSL source.</param>
        /// <returns>The <see cref="BlockSyntax"/> instance trying to retrieve the precompiled shader.</returns>
        private static unsafe BlockSyntax GetShaderBytecodeBody(string hlslSource)
        {
            // This code produces a branch as follows:
            //
            // hlslSource = <HLSL_SOURCE>;
            return
                Block(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("hlslSource"),
                        LiteralExpression(
                            SyntaxKind.StringLiteralExpression,
                            Literal(hlslSource)))));
        }
    }
}
