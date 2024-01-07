using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using ComputeSharp.SourceGeneration.Constants;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A source generator creating descriptors for <see cref="IComputeShader"/> and <see cref="IComputeShader{TPixel}"/> types.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class ComputeShaderDescriptorGenerator : IIncrementalGenerator
{
    /// <summary>
    /// The name of generator to include in the generated code.
    /// </summary>
    private const string GeneratorName = "ComputeSharp.ComputeShaderDescriptorGenerator";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Discover all shader types and extract all the necessary info from each of them
        IncrementalValuesProvider<ShaderInfo> shaderInfo =
            context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "ComputeSharp.GeneratedComputeShaderDescriptorAttribute",
                static (node, _) => node.IsTypeDeclarationWithOrPotentiallyWithBaseTypes<StructDeclarationSyntax>(),
                static (context, token) =>
                {
                    // The source generator requires unsafe blocks to be enabled (eg. for pointers, [SkipLocalsInit], etc.)
                    if (!context.SemanticModel.Compilation.IsAllowUnsafeBlocksEnabled())
                    {
                        return default;
                    }

                    // If the type symbol doesn't have at least one interface, it can't possibly be a shader type.
                    // Additionally, shader types cannot be generic (including nested inside generic types).
                    if (context.TargetSymbol is not INamedTypeSymbol { AllInterfaces.Length: > 0, IsGenericType: false } typeSymbol)
                    {
                        return default;
                    }

                    // Immediately bail if the target type doesn't have internal accessibility
                    if (!typeSymbol.IsAccessibleFromContainingAssembly(context.SemanticModel.Compilation))
                    {
                        return default;
                    }

                    // Check whether type is a compute shader, and if so, if it's pixel shader like
                    if (!TryGetIsPixelShaderLike(typeSymbol, context.SemanticModel.Compilation, out bool isPixelShaderLike))
                    {
                        return default;
                    }

                    // Get the fields info
                    ConstantBuffer.GetInfo(
                        context.SemanticModel.Compilation,
                        typeSymbol,
                        isPixelShaderLike,
                        out int constantBufferSizeInBytes,
                        out ImmutableArray<FieldInfo> fieldInfos);

                    token.ThrowIfCancellationRequested();

                    // Thread group size info
                    NumThreads.GetInfo(
                        typeSymbol,
                        out int threadsX,
                        out int threadsY,
                        out int threadsZ,
                        out bool isCompilationEnabled);

                    token.ThrowIfCancellationRequested();

                    // Get the compile options as well
                    CompileOptions compileOptions = HlslBytecode.GetCompileOptions(typeSymbol);

                    token.ThrowIfCancellationRequested();

                    using ImmutableArrayBuilder<DiagnosticInfo> diagnostics = new();

                    // Transpiled HLSL source info
                    HlslSource.GetInfo(
                        diagnostics,
                        context.SemanticModel.Compilation,
                        typeSymbol,
                        threadsX,
                        threadsY,
                        threadsZ,
                        token,
                        out bool isImplicitTextureUsed,
                        out bool isSamplerUsed,
                        out string hlslSource);

                    token.ThrowIfCancellationRequested();

                    // Get the resources info (we must do this after crawling the HLSL source)
                    Resources.GetInfo(
                        context.SemanticModel.Compilation,
                        typeSymbol,
                        isImplicitTextureUsed,
                        out ImmutableArray<ResourceInfo> resourceInfo,
                        out ImmutableArray<ResourceDescriptor> resourceDescriptors);

                    token.ThrowIfCancellationRequested();

                    // Prepare the lookup key for the HLSL shader bytecode
                    HlslBytecodeInfoKey hlslInfoKey = new(hlslSource, compileOptions, isCompilationEnabled);

                    // Try to get the HLSL bytecode
                    HlslBytecodeInfo hlslInfo = HlslBytecodeSyntaxProcessor.GetInfo(ref hlslInfoKey, token);

                    token.ThrowIfCancellationRequested();

                    HlslBytecodeSyntaxProcessor.GetInfoDiagnostics(typeSymbol, hlslInfo, diagnostics);
                    HlslBytecodeSyntaxProcessor.GetDoublePrecisionSupportDiagnostics(typeSymbol, hlslInfo, diagnostics);

                    token.ThrowIfCancellationRequested();

                    // Also get the hierarchy too
                    HierarchyInfo hierarchyInfo = HierarchyInfo.From(typeSymbol);

                    token.ThrowIfCancellationRequested();

                    return new ShaderInfo(
                        Hierarchy: hierarchyInfo,
                        ThreadsX: threadsX,
                        ThreadsY: threadsY,
                        ThreadsZ: threadsZ,
                        IsPixelShaderLike: isPixelShaderLike,
                        IsSamplerUsed: isSamplerUsed,
                        ConstantBufferSizeInBytes: constantBufferSizeInBytes,
                        Fields: fieldInfos,
                        Resources: resourceInfo,
                        ResourceDescriptors: resourceDescriptors,
                        HlslInfoKey: hlslInfoKey,
                        HlslInfo: hlslInfo,
                        Diagnostcs: diagnostics.ToImmutable());
                })
            .WithTrackingName(WellKnownTrackingNames.Execute)
            .Where(static item => item is not null)!;

        // Split the diagnostics, and drop them from the output provider (see more notes in the D2D1 generator)
        IncrementalValuesProvider<EquatableArray<DiagnosticInfo>> diagnosticInfo =
            shaderInfo
            .Select(static (item, _) => item.Diagnostcs)
            .WithTrackingName(WellKnownTrackingNames.Diagnostics)
            .Where(static item => !item.IsEmpty);

        // Gather the models to produce sources for
        IncrementalValuesProvider<ShaderInfo> outputInfo =
            shaderInfo
            .Select(static (item, _) => item with { Diagnostcs = default })
            .WithTrackingName(WellKnownTrackingNames.Output);

        // Output the diagnostics, if any
        context.ReportDiagnostics(diagnosticInfo);

        // Generate the source files, if any
        context.RegisterSourceOutput(outputInfo, static (context, item) =>
        {
            using ImmutableArrayBuilder<IndentedTextWriter.Callback<ShaderInfo>> declaredMembers = new();

            declaredMembers.Add(NumThreads.WriteThreadsXSyntax);
            declaredMembers.Add(NumThreads.WriteThreadsYSyntax);
            declaredMembers.Add(NumThreads.WriteThreadsZSyntax);
            declaredMembers.Add(MetadataProperties.WriteConstantBufferSizeSyntax);
            declaredMembers.Add(MetadataProperties.WriteIsStaticSamplerRequiredSyntax);
            declaredMembers.Add(ResourceDescriptorRanges.WriteSyntax);
            declaredMembers.Add(HlslSource.WriteSyntax);
            declaredMembers.Add(HlslBytecode.WriteSyntax);
            declaredMembers.Add(ConstantBuffer.WriteSyntax);
            declaredMembers.Add(Resources.WriteSyntax);

            using ImmutableArrayBuilder<IndentedTextWriter.Callback<ShaderInfo>> additionalTypes = new();
            using ImmutableHashSetBuilder<string> usingDirectives = new();

            ConstantBufferSyntaxProcessor.RegisterAdditionalTypesSyntax(GeneratorName, BindingDirection.OneWay, item, additionalTypes, usingDirectives);
            Resources.RegisterAdditionalTypesSyntax(item, additionalTypes, usingDirectives);
            ResourceDescriptorRanges.RegisterAdditionalDataMemberSyntax(item, additionalTypes, usingDirectives);
            HlslBytecode.RegisterAdditionalTypesSyntax(item, additionalTypes, usingDirectives);

            using IndentedTextWriter writer = new();

            item.Hierarchy.WriteSyntax(
                state: item,
                writer: writer,
                baseTypes: [$"global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{item.Hierarchy.Hierarchy[0].QualifiedName}>"],
                memberCallbacks: declaredMembers.WrittenSpan);

            // Append any additional types as well
            if (additionalTypes.Count > 0)
            {
                writer.WriteLine();
                writer.WriteLine("namespace ComputeSharp.Generated");

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