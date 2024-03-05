using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;

namespace ComputeSharp.CodeFixing;

/// <summary>
/// A code fixer that adds an attribute that is missing on a type that requires it.
/// </summary>
public abstract class MissingAttributeCodeFixer : CodeFixProvider
{
    /// <summary>
    /// The title to display for the code fixer.
    /// </summary>
    private readonly string codeActionTitle;

    /// <summary>
    /// The fully qualified type name of the attribute to add.
    /// </summary>
    private readonly string attributeFullyQualifiedMetadataName;

    /// <summary>
    /// The fully qualified type names of leading attributes to skip.
    /// </summary>
    private readonly IEnumerable<string> leadingAttributeFullyQualifiedMetadataNames;

    /// <summary>
    /// Creates a new <see cref="MissingAttributeCodeFixer"/> instance with the specified parameters.
    /// </summary>
    /// <param name="diagnosticId">The diagnostic id to trigger the code fixer.</param>
    /// <param name="codeActionTitle">The title to display for the code fixer.</param>
    /// <param name="attributeFullyQualifiedMetadataName">The fully qualified type name of the attribute to add.</param>
    /// <param name="leadingAttributeFullyQualifiedMetadataNames">The fully qualified type names of leading attributes to skip.</param>
    private protected MissingAttributeCodeFixer(
        string diagnosticId,
        string codeActionTitle,
        string attributeFullyQualifiedMetadataName,
        IEnumerable<string> leadingAttributeFullyQualifiedMetadataNames)
    {
        this.codeActionTitle = codeActionTitle;
        this.attributeFullyQualifiedMetadataName = attributeFullyQualifiedMetadataName;
        this.leadingAttributeFullyQualifiedMetadataNames = leadingAttributeFullyQualifiedMetadataNames;

        FixableDiagnosticIds = [diagnosticId];
    }

    /// <inheritdoc/>
    public sealed override ImmutableArray<string> FixableDiagnosticIds { get; }

    /// <inheritdoc/>
    public sealed override Microsoft.CodeAnalysis.CodeFixes.FixAllProvider? GetFixAllProvider()
    {
        return new FixAllProvider(this);
    }

    /// <inheritdoc/>
    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        Diagnostic diagnostic = context.Diagnostics[0];
        TextSpan diagnosticSpan = context.Span;

        SyntaxNode? root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

