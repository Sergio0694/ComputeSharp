using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A source generator creating data loaders for <see cref="IComputeShader"/> and <see cref="IPixelShader{TPixel}"/> types.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class IShaderGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Check whether or not dynamic shaders are supported
        IncrementalValueProvider<bool> supportsDynamicShaders =
            context.CompilationProvider
            .Select(static (compilation, _) => false);

        // Discover all shader types and extract all the necessary info from each of them
        IncrementalValuesProvider<ShaderInfo> shaderInfoWithErrors =
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

                    // Get the shader type, or return if none is present
                    if (GetShaderType(typeSymbol, context.SemanticModel.Compilation) is not ShaderType shaderType)
                    {
                        return default;
                    }

                    using ImmutableArrayBuilder<DiagnosticInfo> diagnostics = new();

                    // LoadDispatchData() info
                    ImmutableArray<FieldInfo> fieldInfos = LoadDispatchData.GetInfo(
                        diagnostics,
                        typeSymbol,
                        shaderType,
                        out int resourceCount,
                        out int root32BitConstantCount);

                    token.ThrowIfCancellationRequested();

                    // BuildHlslSource() info
                    HlslShaderSourceInfo hlslSourceInfo = BuildHlslSource.GetInfo(
                        diagnostics,
                        context.SemanticModel.Compilation,
                        typeDeclaration,
                        typeSymbol);

                    token.ThrowIfCancellationRequested();

                    // GetDispatchMetadata() info
                    DispatchMetadataInfo dispatchMetadataInfo = LoadDispatchMetadata.GetInfo(
                        root32BitConstantCount,
                        hlslSourceInfo.ImplicitTextureType is not null,
                        hlslSourceInfo.IsSamplerUsed,
                        fieldInfos);

                    token.ThrowIfCancellationRequested();

                    // TryGetBytecode() info
                    ThreadIdsInfo threadIds = LoadBytecode.GetInfo(
                        diagnostics,
                        typeSymbol,
                        false);

                    token.ThrowIfCancellationRequested();

                    return new ShaderInfo(
                        Hierarchy: HierarchyInfo.From(typeSymbol),
                        DispatchData: new DispatchDataInfo(
                            shaderType,
                            fieldInfos,
                            resourceCount,
                            root32BitConstantCount),
                        DispatchMetadata: dispatchMetadataInfo,
                        HlslShaderSource: hlslSourceInfo,
                        ThreadIds: threadIds,
                        Diagnostcs: diagnostics.ToImmutable());
                })
            .Where(static item => item is not null)!;

        // Output the diagnostics, if any
        context.ReportDiagnostics(
            shaderInfoWithErrors
            .Select(static (item, _) => item.Diagnostcs));

        // Get the LoadDispatchData() info (hierarchy and dispatch data info)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchDataInfo DispatchData)> dispatchDataInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.DispatchData));

        // Generate the LoadDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo, static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = LoadDispatchData.GetSyntax(
                item.DispatchData.Type,
                item.DispatchData.FieldInfos,
                item.DispatchData.ResourceCount,
                item.DispatchData.Root32BitConstantCount);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, loadDispatchDataMethod, addSkipLocalsInitAttribute: true);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(LoadDispatchData)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the BuildHlslSource info (hierarchy, HLSL source and parsing options)
        IncrementalValuesProvider<((HierarchyInfo Hierarchy, HlslShaderSourceInfo HlslShaderSource) Left, bool SupportsDynamicShaders)> hlslSourceInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.HlslShaderSource))
            .Combine(supportsDynamicShaders);

        // Generate the BuildHlslSource() methods
        context.RegisterSourceOutput(hlslSourceInfo, static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = BuildHlslSource.GetSyntax(item.Left.HlslShaderSource, item.SupportsDynamicShaders, item.Left.Hierarchy.Hierarchy.Length);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, buildHlslStringMethod, addSkipLocalsInitAttribute: true);

            context.AddSource($"{item.Left.Hierarchy.FullyQualifiedMetadataName}.{nameof(BuildHlslSource)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the LoadDispatchMetadata() info (hierarchy and dispatch metadata info)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchMetadataInfo DispatchMetadata)> dispatchMetadataInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.DispatchMetadata));

        // Generate the LoadDispatchMetadata() methods
        context.RegisterSourceOutput(dispatchMetadataInfo, static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = LoadDispatchMetadata.GetSyntax(item.DispatchMetadata);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, buildHlslStringMethod, addSkipLocalsInitAttribute: true);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(LoadDispatchMetadata)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Transform the raw HLSL source to compile (this step aggregates the HLSL source to ensure compilation is only done for actual HLSL changes)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string Hlsl, ThreadIdsInfo ThreadIds)> shaderBytecodeInfo =
            shaderInfoWithErrors
            .Select(static (item, token) => (item.Hierarchy, BuildHlslSource.GetNonDynamicHlslSource(item.HlslShaderSource), item.ThreadIds));

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo EmbeddedBytecode, DeferredDiagnosticInfo? Diagnostic)> embeddedBytecodeWithErrors =
            shaderBytecodeInfo
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = LoadBytecode.GetBytecode(item.ThreadIds, item.Hlsl, token, out DeferredDiagnosticInfo? diagnostic);

                token.ThrowIfCancellationRequested();

                EmbeddedBytecodeInfo bytecodeInfo = new(
                    item.ThreadIds.X,
                    item.ThreadIds.Y,
                    item.ThreadIds.Z,
                    bytecode);

                return (item.Hierarchy, bytecodeInfo, diagnostic);
            });

        // Gather the diagnostics
        IncrementalValuesProvider<DiagnosticInfo> embeddedBytecodeDiagnostics =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy.FullyQualifiedMetadataName, item.Diagnostic))
            .Where(static item => item.Diagnostic is not null)
            .Combine(context.CompilationProvider)
            .Select(static (item, token) =>
            {
                INamedTypeSymbol typeSymbol = item.Right.GetTypeByMetadataName(item.Left.FullyQualifiedMetadataName)!;

                return DiagnosticInfo.Create(item.Left.Diagnostic!.Descriptor, typeSymbol, new object[] { typeSymbol }.Concat(item.Left.Diagnostic.Arguments).ToArray());
            });

        // Output the diagnostics
        context.ReportDiagnostics(embeddedBytecodeDiagnostics);

        // Get the TryGetBytecode() info (hierarchy and compiled bytecode)
        IncrementalValuesProvider<((HierarchyInfo Hierarchy, EmbeddedBytecodeInfo EmbeddedBytecode) Info, bool SupportsDynamicShaders)> embeddedBytecodeInfo =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.EmbeddedBytecode))
            .Combine(supportsDynamicShaders);

        // Generate the TryGetBytecode() methods
        context.RegisterSourceOutput(embeddedBytecodeInfo, static (context, item) =>
        {
            MethodDeclarationSyntax tryGetBytecodeMethod = LoadBytecode.GetSyntax(item.Info.EmbeddedBytecode, item.SupportsDynamicShaders, out Func<SyntaxNode, SourceText> fixup);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Info.Hierarchy, tryGetBytecodeMethod, addSkipLocalsInitAttribute: false);
            SourceText text = fixup(compilationUnit);

            context.AddSource($"{item.Info.Hierarchy.FullyQualifiedMetadataName}.{nameof(LoadBytecode)}.g.cs", text);
        });
    }
}