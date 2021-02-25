using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.Core.Helpers;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

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

            // Type attributes
            AttributeListSyntax[] attributes = new[]
            {
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("EditorBrowsable")).AddArgumentListArguments(
                    AttributeArgument(ParseExpression("EditorBrowsableState.Never"))))),
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName("Obsolete")).AddArgumentListArguments(
                    AttributeArgument(LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        Literal("This type is not intended to be used directly by user code"))))))
            };

            foreach (StructDeclarationSyntax structDeclaration in structDeclarations)
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(structDeclaration.SyntaxTree);
                INamedTypeSymbol structDeclarationSymbol = semanticModel.GetDeclaredSymbol(structDeclaration)!;

                try
                {
                    OnExecute(context, structDeclaration, structDeclarationSymbol, ref attributes);
                }
                catch
                {
                    context.ReportDiagnostic(IComputeShaderDataLoaderGeneratorError, structDeclaration, structDeclarationSymbol);
                }
            }
        }

        /// <summary>
        /// Processes a given target type.
        /// </summary>
        /// <param name="context">The input <see cref="GeneratorExecutionContext"/> instance to use.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
        /// <param name="attributes">The list of <see cref="AttributeListSyntax"/> instances to append to the first copy of the partial class being generated.</param>
        private static void OnExecute(GeneratorExecutionContext context, StructDeclarationSyntax structDeclaration, INamedTypeSymbol structDeclarationSymbol, ref AttributeListSyntax[] attributes)
        {
            // Only process compute shader types
            if (!structDeclarationSymbol.Interfaces.Any(static interfaceSymbol => interfaceSymbol.Name == nameof(IComputeShader))) return;

            TypeSyntax shaderType = ParseTypeName(structDeclarationSymbol.ToDisplayString());
            BlockSyntax block = Block(GetDispatchDataLoadingStatements(structDeclarationSymbol, out int root32BitConstantsCount));

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
            //         [System.Obsolete("This method is not intended to be called directly by user code")]
            //         [return: ComputeRoot32BitConstants(42)]
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
                    Token(SyntaxKind.PartialKeyword)).AddAttributeLists(attributes).AddMembers(
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
                            Literal("This method is not intended to be called directly by user code")))))),
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName(nameof(ComputeRoot32BitConstantsAttribute).Replace("Attribute", "")))
                        .AddArgumentListArguments(AttributeArgument(LiteralExpression(
                            SyntaxKind.NumericLiteralExpression,
                            Literal(root32BitConstantsCount))))))
                    .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.ReturnKeyword)))).AddModifiers(
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.StaticKeyword)).AddParameterListParameters(
                    Parameter(Identifier("device")).WithType(IdentifierName("ComputeSharp.GraphicsDevice")),
                    Parameter(Identifier("shader")).WithModifiers(TokenList(Token(SyntaxKind.InKeyword))).WithType(shaderType),
                    Parameter(Identifier("r0")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(PredefinedType(Token(SyntaxKind.ULongKeyword))),
                    Parameter(Identifier("r1")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(PredefinedType(Token(SyntaxKind.ByteKeyword))))
                    .WithBody(block)))
                .WithNamespaceKeyword(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.NamespaceKeyword, TriviaList())))
                .NormalizeWhitespace()
                .ToFullString();

            // Clear the attribute list to avoid duplicates
            attributes = Array.Empty<AttributeListSyntax>();

            // Add the method source attribute
            context.AddSource(structDeclarationSymbol.GetGeneratedFileName<IComputeShaderDataLoaderGenerator>(), SourceText.From(source, Encoding.UTF8));
        }

        /// <summary>
        /// Gets a sequence of statements to load the dispatch data for a given shader
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="count">The total number of 32 bit root constants to load.</param>
        /// <returns>The sequence of <see cref="StatementSyntax"/> instances to load shader dispatch data.</returns>
        [Pure]
        private static IEnumerable<StatementSyntax> GetDispatchDataLoadingStatements(INamedTypeSymbol structDeclarationSymbol, out int count)
        {
            List<StatementSyntax> statements = new();

            int
                resourceOffset = 0,
                rawDataOffset = sizeof(int) * 3;

            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (fieldSymbol.Type is not INamedTypeSymbol { IsStatic: false } typeSymbol)
                {
                    continue;
                }

                string typeName = typeSymbol.GetFullMetadataName();

                if (HlslKnownTypes.IsTypedResourceType(typeName))
                {
                    statements.Add(ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            ParseExpression($"Unsafe.Add(ref r0, {resourceOffset++})"),
                            ParseExpression($"GraphicsResourceHelper.ValidateAndGetGpuDescriptorHandle(shader.{fieldSymbol.Name}, device)"))));
                }
                else if (HlslKnownTypes.IsScalarOrVectorType(typeName))
                {
                    if (fieldSymbol.IsConst) continue;

                    var (fieldSize, fieldPack) = HlslKnownSizes.GetTypeInfo(typeName);

                    // Calculate the right offset with 16-bytes padding (HLSL constant buffer).
                    // Since we're in a constant buffer, we need to both pad the starting offset
                    // to be aligned to the packing size of the type, and also to align the initial
                    // offset to ensure that values do not cross 16 bytes boundaries either.
                    rawDataOffset = AlignmentHelper.AlignToBoundary(
                        AlignmentHelper.Pad(rawDataOffset, fieldPack),
                        fieldSize,
                        16);

                    statements.Add(ExpressionStatement(
                        ParseExpression($"Unsafe.WriteUnaligned(ref Unsafe.Add(ref r1, {rawDataOffset}), shader.{fieldSymbol.Name})")));

                    rawDataOffset += fieldSize;
                }
            }

            // After all the captured fields have been processed, ansure the reported byte size for
            // the local variables is padded to a multiple of a 32 bit value. This is necessary to
            // enable loading all the dispatch data after reinterpreting it to a sequence of values
            // of size 32 bits (via SetComputeRoot32BitConstants), without reading out of bounds.
            rawDataOffset = AlignmentHelper.Pad(rawDataOffset, sizeof(int));

            statements.Add(ReturnStatement(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(rawDataOffset))));

            count = rawDataOffset / sizeof(int);

            return statements;
        }
    }
}

