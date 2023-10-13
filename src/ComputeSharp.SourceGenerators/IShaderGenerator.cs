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
    /// <summary>
    /// The name of generator to include in the generated code.
    /// </summary>
    private const string GeneratorName = "ComputeSharp.IShaderGenerator";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
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

                    return new ShaderInfo(
                        Hierarchy: HierarchyInfo.From(typeSymbol),
                        DispatchData: new DispatchDataInfo(
                            isPixelShaderLike,
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
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, _) => item.Diagnostcs));

        // Generate the LoadDispatchData() methods
        context.RegisterSourceOutput(shaderInfoWithErrors, static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = LoadDispatchData.GetSyntax(
                item.DispatchData.IsPixelShaderLike,
                item.DispatchData.FieldInfos,
                item.DispatchData.ResourceCount,
                item.DispatchData.Root32BitConstantCount);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, loadDispatchDataMethod, addSkipLocalsInitAttribute: true);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(LoadDispatchData)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Generate the BuildHlslSource() methods
        context.RegisterSourceOutput(shaderInfoWithErrors, static (context, item) =>
        {
            using ImmutableArrayBuilder<IndentedTextWriter.Callback<ShaderInfo>> declaredMembers = new();

            declaredMembers.Add(BuildHlslSource.WriteSyntax);

            using IndentedTextWriter writer = new();

            item.Hierarchy.WriteSyntax(
                state: item,
                writer: writer,
                baseTypes: ReadOnlySpan<string>.Empty,
                memberCallbacks: declaredMembers.WrittenSpan);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(BuildHlslSource)}.g.cs", writer.ToString());
        });

        // Generate the LoadDispatchMetadata() methods
        context.RegisterSourceOutput(shaderInfoWithErrors, static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = LoadDispatchMetadata.GetSyntax(item.DispatchMetadata);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, buildHlslStringMethod, addSkipLocalsInitAttribute: true);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(LoadDispatchMetadata)}.g.cs", compilationUnit.GetText(Encoding.UTF8));
        });

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo EmbeddedBytecode, DeferredDiagnosticInfo? Diagnostic)> embeddedBytecodeWithErrors =
            shaderInfoWithErrors
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = LoadBytecode.GetBytecode(item.ThreadIds, item.HlslShaderSource.HlslSource, token, out DeferredDiagnosticInfo? diagnostic);

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
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo EmbeddedBytecode)> embeddedBytecodeInfo =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.EmbeddedBytecode));

        // Generate the TryGetBytecode() methods
        context.RegisterSourceOutput(embeddedBytecodeInfo, static (context, item) =>
        {
            MethodDeclarationSyntax tryGetBytecodeMethod = LoadBytecode.GetSyntax(item.EmbeddedBytecode, out Func<SyntaxNode, SourceText> fixup);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, tryGetBytecodeMethod, addSkipLocalsInitAttribute: false);
            SourceText text = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{nameof(LoadBytecode)}.g.cs", text);
        });
    }
}