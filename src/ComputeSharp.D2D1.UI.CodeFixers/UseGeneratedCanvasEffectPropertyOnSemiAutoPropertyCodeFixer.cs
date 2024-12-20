using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp.SourceGenerators.Constants;
#else
using ComputeSharp.D2D1.WinUI.SourceGenerators.Constants;
#endif
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;
#if WINDOWS_UWP
using static ComputeSharp.D2D1.Uwp.SourceGenerators.DiagnosticDescriptors;
#else
using static ComputeSharp.D2D1.WinUI.SourceGenerators.DiagnosticDescriptors;
#endif
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.SourceGenerators;
#else
namespace ComputeSharp.D2D1.WinUI.SourceGenerators;
#endif

/// <summary>
/// A code fixer that converts manual properties into partial properties using <c>[GeneratedCanvasEffectProperty]</c>.
/// </summary>
[ExportCodeFixProvider(LanguageNames.CSharp)]
[Shared]
public sealed class UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyCodeFixer : CodeFixProvider
{
    /// <inheritdoc/>
    public override ImmutableArray<string> FixableDiagnosticIds { get; } = [UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyId];

    /// <inheritdoc/>
    public override Microsoft.CodeAnalysis.CodeFixes.FixAllProvider? GetFixAllProvider()
    {
        return new FixAllProvider();
    }

    /// <inheritdoc/>
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        Diagnostic diagnostic = context.Diagnostics[0];
        TextSpan diagnosticSpan = context.Span;

        // This code fixer needs the semantic model, so check that first
        if (!context.Document.SupportsSemanticModel)
        {
            return;
        }

        // Retrieve the properties passed by the analyzer
        if (!int.TryParse(diagnostic.Properties[UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer.InvalidationModePropertyName], out int invalidationMode))
        {
            return;
        }

        SyntaxNode? root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

