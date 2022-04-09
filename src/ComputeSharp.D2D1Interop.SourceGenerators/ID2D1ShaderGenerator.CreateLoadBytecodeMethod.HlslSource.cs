using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.D2D1Interop.SourceGenerators.Diagnostics;
using ComputeSharp.D2D1Interop.SourceGenerators.Extensions;
using ComputeSharp.D2D1Interop.SourceGenerators.Helpers;
using ComputeSharp.D2D1Interop.SourceGenerators.Mappings;
using ComputeSharp.D2D1Interop.SourceGenerators.SyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.D2D1Interop.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618, RS1024

namespace ComputeSharp.D2D1Interop.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>LoadBytecode</c> method.
    /// </summary>
    internal static partial class LoadBytecode
    {
        /// <summary>
        /// Gathers all necessary information on a transpiled HLSL source for a given shader type.
        /// </summary>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
        /// <param name="diagnostics">The resulting diagnostics from the processing operation.</param>
        /// <returns>The resulting info on the processed shader.</returns>
        public static (string HlslSource, D2D1ShaderProfile? ShaderProfile) GetHlslSource(
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

            // Explore the syntax tree and extract the processed info
            var semanticModelProvider = new SemanticModelProvider(compilation);
            var valueFields = GetInstanceFields(builder, structDeclarationSymbol, discoveredTypes);
            var (entryPoint, processedMethods) = GetProcessedMethods(builder, structDeclaration, structDeclarationSymbol, semanticModelProvider, discoveredTypes, staticMethods, constantDefinitions);
            var declaredTypes = GetDeclaredTypes(discoveredTypes);
            var definedConstants = GetDefinedConstants(constantDefinitions);
            var staticFields = GetStaticFields(builder, semanticModelProvider, structDeclaration, structDeclarationSymbol, discoveredTypes, constantDefinitions);

            // Gather the shader metadata
            GatherD2D1AttributeInfo(
                structDeclarationSymbol,
                out int inputCount,
                out ImmutableArray<int> inputSimpleIndices,
                out ImmutableArray<int> inputComplexIndices,
                out bool requiresScenePosition,
                out D2D1ShaderProfile? shaderProfile);

            diagnostics = builder.ToImmutable();

            // Get the HLSL source
            string hlslSource = GetHlslSource(
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

            return (hlslSource, shaderProfile);
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
        /// <param name="types">The sequence of discovered custom types.</param>
        /// <returns>A sequence of custom type definitions to add to the shader source.</returns>
        private static ImmutableArray<(string Name, string Definition)> GetDeclaredTypes(IEnumerable<INamedTypeSymbol> types)
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
        /// Extracts the metadata definition for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputCount">The number of shader inputs to declare.</param>
        /// <param name="inputSimpleIndices">The indicess of the simple shader inputs.</param>
        /// <param name="inputComplexIndices">The indicess of the complex shader inputs.</param>
        /// <param name="requiresScenePosition">Whether the shader requires the scene position.</param>
        /// <param name="shaderProfile">The shader profile to use to precompile the shader, if requested.</param>
        private static void GatherD2D1AttributeInfo(
            INamedTypeSymbol structDeclarationSymbol,
            out int inputCount,
            out ImmutableArray<int> inputSimpleIndices,
            out ImmutableArray<int> inputComplexIndices,
            out bool requiresScenePosition,
            out D2D1ShaderProfile? shaderProfile)
        {
            inputCount = 0;
            requiresScenePosition = false;
            shaderProfile = null;

            ImmutableArray<int>.Builder inputSimpleIndicesBuilder = ImmutableArray.CreateBuilder<int>();
            ImmutableArray<int>.Builder inputComplexIndicesBuilder = ImmutableArray.CreateBuilder<int>();

            foreach (AttributeData attributeData in structDeclarationSymbol.GetAttributes())
            {
                switch (attributeData.AttributeClass?.GetFullMetadataName())
                {
                    case "ComputeSharp.D2D1Interop.D2DInputCountAttribute":
                        inputCount = (int)attributeData.ConstructorArguments[0].Value!;
                        break;
                    case "ComputeSharp.D2D1Interop.D2DInputSimpleAttribute":
                        inputSimpleIndicesBuilder.Add((int)attributeData.ConstructorArguments[0].Value!);
                        break;
                    case "ComputeSharp.D2D1Interop.D2DInputComplexAttribute":
                        inputComplexIndicesBuilder.Add((int)attributeData.ConstructorArguments[0].Value!);
                        break;
                    case "ComputeSharp.D2D1Interop.D2DRequiresScenePositionAttribute":
                        requiresScenePosition = true;
                        break;
                    case "ComputeSharp.D2D1Interop.D2DEmbeddedBytecodeAttribute":
                        shaderProfile = (D2D1ShaderProfile)attributeData.ConstructorArguments[0].Value!;
                        break;
                    default:
                        break;
                }
            }

            inputSimpleIndices = inputSimpleIndicesBuilder.ToImmutable();
            inputComplexIndices = inputComplexIndicesBuilder.ToImmutable();
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
            foreach (var (name, value) in definedConstants)
            {
                AppendLineAndLF($"#define {name} {value}");
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
