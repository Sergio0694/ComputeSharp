using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static ComputeSharp.D2D1.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.D2D1.SourceGenerators.SyntaxRewriters;

/// <summary>
/// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# methods to convert to HLSL compliant code.
/// </summary>
internal sealed class ShaderSourceRewriter : HlslSourceRewriter
{
    /// <summary>
    /// The type symbol for the shader type.
    /// </summary>
    private readonly INamedTypeSymbol? shaderType;

    /// <summary>
    /// The collection of discovered static methods.
    /// </summary>
    private readonly IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods;

    /// <summary>
    /// The collection of processed local functions in the current tree.
    /// </summary>
    private readonly Dictionary<string, LocalFunctionStatementSyntax> localFunctions;

    /// <summary>
    /// The list of implicit variables to declare at the start of the body.
    /// </summary>
    private readonly List<VariableDeclarationSyntax> implicitVariables;

    /// <summary>
    /// The current <see cref="MethodDeclarationSyntax"/> tree being visited.
    /// </summary>
    private MethodDeclarationSyntax? currentMethod;

    /// <summary>
    /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
    /// </summary>
    /// <param name="shaderType">The type symbol for the shader type.</param>
    /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the target syntax tree.</param>
    /// <param name="discoveredTypes">The set of discovered custom types.</param>
    /// <param name="staticMethods">The set of discovered and processed static methods.</param>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
    public ShaderSourceRewriter(
        INamedTypeSymbol shaderType,
        SemanticModelProvider semanticModel,
        ICollection<INamedTypeSymbol> discoveredTypes,
        IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
        IDictionary<IFieldSymbol, string> constantDefinitions,
        ImmutableArray<Diagnostic>.Builder diagnostics)
        : base(semanticModel, discoveredTypes, constantDefinitions, diagnostics)
    {
        this.shaderType = shaderType;
        this.staticMethods = staticMethods;
        this.localFunctions = new();
        this.implicitVariables = new();
    }

    /// <summary>
    /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
    /// </summary>
    /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the target syntax tree.</param>
    /// <param name="discoveredTypes">The set of discovered custom types.</param>
    /// <param name="staticMethods">The set of discovered and processed static methods.</param>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
    private ShaderSourceRewriter(
        SemanticModelProvider semanticModel,
        ICollection<INamedTypeSymbol> discoveredTypes,
        IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
        IDictionary<IFieldSymbol, string> constantDefinitions,
        ImmutableArray<Diagnostic>.Builder diagnostics)
        : base(semanticModel, discoveredTypes, constantDefinitions, diagnostics)
    {
        this.staticMethods = staticMethods;
        this.implicitVariables = new();
        this.localFunctions = new();
    }

    /// <summary>
    /// Gets the collection of processed local functions in the current tree.
    /// </summary>
    public IReadOnlyDictionary<string, LocalFunctionStatementSyntax> LocalFunctions => this.localFunctions;

    /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
    public MethodDeclarationSyntax? Visit(MethodDeclarationSyntax? node)
    {
        this.currentMethod = node;

        var updatedNode = (MethodDeclarationSyntax?)base.Visit(node)!;

        updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.ReturnType, node!.ReturnType, SemanticModel.For(node), DiscoveredTypes);

        if (node!.Modifiers.Any(m => m.IsKind(SyntaxKind.AsyncKeyword)))
        {
            Diagnostics.Add(AsyncModifierOnMethodOrFunction, node);
        }

        if (node!.Modifiers.Any(m => m.IsKind(SyntaxKind.UnsafeKeyword)))
        {
            Diagnostics.Add(UnsafeModifierOnMethodOrFunction, node);
        }

