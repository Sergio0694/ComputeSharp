using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A source generator creating data loaders for the <see cref="ID2D1PixelShader"/> type.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class ID2D1ShaderGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Discover all shader types and extract all the necessary info from each of them
        IncrementalValuesProvider<D2D1ShaderInfo> shaderInfoWithErrors =
            context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => node.IsTypeDeclarationWithOrPotentiallyWithBaseTypes<StructDeclarationSyntax>(),
                static (context, token) =>
                {
                    // The source generator requires unsafe blocks to be enabled (eg. for pointers, [SkipLocalsInit], etc.)
                    if (!context.SemanticModel.Compilation.IsAllowUnsafeBlocksEnabled())
                    {
                        return default;
                    }

                    StructDeclarationSyntax typeDeclaration = (StructDeclarationSyntax)context.Node;

                    // If the type symbol doesn't have at least one interface, it can't possibly be a shader type
                    if (context.SemanticModel.GetDeclaredSymbol(typeDeclaration, token) is not INamedTypeSymbol { AllInterfaces.Length: > 0 } typeSymbol)
                    {
                        return default;
                    }

                    // Check that the shader implements the ID2D1PixelShader interface
                    if (!IsD2D1PixelShaderType(typeSymbol, context.SemanticModel.Compilation))
                    {
                        return default;
                    }

                    // EffectId info
                    ImmutableArray<byte> effectId = EffectId.GetInfo(context.SemanticModel.Compilation, typeSymbol);

                    token.ThrowIfCancellationRequested();

                    // EffectDisplayName info
                    string? effectDisplayName = EffectMetadata.GetEffectDisplayNameInfo(context.SemanticModel.Compilation, typeSymbol);

                    token.ThrowIfCancellationRequested();

                    // EffectDescription info
                    string? effectDescription = EffectMetadata.GetEffectDescriptionInfo(context.SemanticModel.Compilation, typeSymbol);

                    token.ThrowIfCancellationRequested();

                    // EffectCategory info
                    string? effectCategory = EffectMetadata.GetEffectCategoryInfo(context.SemanticModel.Compilation, typeSymbol);

                    token.ThrowIfCancellationRequested();

                    // EffectAuthor info
                    string? effectAuthor = EffectMetadata.GetEffectAuthorInfo(context.SemanticModel.Compilation, typeSymbol);

                    token.ThrowIfCancellationRequested();

                    using ImmutableArrayBuilder<DiagnosticInfo> diagnostics = ImmutableArrayBuilder<DiagnosticInfo>.Rent();

                    // LoadDispatchData() info
                    ImmutableArray<FieldInfo> fieldInfos = LoadDispatchData.GetInfo(
                        diagnostics,
                        typeSymbol,
                        out int constantBufferSizeInBytes);

                    token.ThrowIfCancellationRequested();

                    // Get the input info for InputTypes
                    InputTypes.GetInfo(
                        diagnostics,
                        typeSymbol,
                        out int inputCount,
                        out ImmutableArray<int> inputSimpleIndices,
                        out ImmutableArray<int> inputComplexIndices,
                        out ImmutableArray<uint> inputTypes);

                    // Get the resource texture info for ResourceTextureDescriptions
                    ResourceTextureDescriptions.GetInfo(
                        diagnostics,
                        typeSymbol,
                        inputCount,
                        out ImmutableArray<ResourceTextureDescription> resourceTextureDescriptions);

                    // Get HLSL source for HlslSource
                    string hlslSource = HlslSource.GetHlslSource(
                        diagnostics,
                        context.SemanticModel.Compilation,
                        typeDeclaration,
                        typeSymbol,
                        inputCount,
                        inputSimpleIndices,
                        inputComplexIndices);

                    token.ThrowIfCancellationRequested();

                    // Get the shader profile and linking info for LoadBytecode()
                    D2D1ShaderProfile? shaderProfile = LoadBytecode.GetShaderProfile(typeSymbol);
                    D2D1CompileOptions? compileOptions = LoadBytecode.GetCompileOptions(diagnostics, typeSymbol);
                    bool isLinkingSupported = LoadBytecode.IsSimpleInputShader(typeSymbol, inputCount);

                    token.ThrowIfCancellationRequested();

                    // Get the info for the output buffer properties
                    OutputBuffer.GetInfo(typeSymbol, out D2D1BufferPrecision bufferPrecision, out D2D1ChannelDepth channelDepth);

                    token.ThrowIfCancellationRequested();

                    // Get the info for InputDescriptions
                    InputDescriptions.GetInfo(
                        diagnostics,
                        typeSymbol,
                        out ImmutableArray<InputDescription> inputDescriptions);

                    token.ThrowIfCancellationRequested();

                    // Get the info for PixelOptions
                    PixelOptions.GetInfo(typeSymbol, out D2D1PixelOptions pixelOptions);

                    token.ThrowIfCancellationRequested();

                    return new D2D1ShaderInfo(
                        Hierarchy: HierarchyInfo.From(typeSymbol),
                        EffectId: new EffectIdInfo(effectId),
                        EffectDisplayName: effectDisplayName,
                        EffectDescription: effectDescription,
                        EffectCategory: effectCategory,
                        EffectAuthor: effectAuthor,
                        DispatchData: new DispatchDataInfo(fieldInfos, constantBufferSizeInBytes),
                        InputTypes: new InputTypesInfo(inputTypes),
                        ResourceTextureDescriptions: new ResourceTextureDescriptionsInfo(resourceTextureDescriptions),
                        HlslShaderSource: new HlslShaderSourceInfo(
                            hlslSource,
                            shaderProfile,
                            compileOptions,
                            isLinkingSupported,
                            HasErrors: diagnostics.Count > 0),
                        OutputBuffer: new OutputBufferInfo(bufferPrecision, channelDepth),
                        InputDescriptions: new InputDescriptionsInfo(inputDescriptions),
                        PixelOptions: pixelOptions,
                        Diagnostcs: diagnostics.ToImmutable());
                })
            .Where(static item => item is not null)!;

        // Output the diagnostics, if any
        context.ReportDiagnostics(
            shaderInfoWithErrors
            .Select(static (item, _) => item.Diagnostcs));

        // Get the EffectId info (hierarchy and effect id info)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EffectIdInfo EffectId)> effectIdInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.EffectId));

        // Generate the EffectId properties
        context.RegisterSourceOutput(effectIdInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax effectDisplayNameProperty = EffectId.GetSyntax(item.EffectId, out Func<SyntaxNode, SourceText> fixup);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, effectDisplayNameProperty, skipLocalsInit: false);
            SourceText text = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(EffectId)}.g.cs", text);
        });

        // Get the EffectDisplayName info (hierarchy and effect display name)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string? EffectDisplayName)> effectDisplayNameInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.EffectDisplayName));

        // Generate the EffectDisplayName properties
        context.RegisterSourceOutput(effectDisplayNameInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax effectDisplayNameProperty = EffectMetadata.GetEffectDisplayNameSyntax(item.EffectDisplayName);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, effectDisplayNameProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.EffectDisplayName.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the EffectDescription info (hierarchy and effect description)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string? EffectDescription)> effectDescriptionInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.EffectDescription));

        // Generate the EffectDescription properties
        context.RegisterSourceOutput(effectDescriptionInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax effectDescriptionProperty = EffectMetadata.GetEffectDescriptionSyntax(item.EffectDescription);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, effectDescriptionProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.EffectDescription.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the EffectCategory info (hierarchy and effect description)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string? EffectCategory)> effectCategoryInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.EffectCategory));

        // Generate the EffectCategory properties
        context.RegisterSourceOutput(effectCategoryInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax effectCategoryProperty = EffectMetadata.GetEffectCategorySyntax(item.EffectCategory);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, effectCategoryProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.EffectCategory.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the EffectAuthor info (hierarchy and effect description)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string? EffectAuthor)> effectAuthorInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.EffectAuthor));

        // Generate the EffectAuthor properties
        context.RegisterSourceOutput(effectAuthorInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax effectCategoryProperty = EffectMetadata.GetEffectAuthorSyntax(item.EffectAuthor);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, effectCategoryProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.EffectAuthor.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the InputCount info (hierarchy and input count)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, int InputCount)> inputCountInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.InputTypes.InputTypes.Length));

        // Generate the InputCount properties
        context.RegisterSourceOutput(inputCountInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax inputCountProperty = InputCount.GetSyntax(item.InputCount);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, inputCountProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(InputCount)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the InputTypes info (hierarchy and input types)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, InputTypesInfo InputTypes)> inputTypesInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.InputTypes));

        // Generate the InputTypes properties
        context.RegisterSourceOutput(inputTypesInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax inputTypesProperty = InputTypes.GetSyntax(item.InputTypes.InputTypes, out TypeDeclarationSyntax[] additionalTypes);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, inputTypesProperty, skipLocalsInit: false, additionalMemberDeclarations: additionalTypes);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(InputTypes)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the ResourceTextureDescriptions info (hierarchy and resource texture descriptions)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, ResourceTextureDescriptionsInfo ResourceTextureDescriptions)> resourceTextureDescriptionsInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.ResourceTextureDescriptions));

        // Generate the ResourceTextureDescriptions properties
        context.RegisterSourceOutput(resourceTextureDescriptionsInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax resourceTextureDescriptionsProperty = ResourceTextureDescriptions.GetSyntax(item.ResourceTextureDescriptions, out TypeDeclarationSyntax[] additionalTypes);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, resourceTextureDescriptionsProperty, skipLocalsInit: false, additionalMemberDeclarations: additionalTypes);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(ResourceTextureDescriptions)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the info for InitializeFromDispatchData() and LoadDispatchData() (hierarchy and dispatch data)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchDataInfo Dispatch)> dispatchDataInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.DispatchData));

        // Generate the InitializeFromDispatchData() and LoadDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo, static (context, item) =>
        {
            MethodDeclarationSyntax initializeFromDispatchDataMethod = InitializeFromDispatchData.GetSyntax(item.Dispatch);
            MethodDeclarationSyntax loadDispatchDataMethod = LoadDispatchData.GetSyntax(item.Hierarchy, item.Dispatch, out TypeDeclarationSyntax[] additionalTypes);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMembers(
                item.Hierarchy,
                memberDeclarations: new (MemberDeclarationSyntax, bool)[] { (initializeFromDispatchDataMethod, false), (loadDispatchDataMethod, true) },
                additionalMemberDeclarations: additionalTypes);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(LoadDispatchData)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the HlslSource info (hierarchy, HLSL source and parsing options)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string HlslSource)> hlslSourceInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.HlslShaderSource.HlslSource));

        // Generate the HlslSource properties
        context.RegisterSourceOutput(hlslSourceInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax hlslStringProperty = HlslSource.GetSyntax(item.HlslSource, item.Hierarchy.Hierarchy.Length);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, hlslStringProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(HlslSource)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get a filtered sequence to enable caching for the HLSL source info, before the shader compilation step
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslShaderSourceInfo Source)> shaderBytecodeInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.HlslShaderSource));

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo, DeferredDiagnosticInfo? Diagnostic)> embeddedBytecodeWithErrors =
            shaderBytecodeInfo
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = LoadBytecode.GetBytecode(
                    item.Source,
                    token,
                    out D2D1ShaderProfile shaderProfile,
                    out D2D1CompileOptions compileOptions,
                    out DeferredDiagnosticInfo? diagnostic);

                token.ThrowIfCancellationRequested();

                EmbeddedBytecodeInfo bytecodeInfo = new(
                    item.Source.HlslSource,
                    shaderProfile,
                    compileOptions,
                    bytecode);

                return (item.Hierarchy, bytecodeInfo, diagnostic);
            });

        // Gather the diagnostics
        IncrementalValuesProvider<DiagnosticInfo> embeddedBytecodeDiagnostics =
            embeddedBytecodeWithErrors
            .Select(static (item, _) => (item.Hierarchy.FullyQualifiedMetadataName, item.Diagnostic))
            .Where(static item => item.Diagnostic is not null)
            .Combine(context.CompilationProvider)
            .Select(static (item, _) =>
            {
                INamedTypeSymbol typeSymbol = item.Right.GetTypeByMetadataName(item.Left.FullyQualifiedMetadataName)!;

                return DiagnosticInfo.Create(item.Left.Diagnostic!.Descriptor, typeSymbol, new object[] { typeSymbol }.Concat(item.Left.Diagnostic.Arguments).ToArray());
            });

        // Output the diagnostics
        context.ReportDiagnostics(embeddedBytecodeDiagnostics);

        // Get the LoadBytecode() info (hierarchy and compiled bytecode)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo)> embeddedBytecode =
            embeddedBytecodeWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.BytecodeInfo));

        // Generate the LoadBytecode() methods
        context.RegisterSourceOutput(embeddedBytecode, static (context, item) =>
        {
            PropertyDeclarationSyntax hlslBytecodeProperty = LoadBytecode.GetHlslBytecodeSyntax(item.BytecodeInfo, out Func<SyntaxNode, SourceText> fixup, out TypeDeclarationSyntax[] additionalTypes);
            CompilationUnitSyntax hlslBytecodeCompilationUnit = GetCompilationUnitFromMember(item.Hierarchy, hlslBytecodeProperty, skipLocalsInit: false, additionalMemberDeclarations: additionalTypes);
            SourceText text = fixup(hlslBytecodeCompilationUnit);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.HlslBytecode.g.cs", text);

            PropertyDeclarationSyntax shaderProfileProperty = LoadBytecode.GetShaderProfileSyntax(item.BytecodeInfo);
            CompilationUnitSyntax shaderProfileCompilationUnit = GetCompilationUnitFromMember(item.Hierarchy, shaderProfileProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.ShaderProfile.g.cs", shaderProfileCompilationUnit.GetText(Encoding.UTF8));

            PropertyDeclarationSyntax compileOptionsProperty = LoadBytecode.GetCompileOptionsSyntax(item.BytecodeInfo);
            CompilationUnitSyntax compileOptionsCompilationUnit = GetCompilationUnitFromMember(item.Hierarchy, compileOptionsProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.CompileOptions.g.cs", compileOptionsCompilationUnit.GetText(Encoding.UTF8));
        });

        // Get the output buffer info (hierarchy and output buffers)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, OutputBufferInfo OutputBuffer)> outputBufferInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.OutputBuffer));

        // Generate the output buffer properties
        context.RegisterSourceOutput(outputBufferInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax bufferPrecisionProperty = OutputBuffer.GetBufferPrecisionSyntax(item.OutputBuffer);
            CompilationUnitSyntax bufferPrecisionCompilationUnit = GetCompilationUnitFromMember(item.Hierarchy, bufferPrecisionProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.BufferPrecision.g.cs", bufferPrecisionCompilationUnit.GetText(Encoding.UTF8));

            PropertyDeclarationSyntax channelDepthProperty = OutputBuffer.GetChannelDepthSyntax(item.OutputBuffer);
            CompilationUnitSyntax channelDepthCompilationUnit = GetCompilationUnitFromMember(item.Hierarchy, channelDepthProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.ChannelDepth.g.cs", channelDepthCompilationUnit.GetText(Encoding.UTF8));
        });

        // Get the InputDescriptions info (hierarchy and input descriptions)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, InputDescriptionsInfo InputDescriptions)> inputDescriptionsInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.InputDescriptions));

        // Generate the InputDescriptions properties
        context.RegisterSourceOutput(inputDescriptionsInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax inputDescriptionsProperty = InputDescriptions.GetSyntax(item.InputDescriptions, out TypeDeclarationSyntax[] additionalTypes);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, inputDescriptionsProperty, skipLocalsInit: false, additionalMemberDeclarations: additionalTypes);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(InputDescriptions)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the PixelOptions info (hierarchy and pixel options)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, D2D1PixelOptions PixelOptions)> pixelOptionsInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.PixelOptions));

        // Generate the PixelOptions properties
        context.RegisterSourceOutput(pixelOptionsInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax pixelOptionsProperty = PixelOptions.GetSyntax(item.PixelOptions);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMember(item.Hierarchy, pixelOptionsProperty, skipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(PixelOptions)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });
    }
}