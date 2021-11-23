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
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

#pragma warning disable CS0618, RS1024

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
public sealed partial class IShaderGenerator
{
    /// <inheritdoc/>
    private static partial MethodDeclarationSyntax CreateBuildHlslStringMethod(
        GeneratorExecutionContext context,
        StructDeclarationSyntax structDeclaration,
        INamedTypeSymbol structDeclarationSymbol,
        out string? implicitTextureType,
        out bool isSamplerUsed,
        out string hlslSource)
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
        var semanticModel = new SemanticModelProvider(context.Compilation);
        var pixelShaderSymbol = structDeclarationSymbol.AllInterfaces.FirstOrDefault(static interfaceSymbol => interfaceSymbol is { IsGenericType: true, Name: nameof(IPixelShader<byte>) });
        var isComputeShader = pixelShaderSymbol is null;
        var pixelShaderTextureType = isComputeShader ? null : HlslKnownTypes.GetMappedNameForPixelShaderType(pixelShaderSymbol!);
        var processedMembers = GetProcessedFields(context, structDeclarationSymbol, discoveredTypes, isComputeShader).ToArray();
        var sharedBuffers = GetGroupSharedMembers(context, structDeclarationSymbol, discoveredTypes).ToArray();
        var (entryPoint, processedMethods, accessesStaticSampler) = GetProcessedMethods(context, structDeclaration, structDeclarationSymbol, semanticModel, discoveredTypes, staticMethods, constantDefinitions, isComputeShader);
        var implicitSamplerField = accessesStaticSampler ? ("SamplerState", "__sampler") : default((string, string)?);
        var processedTypes = GetProcessedTypes(discoveredTypes).ToArray();
        var processedConstants = GetProcessedConstants(constantDefinitions);
        var staticFields = GetStaticFields(context, semanticModel, structDeclaration, structDeclarationSymbol, discoveredTypes, constantDefinitions);
        string namespaceName = structDeclarationSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces));
        string structName = structDeclaration.Identifier.Text;
        SyntaxTokenList structModifiers = structDeclaration.Modifiers;
        IEnumerable<StatementSyntax> bodyStatements = GenerateRenderMethodBody(
            processedConstants,
            staticFields,
            processedTypes,
            isComputeShader,
            processedMembers,
            pixelShaderTextureType,
            accessesStaticSampler,
            sharedBuffers,
            processedMethods,
            entryPoint,
            out hlslSource);

        implicitTextureType = pixelShaderTextureType;
        isSamplerUsed = accessesStaticSampler;

        // This code produces a method declaration as follows:
        //
        // readonly void global::ComputeSharp.__Internals.IShader.BuildHlslString(ref global::ComputeSharp.__Internals.ArrayPoolStringBuilder builder, int threadsX, int threadsY, int threadsZ)
        // {
        //     <BODY>
        // }
        return
            MethodDeclaration(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                Identifier("BuildHlslString"))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
            .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
            .AddParameterListParameters(
                Parameter(Identifier("builder")).AddModifiers(Token(SyntaxKind.OutKeyword)).WithType(IdentifierName("global::ComputeSharp.__Internals.ArrayPoolStringBuilder")),
                Parameter(Identifier("threadsX")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                Parameter(Identifier("threadsY")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                Parameter(Identifier("threadsZ")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))))
            .WithBody(Block(bodyStatements));
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

                string? assignment = staticFieldRewriter.Visit(variableDeclarator)?.NormalizeWhitespace(eol: "\n").ToFullString();

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
    private static (string EntryPoint, IEnumerable<(string Signature, string Definition)> ProcessedMethods, bool IsSamplerUser) GetProcessedMethods(
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
        List<(string, string)> processedMethods = new();
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
                processedMethods.Add((
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
                processedMethods.Add((
                    processedMethod.AsDefinition().NormalizeWhitespace(eol: "\n").ToFullString(),
                    processedMethod.NormalizeWhitespace(eol: "\n").ToFullString()));
            }
        }

        // Process static methods as well
        foreach (MethodDeclarationSyntax staticMethod in staticMethods.Values)
        {
            processedMethods.Add((
                staticMethod.AsDefinition().NormalizeWhitespace(eol: "\n").ToFullString(),
                staticMethod.NormalizeWhitespace(eol: "\n").ToFullString()));
        }

        return (entryPoint!, processedMethods, isSamplerUsed);
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
    internal static IEnumerable<(string Name, string Definition)> GetProcessedTypes(IEnumerable<INamedTypeSymbol> types)
    {
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
            yield return
                (structType,
                 structDeclaration
                 .NormalizeWhitespace(eol: "\n")
                 .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                 .ToFullString());
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

    /// <summary>
    /// Produces the series of statements to build the current HLSL source.
    /// </summary>
    /// <param name="definedConstants">The sequence of defined constants for the shader.</param>
    /// <param name="staticFields">The sequence of static fields referenced by the shader.</param>
    /// <param name="declaredTypes">The sequence of declared types used by the shader.</param>
    /// <param name="isComputeShader">Whether or not the current shader is a compute shader (or a pixel shader).</param>
    /// <param name="instanceFields">The sequence of instance fields for the current shader.</param>
    /// <param name="implicitTextureType">The type of the implicit target texture, if present.</param>
    /// <param name="isSamplerUsed">Whether the static sampler is used by the shader.</param>
    /// <param name="sharedBuffers">The sequence of shared buffers declared by the shader.</param>
    /// <param name="processedMethods">The sequence of processed methods used by the shader.</param>
    /// <param name="executeMethod">The body of the entry point of the shader.</param>
    /// <param name="hlslSource">The generated HLSL source code (ignoring captured delegates, if present).</param>
    /// <returns>The series of statements to build the HLSL source to compile to execute the current shader.</returns>
    private static IEnumerable<StatementSyntax> GenerateRenderMethodBody(
        IEnumerable<(string Name, string Value)> definedConstants,
        IEnumerable<(string Name, string TypeDeclaration, string? Assignment)> staticFields,
        IEnumerable<(string Name, string Definition)> declaredTypes,
        bool isComputeShader,
        IEnumerable<(IFieldSymbol Symbol, string HlslName, string HlslType)> instanceFields,
        string? implicitTextureType,
        bool isSamplerUsed,
        IEnumerable<(string Name, string Type, int? Count)> sharedBuffers,
        IEnumerable<(string Signature, string Definition)> processedMethods,
        string executeMethod,
        out string hlslSource)
    {
        List<StatementSyntax> statements = new();
        StringBuilder chunkedTextBuilder = new();
        StringBuilder aggregateTextBuilder = new();
        int capturedDelegates = 0;
        int prologueStatements = 0;
        int sizeHint = 64;

        void AppendLF()
        {
            chunkedTextBuilder.Append('\n');
        }

        void AppendLine(string text)
        {
            chunkedTextBuilder.Append(text);
        }

        void AppendLineAndLF(string text)
        {
            chunkedTextBuilder.Append(text);
            chunkedTextBuilder.Append('\n');
        }

        void AppendCharacterAndLF(char c)
        {
            chunkedTextBuilder.Append(c);
            chunkedTextBuilder.Append('\n');
        }

        void AppendThreadNumIdentifier(string name)
        {
            aggregateTextBuilder.Append(name);
        }

        void AppendParsedStatement(string text)
        {
            FlushText();

            statements.Add(ParseStatement(text));
        }

        void FlushText()
        {
            if (chunkedTextBuilder.Length > 0)
            {
                string chunkedText = chunkedTextBuilder.ToString();

                aggregateTextBuilder.Append(chunkedText);

                sizeHint += chunkedTextBuilder.Length;

                statements.Add(
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("builder"), IdentifierName("Append")))
                            .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(chunkedText))))));

                chunkedTextBuilder.Clear();
            }
        }

        // Declare the hashsets to track imported members and types from delegates, if needed
        if (instanceFields.Any(static t => t.Symbol.Type.TypeKind == TypeKind.Delegate))
        {
            void DeclareMapping(int index, string name, IEnumerable<string> items)
            {
                // global::System.Collections.Generic.HashSet<string> <NAME> = new();
                statements.Insert(index,
                    LocalDeclarationStatement(VariableDeclaration(
                    GenericName(Identifier("global::System.Collections.Generic.HashSet"))
                    .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.StringKeyword))))
                    .AddVariables(
                        VariableDeclarator(Identifier(name))
                        .WithInitializer(EqualsValueClause(ImplicitObjectCreationExpression())))));

                prologueStatements++;

                // <NAME>.Add("<ITEM>");
                foreach (var item in items)
                {
                    statements.Add(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName(name),
                                    IdentifierName("Add")))
                            .AddArgumentListArguments(Argument(
                                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(item))))));

                    prologueStatements++;
                }
            }

            DeclareMapping(0, "__typeNames", declaredTypes.Select(static t => t.Name));
            DeclareMapping(1, "__constantNames", definedConstants.Select(static t => t.Name));
            DeclareMapping(2, "__methodNames", processedMethods.Select(static t => t.Signature));

            // Go through all existing delegate fields, if any
            foreach (var (fieldSymbol, fieldName, fieldType) in instanceFields.Where(static t => t.Symbol.Type.TypeKind == TypeKind.Delegate))
            {
                // global::ComputeSharp.__Internals.ShaderMethodSourceAttribute __<DELEGATE_NAME>Attribute = global::ComputeSharp.__Internals.ShaderMethodSourceAttribute.GetForDelegate(<DELEGATE_NAME>, "<DELEGATE_NAME>");
                statements.Add(
                    LocalDeclarationStatement(VariableDeclaration(IdentifierName($"global::ComputeSharp.__Internals.{nameof(ShaderMethodSourceAttribute)}"))
                    .AddVariables(
                        VariableDeclarator(Identifier($"__{fieldName}Attribute"))
                        .WithInitializer(
                            EqualsValueClause(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName($"global::ComputeSharp.__Internals.{nameof(ShaderMethodSourceAttribute)}"),
                                        IdentifierName(nameof(ShaderMethodSourceAttribute.GetForDelegate))))
                                .AddArgumentListArguments(
                                    Argument(IdentifierName(fieldName)),
                                    Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(fieldName)))))))));

                capturedDelegates++;
                prologueStatements++;
            }
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
        AppendParsedStatement("builder.Append(threadsX);");
        AppendThreadNumIdentifier("<THREADSX>");
        AppendLF();
        AppendLine("#define __GroupSize__get_Y ");
        AppendParsedStatement("builder.Append(threadsY);");
        AppendThreadNumIdentifier("<THREADSY>");
        AppendLF();
        AppendLine("#define __GroupSize__get_Z ");
        AppendParsedStatement("builder.Append(threadsZ);");
        AppendThreadNumIdentifier("<THREADSZ>");
        AppendLF();

        // Define declarations
        foreach (var (name, value) in definedConstants)
        {
            AppendLineAndLF($"#define {name} {value}");
        }

        // Defines from captured delegates
        foreach (var (_, fieldName, _) in instanceFields.Where(static t => t.Symbol.Type.TypeKind == TypeKind.Delegate))
        {
            AppendParsedStatement($"__{fieldName}Attribute.AppendConstants(ref builder, __constantNames);");
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

        // Declared types from captured delegates
        foreach (var (_, fieldName, _) in instanceFields.Where(static t => t.Symbol.Type.TypeKind == TypeKind.Delegate))
        {
            AppendParsedStatement($"__{fieldName}Attribute.AppendTypes(ref builder, __typeNames);");
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
        foreach (var (fieldSymbol, fieldName, fieldType) in instanceFields.Where(static t => t.Symbol.Type.IsUnmanagedType))
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
        foreach (var (fieldSymbol, fieldName, fieldType) in instanceFields)
        {
            string metadataName = fieldSymbol.Type.GetFullMetadataName();

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
            else if (HlslKnownTypes.IsTypedResourceType(metadataName))
            {
                AppendLF();
                AppendLineAndLF($"{fieldType} {fieldName} : register(u{readWriteBuffersCount++});");
            }
        }

        // Shared buffers
        foreach (var (bufferName, bufferType, bufferCount) in sharedBuffers)
        {
            object count = (object?)bufferCount ?? "threadsX * threadsY * threadsZ";

            AppendLF();
            AppendLineAndLF($"groupshared {bufferType} {bufferName} [{count}];");
        }

        // Forward declarations
        foreach (var (forwardDeclaration, _) in processedMethods)
        {
            AppendLF();
            AppendLineAndLF(forwardDeclaration);
        }

        // Forward declarations from captured delegates
        foreach (var (_, fieldName, _) in instanceFields.Where(static t => t.Symbol.Type.TypeKind == TypeKind.Delegate))
        {
            AppendParsedStatement($"__{fieldName}Attribute.AppendForwardDeclarations(ref builder, __methodNames);");
        }

        // Remove all forward declarations from methods that are embedded into the shader.
        // This is necessary to avoid duplicate definitions from methods from delegates.
        if (capturedDelegates > 0)
        {
            // <NAME>.Add("<ITEM>");
            foreach (var (forwardDeclaration, _) in processedMethods)
            {
                FlushText();

                statements.Add(
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("__methodNames"),
                                IdentifierName("Remove")))
                        .AddArgumentListArguments(Argument(
                            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(forwardDeclaration))))));
            }
        }

        // Captured methods
        foreach (var (_, method) in processedMethods)
        {
            AppendLF();
            AppendLineAndLF(method);
        }

        // Captured methods from captured delegates
        foreach (var (_, fieldName, _) in instanceFields.Where(static t => t.Symbol.Type.TypeKind == TypeKind.Delegate))
        {
            AppendParsedStatement($"__{fieldName}Attribute.AppendMethods(ref builder, __methodNames);");
        }

        // Captured delegate methods
        foreach (var (fieldSymbol, fieldName, _) in instanceFields.Where(static t => t.Symbol.Type.TypeKind == TypeKind.Delegate))
        {
            AppendLF();
            AppendParsedStatement($"__{fieldName}Attribute.AppendMappedInvokeMethod(ref builder, \"{fieldName}\");");
            AppendLF();
        }

        // Entry point
        AppendLF();
        AppendLineAndLF("[NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]");
        AppendLineAndLF(executeMethod);

        FlushText();

        // builder = global::ComputeSharp.__Internals.ArrayPoolStringBuilder.Create(<SIZE_HINT>);
        statements.Insert(
            prologueStatements,
            ExpressionStatement(
                AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    IdentifierName("builder"),
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("global::ComputeSharp.__Internals.ArrayPoolStringBuilder"),
                            IdentifierName("Create")))
                    .AddArgumentListArguments(
                        Argument(LiteralExpression(
                            SyntaxKind.NumericLiteralExpression,
                            Literal(sizeHint)))))));

        hlslSource = aggregateTextBuilder.ToString();

        return statements;
    }
}
