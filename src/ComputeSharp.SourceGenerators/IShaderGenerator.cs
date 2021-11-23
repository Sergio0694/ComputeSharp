using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A source generator creating data loaders for <see cref="IComputeShader"/> and <see cref="IPixelShader{TPixel}"/> types.
/// </summary>
[Generator]
public sealed partial class IShaderGenerator : ISourceGenerator
{
    /// <inheritdoc/>
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(static () => new SyntaxReceiver());
    }

    /// <inheritdoc/>
    public void Execute(GeneratorExecutionContext context)
    {
        // Get the syntax receiver with the candidate nodes
        if (context.SyntaxContextReceiver is not SyntaxReceiver syntaxReceiver)
        {
            return;
        }

        foreach (SyntaxReceiver.Item item in syntaxReceiver.GatheredInfo)
        {
            try
            {
                OnExecute(context, item.StructDeclaration, item.StructSymbol);
            }
            catch
            {
                context.ReportDiagnostic(IShaderGeneratorError, item.StructDeclaration, item.StructSymbol);
            }
        }
    }

    /// <summary>
    /// Processes a given target type.
    /// </summary>
    /// <param name="context">The input <see cref="GeneratorExecutionContext"/> instance to use.</param>
    /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
    /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
    private static void OnExecute(GeneratorExecutionContext context, StructDeclarationSyntax structDeclaration, INamedTypeSymbol structDeclarationSymbol)
    {
        // Create the required interface methods for the current shader type
        MethodDeclarationSyntax getDispatchIdMethod = CreateGetDispatchIdMethod(structDeclarationSymbol, out bool isDynamicShader);
        MethodDeclarationSyntax loadDispatchDataMethod = CreateLoadDispatchDataMethod(context, structDeclarationSymbol, out var discoveredResources, out int root32BitConstants);
        MethodDeclarationSyntax buildHlslStringMethod = CreateBuildHlslStringMethod(context, structDeclaration, structDeclarationSymbol, out string? implicitTextureType, out bool isSamplerUsed, out string hlslSource);
        MethodDeclarationSyntax loadDispatchMetadataMethod = CreateLoadDispatchMetadataMethod(implicitTextureType, discoveredResources, root32BitConstants, isSamplerUsed);
        MethodDeclarationSyntax tryGetBytecodeMethod = CreateTryGetBytecodeMethod(context, structDeclaration, structDeclarationSymbol, isDynamicShader, hlslSource);

        // Reorder the method declarations to respect the order in the interface definition
        MethodDeclarationSyntax[] methods =
        {
            getDispatchIdMethod,
            loadDispatchDataMethod,
            loadDispatchMetadataMethod,
            buildHlslStringMethod,
            tryGetBytecodeMethod
        };

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

        // Add [SkipLocalsInit] if the target project allows it and can access it
        if (context.Compilation.Options is CSharpCompilationOptions { AllowUnsafe: true } &&
            context.Compilation.GetTypeByMetadataName("System.Runtime.CompilerServices.SkipLocalsInit") is not null)
        {
            attributes.Add(AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Runtime.CompilerServices.SkipLocalsInit")))));
        }

        string namespaceName = structDeclarationSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces));
        string structName = structDeclaration.Identifier.Text;
        SyntaxTokenList structModifiers = structDeclaration.Modifiers;

        // Create the partial shader type declaration with the hashcode interface method implementation.
        // This code produces a struct declaration as follows:
        //
        // public <MODIFIERS> struct ShaderType
        // {
        //     <METHODS_LIST>
        // }
        var structDeclarationSyntax =
            StructDeclaration(structName)
                .WithModifiers(structModifiers)
                .AddMembers(methods.Select(m => m.AddAttributeLists(attributes.ToArray())).ToArray());

        TypeDeclarationSyntax typeDeclarationSyntax = structDeclarationSyntax;

        // Add all parent types in ascending order, if any
        foreach (var parentType in structDeclaration.Ancestors().OfType<TypeDeclarationSyntax>())
        {
            typeDeclarationSyntax = parentType
                .WithMembers(SingletonList<MemberDeclarationSyntax>(typeDeclarationSyntax))
                .WithConstraintClauses(List<TypeParameterConstraintClauseSyntax>())
                .WithBaseList(null)
                .WithAttributeLists(List<AttributeListSyntax>())
                .WithoutTrivia();
        }

        // Create a static method to create the combined hashcode for a given shader type.
        // This code takes a block syntax and produces a compilation unit as follows:
        //
        // #pragma warning disable
        //
        // namespace <SHADER_NAMESPACE>;
        // 
        // <SHADER_DECLARATION>
        var source =
            CompilationUnit().AddMembers(
            FileScopedNamespaceDeclaration(IdentifierName(namespaceName)).AddMembers(typeDeclarationSyntax)
            .WithNamespaceKeyword(Token(TriviaList(Trivia(PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true))), SyntaxKind.NamespaceKeyword, TriviaList())))
            .NormalizeWhitespace(eol: "\n")
            .ToFullString();

        // Add the method source attribute
        context.AddSource(structDeclarationSymbol.GetGeneratedFileName(), SourceText.From(source, Encoding.UTF8));
    }

    /// <summary>
    /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>GetDispatchId</c> method.
    /// </summary>
    /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the input struct declaration.</param>
    /// <param name="isDynamicShader">Indicates whether or not the shader is dynamic (ie. captures delegates).</param>
    /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>GetDispatchId</c> method.</returns>
    private static partial MethodDeclarationSyntax CreateGetDispatchIdMethod(INamedTypeSymbol structDeclarationSymbol, out bool isDynamicShader);

    /// <summary>
    /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.
    /// </summary>
    /// <param name="context">The input <see cref="GeneratorExecutionContext"/> instance to use.</param>
    /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the input struct declaration.</param>
    /// <param name="discoveredResources">The collection of discovered resources in the current shader type.</param>
    /// <param name="root32BitConstantsCount">The total number of 32 bit root constants being loaded for the current shader type.</param>
    /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchDataMethod</c> method.</returns>
    private static partial MethodDeclarationSyntax CreateLoadDispatchDataMethod(
        GeneratorExecutionContext context,
        INamedTypeSymbol structDeclarationSymbol,
        out IReadOnlyCollection<string> discoveredResources,
        out int root32BitConstantsCount);

    /// <summary>
    /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslString</c> method.
    /// </summary>
    /// <param name="context">The input <see cref="GeneratorExecutionContext"/> instance to use.</param>
    /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
    /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
    /// <param name="implicitTextureType">The implicit texture type, if available (if the shader is a pixel shader).</param>
    /// <param name="isSamplerUsed">Whether or not the current shader type requires a static sampler to be available.</param>
    /// <param name="hlslSource">The generated HLSL source code (ignoring captured delegates, if present).</param>
    /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslString</c> method.</returns>
    private static partial MethodDeclarationSyntax CreateBuildHlslStringMethod(
        GeneratorExecutionContext context,
        StructDeclarationSyntax structDeclaration,
        INamedTypeSymbol structDeclarationSymbol,
        out string? implicitTextureType,
        out bool isSamplerUsed,
        out string hlslSource);

    /// <summary>
    /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchMetadata</c> method.
    /// </summary>
    /// <param name="implicitTextureType">The implicit texture type, if available (if the shader is a pixel shader).</param>
    /// <param name="discoveredResources">The collection of resources used by the shader.</param>
    /// <param name="root32BitConstantsCount">The total number of 32 bit root constants being loaded for the shader.</param>
    /// <param name="isSamplerUsed">Whether or not the shader requires a static sampler to be available.</param>
    /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>LoadDispatchMetadata</c> method.</returns>
    private static partial MethodDeclarationSyntax CreateLoadDispatchMetadataMethod(
        string? implicitTextureType,
        IReadOnlyCollection<string> discoveredResources,
        int root32BitConstantsCount,
        bool isSamplerUsed);

    /// <summary>
    /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>TryGetBytecode</c> method.
    /// </summary>
    /// <param name="context">The input <see cref="GeneratorExecutionContext"/> instance to use.</param>
    /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> node to process.</param>
    /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for <paramref name="structDeclaration"/>.</param>
    /// <param name="isDynamicShader">Indicates whether or not the shader is dynamic (ie. captures delegates).</param>
    /// <param name="hlslSource">The generated HLSL source code (ignoring captured delegates, if present).</param>
    /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslString</c> method.</returns>
    private static partial MethodDeclarationSyntax CreateTryGetBytecodeMethod(
        GeneratorExecutionContext context,
        StructDeclarationSyntax structDeclaration,
        INamedTypeSymbol structDeclarationSymbol,
        bool isDynamicShader,
        string hlslSource);
}
