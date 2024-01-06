using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Constants;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
                    string? invalidReturnType = Execute.GetInvalidReturnType(methodSymbol);
                    string hlslSource = Execute.GetHlslSource(diagnostics, methodSymbol);
                    D2D1CompileOptions compileOptions = Execute.GetCompileOptions(diagnostics, methodSymbol);

                    // For the shader profile, reuse the same logic as in D2D1PixelShaderDescriptorGenerator
                    D2D1ShaderProfile? requestedShaderProfile = Execute.GetShaderProfile(methodSymbol);
                    D2D1ShaderProfile effectiveShaderProfile = requestedShaderProfile ?? D2D1ShaderProfile.PixelShader50;
                    bool isCompilationEnabled = requestedShaderProfile is not null;

                    token.ThrowIfCancellationRequested();

                    // Prepare the key to cache the bytecode (just like the main D2D1 generator)
                    HlslBytecodeInfoKey hlslInfoKey = new(
                        hlslSource,
                        effectiveShaderProfile,
                        compileOptions,
                        isCompilationEnabled);

                    // Get the existing compiled shader, or compile the processed HLSL code
                    HlslBytecodeInfo hlslInfo = HlslBytecodeSyntaxProcessor.GetInfo(ref hlslInfoKey, token);

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
                })
            .WithTrackingName(WellKnownTrackingNames.Execute);

        // Get the diagnostic incremental collection with filtering (same as in the D2D1 generator)
        IncrementalValuesProvider<EquatableArray<DiagnosticInfo>> diagnosticInfo =
            methodInfo
            .Select(static (item, _) => item.Diagnostcs)
            .WithTrackingName(WellKnownTrackingNames.Diagnostics)
            .Where(static item => !item.IsEmpty);

        // Also get the comparison-friendly incremental collection for the output (same as above)
        IncrementalValuesProvider<D2D1PixelShaderSourceInfo> outputInfo =
            methodInfo
            .Select(static (item, _) => item with { Diagnostcs = default })
            .WithTrackingName(WellKnownTrackingNames.Output);

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
                memberCallbacks: [Execute.WriteSyntax]);

            context.AddSource($"{item.Hierarchy.FullyQualifiedMetadataName}.{item.MethodName}.g.cs", writer.ToString());
        });
    }
}