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
        // Get all method declarations with the [D2DPixelShaderSource] attribute
        IncrementalValuesProvider<IMethodSymbol> methodSymbols =
            context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => node is MethodDeclarationSyntax { Parent: ClassDeclarationSyntax, AttributeLists.Count: > 0 },
                static (context, _) => (IMethodSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!)
            .Where(static symbol => symbol.HasAttributeWithFullyQualifiedName("ComputeSharp.D2D1.D2DPixelShaderSourceAttribute"));

        // Gather info for all annotated methods
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslShaderSourceInfo Source, ImmutableArray<Diagnostic> Diagnostics)> shaderInfoWithErrors =
            methodSymbols
            .Select(static (item, _) =>
            {
                ImmutableArray<Diagnostic>.Builder diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();

                // Get all necessary info for the current shader
                string? hlslSource = Execute.GetHlslSource(diagnostics, item) ?? "";
                D2D1ShaderProfile? shaderProfile = Execute.GetShaderProfile(diagnostics, item);
                D2D1CompileOptions? compileOptions = Execute.GetCompileOptions(diagnostics, item);

                HlslShaderSourceInfo sourceInfo = new(
                    hlslSource,
                    shaderProfile,
                    compileOptions,
                    IsLinkingSupported: false,
                    HasErrors: diagnostics.Count > 0);

                HierarchyInfo hierarchy = HierarchyInfo.From(item.ContainingType!);

                return (hierarchy, sourceInfo, diagnostics.ToImmutable());
            });

        // Output the diagnostics
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, _) => item.Diagnostics));

        // Filter all items to enable caching at a coarse level, and remove diagnostics
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslShaderSourceInfo Source)> shaderInfo =
            shaderInfoWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.Source))
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<HlslShaderSourceInfo>.Default);

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo, DiagnosticInfo? Diagnostic)> embeddedBytecodeWithErrors =
            shaderInfo
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = Execute.GetBytecode(
                    item.Source,
                    token,
                    out DiagnosticInfo? diagnostic);

                token.ThrowIfCancellationRequested();

                EmbeddedBytecodeInfo bytecodeInfo = new(
                    item.Source.HlslSource,
                    item.Source.ShaderProfile,
                    item.Source.CompileOptions,
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

        // Generate the shader bytecode methods
        context.RegisterSourceOutput(embeddedBytecode, static (context, item) =>
        {
            MethodDeclarationSyntax loadBytecodeMethod = Execute.GetSyntax(item.BytecodeInfo, out Func<SyntaxNode, SourceText> fixup);
            CompilationUnitSyntax compilationUnit = item.Hierarchy.GetSyntax(loadBytecodeMethod);
            SourceText text = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FilenameHint}.TESTMETHOD", text);
        });
    }
}
