using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using ComputeSharp.SourceGeneration.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

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
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <param name="inputSimpleIndices">The indicess of the simple shader inputs.</param>
        /// <param name="inputComplexIndices">The indicess of the complex shader inputs.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <returns>The HLSL source for the shader.</returns>
        public static string GetHlslSource(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            Compilation compilation,
            INamedTypeSymbol structDeclarationSymbol,
            int inputCount,
            ImmutableArray<int> inputSimpleIndices,
            ImmutableArray<int> inputComplexIndices,
            CancellationToken token)
        {
            // Detect any invalid properties
            HlslDefinitionsSyntaxProcessor.DetectAndReportInvalidPropertyDeclarations(diagnostics, structDeclarationSymbol);

            // We need to sets to track all discovered custom types and static methods
            HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, (MethodDeclarationSyntax, MethodDeclarationSyntax)> constructors = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, HlslStaticField> staticFieldDefinitions = new(SymbolEqualityComparer.Default);

            token.ThrowIfCancellationRequested();

            // Extract information on all captured fields
            GetInstanceFields(
                diagnostics,
                structDeclarationSymbol,
                discoveredTypes,
                out ImmutableArray<HlslValueField> valueFields,
                out ImmutableArray<HlslResourceTextureField> resourceTextureFields);

            token.ThrowIfCancellationRequested();

            SemanticModelProvider semanticModelProvider = new(compilation);

            // Explore the syntax tree and extract the processed info
            (string entryPoint, ImmutableArray<HlslMethod> processedMethods) = GetProcessedMethods(
                diagnostics,
                structDeclarationSymbol,
                semanticModelProvider,
                discoveredTypes,
                staticMethods,
                instanceMethods,
                constructors,
                constantDefinitions,
                staticFieldDefinitions,
                token,
                out bool methodsNeedD2D1RequiresScenePosition);

            token.ThrowIfCancellationRequested();

            ImmutableArray<HlslStaticField> staticFields = GetStaticFields(
                diagnostics,
                semanticModelProvider,
                structDeclarationSymbol,
                discoveredTypes,
                constantDefinitions,
                staticFieldDefinitions,
                token,
                out bool fieldsNeedD2D1RequiresScenePosition);

            token.ThrowIfCancellationRequested();

            // Process the discovered types and constants
            HlslDefinitionsSyntaxProcessor.GetDeclaredTypes(
                diagnostics,
                structDeclarationSymbol,
                discoveredTypes,
                instanceMethods,
                constructors,
                out ImmutableArray<HlslUserType> typeDeclarations,
                out ImmutableArray<string> typeMethodDeclarations);

            token.ThrowIfCancellationRequested();

            ImmutableArray<HlslConstant> definedConstants = HlslDefinitionsSyntaxProcessor.GetDefinedConstants(constantDefinitions);

            token.ThrowIfCancellationRequested();

            // Check whether the scene position is required
            bool requiresScenePosition = GetD2DRequiresScenePositionInfo(structDeclarationSymbol);

            // Emit diagnostics for incorrect scene position uses
            ReportInvalidD2DRequiresScenePositionUse(
                diagnostics,
                structDeclarationSymbol,
                requiresScenePosition,
                methodsNeedD2D1RequiresScenePosition || fieldsNeedD2D1RequiresScenePosition);

            token.ThrowIfCancellationRequested();

            // Get the HLSL source
            return GetHlslSource(
                definedConstants,
                valueFields,
                resourceTextureFields,
                staticFields,
                processedMethods,
                typeDeclarations,
                typeMethodDeclarations,
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
            out ImmutableArray<HlslValueField> valueFields,
            out ImmutableArray<HlslResourceTextureField> resourceTextureFields)
        {
            using ImmutableArrayBuilder<HlslValueField> values = new();
            using ImmutableArrayBuilder<HlslResourceTextureField> resourceTextures = new();

            foreach (ISymbol memberSymbol in structDeclarationSymbol.GetMembers())
            {
                // Skip constants and static fields, we only care about instance ones here
                if (memberSymbol is not IFieldSymbol { IsConst: false, IsStatic: false } fieldSymbol)
                {
                    continue;
                }

                // Try to get the actual field name
                if (!ConstantBufferSyntaxProcessor.TryGetFieldAccessorName(fieldSymbol, out string? fieldName, out _))
                {
                    continue;
                }

                // Captured fields must be named type symbols
                if (fieldSymbol.Type is not INamedTypeSymbol typeSymbol)
                {
                    diagnostics.Add(InvalidShaderField, fieldSymbol, structDeclarationSymbol, fieldName, fieldSymbol.Type);

                    continue;
                }

                string metadataName = typeSymbol.GetFullyQualifiedMetadataName();
                string typeName = HlslKnownTypes.GetMappedName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldName, out string? mapping);

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
                            fieldName,
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

                    resourceTextures.Add((mapping ?? fieldName, typeName, index));
                }
                else if (typeSymbol.IsUnmanagedType)
                {
                    // Allowed fields must be unmanaged values.
                    // Also, track the type if it's a custom struct.
                    if (!HlslKnownTypes.IsKnownHlslType(metadataName))
                    {
                        types.Add(typeSymbol);
                    }

                    values.Add((mapping ?? fieldName, typeName));
                }
                else
                {
                    diagnostics.Add(InvalidShaderField, fieldSymbol, structDeclarationSymbol, fieldName, typeSymbol);
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
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="staticFieldDefinitions">The collection of discovered static field definitions.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <param name="needsD2D1RequiresScenePosition">Whether or not the shader needs the <c>[D2DRequiresScenePosition]</c> annotation.</param>
        /// <returns>A sequence of static constant fields in <paramref name="structDeclarationSymbol"/>.</returns>
        private static ImmutableArray<HlslStaticField> GetStaticFields(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            SemanticModelProvider semanticModel,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            IDictionary<IFieldSymbol, HlslStaticField> staticFieldDefinitions,
            CancellationToken token,
            out bool needsD2D1RequiresScenePosition)
        {
            using ImmutableArrayBuilder<HlslStaticField> builder = new();

            needsD2D1RequiresScenePosition = false;

            foreach (ISymbol memberSymbol in structDeclarationSymbol.GetMembers())
            {
                if (memberSymbol is not IFieldSymbol fieldSymbol)
                {
                    continue;
                }

                if (HlslDefinitionsSyntaxProcessor.TryGetStaticField(
                    structDeclarationSymbol,
                    fieldSymbol,
                    semanticModel,
                    discoveredTypes,
                    constantDefinitions,
                    staticFieldDefinitions,
                    diagnostics,
                    token,
                    out string? name,
                    out string? typeDeclaration,
                    out string? assignmentExpression,
                    out StaticFieldRewriter? staticFieldRewriter))
                {
                    needsD2D1RequiresScenePosition |= staticFieldRewriter.NeedsD2DRequiresScenePositionAttribute;

                    builder.Add((name, typeDeclaration, assignmentExpression));
                }
            }

            // Also gather the external static fields (same as in the DX12 generator)
            foreach (HlslStaticField externalField in staticFieldDefinitions.Values)
            {
                builder.Add(externalField);
            }

            return builder.ToImmutable();
        }

        /// <summary>
        /// Gets a sequence of processed methods declared within a given type.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
        /// <param name="constructors">The collection of discovered constructors for custom struct types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="staticFieldDefinitions">The collection of discovered static field definitions.</param>
        /// <param name="needsD2D1RequiresScenePosition">Whether or not the shader needs the <c>[D2DRequiresScenePosition]</c> annotation.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclarationSymbol"/>, and the entry point.</returns>
        private static (string EntryPoint, ImmutableArray<HlslMethod> Methods) GetProcessedMethods(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            SemanticModelProvider semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods,
            IDictionary<IMethodSymbol, (MethodDeclarationSyntax, MethodDeclarationSyntax)> constructors,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            IDictionary<IFieldSymbol, HlslStaticField> staticFieldDefinitions,
            CancellationToken token,
            out bool needsD2D1RequiresScenePosition)
        {
            using ImmutableArrayBuilder<HlslMethod> methods = new();

            string? entryPoint = null;

            // By default, the scene position is not required. We will set this while
            // rewriting HLSL, if any method we traverse ends up requiring the position.
            needsD2D1RequiresScenePosition = false;

            foreach (ISymbol memberSymbol in structDeclarationSymbol.GetMembers())
            {
                // Find all declared methods in the type
                if (memberSymbol is not IMethodSymbol { IsImplicitlyDeclared: false, } methodSymbol)
                {
                    continue;
                }

                if (!methodSymbol.TryGetSyntaxNode(token, out MethodDeclarationSyntax? methodDeclaration))
                {
                    continue;
                }

                bool isShaderEntryPoint =
                    methodSymbol.Name == "Execute" &&
                    methodSymbol.ReturnType.HasFullyQualifiedMetadataName("ComputeSharp.Float4") &&
                    methodSymbol.TypeParameters.Length == 0 &&
                    methodSymbol.Parameters.Length == 0;

                // Except for the entry point, ignore explicit interface implementations
                if (!isShaderEntryPoint && !methodSymbol.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
                {
                    continue;
                }

                token.ThrowIfCancellationRequested();

                // Create the source rewriter for the current method
                ShaderSourceRewriter shaderSourceRewriter = new(
                    structDeclarationSymbol,
                    semanticModel,
                    discoveredTypes,
                    staticMethods,
                    instanceMethods,
                    constructors,
                    constantDefinitions,
                    staticFieldDefinitions,
                    diagnostics,
                    token,
                    isShaderEntryPoint);

                // Rewrite the method syntax tree
                MethodDeclarationSyntax? processedMethod = shaderSourceRewriter.Visit(methodDeclaration)!.WithoutTrivia();

                token.ThrowIfCancellationRequested();

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

            token.ThrowIfCancellationRequested();

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
        /// <param name="definedConstants"><inheritdoc cref="HlslSourceSyntaxProcessor.WriteTopDeclarations" path="/param[@name='definedConstants']/node()"/></param>
        /// <param name="valueFields"><inheritdoc cref="HlslSourceSyntaxProcessor.WriteCapturedFields" path="/param[@name='valueFields']/node()"/></param>
        /// <param name="resourceTextureFields">The sequence of captured resource textures for the current shader.</param>
        /// <param name="staticFields"><inheritdoc cref="HlslSourceSyntaxProcessor.WriteTopDeclarations" path="/param[@name='staticFields']/node()"/></param>
        /// <param name="processedMethods"><inheritdoc cref="HlslSourceSyntaxProcessor.WriteTopDeclarations" path="/param[@name='processedMethods']/node()"/></param>
        /// <param name="typeDeclarations"><inheritdoc cref="HlslSourceSyntaxProcessor.WriteTopDeclarations" path="/param[@name='typeDeclarations']/node()"/></param>
        /// <param name="typeMethodDeclarations"><inheritdoc cref="HlslSourceSyntaxProcessor.WriteMethodDeclarations" path="/param[@name='typeMethodDeclarations']/node()"/></param>
        /// <param name="executeMethod">The body of the entry point of the shader.</param>
        /// <param name="inputCount">The number of shader inputs to declare.</param>
        /// <param name="inputSimpleIndices">The indicess of the simple shader inputs.</param>
        /// <param name="inputComplexIndices">The indicess of the complex shader inputs.</param>
        /// <param name="requiresScenePosition">Whether the shader requires the scene position.</param>
        /// <returns>The series of statements to build the HLSL source to compile to execute the current shader.</returns>
        private static string GetHlslSource(
            ImmutableArray<HlslConstant> definedConstants,
            ImmutableArray<HlslValueField> valueFields,
            ImmutableArray<HlslResourceTextureField> resourceTextureFields,
            ImmutableArray<HlslStaticField> staticFields,
            ImmutableArray<HlslMethod> processedMethods,
            ImmutableArray<HlslUserType> typeDeclarations,
            ImmutableArray<string> typeMethodDeclarations,
            string executeMethod,
            int inputCount,
            ImmutableArray<int> inputSimpleIndices,
            ImmutableArray<int> inputComplexIndices,
            bool requiresScenePosition)
        {
            using IndentedTextWriter writer = new();

            // Shader metadata
            writer.WriteLine($"#define D2D_INPUT_COUNT {inputCount}");

            foreach (int simpleInput in inputSimpleIndices)
            {
                writer.WriteLine($"#define D2D_INPUT{simpleInput}_SIMPLE");
            }

            foreach (int complexInput in inputComplexIndices)
            {
                writer.WriteLine($"#define D2D_INPUT{complexInput}_COMPLEX");
            }

            writer.WriteLineIf(requiresScenePosition, "#define D2D_REQUIRES_SCENE_POSITION");

            // Add the "d2d1effecthelpers.hlsli" header
            writer.WriteLine();
            writer.WriteLine("#include \"d2d1effecthelpers.hlsli\"");
            writer.WriteLine();

            // The FXC compiler does not support type forward declarations
            HlslSourceSyntaxProcessor.WriteTopDeclarations(
                writer,
                definedConstants,
                staticFields,
                processedMethods,
                typeDeclarations,
                includeTypeForwardDeclarations: false);

            HlslSourceSyntaxProcessor.WriteCapturedFields(writer, valueFields);

            // Resource textures
            foreach (HlslResourceTextureField field in resourceTextureFields)
            {
                writer.WriteLine(skipIfPresent: true);
                writer.WriteLine($"{field.Type} {field.Name} : register(t{field.Index});");
                writer.WriteLine($"SamplerState __sampler__{field.Name} : register(s{field.Index});");
            }

            HlslSourceSyntaxProcessor.WriteMethodDeclarations(writer, processedMethods, typeMethodDeclarations);

            // Entry point
            writer.WriteLine(executeMethod);

            return writer.ToString();
        }
    }
}