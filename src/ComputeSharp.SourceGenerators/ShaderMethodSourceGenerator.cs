using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static ComputeSharp.SourceGenerators.Helpers.SyntaxFactoryHelper;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators
{
    [Generator]
    public class ShaderMethodSourceGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            // Find all the [ShaderMethod] usages and retrieve the target methods
            ImmutableArray<MethodDeclarationSyntax> methods = (
                from tree in context.Compilation.SyntaxTrees
                from attribute in tree.GetRoot().DescendantNodes().OfType<AttributeSyntax>()
                let symbol = context.Compilation.GetSemanticModel(attribute.SyntaxTree)
                let typeInfo = symbol.GetTypeInfo(attribute)
                where typeInfo.Type is { Name: nameof(ShaderMethodAttribute) }
                select attribute.FirstAncestorOrSelf<MethodDeclarationSyntax>()).ToImmutableArray();

            foreach (MethodDeclarationSyntax methodDeclaration in methods)
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(methodDeclaration.SyntaxTree);
                IMethodSymbol methodDeclarationSymbol = semanticModel.GetDeclaredSymbol(methodDeclaration)!;

                // We need to sets to track all discovered custom types and static methods
                HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
                Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
                Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);

                // Explore the syntax tree and extract the processed info
                var (invokeMethod, processedMethods) = GetProcessedMethods(methodDeclaration, semanticModel, discoveredTypes, staticMethods, constantDefinitions);
                var processedTypes = IComputeShaderSourceGenerator.GetProcessedTypes(discoveredTypes).ToArray();
                var processedConstants = IComputeShaderSourceGenerator.GetProcessedConstants(constantDefinitions);

                // Create the compilation unit with the source attribute
                var source =
                    CompilationUnit().AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName(typeof(ShaderMethodSourceAttribute).FullName)).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(methodDeclarationSymbol.GetFullMetadataName(true)))),
                            AttributeArgument(ArrayExpression(processedTypes)),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(invokeMethod))),
                            AttributeArgument(ArrayExpression(processedMethods)),
                            AttributeArgument(NestedArrayExpression(processedConstants)))))
                    .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
                    .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                    .NormalizeWhitespace()
                    .ToFullString();

                // Add the method source attribute
                context.AddSource(methodDeclarationSymbol.GetGeneratedFileName<ShaderMethodSourceAttribute>(), SourceText.From(source, Encoding.UTF8));
            }
        }

        /// <summary>
        /// Gets a sequence of processed methods from a target method declaration.
        /// </summary>
        /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> instance for the current method.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the method to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of processed methods in <paramref name="methodDeclaration"/>.</returns>
        [Pure]
        private static (string InvokeMethod, IEnumerable<string> Methods) GetProcessedMethods(
            MethodDeclarationSyntax methodDeclaration,
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions)
        {
            ShaderSourceRewriter shaderSourceRewriter = new(semanticModel, discoveredTypes, staticMethods, constantDefinitions);

            // Rewrite the method syntax tree
            var processedMethod = shaderSourceRewriter
                .Visit(methodDeclaration)!
                .WithIdentifier(Identifier(ShaderMethodSourceAttribute.InvokeMethodIdentifier))
                .WithoutTrivia()
                .NormalizeWhitespace()
                .ToFullString();

            List<string> methods = new(shaderSourceRewriter.LocalFunctions.Count);

            // Emit the extracted local functions
            foreach (var localFunction in shaderSourceRewriter.LocalFunctions)
            {
                methods.Add(localFunction.Value.NormalizeWhitespace().ToFullString());
            }

            return (processedMethod, methods);
        }
    }
}

