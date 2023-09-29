using System;
using System.Collections.Immutable;
using System.Linq;
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
        IncrementalValuesProvider<D2D1ShaderInfo> shaderInfo =
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

                    token.ThrowIfCancellationRequested();

                    // Get the resource texture info for ResourceTextureDescriptions
                    ResourceTextureDescriptions.GetInfo(
                        diagnostics,
                        typeSymbol,
                        inputCount,
                        out ImmutableArray<ResourceTextureDescription> resourceTextureDescriptions);

                    token.ThrowIfCancellationRequested();

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

                    // Get the shader profile and linking info for LoadBytecode()
                    bool isLinkingSupported = LoadBytecode.IsSimpleInputShader(typeSymbol, inputCount);
                    D2D1ShaderProfile? requestedShaderProfile = LoadBytecode.GetRequestedShaderProfile(typeSymbol);
                    D2D1CompileOptions? requestedCompileOptions = LoadBytecode.GetRequestedCompileOptions(diagnostics, typeSymbol);
                    D2D1ShaderProfile effectiveShaderProfile = LoadBytecode.GetEffectiveShaderProfile(requestedShaderProfile);
                    D2D1CompileOptions effectiveCompileOptions = LoadBytecode.GetEffectiveCompileOptions(requestedCompileOptions, isLinkingSupported);
                    bool hasErrors = diagnostics.Count > 0;

                    token.ThrowIfCancellationRequested();

                    // As the last steps in the pipeline, try to compile the shader if needed.
                    // This is done last so that it can be skipped if any errors happened before.
                    HlslBytecodeInfoKey hlslInfoKey = new(
                        hlslSource,
                        requestedShaderProfile,
                        requestedCompileOptions,
                        effectiveShaderProfile,
                        effectiveCompileOptions,
                        hasErrors);

                    // TODO: cache this across transform runs
                    HlslBytecodeInfo hlslInfo = LoadBytecode.GetInfo(hlslInfoKey, token);

                    token.ThrowIfCancellationRequested();

                    // Append any diagnostic for the shader compilation
                    LoadBytecode.GetInfoDiagnostics(typeSymbol, hlslInfo, diagnostics);

                    token.ThrowIfCancellationRequested();

                    return new D2D1ShaderInfo(
                        Hierarchy: HierarchyInfo.From(typeSymbol),
                        EffectId: effectId,
                        EffectDisplayName: effectDisplayName,
                        EffectDescription: effectDescription,
                        EffectCategory: effectCategory,
                        EffectAuthor: effectAuthor,
                        ConstantBufferSizeInBytes: constantBufferSizeInBytes,
                        InputTypes: inputTypes,
                        InputDescriptions: inputDescriptions,
                        ResourceTextureDescriptions: resourceTextureDescriptions,
                        Fields: fieldInfos,
                        BufferPrecision: bufferPrecision,
                        ChannelDepth: channelDepth,
                        PixelOptions: pixelOptions,
                        HlslInfoKey: hlslInfoKey,
                        HlslInfo: hlslInfo,
                        Diagnostcs: diagnostics.ToImmutable());
                })
            .Where(static item => item is not null)!;

        // We need to create two more incremental steps to ensure we correctly emit diagnostics and re-generate sources:
        //   - One with just the diagnostics, which will trigger every time any of them changes
        //   - One with just the shader info (and no diagnostics), so that changes there don't trigger generation unnecessarily
        IncrementalValuesProvider<EquatableArray<DiagnosticInfo>> diagnosticInfo = shaderInfo.Select(static (item, _) => item.Diagnostcs);
        IncrementalValuesProvider<D2D1ShaderInfo> outputInfo = shaderInfo.Select(static (item, _) => item with { Diagnostcs = default });

        // Output the diagnostics, if any
        context.ReportDiagnostics(diagnosticInfo);

        // Generate the source files, if any
        context.RegisterSourceOutput(outputInfo, static (context, item) =>
        {
            PropertyDeclarationSyntax effectIdProperty = EffectId.GetSyntax(item.EffectId);
            PropertyDeclarationSyntax effectDisplayNameProperty = EffectMetadata.GetEffectDisplayNameSyntax(item.EffectDisplayName);
            PropertyDeclarationSyntax effectDescriptionProperty = EffectMetadata.GetEffectDescriptionSyntax(item.EffectDescription);
            PropertyDeclarationSyntax effectCategoryProperty = EffectMetadata.GetEffectCategorySyntax(item.EffectCategory);
            PropertyDeclarationSyntax effectAuthorProperty = EffectMetadata.GetEffectAuthorSyntax(item.EffectAuthor);
            PropertyDeclarationSyntax inputCountProperty = InputCount.GetSyntax(item.InputTypes.Length);
            PropertyDeclarationSyntax inputTypesProperty = InputTypes.GetSyntax(item.InputTypes, out TypeDeclarationSyntax[] inputTypesAdditionalTypes);
            PropertyDeclarationSyntax inputDescriptionsProperty = InputDescriptions.GetSyntax(item.InputDescriptions, out MemberDeclarationSyntax[] inputDescriptionsAdditionalDataMembers);
            PropertyDeclarationSyntax resourceTextureDescriptionsProperty = ResourceTextureDescriptions.GetSyntax(item.ResourceTextureDescriptions, out MemberDeclarationSyntax[] resourceTexturesAditionalDataMembers);
            PropertyDeclarationSyntax hlslStringProperty = HlslSource.GetSyntax(item.HlslInfoKey.HlslSource, item.Hierarchy.Hierarchy.Length);
            PropertyDeclarationSyntax hlslBytecodeProperty = LoadBytecode.GetHlslBytecodeSyntax(item.HlslInfo, out Func<SyntaxNode, SourceText> fixup, out TypeDeclarationSyntax[] hlslBytecodeAdditionalTypes);
            PropertyDeclarationSyntax shaderProfileProperty = LoadBytecode.GetShaderProfileSyntax(item.HlslInfoKey.EffectiveShaderProfile);
            PropertyDeclarationSyntax compileOptionsProperty = LoadBytecode.GetCompileOptionsSyntax(item.HlslInfoKey.EffectiveCompileOptions);
            PropertyDeclarationSyntax bufferPrecisionProperty = OutputBuffer.GetBufferPrecisionSyntax(item.BufferPrecision);
            PropertyDeclarationSyntax channelDepthProperty = OutputBuffer.GetChannelDepthSyntax(item.ChannelDepth);
            PropertyDeclarationSyntax pixelOptionsProperty = PixelOptions.GetSyntax(item.PixelOptions);

            MethodDeclarationSyntax initializeFromDispatchDataMethod = InitializeFromDispatchData.GetSyntax(item.Fields);
            MethodDeclarationSyntax loadDispatchDataMethod = LoadDispatchData.GetSyntax(item.Hierarchy, item.Fields, item.ConstantBufferSizeInBytes, out TypeDeclarationSyntax[] loadDispatchDataAditionalTypes);

            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMembers(
                hierarchyInfo: item.Hierarchy,
                memberDeclarations: new (MemberDeclarationSyntax, bool)[]
                {
                    (effectIdProperty, false), (effectDisplayNameProperty, false), (effectDescriptionProperty, false), (effectCategoryProperty, false), (effectAuthorProperty, false),
                    (inputCountProperty, false), (inputTypesProperty, false), (inputDescriptionsProperty, false), (resourceTextureDescriptionsProperty, false), (hlslStringProperty, false),
                    (hlslBytecodeProperty, false), (shaderProfileProperty, false), (compileOptionsProperty, false), (bufferPrecisionProperty, false), (channelDepthProperty, false),
                    (pixelOptionsProperty, false), (initializeFromDispatchDataMethod, true), (loadDispatchDataMethod, true)
                },
                additionalMemberDeclarations: new TypeDeclarationSyntax[][]
                {
                    loadDispatchDataAditionalTypes,
                    InputDescriptions.GetDataTypeDeclarations(new[] { inputDescriptionsAdditionalDataMembers, resourceTexturesAditionalDataMembers }.SelectMany(static members => members).ToArray()),
                    inputTypesAdditionalTypes, hlslBytecodeAdditionalTypes
                }
                .SelectMany(static types => types)
                .Cast<MemberDeclarationSyntax>()
                .ToArray());

            SourceText sourceText = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.g.cs", sourceText);
        });
    }
}