        if (updatedNode is not null)
        {
            var implicitBlock = Block(this.implicitVariables.Select(static v => LocalDeclarationStatement(v)).ToArray());

            // Add the tracked implicit declarations (at the start of the body)
            updatedNode = updatedNode.WithBody(implicitBlock).AddBodyStatements(updatedNode.Body!.Statements.ToArray());
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitParameter(ParameterSyntax node)
    {
        var updatedNode = (ParameterSyntax)base.VisitParameter(node)!;

        // Convert the C# parameter modifiers to the HLSL equivalent
        SyntaxToken modifier =
            node.Modifiers
            .Where(static m => m.Kind() is SyntaxKind.OutKeyword or SyntaxKind.RefKeyword or SyntaxKind.InKeyword)
            .Select(static m => m.Kind() switch
            {
                SyntaxKind.OutKeyword => m,
                SyntaxKind.InKeyword => m,
                SyntaxKind.RefKeyword => ParseToken("inout"),
                _ => Token(SyntaxKind.None)
            }).FirstOrDefault();

        return updatedNode
            .WithAttributeLists(default)
            .ReplaceAndTrackType(updatedNode.Type!, node.Type!, SemanticModel.For(node), DiscoveredTypes)
            .WithModifiers(TokenList(modifier));
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
        var updatedNode = ((LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node)!);

        if (SemanticModel.For(node).GetOperation(node) is IOperation { Kind: OperationKind.UsingDeclaration })
        {
            Diagnostics.Add(UsingStatementOrDeclaration, node);
        }

        return updatedNode.ReplaceAndTrackType(updatedNode.Declaration.Type, node.Declaration.Type, SemanticModel.For(node), DiscoveredTypes);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitDeclarationExpression(DeclarationExpressionSyntax node)
    {
        var updatedNode = (DeclarationExpressionSyntax)base.VisitDeclarationExpression(node)!;

        updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node.Type, SemanticModel.For(node), DiscoveredTypes);

        // Add the variable to the list of implicit declarations
        this.implicitVariables.Add(VariableDeclaration(updatedNode.Type).AddVariables(VariableDeclarator(updatedNode.Designation.ToString())));

        return IdentifierName(updatedNode.Designation.ToString());
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        return
            ((MethodDeclarationSyntax)base.VisitMethodDeclaration(node)!)
            .WithBlockBody()
            .WithoutAccessibilityModifiers()
            .WithAttributeLists(List<AttributeListSyntax>());
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
    {
        var updatedNode =
            ((LocalFunctionStatementSyntax)base.VisitLocalFunctionStatement(node)!)
            .WithBlockBody()
            .WithAttributeLists(List<AttributeListSyntax>())
            .WithIdentifier(Identifier($"__{this.currentMethod!.Identifier.Text}__{node.Identifier.Text}"));

        updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.ReturnType, node!.ReturnType, SemanticModel.For(node), DiscoveredTypes);

        if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.AsyncKeyword)))
        {
            Diagnostics.Add(AsyncModifierOnMethodOrFunction, node);
        }

        if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.UnsafeKeyword)))
        {
            Diagnostics.Add(UnsafeModifierOnMethodOrFunction, node);
        }

        // HLSL doesn't support local functions, so we first process them as usual and then remove
        // them from the current syntax tree completely. These will be added to the shader source
        // as external static method with a special name to avoid conflicts with other methods.
        // The name will simply be in the format: "__<MethodName>__<FunctionName>".
        this.localFunctions.Add(updatedNode.Identifier.Text, updatedNode);

        return null;
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        var updatedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node)!;
        
        if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
            SemanticModel.For(node).GetOperation(node) is IMemberReferenceOperation operation)
        {
            if (operation is IFieldReferenceOperation fieldOperation)
            {
                // If the member access is a constant, track it and replace the tree with the processed constant name
                if (fieldOperation.Field.IsConst &&
                    fieldOperation.Type!.TypeKind != TypeKind.Enum)
                {
                    if (HlslKnownFields.TryGetMappedName(fieldOperation.Member.ToDisplayString(), out string? constantLiteral))
                    {
                        return ParseExpression(constantLiteral!);
                    }

                    ConstantDefinitions[fieldOperation.Field] = ((IFormattable)fieldOperation.Field.ConstantValue!).ToString(null, CultureInfo.InvariantCulture);

                    var ownerTypeName = ((INamedTypeSymbol)fieldOperation.Field.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
                    var constantName = $"__{ownerTypeName}__{fieldOperation.Field.Name}";

                    return IdentifierName(constantName);
                }

                // If the member access is a this.<FIELD> access, rewrite it to strip "this."
                if (node.Expression.IsKind(SyntaxKind.ThisExpression))
                {
                    return updatedNode.Name;
                }
            }

            // If the current member access is a field or property access, check the lookup table
            // to see if the member should be rewritten for HLSL compliance, and rewrite if needed.
            if (HlslKnownProperties.TryGetMappedName(operation.Member.ToDisplayString(), out string? mapping))
            {
                // Static and instance members are handled differently, with static members being
                // converted into a literal expression, and instance getting an updated identifier.
                // For instance, consider these three cases:
                //   - Vector3.One (static) => float3(1.0f, 1.0f, 1.0f)
                //   - ThreadIds.X (custom) => ThreadIds.x
                //   - uint3.X (instance) => [arg].x
                return operation.Member.IsStatic switch
                {
                    true => ParseExpression(mapping!),
                    false => updatedNode.WithName(IdentifierName(mapping!))
                };
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        var updatedNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node)!;

        if (SemanticModel.For(node).GetOperation(node) is IInvocationOperation operation &&
            operation.TargetMethod is IMethodSymbol method &&
            method.IsStatic)
        {
            string metadataName = method.GetFullMetadataName();

            // If the invocation consists of invoking a static method that has a direct
            // mapping to HLSL, rewrite the expression in the current invocation node.
            // For instance: Math.Abs(expr) => abs(expr).
            if (HlslKnownMethods.TryGetMappedName(metadataName, out string? mapping))
            {
                // Track whether the method needs [D2DRequiresScenePosition]
                if (HlslKnownMethods.NeedsD2DRequiresScenePositionAttribute(metadataName))
                {
                    NeedsD2DRequiresScenePositionAttribute = true;
                }

                return updatedNode.WithExpression(ParseExpression(mapping!));
            }

            // Update the name if the target is a local function. The exact schema for the
            // updated name is detailed in the override handling the local function statement.
            if (method.MethodKind == MethodKind.LocalFunction)
            {
                var functionIdentifier = $"__{this.currentMethod!.Identifier.Text}__{method.Name}";

                return updatedNode.WithExpression(ParseExpression(functionIdentifier));
            }

            // If the method is an external static method, import and rewrite it as well.
            // This assumes that the target method is actually part of the same compilation.
            // We need to check the declaring type to avoid rewriting static methods in the shader
            // type as well, as they will be processed by the generator in a different path.
            if (!SymbolEqualityComparer.Default.Equals(this.shaderType, method.ContainingType))
            {
                var methodIdentifier = method.GetFullMetadataName().ToHlslIdentifierName();

                if (!this.staticMethods.ContainsKey(method))
                {
                    if (method.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() is not MethodDeclarationSyntax methodNode)
                    {
                        Diagnostics.Add(InvalidMethodCall, node, method);

                        return updatedNode;
                    }

                    ShaderSourceRewriter shaderSourceRewriter = new(SemanticModel, DiscoveredTypes, this.staticMethods, ConstantDefinitions, Diagnostics);
                    MethodDeclarationSyntax processedMethod = shaderSourceRewriter.Visit(methodNode)!.NormalizeWhitespace(eol: "\n").WithoutTrivia();

                    this.staticMethods.Add(method, processedMethod.WithIdentifier(Identifier(methodIdentifier)));
                }

                return updatedNode.WithExpression(ParseExpression(methodIdentifier));
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitArgument(ArgumentSyntax node)
    {
        var updatedNode = (ArgumentSyntax)base.VisitArgument(node)!;

        // Strip the ref keywords from arguments
        updatedNode = updatedNode.WithRefKindKeyword(Token(SyntaxKind.None));

        // Track and rewrite the discarded declaration
        if (SemanticModel.For(node).GetOperation(node.Expression) is IDiscardOperation operation)
        {
            TypeSyntax typeSyntax = ParseTypeName(HlslKnownTypes.TrackType(operation.Type!, DiscoveredTypes));
            string identifier = $"__implicit{this.implicitVariables.Count}";

            // Add the variable to the list of implicit declarations
            this.implicitVariables.Add(VariableDeclaration(typeSyntax).AddVariables(VariableDeclarator(identifier)));

            return updatedNode.WithExpression(IdentifierName(identifier));
        }

        return updatedNode;
    }
}
