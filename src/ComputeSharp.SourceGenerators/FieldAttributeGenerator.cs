using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

namespace ComputeSharp.SourceGenerators
{
    [Generator]
    public class FieldAttributeGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            // Find all the [Field] usages
            var attributes = new Queue<IGrouping<StructDeclarationSyntax, AttributeData>>(
                from tree in context.Compilation.SyntaxTrees
                from structDeclaration in tree.GetRoot().DescendantNodes().OfType<StructDeclarationSyntax>()
                let semanticModel = context.Compilation.GetSemanticModel(structDeclaration.SyntaxTree)
                let structSymbol = semanticModel.GetDeclaredSymbol(structDeclaration)
                from attribute in structSymbol.GetAttributes()
                where attribute.AttributeClass is { Name: nameof(FieldAttribute) }
                group attribute by structDeclaration into groups
                select groups);

            foreach (IGrouping<StructDeclarationSyntax, AttributeData> group in attributes)
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(group.Key.SyntaxTree);
                INamedTypeSymbol structDeclarationSymbol = semanticModel.GetDeclaredSymbol(group.Key)!;

                var generatedTypeInfo = GetProcessedFields(group);

                // Extract the info on the type to process
                var namespaceName = structDeclarationSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces));
                var structName = group.Key.Identifier.Text;
                var structModifiers = group.Key.Modifiers;

                // Declare the partial type with the right layout attribute to track the size and
                // the HLSL packing, and add the generated field declarations requested by the user.
                var structDeclarationSyntax =
                    StructDeclaration(structName).WithModifiers(structModifiers)
                    .AddAttributeLists(AttributeList(SingletonSeparatedList(generatedTypeInfo.Layout)))
                    .AddMembers(generatedTypeInfo.Fields.ToArray());

                TypeDeclarationSyntax typeDeclarationSyntax = structDeclarationSyntax;

                // Add all parent types in ascending order, if any
                foreach (var parentType in group.Key.Ancestors().OfType<TypeDeclarationSyntax>())
                {
                    typeDeclarationSyntax = parentType
                        .WithMembers(SingletonList<MemberDeclarationSyntax>(typeDeclarationSyntax))
                        .WithConstraintClauses(List<TypeParameterConstraintClauseSyntax>())
                        .WithBaseList(null)
                        .WithoutTrivia();
                }

                // Create the compilation unit with the namespace and target member.
                var source =
                    CompilationUnit().AddMembers(
                    NamespaceDeclaration(IdentifierName(namespaceName)).AddMembers(typeDeclarationSyntax))
                    .NormalizeWhitespace()
                    .ToFullString();

                // Add the partial type
                context.AddSource($"__ComputeSharp_{typeof(FieldAttribute).FullName}_{structDeclarationSymbol.Name}", SourceText.From(source, Encoding.UTF8));
            }
        }

        /// <summary>
        /// Gets a sequence of field declarations for a given type.
        /// </summary>
        /// <param name="attributes">The input sequence of <see cref="FieldAttribute"/> data instances.</param>
        /// <returns>An attribute for the layout of the kind, and the sequence of field declarations.</returns>
        private static (AttributeSyntax Layout, IEnumerable<FieldDeclarationSyntax> Fields) GetProcessedFields(IEnumerable<AttributeData> attributes)
        {
            int size = 0, pack = 0;
            List<FieldDeclarationSyntax> fieldDeclarations = new();

            foreach (var attribute in attributes)
            {
                // Extract the field info
                string fieldName = (string)attribute.ConstructorArguments[0].Value!;
                INamedTypeSymbol fieldType = (INamedTypeSymbol)attribute.ConstructorArguments[1].Value!;

                // Get the size and pack for the current field
                if (!HlslKnownSizes.TryGetMappedSize(fieldType.GetFullMetadataName(), out var mapping))
                {
                    var structLayout = fieldType.GetAttributes().First(a => a.AttributeClass is { Name: nameof(StructLayoutAttribute) });

                    _ = structLayout.TryGetNamedArgument(nameof(StructLayoutAttribute.Size), out mapping.Size);
                    _ = structLayout.TryGetNamedArgument(nameof(StructLayoutAttribute.Pack), out mapping.Pack);
                }

                // Calculate the target offset
                int
                    adjustment = size % mapping.Pack,
                    offset = size + adjustment;

                size += adjustment + mapping.Size;
                pack = Math.Max(pack, mapping.Pack);

                // Create the field declaration
                var fieldDeclaration =
                    FieldDeclaration(                    
                    VariableDeclaration(IdentifierName(fieldType.GetFullMetadataName()))
                    .AddVariables(VariableDeclarator(Identifier(fieldName))))
                    .AddAttributeLists(AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName(typeof(FieldOffsetAttribute).FullName)).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(offset)))))))
                    .AddModifiers(Token(SyntaxKind.PublicKeyword));

                // Add the <summary> XML tag, if a summary is present
                if (attribute.TryGetNamedArgument(nameof(FieldAttribute.Summary), out string? summary))
                {
                    fieldDeclaration =
                        fieldDeclaration.WithLeadingTrivia(
                        TriviaList(Trivia(DocumentationCommentTrivia(
                        SyntaxKind.SingleLineDocumentationCommentTrivia,
                        List(new XmlNodeSyntax[]
                        {
                            XmlText().AddTextTokens(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), " ", " ", TriviaList())),
                            XmlExampleElement(SingletonList<XmlNodeSyntax>(XmlText().AddTextTokens(XmlTextLiteral(summary!))))
                            .WithStartTag(XmlElementStartTag(XmlName(Identifier("summary"))))
                            .WithEndTag(XmlElementEndTag(XmlName(Identifier("summary")))),
                            XmlText().AddTextTokens(XmlTextNewLine("\n", false))
                        })))));
                }

                fieldDeclarations.Add(fieldDeclaration);
            }

            // Add the trailing padding, if needed
            size += size % pack;

            // Get the attribute for the layout of the entire type
            var layoutAttribute =
                Attribute(IdentifierName(typeof(StructLayoutAttribute).FullName)).AddArgumentListArguments(
                    AttributeArgument(MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(typeof(LayoutKind).FullName),
                        IdentifierName(nameof(LayoutKind.Explicit)))),
                    AttributeArgument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(size)))
                    .WithNameEquals(NameEquals(IdentifierName(nameof(StructLayoutAttribute.Size)))),
                    AttributeArgument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(pack)))
                    .WithNameEquals(NameEquals(IdentifierName(nameof(StructLayoutAttribute.Pack)))));

            return (layoutAttribute, fieldDeclarations);
        }
    }
}

