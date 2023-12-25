using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <summary>
/// A processor responsible for extracting definitions from shader types.
/// </summary>
internal static class HlslDefinitionsSyntaxProcessor
{
    /// <summary>
    /// Gets a sequence of discovered constants.
    /// </summary>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <returns>A sequence of discovered constants to declare in the shader.</returns>
    public static ImmutableArray<HlslConstant> GetDefinedConstants(IReadOnlyDictionary<IFieldSymbol, string> constantDefinitions)
    {
        using ImmutableArrayBuilder<HlslConstant> builder = new();

        foreach (KeyValuePair<IFieldSymbol, string> constant in constantDefinitions)
        {
            string ownerTypeName = ((INamedTypeSymbol)constant.Key.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
            string constantName = $"__{ownerTypeName}__{constant.Key.Name}";

            builder.Add((constantName, constant.Value));
        }

        return builder.ToImmutable();
    }

    /// <summary>
    /// Tries to get and rewrite a given static field to be used in a shader.
    /// </summary>
    /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
    /// <param name="fieldSymbol">The symbol for the field to analyze.</param>
    /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
    /// <param name="discoveredTypes">The collection of currently discovered types.</param>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <param name="staticFieldDefinitions">The collection of discovered static field definitions.</param>
    /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
    /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
    /// <param name="name">The mapped name for the field.</param>
    /// <param name="typeDeclaration">The type declaration for the field.</param>
    /// <param name="assignmentExpression">The assignment expression for the field, if present.</param>
    /// <param name="staticFieldRewriter">The <see cref="StaticFieldRewriter"/> instance used to rewrite the field expression.</param>
    /// <returns>Whether the field was processed successfully and is valid.</returns>
    public static bool TryGetStaticField(
        INamedTypeSymbol structDeclarationSymbol,
        IFieldSymbol fieldSymbol,
        SemanticModelProvider semanticModel,
        ICollection<INamedTypeSymbol> discoveredTypes,
        IDictionary<IFieldSymbol, string> constantDefinitions,
        IDictionary<IFieldSymbol, (string, string, string?)> staticFieldDefinitions,
        ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
        CancellationToken token,
        [NotNullWhen(true)] out string? name,
        [NotNullWhen(true)] out string? typeDeclaration,
        out string? assignmentExpression,
        [NotNullWhen(true)] out StaticFieldRewriter? staticFieldRewriter)
    {
        if (fieldSymbol.IsImplicitlyDeclared || !fieldSymbol.IsStatic || fieldSymbol.IsConst)
        {
            goto Failure;
        }

        if (!fieldSymbol.TryGetSyntaxNode(token, out VariableDeclaratorSyntax? variableDeclarator))
        {
            goto Failure;
        }

        // Static fields must be of a primitive, vector or matrix type
        if (fieldSymbol.Type is not INamedTypeSymbol typeSymbol ||
            !HlslKnownTypes.IsKnownHlslType(typeSymbol.GetFullyQualifiedMetadataName()))
        {
            diagnostics.Add(InvalidShaderStaticFieldType, variableDeclarator, structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type);

            goto Failure;
        }

        _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

        // The field name is either the mapped name (if a reserved name) or just the field name.
        // This method is shared across external fields too, and callers can just override this.
        name = mapping ?? fieldSymbol.Name;

        // Readonly fields are rewritten to static const fields, and mutable fields are just static.
        // Note that there's no protection for mutable static fields that may have been written to
        // in C# elsewhere. Shader authors should be aware that those writes would not appear in HLSL,
        // as each shader invocation would only see the initial assignment value (or the default value).
        typeDeclaration = fieldSymbol.IsReadOnly switch
        {
            true => $"static const {HlslKnownTypes.GetMappedName(typeSymbol)}",
            false => $"static {HlslKnownTypes.GetMappedName(typeSymbol)}"
        };

        token.ThrowIfCancellationRequested();

        // Create the rewriter to use, which is also returned to callers so they can extract any additional
        // info if needed. For instance, the D2D generator will check if the dispatch position is required.
        staticFieldRewriter = new StaticFieldRewriter(
            semanticModel,
            discoveredTypes,
            constantDefinitions,
            staticFieldDefinitions,
            diagnostics,
            token);

        ExpressionSyntax? processedDeclaration = staticFieldRewriter.Visit(variableDeclarator);

        token.ThrowIfCancellationRequested();

        assignmentExpression = processedDeclaration?.NormalizeWhitespace(eol: "\n").ToFullString();

        return true;

        Failure:
        name = null;
        typeDeclaration = null;
        assignmentExpression = null;
        staticFieldRewriter = null;

        return false;
    }

    /// <summary>
    /// Gets the sequence of processed discovered custom types.
    /// </summary>
    /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
    /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
    /// <param name="types">The sequence of discovered custom types.</param>
    /// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
    /// <param name="constructors">The collection of discovered constructors for custom struct types.</param>
    /// <param name="typeDeclarations">The collection of declarations of all custom types.</param>
    /// <param name="methodDeclarations">The collection of implementations of all methods in all custom types.</param>
    public static void GetDeclaredTypes(
        ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
        INamedTypeSymbol structDeclarationSymbol,
        IEnumerable<INamedTypeSymbol> types,
        IReadOnlyDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods,
        IReadOnlyDictionary<IMethodSymbol, (MethodDeclarationSyntax, MethodDeclarationSyntax)> constructors,
        out ImmutableArray<HlslUserType> typeDeclarations,
        out ImmutableArray<string> methodDeclarations)
    {
        using ImmutableArrayBuilder<HlslUserType> typeDeclarationsBuilder = new();
        using ImmutableArrayBuilder<string> methodDeclarationsBuilder = new();

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
                        IdentifierName(mappedType)).AddVariables(
                        VariableDeclarator(Identifier(mappedName!)))));
            }

            // Enumerate all members in a single pass, so we can avoid materializing the collection.
            // Additionally, this lets us add all members to the struct declaration in a single go.
            static IEnumerable<MethodDeclarationSyntax> GatherInstanceMethods(
                INamedTypeSymbol type,
                IReadOnlyDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods,
                IReadOnlyDictionary<IMethodSymbol, (MethodDeclarationSyntax, MethodDeclarationSyntax)> constructors)
            {
                // Normal instance methods
                foreach (KeyValuePair<IMethodSymbol, MethodDeclarationSyntax> method in instanceMethods)
                {
                    if (SymbolEqualityComparer.Default.Equals(method.Key.ContainingType, type))
                    {
                        yield return method.Value;
                    }
                }

                // Constructors and stubs
                foreach (KeyValuePair<IMethodSymbol, (MethodDeclarationSyntax Stub, MethodDeclarationSyntax Ctor)> methods in constructors)
                {
                    if (SymbolEqualityComparer.Default.Equals(methods.Key.ContainingType, type))
                    {
                        yield return methods.Value.Stub;
                        yield return methods.Value.Ctor;
                    }
                }
            }

            using (ImmutableArrayBuilder<MethodDeclarationSyntax> methodDefinitions = new())
            {
                // Forward declarations for all methods, and implementations.
                // We need these to ensure things work with arbitrary ordering.
                foreach (MethodDeclarationSyntax methodDeclaration in GatherInstanceMethods(type, instanceMethods, constructors))
                {
                    methodDefinitions.Add(methodDeclaration.AsDefinition());

                    // We need to normalize the whitespaces here, as this methhod declaration will not be added to
                    // the list of members for the struct declaration, but rather it will be written directly into
                    // the resulting HLSL source (as the implementation of this forward declaration). For the same
                    // reason, we also need to change the identifier to also include the containing type with '::'.
                    string methodImplementation =
                        methodDeclaration
                        .WithIdentifier(Identifier($"{structType}::{methodDeclaration.Identifier.Text}"))
                        .NormalizeWhitespace(eol: "\n")
                        .ToFullString();

                    methodDeclarationsBuilder.Add(methodImplementation);
                }

                // Add all method forward declarations to the current type
                structDeclaration = structDeclaration.WithMembers(structDeclaration.Members.AddRange(methodDefinitions.AsEnumerable()));
            }

            // Insert the trailing ; right after the closing bracket (after normalization)
            typeDeclarationsBuilder.Add((
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

        typeDeclarations = typeDeclarationsBuilder.ToImmutable();
        methodDeclarations = methodDeclarationsBuilder.ToImmutable();
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