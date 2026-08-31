using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Constants;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
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
        // (with the exception of the compiled HLSL bytecode, which is processed later)
        IncrementalValuesProvider<D2D1ShaderInfo> shaderInfoWithNoHlslBytecode =
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

                    // If the type symbol doesn't have at least one interface, it can't possibly be a shader type.
                    // Also check for generic types, just like with DX12 shaders (including nesting inside generics).
                    if (context.TargetSymbol is not INamedTypeSymbol { AllInterfaces.Length: > 0, IsGenericType: false } typeSymbol)
                    {
                        return default;
                    }

                    // Immediately bail if the target type doesn't have internal accessibility
                    if (!typeSymbol.IsAccessibleFromContainingAssembly(context.SemanticModel.Compilation))
                    {
                        return default;
                    }

                    INamedTypeSymbol shaderInterfaceType = context.SemanticModel.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.ID2D1PixelShader")!;

                    // Check that the shader implements the ID2D1PixelShader interface
                    if (!typeSymbol.HasInterfaceWithType(shaderInterfaceType))
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

                    // Constant buffer info
                    ConstantBuffer.GetInfo(
                        context.SemanticModel.Compilation,
                        typeSymbol,
                        out int constantBufferSizeInBytes,
                        out ImmutableArray<FieldInfo> fieldInfos);

                    token.ThrowIfCancellationRequested();

                    // Get the input info for InputTypes
                    InputTypes.GetInfo(
                        typeSymbol,
                        out int inputCount,
                        out ImmutableArray<int> inputSimpleIndices,
                        out ImmutableArray<int> inputComplexIndices,
                        out ImmutableArray<uint> inputTypes);

                    token.ThrowIfCancellationRequested();

                    // Get the resource texture info for ResourceTextureDescriptions
                    ResourceTextureDescriptions.GetInfo(
                        typeSymbol,
                        out ImmutableArray<ResourceTextureDescription> resourceTextureDescriptions);

                    token.ThrowIfCancellationRequested();

                    // Get the info for the output buffer properties
                    OutputBuffer.GetInfo(typeSymbol, out D2D1BufferPrecision bufferPrecision, out D2D1ChannelDepth channelDepth);

                    token.ThrowIfCancellationRequested();

                    // Get the info for InputDescriptions
                    InputDescriptions.GetInfo(
                        typeSymbol,
                        out ImmutableArray<InputDescription> inputDescriptions);

                    token.ThrowIfCancellationRequested();

                    // Get the info for PixelOptions
                    PixelOptions.GetInfo(typeSymbol, out D2D1PixelOptions pixelOptions);

                    token.ThrowIfCancellationRequested();

                    // Get the shader profile and linking info for the HLSL bytecode properties
                    D2D1ShaderProfile? requestedShaderProfile = HlslBytecode.GetRequestedShaderProfile(typeSymbol);
                    D2D1CompileOptions? requestedCompileOptions = HlslBytecode.GetRequestedCompileOptions(typeSymbol);
                    D2D1ShaderProfile effectiveShaderProfile = HlslBytecode.GetEffectiveShaderProfile(requestedShaderProfile, out bool isCompilationEnabled);
                    D2D1CompileOptions effectiveCompileOptions = HlslBytecode.GetEffectiveCompileOptions(requestedCompileOptions);

                    token.ThrowIfCancellationRequested();

                    using ImmutableArrayBuilder<DiagnosticInfo> diagnostics = new();

                    // Get HLSL source for HlslSource
                    string hlslSource = HlslSource.GetHlslSource(
                        diagnostics,
                        context.SemanticModel.Compilation,
                        typeSymbol,
                        shaderInterfaceType,
                        inputCount,
                        inputSimpleIndices,
                        inputComplexIndices,
                        token);

                    token.ThrowIfCancellationRequested();

                    // Prepare the key to compile the shader afterwards. The compilation is deliberately not
                    // done here: the incremental driver invokes transform callbacks sequentially, so compiling
                    // here would serialize all shader compilations. Instead, compilation is deferred to a
                    // dedicated node below, which can process all shaders in the compilation in parallel.
                    HlslBytecodeInfoKey hlslInfoKey = new(
                        hlslSource,
                        effectiveShaderProfile,
                        effectiveCompileOptions,
                        isCompilationEnabled);

                    // Capture the info needed to synthesize the diagnostics for the deferred compilation,
                    // as they cannot be created later (symbols must not be used past the transform node)
                    HlslBytecodeDiagnosticsInfo hlslDiagnosticsInfo = HlslBytecodeSyntaxProcessor.GetDiagnosticsInfo(typeSymbol);

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
                        HlslInfo: HlslBytecodeInfo.Missing.Instance,
                        HlslDiagnosticsInfo: hlslDiagnosticsInfo,
                        Diagnostcs: diagnostics.ToImmutable());
                })
            .WithTrackingName(WellKnownTrackingNames.Execute)
            .Where(static item => item is not null)!;

        // Compile all shaders in parallel in a single dedicated node, warming up the shared bytecode
        // cache. The node produces no meaningful value: it only exists so that the join node below has
        // an edge ordering it after all compilations are done (its input requires this node's output).
        IncrementalValueProvider<bool> hlslBytecodeCache =
            shaderInfoWithNoHlslBytecode
            .Select(static (item, _) => item.HlslInfoKey)
            .Collect()
            .Select(static (keys, token) =>
            {
                HlslBytecodeSyntaxProcessor.CompileAllInParallel(keys, token);

                return true;
            });

        // Join each shader with its compiled bytecode (guaranteed to be a cache hit, given the ordering
        // edge on the node above), and synthesize the deferred diagnostics for the shader compilation
        IncrementalValuesProvider<D2D1ShaderInfo> shaderInfo =
            shaderInfoWithNoHlslBytecode
            .Combine(hlslBytecodeCache)
            .Select(static (pair, token) =>
            {
                D2D1ShaderInfo item = pair.Left;

                HlslBytecodeInfoKey hlslInfoKey = item.HlslInfoKey;

                // Get the compiled shader from the warmed up cache
                HlslBytecodeInfo hlslInfo = HlslBytecodeSyntaxProcessor.GetInfo(ref hlslInfoKey, token);

                token.ThrowIfCancellationRequested();

                using ImmutableArrayBuilder<DiagnosticInfo> diagnostics = new();

                diagnostics.AddRange(item.Diagnostcs.AsSpan());

                // Append any diagnostic for the shader compilation
                HlslBytecodeSyntaxProcessor.GetInfoDiagnostics(item.HlslDiagnosticsInfo!, hlslInfo, diagnostics);
                HlslBytecodeSyntaxProcessor.GetDoublePrecisionSupportDiagnostics(item.HlslDiagnosticsInfo!, hlslInfo, diagnostics);

                token.ThrowIfCancellationRequested();

                // The diagnostics info is dropped here, as it has served its purpose. This also improves
                // incrementality, as it holds a reference to the syntax tree of the shader type, which
                // would otherwise cause spurious changes in the resulting models on unrelated edits.
                return item with
                {
                    HlslInfoKey = hlslInfoKey,
                    HlslInfo = hlslInfo,
                    HlslDiagnosticsInfo = null,
                    Diagnostcs = diagnostics.ToImmutable()
                };
            });

        // We need to create two more incremental steps to ensure we correctly emit diagnostics and re-generate sources.
        // First, select an incremental provider with just the diagnostics, which will trigger every time any of them changes.
        IncrementalValuesProvider<EquatableArray<DiagnosticInfo>> diagnosticInfo =
            shaderInfo
            .Select(static (item, _) => item.Diagnostcs)
            .WithTrackingName(WellKnownTrackingNames.Diagnostics)
            .Where(static item => !item.IsEmpty);

        // Next, select one with just the shader info (and no diagnostics), so that changes there don't trigger generation unnecessarily
        IncrementalValuesProvider<D2D1ShaderInfo> outputInfo =
            shaderInfo
            .Select(static (item, _) => item with { Diagnostcs = default })
            .WithTrackingName(WellKnownTrackingNames.Output);

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
            declaredMembers.Add(EffectFactory.WriteSyntax);
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
            declaredMembers.Add(ConstantBuffer.WriteCreateFromConstantBufferSyntax);
            declaredMembers.Add(ConstantBuffer.WriteLoadConstantBufferSyntax);

            using ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> additionalTypes = new();
            using ImmutableHashSetBuilder<string> usingDirectives = new();

            ConstantBufferSyntaxProcessor.RegisterAdditionalTypesSyntax(GeneratorName, BindingDirection.TwoWay, item, additionalTypes, usingDirectives);
            InputDescriptions.RegisterAdditionalTypesSyntax(item, additionalTypes, usingDirectives);
            InputTypes.RegisterAdditionalTypesSyntax(item, additionalTypes, usingDirectives);
            HlslBytecode.RegisterAdditionalTypesSyntax(item, additionalTypes, usingDirectives);

            using IndentedTextWriter writer = new();

            item.Hierarchy.WriteSyntax(
                state: item,
                writer: writer,
                baseTypes: [$"global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{item.Hierarchy.Hierarchy[0].QualifiedName}>"],
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