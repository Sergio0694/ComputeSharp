using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGenerators.Diagnostics;
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
            .Select(static (compilation, token) =>
                compilation.Options is CSharpCompilationOptions { AllowUnsafe: true } &&
                compilation.GetTypeByMetadataName("System.Runtime.CompilerServices.SkipLocalsInitAttribute") is not null);

        // Check whether or not dynamic shaders are supported
        IncrementalValueProvider<bool> supportsDynamicShaders =
            context.CompilationProvider
            .Select(static (compilation, token) => compilation.ReferencedAssemblyNames.Any(
                static identity => identity.Name is "ComputeSharp.Dynamic"));

        // Get all declared struct types and their type symbols
        IncrementalValuesProvider<(StructDeclarationSyntax Syntax, INamedTypeSymbol Symbol)> structDeclarations =
            context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, token) => node is StructDeclarationSyntax structDeclaration,
                static (context, token) => (
                    (StructDeclarationSyntax)context.Node,
                    Symbol: (INamedTypeSymbol?)context.SemanticModel.GetDeclaredSymbol(context.Node, token)))
            .Where(static pair => pair.Symbol is { Interfaces.IsEmpty: false })!;

        // Get the symbol for the IComputeShader and IPixelShader<TPixel> interfaces
        IncrementalValueProvider<(INamedTypeSymbol Compute, INamedTypeSymbol Pixel) > shaderInterfaces =
            context.CompilationProvider
            .Select(static (compilation, token) => (
                Compute: compilation.GetTypeByMetadataName(typeof(IComputeShader).FullName)!,
                Pixel: compilation.GetTypeByMetadataName(typeof(IPixelShader<>).FullName)!));

        // Filter struct declarations that implement a shader interface, and also gather the hierarchy info and shader type
        IncrementalValuesProvider<(StructDeclarationSyntax Syntax, INamedTypeSymbol Symbol, HierarchyInfo Hierarchy, Type Type)> shaderDeclarations =
            structDeclarations
            .Combine(shaderInterfaces)
            .Select(static (item, token) => (item.Left, Type: GetShaderType(item.Left.Symbol, item.Right.Compute, item.Right.Pixel)))
            .Where(static item => item.Type is not null)
            .Select(static (item, token) => (item.Left.Syntax, item.Left.Symbol, HierarchyInfo.From(item.Left.Symbol), item.Type!));

        // Get the hierarchy info and delegate field names for each shader type
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchIdInfo Info)> hierarchyAndDispatchIdInfo =
            shaderDeclarations
            .Select(static (item, token) => (item.Hierarchy, new DispatchIdInfo(GetDispatchId.GetInfo(item.Symbol))))
            .WithComparers(HierarchyInfo.Comparer.Default, DispatchIdInfo.Comparer.Default);

        // Generate the GetDispatchId() methods
        context.RegisterSourceOutput(hierarchyAndDispatchIdInfo.Combine(supportsDynamicShaders), static (context, item) =>
        {
            MethodDeclarationSyntax getDispatchIdMethod = GetDispatchId.GetSyntax(item.Left.Info.Delegates, item.Right);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, getDispatchIdMethod, false);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(GetDispatchId)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the dispatch data, HLSL source and embedded bytecode info. This info is computed on the
        // same step as parts are shared in following sub-branches in the incremental generator pipeline.
        IncrementalValuesProvider<(Result<DispatchDataInfo> Dispatch, Result<HlslShaderSourceInfo> Hlsl, Result<ThreadIdsInfo> ThreadIds)> shaderInfoWithErrors =
            shaderDeclarations
            .Combine(supportsDynamicShaders)
            .Combine(context.CompilationProvider)
            .Select(static (item, token) =>
            {
                // LoadDispatchData() info
                ImmutableArray<FieldInfo> fieldInfos = LoadDispatchData.GetInfo(
                    item.Left.Left.Symbol,
                    item.Left.Left.Type,
                    out int resourceCount,
                    out int root32BitConstantCount,
                    out ImmutableArray<Diagnostic> dispatchDataDiagnostics);

                token.ThrowIfCancellationRequested();

                DispatchDataInfo dispatchDataInfo = new(
                    item.Left.Left.Hierarchy,
                    item.Left.Left.Type,
                    fieldInfos,
                    resourceCount,
                    root32BitConstantCount);

                token.ThrowIfCancellationRequested();

                // BuildHlslSource() info
                HlslShaderSourceInfo hlslSourceInfo = BuildHlslSource.GetInfo(
                    item.Right,
                    item.Left.Left.Syntax,
                    item.Left.Left.Symbol,
                    out ImmutableArray<Diagnostic> hlslSourceDiagnostics);

                token.ThrowIfCancellationRequested();

                // TryGetBytecode() info
                ThreadIdsInfo threadIds = LoadBytecode.GetInfo(
                    item.Left.Left.Symbol,
                    !hlslSourceInfo.Delegates.IsEmpty,
                    item.Left.Right,
                    out ImmutableArray<Diagnostic> threadIdsDiagnostics);

                token.ThrowIfCancellationRequested();

                // Ensure the bytecode generation is disabled if any errors are present
                if (!dispatchDataDiagnostics.IsDefaultOrEmpty ||
                    !hlslSourceDiagnostics.IsDefaultOrEmpty)
                {
                    threadIds = new ThreadIdsInfo(true, 0, 0, 0);
                }

                return (
                    new Result<DispatchDataInfo>(dispatchDataInfo, dispatchDataDiagnostics),
                    new Result<HlslShaderSourceInfo>(hlslSourceInfo, hlslSourceDiagnostics),
                    new Result<ThreadIdsInfo>(threadIds, threadIdsDiagnostics));
            });

        // Output the diagnostics
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Dispatch.Errors));
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Hlsl.Errors));
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.ThreadIds.Errors));

        // Filter all items to enable caching at a coarse level, and remove diagnostics
        IncrementalValuesProvider<(DispatchDataInfo Dispatch, HlslShaderSourceInfo Hlsl, ThreadIdsInfo ThreadIds)> shaderInfo =
            shaderInfoWithErrors
            .Select(static (item, token) => (item.Dispatch.Value, item.Hlsl.Value, item.ThreadIds.Value))
            .WithComparers(DispatchDataInfo.Comparer.Default, HlslShaderSourceInfo.Comparer.Default, EqualityComparer<ThreadIdsInfo>.Default);

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

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadDispatchData)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslShaderSourceInfo SourceInfo, bool SupportsDynamicShaders)> hlslSourceInfo =
            shaderInfo
            .Combine(supportsDynamicShaders)
            .Select(static (item, token) => (item.Left.Dispatch.Hierarchy, item.Left.Hlsl, item.Right))
            .WithComparers(HierarchyInfo.Comparer.Default, HlslShaderSourceInfo.Comparer.Default, EqualityComparer<bool>.Default);

        // Generate the BuildHlslSource() methods
        context.RegisterSourceOutput(hlslSourceInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = BuildHlslSource.GetSyntax(item.Left.SourceInfo, item.Left.SupportsDynamicShaders);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, buildHlslStringMethod, item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(BuildHlslSource)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the dispatch metadata info
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchMetadataInfo MetadataInfo)> dispatchMetadataInfo =
            shaderInfo
            .Select(static (item, token) =>
            {
                DispatchMetadataInfo dispatchMetadataInfo = LoadDispatchMetadata.GetInfo(
                    item.Dispatch.Root32BitConstantCount,
                    item.Hlsl.ImplicitTextureType is not null,
                    item.Hlsl.IsSamplerUsed,
                    item.Dispatch.FieldInfos);

                token.ThrowIfCancellationRequested();

                return (item.Dispatch.Hierarchy, dispatchMetadataInfo);
            })
            .WithComparers(HierarchyInfo.Comparer.Default, DispatchMetadataInfo.Comparer.Default);

        // Generate the LoadDispatchMetadata() methods
        context.RegisterSourceOutput(dispatchMetadataInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = LoadDispatchMetadata.GetSyntax(item.Left.MetadataInfo);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, buildHlslStringMethod, item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadDispatchMetadata)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Transform the raw HLSL source to compile
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string Hlsl, ThreadIdsInfo ThreadIds)> shaderBytecodeInfo =
            shaderInfo
            .Select(static (item, token) => (item.Dispatch.Hierarchy, BuildHlslSource.GetNonDynamicHlslSource(item.Hlsl), item.ThreadIds))
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<string>.Default, EqualityComparer<ThreadIdsInfo>.Default);

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo, DiagnosticInfo? Diagnostic)> embeddedBytecodeWithErrors =
            shaderBytecodeInfo
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = LoadBytecode.GetBytecode(item.ThreadIds, item.Hlsl, token, out DiagnosticInfo? diagnostic);

                token.ThrowIfCancellationRequested();

                EmbeddedBytecodeInfo bytecodeInfo = new(
                    item.ThreadIds.X,
                    item.ThreadIds.Y,
                    item.ThreadIds.Z,
                    bytecode);

                return (item.Hierarchy, bytecodeInfo, diagnostic);
            });

        // Gather the diagnostics
        IncrementalValuesProvider<Diagnostic> embeddedBytecodeDiagnostics =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy.FullyQualifiedMetadataName, item.Diagnostic))
            .Where(static item => item.Diagnostic is not null)
            .Combine(context.CompilationProvider)
            .Select(static (item, token) =>
            {
                INamedTypeSymbol typeSymbol = item.Right.GetTypeByMetadataName(item.Left.FullyQualifiedMetadataName)!;
                
                return Diagnostic.Create(
                    item.Left.Diagnostic!.Descriptor,
                    typeSymbol.Locations.FirstOrDefault(),
                    new object[] { typeSymbol }.Concat(item.Left.Diagnostic.Args).ToArray());
            });

        // Output the diagnostics
        context.ReportDiagnostics(embeddedBytecodeDiagnostics);

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo)> embeddedBytecode =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.BytecodeInfo))
            .WithComparers(HierarchyInfo.Comparer.Default, EmbeddedBytecodeInfo.Comparer.Default);

        // Generate the TryGetBytecode() methods
        context.RegisterSourceOutput(embeddedBytecode.Combine(supportsDynamicShaders), static (context, item) =>
        {
            MethodDeclarationSyntax tryGetBytecodeMethod = LoadBytecode.GetSyntax(item.Left.BytecodeInfo, item.Right, out Func<SyntaxNode, SourceText> fixup);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, tryGetBytecodeMethod, false);
            SourceText text = fixup(compilationUnit);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadBytecode)}", text);
        });
    }
}
