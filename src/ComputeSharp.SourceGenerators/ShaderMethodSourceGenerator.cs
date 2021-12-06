using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Helpers;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static ComputeSharp.SourceGenerators.Helpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618, RS1024

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A source generator for processing static methods referenced from compute shaders.
/// </summary>
[Generator]
public sealed partial class ShaderMethodSourceGenerator : ISourceGenerator
{
    /// <inheritdoc/>
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(static () => new SyntaxReceiver());
    }

    /// <inheritdoc/>
    public void Execute(GeneratorExecutionContext context)
    {
        // Get the syntax receiver with the candidate nodes
        if (context.SyntaxContextReceiver is not SyntaxReceiver syntaxReceiver)
        {
            return;
        }

        foreach (SyntaxReceiver.Item item in syntaxReceiver.GatheredInfo)
        {
            SemanticModelProvider semanticModel = new(context.Compilation);

            try
            {
                OnExecute(context, item.MethodDeclaration, semanticModel, item.MethodSymbol);
            }
            catch
            {
                context.ReportDiagnostic(ShaderMethodSourceGeneratorError, item.MethodDeclaration, item.MethodSymbol);
            }
        }
    }

    /// <summary>
    /// Processes a given target method.
    /// </summary>
    /// <param name="context">The input <see cref="GeneratorExecutionContext"/> instance to use.</param>
    /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> node to process.</param>
    /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> with metadata on the types being processed.</param>
    /// <param name="methodDeclarationSymbol">The <see cref="IMethodSymbol"/> for <paramref name="methodDeclaration"/>.</param>
    private static void OnExecute(GeneratorExecutionContext context, MethodDeclarationSyntax methodDeclaration, SemanticModelProvider semanticModel, IMethodSymbol methodDeclarationSymbol)
    {
        // We need to sets to track all discovered custom types and static methods
        HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
        Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);

        // Explore the syntax tree and extract the processed info
        var (invokeMethod, processedMethods) = GetProcessedMethods(context, methodDeclaration, semanticModel, discoveredTypes, constantDefinitions);
        var processedTypes = IShaderGenerator.GetDeclaredTypes(discoveredTypes);
        var processedConstants = IShaderGenerator.GetDefinedConstants(constantDefinitions);

        // Create the compilation unit with the source attribute
        var source =
            CompilationUnit().AddAttributeLists(
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName($"global::{typeof(ShaderMethodSourceAttribute).FullName}")).AddArgumentListArguments(
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(methodDeclarationSymbol.GetFullMetadataName(true)))),
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(invokeMethod))),
                    AttributeArgument(NestedArrayExpression(processedTypes)),
                    AttributeArgument(NestedArrayExpression(processedMethods)),
                    AttributeArgument(NestedArrayExpression(processedConstants)))))
            .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
            .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
            .NormalizeWhitespace(eol: "\n")
            .ToFullString();

        // Add the method source attribute
        context.AddSource(methodDeclarationSymbol.GetGeneratedFileName(), SourceText.From(source, Encoding.UTF8));
    }

    /// <summary>
    /// Gets a sequence of processed methods from a target method declaration.
    /// </summary>
    /// <param name="context">The current generator context in use.</param>
    /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> instance for the current method.</param>
    /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the method to process.</param>
    /// <param name="discoveredTypes">The collection of currently discovered types.</param>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <returns>A sequence of processed methods in <paramref name="methodDeclaration"/> (main method and all captured methods).</returns>
    [Pure]
    private static (string TargetMethod, IEnumerable<(string Signature, string Definition)> ProcessedMethods) GetProcessedMethods(
        GeneratorExecutionContext context,
        MethodDeclarationSyntax methodDeclaration,
        SemanticModelProvider semanticModel,
        ICollection<INamedTypeSymbol> discoveredTypes,
        IDictionary<IFieldSymbol, string> constantDefinitions)
    {
        Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);

        ShaderSourceRewriter shaderSourceRewriter = new(semanticModel, discoveredTypes, staticMethods, constantDefinitions, context);

        // Rewrite the method syntax tree
        var targetMethod = shaderSourceRewriter
            .Visit(methodDeclaration)!
            .WithIdentifier(Identifier(ShaderMethodSourceAttribute.InvokeMethodIdentifier))
            .WithoutTrivia()
            .NormalizeWhitespace(eol: "\n")
            .ToFullString();

        List<(string, string)> processedMethods = new(shaderSourceRewriter.LocalFunctions.Count);

        // Emit the extracted local functions
        foreach (var localFunction in shaderSourceRewriter.LocalFunctions)
        {
            processedMethods.Add((
                localFunction.Value.AsDefinition().NormalizeWhitespace(eol: "\n").ToFullString(),
                localFunction.Value.NormalizeWhitespace(eol: "\n").ToFullString()));
        }

        // Emit the discovered static methods
        foreach (var staticMethod in staticMethods.Values)
        {
            processedMethods.Add((
                staticMethod.AsDefinition().NormalizeWhitespace(eol: "\n").ToFullString(),
                staticMethod.NormalizeWhitespace(eol: "\n").ToFullString()));
        }

        return (targetMethod, processedMethods);
    }
}
