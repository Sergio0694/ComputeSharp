using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A source generator creating pixel shader descriptors for annotated D2D1 pixel shader types.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class D2DPixelShaderDescriptorGenerator : IIncrementalGenerator
{
    /// <summary>
    /// The name of generator to include in the generated code.
    /// </summary>
    private const string GeneratorName = "ComputeSharp.D2D1.D2DPixelShaderDescriptorGenerator";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Discover all shader types and extract all the necessary info from each of them
        IncrementalValuesProvider<D2D1ShaderInfo> shaderInfo =
            context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "ComputeSharp.D2D1.D2DGeneratedPixelShaderDescriptorAttribute",
                static (node, _) => node.IsTypeDeclarationWithOrPotentiallyWithBaseTypes<StructDeclarationSyntax>(),
                static (context, token) =>
                {
                    // The source generator requires unsafe blocks to be enabled (eg. for pointers, [SkipLocalsInit], etc.)
                    if (!context.SemanticModel.Compilation.IsAllowUnsafeBlocksEnabled())
                    {
                        return default;
                    }

                    // If the type symbol doesn't have at least one interface, it can't possibly be a shader type
                    if (context.TargetSymbol is not INamedTypeSymbol { AllInterfaces.Length: > 0 } typeSymbol)
                    {
                        return default;
                    }

                    // Check that the shader implements the ID2D1PixelShader interface
                    if (!typeSymbol.HasInterfaceWithType(context.SemanticModel.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.ID2D1PixelShader")!))
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

                    using ImmutableArrayBuilder<DiagnosticInfo> diagnostics = new();

                    // LoadDispatchData() info
                    ImmutableArray<FieldInfo> fieldInfos = LoadConstantBuffer.GetInfo(
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
                        (StructDeclarationSyntax)context.TargetNode,
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
                    bool isLinkingSupported = HlslBytecode.IsSimpleInputShader(typeSymbol, inputCount);
                    D2D1ShaderProfile? requestedShaderProfile = HlslBytecode.GetRequestedShaderProfile(typeSymbol);
                    D2D1CompileOptions? requestedCompileOptions = HlslBytecode.GetRequestedCompileOptions(diagnostics, typeSymbol);
                    D2D1ShaderProfile effectiveShaderProfile = HlslBytecode.GetEffectiveShaderProfile(requestedShaderProfile, out bool isCompilationEnabled);
                    D2D1CompileOptions effectiveCompileOptions = HlslBytecode.GetEffectiveCompileOptions(requestedCompileOptions, isLinkingSupported);

                    token.ThrowIfCancellationRequested();

                    // As the last steps in the pipeline, try to compile the shader if needed.
                    // This is done last so that it can be skipped if any errors happened before.
                    HlslBytecodeInfoKey hlslInfoKey = new(
                        hlslSource,
                        effectiveShaderProfile,
                        effectiveCompileOptions,
                        isCompilationEnabled);

                    // Get the existing compiled shader, or compile the processed HLSL code
                    HlslBytecodeInfo hlslInfo = HlslBytecode.GetInfo(ref hlslInfoKey, token);

                    token.ThrowIfCancellationRequested();

                    // Append any diagnostic for the shader compilation
                    HlslBytecode.GetInfoDiagnostics(typeSymbol, hlslInfo, diagnostics);

                    token.ThrowIfCancellationRequested();

                    // Finally, get the hierarchy too
                    HierarchyInfo hierarchyInfo = HierarchyInfo.From(typeSymbol);

                    token.ThrowIfCancellationRequested();

                    return new D2D1ShaderInfo(
                        Hierarchy: hierarchyInfo,
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
            using ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> declaredMembers = new();

            declaredMembers.Add(EffectId.WriteSyntax);
            declaredMembers.Add(EffectMetadata.WriteEffectDisplayNameSyntax);
            declaredMembers.Add(EffectMetadata.WriteEffectDescriptionSyntax);
            declaredMembers.Add(EffectMetadata.WriteEffectCategorySyntax);
            declaredMembers.Add(EffectMetadata.WriteEffectAuthorSyntax);
            declaredMembers.Add(NumericProperties.WriteConstantBufferSizeSyntax);
            declaredMembers.Add(NumericProperties.WriteInputCountSyntax);
            declaredMembers.Add(NumericProperties.WriteResourceTextureCountSyntax);
            declaredMembers.Add(InputTypes.WriteSyntax);
            declaredMembers.Add(InputDescriptions.WriteSyntax);
            declaredMembers.Add(ResourceTextureDescriptions.WriteSyntax);
            declaredMembers.Add(PixelOptions.WriteSyntax);
            declaredMembers.Add(OutputBuffer.WriteBufferPrecisionSyntax);
            declaredMembers.Add(OutputBuffer.WriteChannelDepthSyntax);
            declaredMembers.Add(HlslBytecode.WriteShaderProfileSyntax);
            declaredMembers.Add(HlslBytecode.WriteCompileOptionsSyntax);
            declaredMembers.Add(HlslSource.WriteSyntax);
            declaredMembers.Add(HlslBytecode.WriteHlslBytecodeSyntax);
            declaredMembers.Add(CreateFromConstantBuffer.WriteSyntax);
            declaredMembers.Add(LoadConstantBuffer.WriteSyntax);

            using ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> additionalTypes = new();
            using ImmutableHashSetBuilder<string> usingDirectives = new();

            LoadConstantBuffer.RegisterAdditionalTypeSyntax(item, additionalTypes, usingDirectives);
            InputDescriptions.RegisterAdditionalTypeSyntax(item, additionalTypes, usingDirectives);
            InputTypes.RegisterAdditionalTypeSyntax(item, additionalTypes, usingDirectives);
            HlslBytecode.RegisterAdditionalTypeSyntax(item, additionalTypes, usingDirectives);

            using IndentedTextWriter writer = new();

            item.Hierarchy.WriteSyntax(
                state: item,
                writer: writer,
                baseTypes: new[] { $"global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{item.Hierarchy.Hierarchy[0].QualifiedName}>" },
                memberCallbacks: declaredMembers.WrittenSpan);

            // If any generated types are needed, they go into a separate namespace
            // This allows the code to use using directives without any conflicts.
            if (additionalTypes.Count > 0)
            {
                writer.WriteLine();
                writer.WriteLine("namespace ComputeSharp.D2D1.Generated");

                using (writer.WriteBlock())
                {
                    writer.WriteSortedUsingDirectives(usingDirectives.AsEnumerable());
                    writer.WriteLineSeparatedMembers(additionalTypes.WrittenSpan, (callback, writer) => callback(item, writer));
                }
            }

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.g.cs", writer.ToString());
        });
    }
}