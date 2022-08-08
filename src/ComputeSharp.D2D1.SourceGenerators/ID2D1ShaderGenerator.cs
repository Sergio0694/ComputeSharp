using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A source generator creating data loaders for the <see cref="ID2D1PixelShader"/> type.
/// </summary>
[Generator(LanguageNames.CSharp)]
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

        // Get the dispatch data, input types, resource texture descriptions, HLSL source and embedded bytecode info. This info
        // is computed on the same step as parts are shared in following sub-branches in the incremental generator pipeline.
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchDataInfo Dispatch, InputTypesInfo InputTypes, ResourceTextureDescriptionsInfo ResourceTextureDescriptions, HlslShaderSourceInfo Source, ImmutableArray<Diagnostic> Diagnostics)> shaderInfoWithErrors =
            shaderDeclarations
            .Combine(context.CompilationProvider)
            .Select(static (item, token) =>
            {
                ImmutableArray<Diagnostic>.Builder diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();

                // LoadDispatchData() info
                ImmutableArray<FieldInfo> fieldInfos = LoadDispatchData.GetInfo(
                    diagnostics,
                    item.Left.Symbol,
                    out int constantBufferSizeInBytes);

                token.ThrowIfCancellationRequested();

                DispatchDataInfo dispatchDataInfo = new(fieldInfos, constantBufferSizeInBytes);

                // Get the input info for GetInputInfo()
                GetInputType.GetInfo(
                    diagnostics,
                    item.Left.Symbol,
                    out int inputCount,
                    out ImmutableArray<int> inputSimpleIndices,
                    out ImmutableArray<int> inputComplexIndices,
                    out ImmutableArray<uint> inputTypes);

                InputTypesInfo inputTypesInfo = new(inputTypes);

                // Get the resource texture info for LoadResourceTextureDescriptions()
                LoadResourceTextureDescriptions.GetInfo(
                    diagnostics,
                    item.Left.Symbol,
                    inputCount,
                    out ImmutableArray<ResourceTextureDescription> resourceTextureDescriptions);

                ResourceTextureDescriptionsInfo resourceTextureDescriptionsInfo = new(resourceTextureDescriptions);

                // Get HLSL source for BuildHlslSource()
                string hlslSource = BuildHlslSource.GetHlslSource(
                    diagnostics,
                    item.Right,
                    item.Left.Syntax,
                    item.Left.Symbol,
                    inputCount,
                    inputSimpleIndices,
                    inputComplexIndices);

                token.ThrowIfCancellationRequested();

                // Get the shader profile and linking info for LoadBytecode()
                D2D1ShaderProfile? shaderProfile = LoadBytecode.GetShaderProfile(item.Left.Symbol);
                D2D1CompileOptions? compileOptions = LoadBytecode.GetCompileOptions(diagnostics, item.Left.Symbol);
                bool isLinkingSupported = LoadBytecode.IsSimpleInputShader(item.Left.Symbol, inputCount);

                HlslShaderSourceInfo sourceInfo = new(
                    hlslSource,
                    shaderProfile,
                    compileOptions,
                    isLinkingSupported,
                    HasErrors: diagnostics.Count > 0);

                token.ThrowIfCancellationRequested();

                return (
                    item.Left.Hierarchy,
                    dispatchDataInfo,
                    inputTypesInfo,
                    resourceTextureDescriptionsInfo,
                    sourceInfo,
                    diagnostics.ToImmutableArray());
            });

        // Output the diagnostics
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Diagnostics));

        // Get all errors for invalid [D2D1CompileOptions] attributes added to the assembly level
        IncrementalValuesProvider<Diagnostic> d2DCompileOptionsAtAssemblyLevelErrors =
            context.CompilationProvider
            .SelectMany(static (compilation, token) => ImmutableArray.Create(LoadBytecode.GetAssemblyLevelCompileOptionsDiagnostics(compilation.Assembly, token)))
            .Where(static item => item is not null)!;

        // Output the diagnostics
        context.ReportDiagnostics(d2DCompileOptionsAtAssemblyLevelErrors.Select(static (item, token) => item));

        // Filter all items to enable caching at a coarse level, and remove diagnostics
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchDataInfo Dispatch, InputTypesInfo InputTypes, ResourceTextureDescriptionsInfo ResourceTextureDescriptions, HlslShaderSourceInfo Source)> shaderInfo =
            shaderInfoWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.Dispatch, item.InputTypes, item.ResourceTextureDescriptions, item.Source))
            .WithComparers(HierarchyInfo.Comparer.Default, DispatchDataInfo.Comparer.Default, InputTypesInfo.Comparer.Default, ResourceTextureDescriptionsInfo.Comparer.Default, EqualityComparer<HlslShaderSourceInfo>.Default);

        // Filter items to enable caching for the input count methods
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, int InputCount)> inputCountInfo =
            shaderInfo
            .Select(static (item, token) => (item.Hierarchy, item.InputTypes.InputTypes.Length))
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<int>.Default);

        // Generate the GetInputCount() methods
        context.RegisterSourceOutput(inputCountInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getInputCountMethod = GetInputCount.GetSyntax(item.InputCount);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getInputCountMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(GetInputCount)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get a filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, InputTypesInfo InputTypes)> inputTypesInfo =
            shaderInfo
            .Select(static (item, token) => (item.Hierarchy, item.InputTypes))
            .WithComparers(HierarchyInfo.Comparer.Default, InputTypesInfo.Comparer.Default);

        // Generate the GetInputType() methods
        context.RegisterSourceOutput(inputTypesInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getInputTypeMethod = GetInputType.GetSyntax(item.InputTypes.InputTypes);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getInputTypeMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(GetInputType)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get a filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, ResourceTextureDescriptionsInfo ResourceTextureDescriptions)> resourceTextureDescriptionsInfo =
            shaderInfo
            .Select(static (item, token) => (item.Hierarchy, item.ResourceTextureDescriptions))
            .WithComparers(HierarchyInfo.Comparer.Default, ResourceTextureDescriptionsInfo.Comparer.Default);

        // Generate the LoadResourceTextureDescriptions() methods
        context.RegisterSourceOutput(resourceTextureDescriptionsInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax getInputTypeMethod = LoadResourceTextureDescriptions.GetSyntax(item.Left.ResourceTextureDescriptions);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, getInputTypeMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadResourceTextureDescriptions)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get a filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchDataInfo Dispatch)> dispatchDataInfo =
            shaderInfo
            .Select(static (item, token) => (item.Hierarchy, item.Dispatch))
            .WithComparers(HierarchyInfo.Comparer.Default, DispatchDataInfo.Comparer.Default);

        // Generate the LoadDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = LoadDispatchData.GetSyntax(item.Left.Dispatch);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, loadDispatchDataMethod, canUseSkipLocalsInit: item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadDispatchData)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Generate the InitializeFromDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo, static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = InitializeFromDispatchData.GetSyntax(item.Dispatch);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, loadDispatchDataMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(InitializeFromDispatchData)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string HlslSource)> hlslSourceInfo =
            shaderInfo
            .Select(static (item, token) => (item.Hierarchy, item.Source.HlslSource))
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<string>.Default);

        // Check whether raw multiline string literals can be used (C# preview)
        IncrementalValueProvider<bool> canUseRawMultiLineStringLiterals =
            context.ParseOptionsProvider
            .Select((item, _) => item is CSharpParseOptions { LanguageVersion: >= LanguageVersion.Preview });

        // Generate the BuildHlslSource() methods
        context.RegisterSourceOutput(hlslSourceInfo.Combine(canUseRawMultiLineStringLiterals), static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = BuildHlslSource.GetSyntax(item.Left.HlslSource, item.Left.Hierarchy.Hierarchy.Length, item.Right);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, buildHlslStringMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(BuildHlslSource)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get a filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslShaderSourceInfo Source)> shaderBytecodeInfo =
            shaderInfo
            .Select(static (item, token) => (item.Hierarchy, item.Source))
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<HlslShaderSourceInfo>.Default);

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo, DiagnosticInfo? Diagnostic)> embeddedBytecodeWithErrors =
            shaderBytecodeInfo
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = LoadBytecode.GetBytecode(
                    item.Source,
                    token,
                    out D2D1CompileOptions options,
                    out DiagnosticInfo? diagnostic);

                token.ThrowIfCancellationRequested();

                EmbeddedBytecodeInfo bytecodeInfo = new(
                    item.Source.HlslSource,
                    item.Source.ShaderProfile,
                    options,
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

        // Generate the LoadBytecode() methods
        context.RegisterSourceOutput(embeddedBytecode, static (context, item) =>
        {
            MethodDeclarationSyntax loadBytecodeMethod = LoadBytecode.GetSyntax(item.BytecodeInfo, out Func<SyntaxNode, SourceText> fixup);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, loadBytecodeMethod, canUseSkipLocalsInit: false);
            SourceText text = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(LoadBytecode)}", text);
        });

        // Get the output buffer data, which can be computed separately from all other generation steps.
        // Caching at a fine grained level can be enabled immediately, as both models are comparable.
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, OutputBufferInfo Info)> outputBufferInfo =
            shaderDeclarations
            .Select(static (item, token) =>
            {
                // GetOutputBuffer() info
                GetOutputBuffer.GetInfo(item.Symbol, out D2D1BufferPrecision bufferPrecision, out D2D1ChannelDepth channelDepth);

                token.ThrowIfCancellationRequested();

                OutputBufferInfo outputBufferInfo = new(bufferPrecision, channelDepth);

                return (item.Hierarchy, outputBufferInfo);
            })
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<OutputBufferInfo>.Default);

        // Generate the GetOutputBuffer() methods
        context.RegisterSourceOutput(outputBufferInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getOutputBufferMethod = GetOutputBuffer.GetSyntax(item.Info);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getOutputBufferMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(GetOutputBuffer)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the input description data, which can also be computed separately from all other generation steps.
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, Result<InputDescriptionsInfo> Info)> inputDescriptionsInfoWithErrors =
            shaderDeclarations
            .Select(static (item, token) =>
            {
                // LoadInputDescriptions() info
                LoadInputDescriptions.GetInfo(
                    item.Symbol,
                    out ImmutableArray<InputDescription> inputDescriptions,
                    out ImmutableArray<Diagnostic> diagnostics);

                token.ThrowIfCancellationRequested();

                InputDescriptionsInfo inputDescriptionsInfo = new(inputDescriptions);

                return (item.Hierarchy, new Result<InputDescriptionsInfo>(inputDescriptionsInfo, diagnostics));
            });

        // Output the diagnostics
        context.ReportDiagnostics(inputDescriptionsInfoWithErrors.Select(static (item, token) => item.Info.Errors));

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, InputDescriptionsInfo Info)> inputDescriptionsInfo =
            inputDescriptionsInfoWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.Info.Value))
            .WithComparers(HierarchyInfo.Comparer.Default, InputDescriptionsInfo.Comparer.Default);

        // Generate the LoadInputDescriptions() methods
        context.RegisterSourceOutput(inputDescriptionsInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax loadInputDescriptionsMethod = LoadInputDescriptions.GetSyntax(item.Left.Info);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, loadInputDescriptionsMethod, canUseSkipLocalsInit: item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadInputDescriptions)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the pixel options, again independently from other generation steps.
        // This can also be immediately cached at a fine grained level.
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, D2D1PixelOptions Options)> pixelOptionsInfo =
            shaderDeclarations
            .Select(static (item, token) =>
            {
                // GetPixelOptions() info
                GetPixelOptions.GetInfo(item.Symbol, out D2D1PixelOptions pixelOptions);

                token.ThrowIfCancellationRequested();

                return (item.Hierarchy, pixelOptions);
            })
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<D2D1PixelOptions>.Default);

        // Generate the GetPixelOptions() methods
        context.RegisterSourceOutput(pixelOptionsInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getPixelOptionsMethod = GetPixelOptions.GetSyntax(item.Options);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getPixelOptionsMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(GetPixelOptions)}", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get all diagnostics for invalid [D2DResourceTextureIndex] uses
        IncrementalValuesProvider<Diagnostic> invalidD2DResourceTextureIndexUseErrors =
            context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => node is FieldDeclarationSyntax { AttributeLists.Count: > 0 },
                static (context, _) => ((FieldDeclarationSyntax)context.Node).Declaration.Variables.Select(v => (IFieldSymbol)context.SemanticModel.GetDeclaredSymbol(v)!))
            .SelectMany(static (item, _) => item)
            .Select(static (item, token) => LoadResourceTextureDescriptions.GetDiagnosticForFieldWithInvalidD2DResourceTextureIndexAttribute(item))
            .Where(static diagnostic => diagnostic is not null)!;

        // Output the diagnostics
        context.ReportDiagnostics(invalidD2DResourceTextureIndexUseErrors);
    }
}
