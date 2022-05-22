using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.D2D1.SourceGenerators.SyntaxRewriters;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.D2D1.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618, RS1024

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>BuildHlslSource</c> method.
    /// </summary>
    private static partial class BuildHlslSource
    {
        /// <summary>
        /// Gathers all necessary information on a transpiled HLSL source for a given shader type.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <param name="inputSimpleIndices">The indicess of the simple shader inputs.</param>
        /// <param name="inputComplexIndices">The indicess of the complex shader inputs.</param>
        /// <returns>The HLSL source for the shader.</returns>
        public static string GetHlslSource(
            ImmutableArray<Diagnostic>.Builder diagnostics,
            Compilation compilation,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            int inputCount,
            ImmutableArray<int> inputSimpleIndices,
            ImmutableArray<int> inputComplexIndices)
        {
            // Properties are not supported
            DetectAndReportInvalidPropertyDeclarations(diagnostics, structDeclarationSymbol);

            // We need to sets to track all discovered custom types and static methods
            HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);

            // Explore the syntax tree and extract the processed info
            var semanticModelProvider = new SemanticModelProvider(compilation);
            var valueFields = GetInstanceFields(diagnostics, structDeclarationSymbol, discoveredTypes);
            var (entryPoint, processedMethods) = GetProcessedMethods(diagnostics, structDeclaration, structDeclarationSymbol, semanticModelProvider, discoveredTypes, staticMethods, constantDefinitions);
            var staticFields = GetStaticFields(diagnostics, semanticModelProvider, structDeclaration, structDeclarationSymbol, discoveredTypes, constantDefinitions);

            // Process the discovered types and constants
            var declaredTypes = GetDeclaredTypes(diagnostics, structDeclarationSymbol, discoveredTypes);
            var definedConstants = GetDefinedConstants(constantDefinitions);

            // Check whether the scene position is required
            bool requiresScenePosition = GetRequiresScenePositionInfo(structDeclarationSymbol);

            // Get the HLSL source
            return GetHlslSource(
                definedConstants,
                declaredTypes,
                valueFields,
                staticFields,
                processedMethods,
                entryPoint,
                inputCount,
                inputSimpleIndices,
                inputComplexIndices,
                requiresScenePosition);
        }

        /// <summary>
        /// Gets a sequence of captured fields and their mapped names.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of captured fields in <paramref name="structDeclarationSymbol"/>.</returns>
        private static ImmutableArray<(string Name, string HlslType)> GetInstanceFields(
            ImmutableArray<Diagnostic>.Builder diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> types)
        {
            ImmutableArray<(string, string)>.Builder values = ImmutableArray.CreateBuilder<(string, string)>();

            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (fieldSymbol.IsStatic)
                {
                    continue;
                }

                // Captured fields must be named type symbols
                if (fieldSymbol.Type is not INamedTypeSymbol typeSymbol)
                {
                    diagnostics.Add(InvalidShaderField, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type);

                    continue;
                }

                string metadataName = typeSymbol.GetFullMetadataName();
                string typeName = HlslKnownTypes.GetMappedName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                // Allowed fields must be unmanaged values
                if (typeSymbol.IsUnmanagedType)
                {
                    // Track the type if it's a custom struct
                    if (!HlslKnownTypes.IsKnownHlslType(metadataName))
                    {
                        types.Add(typeSymbol);
                    }

                    values.Add((mapping ?? fieldSymbol.Name, typeName));
                }
                else
                {
                    diagnostics.Add(InvalidShaderField, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name, typeSymbol);
                }
            }

            return values.ToImmutable();
        }

        /// <summary>
        /// Gets a sequence of shader static fields and their mapped names.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of static constant fields in <paramref name="structDeclarationSymbol"/>.</returns>
        private static ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> GetStaticFields(
            ImmutableArray<Diagnostic>.Builder diagnostics,
            SemanticModelProvider semanticModel,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions)
        {
            ImmutableArray<(string, string, string?)>.Builder builder = ImmutableArray.CreateBuilder<(string, string, string?)>();

            foreach (var fieldDeclaration in structDeclaration.Members.OfType<FieldDeclarationSyntax>())
            {
                foreach (var variableDeclarator in fieldDeclaration.Declaration.Variables)
                {
                    IFieldSymbol fieldSymbol = (IFieldSymbol)semanticModel.For(variableDeclarator).GetDeclaredSymbol(variableDeclarator)!;

                    if (!fieldSymbol.IsStatic || fieldSymbol.IsConst)
                    {
                        continue;
                    }

                    // Constant properties must be of a primitive, vector or matrix type
                    if (fieldSymbol.Type is not INamedTypeSymbol typeSymbol ||
                        !HlslKnownTypes.IsKnownHlslType(typeSymbol.GetFullMetadataName()))
                    {
                        diagnostics.Add(InvalidShaderStaticFieldType, variableDeclarator, structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type);

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
                        diagnostics);

                    string? assignment = staticFieldRewriter.Visit(variableDeclarator)?.NormalizeWhitespace(eol: "\n").ToFullString();

                    builder.Add((mapping ?? fieldSymbol.Name, typeDeclaration, assignment));
                }
            }

            return builder.ToImmutable();
        }

        /// <summary>
        /// Gets a sequence of processed methods declared within a given type.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>, and the entry point.</returns>
        private static (string EntryPoint, ImmutableArray<(string Signature, string Definition)> Methods) GetProcessedMethods(
            ImmutableArray<Diagnostic>.Builder diagnostics,
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
            ImmutableArray<(string, string)>.Builder methods = ImmutableArray.CreateBuilder<(string, string)>();

            foreach (MethodDeclarationSyntax methodDeclaration in methodDeclarations)
            {
                IMethodSymbol methodDeclarationSymbol = semanticModel.For(methodDeclaration).GetDeclaredSymbol(methodDeclaration)!;
                bool isShaderEntryPoint =
                    methodDeclarationSymbol.Name == nameof(ID2D1PixelShader.Execute) &&
                    methodDeclarationSymbol.ReturnType is not null && // TODO: match for pixel type
                    methodDeclarationSymbol.TypeParameters.Length == 0 &&
                    methodDeclarationSymbol.Parameters.Length == 0;

                // Except for the entry point, ignore explicit interface implementations
                if (!isShaderEntryPoint && !methodDeclarationSymbol.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
                {
                    continue;
                }

                // Create the source rewriter for the current method
                ShaderSourceRewriter shaderSourceRewriter = new(
                    structDeclarationSymbol,
                    semanticModel,
                    discoveredTypes,
                    staticMethods,
                    constantDefinitions,
                    diagnostics);

                // Rewrite the method syntax tree
                MethodDeclarationSyntax? processedMethod = shaderSourceRewriter.Visit(methodDeclaration)!.WithoutTrivia();

                // Emit the extracted local functions first
                foreach (var localFunction in shaderSourceRewriter.LocalFunctions)
                {
                    methods.Add((
                        localFunction.Value.AsDefinition().NormalizeWhitespace(eol: "\n").ToFullString(),
                        localFunction.Value.NormalizeWhitespace(eol: "\n").ToFullString()));
                }

                // If the method is the shader entry point, do additional processing
                if (isShaderEntryPoint)
                {
                    string entryPointDeclaration = processedMethod.NormalizeWhitespace(eol: "\n").ToFullString();
                    string adjustedEntryPointDeclaration = entryPointDeclaration.Replace("float4 Execute()", "D2D_PS_ENTRY(Execute)");

                    entryPoint = adjustedEntryPointDeclaration;
                }
                else
                {
                    methods.Add((
                        processedMethod.AsDefinition().NormalizeWhitespace(eol: "\n").ToFullString(),
                        processedMethod.NormalizeWhitespace(eol: "\n").ToFullString()));
                }
            }

            // Process static methods as well
            foreach (MethodDeclarationSyntax staticMethod in staticMethods.Values)
            {
                methods.Add((
                    staticMethod.AsDefinition().NormalizeWhitespace(eol: "\n").ToFullString(),
                    staticMethod.NormalizeWhitespace(eol: "\n").ToFullString()));
            }

            return (entryPoint!, methods.ToImmutable());
        }

        /// <summary>
        /// Gets a sequence of discovered constants.
        /// </summary>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <returns>A sequence of discovered constants to declare in the shader.</returns>
        private static ImmutableArray<(string Name, string Value)> GetDefinedConstants(IReadOnlyDictionary<IFieldSymbol, string> constantDefinitions)
        {
            ImmutableArray<(string, string)>.Builder builder = ImmutableArray.CreateBuilder<(string, string)>(constantDefinitions.Count);

            foreach (var constant in constantDefinitions)
            {
                var ownerTypeName = ((INamedTypeSymbol)constant.Key.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
                var constantName = $"__{ownerTypeName}__{constant.Key.Name}";

                builder.Add((constantName, constant.Value));
            }

            return builder.MoveToImmutable();
        }

        /// <summary>
        /// Gets the sequence of processed discovered custom types.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="types">The sequence of discovered custom types.</param>
        /// <returns>A sequence of custom type definitions to add to the shader source.</returns>
        private static ImmutableArray<(string Name, string Definition)> GetDeclaredTypes(
            ImmutableArray<Diagnostic>.Builder diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            IEnumerable<INamedTypeSymbol> types)
        {
            ImmutableArray<(string, string)>.Builder builder = ImmutableArray.CreateBuilder<(string, string)>();
            IReadOnlyCollection<INamedTypeSymbol> invalidTypes;

            // Process the discovered types
            foreach (var type in HlslKnownTypes.GetCustomTypes(types, out invalidTypes))
            {
                var structType = type.GetFullMetadataName().ToHlslIdentifierName();
                var structDeclaration = StructDeclaration(structType);

                // Declare the fields of the current type
                foreach (var field in type.GetMembers().OfType<IFieldSymbol>())
                {
                    if (field.IsStatic) continue;

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
                builder.Add((
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

            return builder.ToImmutable();
        }

        /// <summary>
        /// Finds and reports all invalid declared properties in a shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        private static void DetectAndReportInvalidPropertyDeclarations(ImmutableArray<Diagnostic>.Builder diagnostics, INamedTypeSymbol structDeclarationSymbol)
        {
            foreach (var memberSymbol in structDeclarationSymbol.GetMembers())
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

        /// <summary>
        /// Gets whether or not a given shader requires the scene position.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <remarks>Whether <paramref name="structDeclarationSymbol"/> requires the scene position.</remarks>
        private static bool GetRequiresScenePositionInfo(INamedTypeSymbol structDeclarationSymbol)
        {
            return structDeclarationSymbol.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DRequiresScenePositionAttribute", out _);
        }

        /// <summary>
        /// Produces the series of statements to build the current HLSL source.
        /// </summary>
        /// <param name="definedConstants">The sequence of defined constants for the shader.</param>
        /// <param name="declaredTypes">The sequence of declared types used by the shader.</param>
        /// <param name="valueFields">The sequence of value instance fields for the current shader.</param>
        /// <param name="staticFields">The sequence of static fields referenced by the shader.</param>
        /// <param name="processedMethods">The sequence of processed methods used by the shader.</param>
        /// <param name="executeMethod">The body of the entry point of the shader.</param>
        /// <param name="inputCount">The number of shader inputs to declare.</param>
        /// <param name="inputSimpleIndices">The indicess of the simple shader inputs.</param>
        /// <param name="inputComplexIndices">The indicess of the complex shader inputs.</param>
        /// <param name="requiresScenePosition">Whether the shader requires the scene position.</param>
        /// <returns>The series of statements to build the HLSL source to compile to execute the current shader.</returns>
        private static string GetHlslSource(
            ImmutableArray<(string Name, string Value)> definedConstants,
            ImmutableArray<(string Name, string Definition)> declaredTypes,
            ImmutableArray<(string Name, string HlslType)> valueFields,
            ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> staticFields,
            ImmutableArray<(string Signature, string Definition)> processedMethods,
            string executeMethod,
            int inputCount,
            ImmutableArray<int> inputSimpleIndices,
            ImmutableArray<int> inputComplexIndices,
            bool requiresScenePosition)
        {
            StringBuilder hlslBuilder = new();

            void AppendLF()
            {
                hlslBuilder.Append('\n');
            }

            void AppendLineAndLF(string text)
            {
                hlslBuilder.Append(text);
                hlslBuilder.Append('\n');
            }

            // Header
            AppendLineAndLF("// ================================================");
            AppendLineAndLF("//                  AUTO GENERATED");
            AppendLineAndLF("// ================================================");
            AppendLineAndLF("// This shader was created by ComputeSharp.");
            AppendLineAndLF("// See: https://github.com/Sergio0694/ComputeSharp.");
            AppendLF();

            // Shader metadata
            AppendLineAndLF($"#define D2D_INPUT_COUNT {inputCount}");

            foreach (int simpleInput in inputSimpleIndices)
            {
                AppendLineAndLF($"#define D2D_INPUT{simpleInput}_SIMPLE");
            }

            foreach (int complexInput in inputComplexIndices)
            {
                AppendLineAndLF($"#define D2D_INPUT{complexInput}_COMPLEX");
            }

            if (requiresScenePosition)
            {
                AppendLineAndLF("#define D2D_REQUIRES_SCENE_POSITION");
            }

            // Add the "d2d1effecthelpers.hlsli" header
            AppendLF();
            AppendLineAndLF("#include \"d2d1effecthelpers.hlsli\"");

            // Define declarations
            if (definedConstants.Any())
            {
                AppendLF();

                foreach (var (name, value) in definedConstants)
                {
                    AppendLineAndLF($"#define {name} {value}");
                }
            }

            // Static fields
            if (staticFields.Any())
            {
                AppendLF();

                foreach (var field in staticFields)
                {
                    if (field.Assignment is string assignment)
                    {
                        AppendLineAndLF($"{field.TypeDeclaration} {field.Name} = {assignment};");
                    }
                    else
                    {
                        AppendLineAndLF($"{field.TypeDeclaration} {field.Name};");
                    }
                }
            }

            // Declared types
            foreach (var (_, typeDefinition) in declaredTypes)
            {
                AppendLF();
                AppendLineAndLF(typeDefinition);
            }

            // Captured variables
            AppendLF();

            // User-defined values
            foreach (var (fieldName, fieldType) in valueFields)
            {
                AppendLineAndLF($"{fieldType} {fieldName};");
            }

            // Forward declarations
            foreach (var (forwardDeclaration, _) in processedMethods)
            {
                AppendLF();
                AppendLineAndLF(forwardDeclaration);
            }

            // Captured methods
            foreach (var (_, method) in processedMethods)
            {
                AppendLF();
                AppendLineAndLF(method);
            }

            // Entry point
            AppendLF();
            AppendLineAndLF(executeMethod);

            return hlslBuilder.ToString();
        }
    }
}
