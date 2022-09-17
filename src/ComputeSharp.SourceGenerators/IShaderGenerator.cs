using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGeneration.Extensions;
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
        // Check whether [SkipLocalsInit] can be used
        IncrementalValueProvider<bool> canUseSkipLocalsInit =
            context.CompilationProvider
            .Select(static (compilation, _) =>
                compilation.Options is CSharpCompilationOptions { AllowUnsafe: true } &&
                compilation.HasAccessibleTypeWithMetadataName("System.Runtime.CompilerServices.SkipLocalsInitAttribute"));

        // Check whether or not dynamic shaders are supported
        IncrementalValueProvider<bool> supportsDynamicShaders =
            context.CompilationProvider
            .Select(static (compilation, _) => IsDynamicCompilationSupported(compilation));

        // Discover all shader types and extract all the necessary info from each of them
        IncrementalValuesProvider<ShaderInfo> shaderInfoWithErrors =
            context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => node.IsTypeDeclarationWithOrPotentiallyWithBaseTypes<StructDeclarationSyntax>(),
                static (context, token) =>
                {
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

                    ImmutableArray<DiagnosticInfo>.Builder diagnostics = ImmutableArray.CreateBuilder<DiagnosticInfo>();

                    // GetDispatchId() info
                    ImmutableArray<string> dispatchIdInfo = GetDispatchId.GetInfo(typeSymbol);

                    token.ThrowIfCancellationRequested();

                    // LoadDispatchData() info
                    ImmutableArray<FieldInfo> fieldInfos = LoadDispatchData.GetInfo(
                        typeSymbol,
                        shaderType,
                        out int resourceCount,
                        out int root32BitConstantCount,
                        out ImmutableArray<DiagnosticInfo> dispatchDataDiagnostics);

                    token.ThrowIfCancellationRequested();

                    // BuildHlslSource() info
                    HlslShaderSourceInfo hlslSourceInfo = BuildHlslSource.GetInfo(
                        context.SemanticModel.Compilation,
                        typeDeclaration,
                        typeSymbol,
                        out ImmutableArray<DiagnosticInfo> hlslSourceDiagnostics);

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
                        typeSymbol,
                        !hlslSourceInfo.Delegates.IsEmpty,
                        IsDynamicCompilationSupported(context.SemanticModel.Compilation),
                        out ImmutableArray<DiagnosticInfo> threadIdsDiagnostics);

                    token.ThrowIfCancellationRequested();

                    // Ensure the bytecode generation is disabled if any errors are present
                    if (!dispatchDataDiagnostics.IsDefaultOrEmpty ||
                        !hlslSourceDiagnostics.IsDefaultOrEmpty)
                    {
                        threadIds = new ThreadIdsInfo(true, 0, 0, 0);
                    }

                    return new ShaderInfo(
                        Hierarchy: HierarchyInfo.From(typeSymbol),
                        DispatchId: new DispatchIdInfo(dispatchIdInfo),
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
            .Select(static (item, _) => item.Diagnostcs)
            .WithComparer(EqualityComparer<DiagnosticInfo>.Default.ForImmutableArray()));

        // Get the GetDispatchId() info (hierarchy and dispatch id info)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchIdInfo DispatchId)> dispatchIdInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.DispatchId));

        // Generate the GetDispatchId() methods
        context.RegisterSourceOutput(dispatchIdInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getDispatchIdMethod = GetDispatchId.GetSyntax(item.DispatchId.Delegates);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getDispatchIdMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(GetDispatchId)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the LoadDispatchData() info (hierarchy and dispatch data info)
        IncrementalValuesProvider<((HierarchyInfo Hierarchy, DispatchDataInfo DispatchData) Info, bool CanUseSkipLocalsInit) > dispatchDataInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.DispatchData))
            .Combine(canUseSkipLocalsInit);

        // Generate the LoadDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo, static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = LoadDispatchData.GetSyntax(
                item.Info.DispatchData.Type,
                item.Info.DispatchData.FieldInfos,
                item.Info.DispatchData.ResourceCount,
                item.Info.DispatchData.Root32BitConstantCount);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Info.Hierarchy, loadDispatchDataMethod, item.CanUseSkipLocalsInit);

            context.AddSource($"{item.Info.Hierarchy.FilenameHint}.{nameof(LoadDispatchData)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the BuildHlslSource info (hierarchy, HLSL source and parsing options)
        IncrementalValuesProvider<((HierarchyInfo Hierarchy, HlslShaderSourceInfo HlslShaderSource) Info, (bool CanUseSkipLocalsInit, bool SupportsDynamicShaders) Options)> hlslSourceInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.HlslShaderSource))
            .Combine(canUseSkipLocalsInit.Combine(supportsDynamicShaders));

        // Generate the BuildHlslSource() methods
        context.RegisterSourceOutput(hlslSourceInfo, static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = BuildHlslSource.GetSyntax(item.Info.HlslShaderSource, item.Options.SupportsDynamicShaders, item.Info.Hierarchy.Hierarchy.Length);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Info.Hierarchy, buildHlslStringMethod, item.Options.CanUseSkipLocalsInit);

            context.AddSource($"{item.Info.Hierarchy.FilenameHint}.{nameof(BuildHlslSource)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the LoadDispatchMetadata() info (hierarchy and dispatch metadata info)
        IncrementalValuesProvider<((HierarchyInfo Hierarchy, DispatchMetadataInfo DispatchMetadata) Info, bool CanUseSkipLocalsInit)> dispatchMetadataInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.DispatchMetadata))
            .Combine(canUseSkipLocalsInit);

        // Generate the LoadDispatchMetadata() methods
        context.RegisterSourceOutput(dispatchMetadataInfo, static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = LoadDispatchMetadata.GetSyntax(item.Info.DispatchMetadata);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Info.Hierarchy, buildHlslStringMethod, item.CanUseSkipLocalsInit);

            context.AddSource($"{item.Info.Hierarchy.FilenameHint}.{nameof(LoadDispatchMetadata)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
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
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Info.Hierarchy, tryGetBytecodeMethod, canUseSkipLocalsInit: false);
            SourceText text = fixup(compilationUnit);

            context.AddSource($"{item.Info.Hierarchy.FilenameHint}.{nameof(LoadBytecode)}.g.cs", text);
        });
    }
}
