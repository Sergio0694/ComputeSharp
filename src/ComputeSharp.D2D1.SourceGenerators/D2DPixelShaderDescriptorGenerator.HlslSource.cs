using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>HlslSource</c> property.
    /// </summary>
    private static partial class HlslSource
    {
        /// <summary>
        /// Gathers all necessary information on a transpiled HLSL source for a given shader type.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <param name="inputSimpleIndices">The indicess of the simple shader inputs.</param>
        /// <param name="inputComplexIndices">The indicess of the complex shader inputs.</param>
        /// <returns>The HLSL source for the shader.</returns>
        public static string GetHlslSource(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
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
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);

            // Extract information on all captured fields
            GetInstanceFields(
                diagnostics,
                structDeclarationSymbol,
                discoveredTypes,
                out ImmutableArray<(string Name, string HlslType)> valueFields,
                out ImmutableArray<(string Name, string HlslType, int Index)> resourceTextureFields);

            // Explore the syntax tree and extract the processed info
            SemanticModelProvider semanticModelProvider = new(compilation);
            (string entryPoint, ImmutableArray<(string Signature, string Definition)> processedMethods) = GetProcessedMethods(diagnostics, structDeclaration, structDeclarationSymbol, semanticModelProvider, discoveredTypes, staticMethods, instanceMethods, constantDefinitions, out bool methodsNeedD2D1RequiresScenePosition);
            ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> staticFields = GetStaticFields(diagnostics, semanticModelProvider, structDeclaration, structDeclarationSymbol, discoveredTypes, constantDefinitions, out bool fieldsNeedD2D1RequiresScenePosition);

            // Process the discovered types and constants
            ImmutableArray<(string Name, string Definition)> declaredTypes = GetDeclaredTypes(diagnostics, structDeclarationSymbol, discoveredTypes, instanceMethods);
            ImmutableArray<(string Name, string Value)> definedConstants = GetDefinedConstants(constantDefinitions);

            // Check whether the scene position is required
            bool requiresScenePosition = GetD2DRequiresScenePositionInfo(structDeclarationSymbol);

            // Emit diagnostics for incorrect scene position uses
            ReportInvalidD2DRequiresScenePositionUse(diagnostics, structDeclarationSymbol, requiresScenePosition, methodsNeedD2D1RequiresScenePosition || fieldsNeedD2D1RequiresScenePosition);

            // Get the HLSL source
            return GetHlslSource(
                definedConstants,
                declaredTypes,
                valueFields,
                resourceTextureFields,
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
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <param name="valueFields">The sequence of captured fields in <paramref name="structDeclarationSymbol"/>.</param>
        /// <param name="resourceTextureFields">The sequence of captured resource textures in <paramref name="structDeclarationSymbol"/>.</param>
        private static void GetInstanceFields(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> types,
            out ImmutableArray<(string Name, string HlslType)> valueFields,
            out ImmutableArray<(string Name, string HlslType, int Index)> resourceTextureFields)
        {
            using ImmutableArrayBuilder<(string, string)> values = new();
            using ImmutableArrayBuilder<(string, string, int)> resourceTextures = new();

            foreach (IFieldSymbol fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
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

                string metadataName = typeSymbol.GetFullyQualifiedMetadataName();
                string typeName = HlslKnownTypes.GetMappedName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                // Handle resource textures as a special case
                if (HlslKnownTypes.IsResourceTextureType(metadataName))
                {
                    ITypeSymbol resourceTextureTypeArgumentSymbol = typeSymbol.TypeArguments[0];

                    // Validate that the type argument is only either float or float4
                    if (!resourceTextureTypeArgumentSymbol.HasFullyQualifiedMetadataName("System.Single") &&
                        !resourceTextureTypeArgumentSymbol.HasFullyQualifiedMetadataName("ComputeSharp.Float4"))
                    {
                        diagnostics.Add(
                            InvalidResourceTextureElementType,
                            fieldSymbol,
                            fieldSymbol.Name,
                            structDeclarationSymbol,
                            fieldSymbol.Type);
                    }

                    int index = 0;

                    // If [D2DResourceTextureIndex] is present, get the resource texture index.
                    // This generator doesn't need to emit diagnostics, as that's handled separately
                    // by the logic handling the info gathering for LoadResourceTextureDescriptions.
                    if (fieldSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DResourceTextureIndexAttribute", out AttributeData? attributeData))
                    {
                        _ = attributeData.TryGetConstructorArgument(0, out index);
                    }

                    resourceTextures.Add((mapping ?? fieldSymbol.Name, typeName, index));
                }
                else if (typeSymbol.IsUnmanagedType)
                {
                    // Allowed fields must be unmanaged values.
                    // Also, track the type if it's a custom struct.
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

            valueFields = values.ToImmutable();
            resourceTextureFields = resourceTextures.ToImmutable();
        }

        /// <summary>
        /// Gets a sequence of shader static fields and their mapped names.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="needsD2D1RequiresScenePosition">Whether or not the shader needs the <c>[D2DRequiresScenePosition]</c> annotation.</param>
        /// <returns>A sequence of static constant fields in <paramref name="structDeclarationSymbol"/>.</returns>
        private static ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> GetStaticFields(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            SemanticModelProvider semanticModel,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            out bool needsD2D1RequiresScenePosition)
        {
            using ImmutableArrayBuilder<(string, string, string?)> builder = new();

            needsD2D1RequiresScenePosition = false;

            foreach (FieldDeclarationSyntax fieldDeclaration in structDeclaration.Members.OfType<FieldDeclarationSyntax>())
            {
                foreach (VariableDeclaratorSyntax variableDeclarator in fieldDeclaration.Declaration.Variables)
                {
                    IFieldSymbol fieldSymbol = (IFieldSymbol)semanticModel.For(variableDeclarator).GetDeclaredSymbol(variableDeclarator)!;

                    if (!fieldSymbol.IsStatic || fieldSymbol.IsConst)
                    {
                        continue;
                    }

                    // Constant properties must be of a primitive, vector or matrix type
                    if (fieldSymbol.Type is not INamedTypeSymbol typeSymbol ||
                        !HlslKnownTypes.IsKnownHlslType(typeSymbol.GetFullyQualifiedMetadataName()))
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

                    needsD2D1RequiresScenePosition |= staticFieldRewriter.NeedsD2DRequiresScenePositionAttribute;

                    builder.Add((mapping ?? fieldSymbol.Name, typeDeclaration, assignment));
                }
            }

            return builder.ToImmutable();
        }

        /// <summary>
        /// Gets a sequence of processed methods declared within a given type.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="needsD2D1RequiresScenePosition">Whether or not the shader needs the <c>[D2DRequiresScenePosition]</c> annotation.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>, and the entry point.</returns>
        private static (string EntryPoint, ImmutableArray<(string Signature, string Definition)> Methods) GetProcessedMethods(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            SemanticModelProvider semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            out bool needsD2D1RequiresScenePosition)
        {
            // Find all declared methods in the type
            ImmutableArray<MethodDeclarationSyntax> methodDeclarations = (
                from syntaxNode in structDeclaration.DescendantNodes()
                where syntaxNode.IsKind(SyntaxKind.MethodDeclaration)
                select (MethodDeclarationSyntax)syntaxNode).ToImmutableArray();

            string? entryPoint = null;

            using ImmutableArrayBuilder<(string, string)> methods = new();

            needsD2D1RequiresScenePosition = false;

            foreach (MethodDeclarationSyntax methodDeclaration in methodDeclarations)
            {
                IMethodSymbol methodDeclarationSymbol = semanticModel.For(methodDeclaration).GetDeclaredSymbol(methodDeclaration)!;
                bool isShaderEntryPoint =
                    methodDeclarationSymbol.Name == "Execute" &&
                    methodDeclarationSymbol.ReturnType.HasFullyQualifiedMetadataName("ComputeSharp.Float4") &&
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
                    instanceMethods,
                    constantDefinitions,
                    diagnostics,
                    isShaderEntryPoint);

                // Rewrite the method syntax tree
                MethodDeclarationSyntax? processedMethod = shaderSourceRewriter.Visit(methodDeclaration)!.WithoutTrivia();

                // Update the position requirement
                needsD2D1RequiresScenePosition |= shaderSourceRewriter.NeedsD2DRequiresScenePositionAttribute;

                // Emit the extracted local functions first
                foreach (KeyValuePair<string, LocalFunctionStatementSyntax> localFunction in shaderSourceRewriter.LocalFunctions)
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
            using ImmutableArrayBuilder<(string, string)> builder = new();

            foreach (KeyValuePair<IFieldSymbol, string> constant in constantDefinitions)
            {
                string ownerTypeName = ((INamedTypeSymbol)constant.Key.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
                string constantName = $"__{ownerTypeName}__{constant.Key.Name}";

                builder.Add((constantName, constant.Value));
            }

            return builder.ToImmutable();
        }

        /// <summary>
        /// Gets the sequence of processed discovered custom types.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="types">The sequence of discovered custom types.</param>
        /// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
        /// <returns>A sequence of custom type definitions to add to the shader source.</returns>
        private static ImmutableArray<(string Name, string Definition)> GetDeclaredTypes(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            IEnumerable<INamedTypeSymbol> types,
            IReadOnlyDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods)
        {
            using ImmutableArrayBuilder<(string, string)> builder = new();

            IReadOnlyCollection<INamedTypeSymbol> invalidTypes;

            // Process the discovered types
            foreach (INamedTypeSymbol type in HlslKnownTypes.GetCustomTypes(types, out invalidTypes))
            {
                string structType = type.GetFullyQualifiedMetadataName().ToHlslIdentifierName();
                StructDeclarationSyntax structDeclaration = StructDeclaration(structType);

                // Declare the fields of the current type
                foreach (IFieldSymbol field in type.GetMembers().OfType<IFieldSymbol>())
                {
                    if (field.IsStatic)
                    {
                        continue;
                    }

                    INamedTypeSymbol fieldType = (INamedTypeSymbol)field.Type;

                    // Convert the name to the fully qualified HLSL version
                    if (!HlslKnownTypes.TryGetMappedName(fieldType.GetFullyQualifiedMetadataName(), out string? mappedType))
                    {
                        mappedType = fieldType.GetFullyQualifiedMetadataName().ToHlslIdentifierName();
                    }

                    // Get the field name as a valid HLSL identifier
                    if (!HlslKnownKeywords.TryGetMappedName(field.Name, out string? mappedName))
                    {
                        mappedName = field.Name;
                    }

                    structDeclaration = structDeclaration.AddMembers(
                        FieldDeclaration(VariableDeclaration(
                            IdentifierName(mappedType!)).AddVariables(
                            VariableDeclarator(Identifier(mappedName!)))));
                }

                // Declare the methods of the current type
                foreach (KeyValuePair<IMethodSymbol, MethodDeclarationSyntax> method in instanceMethods.Where(pair => SymbolEqualityComparer.Default.Equals(pair.Key.ContainingType, type)))
                {
                    structDeclaration = structDeclaration.AddMembers(method.Value);
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
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        private static void DetectAndReportInvalidPropertyDeclarations(ImmutableArrayBuilder<DiagnosticInfo> diagnostics, INamedTypeSymbol structDeclarationSymbol)
        {
            foreach (ISymbol memberSymbol in structDeclarationSymbol.GetMembers())
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
        /// Reports diagnostics for invalid uses of <c>[D2DRequiresScenePosition]</c> in a shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="requiresScenePosition">Whether the shader type is declaring the need for scene position.</param>
        /// <param name="usesPositionDependentMethods">Whether the shader is using APIs that rely on scene position.</param>
        private static void ReportInvalidD2DRequiresScenePositionUse(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            bool requiresScenePosition,
            bool usesPositionDependentMethods)
        {
            if (!requiresScenePosition && usesPositionDependentMethods)
            {
                diagnostics.Add(MissingD2DRequiresScenePositionAttribute, structDeclarationSymbol, structDeclarationSymbol);
            }
        }

        /// <summary>
        /// Gets whether or not a given shader requires the scene position.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <remarks>Whether <paramref name="structDeclarationSymbol"/> requires the scene position.</remarks>
        private static bool GetD2DRequiresScenePositionInfo(INamedTypeSymbol structDeclarationSymbol)
        {
            return structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DRequiresScenePositionAttribute", out _);
        }

        /// <summary>
        /// Produces the series of statements to build the current HLSL source.
        /// </summary>
        /// <param name="definedConstants">The sequence of defined constants for the shader.</param>
        /// <param name="declaredTypes">The sequence of declared types used by the shader.</param>
        /// <param name="valueFields">The sequence of value instance fields for the current shader.</param>
        /// <param name="resourceTextureFields">The sequence of captured resource textures for the current shader.</param>
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
            ImmutableArray<(string Name, string HlslType, int Index)> resourceTextureFields,
            ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> staticFields,
            ImmutableArray<(string Signature, string Definition)> processedMethods,
            string executeMethod,
            int inputCount,
            ImmutableArray<int> inputSimpleIndices,
            ImmutableArray<int> inputComplexIndices,
            bool requiresScenePosition)
        {
            using ImmutableArrayBuilder<char> hlslBuilder = new();

            void AppendLF()
            {
                hlslBuilder.Add('\n');
            }

            void AppendLineAndLF(string text)
            {
                hlslBuilder.AddRange(text.AsSpan());
                hlslBuilder.Add('\n');
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

                foreach ((string name, string value) in definedConstants)
                {
                    AppendLineAndLF($"#define {name} {value}");
                }
            }

            // Static fields
            if (staticFields.Any())
            {
                AppendLF();

                foreach ((string Name, string TypeDeclaration, string? Assignment) field in staticFields)
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
            foreach ((string _, string typeDefinition) in declaredTypes)
            {
                AppendLF();
                AppendLineAndLF(typeDefinition);
            }

            // Captured variables
            if (valueFields.Any())
            {
                AppendLF();

                foreach ((string fieldName, string fieldType) in valueFields)
                {
                    AppendLineAndLF($"{fieldType} {fieldName};");
                }
            }

            // Resource textures
            foreach ((string fieldName, string fieldType, int index) in resourceTextureFields)
            {
                AppendLF();

                AppendLineAndLF($"{fieldType} {fieldName} : register(t{index});");
                AppendLineAndLF($"SamplerState __sampler__{fieldName} : register(s{index});");
            }

            // Forward declarations
            foreach ((string forwardDeclaration, string _) in processedMethods)
            {
                AppendLF();
                AppendLineAndLF(forwardDeclaration);
            }

            // Captured methods
            foreach ((string _, string method) in processedMethods)
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