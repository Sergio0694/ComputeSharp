using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxRewriters;
using ComputeSharp.SourceGenerators.Models;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>BuildHlslSource</c> method.
    /// </summary>
    internal static partial class BuildHlslSource
    {
        /// <summary>
        /// Gathers all necessary information on a transpiled HLSL source for a given shader type.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
        /// <param name="threadIds">The info on the shader thread count size to use.</param>
        /// <param name="isImplicitTextureUsed">Indicates whether the current shader uses an implicit texture.</param>
        /// <returns>The resulting info on the processed shader.</returns>
        public static HlslShaderSourceInfo GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            Compilation compilation,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            ThreadIdsInfo threadIds,
            out bool isImplicitTextureUsed)
        {
            // Detect invalid properties
            DetectAndReportInvalidPropertyDeclarations(diagnostics, structDeclarationSymbol);

            // We need to sets to track all discovered custom types and static methods
            HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);

            // A given type can only represent a single shader type
            if (structDeclarationSymbol.AllInterfaces.Count(static interfaceSymbol => interfaceSymbol is { Name: nameof(IComputeShader) } or { IsGenericType: true, Name: nameof(IPixelShader<byte>) }) > 1)
            {
                diagnostics.Add(MultipleShaderTypesImplemented, structDeclarationSymbol, structDeclarationSymbol);
            }

            // Explore the syntax tree and extract the processed info
            SemanticModelProvider semanticModelProvider = new(compilation);
            INamedTypeSymbol? pixelShaderSymbol = structDeclarationSymbol.AllInterfaces.FirstOrDefault(static interfaceSymbol => interfaceSymbol is { IsGenericType: true, Name: nameof(IPixelShader<byte>) });
            bool isComputeShader = pixelShaderSymbol is null;
            string? implicitTextureType = isComputeShader ? null : HlslKnownTypes.GetMappedNameForPixelShaderType(pixelShaderSymbol!);
            (ImmutableArray<(string MetadataName, string Name, string HlslType)> resourceFields, ImmutableArray<(string Name, string HlslType)> valueFields) = GetInstanceFields(diagnostics, structDeclarationSymbol, discoveredTypes, isComputeShader);
            ImmutableArray<(string Name, string Type, int? Count)> sharedBuffers = GetSharedBuffers(diagnostics, structDeclarationSymbol, discoveredTypes);
            (string entryPoint, ImmutableArray<(string Signature, string Definition)> processedMethods, bool isSamplerUsed) = GetProcessedMethods(diagnostics, structDeclaration, structDeclarationSymbol, semanticModelProvider, discoveredTypes, staticMethods, instanceMethods, constantDefinitions, isComputeShader);
            (string, string)? implicitSamplerField = isSamplerUsed ? ("SamplerState", "__sampler") : default((string, string)?);
            ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> staticFields = GetStaticFields(diagnostics, semanticModelProvider, structDeclaration, structDeclarationSymbol, discoveredTypes, constantDefinitions);

            // Process the discovered types and constants
            ImmutableArray<(string Name, string Definition)> declaredTypes = GetDeclaredTypes(diagnostics, structDeclarationSymbol, discoveredTypes, instanceMethods);
            ImmutableArray<(string Name, string Value)> definedConstants = GetDefinedConstants(constantDefinitions);

            // Check whether an implicit texture is used in the shader
            isImplicitTextureUsed = implicitTextureType is not null;

            // Get the HLSL source data with the intermediate info
            return GetHlslSourceInfo(
                threadIds,
                definedConstants,
                declaredTypes,
                resourceFields,
                valueFields,
                staticFields,
                sharedBuffers,
                processedMethods,
                isComputeShader,
                implicitTextureType,
                isSamplerUsed,
                entryPoint);
        }

        /// <summary>
        /// Gets a sequence of captured fields and their mapped names.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <param name="isComputeShader">Indicates whether or not <paramref name="structDeclarationSymbol"/> represents a compute shader.</param>
        /// <returns>A sequence of captured fields in <paramref name="structDeclarationSymbol"/>.</returns>
        private static (
            ImmutableArray<(string MetadataName, string Name, string HlslType)>,
            ImmutableArray<(string Name, string HlslType)>)
            GetInstanceFields(
                ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
                INamedTypeSymbol structDeclarationSymbol,
                ICollection<INamedTypeSymbol> types,
                bool isComputeShader)
        {
            using ImmutableArrayBuilder<(string, string, string)> resources = new();
            using ImmutableArrayBuilder<(string, string)> values = new();

            bool hlslResourceFound = false;

            foreach (IFieldSymbol fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (fieldSymbol.IsStatic)
                {
                    continue;
                }

                AttributeData? attribute = fieldSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass?.ToDisplayString() == typeof(GroupSharedAttribute).FullName);

                // Group shared fields must be static
                if (attribute is not null)
                {
                    diagnostics.Add(InvalidGroupSharedFieldDeclaration, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name);
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

                // Allowed fields must be either resources, unmanaged values or delegates
                if (HlslKnownTypes.IsTypedResourceType(metadataName))
                {
                    hlslResourceFound = true;

                    // Track the type of items in the current buffer
                    if (HlslKnownTypes.IsStructuredBufferType(metadataName))
                    {
                        types.Add((INamedTypeSymbol)typeSymbol.TypeArguments[0]);
                    }

                    // Add the current mapping for the name (if the name used a reserved keyword)
                    resources.Add((metadataName, mapping ?? fieldSymbol.Name, typeName));
                }
                else if (!typeSymbol.IsUnmanagedType &&
                         typeSymbol.TypeKind != TypeKind.Delegate)
                {
                    // Shaders can only capture valid HLSL resource types or unmanaged types
                    diagnostics.Add(InvalidShaderField, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name, typeSymbol);

                    continue;
                }
                else if (typeSymbol.IsUnmanagedType)
                {
                    // Track the type if it's a custom struct
                    if (!HlslKnownTypes.IsKnownHlslType(metadataName))
                    {
                        types.Add(typeSymbol);
                    }

                    values.Add((mapping ?? fieldSymbol.Name, typeName));
                }
            }

            // If the shader is a compute one (so no implicit output texture), it has to contain at least one resource
            if (!hlslResourceFound && isComputeShader)
            {
                diagnostics.Add(MissingShaderResources, structDeclarationSymbol, structDeclarationSymbol);
            }

            return (resources.ToImmutable(), values.ToImmutable());
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
        /// <returns>A sequence of static constant fields in <paramref name="structDeclarationSymbol"/>.</returns>
        private static ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> GetStaticFields(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            SemanticModelProvider semanticModel,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions)
        {
            using ImmutableArrayBuilder<(string, string, string?)> builder = new();

            foreach (FieldDeclarationSyntax fieldDeclaration in structDeclaration.Members.OfType<FieldDeclarationSyntax>())
            {
                foreach (VariableDeclaratorSyntax variableDeclarator in fieldDeclaration.Declaration.Variables)
                {
                    IFieldSymbol fieldSymbol = (IFieldSymbol)semanticModel.For(variableDeclarator).GetDeclaredSymbol(variableDeclarator)!;

                    if (!fieldSymbol.IsStatic || fieldSymbol.IsConst)
                    {
                        continue;
                    }

                    AttributeData? attribute = fieldSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass?.ToDisplayString() == typeof(GroupSharedAttribute).FullName);

                    if (attribute is not null)
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

                    builder.Add((mapping ?? fieldSymbol.Name, typeDeclaration, assignment));
                }
            }

            return builder.ToImmutable();
        }

        /// <summary>
        /// Gets a sequence of captured members and their mapped names.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of captured members in <paramref name="structDeclarationSymbol"/>.</returns>
        private static ImmutableArray<(string Name, string Type, int? Count)> GetSharedBuffers(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> types)
        {
            using ImmutableArrayBuilder<(string, string, int?)> builder = new();

            foreach (IFieldSymbol fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
            {
                if (!fieldSymbol.IsStatic)
                {
                    continue;
                }

                AttributeData? attribute = fieldSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass?.ToDisplayString() == typeof(GroupSharedAttribute).FullName);

                if (attribute is null)
                {
                    continue;
                }

                if (fieldSymbol.Type is not IArrayTypeSymbol typeSymbol)
                {
                    diagnostics.Add(InvalidGroupSharedFieldType, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type);

                    continue;
                }

                if (!typeSymbol.ElementType.IsUnmanagedType)
                {
                    diagnostics.Add(InvalidGroupSharedFieldElementType, fieldSymbol, structDeclarationSymbol, fieldSymbol.Name, fieldSymbol.Type);

                    continue;
                }

                int? bufferSize = (int?)attribute.ConstructorArguments.FirstOrDefault().Value;

                string typeName = HlslKnownTypes.GetMappedElementName(typeSymbol);

                _ = HlslKnownKeywords.TryGetMappedName(fieldSymbol.Name, out string? mapping);

                builder.Add((mapping ?? fieldSymbol.Name, typeName, bufferSize));

                types.Add((INamedTypeSymbol)typeSymbol.ElementType);
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
        /// <param name="isComputeShader">Indicates whether or not <paramref name="structDeclarationSymbol"/> represents a compute shader.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>, and the entry point.</returns>
        private static (string EntryPoint, ImmutableArray<(string Signature, string Definition)> Methods, bool IsSamplerUser) GetProcessedMethods(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            SemanticModelProvider semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            bool isComputeShader)
        {
            // Find all declared methods in the type
            ImmutableArray<MethodDeclarationSyntax> methodDeclarations = (
                from syntaxNode in structDeclaration.DescendantNodes()
                where syntaxNode.IsKind(SyntaxKind.MethodDeclaration)
                select (MethodDeclarationSyntax)syntaxNode).ToImmutableArray();

            using ImmutableArrayBuilder<(string, string)> methods = new();

            string? entryPoint = null;
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

                // Track the implicit sampler, if used
                isSamplerUsed = isSamplerUsed || shaderSourceRewriter.IsSamplerUsed;

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
                    processedMethod = isComputeShader switch
                    {
                        true => new ExecuteMethodRewriter.Compute(shaderSourceRewriter).Visit(processedMethod)!,
                        false => new ExecuteMethodRewriter.Pixel(shaderSourceRewriter).Visit(processedMethod)!
                    };

                    entryPoint = processedMethod.NormalizeWhitespace(eol: "\n").ToFullString();
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

            return (entryPoint!, methods.ToImmutable(), isSamplerUsed);
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
        /// <param name="sourceSymbol">The symbol for the current object being processed.</param>
        /// <param name="types">The sequence of discovered custom types.</param>
        /// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
        /// <returns>A sequence of custom type definitions to add to the shader source.</returns>
        private static ImmutableArray<(string Name, string Definition)> GetDeclaredTypes(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            ISymbol sourceSymbol,
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
                diagnostics.Add(InvalidDiscoveredType, sourceSymbol, sourceSymbol, invalidType);
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
        /// Produces the series of statements to build the current HLSL source.
        /// </summary>
        /// <param name="threadIds">The info on the shader thread count size to use.</param>
        /// <param name="definedConstants">The sequence of defined constants for the shader.</param>
        /// <param name="declaredTypes">The sequence of declared types used by the shader.</param>
        /// <param name="resourceFields">The sequence of resource instance fields for the current shader.</param>
        /// <param name="valueFields">The sequence of value instance fields for the current shader.</param>
        /// <param name="staticFields">The sequence of static fields referenced by the shader.</param>
        /// <param name="sharedBuffers">The sequence of shared buffers declared by the shader.</param>
        /// <param name="processedMethods">The sequence of processed methods used by the shader.</param>
        /// <param name="isComputeShader">Whether or not the current shader type is a compute shader.</param>
        /// <param name="implicitTextureType">The type of the implicit target texture, if present.</param>
        /// <param name="isSamplerUsed">Whether the static sampler is used by the shader.</param>
        /// <param name="executeMethod">The body of the entry point of the shader.</param>
        /// <returns>The series of statements to build the HLSL source to compile to execute the current shader.</returns>
        private static HlslShaderSourceInfo GetHlslSourceInfo(
            ThreadIdsInfo threadIds,
            ImmutableArray<(string Name, string Value)> definedConstants,
            ImmutableArray<(string Name, string Definition)> declaredTypes,
            ImmutableArray<(string MetadataName, string Name, string HlslType)> resourceFields,
            ImmutableArray<(string Name, string HlslType)> valueFields,
            ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> staticFields,
            ImmutableArray<(string Name, string Type, int? Count)> sharedBuffers,
            ImmutableArray<(string Signature, string Definition)> processedMethods,
            bool isComputeShader,
            string? implicitTextureType,
            bool isSamplerUsed,
            string executeMethod)
        {
            using ImmutableArrayBuilder<char> hlslBuilder = new();

            void AppendLF()
            {
                hlslBuilder.Add('\n');
            }

            void AppendLine(string text)
            {
                hlslBuilder.AddRange(text.AsSpan());
            }

            void AppendLineAndLF(string text)
            {
                hlslBuilder.AddRange(text.AsSpan());
                hlslBuilder.Add('\n');
            }

            void AppendCharacterAndLF(char c)
            {
                hlslBuilder.Add(c);
                hlslBuilder.Add('\n');
            }

            string FlushText()
            {
                string text = hlslBuilder.ToString();

                hlslBuilder.Clear();

                return text;
            }

            // Header
            AppendLineAndLF("// ================================================");
            AppendLineAndLF("//                  AUTO GENERATED");
            AppendLineAndLF("// ================================================");
            AppendLineAndLF("// This shader was created by ComputeSharp.");
            AppendLineAndLF("// See: https://github.com/Sergio0694/ComputeSharp.");

            // Group size constants
            AppendLF();
            AppendLine("#define __GroupSize__get_X ");
            AppendLineAndLF(threadIds.X.ToString());
            AppendLine("#define __GroupSize__get_Y ");
            AppendLineAndLF(threadIds.Y.ToString());
            AppendLine("#define __GroupSize__get_Z ");
            AppendLineAndLF(threadIds.Z.ToString());

            // Define declarations
            foreach ((string name, string value) in definedConstants)
            {
                AppendLineAndLF($"#define {name} {value}");
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
            AppendLF();
            AppendLineAndLF("cbuffer _ : register(b0)");
            AppendCharacterAndLF('{');
            AppendLineAndLF("    uint __x;");
            AppendLineAndLF("    uint __y;");

            if (isComputeShader)
            {
                AppendLineAndLF("    uint __z;");
            }

            // User-defined values
            foreach ((string fieldName, string fieldType) in valueFields)
            {
                AppendLineAndLF($"    {fieldType} {fieldName};");
            }

            AppendCharacterAndLF('}');

            int constantBuffersCount = 1;
            int readOnlyBuffersCount = 0;
            int readWriteBuffersCount = 0;

            // Optional implicit texture field
            if (!isComputeShader)
            {
                AppendLF();
                AppendLineAndLF($"{implicitTextureType} __outputTexture : register(u{readWriteBuffersCount++});");
            }

            // Optional sampler field
            if (isSamplerUsed)
            {
                AppendLF();
                AppendLineAndLF("SamplerState __sampler : register(s);");
            }

            // Resources
            foreach ((string metadataName, string fieldName, string fieldType) in resourceFields)
            {
                if (HlslKnownTypes.IsConstantBufferType(metadataName))
                {
                    AppendLF();
                    AppendLineAndLF($"cbuffer _{fieldName} : register(b{constantBuffersCount++})");
                    AppendCharacterAndLF('{');
                    AppendLineAndLF($"    {fieldType} {fieldName}[2];");
                    AppendCharacterAndLF('}');
                }
                else if (HlslKnownTypes.IsReadOnlyTypedResourceType(metadataName))
                {
                    AppendLF();
                    AppendLineAndLF($"{fieldType} {fieldName} : register(t{readOnlyBuffersCount++});");
                }
                else if (HlslKnownTypes.IsReadWriteTypedResourceType(metadataName))
                {
                    AppendLF();
                    AppendLineAndLF($"{fieldType} {fieldName} : register(u{readWriteBuffersCount++});");
                }
            }

            // Shared buffers
            foreach ((string bufferName, string bufferType, int? bufferCount) in sharedBuffers)
            {
                object count = (object?)bufferCount ?? "__GroupSize__get_X * __GroupSize__get_Y * __GroupSize__get_Z";

                AppendLF();
                AppendLineAndLF($"groupshared {bufferType} {bufferName} [{count}];");
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
            AppendLineAndLF("[NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]");
            AppendLineAndLF(executeMethod);

            string hlslSource = FlushText();

            return new(hlslSource, isSamplerUsed);
        }
    }
}