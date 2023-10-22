using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.D2D1.SourceGenerators.D2DPixelShaderDescriptorGenerator;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A source generator compiling methods annotated with <see cref="D2DPixelShaderSourceAttribute"/>.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class D2DPixelShaderSourceGenerator : IIncrementalGenerator
{
    /// <inheritdoc cref="D2DPixelShaderDescriptorGenerator.GeneratorName"/>
    private const string GeneratorName = "ComputeSharp.D2D1.D2DPixelShaderSourceGenerator";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Get all method declarations with the [D2DPixelShaderSource] attribute and gather all necessary info
        IncrementalValuesProvider<D2D1PixelShaderSourceInfo> methodInfo =
            context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "ComputeSharp.D2D1.D2DPixelShaderSourceAttribute",
                static (node, _) => node is MethodDeclarationSyntax,
                static (context, token) =>
                {
                    MethodDeclarationSyntax methodDeclaration = (MethodDeclarationSyntax)context.TargetNode;
                    IMethodSymbol methodSymbol = (IMethodSymbol)context.TargetSymbol;

                    using ImmutableArrayBuilder<DiagnosticInfo> diagnostics = new();

                    // Get the remaining info for the current shader
                    ImmutableArray<ushort> modifiers = methodDeclaration.Modifiers.Select(token => (ushort)token.Kind()).ToImmutableArray();
                    string methodName = methodSymbol.Name;
                    string? invalidReturnType = Execute.GetInvalidReturnType(diagnostics, methodSymbol);
                    string hlslSource = Execute.GetHlslSource(diagnostics, methodSymbol);
                    D2D1ShaderProfile shaderProfile = Execute.GetShaderProfile(diagnostics, methodSymbol);
                    D2D1CompileOptions compileOptions = Execute.GetCompileOptions(diagnostics, methodSymbol);
                    bool isCompilationEnabled = diagnostics.Count == 0;

                    token.ThrowIfCancellationRequested();

                    // Prepare the key to cache the bytecode (just like the main D2D1 generator)
                    HlslBytecodeInfoKey hlslInfoKey = new(
                        hlslSource,
                        shaderProfile,
                        compileOptions,
                        isCompilationEnabled);

                    // Get the existing compiled shader, or compile the processed HLSL code
                    HlslBytecodeInfo hlslInfo = HlslBytecode.GetInfo(ref hlslInfoKey, token);

                    token.ThrowIfCancellationRequested();

                    // Append any diagnostic for the shader compilation
                    Execute.GetInfoDiagnostics(methodSymbol, hlslInfo, diagnostics);

                    token.ThrowIfCancellationRequested();

                    // Finally, get the hierarchy too
                    HierarchyInfo hierarchyInfo = HierarchyInfo.From(methodSymbol.ContainingType!);

                    token.ThrowIfCancellationRequested();

                    return new D2D1PixelShaderSourceInfo(
                        Hierarchy: hierarchyInfo,
                        Modifiers: modifiers,
                        MethodName: methodName,
                        InvalidReturnType: invalidReturnType,
                        HlslInfoKey: hlslInfoKey,
                        HlslInfo: hlslInfo,
                        Diagnostcs: diagnostics.ToImmutable());
                });

        IncrementalValuesProvider<EquatableArray<DiagnosticInfo>> diagnosticInfo = methodInfo.Select(static (item, _) => item.Diagnostcs);
        IncrementalValuesProvider<D2D1PixelShaderSourceInfo> outputInfo = methodInfo.Select(static (item, _) => item with { Diagnostcs = default });

        // Output the diagnostics, if any
        context.ReportDiagnostics(diagnosticInfo);

        // Generate the shader bytecode methods
        context.RegisterSourceOutput(outputInfo, static (context, item) =>
        {
            using IndentedTextWriter writer = new();

            item.Hierarchy.WriteSyntax(
                state: item,
                writer: writer,
                baseTypes: [],
                memberCallbacks: new IndentedTextWriter.Callback<D2D1PixelShaderSourceInfo>[] { Execute.WriteSyntax });

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{item.MethodName}.g.cs", writer.ToString());
        });
    }
}