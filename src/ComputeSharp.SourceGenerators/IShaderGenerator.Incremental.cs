using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A source generator creating data loaders for <see cref="IComputeShader"/> and <see cref="IPixelShader{TPixel}"/> types.
/// </summary>
[Generator]
public sealed partial class IShaderGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Check whether [SkipLocalsInit] can be used
        IncrementalValueProvider<bool> canUseSkipLocalsInit =
            context.CompilationProvider
            .Select(static (compilation, token) =>
                compilation.Options is CSharpCompilationOptions { AllowUnsafe: true } &&
                compilation.GetTypeByMetadataName("System.Runtime.CompilerServices.SkipLocalsInit") is not null);

        // Get all declared struct types and their type symbols
        IncrementalValuesProvider<(StructDeclarationSyntax Syntax, INamedTypeSymbol Symbol)> structDeclarations =
            context.SyntaxProvider.CreateSyntaxProvider(
                static (node, token) => node is StructDeclarationSyntax structDeclaration,
                static (context, token) => (
                    (StructDeclarationSyntax)context.Node,
                    Symbol: (INamedTypeSymbol?)context.SemanticModel.GetDeclaredSymbol(context.Node, token)))
            .Where(static pair => pair.Symbol is not null)!;

        // Get the symbol for the IComputeShader interface
        IncrementalValueProvider<INamedTypeSymbol> iComputeShaderSymbol =
            context.CompilationProvider
            .Select(static (compilation, token) => compilation.GetTypeByMetadataName(typeof(IComputeShader).FullName))!;

        // Get the symbol for the IPixelShader<TPixel> interface
        IncrementalValueProvider<INamedTypeSymbol> iPixelShaderSymbol =
            context.CompilationProvider
            .Select(static (compilation, token) => compilation.GetTypeByMetadataName(typeof(IPixelShader<>).FullName))!;

        // Filter struct declarations that implement a shader interface, and also gather the hierarchy info and shader type
        IncrementalValuesProvider<(StructDeclarationSyntax Syntax, INamedTypeSymbol Symbol, HierarchyInfo Hierarchy, Type Type)> shaderDeclarations =
            structDeclarations
            .Combine(iComputeShaderSymbol.Combine(iPixelShaderSymbol))
            .Select(static (item, token) => (item.Left, Type: GetShaderType(item.Left.Symbol, item.Right.Left, item.Right.Right)))
            .Where(static item => item.Type is not null)
            .Select(static (item, token) => (item.Left.Syntax, item.Left.Symbol, HierarchyInfo.From(item.Left.Symbol), item.Type!));

        // Get the hierarchy info and delegate field names for each shader type
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchIdInfo Info)> hierarchyAndDispatchIdInfo =
            shaderDeclarations
            .Select(static (item, token) => (item.Hierarchy, new DispatchIdInfo(GetDelegateFieldNames(item.Symbol))))
            .WithComparers(HierarchyInfo.Comparer.Default, DispatchIdInfo.Comparer.Default);

        // Generate the GetDispatchId() methods
        context.RegisterSourceOutput(hierarchyAndDispatchIdInfo, static (context, item) =>
        {
            MethodDeclarationSyntax getDispatchIdMethod = CreateGetDispatchIdMethod(item.Info.Delegates);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, getDispatchIdMethod, false);

            context.AddSource($"{item.Hierarchy.FilenameHint}.GetDispatchId", SourceText.From(compilationUnit.ToFullString(), Encoding.UTF8));
        });

        // Get the [EmbeddedBytecode] attribute
        IncrementalValueProvider<INamedTypeSymbol> embeddedBytecodeSymbol =
            context.CompilationProvider
            .Select(static (compilation, token) => compilation.GetTypeByMetadataName(typeof(EmbeddedBytecodeAttribute).FullName)!);

        // Get the dispatch data, HLSL source and embedded bytecode info. This info is computed on the
        // same step as parts are shared in following sub-branches in the incremental generator pipeline.
        IncrementalValuesProvider<(Result<DispatchDataInfo> Dispatch, Result<HlslSourceInfo> Hlsl, Result<ThreadIdsInfo> ThreadIds)> shaderInfoWithErrors =
            shaderDeclarations
            .Combine(context.CompilationProvider)
            .Combine(embeddedBytecodeSymbol)
            .Select(static (item, token) =>
            {
                // GetDispatchId() data
                ImmutableArray<FieldInfo> fieldInfos = GetCapturedFieldInfos(
                    item.Left.Left.Symbol,
                    item.Left.Left.Type,
                    out int resourceCount,
                    out int root32BitConstantCount,
                    out ImmutableArray<Diagnostic> dispatchDataDiagnostics);

                Result<DispatchDataInfo> dispatchDataInfo = new(new DispatchDataInfo(
                    item.Left.Left.Hierarchy,
                    item.Left.Left.Type,
                    fieldInfos,
                    resourceCount,
                    root32BitConstantCount),
                    dispatchDataDiagnostics);

                // BuildHlslString() info
                HlslSourceInfo hlslSourceInfo = GetNonDynamicHlslSourceInfo(
                    item.Left.Right,
                    item.Left.Left.Syntax,
                    item.Left.Left.Symbol,
                    out ImmutableArray<Diagnostic> hlslSourceDiagnostics);

                // TryGetBytecode() info
                ThreadIdsInfo threadIds = GetThreadIdsForEmbeddedShader(
                    item.Right,
                    item.Left.Left.Symbol,
                    !hlslSourceInfo.Delegates.IsEmpty,
                    out ImmutableArray<Diagnostic> threadIdsDiagnostics);

                return (
                    dispatchDataInfo,
                    new Result<HlslSourceInfo>(hlslSourceInfo, hlslSourceDiagnostics),
                    new Result<ThreadIdsInfo>(threadIds, threadIdsDiagnostics));
            });

        // Output the diagnostics
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Dispatch.Errors));
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.Hlsl.Errors));
        context.ReportDiagnostics(shaderInfoWithErrors.Select(static (item, token) => item.ThreadIds.Errors));

        // Filter all items to enable caching at a coarse level, and remove diagnostics
        IncrementalValuesProvider<(DispatchDataInfo Dispatch, HlslSourceInfo Hlsl, ThreadIdsInfo ThreadIds)> shaderInfo =
            shaderInfoWithErrors
            .Select(static (item, token) => (item.Dispatch.Value, item.Hlsl.Value, item.ThreadIds.Value))
            .WithComparers(DispatchDataInfo.Comparer.Default, HlslSourceInfo.Comparer.Default, ThreadIdsInfo.Comparer.Default);

        // Get a filtered sequence to enable caching
        IncrementalValuesProvider<DispatchDataInfo> dispatchDataInfo =
            shaderInfo
            .Select(static (item, token) => item.Dispatch)
            .WithComparer(DispatchDataInfo.Comparer.Default);

        // Generate the LoadDispatchData() methods
        context.RegisterSourceOutput(dispatchDataInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax loadDispatchDataMethod = CreateLoadDispatchDataMethod(
                item.Left.Type,
                item.Left.FieldInfos,
                item.Left.ResourceCount,
                item.Left.Root32BitConstantCount);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, loadDispatchDataMethod, item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.LoadDispatchData", SourceText.From(compilationUnit.ToFullString(), Encoding.UTF8));
        }); 

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, HlslSourceInfo SourceInfo)> hlslSourceInfo =
            shaderInfo
            .Select(static (item, token) => (item.Dispatch.Hierarchy, item.Hlsl))
            .WithComparers(HierarchyInfo.Comparer.Default, HlslSourceInfo.Comparer.Default);

        // Generate the BuildHlslString() methods
        context.RegisterSourceOutput(hlslSourceInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = CreateBuildHlslStringMethod(item.Left.SourceInfo);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, buildHlslStringMethod, item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.BuildHlslString", SourceText.From(compilationUnit.ToFullString(), Encoding.UTF8));
        });

        // Get the dispatch metadata info
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, DispatchMetadataInfo MetadataInfo)> dispatchMetadataInfo =
            shaderInfo
            .Select(static (item, token) => (
                item.Dispatch.Hierarchy,
                GetDispatchMetadataInfo(
                    item.Dispatch.Root32BitConstantCount,
                    item.Hlsl.ImplicitTextureType is not null,
                    item.Hlsl.IsSamplerUsed,
                    item.Dispatch.FieldInfos)))
            .WithComparers(HierarchyInfo.Comparer.Default, DispatchMetadataInfo.Comparer.Default);

        // Generate the LoadDispatchMetadata() methods
        context.RegisterSourceOutput(dispatchMetadataInfo.Combine(canUseSkipLocalsInit), static (context, item) =>
        {
            MethodDeclarationSyntax buildHlslStringMethod = CreateLoadDispatchMetadataMethod(item.Left.MetadataInfo);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Left.Hierarchy, buildHlslStringMethod, item.Right);

            context.AddSource($"{item.Left.Hierarchy.FilenameHint}.LoadDispatchMetadata", SourceText.From(compilationUnit.ToFullString(), Encoding.UTF8));
        });

        // Transform the raw HLSL source to compile
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, string Hlsl, ThreadIdsInfo ThreadIds)> shaderBytecodeInfo =
            shaderInfo
            .Select(static (item, token) => (item.Dispatch.Hierarchy, GetNonDynamicHlslSource(item.Hlsl), item.ThreadIds))
            .WithComparers(HierarchyInfo.Comparer.Default, EqualityComparer<string>.Default, ThreadIdsInfo.Comparer.Default);

        // Compile the requested shader bytecodes
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, Result<EmbeddedBytecodeInfo> BytecodeInfo)> embeddedBytecodeWithErrors =
            shaderBytecodeInfo
            .Select(static (item, token) =>
            {
                ImmutableArray<byte> bytecode = GetShaderBytecode(item.ThreadIds, item.Hlsl, out ImmutableArray<Diagnostic> diagnostics);

                EmbeddedBytecodeInfo bytecodeInfo = new(
                    item.ThreadIds.X,
                    item.ThreadIds.Y,
                    item.ThreadIds.Z,
                    bytecode);

                return (item.Hierarchy, new Result<EmbeddedBytecodeInfo>(bytecodeInfo, diagnostics));
            });

        // Output the diagnostics
        context.ReportDiagnostics(embeddedBytecodeWithErrors.Select(static (item, token) => item.BytecodeInfo.Errors));

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, EmbeddedBytecodeInfo BytecodeInfo)> embeddedBytecode =
            embeddedBytecodeWithErrors
            .Select(static (item, token) => (item.Hierarchy, item.BytecodeInfo.Value))
            .WithComparers(HierarchyInfo.Comparer.Default, EmbeddedBytecodeInfo.Comparer.Default);

        // Generate the TryGetBytecode() methods
        context.RegisterSourceOutput(embeddedBytecode, static (context, item) =>
        {
            MethodDeclarationSyntax tryGetBytecodeMethod = CreateTryGetBytecodeMethod(item.BytecodeInfo, out Func<SyntaxNode, string> fixup);
            CompilationUnitSyntax compilationUnit = GetCompilationUnitFromMethod(item.Hierarchy, tryGetBytecodeMethod, false);
            string text = fixup(compilationUnit);

            context.AddSource($"{item.Hierarchy.FilenameHint}.TryGetBytecode", SourceText.From(text, Encoding.UTF8));
        });
    }

    /// <summary>
    /// Gets the shader type for a given shader, if any.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to check.</param>
    /// <param name="iComputeShaderSymbol">The <see cref="INamedTypeSymbol"/> instance for <see cref="IComputeShader"/>.</param>
    /// <param name="iPixelShaderSymbol">The <see cref="INamedTypeSymbol"/> instance for <see cref="IPixelShader{TPixel}"/>.</param>
    /// <returns>Either <see cref="IComputeShader"/> or <see cref="IPixelShader{TPixel}"/>, or <see langword="null"/>.</returns>
    [Pure]
    private static Type? GetShaderType(
        INamedTypeSymbol typeSymbol,
        INamedTypeSymbol iComputeShaderSymbol,
        INamedTypeSymbol iPixelShaderSymbol)
    {
        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.Interfaces)
        {
            if (interfaceSymbol.Name == nameof(IComputeShader) &&
                 SymbolEqualityComparer.Default.Equals(interfaceSymbol, iComputeShaderSymbol))
            {
                return typeof(IComputeShader);
            }

            if (interfaceSymbol.IsGenericType &&
                interfaceSymbol.Name == nameof(IPixelShader<byte>) &&
                SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, iPixelShaderSymbol))
            {
                return typeof(IPixelShader<>);
            }
        }

        return null;
    }
    
    /// <summary>
    /// Creates a <see cref="CompilationUnitSyntax"/> instance wrapping the given method.
    /// </summary>
    /// <param name="hierarchyInfo">The <see cref="HierarchyInfo"/> instance for the current type.</param>
    /// <param name="methodDeclaration">The <see cref="MethodDeclarationSyntax"/> item to insert.</param>
    /// <param name="canUseSkipLocalsInit">Whether <c>[SkipLocalsInit]</c> can be used.</param>
    /// <returns>A <see cref="CompilationUnitSyntax"/> object wrapping <paramref name="methodDeclaration"/>.</returns>
    [Pure]
    private static CompilationUnitSyntax GetCompilationUnitFromMethod(
        HierarchyInfo hierarchyInfo,
        MethodDeclarationSyntax methodDeclaration,
        bool canUseSkipLocalsInit)
    {
        // Method attributes
        List<AttributeListSyntax> attributes = new()
        {
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(IShaderGenerator).FullName))),
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(IShaderGenerator).Assembly.GetName().Version.ToString())))))),
            AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
            AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage")))),
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.ComponentModel.EditorBrowsable")).AddArgumentListArguments(
                AttributeArgument(ParseExpression("global::System.ComponentModel.EditorBrowsableState.Never"))))),
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.Obsolete")).AddArgumentListArguments(
                AttributeArgument(LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Literal("This method is not intended to be used directly by user code"))))))
        };

        // Add [SkipLocalsInit] if the target project allows it
        if (canUseSkipLocalsInit)
        {
            attributes.Add(AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Runtime.CompilerServices.SkipLocalsInit")))));
        }

        // Create the partial shader type declaration with the given method implementation.
        // This code produces a struct declaration as follows:
        //
        // partial struct <SHADER_TYPE>
        // {
        //     <METHOD>
        // }
        StructDeclarationSyntax structDeclarationSyntax =
            StructDeclaration(hierarchyInfo.Names[0])
            .AddModifiers(Token(SyntaxKind.PartialKeyword))
            .AddMembers(methodDeclaration.AddAttributeLists(attributes.ToArray()));

        TypeDeclarationSyntax typeDeclarationSyntax = structDeclarationSyntax;

        // Add all parent types in ascending order, if any
        foreach (string parentType in hierarchyInfo.Names.AsSpan().Slice(1))
        {
            typeDeclarationSyntax =
                ClassDeclaration(parentType)
                .AddModifiers(Token(SyntaxKind.PartialKeyword))
                .AddMembers(typeDeclarationSyntax);
        }

        // Create a static method to create the combined hashcode for a given shader type.
        // This code takes a block syntax and produces a compilation unit as follows:
        //
        // #pragma warning disable
        //
        // namespace <NAMESPACE>;
        // 
        // <TYPE_HIERARCHY>
        return
            CompilationUnit().AddMembers(
            FileScopedNamespaceDeclaration(IdentifierName(hierarchyInfo.Namespace))
            .AddMembers(typeDeclarationSyntax)
            .WithNamespaceKeyword(Token(TriviaList(
                Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))),
                SyntaxKind.NamespaceKeyword,
                TriviaList())))
            .NormalizeWhitespace(eol: "\n");
    }
}
