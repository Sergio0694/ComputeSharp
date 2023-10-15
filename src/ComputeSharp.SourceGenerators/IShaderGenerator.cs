using System;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A source generator creating data loaders for <see cref="IComputeShader"/> and <see cref="IPixelShader{TPixel}"/> types.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class IShaderGenerator : IIncrementalGenerator
{
    /// <summary>
    /// The name of generator to include in the generated code.
    /// </summary>
    private const string GeneratorName = "ComputeSharp.IShaderGenerator";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Discover all shader types and extract all the necessary info from each of them
        IncrementalValuesProvider<ShaderInfo> shaderInfo =
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

                    // Check whether type is a compute shader, and if so, if it's pixel shader like
                    if (!TryGetIsPixelShaderLike(typeSymbol, context.SemanticModel.Compilation, out bool isPixelShaderLike))
                    {
                        return default;
                    }

                    using ImmutableArrayBuilder<DiagnosticInfo> diagnostics = new();

                    // LoadDispatchData() info
                    ImmutableArray<FieldInfo> fieldInfos = LoadDispatchData.GetInfo(
                        diagnostics,
                        typeSymbol,
                        isPixelShaderLike,
                        out int constantBufferSizeInBytes,
                        out int resourceCount);

                    token.ThrowIfCancellationRequested();

                    // TryGetBytecode() info
                    LoadBytecode.GetInfo(
                        diagnostics,
                        typeSymbol,
                        out int threadsX,
                        out int threadsY,
                        out int threadsZ,
                        out bool isCompilationEnabled);

                    token.ThrowIfCancellationRequested();

                    // BuildHlslSource() info
                    BuildHlslSource.GetInfo(
                        diagnostics,
                        context.SemanticModel.Compilation,
                        typeDeclaration,
                        typeSymbol,
                        threadsX,
                        threadsY,
                        threadsZ,
                        out bool isImplicitTextureUsed,
                        out bool isSamplerUsed,
                        out string hlslSource);

                    token.ThrowIfCancellationRequested();

                    // GetDispatchMetadata() info
                    ImmutableArray<ResourceDescriptor> resourceDescriptors = LoadDispatchMetadata.GetInfo(
                        isImplicitTextureUsed,
                        isSamplerUsed,
                        fieldInfos);

                    token.ThrowIfCancellationRequested();

                    // Prepare the lookup key for the HLSL shader bytecode
                    HlslBytecodeInfoKey hlslInfoKey = new(hlslSource, isCompilationEnabled);

                    // Try to get the HLSL bytecode
                    HlslBytecodeInfo hlslInfo = LoadBytecode.GetBytecode(ref hlslInfoKey, token);

                    token.ThrowIfCancellationRequested();

                    LoadBytecode.GetInfoDiagnostics(typeSymbol, hlslInfo, diagnostics);

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
                        ResourceCount: resourceCount,
                        Fields: fieldInfos,
                        ResourceDescriptors: resourceDescriptors,
                        HlslInfoKey: hlslInfoKey,
                        HlslInfo: hlslInfo,
                        Diagnostcs: diagnostics.ToImmutable());
                })
            .Where(static item => item is not null)!;

        // Split the diagnostics, and drop them from the output provider (see more notes in the D2D1 generator)
        IncrementalValuesProvider<EquatableArray<DiagnosticInfo>> diagnosticInfo = shaderInfo.Select(static (item, _) => item.Diagnostcs);
        IncrementalValuesProvider<ShaderInfo> outputInfo = shaderInfo.Select(static (item, _) => item with { Diagnostcs = default });

        // Output the diagnostics, if any
        context.ReportDiagnostics(diagnosticInfo);

        // Generate the source files, if any
        context.RegisterSourceOutput(outputInfo, static (context, item) =>
        {
            using ImmutableArrayBuilder<IndentedTextWriter.Callback<ShaderInfo>> declaredMembers = new();

            declaredMembers.Add(LoadBytecode.WriteThreadsXSyntax);
            declaredMembers.Add(LoadBytecode.WriteThreadsYSyntax);
            declaredMembers.Add(LoadBytecode.WriteThreadsZSyntax);
            declaredMembers.Add(LoadDispatchMetadata.WriteConstantBufferSizeSyntax);
            declaredMembers.Add(LoadDispatchMetadata.WriteIsStaticSamplerRequiredSyntax);
            declaredMembers.Add(LoadDispatchMetadata.WriteSyntax);
            declaredMembers.Add(BuildHlslSource.WriteSyntax);
            declaredMembers.Add(LoadBytecode.WriteHlslBytecodeSyntax);
            declaredMembers.Add(LoadDispatchData.WriteLoadConstantBufferSyntax);
            declaredMembers.Add(LoadDispatchData.WriteLoadGraphicsResourcesSyntax);

            using ImmutableArrayBuilder<IndentedTextWriter.Callback<ShaderInfo>> additionalTypes = new();
            using ImmutableHashSetBuilder<string> usingDirectives = new();

            LoadDispatchMetadata.RegisterAdditionalDataMemberSyntax(item, additionalTypes, usingDirectives);
            LoadBytecode.RegisterAdditionalTypeSyntax(item, additionalTypes, usingDirectives);

            using IndentedTextWriter writer = new();

            item.Hierarchy.WriteSyntax(
                state: item,
                writer: writer,
                baseTypes: ReadOnlySpan<string>.Empty,
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