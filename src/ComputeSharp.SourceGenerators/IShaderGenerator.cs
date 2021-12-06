using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A source generator creating data loaders for <see cref="IComputeShader"/> and <see cref="IPixelShader{TPixel}"/> types.
/// </summary>
[Generator]
public sealed partial class IShaderGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Check whether [SkipLocalsInit] can be used
        IncrementalValueProvider<bool> canUseSkipLocalsInit =
            context.CompilationProvider
            .Select(static (compilation, token) =>
                compilation.Options is CSharpCompilationOptions { AllowUnsafe: true } &&
                compilation.GetTypeByMetadataName("System.Runtime.CompilerServices.SkipLocalsInit") is not null);

        // Get all declared struct types and their type symbols
        IncrementalValuesProvider<(StructDeclarationSyntax Syntax, INamedTypeSymbol Symbol)> structDeclarations =
            context.SyntaxProvider.CreateSyntaxProvider(
                static (node, token) => node is StructDeclarationSyntax structDeclaration,
                static (context, token) => (
                    (StructDeclarationSyntax)context.Node,
                    Symbol: (INamedTypeSymbol?)context.SemanticModel.GetDeclaredSymbol(context.Node, token)))
            .Where(static pair => pair.Symbol is not null)!;

        // Get the symbol for the IComputeShader interface
        IncrementalValueProvider<INamedTypeSymbol> iComputeShaderSymbol =
            context.CompilationProvider
            .Select(static (compilation, token) => compilation.GetTypeByMetadataName(typeof(IComputeShader).FullName))!;

        // Get the symbol for the IPixelShader<TPixel> interface
        IncrementalValueProvider<INamedTypeSymbol> iPixelShaderSymbol =
            context.CompilationProvider
            .Select(static (compilation, token) => compilation.GetTypeByMetadataName(typeof(IPixelShader<>).FullName))!;

        // Filter struct declarations that implement a shader interface, and also gather the hierarchy info and shader type
        IncrementalValuesProvider<(StructDeclarationSyntax Syntax, INamedTypeSymbol Symbol, HierarchyInfo Hierarchy, Type Type)> shaderDeclarations =
            structDeclarations
            .Combine(iComputeShaderSymbol.Combine(iPixelShaderSymbol))
            .Select(static (item, token) => (item.Left, Type: GetShaderType(item.Left.Symbol, item.Right.Left, item.Right.Right)))
            .Where(static item => item.Type is not null)
            .Select(static (item, token) => (item.Left.Syntax, item.Left.Symbol, HierarchyInfo.From(item.Left.Symbol), item.Type!));

        // Get the hierarchy info and delegate field names for each shader type
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchIdInfo Info)> hierarchyAndDispatchIdInfo =
            shaderDeclarations
            .Select(static (item, token) => (item.Hierarchy, new DispatchIdInfo(GetDispatchId.GetInfo(item.Symbol))))
            .WithComparers(HierarchyInfo.Comparer.Default, DispatchIdInfo.Comparer.Default);

        // Generate the GetDispatchId() methods
        context.RegisterSourceOutput(hierarchyAndDispatchIdInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getDispatchIdMethod = GetDispatchId.GetSyntax(item.Info.Delegates);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getDispatchIdMethod, false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.GetDispatchId", SourceText.From(compilationUnit.ToFullString(), Encoding.UTF8));
        });

        // Get the [EmbeddedBytecode] attribute
        IncrementalValueProvider<INamedTypeSymbol> embeddedBytecodeSymbol =
            context.CompilationProvider
            .Select(static (compilation, token) => compilation.GetTypeByMetadataName(typeof(EmbeddedBytecodeAttribute).FullName)!);

        // Get the dispatch data, HLSL source and embedded bytecode info. This info is computed on the
        // same step as parts are shared in following sub-branches in the incremental generator pipeline.
        IncrementalValuesProvider<(Result<DispatchDataInfo> Dispatch, Result<HlslSourceInfo> Hlsl, Result<ThreadIdsInfo> ThreadIds)> shaderInfoWithErrors =
            shaderDeclarations
            .Combine(context.CompilationProvider)
            .Combine(embeddedBytecodeSymbol)
            .Select(static (item, token) =>
            {
                // LoadDispatchData() info
                ImmutableArray<FieldInfo> fieldInfos = LoadDispatchData.GetInfo(
                    item.Left.Left.Symbol,
                    item.Left.Left.Type,
                    out int resourceCount,
                    out int root32BitConstantCount,
                    out ImmutableArray<Diagnostic> dispatchDataDiagnostics);

                Result<DispatchDataInfo> dispatchDataInfo = new(new DispatchDataInfo(
                    item.Left.Left.Hierarchy,
                    item.Left.Left.Type,
                    fieldInfos,
                    resourceCount,
                    root32BitConstantCount),
                    dispatchDataDiagnostics);

                // BuildHlslString() info
                HlslSourceInfo hlslSourceInfo = BuildHlslString.GetInfo(
                    item.Left.Right,
                    item.Left.Left.Syntax,
                    item.Left.Left.Symbol,
                    out ImmutableArray<Diagnostic> hlslSourceDiagnostics);

                // TryGetBytecode() info
                ThreadIdsInfo threadIds = TryGetBytecode.GetInfo(
                    item.Right,
                    item.Left.Left.Symbol,
                    !hlslSourceInfo.Delegates.IsEmpty,
                    out ImmutableArray<Diagnostic> threadIdsDiagnostics);

                return (
                    dispatchDataInfo,
                    new Result<HlslSourceInfo>(hlslSourceInfo, hlslSourceDiagnostics),
                    new Result<ThreadIdsInfo>(threadIds, threadIdsDiagnostics));
            });

        // Output the diagnostics
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Dispatch.Errors));
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Hlsl.Errors));
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.ThreadIds.Errors));

        // Filter all items to enable caching at a coarse level, and remove diagnostics
        IncrementalValuesProvider<(DispatchDataInfo Dispatch, HlslSourceInfo Hlsl, ThreadIdsInfo ThreadIds)> shaderInfo =
            shaderInfoWithErrors
            .Select(static (item, token) => (item.Dispatch.Value, item.Hlsl.Value, item.ThreadIds.Value))
            .WithComparers(DispatchDataInfo.Comparer.Default, HlslSourceInfo.Comparer.Default, ThreadIdsInfo.Comparer.Default);

        // Get a filtered sequence to enable caching
        IncrementalValuesProvider<DispatchDataInfo> dispatchDataInfo =
            shaderInfo
            .Select(static (item, token) => item.Dispatch)
            .WithComparer(DispatchDataInfo.Comparer.Default);

        // Generate the LoadDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = LoadDispatchData.GetSyntax(
                item.Left.Type,
                item.Left.FieldInfos,
                item.Left.ResourceCount,
                item.Left.Root32BitConstantCount);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, loadDispatchDataMethod, item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.LoadDispatchData", SourceText.From(compilationUnit.ToFullString(), Encoding.UTF8));
        }); 

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslSourceInfo SourceInfo)> hlslSourceInfo =
            shaderInfo
            .Select(static (item, token) => (item.Dispatch.Hierarchy, item.Hlsl))
            .WithComparers(HierarchyInfo.Comparer.Default, HlslSourceInfo.Comparer.Default);

        // Generate the BuildHlslString() methods
        context.RegisterSourceOutput(hlslSourceInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = BuildHlslString.GetSyntax(item.Left.SourceInfo);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, buildHlslStringMethod, item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.BuildHlslString", SourceText.From(compilationUnit.ToFullString(), Encoding.UTF8));
        });

        // Get the dispatch metadata info
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchMetadataInfo MetadataInfo)> dispatchMetadataInfo =
            shaderInfo
            .Select(static (item, token) => (
                item.Dispatch.Hierarchy,
                LoadDispatchMetadata.GetInfo(
                    item.Dispatch.Root32BitConstantCount,
                    item.Hlsl.ImplicitTextureType is not null,
                    item.Hlsl.IsSamplerUsed,
                    item.Dispatch.FieldInfos)))
            .WithComparers(HierarchyInfo.Comparer.Default, DispatchMetadataInfo.Comparer.Default);

        // Generate the LoadDispatchMetadata() methods
        context.RegisterSourceOutput(dispatchMetadataInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = LoadDispatchMetadata.GetSyntax(item.Left.MetadataInfo);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, buildHlslStringMethod, item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.LoadDispatchMetadata", SourceText.From(compilationUnit.ToFullString(), Encoding.UTF8));
        });

        // Transform the raw HLSL source to compile
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string Hlsl, ThreadIdsInfo ThreadIds)> shaderBytecodeInfo =
            shaderInfo
            .Select(static (item, token) => (item.Dispatch.Hierarchy, BuildHlslString.GetNonDynamicHlslSource(item.Hlsl), item.ThreadIds))
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<string>.Default, ThreadIdsInfo.Comparer.Default);

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, Result<EmbeddedBytecodeInfo> BytecodeInfo)> embeddedBytecodeWithErrors =
            shaderBytecodeInfo
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = TryGetBytecode.GetBytecode(item.ThreadIds, item.Hlsl, out ImmutableArray<Diagnostic> diagnostics);

                EmbeddedBytecodeInfo bytecodeInfo = new(
                    item.ThreadIds.X,
                    item.ThreadIds.Y,
                    item.ThreadIds.Z,
                    bytecode);

                return (item.Hierarchy, new Result<EmbeddedBytecodeInfo>(bytecodeInfo, diagnostics));
            });

        // Output the diagnostics
        context.ReportDiagnostics(embeddedBytecodeWithErrors.Select(static (item, token) => item.BytecodeInfo.Errors));

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo)> embeddedBytecode =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.BytecodeInfo.Value))
            .WithComparers(HierarchyInfo.Comparer.Default, EmbeddedBytecodeInfo.Comparer.Default);

        // Generate the TryGetBytecode() methods
        context.RegisterSourceOutput(embeddedBytecode, static (context, item) =>
        {
            MethodDeclarationSyntax tryGetBytecodeMethod = TryGetBytecode.GetSyntax(item.BytecodeInfo, out Func<SyntaxNode, string> fixup);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, tryGetBytecodeMethod, false);
            string text = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FilenameHint}.TryGetBytecode", SourceText.From(text, Encoding.UTF8));
        });
    }
}
