using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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

                // Explore the syntax tree and extract the processed info
                var (invokeMethod, processedMethods) = GetProcessedMethods(methodDeclaration, semanticModel, discoveredTypes, staticMethods);
                var processedTypes = IComputeShaderSourceGenerator.GetProcessedTypes(discoveredTypes).ToArray();

                // Helper that converts a sequence of strings into an array expression.
                // That is, this applies the following transformation:
                //   - { "S1", "S2" } => new string[] { "S1", "S2" }
                static ArrayCreationExpressionSyntax ArrayExpression(IEnumerable<string> values)
                {
                    return
                        ArrayCreationExpression(
                        ArrayType(PredefinedType(Token(SyntaxKind.StringKeyword)))
                        .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                        .WithInitializer(InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                        .AddExpressions(values.Select(static value => LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(value))).ToArray()));
                }

                // Create the compilation unit with the source attribute
                var source =
                    CompilationUnit().AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName(typeof(ShaderMethodSourceAttribute).FullName)).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(methodDeclarationSymbol.GetFullMetadataName(true)))),
                            AttributeArgument(ArrayExpression(processedTypes)),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(invokeMethod))),
                            AttributeArgument(ArrayExpression(processedMethods)))))
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
        /// <returns>A sequence of processed methods in <paramref name="methodDeclaration"/>.</returns>
        [Pure]
        private static (string InvokeMethod, IEnumerable<string> Methods) GetProcessedMethods(
            MethodDeclarationSyntax methodDeclaration,
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods)
        {
            ShaderSourceRewriter shaderSourceRewriter = new(semanticModel, discoveredTypes, staticMethods);

            // Rewrite the method syntax tree
            var processedMethod = shaderSourceRewriter
                .Visit(methodDeclaration)!
                .WithIdentifier(Identifier("__<NAME>__"))
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

