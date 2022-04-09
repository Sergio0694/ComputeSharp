using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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
        IncrementalValuesProvider<(Result<DispatchDataInfo> Dispatch, Result<string> SourceInfo, Result<D2D1ShaderProfile?> ProfileInfo)> shaderInfoWithErrors =
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

                // Get HLSL source for BuildHlslSource()
                string hlslSource = BuildHlslSource.GetHlslSource(
                    item.Right,
                    item.Left.Syntax,
                    item.Left.Symbol,
                    out ImmutableArray<Diagnostic> hlslSourceDiagnostics);

                token.ThrowIfCancellationRequested();

                // Get the shader profile
                D2D1ShaderProfile? shaderProfile = LoadBytecode.GetShaderProfile(item.Left.Symbol);

                return (
                    new Result<DispatchDataInfo>(dispatchDataInfo, dispatchDataDiagnostics),
                    new Result<string>(hlslSource, hlslSourceDiagnostics),
                    new Result<D2D1ShaderProfile?>(shaderProfile, ImmutableArray<Diagnostic>.Empty));
            });

        // Output the diagnostics
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Dispatch.Errors));
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.SourceInfo.Errors));

        // Filter all items to enable caching at a coarse level, and remove diagnostics
        IncrementalValuesProvider<(DispatchDataInfo Dispatch, string HlslSource, D2D1ShaderProfile? ShaderProfile)> shaderInfo =
            shaderInfoWithErrors
            .Select(static (item, token) => (item.Dispatch.Value, item.SourceInfo.Value, item.ProfileInfo.Value))
            .WithComparers(DispatchDataInfo.Comparer.Default, EqualityComparer<string>.Default, EqualityComparer<D2D1ShaderProfile?>.Default);

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

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string HlslSource)> hlslSourceInfo =
            shaderInfo
            .Select(static (item, token) => (item.Dispatch.Hierarchy, item.HlslSource))
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<string>.Default);

        // Generate the BuildHlslSource() methods
        context.RegisterSourceOutput(hlslSourceInfo, static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = BuildHlslSource.GetSyntax(item.HlslSource);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, buildHlslStringMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(BuildHlslSource)}", compilationUnit.ToFullString());
        });

        // Get a filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string HlslSource, D2D1ShaderProfile? ShaderProfile)> shaderBytecodeInfo =
            shaderInfo
            .Select(static (item, token) => (item.Dispatch.Hierarchy, item.HlslSource, item.ShaderProfile))
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<string>.Default, EqualityComparer<D2D1ShaderProfile?>.Default);

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo, DiagnosticInfo? Diagnostic)> embeddedBytecodeWithErrors =
            shaderBytecodeInfo
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = LoadBytecode.GetBytecode(item.HlslSource, item.ShaderProfile, token, out DiagnosticInfo? diagnostic);

                token.ThrowIfCancellationRequested();

                EmbeddedBytecodeInfo bytecodeInfo = new(item.HlslSource, bytecode);

                return (item.Hierarchy, bytecodeInfo, diagnostic);
            });

        // Gather the diagnostics
        IncrementalValuesProvider<ImmutableArray<Diagnostic>> embeddedBytecodeDiagnostics =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy.MetadataName, item.Diagnostic))
            .Where(static item => item.Diagnostic is not null)
            .Combine(context.CompilationProvider)
            .Select(static (item, token) =>
            {
                INamedTypeSymbol? typeSymbol = item.Right.GetTypeByMetadataName(item.Left.MetadataName)!;
                Diagnostic diagnostic = Diagnostic.Create(
                    item.Left.Diagnostic!.Descriptor,
                    typeSymbol.Locations.FirstOrDefault(),
                    new object[] { typeSymbol }.Concat(item.Left.Diagnostic.Args).ToArray());

                return ImmutableArray.Create(diagnostic);
            });

        // Output the diagnostics
        context.ReportDiagnostics(embeddedBytecodeDiagnostics);

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo)> embeddedBytecode =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.BytecodeInfo))
            .WithComparers(HierarchyInfo.Comparer.Default, EmbeddedBytecodeInfo.Comparer.Default);

        // Generate the LoadBytecode() methods
        context.RegisterSourceOutput(embeddedBytecode, static (context, item) =>
        {
            MethodDeclarationSyntax loadBytecodeMethod = LoadBytecode.GetSyntax(item.BytecodeInfo, out Func<SyntaxNode, string> fixup);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, loadBytecodeMethod, false);
            string text = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(LoadBytecode)}", text);
        });
    }
}
