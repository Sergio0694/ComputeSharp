using System.Collections.Generic;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// Gets the shader type for a given shader, if any.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to check.</param>
    /// <param name="d2D1PixelShaderSymbol">The <see cref="INamedTypeSymbol"/> instance for <see cref="ID2D1PixelShader"/>.</param>
    /// <returns>Whether or not <paramref name="typeSymbol"/> implements <paramref name="d2D1PixelShaderSymbol"/>.</returns>
    public static bool IsD2D1PixelShaderType(INamedTypeSymbol typeSymbol, INamedTypeSymbol d2D1PixelShaderSymbol)
    {
        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            if (interfaceSymbol.Name == nameof(ID2D1PixelShader) &&
                 SymbolEqualityComparer.Default.Equals(interfaceSymbol, d2D1PixelShaderSymbol))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Creates a <see cref="CompilationUnitSyntax"/> instance wrapping the given method.
    /// </summary>
    /// <param name="hierarchyInfo">The <see cref="HierarchyInfo"/> instance for the current type.</param>
    /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> item to insert.</param>
    /// <param name="canUseSkipLocalsInit">Whether <c>[SkipLocalsInit]</c> can be used.</param>
    /// <returns>A <see cref="CompilationUnitSyntax"/> object wrapping <paramref name="methodDeclaration"/>.</returns>
    private static CompilationUnitSyntax GetCompilationUnitFromMethod(
        HierarchyInfo hierarchyInfo,
        MethodDeclarationSyntax methodDeclaration,
        bool canUseSkipLocalsInit)
    {
        // Method attributes
        List<AttributeListSyntax> attributes = new()
        {
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).FullName))),
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).Assembly.GetName().Version.ToString())))))),
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

        // Add [SkipLocalsInit] if the target project allows it
        if (canUseSkipLocalsInit)
        {
            attributes.Add(AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Runtime.CompilerServices.SkipLocalsInit")))));
        }

        return hierarchyInfo.GetSyntax(methodDeclaration.AddAttributeLists(attributes.ToArray()));
    }
}
