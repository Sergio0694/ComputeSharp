using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Helpers;
using ComputeSharp.SourceGenerators.Models;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static ComputeSharp.SourceGenerators.Helpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618, RS1024

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A source generator for processing static methods referenced from compute shaders.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class ShaderMethodSourceGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Get all declared methods with the [ShaderMethod] attribute
        IncrementalValuesProvider<(MethodDeclarationSyntax Syntax, IMethodSymbol Symbol)> structDeclarations =
            context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, token) => node is MethodDeclarationSyntax { AttributeLists.Count: > 0 } methodDeclaration,
                static (context, token) => (
                    Syntax: (MethodDeclarationSyntax)context.Node,
                    Symbol: (IMethodSymbol?)context.SemanticModel.GetDeclaredSymbol(context.Node, token)))
            .Where(static item => item.Symbol is not null &&
                                  item.Symbol.GetAttributes().Any(static a => a.AttributeClass?.ToDisplayString() == typeof(ShaderMethodAttribute).FullName))!;

        // Get the source info for each method
        IncrementalValuesProvider<Result<HlslMethodSourceInfo>> methodSourceInfoWithErrors =
            structDeclarations
            .Combine(context.CompilationProvider)
            .Select(static (item, token) =>
            {
                HlslMethodSourceInfo sourceInfo = Execute.GetData(
                    item.Right,
                    item.Left.Syntax,
                    item.Left.Symbol,
                    out ImmutableArray<Diagnostic> diagnostics);

                return new Result<HlslMethodSourceInfo>(sourceInfo, diagnostics);
            });

        // Output the diagnostics
        context.ReportDiagnostics(methodSourceInfoWithErrors.Select(static (item, token) => item.Errors));

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<HlslMethodSourceInfo> methodSourceInfo =
            methodSourceInfoWithErrors
            .Select(static (item, token) => item.Value)
            .WithComparer(HlslMethodSourceInfo.Comparer.Default);

        // Generate the [ShaderMethodSource] attributes
        context.RegisterSourceOutput(methodSourceInfo, static (context, item) =>
        {
            CompilationUnitSyntax compilationUnit = Execute.GetSyntax(item);
            string filename = item.MetadataName.Replace('`', '-').Replace('+', '.');

            context.AddSource(filename, SourceText.From(compilationUnit.ToFullString(), Encoding.UTF8));
        });
    }

    /// <summary>
    /// A helper with all logic to generate the attribute declarations.
    /// </summary>
    private static class Execute
    {
        /// <summary>
        /// Processes a given target method.
        /// </summary>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> node to process.</param>
        /// <param name="methodSymbol">The <see cref="IMethodSymbol"/> instance for the current method.</param>
        /// <param name="diagnostics">The resulting diagnostics from the processing operation.</param>
        public static HlslMethodSourceInfo GetData(
            Compilation compilation,
            MethodDeclarationSyntax methodDeclaration,
            IMethodSymbol methodSymbol,
            out ImmutableArray<Diagnostic> diagnostics)
        {
            // We need to sets to track all discovered custom types and static methods
            HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);

            // Explore the syntax tree and extract the processed info
            var semanticModel = new SemanticModelProvider(compilation);
            var (entryPoint, dependentMethods) = GetProcessedMethods(methodDeclaration, semanticModel, discoveredTypes, constantDefinitions, out diagnostics);
            var definedTypes = IShaderGenerator.BuildHlslString.GetDeclaredTypes(discoveredTypes);
            var definedConstants = IShaderGenerator.BuildHlslString.GetDefinedConstants(constantDefinitions);

            return new(
                methodSymbol.GetFullMetadataName(includeParameters: true),
                entryPoint,
                definedTypes,
                definedConstants,
                dependentMethods);
        }

        /// <summary>
        /// Gets a sequence of processed methods from a target method declaration.
        /// </summary>
        /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> instance for the current method.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the method to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="diagnostics">The resulting diagnostics from the processing operation.</param>
        /// <returns>A sequence of processed methods in <paramref name="methodDeclaration"/> (main method and all captured methods).</returns>
        [Pure]
        private static (string TargetMethod, ImmutableArray<(string Signature, string Definition)> DependentMethods) GetProcessedMethods(
            MethodDeclarationSyntax methodDeclaration,
            SemanticModelProvider semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            out ImmutableArray<Diagnostic> diagnostics)
        {
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
            ImmutableArray<Diagnostic>.Builder builder = ImmutableArray.CreateBuilder<Diagnostic>();

            ShaderSourceRewriter shaderSourceRewriter = new(semanticModel, discoveredTypes, staticMethods, constantDefinitions, builder);

            diagnostics = builder.ToImmutable();

            // Rewrite the method syntax tree
            var targetMethod = shaderSourceRewriter
                .Visit(methodDeclaration)!
                .WithIdentifier(Identifier(ShaderMethodSourceAttribute.InvokeMethodIdentifier))
                .WithoutTrivia()
                .NormalizeWhitespace(eol: "\n")
                .ToFullString();

            ImmutableArray<(string, string)>.Builder methods = ImmutableArray.CreateBuilder<(string, string)>(shaderSourceRewriter.LocalFunctions.Count);

            // Emit the extracted local functions
            foreach (var localFunction in shaderSourceRewriter.LocalFunctions)
            {
                methods.Add((
                    localFunction.Value.AsDefinition().NormalizeWhitespace(eol: "\n").ToFullString(),
                    localFunction.Value.NormalizeWhitespace(eol: "\n").ToFullString()));
            }

            // Emit the discovered static methods
            foreach (var staticMethod in staticMethods.Values)
            {
                methods.Add((
                    staticMethod.AsDefinition().NormalizeWhitespace(eol: "\n").ToFullString(),
                    staticMethod.NormalizeWhitespace(eol: "\n").ToFullString()));
            }

            return (targetMethod, methods.ToImmutable());
        }

        /// <summary>
        /// Creates a <see cref="CompilationUnitSyntax"/> instance with the processed attribute.
        /// </summary>
        /// <param name="methodSourceInfo">The input <see cref="HlslMethodSourceInfo"/> instance to use.</param>
        public static CompilationUnitSyntax GetSyntax(HlslMethodSourceInfo methodSourceInfo)
        {
            return
                CompilationUnit().AddAttributeLists(
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName($"global::{typeof(ShaderMethodSourceAttribute).FullName}")).AddArgumentListArguments(
                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(methodSourceInfo.MetadataName))),
                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(methodSourceInfo.EntryPoint))),
                        AttributeArgument(NestedArrayExpression(methodSourceInfo.DefinedTypes)),
                        AttributeArgument(NestedArrayExpression(methodSourceInfo.DependentMethods)),
                        AttributeArgument(NestedArrayExpression(methodSourceInfo.DefinedConstants)))))
                .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
                .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                .NormalizeWhitespace(eol: "\n");
        }
    }
}
