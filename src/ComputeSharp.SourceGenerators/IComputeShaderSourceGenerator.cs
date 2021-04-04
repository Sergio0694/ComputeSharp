using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static ComputeSharp.SourceGenerators.Helpers.SyntaxFactoryHelper;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using ComputeSharp.SourceGenerators.Helpers;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators
{
    /// <summary>
    /// A source generator for processing <see cref="IComputeShader"/> types.
    /// </summary>
    [Generator]
    public sealed partial class IComputeShaderSourceGenerator : ISourceGenerator
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
                    OnExecute(context, item.StructDeclaration, semanticModel, item.StructSymbol);
                }
                catch
                {
                    context.ReportDiagnostic(IComputeShaderSourceGeneratorError, item.StructDeclaration, item.StructSymbol);
                }
            }
        }

        /// <summary>
        /// Processes a given target type.
        /// </summary>
        /// <param name="context">The input <see cref="GeneratorExecutionContext"/> instance to use.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> with metadata on the types being processed.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
        private static void OnExecute(GeneratorExecutionContext context, StructDeclarationSyntax structDeclaration, SemanticModelProvider semanticModel, INamedTypeSymbol structDeclarationSymbol)
        {
            // Properties are not supported
            DetectAndReportPropertyDeclarations(context, structDeclarationSymbol);

            // We need to sets to track all discovered custom types and static methods
            HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);

            // Explore the syntax tree and extract the processed info
            var processedMembers = GetProcessedFields(context, structDeclarationSymbol, discoveredTypes).ToArray();
            var sharedBuffers = GetGroupSharedMembers(context, structDeclarationSymbol, discoveredTypes).ToArray();
            var (entryPoint, processedMethods, forwardDeclarations) = GetProcessedMethods(context, structDeclaration, structDeclarationSymbol, semanticModel, discoveredTypes, staticMethods, constantDefinitions);
            var processedTypes = GetProcessedTypes(discoveredTypes).ToArray();
            var processedConstants = GetProcessedConstants(constantDefinitions);
            var staticFields = GetStaticFields(context, semanticModel, structDeclaration, structDeclarationSymbol, discoveredTypes, constantDefinitions);

            // Create the compilation unit with the source attribute
            var source =
                CompilationUnit().AddUsings(
                UsingDirective(IdentifierName("ComputeSharp.__Internals"))).AddAttributeLists(
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName(typeof(IComputeShaderSourceAttribute).FullName)).AddArgumentListArguments(
                        AttributeArgument(TypeOfExpression(IdentifierName(structDeclarationSymbol.ToDisplayString()))),
                        AttributeArgument(ArrayExpression(processedTypes)),
                        AttributeArgument(NestedArrayExpression(processedMembers)),
                        AttributeArgument(ArrayExpression(forwardDeclarations)),
                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(entryPoint))),
                        AttributeArgument(ArrayExpression(processedMethods)),
                        AttributeArgument(NestedArrayExpression(processedConstants)),
                        AttributeArgument(NestedArrayExpression(staticFields)),
                        AttributeArgument(NestedArrayExpression(sharedBuffers)))))
                .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
                .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                .NormalizeWhitespace()
                .ToFullString();

            // Add the method source attribute
            context.AddSource(structDeclarationSymbol.GetGeneratedFileName<IComputeShaderSourceGenerator>(), SourceText.From(source, Encoding.UTF8));
        }

        /// <summary>
        /// Gets a sequence of captured fields and their mapped names.
        /// </summary>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of captured fields in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<IEnumerable<string>> GetProcessedFields(
            GeneratorExecutionContext context,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> types)
        {
            bool hlslResourceFound = false;

            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (fieldSymbol.IsStatic) continue;

                AttributeData? attribute = fieldSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass is { Name: nameof(GroupSharedAttribute) });

                // Group shared fields must be static
                if (attribute is not null)
                {
                    context.ReportDiagnostic(InvalidGroupSharedFieldDeclaration, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name);
                }

                // Captured fields must be named type symbols
                if (fieldSymbol.Type is not INamedTypeSymbol typeSymbol)
                {
                    context.ReportDiagnostic(InvalidShaderField, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type);

                    continue;
                }

                string metadataName = typeSymbol.GetFullMetadataName();

                // Allowed fields must be either resources, unmanaged values or delegates
                if (HlslKnownTypes.IsTypedResourceType(metadataName))
                {
                    hlslResourceFound = true;

                    // Track the type of items in the current buffer
                    if (HlslKnownTypes.IsStructuredBufferType(metadataName))
                    {
                        types.Add((INamedTypeSymbol)typeSymbol.TypeArguments[0]);
                    }
                }
                else if (!typeSymbol.IsUnmanagedType &&
                         typeSymbol.TypeKind != TypeKind.Delegate)
                {
                    // Shaders can only capture valid HLSL resource types or unmanaged types
                    context.ReportDiagnostic(InvalidShaderField, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name, typeSymbol);

                    continue;
                }
                else if (!HlslKnownTypes.IsKnownHlslType(metadataName))
                {
                    // Track the type if it's a custom struct
                    types.Add(typeSymbol);
                }

                string typeName = HlslKnownTypes.GetMappedName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                // Yield back the current mapping for the name (if the name used a reserved keyword)
                yield return new[] { fieldSymbol.Name, mapping ?? fieldSymbol.Name, typeName };
            }

            if (!hlslResourceFound)
            {
                context.ReportDiagnostic(MissingShaderResources, structDeclarationSymbol, structDeclarationSymbol);
            }
        }

        /// <summary>
        /// Gets a sequence of shader static fields and their mapped names.
        /// </summary>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of static constant fields in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<IEnumerable<string?>> GetStaticFields(
            GeneratorExecutionContext context,
            SemanticModelProvider semanticModel,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions)
        {
            foreach (var fieldDeclaration in structDeclaration.Members.OfType<FieldDeclarationSyntax>())
            {
                foreach (var variableDeclarator in fieldDeclaration.Declaration.Variables)
                {
                    IFieldSymbol fieldSymbol = (IFieldSymbol)semanticModel.For(variableDeclarator).GetDeclaredSymbol(variableDeclarator)!;

                    if (!fieldSymbol.IsStatic || fieldSymbol.IsConst)
                    {
                        continue;
                    }

                    AttributeData? attribute = fieldSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass is { Name: nameof(GroupSharedAttribute) });

                    if (attribute is not null) continue;

                    // Constant properties must be of a primitive, vector or matrix type
                    if (fieldSymbol.Type is not INamedTypeSymbol typeSymbol ||
                        !HlslKnownTypes.IsKnownHlslType(typeSymbol.GetFullMetadataName()))
                    {
                        context.ReportDiagnostic(InvalidShaderStaticFieldType, variableDeclarator, structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type);

                        continue;
                    }

                    _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                    string typeDeclaration = fieldSymbol.IsReadOnly switch
                    {
                        true => $"static const {HlslKnownTypes.GetMappedName(typeSymbol)}",
                        false => $"static {HlslKnownTypes.GetMappedName(typeSymbol)}"
                    };

                    StaticFieldRewriter staticFieldRewriter = new(
                        semanticModel,
                        discoveredTypes,
                        constantDefinitions,
                        context);

                    string? assignment = staticFieldRewriter.Visit(variableDeclarator)?.NormalizeWhitespace().ToFullString();

                    yield return new[] { mapping ?? fieldSymbol.Name, typeDeclaration, assignment };
                }
            }
        }

        /// <summary>
        /// Gets a sequence of captured members and their mapped names.
        /// </summary>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of captured members in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<(string Name, string Type, int? Count)> GetGroupSharedMembers(
            GeneratorExecutionContext context,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> types)
        {
            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (!fieldSymbol.IsStatic) continue;

                AttributeData? attribute = fieldSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass is { Name: nameof(GroupSharedAttribute) });

                if (attribute is null) continue;

                if (fieldSymbol.Type is not IArrayTypeSymbol typeSymbol)
                {
                    context.ReportDiagnostic(InvalidGroupSharedFieldType, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type);

                    continue;
                }

                if (!typeSymbol.ElementType.IsUnmanagedType)
                {
                    context.ReportDiagnostic(InvalidGroupSharedFieldElementType, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type);

                    continue;
                }
                
                int? bufferSize = (int?)attribute.ConstructorArguments.FirstOrDefault().Value;

                string typeName = HlslKnownTypes.GetMappedElementName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                yield return (mapping ?? fieldSymbol.Name, typeName, bufferSize);

                types.Add((INamedTypeSymbol)typeSymbol.ElementType);
            }
        }

        /// <summary>
        /// Gets a sequence of processed methods declared within a given type.
        /// </summary>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>, and the entry point.</returns>
        [Pure]
        private static (string EntryPoint, IEnumerable<string> Methods, IEnumerable<string> Declarations) GetProcessedMethods(
            GeneratorExecutionContext context,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            SemanticModelProvider semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions)
        {
            // Find all declared methods in the type
            ImmutableArray<MethodDeclarationSyntax> methodDeclarations = (
                from syntaxNode in structDeclaration.DescendantNodes()
                where syntaxNode.IsKind(SyntaxKind.MethodDeclaration)
                select (MethodDeclarationSyntax)syntaxNode).ToImmutableArray();

            string? entryPoint = null;
            List<string> methods = new();
            List<string> declarations = new();

            foreach (MethodDeclarationSyntax methodDeclaration in methodDeclarations)
            {
                IMethodSymbol methodDeclarationSymbol = semanticModel.For(methodDeclaration).GetDeclaredSymbol(methodDeclaration)!;
                bool isShaderEntryPoint =
                    methodDeclarationSymbol.Name == nameof(IComputeShader.Execute) &&
                    methodDeclarationSymbol.ReturnsVoid &&
                    methodDeclarationSymbol.TypeParameters.Length == 0 &&
                    methodDeclarationSymbol.Parameters.Length == 0;

                // Create the source rewriter for the current method
                ShaderSourceRewriter shaderSourceRewriter = new(
                    structDeclarationSymbol,
                    semanticModel,
                    discoveredTypes,
                    staticMethods,
                    constantDefinitions,
                    context,
                    isShaderEntryPoint);

                // Rewrite the method syntax tree
                MethodDeclarationSyntax? processedMethod = shaderSourceRewriter.Visit(methodDeclaration)!.WithoutTrivia();

                // Emit the extracted local functions first
                foreach (var localFunction in shaderSourceRewriter.LocalFunctions)
                {
                    methods.Add(localFunction.Value.NormalizeWhitespace().ToFullString());
                    declarations.Add(localFunction.Value.AsDefinition().NormalizeWhitespace().ToFullString());
                }

                // If the method is the shader entry point, do additional processing
                if (isShaderEntryPoint)
                {
                    processedMethod = new ExecuteMethodRewriter(shaderSourceRewriter).Visit(processedMethod)!;

                    entryPoint = processedMethod.NormalizeWhitespace().ToFullString();
                }
                else
                {
                    methods.Add(processedMethod.NormalizeWhitespace().ToFullString());
                    declarations.Add(processedMethod.AsDefinition().NormalizeWhitespace().ToFullString());
                }
            }

            // Process static methods as well
            foreach (MethodDeclarationSyntax staticMethod in staticMethods.Values)
            {
                methods.Add(staticMethod.NormalizeWhitespace().ToFullString());
                declarations.Add(staticMethod.AsDefinition().NormalizeWhitespace().ToFullString());
            }

            return (entryPoint!, methods, declarations);
        }

        /// <summary>
        /// Gets a sequence of discovered constants.
        /// </summary>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of discovered constants to declare in the shader.</returns>
        [Pure]
        internal static IEnumerable<IEnumerable<string>> GetProcessedConstants(IReadOnlyDictionary<IFieldSymbol, string> constantDefinitions)
        {
            foreach (var constant in constantDefinitions)
            {
                var ownerTypeName = ((INamedTypeSymbol)constant.Key.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
                var constantName = $"__{ownerTypeName}__{constant.Key.Name}";

                yield return new string[] { constantName, constant.Value };
            }
        }

        /// <summary>
        /// Gets the sequence of processed discovered custom types.
        /// </summary>
        /// <param name="types">The sequence of discovered custom types.</param>
        /// <returns>A sequence of custom type definitions to add to the shader source.</returns>
        internal static IEnumerable<string> GetProcessedTypes(IEnumerable<INamedTypeSymbol> types)
        {
            foreach (var type in HlslKnownTypes.GetCustomTypes(types))
            {
                var structType = type.GetFullMetadataName().ToHlslIdentifierName();
                var structDeclaration = StructDeclaration(structType);

                // Declare the fields of the current type
                foreach (var field in type.GetMembers().OfType<IFieldSymbol>())
                {
                    INamedTypeSymbol fieldType = (INamedTypeSymbol)field.Type;

                    // Convert the name to the fully qualified HLSL version
                    if (!HlslKnownTypes.TryGetMappedName(fieldType.GetFullMetadataName(), out string? mapped))
                    {
                        mapped = fieldType.GetFullMetadataName().ToHlslIdentifierName();
                    }

                    structDeclaration = structDeclaration.AddMembers(
                        FieldDeclaration(VariableDeclaration(
                            IdentifierName(mapped!)).AddVariables(
                            VariableDeclarator(Identifier(field.Name)))));
                }

                // Insert the trailing ; right after the closing bracket (after normalization)
                yield return
                    structDeclaration
                    .NormalizeWhitespace()
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                    .ToFullString();
            }
        }

        /// <summary>
        /// Finds and reports all declared properties in a shader.
        /// </summary>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        private static void DetectAndReportPropertyDeclarations(GeneratorExecutionContext context, INamedTypeSymbol structDeclarationSymbol)
        {
            foreach (var propertySymbol in structDeclarationSymbol.GetMembers().OfType<IPropertySymbol>())
            {
                context.ReportDiagnostic(DiagnosticDescriptors.PropertyDeclaration, propertySymbol);
            }
        }
    }
}

