using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using ComputeSharp.SourceGeneration.SyntaxRewriters;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>HlslSource</c> property.
    /// </summary>
    internal static partial class HlslSource
    {
        /// <summary>
        /// Gathers all necessary information on a transpiled HLSL source for a given shader type.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type.</param>
        /// <param name="threadsX">The thread ids value for the X axis.</param>
        /// <param name="threadsY">The thread ids value for the Y axis.</param>
        /// <param name="threadsZ">The thread ids value for the Z axis.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <param name="isImplicitTextureUsed">Indicates whether the current shader uses an implicit texture.</param>
        /// <param name="isSamplerUsed">Whether or not the static sampler is used.</param>
        /// <param name="hlslSource">The resulting HLSL source for the current shader.</param>
        public static void GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            Compilation compilation,
            INamedTypeSymbol structDeclarationSymbol,
            int threadsX,
            int threadsY,
            int threadsZ,
            CancellationToken token,
            out bool isImplicitTextureUsed,
            out bool isSamplerUsed,
            out string hlslSource)
        {
            // Detect any invalid properties
            HlslDefinitionsSyntaxProcessor.DetectAndReportInvalidPropertyDeclarations(diagnostics, structDeclarationSymbol);

            // We need to sets to track all discovered custom types and static methods
            HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, (MethodDeclarationSyntax, MethodDeclarationSyntax)> constructors = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, (string, string, string?)> staticFieldDefinitions = new(SymbolEqualityComparer.Default);

            // Setup the semantic model and basic properties
            INamedTypeSymbol? pixelShaderSymbol = structDeclarationSymbol.AllInterfaces.FirstOrDefault(static interfaceSymbol => interfaceSymbol is { IsGenericType: true, Name: "IComputeShader" });
            bool isComputeShader = pixelShaderSymbol is null;
            string? implicitTextureType = isComputeShader ? null : HlslKnownTypes.GetMappedNameForPixelShaderType(pixelShaderSymbol!);

            token.ThrowIfCancellationRequested();

            (ImmutableArray<(string MetadataName, string Name, string HlslType)> resourceFields, ImmutableArray<(string Name, string HlslType)> valueFields) = GetInstanceFields(
                diagnostics,
                structDeclarationSymbol,
                discoveredTypes,
                isComputeShader);

            token.ThrowIfCancellationRequested();

            ImmutableArray<(string Name, string Type, int? Count)> sharedBuffers = GetSharedBuffers(
                diagnostics,
                structDeclarationSymbol,
                discoveredTypes);

            token.ThrowIfCancellationRequested();

            SemanticModelProvider semanticModelProvider = new(compilation);

            (string entryPoint, ImmutableArray<(string Signature, string Definition)> processedMethods, isSamplerUsed) = GetProcessedMethods(
                diagnostics,
                structDeclarationSymbol,
                semanticModelProvider,
                discoveredTypes,
                staticMethods,
                instanceMethods,
                constructors,
                constantDefinitions,
                staticFieldDefinitions,
                isComputeShader,
                token);

            token.ThrowIfCancellationRequested();

            (string, string)? implicitSamplerField = isSamplerUsed ? ("SamplerState", "__sampler") : default((string, string)?);
            ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> staticFields = GetStaticFields(
                diagnostics,
                semanticModelProvider,
                structDeclarationSymbol,
                discoveredTypes,
                constantDefinitions,
                staticFieldDefinitions,
                token);

            token.ThrowIfCancellationRequested();

            // Process the discovered types and constants
            ImmutableArray<(string Name, string Definition)> declaredTypes = HlslDefinitionsSyntaxProcessor.GetDeclaredTypes(
                diagnostics,
                structDeclarationSymbol,
                discoveredTypes,
                instanceMethods,
                constructors);

            token.ThrowIfCancellationRequested();

            ImmutableArray<(string Name, string Value)> definedConstants = HlslDefinitionsSyntaxProcessor.GetDefinedConstants(constantDefinitions);

            token.ThrowIfCancellationRequested();

            // Check whether an implicit texture is used in the shader
            isImplicitTextureUsed = implicitTextureType is not null;

            // Get the HLSL source data with the intermediate info
            hlslSource = GetHlslSourceInfo(
                threadsX,
                threadsY,
                threadsZ,
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

                // Allowed fields must be either resources, unmanaged values or delegates
                if (HlslKnownTypes.IsTypedResourceType(metadataName))
                {
                    hlslResourceFound = true;

                    // Track the type of items in the current buffer
                    if (HlslKnownTypes.IsStructuredBufferType(metadataName))
                    {
                        types.Add((INamedTypeSymbol)typeSymbol.TypeArguments[0]);
                    }

                    // Check whether the resource is a globallycoherent writeable buffer
                    if (HlslKnownTypes.IsReadWriteBufferType(metadataName) &&
                        fieldSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.GloballyCoherentAttribute", out _))
                    {
                        typeName = "globallycoherent " + typeName;
                    }

                    // Add the current mapping for the name (if the name used a reserved keyword)
                    resources.Add((metadataName, mapping ?? fieldName, typeName));
                }
                else if (!typeSymbol.IsUnmanagedType &&
                         typeSymbol.TypeKind != TypeKind.Delegate)
                {
                    // Shaders can only capture valid HLSL resource types or unmanaged types
                    diagnostics.Add(InvalidShaderField, fieldSymbol, structDeclarationSymbol, fieldName, typeSymbol);

                    continue;
                }
                else if (typeSymbol.IsUnmanagedType)
                {
                    // Track the type if it's a custom struct
                    if (!HlslKnownTypes.IsKnownHlslType(metadataName))
                    {
                        types.Add(typeSymbol);
                    }

                    values.Add((mapping ?? fieldName, typeName));
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
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="staticFieldDefinitions">The collection of discovered static field definitions.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <returns>A sequence of static constant fields in <paramref name="structDeclarationSymbol"/>.</returns>
        private static ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> GetStaticFields(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            SemanticModelProvider semanticModel,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            IDictionary<IFieldSymbol, (string, string, string?)> staticFieldDefinitions,
            CancellationToken token)
        {
            using ImmutableArrayBuilder<(string, string, string?)> builder = new();

            foreach (ISymbol memberSymbol in structDeclarationSymbol.GetMembers())
            {
                // Pre-filter fields that cannot be valid, to reduce the calls to check the [GroupShared] attribute
                if (memberSymbol is not IFieldSymbol { IsImplicitlyDeclared: false, IsStatic: true, IsConst: false, } fieldSymbol)
                {
                    continue;
                }

                // Ignore [GroupShared] fields, they'll be gathered in a separate rewriting step
                if (fieldSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.GroupSharedAttribute", out _))
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
                    out _))
                {
                    builder.Add((name, typeDeclaration, assignmentExpression));
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

            foreach (ISymbol memberSymbol in structDeclarationSymbol.GetMembers())
            {
                // We're only looking for static fields of a valid type for group shared buffers
                if (memberSymbol is not IFieldSymbol { IsStatic: true, Type: IArrayTypeSymbol { ElementType.IsUnmanagedType: true } typeSymbol } fieldSymbol)
                {
                    continue;
                }

                if (!fieldSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.GroupSharedAttribute", out AttributeData? attribute))
                {
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
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
        /// <param name="constructors">The collection of discovered constructors for custom struct types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="staticFieldDefinitions">The collection of discovered static field definitions.</param>
        /// <param name="isComputeShader">Indicates whether or not <paramref name="structDeclarationSymbol"/> represents a compute shader.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclarationSymbol"/>, and the entry point.</returns>
        private static (string EntryPoint, ImmutableArray<(string Signature, string Definition)> Methods, bool IsSamplerUser) GetProcessedMethods(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            SemanticModelProvider semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods,
            IDictionary<IMethodSymbol, (MethodDeclarationSyntax, MethodDeclarationSyntax)> constructors,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            IDictionary<IFieldSymbol, (string, string, string?)> staticFieldDefinitions,
            bool isComputeShader,
            CancellationToken token)
        {
            using ImmutableArrayBuilder<(string, string)> methods = new();

            string? entryPoint = null;
            bool isSamplerUsed = false;

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
                    (isComputeShader &&
                     methodSymbol.Name == "Execute" &&
                     methodSymbol.ReturnsVoid &&
                     methodSymbol.TypeParameters.Length == 0 &&
                     methodSymbol.Parameters.Length == 0) ||
                    (!isComputeShader &&
                     methodSymbol.Name == "Execute" &&
                     methodSymbol.ReturnType is not null && // TODO: match for pixel type
                     methodSymbol.TypeParameters.Length == 0 &&
                     methodSymbol.Parameters.Length == 0);

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

            token.ThrowIfCancellationRequested();

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
        /// Produces the series of statements to build the current HLSL source.
        /// </summary>
        /// <param name="threadsX">The thread ids value for the X axis.</param>
        /// <param name="threadsY">The thread ids value for the Y axis.</param>
        /// <param name="threadsZ">The thread ids value for the Z axis.</param>
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
        /// <returns>The HLSL source for the current shader.</returns>
        private static string GetHlslSourceInfo(
            int threadsX,
            int threadsY,
            int threadsZ,
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
            AppendLineAndLF($"#define __GroupSize__get_X {threadsX}");
            AppendLineAndLF($"#define __GroupSize__get_Y {threadsY}");
            AppendLineAndLF($"#define __GroupSize__get_Z {threadsZ}");

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

            return FlushText();
        }
    }
}