        // Get the property declaration from the target diagnostic
        if (root!.FindNode(diagnosticSpan) is PropertyDeclarationSyntax propertyDeclaration)
        {
            // Get the semantic model, as we need to resolve symbols
            SemanticModel semanticModel = (await context.Document.GetSemanticModelAsync(context.CancellationToken).ConfigureAwait(false))!;

            // Register the code fix to update the semi-auto property to a partial property with [GeneratedCanvasEffectProperty]
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: "Use [GeneratedCanvasEffectProperty]",
                    createChangedDocument: token => ConvertToPartialProperty(
                        context.Document,
                        semanticModel,
                        root,
                        propertyDeclaration,
                        invalidationMode),
                    equivalenceKey: "Use [GeneratedCanvasEffectProperty]"),
                diagnostic);
        }
    }

    /// <summary>
    /// Tries to get an <see cref="AttributeListSyntax"/> for the <c>[GeneratedCanvasEffectProperty]</c> attribute.
    /// </summary>
    /// <param name="document">The original document being fixed.</param>
    /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the current compilation.</param>
    /// <param name="generatedCanvasEffectPropertyAttributeList">The resulting attribute list, if successfully retrieved.</param>
    /// <returns>Whether <paramref name="generatedCanvasEffectPropertyAttributeList"/> could be retrieved successfully.</returns>
    private static bool TryGetGeneratedCanvasEffectPropertyAttributeList(
        Document document,
        SemanticModel semanticModel,
        [NotNullWhen(true)] out AttributeListSyntax? generatedCanvasEffectPropertyAttributeList)
    {
        // Make sure we can resolve the '[GeneratedCanvasEffectProperty]' attribute
        if (semanticModel.Compilation.GetTypeByMetadataName(WellKnownTypeNames.GeneratedCanvasEffectPropertyAttribute) is not INamedTypeSymbol attributeSymbol)
        {
            generatedCanvasEffectPropertyAttributeList = null;

            return false;
        }

        SyntaxGenerator syntaxGenerator = SyntaxGenerator.GetGenerator(document);

        // Create the attribute syntax for the new '[GeneratedCanvasEffectProperty]' attribute here too
        SyntaxNode attributeTypeSyntax = syntaxGenerator.TypeExpression(attributeSymbol).WithAdditionalAnnotations(Simplifier.AddImportsAnnotation);

        generatedCanvasEffectPropertyAttributeList = (AttributeListSyntax)syntaxGenerator.Attribute(attributeTypeSyntax);

        return true;
    }

    /// <summary>
    /// Updates an <see cref="AttributeListSyntax"/> for the <c>[GeneratedCanvasEffectProperty]</c> attribute with the right default value.
    /// </summary>
    /// <param name="document">The original document being fixed.</param>
    /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the current compilation.</param>
    /// <param name="generatedCanvasEffectPropertyAttributeList">The <see cref="AttributeListSyntax"/> with the attribute to add.</param>
    /// <param name="invalidationMode">The invalidation mode to use.</param>
    /// <returns>The updated attribute syntax.</returns>
    private static AttributeListSyntax UpdateGeneratedCanvasEffectPropertyAttributeList(
        Document document,
        SemanticModel semanticModel,
        AttributeListSyntax generatedCanvasEffectPropertyAttributeList,
        int invalidationMode)
    {
        // If the invalidation mode is not the default, set it in the attribute.
        // We extract the generated attribute so we can add the new argument.
        // It's important to reuse it, as it has the "add usings" annotation.
        if (invalidationMode == 1)
        {
            // Try to resolve the attribute type, if present (this should always be the case)
            if (semanticModel.Compilation.GetTypeByMetadataName(WellKnownTypeNames.CanvasEffectInvalidationType) is INamedTypeSymbol enumTypeSymbol)
            {
                SyntaxGenerator syntaxGenerator = SyntaxGenerator.GetGenerator(document);

                // Create the identifier syntax for the enum type, with the right annotations
                SyntaxNode enumTypeSyntax = syntaxGenerator.TypeExpression(enumTypeSymbol).WithAdditionalAnnotations(Simplifier.AddImportsAnnotation);

                // Create the member access expression for the target enum type.
                // We only ever take this path for the 'Creation' invalidation mode.
                SyntaxNode enumMemberAccessExpressionSyntax = syntaxGenerator.MemberAccessExpression(enumTypeSyntax, "Creation");

                // Create the attribute argument to insert
                SyntaxNode attributeArgumentSyntax = syntaxGenerator.AttributeArgument("InvalidationMode", enumMemberAccessExpressionSyntax);

                // Actually add the argument to the existing attribute syntax
                return (AttributeListSyntax)syntaxGenerator.AddAttributeArguments(generatedCanvasEffectPropertyAttributeList, [attributeArgumentSyntax]);
            }

            // This failed... For some reason. Use the fully qualified type name as a last resort.
            return
                AttributeList(SingletonSeparatedList(
                    generatedCanvasEffectPropertyAttributeList.Attributes[0]
                    .AddArgumentListArguments(
                        AttributeArgument(ParseExpression($"global::{WellKnownTypeNames.CanvasEffectInvalidationType}.Creation"))
                        .WithNameEquals(NameEquals(IdentifierName("InvalidationMode"))))));
        }

        // If we have no custom invalidation mode, we can just reuse the attribute with no changes
        return generatedCanvasEffectPropertyAttributeList;
    }

    /// <summary>
    /// Applies the code fix to a target identifier and returns an updated document.
    /// </summary>
    /// <param name="document">The original document being fixed.</param>
    /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the current compilation.</param>
    /// <param name="root">The original tree root belonging to the current document.</param>
    /// <param name="propertyDeclaration">The <see cref="PropertyDeclarationSyntax"/> for the property being updated.</param>
    /// <param name="invalidationMode">The invalidation mode to use.</param>
    /// <returns>An updated document with the applied code fix, and <paramref name="propertyDeclaration"/> being replaced with a partial property.</returns>
    private static async Task<Document> ConvertToPartialProperty(
        Document document,
        SemanticModel semanticModel,
        SyntaxNode root,
        PropertyDeclarationSyntax propertyDeclaration,
        int invalidationMode)
    {
        await Task.CompletedTask;

        // If we can't generate the new attribute list, bail (this should never happen)
        if (!TryGetGeneratedCanvasEffectPropertyAttributeList(document, semanticModel, out AttributeListSyntax? generatedCanvasEffectPropertyAttributeList))
        {
            return document;
        }

        // Create an editor to perform all mutations
        SyntaxEditor syntaxEditor = new(root, document.Project.Solution.Workspace.Services);

        ConvertToPartialProperty(
            document,
            semanticModel,
            propertyDeclaration,
            generatedCanvasEffectPropertyAttributeList,
            syntaxEditor,
            invalidationMode);

        // Create the new document with the single change
        return document.WithSyntaxRoot(syntaxEditor.GetChangedRoot());
    }

    /// <summary>
    /// Applies the code fix to a target identifier and returns an updated document.
    /// </summary>
    /// <param name="document">The original document being fixed.</param>
    /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the current compilation.</param>
    /// <param name="propertyDeclaration">The <see cref="PropertyDeclarationSyntax"/> for the property being updated.</param>
    /// <param name="generatedCanvasEffectPropertyAttributeList">The <see cref="AttributeListSyntax"/> with the attribute to add.</param>
    /// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> instance to use.</param>
    /// <param name="invalidationMode">The invalidation mode to use.</param>
    /// <returns>An updated document with the applied code fix, and <paramref name="propertyDeclaration"/> being replaced with a partial property.</returns>
    private static void ConvertToPartialProperty(
        Document document,
        SemanticModel semanticModel,
        PropertyDeclarationSyntax propertyDeclaration,
        AttributeListSyntax generatedCanvasEffectPropertyAttributeList,
        SyntaxEditor syntaxEditor,
        int invalidationMode)
    {
        // Update the attribute to insert with the invalidation mode, if needed
        generatedCanvasEffectPropertyAttributeList = UpdateGeneratedCanvasEffectPropertyAttributeList(
            document,
            semanticModel,
            generatedCanvasEffectPropertyAttributeList,
            invalidationMode);

        // Start setting up the updated attribute lists
        SyntaxList<AttributeListSyntax> attributeLists = propertyDeclaration.AttributeLists;

        if (attributeLists is [AttributeListSyntax firstAttributeListSyntax, ..])
        {
            // Remove the trivia from the original first attribute
            attributeLists = attributeLists.Replace(
                nodeInList: firstAttributeListSyntax,
                newNode: firstAttributeListSyntax.WithoutTrivia());

            // If the property has at least an attribute list, move the trivia from it to the new attribute
            generatedCanvasEffectPropertyAttributeList = generatedCanvasEffectPropertyAttributeList.WithTriviaFrom(firstAttributeListSyntax);

            // Insert the new attribute
            attributeLists = attributeLists.Insert(0, generatedCanvasEffectPropertyAttributeList);
        }
        else
        {
            // Otherwise (there are no attribute lists), transfer the trivia to the new (only) attribute list
            generatedCanvasEffectPropertyAttributeList = generatedCanvasEffectPropertyAttributeList.WithTriviaFrom(propertyDeclaration);

            // Save the new attribute list
            attributeLists = attributeLists.Add(generatedCanvasEffectPropertyAttributeList);
        }

        // Get a new property that is partial and with semicolon token accessors
        PropertyDeclarationSyntax updatedPropertyDeclaration =
            propertyDeclaration
            .AddModifiers(Token(SyntaxKind.PartialKeyword))
            .WithoutLeadingTrivia()
            .WithAttributeLists(attributeLists)
            .WithAdditionalAnnotations(Formatter.Annotation)
            .WithAccessorList(AccessorList(List(
            [
                // Keep the accessors (so we can easily keep all trivia, modifiers, attributes, etc.) but make them semicolon only
                propertyDeclaration.AccessorList!.Accessors[0]
                .WithBody(null)
                .WithExpressionBody(null)
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                .WithAdditionalAnnotations(Formatter.Annotation),
                propertyDeclaration.AccessorList!.Accessors[1]
                .WithBody(null)
                .WithExpressionBody(null)
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                .WithTrailingTrivia(propertyDeclaration.AccessorList.Accessors[1].GetTrailingTrivia())
                .WithAdditionalAnnotations(Formatter.Annotation)
            ])).WithTrailingTrivia(propertyDeclaration.AccessorList.GetTrailingTrivia()));

        syntaxEditor.ReplaceNode(propertyDeclaration, updatedPropertyDeclaration);

        // Find the parent type for the property
        TypeDeclarationSyntax typeDeclaration = propertyDeclaration.FirstAncestorOrSelf<TypeDeclarationSyntax>()!;

        // Make sure it's partial (we create the updated node in the function to preserve the updated property declaration).
        // If we created it separately and replaced it, the whole tree would also be replaced, and we'd lose the new property.
        if (!typeDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword))
        {
            syntaxEditor.ReplaceNode(typeDeclaration, static (node, generator) => generator.WithModifiers(node, generator.GetModifiers(node).WithPartial(true)));
        }
    }

    /// <summary>
    /// A custom <see cref="FixAllProvider"/> with the logic from <see cref="UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyCodeFixer"/>.
    /// </summary>
    private sealed class FixAllProvider : DocumentBasedFixAllProvider
    {
        /// <inheritdoc/>
        protected override async Task<Document?> FixAllAsync(FixAllContext fixAllContext, Document document, ImmutableArray<Diagnostic> diagnostics)
        {
            // Get the semantic model, as we need to resolve symbols
            if (await document.GetSemanticModelAsync(fixAllContext.CancellationToken).ConfigureAwait(false) is not SemanticModel semanticModel)
            {
                return document;
            }

            // Get the document root (this should always succeed)
            if (await document.GetSyntaxRootAsync(fixAllContext.CancellationToken).ConfigureAwait(false) is not SyntaxNode root)
            {
                return document;
            }

            // If we can't generate the new attribute list, bail (this should never happen)
            if (!TryGetGeneratedCanvasEffectPropertyAttributeList(document, semanticModel, out AttributeListSyntax? generatedCanvasEffectPropertyAttributeList))
            {
                return document;
            }

            // Create an editor to perform all mutations (across all edits in the file)
            SyntaxEditor syntaxEditor = new(root, fixAllContext.Solution.Services);

            foreach (Diagnostic diagnostic in diagnostics)
            {
                // Get the current property declaration for the diagnostic
                if (root.FindNode(diagnostic.Location.SourceSpan) is not PropertyDeclarationSyntax propertyDeclaration)
                {
                    continue;
                }

                // Retrieve the properties passed by the analyzer
                if (!int.TryParse(diagnostic.Properties[UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer.InvalidationModePropertyName], out int invalidationMode))
                {
                    continue;
                }

                ConvertToPartialProperty(
                    document,
                    semanticModel,
                    propertyDeclaration,
                    generatedCanvasEffectPropertyAttributeList,
                    syntaxEditor,
                    invalidationMode);
            }

            return document.WithSyntaxRoot(syntaxEditor.GetChangedRoot());
        }
    }
}