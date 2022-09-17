using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A source generator compiling methods annotated with <see cref="D2DPixelShaderSourceAttribute"/>.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class D2DPixelShaderSourceGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Get all method declarations with the [D2DPixelShaderSource] attribute and gather all necessary info
        IncrementalValuesProvider<D2D1PixelShaderSourceInfo> shaderInfoWithErrors =
            context.SyntaxProvider
            .ForAttributeWithMetadataName(
                typeof(D2DPixelShaderSourceAttribute).FullName,
                static (node, _) => node is MethodDeclarationSyntax,
                static (context, token) =>
                {
                    MethodDeclarationSyntax methodDeclaration = (MethodDeclarationSyntax)context.TargetNode;
                    IMethodSymbol methodSymbol = (IMethodSymbol)context.TargetSymbol;

                    ImmutableArray<DiagnosticInfo>.Builder diagnostics = ImmutableArray.CreateBuilder<DiagnosticInfo>();

                    // Get the processed HLSL first and check for cancellation
                    string hlslSource = Execute.GetHlslSource(diagnostics, methodSymbol);

                    token.ThrowIfCancellationRequested();

                    // Get the remaining info for the current shader
                    ImmutableArray<SyntaxKind> modifiers = methodDeclaration.Modifiers.Select(token => token.Kind()).ToImmutableArray();
                    string methodName = methodSymbol.Name;
                    string? invalidReturnType = Execute.GetInvalidReturnType(diagnostics, methodSymbol);
                    D2D1ShaderProfile shaderProfile = Execute.GetShaderProfile(diagnostics, methodSymbol);
                    D2D1CompileOptions compileOptions = Execute.GetCompileOptions(diagnostics, methodSymbol);

                    return new D2D1PixelShaderSourceInfo(
                        Hierarchy: HierarchyInfo.From(methodSymbol.ContainingType!),
                        HlslShaderMethodSource: new HlslShaderMethodSourceInfo(
                            modifiers,
                            methodName,
                            invalidReturnType,
                            hlslSource,
                            shaderProfile,
                            compileOptions,
                            HasErrors: diagnostics.Count > 0),
                        Diagnostcs: diagnostics.ToImmutable());
                });

        // Output the diagnostics
        context.ReportDiagnostics(
            shaderInfoWithErrors
            .Select(static (item, _) => item.Diagnostcs)
            .WithComparer(EqualityComparer<DiagnosticInfo>.Default.ForImmutableArray()));

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeMethodInfo BytecodeInfo, DeferredDiagnosticInfo? Diagnostic)> embeddedBytecodeWithErrors =
            shaderInfoWithErrors
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = Execute.GetBytecode(
                    item.HlslShaderMethodSource,
                    token,
                    out DeferredDiagnosticInfo? diagnostic);

                token.ThrowIfCancellationRequested();

                EmbeddedBytecodeMethodInfo bytecodeInfo = new(
                    item.HlslShaderMethodSource.Modifiers,
                    item.HlslShaderMethodSource.MethodName,
                    item.HlslShaderMethodSource.InvalidReturnType,
                    item.HlslShaderMethodSource.HlslSource,
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

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeMethodInfo BytecodeInfo)> embeddedBytecode =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.BytecodeInfo));

        // Generate the shader bytecode methods
        context.RegisterSourceOutput(embeddedBytecode, static (context, item) =>
        {
            MethodDeclarationSyntax loadBytecodeMethod = Execute.GetSyntax(item.BytecodeInfo, out Func<SyntaxNode, SourceText> fixup);
            CompilationUnitSyntax compilationUnit = item.Hierarchy.GetSyntax(loadBytecodeMethod);
            SourceText text = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FilenameHint}.{item.BytecodeInfo.MethodName}.g.cs", text);
        });
    }
}
