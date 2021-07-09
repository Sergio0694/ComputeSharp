using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Helpers;
using ComputeSharp.SourceGenerators.Mappings;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static ComputeSharp.SourceGenerators.Helpers.SyntaxFactoryHelper;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

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

            // A given type can only represent a single shader type
            if (structDeclarationSymbol.AllInterfaces.Count(static interfaceSymbol => interfaceSymbol is { Name: nameof(IComputeShader) } or { IsGenericType: true, Name: nameof(IPixelShader<byte>) }) > 1)
            {
                context.ReportDiagnostic(MultipleShaderTypesImplemented, structDeclarationSymbol, structDeclarationSymbol);
            }

            // Explore the syntax tree and extract the processed info
            var pixelShaderSymbol = structDeclarationSymbol.AllInterfaces.FirstOrDefault(static interfaceSymbol => interfaceSymbol is { IsGenericType: true, Name: nameof(IPixelShader<byte>) });
            var isComputeShader = pixelShaderSymbol is null;
            var implicitTextureType = isComputeShader ? null : HlslKnownTypes.GetMappedNameForPixelShaderType(pixelShaderSymbol!);
            var processedMembers = GetProcessedFields(context, structDeclarationSymbol, discoveredTypes, isComputeShader).ToArray();
            var sharedBuffers = GetGroupSharedMembers(context, structDeclarationSymbol, discoveredTypes).ToArray();
            var (entryPoint, processedMethods, forwardDeclarations, isSamplerUsed) = GetProcessedMethods(context, structDeclaration, structDeclarationSymbol, semanticModel, discoveredTypes, staticMethods, constantDefinitions, isComputeShader);
            var implicitSamplerField = isSamplerUsed ? ("SamplerState", "__sampler") : default((string, string)?);
            var processedTypes = GetProcessedTypes(discoveredTypes).ToArray();
            var processedConstants = GetProcessedConstants(constantDefinitions);
            var staticFields = GetStaticFields(context, semanticModel, structDeclaration, structDeclarationSymbol, discoveredTypes, constantDefinitions);

            GenerateRenderMethod(
                context,
                structDeclaration,
                structDeclarationSymbol,
                processedConstants,
                staticFields,
                processedTypes,
                isComputeShader,
                processedMembers,
                implicitTextureType,
                isSamplerUsed,
                sharedBuffers,
                forwardDeclarations,
                processedMethods,
                entryPoint);

            // Create the compilation unit with the source attribute
            var source =
                CompilationUnit().AddUsings(
                UsingDirective(IdentifierName("ComputeSharp.__Internals"))).AddAttributeLists(
                AttributeList(SingletonSeparatedList(
                    Attribute(IdentifierName(typeof(IComputeShaderSourceAttribute).FullName)).AddArgumentListArguments(
                        AttributeArgument(TypeOfExpression(IdentifierName(structDeclarationSymbol.ToDisplayString()))),
                        AttributeArgument(ArrayExpression(Array.Empty<string>())), // TODO
                        AttributeArgument(ArrayExpression(Array.Empty<string>())), // TODO
                        AttributeArgument(ArrayExpression(Array.Empty<string>())), // TODO
                        AttributeArgument(NestedArrayExpression(Array.Empty<string[]>())), // TODO
                        AttributeArgument(ArrayExpression(Array.Empty<string>())), // TODO
                        AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(""))), // TODO
                        AttributeArgument(ArrayExpression(Array.Empty<string>())), // TODO
                        AttributeArgument(NestedArrayExpression(Array.Empty<string[]>())), // TODO
                        AttributeArgument(NestedArrayExpression(Array.Empty<string[]>())), // TODO
                        AttributeArgument(NestedArrayExpression(Array.Empty<string[]>()))))) // TODO
                .WithOpenBracketToken(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.OpenBracketToken, TriviaList()))
                .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword))))
                .NormalizeWhitespace()
                .ToFullString();

            // Add the method source attribute
            context.AddSource(structDeclarationSymbol.GetGeneratedFileName(), SourceText.From(source, Encoding.UTF8));
        }

        /// <summary>
        /// Gets a sequence of captured fields and their mapped names.
        /// </summary>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <param name="isComputeShader">Indicates whether or not <paramref name="structDeclarationSymbol"/> represents a compute shader.</param>
        /// <returns>A sequence of captured fields in <paramref name="structDeclarationSymbol"/>.</returns>
        [Pure]
        private static IEnumerable<(IFieldSymbol Symbol, string HlslName, string HlslType)> GetProcessedFields(
            GeneratorExecutionContext context,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> types,
            bool isComputeShader)
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
                else if (typeSymbol.IsUnmanagedType &&
                         !HlslKnownTypes.IsKnownHlslType(metadataName))
                {
                    // Track the type if it's a custom struct
                    types.Add(typeSymbol);
                }

                string typeName = HlslKnownTypes.GetMappedName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                // Yield back the current mapping for the name (if the name used a reserved keyword)
                yield return (fieldSymbol, mapping ?? fieldSymbol.Name, typeName);
            }

            // If the shader is a compute one (so no implicit output texture), it has to contain at least one resource
            if (!hlslResourceFound && isComputeShader)
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
        private static IEnumerable<(string Name, string TypeDeclaration, string? Assignment)> GetStaticFields(
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

                    yield return (mapping ?? fieldSymbol.Name, typeDeclaration, assignment);
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
        /// <param name="isComputeShader">Indicates whether or not <paramref name="structDeclarationSymbol"/> represents a compute shader.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>, and the entry point.</returns>
        [Pure]
        private static (string EntryPoint, IEnumerable<string> Methods, IEnumerable<string> Declarations, bool IsSamplerUser) GetProcessedMethods(
            GeneratorExecutionContext context,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            SemanticModelProvider semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            bool isComputeShader)
        {
            // Find all declared methods in the type
            ImmutableArray<MethodDeclarationSyntax> methodDeclarations = (
                from syntaxNode in structDeclaration.DescendantNodes()
                where syntaxNode.IsKind(SyntaxKind.MethodDeclaration)
                select (MethodDeclarationSyntax)syntaxNode).ToImmutableArray();

            string? entryPoint = null;
            List<string> methods = new();
            List<string> declarations = new();
            bool isSamplerUsed = false;

            foreach (MethodDeclarationSyntax methodDeclaration in methodDeclarations)
            {
                IMethodSymbol methodDeclarationSymbol = semanticModel.For(methodDeclaration).GetDeclaredSymbol(methodDeclaration)!;
                bool isShaderEntryPoint =
                    (isComputeShader &&
                     methodDeclarationSymbol.Name == nameof(IComputeShader.Execute) &&
                     methodDeclarationSymbol.ReturnsVoid &&
                     methodDeclarationSymbol.TypeParameters.Length == 0 &&
                     methodDeclarationSymbol.Parameters.Length == 0) ||
                    (!isComputeShader &&
                     methodDeclarationSymbol.Name == nameof(IPixelShader<byte>.Execute) &&
                     methodDeclarationSymbol.ReturnType is not null && // TODO: match for pixel type
                     methodDeclarationSymbol.TypeParameters.Length == 0 &&
                     methodDeclarationSymbol.Parameters.Length == 0);

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

                // Track the implicit sampler, if used
                isSamplerUsed = isSamplerUsed || shaderSourceRewriter.IsSamplerUsed;

                // Emit the extracted local functions first
                foreach (var localFunction in shaderSourceRewriter.LocalFunctions)
                {
                    methods.Add(localFunction.Value.NormalizeWhitespace().ToFullString());
                    declarations.Add(localFunction.Value.AsDefinition().NormalizeWhitespace().ToFullString());
                }

                // If the method is the shader entry point, do additional processing
                if (isShaderEntryPoint)
                {
                    processedMethod = isComputeShader switch
                    {
                        true => new ExecuteMethodRewriter.Compute(shaderSourceRewriter).Visit(processedMethod)!,
                        false => new ExecuteMethodRewriter.Pixel(shaderSourceRewriter).Visit(processedMethod)!
                    };

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

            return (entryPoint!, methods, declarations, isSamplerUsed);
        }

        /// <summary>
        /// Gets a sequence of discovered constants.
        /// </summary>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of discovered constants to declare in the shader.</returns>
        [Pure]
        internal static IEnumerable<(string Name, string Value)> GetProcessedConstants(IReadOnlyDictionary<IFieldSymbol, string> constantDefinitions)
        {
            foreach (var constant in constantDefinitions)
            {
                var ownerTypeName = ((INamedTypeSymbol)constant.Key.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
                var constantName = $"__{ownerTypeName}__{constant.Key.Name}";

                yield return (constantName, constant.Value);
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

        private static void GenerateRenderMethod(
            GeneratorExecutionContext context,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            IEnumerable<(string Name, string Value)> definedConstants,
            IEnumerable<(string Name, string TypeDeclaration, string? Assignment)> staticFields,
            IEnumerable<string> declaredTypes,
            bool isComputeShader,
            IEnumerable<(IFieldSymbol Symbol, string HlslName, string HlslType)> instanceFields,
            string? implicitTextureType,
            bool isSamplerUsed,
            IEnumerable<(string Name, string Type, int? Count)> sharedBuffers,
            IEnumerable<string>? forwardDeclarations,
            IEnumerable<string> processedMethods,
            string executeMethod)
        {
            string namespaceName = structDeclarationSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces));
            string structName = structDeclaration.Identifier.Text;
            SyntaxTokenList structModifiers = structDeclaration.Modifiers;
            IEnumerable<StatementSyntax> bodyStatements = GenerateRenderMethodBody(
                definedConstants,
                staticFields,
                declaredTypes,
                isComputeShader,
                instanceFields,
                implicitTextureType,
                isSamplerUsed,
                sharedBuffers,
                forwardDeclarations,
                processedMethods,
                executeMethod);

            // Create the partial shader type declaration with the hashcode interface method implementation.
            // This code produces a struct declaration as follows:
            //
            // public struct ShaderType : IShader<ShaderType>
            // {
            //     [GeneratedCode("...", "...")]
            //     [DebuggerNonUserCode]
            //     [ExcludeFromCodeCoverage]
            //     [EditorBrowsable(EditorBrowsableState.Never)]
            //     [Obsolete("This method is not intended to be called directly by user code")]
            //     public readonly void BuildHlslString(ref ArrayPoolStringBuilder builder, int threadsX, int threadsY, int threadsZ)
            //     {
            //         <BODY>
            //     }
            // }
            var structDeclarationSyntax =
                StructDeclaration(structName).WithModifiers(structModifiers)
                    .AddBaseListTypes(SimpleBaseType(
                        GenericName(Identifier("IShader"))
                        .AddTypeArgumentListArguments(IdentifierName(structName)))).AddMembers(
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("BuildHlslString"))
                .AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("GeneratedCode")).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(IComputeShaderHashCodeGenerator).FullName))),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(IComputeShaderHashCodeGenerator).Assembly.GetName().Version.ToString())))))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("DebuggerNonUserCode")))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("ExcludeFromCodeCoverage")))),
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("EditorBrowsable")).AddArgumentListArguments(
                        AttributeArgument(ParseExpression("EditorBrowsableState.Never"))))),
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("Obsolete")).AddArgumentListArguments(
                        AttributeArgument(LiteralExpression(
                            SyntaxKind.StringLiteralExpression,
                            Literal("This method is not intended to be used directly by user code")))))))
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.ReadOnlyKeyword))
                .AddParameterListParameters(
                    Parameter(Identifier("builder")).AddModifiers(Token(SyntaxKind.RefKeyword)).WithType(IdentifierName("ArrayPoolStringBuilder")),
                    Parameter(Identifier("threadsX")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("threadsY")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("threadsZ")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))))
                .WithBody(Block(bodyStatements)));

            TypeDeclarationSyntax typeDeclarationSyntax = structDeclarationSyntax;

            // Add all parent types in ascending order, if any
            foreach (var parentType in structDeclaration.Ancestors().OfType<TypeDeclarationSyntax>())
            {
                typeDeclarationSyntax = parentType
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(typeDeclarationSyntax))
                    .WithConstraintClauses(List<TypeParameterConstraintClauseSyntax>())
                    .WithBaseList(null)
                    .WithAttributeLists(List<AttributeListSyntax>())
                    .WithoutTrivia();
            }

            // Create a static method to create the combined hashcode for a given shader type.
            // This code takes a block syntax and produces a compilation unit as follows:
            //
            // using System;
            // using System.CodeDom.Compiler;
            // using System.ComponentModel;
            // using System.Diagnostics;
            // using System.Diagnostics.CodeAnalysis;
            // using ComputeSharp.__Internals;
            //
            // #pragma warning disable
            //
            // namespace <SHADER_NAMESPACE>
            // {
            //     <SHADER_DECLARATION>
            // }
            var source =
                CompilationUnit().AddUsings(
                UsingDirective(IdentifierName("System")),
                UsingDirective(IdentifierName("System.CodeDom.Compiler")),
                UsingDirective(IdentifierName("System.ComponentModel")),
                UsingDirective(IdentifierName("System.Diagnostics")),
                UsingDirective(IdentifierName("System.Diagnostics.CodeAnalysis")),
                UsingDirective(IdentifierName("ComputeSharp.__Internals"))).AddMembers(
                NamespaceDeclaration(IdentifierName(namespaceName)).AddMembers(typeDeclarationSyntax)
                .WithNamespaceKeyword(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.NamespaceKeyword, TriviaList())))
                .NormalizeWhitespace()
                .ToFullString();

            // Add the method source attribute
            context.AddSource("PREVIEW" + structDeclarationSymbol.GetGeneratedFileName(), SourceText.From(source, Encoding.UTF8));
        }

        private static IEnumerable<StatementSyntax> GenerateRenderMethodBody(
            IEnumerable<(string Name, string Value)> definedConstants,
            IEnumerable<(string Name, string TypeDeclaration, string? Assignment)> staticFields,
            IEnumerable<string> declaredTypes,
            bool isComputeShader,
            IEnumerable<(IFieldSymbol Symbol, string HlslName, string HlslType)> instanceFields,
            string? implicitTextureType,
            bool isSamplerUsed,
            IEnumerable<(string Name, string Type, int? Count)> sharedBuffers,
            IEnumerable<string>? forwardDeclarations,
            IEnumerable<string> processedMethods,
            string executeMethod)
        {
            // Header
            yield return ParseStatement("builder.AppendLine(\"// ================================================\");");
            yield return ParseStatement("builder.AppendLine(\"//                  AUTO GENERATED\");");
            yield return ParseStatement("builder.AppendLine(\"// ================================================\");");
            yield return ParseStatement("builder.AppendLine(\"// This shader was created by ComputeSharp.\");");
            yield return ParseStatement("builder.AppendLine(\"// See: https://github.com/Sergio0694/ComputeSharp.\");");

            // Group size constants
            yield return ParseStatement("builder.AppendLine();");
            yield return ParseStatement("builder.Append(\"#define __GroupSize__get_X \");");
            yield return ParseStatement("builder.AppendLine(threadsX.ToString());");
            yield return ParseStatement("builder.Append(\"#define __GroupSize__get_Y \");");
            yield return ParseStatement("builder.AppendLine(threadsY.ToString());");
            yield return ParseStatement("builder.Append(\"#define __GroupSize__get_Z \");");
            yield return ParseStatement("builder.AppendLine(threadsZ.ToString());");

            // Define declarations
            foreach (var (name, value) in definedConstants)
            {
                yield return ParseStatement($"builder.AppendLine(\"#define {name} {value}\");");
            }

            // Static fields
            if (staticFields.Any())
            {
                yield return ParseStatement("builder.AppendLine();");

                foreach (var field in staticFields)
                {
                    if (field.Assignment is string assignment)
                    {
                        yield return ParseStatement($"builder.AppendLine(\"{field.TypeDeclaration} {field.Name} = {assignment};\");");
                    }
                    else
                    {
                        yield return ParseStatement($"builder.AppendLine(\"{field.TypeDeclaration} {field.Name};\");");
                    }
                }
            }

            // Declared types
            foreach (var type in declaredTypes)
            {
                yield return ParseStatement("builder.AppendLine();");
                yield return ParseStatement($"builder.AppendLine(\"{type}\");");
            }

            // Captured variables
            yield return ParseStatement("builder.AppendLine();");
            yield return ParseStatement("builder.AppendLine(\"cbuffer _ : register(b0)\");");
            yield return ParseStatement("builder.AppendLine('{');");
            yield return ParseStatement("builder.AppendLine(\"    uint __x;\");");
            yield return ParseStatement("builder.AppendLine(\"    uint __y;\");");

            if (isComputeShader)
            {
                yield return ParseStatement("builder.AppendLine(\"    uint __z;\");");
            }

            // User-defined values
            foreach (var instanceField in instanceFields)
            {
                if (instanceField.Symbol.Type.IsUnmanagedType)
                {
                    yield return ParseStatement($"builder.AppendLine(\"    {instanceField.HlslType} {instanceField.HlslName};\");");
                }
            }

            yield return ParseStatement("builder.AppendLine('}');");

            int
                constantBuffersCount = 0,
                readOnlyBuffersCount = 0,
                readWriteBuffersCount = 0;

            // Optional implicit texture field
            if (!isComputeShader)
            {
                yield return ParseStatement("builder.AppendLine();");
                yield return ParseStatement($"builder.AppendLine(\"{implicitTextureType} __outputTexture : register(u{readWriteBuffersCount++});\");");
            }

            // Optional sampler field
            if (isSamplerUsed)
            {
                yield return ParseStatement("builder.AppendLine();");
                yield return ParseStatement("builder.AppendLine(\"SamplerState __sampler : register(s);\");");
            }

            // Resources
            foreach (var instanceField in instanceFields)
            {
                string metadataName = instanceField.Symbol.GetFullMetadataName();

                if (HlslKnownTypes.IsConstantBufferType(metadataName))
                {
                    yield return ParseStatement("builder.AppendLine();");
                    yield return ParseStatement($"builder.AppendLine(\"cbuffer _{instanceField.HlslName} : register(b{constantBuffersCount++})\");");
                    yield return ParseStatement("builder.AppendLine('{');");
                    yield return ParseStatement($"builder.AppendLine(\"    {instanceField.HlslType} {instanceField.HlslName}[2];\");");
                    yield return ParseStatement("builder.AppendLine('}');");
                }
                else if (HlslKnownTypes.IsReadOnlyTypedResourceType(metadataName))
                {
                    yield return ParseStatement("builder.AppendLine();");
                    yield return ParseStatement($"builder.AppendLine(\"{instanceField.HlslType} : register(t{readOnlyBuffersCount++});\");");
                }
                else if (HlslKnownTypes.IsTypedResourceType(metadataName))
                {
                    yield return ParseStatement("builder.AppendLine();");
                    yield return ParseStatement($"builder.AppendLine(\"{instanceField.HlslType} : register(u{readWriteBuffersCount++});\");");
                }
            }

            // Shared buffers
            foreach (var sharedBuffer in sharedBuffers)
            {
                object count = (object?)sharedBuffer.Count ?? "threadsX * threadsY * threadsZ";

                yield return ParseStatement("builder.AppendLine();");
                yield return ParseStatement($"builder.AppendLine(\"groupshared {sharedBuffer.Type} {sharedBuffer.Name} [{count}];\");");
            }

            // Forward declarations
            if (forwardDeclarations is not null)
            {
                foreach (var forwardDeclaration in forwardDeclarations)
                {
                    yield return ParseStatement("builder.AppendLine();");
                    yield return ParseStatement($"builder.AppendLine(\"{forwardDeclaration}\");");
                }
            }

            // Captured methods
            if (processedMethods is not null)
            {
                foreach (var method in processedMethods)
                {
                    yield return ParseStatement("builder.AppendLine();");
                    yield return
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("builder"), IdentifierName("AppendLine")))
                                .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(method)))));
                }
            }

            // Entry point
            yield return ParseStatement("builder.AppendLine();");
            yield return ParseStatement("builder.AppendLine(\"[NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]\");");
            yield return
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("builder"), IdentifierName("AppendLine")))
                        .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(executeMethod)))));
        }
    }
}

