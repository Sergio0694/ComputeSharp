using System;
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
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslShaderMethodSourceInfo Source, ImmutableArray<Diagnostic> Diagnostics)> shaderInfoWithErrors =
            context.SyntaxProvider
            .ForAttributeWithMetadataName(
                typeof(D2DPixelShaderSourceAttribute).FullName,
                static (node, _) => node is MethodDeclarationSyntax,
                static (context, _) =>
                {
                    MethodDeclarationSyntax methodDeclaration = (MethodDeclarationSyntax)context.TargetNode;
                    IMethodSymbol methodSymbol = (IMethodSymbol)context.TargetSymbol;

                    ImmutableArray<Diagnostic>.Builder diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();

                    // Get all necessary info for the current shader
                    ImmutableArray<SyntaxKind> modifiers = methodDeclaration.Modifiers.Select(token => token.Kind()).ToImmutableArray();
                    string methodName = methodSymbol.Name;
                    string? invalidReturnType = Execute.GetInvalidReturnType(diagnostics, methodSymbol);
                    string hlslSource = Execute.GetHlslSource(diagnostics, methodSymbol);
                    D2D1ShaderProfile shaderProfile = Execute.GetShaderProfile(diagnostics, methodSymbol);
                    D2D1CompileOptions compileOptions = Execute.GetCompileOptions(diagnostics, methodSymbol);

                    HlslShaderMethodSourceInfo sourceInfo = new(
                        modifiers,
                        methodName,
                        invalidReturnType,
                        hlslSource,
                        shaderProfile,
                        compileOptions,
                        HasErrors: diagnostics.Count > 0);

                    HierarchyInfo hierarchy = HierarchyInfo.From(methodSymbol.ContainingType!);

                    return (hierarchy, sourceInfo, diagnostics.ToImmutable());
                });

        // Output the diagnostics
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, _) => item.Diagnostics));

        // Filter all items to enable caching at a coarse level, and remove diagnostics
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslShaderMethodSourceInfo Source)> shaderInfo =
            shaderInfoWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.Source));

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeMethodInfo BytecodeInfo, DiagnosticInfo? Diagnostic)> embeddedBytecodeWithErrors =
            shaderInfo
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = Execute.GetBytecode(
                    item.Source,
                    token,
                    out DiagnosticInfo? diagnostic);

                token.ThrowIfCancellationRequested();

                EmbeddedBytecodeMethodInfo bytecodeInfo = new(
                    item.Source.Modifiers,
                    item.Source.MethodName,
                    item.Source.InvalidReturnType,
                    item.Source.HlslSource,
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
