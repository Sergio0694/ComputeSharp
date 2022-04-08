using System.Collections.Immutable;
using ComputeSharp.D2D1Interop.SourceGenerators.Diagnostics;
using ComputeSharp.D2D1Interop.SourceGenerators.Extensions;
using ComputeSharp.D2D1Interop.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.D2D1Interop.SourceGenerators;

/// <summary>
/// A source generator creating data loaders for the <see cref="ID2D1PixelShader"/> type.
/// </summary>
[Generator]
public sealed partial class ID2D1ShaderGenerator : IIncrementalGenerator
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

        // Get all declared struct types and their type symbols
        IncrementalValuesProvider<(StructDeclarationSyntax Syntax, INamedTypeSymbol Symbol)> structDeclarations =
            context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, token) => node is StructDeclarationSyntax structDeclaration,
                static (context, token) => (
                    (StructDeclarationSyntax)context.Node,
                    Symbol: (INamedTypeSymbol?)context.SemanticModel.GetDeclaredSymbol(context.Node, token)))
            .Where(static pair => pair.Symbol is { Interfaces.IsEmpty: false })!;

        // Get the symbol for the ID2D1PixelShader interface
        IncrementalValueProvider<INamedTypeSymbol> pixelShaderInterface =
            context.CompilationProvider
            .Select(static (compilation, token) => compilation.GetTypeByMetadataName(typeof(ID2D1PixelShader).FullName)!);

        // Filter struct declarations that implement the shader interface, and also gather the hierarchy info
        IncrementalValuesProvider<(StructDeclarationSyntax Syntax, INamedTypeSymbol Symbol, HierarchyInfo Hierarchy)> shaderDeclarations =
            structDeclarations
            .Combine(pixelShaderInterface)
            .Where(static item => IsD2D1PixelShaderType(item.Left.Symbol, item.Right))
            .Select(static (item, token) => (item.Left.Syntax, item.Left.Symbol, HierarchyInfo.From(item.Left.Symbol)));

        // Get the dispatch data, HLSL source and embedded bytecode info. This info is computed on the
        // same step as parts are shared in following sub-branches in the incremental generator pipeline.
        IncrementalValuesProvider<(Result<DispatchDataInfo> Dispatch, Result<HlslShaderSourceInfo> Hlsl, Result<ThreadIdsInfo> ThreadIds)> shaderInfoWithErrors =
            shaderDeclarations
            .Combine(context.CompilationProvider)
            .Select(static (item, token) =>
            {
                // LoadDispatchData() info
                ImmutableArray<FieldInfo> fieldInfos = LoadDispatchData.GetInfo(
                    item.Left.Symbol,
                    out int root32BitConstantCount,
                    out ImmutableArray<Diagnostic> dispatchDataDiagnostics);

                token.ThrowIfCancellationRequested();

                DispatchDataInfo dispatchDataInfo = new(
                    item.Left.Hierarchy,
                    fieldInfos,
                    root32BitConstantCount);

                token.ThrowIfCancellationRequested();

                // TODO

                return (
                    new Result<DispatchDataInfo>(dispatchDataInfo, dispatchDataDiagnostics),
                    new Result<HlslShaderSourceInfo>(null!, ImmutableArray<Diagnostic>.Empty),
                    new Result<ThreadIdsInfo>(null!, ImmutableArray<Diagnostic>.Empty));
            });

        // Output the diagnostics
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Dispatch.Errors));
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Hlsl.Errors));
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.ThreadIds.Errors));

        // Filter all items to enable caching at a coarse level, and remove diagnostics
        IncrementalValuesProvider<(DispatchDataInfo Dispatch, HlslShaderSourceInfo Hlsl, ThreadIdsInfo ThreadIds)> shaderInfo =
            shaderInfoWithErrors
            .Select(static (item, token) => (item.Dispatch.Value, item.Hlsl.Value, item.ThreadIds.Value))
            .WithComparers(DispatchDataInfo.Comparer.Default, HlslShaderSourceInfo.Comparer.Default, ThreadIdsInfo.Comparer.Default);

        // Get a filtered sequence to enable caching
        IncrementalValuesProvider<DispatchDataInfo> dispatchDataInfo =
            shaderInfo
            .Select(static (item, token) => item.Dispatch)
            .WithComparer(DispatchDataInfo.Comparer.Default);

        // Generate the LoadDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = LoadDispatchData.GetSyntax(item.Left.FieldInfos, item.Left.Root32BitConstantCount);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, loadDispatchDataMethod, item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadDispatchData)}", compilationUnit.ToFullString());
        });
    }
}