        // Get the struct declaration from the target diagnostic
        if (root?.FindNode(diagnosticSpan) is StructDeclarationSyntax structDeclaration)
        {
            // Register the code fix to update the return type to be Task instead
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: this.codeActionTitle,
                    createChangedDocument: token => AddMissingAttribute(context.Document, root, structDeclaration, token),
                    equivalenceKey: this.codeActionTitle),
                diagnostic);
        }
    }

    /// <summary>
    /// Applies the code fix to add the missing attribute to a target type.
    /// </summary>
    /// <param name="document">The original document being fixed.</param>
    /// <param name="root">The original tree root belonging to the current document.</param>
    /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> to update.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>An updated document with the applied code fix, and the return type of the method being <see cref="Task"/>.</returns>
    private async Task<Document> AddMissingAttribute(
        Document document,
        SyntaxNode root,
        StructDeclarationSyntax structDeclaration,
        CancellationToken cancellationToken)
    {
        // Get the new struct declaration
        SyntaxNode updatedStructDeclaration = await AddMissingAttribute(
            document,
            structDeclaration,
            cancellationToken);

        // Replace the node in the document tree
        return document.WithSyntaxRoot(root.ReplaceNode(structDeclaration, updatedStructDeclaration));
    }

    /// <summary>
    /// Applies the code fix to add the missing attribute to a target type.
    /// </summary>
    /// <param name="document">The original document being fixed.</param>
    /// <param name="structDeclaration">The <see cref="StructDeclarationSyntax"/> to update.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>An updated document with the applied code fix, and the return type of the method being <see cref="Task"/>.</returns>
    private async Task<SyntaxNode> AddMissingAttribute(
        Document document,
        StructDeclarationSyntax structDeclaration,
        CancellationToken cancellationToken)
    {
        // Get the semantic model (bail if it's not available)
        if (await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false) is not SemanticModel semanticModel)
        {
            return structDeclaration;
        }

        // Build the map of attributes to look for
        if (!semanticModel.Compilation.TryBuildNamedTypeSymbolSet(this.leadingAttributeFullyQualifiedMetadataNames, out ImmutableHashSet<INamedTypeSymbol>? leadingAttributeTypeSymbols))
        {
            return structDeclaration;
        }

        // Also bail if we can't resolve the target attribute symbol (this should really never happen)
        if (semanticModel.Compilation.GetTypeByMetadataName(this.attributeFullyQualifiedMetadataName) is not INamedTypeSymbol attributeSymbol)
        {
            return structDeclaration;
        }

        int index = 0;

        // Find the index to use to insert the attribute. We want to make it so that if the struct declaration
        // has a bunch of leading attributes, the new one will be inserted right after that. This way the final list
        // will be nicely sorted, instead of having attributes interleaving other unrelated attributes, if any.
        foreach (AttributeListSyntax attributeList in structDeclaration.AttributeLists)
        {
            // Make sure we have an attribute to check
            if (attributeList.Attributes is not [AttributeSyntax attribute, ..])
            {
                continue;
            }

            // Resolve the symbol for the attribute (stop here if this failed for whatever reason)
            if (!semanticModel.GetSymbolInfo(attribute, cancellationToken).TryGetAttributeTypeSymbol(out INamedTypeSymbol? attributeTypeSymbol))
            {
                break;
            }

            // If the attribute is a leading one, increment the index and continue
            if (leadingAttributeTypeSymbols.Contains(attributeTypeSymbol))
            {
                index++;
            }
            else
            {
                // Otherwise, stop here, we reached the end of the sequence
                break;
            }
        }

        SyntaxGenerator syntaxGenerator = SyntaxGenerator.GetGenerator(document);

        // Create the attribute syntax for the new attribute. Also annotate it
        // to automatically add using directives to the document, if needed.
        // Then create the attribute syntax and insert it at the right position.
        SyntaxNode attributeTypeSyntax = syntaxGenerator.TypeExpression(attributeSymbol).WithAdditionalAnnotations(Simplifier.AddImportsAnnotation);
        SyntaxNode attributeSyntax = syntaxGenerator.Attribute(attributeTypeSyntax);
        SyntaxNode updatedStructDeclarationSyntax = syntaxGenerator.InsertAttributes(structDeclaration, index, attributeSyntax);

        // Replace the node in the syntax tree
        return updatedStructDeclarationSyntax;
    }

    /// <summary>
    /// A custom <see cref="FixAllProvider"/> with the logic from <see cref="MissingAttributeCodeFixer"/>.
    /// </summary>
    /// <param name="codeFixer">The owning <see cref="MissingAttributeCodeFixer"/> instance.</param>
    private sealed class FixAllProvider(MissingAttributeCodeFixer codeFixer) : DocumentBasedFixAllProvider
    {
        /// <inheritdoc/>
        protected override async Task<Document?> FixAllAsync(FixAllContext fixAllContext, Document document, ImmutableArray<Diagnostic> diagnostics)
        {
            // Get the document root (this should always succeed)
            if (await document.GetSyntaxRootAsync(fixAllContext.CancellationToken).ConfigureAwait(false) is not SyntaxNode root)
            {
                return document;
            }

            SyntaxEditor syntaxEditor = new(root, fixAllContext.Solution.Services);

            foreach (Diagnostic diagnostic in diagnostics)
            {
                // Get the current struct declaration for the diagnostic
                if (root.FindNode(diagnostic.Location.SourceSpan) is not StructDeclarationSyntax structDeclaration)
                {
                    continue;
                }

                // Get the syntax node with the updated declaration
                SyntaxNode updatedStructDeclaration = await codeFixer.AddMissingAttribute(
                    document,
                    structDeclaration,
                    fixAllContext.CancellationToken);

                // Replace the node via the editor
                syntaxEditor.ReplaceNode(structDeclaration, updatedStructDeclaration);
            }

            return document.WithSyntaxRoot(syntaxEditor.GetChangedRoot());
        }
    }
}