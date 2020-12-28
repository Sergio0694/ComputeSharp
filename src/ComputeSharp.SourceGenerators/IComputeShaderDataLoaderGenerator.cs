using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.Core.Helpers;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators
{
    [Generator]
    public class IComputeShaderDataLoaderGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            // Find all the struct declarations
            ImmutableArray<StructDeclarationSyntax> structDeclarations = (
                from tree in context.Compilation.SyntaxTrees
                from structDeclaration in tree.GetRoot().DescendantNodes().OfType<StructDeclarationSyntax>()
                select structDeclaration).ToImmutableArray();

            foreach (StructDeclarationSyntax structDeclaration in structDeclarations)
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(structDeclaration.SyntaxTree);
                INamedTypeSymbol structDeclarationSymbol = semanticModel.GetDeclaredSymbol(structDeclaration)!;

                // Only process compute shader types
                if (!structDeclarationSymbol.Interfaces.Any(static interfaceSymbol => interfaceSymbol.Name == nameof(IComputeShader))) continue;

                TypeSyntax shaderType = ParseTypeName(structDeclarationSymbol.ToDisplayString());
                BlockSyntax block = Block(GetDispatchDataLoadingStatements(structDeclarationSymbol));

                // Create a static method to create the combined hashcode for a given shader type.
                // This code takes a block syntax and produces a compilation unit as follows:
                //
                // using System;
                // using System.ComponentModel;
                //
                // namespace ComputeSharp.__Internals
                // {
                //     internal static partial class DispatchDataLoader
                //     {
                //         [System.ComponentModel.EditorBrowsable(EditorBrowsableState.Never)]
                //         [System.Obsolete("This method is intended for internal usage only")]
                //         public static int LoadDispatchData(GraphicsDevice device, in ShaderType shader, ref ulong r0, ref byte r1)
                //         {
                //             ...
                //         }
                //     }
                // }
                var source =
                    CompilationUnit().AddUsings(
                    UsingDirective(IdentifierName("System")),
                    UsingDirective(IdentifierName("System.ComponentModel")),
                    UsingDirective(IdentifierName("System.Runtime.CompilerServices"))).AddMembers(
                    NamespaceDeclaration(IdentifierName("ComputeSharp.__Internals")).AddMembers(
                    ClassDeclaration("DispatchDataLoader").AddModifiers(
                        Token(SyntaxKind.InternalKeyword),
                        Token(SyntaxKind.StaticKeyword),
                        Token(SyntaxKind.PartialKeyword)).AddMembers(
                    MethodDeclaration(
                        PredefinedType(Token(SyntaxKind.IntKeyword)),
                        Identifier("LoadDispatchData")).AddAttributeLists(
                        AttributeList(SingletonSeparatedList(
                            Attribute(IdentifierName("EditorBrowsable")).AddArgumentListArguments(
                            AttributeArgument(ParseExpression("EditorBrowsableState.Never"))))),
                        AttributeList(SingletonSeparatedList(
                            Attribute(IdentifierName("Obsolete")).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(
                                SyntaxKind.StringLiteralExpression,
                                Literal("This method is intended for internal usage only"))))))).AddModifiers(
                        Token(SyntaxKind.PublicKeyword),
                        Token(SyntaxKind.StaticKeyword)).AddParameterListParameters(
                        Parameter(Identifier("device")).WithType(IdentifierName("ComputeSharp.Graphics.GraphicsDevice")),
                        Parameter(Identifier("shader")).WithModifiers(TokenList(Token(SyntaxKind.InKeyword))).WithType(shaderType),
                        Parameter(Identifier("r0")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(PredefinedType(Token(SyntaxKind.ULongKeyword))),
                        Parameter(Identifier("r1")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                        .WithBody(block))))
                    .NormalizeWhitespace()
                    .ToFullString();

                // Add the method source attribute
                context.AddSource(structDeclarationSymbol.GetGeneratedFileName<IComputeShaderDataLoaderGenerator>(), SourceText.From(source, Encoding.UTF8));
            }
        }

        /// <summary>
        /// Gets a sequence of statements to load the dispatch data for a given shader
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load shader dispatch data.</returns>
        [Pure]
        private static IEnumerable<StatementSyntax> GetDispatchDataLoadingStatements(INamedTypeSymbol structDeclarationSymbol)
        {
            int
                resourceOffset = 0,
                rawDataOffset = sizeof(int) * 3;

            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                INamedTypeSymbol typeSymbol = (INamedTypeSymbol)fieldSymbol.Type;
                string typeName = typeSymbol.GetFullMetadataName();

                if (HlslKnownTypes.IsTypedResourceType(typeName))
                {
                    yield return ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            ParseExpression($"Unsafe.Add(ref r0, {resourceOffset++})"),
                            ParseExpression($"GraphicsResourceHelper.ValidateAndGetGpuDescriptorHandle(shader.{fieldSymbol.Name}, device)")));
                }
                else if (HlslKnownTypes.IsScalarOrVectorType(typeName))
                {
                    var (fieldSize, fieldPack) = HlslKnownSizes.GetTypeInfo(typeName);

                    // Calculate the right offset with 16-bytes padding (HLSL constant buffer).
                    // Since we're in a constant buffer, we need to both pad the starting offset
                    // to be aligned to the packing size of the type, and also to align the initial
                    // offset to ensure that values do not cross 16 bytes boundaries either.
                    rawDataOffset = AlignmentHelper.AlignToBoundary(
                        AlignmentHelper.Pad(rawDataOffset, fieldPack),
                        fieldSize,
                        16);

                    yield return ExpressionStatement(
                        ParseExpression($"Unsafe.WriteUnaligned(ref Unsafe.Add(ref r1, {rawDataOffset}), shader.{fieldSymbol.Name})"));

                    rawDataOffset += fieldSize;
                }
            }

            // After all the captured fields have been processed, ansure the reported byte size for
            // the local variables is padded to the standard alignment for constant buffers. This is
            // necessary to enable loading all the dispatch data after reinterpreting it to a sequence
            // of values of size 16 bytes (in particular, Int4 is used), without reading out of bounds.
            rawDataOffset = AlignmentHelper.Pad(rawDataOffset, 16);

            yield return ReturnStatement(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(rawDataOffset)));
        }
    }
}

