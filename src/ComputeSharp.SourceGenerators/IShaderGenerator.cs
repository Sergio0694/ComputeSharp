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
                        out int resourceCount,
                        out int root32BitConstantCount);

                    token.ThrowIfCancellationRequested();

                    // TryGetBytecode() info
                    ThreadIdsInfo threadIds = LoadBytecode.GetInfo(
                        diagnostics,
                        typeSymbol,
                        false);

                    token.ThrowIfCancellationRequested();

                    // BuildHlslSource() info
                    HlslShaderSourceInfo hlslSourceInfo = BuildHlslSource.GetInfo(
                        diagnostics,
                        context.SemanticModel.Compilation,
                        typeDeclaration,
                        typeSymbol,
                        threadIds,
                        out bool isImplicitTextureUsed);

                    token.ThrowIfCancellationRequested();

                    // GetDispatchMetadata() info
                    DispatchMetadataInfo dispatchMetadataInfo = LoadDispatchMetadata.GetInfo(
                        root32BitConstantCount,
                        isImplicitTextureUsed,
                        hlslSourceInfo.IsSamplerUsed,
                        fieldInfos);

                    token.ThrowIfCancellationRequested();

                    ImmutableArray<byte> bytecode = LoadBytecode.GetBytecode(threadIds, hlslSourceInfo.HlslSource, token, out DeferredDiagnosticInfo? diagnostic);

                    token.ThrowIfCancellationRequested();

                    EmbeddedBytecodeInfo bytecodeInfo = new(
                        threadIds.X,
                        threadIds.Y,
                        threadIds.Z,
                        bytecode);

                    return new ShaderInfo(
                        Hierarchy: HierarchyInfo.From(typeSymbol),
                        DispatchData: new DispatchDataInfo(
                            isPixelShaderLike,
                            fieldInfos,
                            resourceCount,
                            root32BitConstantCount),
                        DispatchMetadata: dispatchMetadataInfo,
                        HlslShaderSource: hlslSourceInfo,
                        EmbeddedBytecode: bytecodeInfo,
                        ThreadIds: threadIds,
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

            declaredMembers.Add(LoadDispatchData.WriteSyntax);
            declaredMembers.Add(LoadDispatchMetadata.WriteSyntax);
            declaredMembers.Add(BuildHlslSource.WriteSyntax);
            declaredMembers.Add(LoadBytecode.WriteSyntax);

            using IndentedTextWriter writer = new();

            item.Hierarchy.WriteSyntax(
                state: item,
                writer: writer,
                baseTypes: ReadOnlySpan<string>.Empty,
                memberCallbacks: declaredMembers.WrittenSpan);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.g.cs", writer.ToString());
        });
    }
}