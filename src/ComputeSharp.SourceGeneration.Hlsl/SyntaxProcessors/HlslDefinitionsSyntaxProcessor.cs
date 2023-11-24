using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <summary>
/// A processor responsible for extracting definitions from shader types
/// </summary>
internal static class HlslDefinitionsSyntaxProcessor
{
    /// <summary>
    /// Gets a sequence of discovered constants.
    /// </summary>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <returns>A sequence of discovered constants to declare in the shader.</returns>
    public static ImmutableArray<(string Name, string Value)> GetDefinedConstants(IReadOnlyDictionary<IFieldSymbol, string> constantDefinitions)
    {
        using ImmutableArrayBuilder<(string, string)> builder = new();

        foreach (KeyValuePair<IFieldSymbol, string> constant in constantDefinitions)
        {
            string ownerTypeName = ((INamedTypeSymbol)constant.Key.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
            string constantName = $"__{ownerTypeName}__{constant.Key.Name}";

            builder.Add((constantName, constant.Value));
        }

        return builder.ToImmutable();
    }

    /// <summary>
    /// Gets the sequence of processed discovered custom types.
    /// </summary>
    /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
    /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
    /// <param name="types">The sequence of discovered custom types.</param>
    /// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
    /// <returns>A sequence of custom type definitions to add to the shader source.</returns>
    public static ImmutableArray<(string Name, string Definition)> GetDeclaredTypes(
        ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
        INamedTypeSymbol structDeclarationSymbol,
        IEnumerable<INamedTypeSymbol> types,
        IReadOnlyDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods)
    {
        using ImmutableArrayBuilder<(string, string)> builder = new();

        IReadOnlyCollection<INamedTypeSymbol> invalidTypes;

        // Process the discovered types
        foreach (INamedTypeSymbol type in HlslKnownTypes.GetCustomTypes(types, out invalidTypes))
        {
            string structType = type.GetFullyQualifiedMetadataName().ToHlslIdentifierName();
            StructDeclarationSyntax structDeclaration = StructDeclaration(structType);

            // Declare the fields of the current type
            foreach (ISymbol memberSymbol in type.GetMembers())
            {
                // Once again, skip constants and static fields
                if (memberSymbol is not IFieldSymbol { IsConst: false, IsStatic: false } fieldSymbol)
                {
                    continue;
                }

                // Try to get the actual field name
                if (!ConstantBufferSyntaxProcessor.TryGetFieldAccessorName(fieldSymbol, out string? fieldName, out _))
                {
                    continue;
                }

                INamedTypeSymbol fieldType = (INamedTypeSymbol)fieldSymbol.Type;

                // Convert the name to the fully qualified HLSL version
                if (!HlslKnownTypes.TryGetMappedName(fieldType.GetFullyQualifiedMetadataName(), out string? mappedType))
                {
                    mappedType = fieldType.GetFullyQualifiedMetadataName().ToHlslIdentifierName();
                }

                // Get the field name as a valid HLSL identifier
                if (!HlslKnownKeywords.TryGetMappedName(fieldName, out string? mappedName))
                {
                    mappedName = fieldName;
                }

                structDeclaration = structDeclaration.AddMembers(
                    FieldDeclaration(VariableDeclaration(
                        IdentifierName(mappedType!)).AddVariables(
                        VariableDeclarator(Identifier(mappedName!)))));
            }

            // Declare the methods of the current type
            foreach (KeyValuePair<IMethodSymbol, MethodDeclarationSyntax> method in instanceMethods.Where(pair => SymbolEqualityComparer.Default.Equals(pair.Key.ContainingType, type)))
            {
                structDeclaration = structDeclaration.AddMembers(method.Value);
            }

            // Insert the trailing ; right after the closing bracket (after normalization)
            builder.Add((
                structType,
                structDeclaration
                    .NormalizeWhitespace(eol: "\n")
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                    .ToFullString()));
        }

        // Process the invalid types
        foreach (INamedTypeSymbol invalidType in invalidTypes)
        {
            diagnostics.Add(InvalidDiscoveredType, structDeclarationSymbol, structDeclarationSymbol, invalidType);
        }

        return builder.ToImmutable();
    }

    /// <summary>
    /// Finds and reports all invalid declared properties in a shader.
    /// </summary>
    /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
    /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
    public static void DetectAndReportInvalidPropertyDeclarations(ImmutableArrayBuilder<DiagnosticInfo> diagnostics, INamedTypeSymbol structDeclarationSymbol)
    {
        foreach (ISymbol memberSymbol in structDeclarationSymbol.GetMembers())
        {
            // Detect properties that are not explicit interface implementations
            if (memberSymbol is IPropertySymbol { ExplicitInterfaceImplementations.IsEmpty: true })
            {
                diagnostics.Add(InvalidPropertyDeclaration, memberSymbol, structDeclarationSymbol, memberSymbol);
            }

            // Detect properties causing a field to be generated
            if (memberSymbol is IFieldSymbol { AssociatedSymbol: IPropertySymbol associatedProperty })
            {
                diagnostics.Add(InvalidPropertyDeclaration, associatedProperty, structDeclarationSymbol, associatedProperty);
            }
        }
    }
}
