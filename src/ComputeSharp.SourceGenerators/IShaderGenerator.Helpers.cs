using System.Collections.Generic;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <summary>
    /// Gets whether a given type is a compute shader type (ie. implements any of the interfaces).
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to check.</param>
    /// <param name="compilation">The <see cref="Compilation"/> instance currently in use.</param>
    /// <param name="result">Whether <paramref name="typeSymbol"/> is a "pixel shader like" type.</param>
    /// <returns>Whether <paramref name="typeSymbol"/> is a compute shader type at all.</returns>
    private static bool TryGetIsPixelShaderLike(INamedTypeSymbol typeSymbol, Compilation compilation, out bool result)
    {
        INamedTypeSymbol computeShaderSymbol = compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader")!;
        INamedTypeSymbol pixelShaderSymbol = compilation.GetTypeByMetadataName("ComputeSharp.IPixelShader`1")!;

        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(interfaceSymbol, computeShaderSymbol))
            {
                result = false;

                return true;
            }
            else if (interfaceSymbol is { IsGenericType: true } &&
                     SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, pixelShaderSymbol))
            {
                result = true;

                return true;
            }
        }

        result = false;

        return false;
    }

    /// <summary>
    /// Creates a <see cref="CompilationUnitSyntax"/> instance wrapping the given method.
    /// </summary>
    /// <param name="hierarchyInfo">The <see cref="HierarchyInfo"/> instance for the current type.</param>
    /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> item to insert.</param>
    /// <param name="addSkipLocalsInitAttribute">Whether <c>[SkipLocalsInit]</c> should be used.</param>
    /// <returns>A <see cref="CompilationUnitSyntax"/> object wrapping <paramref name="methodDeclaration"/>.</returns>
    private static CompilationUnitSyntax GetCompilationUnitFromMethod(
        HierarchyInfo hierarchyInfo,
        MethodDeclarationSyntax methodDeclaration,
        bool addSkipLocalsInitAttribute)
    {
        // Method attributes
        List<AttributeListSyntax> attributes = new()
        {
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(IShaderGenerator).FullName))),
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(IShaderGenerator).Assembly.GetName().Version.ToString())))))),
            AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
            AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage")))),
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.ComponentModel.EditorBrowsable")).AddArgumentListArguments(
                AttributeArgument(ParseExpression("global::System.ComponentModel.EditorBrowsableState.Never"))))),
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.Obsolete")).AddArgumentListArguments(
                AttributeArgument(LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Literal("This method is not intended to be used directly by user code"))))))
        };

        // Add [SkipLocalsInit] if needed
        if (addSkipLocalsInitAttribute)
        {
            attributes.Add(AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Runtime.CompilerServices.SkipLocalsInit")))));
        }

        return hierarchyInfo.GetSyntax(methodDeclaration.AddAttributeLists(attributes.ToArray()));
    }
}