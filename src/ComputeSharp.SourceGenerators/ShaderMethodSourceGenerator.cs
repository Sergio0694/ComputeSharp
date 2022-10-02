using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxRewriters;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGenerators.Helpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

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
        // Get all declared methods (including global function declarations) with the [ShaderMethod] attribute, and their info
        IncrementalValuesProvider<ShaderMethodInfo> methodInfoWithErrors =
            context.SyntaxProvider
            .ForAttributeWithMetadataName(
                typeof(ShaderMethodAttribute).FullName,
                static (node, _) => node is MethodDeclarationSyntax { AttributeLists.Count: > 0 } or LocalFunctionStatementSyntax { Parent: GlobalStatementSyntax { Parent: CompilationUnitSyntax } },
                static (context, token) =>
                {
                    HlslMethodSourceInfo hlslMethodSourceInfo = Execute.GetData(
                        context.SemanticModel.Compilation,
                        (CSharpSyntaxNode)context.TargetNode,
                        (IMethodSymbol)context.TargetSymbol,
                        out ImmutableArray<DiagnosticInfo> diagnostics);

                    return new ShaderMethodInfo(hlslMethodSourceInfo, diagnostics);
                });

        // Output the diagnostics, if any
        context.ReportDiagnostics(
            methodInfoWithErrors
            .Select(static (item, _) => item.Diagnostcs)
            .WithComparer(EqualityComparer<DiagnosticInfo>.Default.ForImmutableArray()));

        // Generate the [ShaderMethodSource] attributes
        context.RegisterSourceOutput(methodInfoWithErrors, static (context, item) =>
        {
            CompilationUnitSyntax compilationUnit = Execute.GetSyntax(item.HlslMethodSource);
            string filename = item.HlslMethodSource.MetadataName.Replace('`', '-').Replace('+', '.');

            context.AddSource($"{filename}.g.cs", compilationUnit.GetText(Encoding.UTF8));
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
        /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> or <see cref="LocalFunctionStatementSyntax"/> node to process.</param>
        /// <param name="methodSymbol">The <see cref="IMethodSymbol"/> instance for the current method.</param>
        /// <param name="diagnostics">The resulting diagnostics from the processing operation.</param>
        public static HlslMethodSourceInfo GetData(
            Compilation compilation,
            CSharpSyntaxNode methodDeclaration,
            IMethodSymbol methodSymbol,
            out ImmutableArray<DiagnosticInfo> diagnostics)
        {
            ImmutableArray<DiagnosticInfo>.Builder builder = ImmutableArray.CreateBuilder<DiagnosticInfo>();

            // We need to sets to track all discovered custom types and static methods
            HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods = new(SymbolEqualityComparer.Default); // Unused in this generator

            // Explore the syntax tree and extract the processed info
            var semanticModel = new SemanticModelProvider(compilation);
            var (entryPoint, dependentMethods) = GetProcessedMethods(builder, methodDeclaration, semanticModel, discoveredTypes, constantDefinitions);
            var definedTypes = IShaderGenerator.BuildHlslSource.GetDeclaredTypes(builder, methodSymbol, discoveredTypes, instanceMethods);
            var definedConstants = IShaderGenerator.BuildHlslSource.GetDefinedConstants(constantDefinitions);

            diagnostics = builder.ToImmutable();

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
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> or <see cref="LocalFunctionStatementSyntax"/> instance for the current method.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the method to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of processed methods in <paramref name="methodDeclaration"/> (main method and all captured methods).</returns>
        private static (string TargetMethod, ImmutableArray<(string Signature, string Definition)> DependentMethods) GetProcessedMethods(
            ImmutableArray<DiagnosticInfo>.Builder diagnostics,
            CSharpSyntaxNode methodDeclaration,
            SemanticModelProvider semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions)
        {
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods = new(SymbolEqualityComparer.Default);

            ShaderSourceRewriter shaderSourceRewriter = new(semanticModel, discoveredTypes, staticMethods, instanceMethods, constantDefinitions, diagnostics);

            // Process the possible syntax nodes
            SyntaxNode visitedMethod = methodDeclaration switch
            {
                MethodDeclarationSyntax methodDeclarationSyntax => shaderSourceRewriter.Visit(methodDeclarationSyntax)!.WithIdentifier(Identifier(ShaderMethodSourceAttribute.InvokeMethodIdentifier)),
                LocalFunctionStatementSyntax functionStatementSyntax => shaderSourceRewriter.Visit(functionStatementSyntax)!.WithIdentifier(Identifier(ShaderMethodSourceAttribute.InvokeMethodIdentifier)),
                _ => throw new ArgumentException("Invalid method declaration syntax node type.")
            };

            // Rewrite the method syntax tree
            string targetMethod = visitedMethod
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
            // This produces an assembly attribute declaration as follows:
            //
            // // <auto-generated/>
            // #pragma warning disable
            // [assembly: global::ComputeSharp.ShaderMethodSourceAttribute(<ARGUMENTS>)]
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
                .WithOpenBracketToken(Token(TriviaList(
                    Comment("// <auto-generated/>"),
                    Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))),
                    SyntaxKind.OpenBracketToken,
                    TriviaList()))
                .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                .NormalizeWhitespace(eol: "\n");
        }
    }
}
