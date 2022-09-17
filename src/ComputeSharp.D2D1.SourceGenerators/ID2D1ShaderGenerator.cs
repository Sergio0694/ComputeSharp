using System;
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
        IncrementalValuesProvider<D2D1ShaderInfo> shaderInfoWithErrors =
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

                    // Check that the shader implements the ID2D1PixelShader interface
                    if (!IsD2D1PixelShaderType(typeSymbol, context.SemanticModel.Compilation))
                    {
                        return default;
                    }

                    ImmutableArray<Diagnostic>.Builder diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();

                    // LoadDispatchData() info
                    ImmutableArray<FieldInfo> fieldInfos = LoadDispatchData.GetInfo(
                        diagnostics,
                        typeSymbol,
                        out int constantBufferSizeInBytes);

                    token.ThrowIfCancellationRequested();

                    // Get the input info for GetInputInfo()
                    GetInputType.GetInfo(
                        diagnostics,
                        typeSymbol,
                        out int inputCount,
                        out ImmutableArray<int> inputSimpleIndices,
                        out ImmutableArray<int> inputComplexIndices,
                        out ImmutableArray<uint> inputTypes);

                    // Get the resource texture info for LoadResourceTextureDescriptions()
                    LoadResourceTextureDescriptions.GetInfo(
                        diagnostics,
                        typeSymbol,
                        inputCount,
                        out ImmutableArray<ResourceTextureDescription> resourceTextureDescriptions);

                    // Get HLSL source for BuildHlslSource()
                    string hlslSource = BuildHlslSource.GetHlslSource(
                        diagnostics,
                        context.SemanticModel.Compilation,
                        typeDeclaration,
                        typeSymbol,
                        inputCount,
                        inputSimpleIndices,
                        inputComplexIndices);

                    token.ThrowIfCancellationRequested();

                    // Get the shader profile and linking info for LoadBytecode()
                    D2D1ShaderProfile? shaderProfile = LoadBytecode.GetShaderProfile(typeSymbol);
                    D2D1CompileOptions? compileOptions = LoadBytecode.GetCompileOptions(diagnostics, typeSymbol);
                    bool isLinkingSupported = LoadBytecode.IsSimpleInputShader(typeSymbol, inputCount);

                    token.ThrowIfCancellationRequested();

                    // Get the info for GetOutputBuffer()
                    GetOutputBuffer.GetInfo(typeSymbol, out D2D1BufferPrecision bufferPrecision, out D2D1ChannelDepth channelDepth);

                    token.ThrowIfCancellationRequested();

                    // Get the info for LoadInputDescriptions()
                    LoadInputDescriptions.GetInfo(
                        diagnostics,
                        typeSymbol,
                        out ImmutableArray<InputDescription> inputDescriptions);

                    token.ThrowIfCancellationRequested();

                    // Get the info for GetPixelOptions()
                    GetPixelOptions.GetInfo(typeSymbol, out D2D1PixelOptions pixelOptions);

                    token.ThrowIfCancellationRequested();

                    return new D2D1ShaderInfo(
                        Hierarchy: HierarchyInfo.From(typeSymbol),
                        DispatchData: new DispatchDataInfo(fieldInfos, constantBufferSizeInBytes),
                        InputTypes: new InputTypesInfo(inputTypes),
                        ResourceTextureDescriptions: new ResourceTextureDescriptionsInfo(resourceTextureDescriptions),
                        HlslShaderSource: new HlslShaderSourceInfo(
                            hlslSource,
                            shaderProfile,
                            compileOptions,
                            isLinkingSupported,
                            HasErrors: diagnostics.Count > 0),
                        OutputBuffer: new OutputBufferInfo(bufferPrecision, channelDepth),
                        InputDescriptions: new InputDescriptionsInfo(inputDescriptions),
                        PixelOptions: pixelOptions,
                        Diagnostcs: diagnostics.ToImmutable());
                })
            .Where(static item => item is not null)!;

        // Check whether [SkipLocalsInit] can be used
        IncrementalValueProvider<bool> canUseSkipLocalsInit =
            context.CompilationProvider
            .Select(static (compilation, _) =>
                compilation.Options is CSharpCompilationOptions { AllowUnsafe: true } &&
                compilation.HasAccessibleTypeWithMetadataName("System.Runtime.CompilerServices.SkipLocalsInitAttribute"));

        // Output the diagnostics
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, _) => item.Diagnostcs));

        // Get the GetInputCount() info (hierarchy and input count)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, int InputCount)> inputCountInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.InputTypes.InputTypes.Length));

        // Generate the GetInputCount() methods
        context.RegisterSourceOutput(inputCountInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getInputCountMethod = GetInputCount.GetSyntax(item.InputCount);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getInputCountMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(GetInputCount)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the GetInputType() info (hierarchy and input types)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, InputTypesInfo InputTypes)> inputTypesInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.InputTypes));

        // Generate the GetInputType() methods
        context.RegisterSourceOutput(inputTypesInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getInputTypeMethod = GetInputType.GetSyntax(item.InputTypes.InputTypes);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getInputTypeMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(GetInputType)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the LoadResourceTextureDescriptions() info (hierarchy and resource texture descriptions)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, ResourceTextureDescriptionsInfo ResourceTextureDescriptions)> resourceTextureDescriptionsInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.ResourceTextureDescriptions));

        // Generate the LoadResourceTextureDescriptions() methods
        context.RegisterSourceOutput(resourceTextureDescriptionsInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax getInputTypeMethod = LoadResourceTextureDescriptions.GetSyntax(item.Left.ResourceTextureDescriptions);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, getInputTypeMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadResourceTextureDescriptions)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the info for both LoadDispatchData() and InitializeFromDispatchData() (hierarchy and dispatch data)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchDataInfo Dispatch)> dispatchDataInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.DispatchData));

        // Generate the LoadDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = LoadDispatchData.GetSyntax(item.Left.Dispatch);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, loadDispatchDataMethod, canUseSkipLocalsInit: item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadDispatchData)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Generate the InitializeFromDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo, static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = InitializeFromDispatchData.GetSyntax(item.Dispatch);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, loadDispatchDataMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(InitializeFromDispatchData)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the BuildHlslSource() info (hierarchy and HLSL source)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string HlslSource)> hlslSourceInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.HlslShaderSource.HlslSource));

        // Generate the BuildHlslSource() methods
        context.RegisterSourceOutput(hlslSourceInfo, static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = BuildHlslSource.GetSyntax(item.HlslSource, item.Hierarchy.Hierarchy.Length);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, buildHlslStringMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(BuildHlslSource)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get a filtered sequence to enable caching for the HLSL source info, before the shader compilation step
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslShaderSourceInfo Source)> shaderBytecodeInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.HlslShaderSource));

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
            .Select(static (item, _) => (item.Hierarchy.FullyQualifiedMetadataName, item.Diagnostic))
            .Where(static item => item.Diagnostic is not null)
            .Combine(context.CompilationProvider)
            .Select(static (item, _) =>
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
            .Select(static (item, _) => (item.Hierarchy, item.BytecodeInfo));

        // Generate the LoadBytecode() methods
        context.RegisterSourceOutput(embeddedBytecode, static (context, item) =>
        {
            MethodDeclarationSyntax loadBytecodeMethod = LoadBytecode.GetSyntax(item.BytecodeInfo, out Func<SyntaxNode, SourceText> fixup);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, loadBytecodeMethod, canUseSkipLocalsInit: false);
            SourceText text = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(LoadBytecode)}.g.cs", text);
        });

        // Get the GetOutputBuffer() info (hierarchy and output buffers)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, OutputBufferInfo OutputBuffer)> outputBufferInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.OutputBuffer));

        // Generate the GetOutputBuffer() methods
        context.RegisterSourceOutput(outputBufferInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getOutputBufferMethod = GetOutputBuffer.GetSyntax(item.OutputBuffer);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getOutputBufferMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(GetOutputBuffer)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the LoadInputDescriptions() info (hierarchy and input descriptions)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, InputDescriptionsInfo InputDescriptions)> inputDescriptionsInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.InputDescriptions));

        // Generate the LoadInputDescriptions() methods
        context.RegisterSourceOutput(inputDescriptionsInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax loadInputDescriptionsMethod = LoadInputDescriptions.GetSyntax(item.Left.InputDescriptions);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, loadInputDescriptionsMethod, canUseSkipLocalsInit: item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.{nameof(LoadInputDescriptions)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Get the GetPixelOptions() info (hierarchy and pixel options)
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, D2D1PixelOptions PixelOptions)> pixelOptionsInfo =
            shaderInfoWithErrors
            .Select(static (item, _) => (item.Hierarchy, item.PixelOptions));

        // Generate the GetPixelOptions() methods
        context.RegisterSourceOutput(pixelOptionsInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getPixelOptionsMethod = GetPixelOptions.GetSyntax(item.PixelOptions);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getPixelOptionsMethod, canUseSkipLocalsInit: false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{nameof(GetPixelOptions)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });
    }
}
