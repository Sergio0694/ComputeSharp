using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Models;
using ComputeSharp.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618, RS1024

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
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
        /// <param name="diagnostics">The resulting diagnostics from the processing operation.</param>
        /// <returns>The resulting info on the processed shader.</returns>
        public static HlslShaderSourceInfo GetInfo(
            Compilation compilation,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            out ImmutableArray<Diagnostic> diagnostics)
        {
            ImmutableArray<Diagnostic>.Builder builder = ImmutableArray.CreateBuilder<Diagnostic>();

            // Properties are not supported
            DetectAndReportPropertyDeclarations(builder, structDeclarationSymbol);

            // We need to sets to track all discovered custom types and static methods
            HashSet<INamedTypeSymbol> discoveredTypes = new(SymbolEqualityComparer.Default);
            Dictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = new(SymbolEqualityComparer.Default);
            Dictionary<IFieldSymbol, string> constantDefinitions = new(SymbolEqualityComparer.Default);

            // A given type can only represent a single shader type
            if (structDeclarationSymbol.AllInterfaces.Count(static interfaceSymbol => interfaceSymbol is { Name: nameof(IComputeShader) } or { IsGenericType: true, Name: nameof(IPixelShader<byte>) }) > 1)
            {
                builder.Add(MultipleShaderTypesImplemented, structDeclarationSymbol, structDeclarationSymbol);
            }

            // Explore the syntax tree and extract the processed info
            var semanticModelProvider = new SemanticModelProvider(compilation);
            var pixelShaderSymbol = structDeclarationSymbol.AllInterfaces.FirstOrDefault(static interfaceSymbol => interfaceSymbol is { IsGenericType: true, Name: nameof(IPixelShader<byte>) });
            var isComputeShader = pixelShaderSymbol is null;
            var implicitTextureType = isComputeShader ? null : HlslKnownTypes.GetMappedNameForPixelShaderType(pixelShaderSymbol!);
            var (resourceFields, valueFields) = GetInstanceFields(builder, structDeclarationSymbol, discoveredTypes, isComputeShader);
            var delegateInstanceFields = GetDispatchId.GetInfo(structDeclarationSymbol);
            var sharedBuffers = GetSharedBuffers(builder, structDeclarationSymbol, discoveredTypes);
            var (entryPoint, processedMethods, isSamplerUsed) = GetProcessedMethods(builder, structDeclaration, structDeclarationSymbol, semanticModelProvider, discoveredTypes, staticMethods, constantDefinitions, isComputeShader);
            var implicitSamplerField = isSamplerUsed ? ("SamplerState", "__sampler") : default((string, string)?);
            var declaredTypes = GetDeclaredTypes(discoveredTypes);
            var definedConstants = GetDefinedConstants(constantDefinitions);
            var staticFields = GetStaticFields(builder, semanticModelProvider, structDeclaration, structDeclarationSymbol, discoveredTypes, constantDefinitions);

            diagnostics = builder.ToImmutable();

            // Get the HLSL source data with the intermediate info
            return GetHlslSourceInfo(
                definedConstants,
                declaredTypes,
                resourceFields,
                valueFields,
                delegateInstanceFields,
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
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <param name="isComputeShader">Indicates whether or not <paramref name="structDeclarationSymbol"/> represents a compute shader.</param>
        /// <returns>A sequence of captured fields in <paramref name="structDeclarationSymbol"/>.</returns>
        private static (
            ImmutableArray<(string MetadataName, string Name, string HlslType)>,
            ImmutableArray<(string Name, string HlslType)>)
            GetInstanceFields(
                ImmutableArray<Diagnostic>.Builder diagnostics,
                INamedTypeSymbol structDeclarationSymbol,
                ICollection<INamedTypeSymbol> types,
                bool isComputeShader)
        {
            ImmutableArray<(string, string, string)>.Builder resources = ImmutableArray.CreateBuilder<(string, string, string)>();
            ImmutableArray<(string, string)>.Builder values = ImmutableArray.CreateBuilder<(string, string)>();
            bool hlslResourceFound = false;

            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
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

                string metadataName = typeSymbol.GetFullMetadataName();
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

                    AttributeData? attribute = fieldSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass?.ToDisplayString() == typeof(GroupSharedAttribute).FullName);

                    if (attribute is not null)
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
        /// Gets a sequence of captured members and their mapped names.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="types">The collection of currently discovered types.</param>
        /// <returns>A sequence of captured members in <paramref name="structDeclarationSymbol"/>.</returns>
        private static ImmutableArray<(string Name, string Type, int? Count)> GetSharedBuffers(
            ImmutableArray<Diagnostic>.Builder diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            ICollection<INamedTypeSymbol> types)
        {
            ImmutableArray<(string, string, int?)>.Builder builder = ImmutableArray.CreateBuilder<(string, string, int?)>();

            foreach (var fieldSymbol in structDeclarationSymbol.GetMembers().OfType<IFieldSymbol>())
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
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The type symbol for the shader type.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> instance for the current type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the type to process.</param>
        /// <param name="discoveredTypes">The collection of currently discovered types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="isComputeShader">Indicates whether or not <paramref name="structDeclarationSymbol"/> represents a compute shader.</param>
        /// <returns>A sequence of processed methods in <paramref name="structDeclaration"/>, and the entry point.</returns>
        private static (string EntryPoint, ImmutableArray<(string Signature, string Definition)> Methods, bool IsSamplerUser) GetProcessedMethods(
            ImmutableArray<Diagnostic>.Builder diagnostics,
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
            ImmutableArray<(string, string)>.Builder methods = ImmutableArray.CreateBuilder<(string, string)>();
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
                    diagnostics,
                    isShaderEntryPoint);

                // Rewrite the method syntax tree
                MethodDeclarationSyntax? processedMethod = shaderSourceRewriter.Visit(methodDeclaration)!.WithoutTrivia();

                // Track the implicit sampler, if used
                isSamplerUsed = isSamplerUsed || shaderSourceRewriter.IsSamplerUsed;

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
        internal static ImmutableArray<(string Name, string Value)> GetDefinedConstants(IReadOnlyDictionary<IFieldSymbol, string> constantDefinitions)
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
        /// <param name="types">The sequence of discovered custom types.</param>
        /// <returns>A sequence of custom type definitions to add to the shader source.</returns>
        internal static ImmutableArray<(string Name, string Definition)> GetDeclaredTypes(IEnumerable<INamedTypeSymbol> types)
        {
            ImmutableArray<(string, string)>.Builder builder = ImmutableArray.CreateBuilder<(string, string)>();

            foreach (var type in HlslKnownTypes.GetCustomTypes(types))
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

            return builder.ToImmutable();
        }

        /// <summary>
        /// Finds and reports all declared properties in a shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        private static void DetectAndReportPropertyDeclarations(ImmutableArray<Diagnostic>.Builder diagnostics, INamedTypeSymbol structDeclarationSymbol)
        {
            foreach (var propertySymbol in structDeclarationSymbol.GetMembers().OfType<IPropertySymbol>())
            {
                diagnostics.Add(DiagnosticDescriptors.PropertyDeclaration, propertySymbol);
            }
        }

        /// <summary>
        /// Produces the series of statements to build the current HLSL source.
        /// </summary>
        /// <param name="definedConstants">The sequence of defined constants for the shader.</param>
        /// <param name="declaredTypes">The sequence of declared types used by the shader.</param>
        /// <param name="resourceFields">The sequence of resource instance fields for the current shader.</param>
        /// <param name="valueFields">The sequence of value instance fields for the current shader.</param>
        /// <param name="delegateInstanceFields">The sequence of delegate fields for the current shader.</param>
        /// <param name="staticFields">The sequence of static fields referenced by the shader.</param>
        /// <param name="sharedBuffers">The sequence of shared buffers declared by the shader.</param>
        /// <param name="processedMethods">The sequence of processed methods used by the shader.</param>
        /// <param name="isComputeShader">Whether or not the current shader type is a compute shader.</param>
        /// <param name="implicitTextureType">The type of the implicit target texture, if present.</param>
        /// <param name="isSamplerUsed">Whether the static sampler is used by the shader.</param>
        /// <param name="executeMethod">The body of the entry point of the shader.</param>
        /// <returns>The series of statements to build the HLSL source to compile to execute the current shader.</returns>
        private static HlslShaderSourceInfo GetHlslSourceInfo(
            ImmutableArray<(string Name, string Value)> definedConstants,
            ImmutableArray<(string Name, string Definition)> declaredTypes,
            ImmutableArray<(string MetadataName, string Name, string HlslType)> resourceFields,
            ImmutableArray<(string Name, string HlslType)> valueFields,
            ImmutableArray<string> delegateInstanceFields,
            ImmutableArray<(string Name, string TypeDeclaration, string? Assignment)> staticFields,
            ImmutableArray<(string Name, string Type, int? Count)> sharedBuffers,
            ImmutableArray<(string Signature, string Definition)> processedMethods,
            bool isComputeShader,
            string? implicitTextureType,
            bool isSamplerUsed,

            string executeMethod)
        {
            StringBuilder hlslBuilder = new();

            void AppendLF()
            {
                hlslBuilder.Append('\n');
            }

            void AppendLine(string text)
            {
                hlslBuilder.Append(text);
            }

            void AppendLineAndLF(string text)
            {
                hlslBuilder.Append(text);
                hlslBuilder.Append('\n');
            }

            void AppendCharacterAndLF(char c)
            {
                hlslBuilder.Append(c);
                hlslBuilder.Append('\n');
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

            string headerAndThreadsX = FlushText();

            AppendLF();
            AppendLine("#define __GroupSize__get_Y ");

            string threadsY = FlushText();

            AppendLF();
            AppendLine("#define __GroupSize__get_Z ");

            string threadsZ = FlushText();

            AppendLF();

            // Define declarations
            foreach (var (name, value) in definedConstants)
            {
                AppendLineAndLF($"#define {name} {value}");
            }

            string defines = FlushText();

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

            string staticFieldsAndDeclaredTypes = FlushText();

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
            foreach (var (fieldName, fieldType) in valueFields)
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
            foreach (var (metadataName, fieldName, fieldType) in resourceFields)
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
            foreach (var (bufferName, bufferType, bufferCount) in sharedBuffers)
            {
                object count = (object?)bufferCount ?? "__GroupSize__get_X * __GroupSize__get_Y * __GroupSize__get_Z";

                AppendLF();
                AppendLineAndLF($"groupshared {bufferType} {bufferName} [{count}];");
            }

            // Forward declarations
            foreach (var (forwardDeclaration, _) in processedMethods)
            {
                AppendLF();
                AppendLineAndLF(forwardDeclaration);
            }

            string capturedFieldsAndResourcesAndForwardDeclarations = FlushText();

            // Captured methods
            foreach (var (_, method) in processedMethods)
            {
                AppendLF();
                AppendLineAndLF(method);
            }

            string capturedMethods = FlushText();

            // Entry point
            AppendLF();
            AppendLineAndLF("[NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]");
            AppendLineAndLF(executeMethod);

            string entryPoint = FlushText();

            return new(
                headerAndThreadsX,
                threadsY,
                threadsZ,
                defines,
                staticFieldsAndDeclaredTypes,
                capturedFieldsAndResourcesAndForwardDeclarations,
                capturedMethods,
                entryPoint,
                implicitTextureType,
                isSamplerUsed,
                declaredTypes.Select(static t => t.Name).ToImmutableArray(),
                definedConstants.Select(static c => c.Name).ToImmutableArray(),
                processedMethods.Select(static m => m.Signature).ToImmutableArray(),
                delegateInstanceFields);
        }

        /// <summary>
        /// Produces the non dynamic HLSL source (concatenating all generated HLSL source chunks).
        /// </summary>
        /// <param name="hlslSourceInfo">The input <see cref="HlslShaderSourceInfo"/> instance to use.</param>
        /// <returns>The non dynamic HLSL source code.</returns>
        public static string GetNonDynamicHlslSource(HlslShaderSourceInfo hlslSourceInfo)
        {
            return
                hlslSourceInfo.HeaderAndThreadsX + "<THREADSX>" +
                hlslSourceInfo.ThreadsY + "<THREADSY>" +
                hlslSourceInfo.ThreadsZ + "<THREADSZ>" +
                hlslSourceInfo.Defines +
                hlslSourceInfo.StaticFieldsAndDeclaredTypes +
                hlslSourceInfo.CapturedFieldsAndResourcesAndForwardDeclarations +
                hlslSourceInfo.CapturedMethods +
                hlslSourceInfo.EntryPoint;
        }
    }
}
