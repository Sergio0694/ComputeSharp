using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable IDE0052

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <summary>
/// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# methods to convert to HLSL compliant code.
/// </summary>
internal sealed partial class ShaderSourceRewriter : HlslSourceRewriter
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
    /// The collection of discovered instance methods for custom struct types.
    /// </summary>
    private readonly IDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods;

    /// <summary>
    /// The collection of processed local functions in the current tree.
    /// </summary>
    private readonly Dictionary<string, LocalFunctionStatementSyntax> localFunctions;

    /// <summary>
    /// The list of implicit variables to declare at the start of the body.
    /// </summary>
    private readonly List<VariableDeclarationSyntax> implicitVariables;

    /// <summary>
    /// Whether or not the current instance is processing a shader entry point.
    /// </summary>
    private readonly bool isEntryPoint;

    /// <summary>
    /// The identifier of the current <see cref="MethodDeclarationSyntax"/> or <see cref="LocalFunctionStatementSyntax"/> tree being visited.
    /// </summary>
    private SyntaxToken currentMethodIdentifier;

    /// <summary>
    /// The current depth inside local declarations.
    /// </summary>
    private int localFunctionDepth;

    /// <summary>
    /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
    /// </summary>
    /// <param name="shaderType">The type symbol for the shader type.</param>
    /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the target syntax tree.</param>
    /// <param name="discoveredTypes">The set of discovered custom types.</param>
    /// <param name="staticMethods">The set of discovered and processed static methods.</param>
    /// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
    /// <param name="isEntryPoint">Whether or not the current instance is processing a shader entry point.</param>
    public ShaderSourceRewriter(
        INamedTypeSymbol shaderType,
        SemanticModelProvider semanticModel,
        ICollection<INamedTypeSymbol> discoveredTypes,
        IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
        IDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods,
        IDictionary<IFieldSymbol, string> constantDefinitions,
        ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
        bool isEntryPoint)
        : base(semanticModel, discoveredTypes, constantDefinitions, diagnostics)
    {
        this.shaderType = shaderType;
        this.staticMethods = staticMethods;
        this.instanceMethods = instanceMethods;
        this.localFunctions = [];
        this.implicitVariables = [];
        this.isEntryPoint = isEntryPoint;
    }

    /// <summary>
    /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
    /// </summary>
    /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the target syntax tree.</param>
    /// <param name="discoveredTypes">The set of discovered custom types.</param>
    /// <param name="staticMethods">The set of discovered and processed static methods.</param>
    /// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
    /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
    /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
    public ShaderSourceRewriter(
        SemanticModelProvider semanticModel,
        ICollection<INamedTypeSymbol> discoveredTypes,
        IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
        IDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods,
        IDictionary<IFieldSymbol, string> constantDefinitions,
        ImmutableArrayBuilder<DiagnosticInfo> diagnostics)
        : base(semanticModel, discoveredTypes, constantDefinitions, diagnostics)
    {
        this.staticMethods = staticMethods;
        this.instanceMethods = instanceMethods;
        this.implicitVariables = [];
        this.localFunctions = [];
    }

    /// <summary>
    /// Gets the collection of processed local functions in the current tree.
    /// </summary>
    public IReadOnlyDictionary<string, LocalFunctionStatementSyntax> LocalFunctions => this.localFunctions;

    /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
    public MethodDeclarationSyntax? Visit(MethodDeclarationSyntax? node)
    {
        if (node is null)
        {
            return null;
        }

        this.currentMethodIdentifier = node.Identifier;

        MethodDeclarationSyntax? updatedNode = (MethodDeclarationSyntax?)base.Visit(node)!;

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
            BlockSyntax implicitBlock = Block(this.implicitVariables.Select(static v => LocalDeclarationStatement(v)).ToArray());

            // Add the tracked implicit declarations (at the start of the body)
            updatedNode = updatedNode.WithBody(implicitBlock).AddBodyStatements([.. updatedNode.Body!.Statements]);
        }

        return updatedNode;
    }

    /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
    public LocalFunctionStatementSyntax? Visit(LocalFunctionStatementSyntax? node)
    {
        if (node is null)
        {
            return null;
        }

        this.currentMethodIdentifier = node.Identifier;

        LocalFunctionStatementSyntax? updatedNode = (LocalFunctionStatementSyntax?)base.Visit(node)!;

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
            BlockSyntax implicitBlock = Block(this.implicitVariables.Select(static v => LocalDeclarationStatement(v)).ToArray());

            updatedNode = updatedNode.WithBody(implicitBlock).AddBodyStatements([.. updatedNode.Body!.Statements]);
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitParameter(ParameterSyntax node)
    {
        ParameterSyntax updatedNode = (ParameterSyntax)base.VisitParameter(node)!;

        // By default, parameters just have no modifier
        SyntaxToken modifier = Token(SyntaxKind.None);

        // Convert the C# parameter modifiers to the HLSL equivalent
        for (int i = 0; i < node.Modifiers.Count; i++)
        {
            SyntaxToken currentModifier = node.Modifiers[i];

            // We're only looking for in, ref, readonly, and out. The readonly modifier
            // needs special handling, as it only makes sense right after a ref modifier.
            switch (currentModifier.Kind())
            {
                case SyntaxKind.InKeyword:
                case SyntaxKind.OutKeyword:
                    modifier = currentModifier;
                    goto ExitLoop;
                case SyntaxKind.RefKeyword when node.Modifiers.Count >= 2 && i < node.Modifiers.Count - 1 && node.Modifiers[i + 1].IsKind(SyntaxKind.ReadOnlyKeyword):
                    modifier = Token(SyntaxKind.InKeyword);
                    goto ExitLoop;
                case SyntaxKind.RefKeyword:
                    modifier = ParseToken("inout");
                    goto ExitLoop;
            }
        }

        ExitLoop:

        return updatedNode
            .WithAttributeLists(default)
            .ReplaceAndTrackType(updatedNode.Type!, node.Type!, SemanticModel.For(node), DiscoveredTypes)
            .WithModifiers(TokenList(modifier));
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
        LocalDeclarationStatementSyntax updatedNode = (LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node)!;

        if (SemanticModel.For(node).GetOperation(node) is IOperation { Kind: OperationKind.UsingDeclaration })
        {
            Diagnostics.Add(UsingStatementOrDeclaration, node);
        }

        return updatedNode.ReplaceAndTrackType(updatedNode.Declaration.Type, node.Declaration.Type, SemanticModel.For(node), DiscoveredTypes);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitDeclarationExpression(DeclarationExpressionSyntax node)
    {
        DeclarationExpressionSyntax updatedNode = (DeclarationExpressionSyntax)base.VisitDeclarationExpression(node)!;

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
            .WithoutInvalidHlslModifiers()
            .WithAttributeLists(List<AttributeListSyntax>());
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
    {
        // If the current identifier matches the one for the current method, it means the local function
        // statement is the one being inspected, and it's nto coming from a local function in a method
        // being parsed. That is, this was a blobal method that was annotated in source.
        if (node.Identifier == this.currentMethodIdentifier)
        {
            LocalFunctionStatementSyntax updatedNode =
                ((LocalFunctionStatementSyntax)base.VisitLocalFunctionStatement(node)!)
                .WithBlockBody()
                .WithAttributeLists(List<AttributeListSyntax>());

            updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.ReturnType, node!.ReturnType, SemanticModel.For(node), DiscoveredTypes);

            if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.AsyncKeyword)))
            {
                Diagnostics.Add(AsyncModifierOnMethodOrFunction, node);
            }

            if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.UnsafeKeyword)))
            {
                Diagnostics.Add(UnsafeModifierOnMethodOrFunction, node);
            }

            return updatedNode;
        }
        else
        {
            this.localFunctionDepth++;

            LocalFunctionStatementSyntax updatedNode =
                ((LocalFunctionStatementSyntax)base.VisitLocalFunctionStatement(node)!)
                .WithBlockBody()
                .WithAttributeLists(List<AttributeListSyntax>())
                .WithIdentifier(Identifier($"__{this.currentMethodIdentifier.Text}__{node.Identifier.Text}"));

            updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.ReturnType, node!.ReturnType, SemanticModel.For(node), DiscoveredTypes);

            if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.AsyncKeyword)))
            {
                Diagnostics.Add(AsyncModifierOnMethodOrFunction, node);
            }

            if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.UnsafeKeyword)))
            {
                Diagnostics.Add(UnsafeModifierOnMethodOrFunction, node);
            }

            this.localFunctionDepth--;

            // HLSL doesn't support local functions, so we first process them as usual and then remove
            // them from the current syntax tree completely. These will be added to the shader source
            // as external static method with a special name to avoid conflicts with other methods.
            // The name will simply be in the format: "__<MethodName>__<FunctionName>".
            this.localFunctions.Add(updatedNode.Identifier.Text, updatedNode);

            return null;
        }
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        MemberAccessExpressionSyntax updatedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node)!;

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

                    string ownerTypeName = ((INamedTypeSymbol)fieldOperation.Field.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
                    string constantName = $"__{ownerTypeName}__{fieldOperation.Field.Name}";

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
                // Allow specialized rewriters to track the property, if needed (eg. to emit diagnostics)
                TrackKnownPropertyAccess(operation, node, mapping!);

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

            // If the current member access is an access to any of the size properties in the available
            // resource types (either buffer or texture types), then rewrite the subtree to transform the
            // property access into an invocation of an autogenerated static helper method to get the target
            // dimension for the buffer in use. The helper method is added to the list of static helpers.
            if (operation is IPropertyReferenceOperation propertyOperation &&
                HlslKnownProperties.TryGetAccessorRankAndAxis(propertyOperation.Property.GetFullyQualifiedMetadataName(), out int rank, out int axis))
            {
                IMethodSymbol key = propertyOperation.Property.GetMethod!;

                if (!this.staticMethods.TryGetValue(key, out MethodDeclarationSyntax? methodSyntax))
                {
                    INamedTypeSymbol resourceType = (INamedTypeSymbol)SemanticModel.For(node).GetTypeInfo(node.Expression).Type!;
                    string resourceName = HlslKnownTypes.GetMappedName(resourceType);

                    // Create a static method to get a specified dimension for a target resource type.
                    // The method will automatically create the necessary number of temporary variables
                    // depending on the rank of the input resource (eg. 2 for Texture2D<T> resources).
                    // For instance, this code will generate the following tree for Texture2D<float>.Width:
                    //
                    // static int __get_Dimension0(Texture2D<float> texture)
                    // {
                    //     uint _0, _1;
                    //     texture.GetDimensions(_0, _1);
                    //
                    //     return _0;
                    // }
                    methodSyntax =
                        MethodDeclaration(PredefinedType(Token(SyntaxKind.IntKeyword)), $"__get_Dimension{axis}")
                        .AddModifiers(Token(SyntaxKind.StaticKeyword))
                        .AddParameterListParameters(
                            Parameter(Identifier("resource"))
                            .WithType(IdentifierName(resourceName)))
                        .WithBody(Block(
                            LocalDeclarationStatement(
                                VariableDeclaration(PredefinedType(Token(SyntaxKind.UIntKeyword)))
                                .AddVariables(Enumerable.Range(0, rank).Select(static i => VariableDeclarator(Identifier("_" + i))).ToArray())),
                            ExpressionStatement(
                                InvocationExpression(MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("resource"),
                                    IdentifierName("GetDimensions")))
                                .AddArgumentListArguments(Enumerable.Range(0, rank).Select(static i => Argument(IdentifierName("_" + i))).ToArray())),
                            ReturnStatement(IdentifierName("_" + axis))));

                    this.staticMethods.Add(key, methodSyntax);
                }

                return InvocationExpression(IdentifierName(methodSyntax.Identifier)).AddArgumentListArguments(Argument(updatedNode.Expression));
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        InvocationExpressionSyntax updatedNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node)!;

        if (SemanticModel.For(node).GetOperation(node) is IInvocationOperation { TargetMethod: IMethodSymbol method } operation)
        {
            if (method.IsStatic)
            {
                string metadataName = method.GetFullyQualifiedMetadataName();

                // If the invocation consists of invoking a static method that has a direct
                // mapping to HLSL, rewrite the expression in the current invocation node.
                // For instance: Math.Abs(expr) => abs(expr).
                if (HlslKnownMethods.TryGetMappedName(metadataName, out string? mapping, out bool requiresParametersMapping))
                {
                    if (requiresParametersMapping)
                    {
                        mapping = HlslKnownMethods.GetMappedNameWithParameters(method.Name, method.Parameters.Select(static p => p.Type.Name));
                    }

                    // Allow specialized types to track the method invocation, if needed
                    TrackKnownMethodInvocation(metadataName);

                    return updatedNode.WithExpression(IdentifierName(mapping!));
                }

                // Update the name if the target is a local function. The exact schema for the
                // updated name is detailed in the override handling the local function statement.
                if (method.MethodKind == MethodKind.LocalFunction)
                {
                    string functionIdentifier = $"__{this.currentMethodIdentifier.Text}__{method.Name}";

                    return updatedNode.WithExpression(IdentifierName(functionIdentifier));
                }

                // If the method is an external static method, import and rewrite it as well.
                // This assumes that the target method is actually part of the same compilation.
                // We need to check the declaring type to avoid rewriting static methods in the shader
                // type as well, as they will be processed by the generator in a different path.
                if (!SymbolEqualityComparer.Default.Equals(this.shaderType, method.ContainingType))
                {
                    string methodIdentifier = method.GetFullyQualifiedMetadataName().ToHlslIdentifierName();

                    if (!this.staticMethods.ContainsKey(method))
                    {
                        if (method.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() is not MethodDeclarationSyntax methodNode)
                        {
                            Diagnostics.Add(InvalidMethodCall, node, method);

                            return updatedNode;
                        }

                        ShaderSourceRewriter shaderSourceRewriter = new(SemanticModel, DiscoveredTypes, this.staticMethods, this.instanceMethods, ConstantDefinitions, Diagnostics);
                        MethodDeclarationSyntax processedMethod = shaderSourceRewriter.Visit(methodNode)!.NormalizeWhitespace(eol: "\n").WithoutTrivia();

                        this.staticMethods.Add(method, processedMethod.WithIdentifier(Identifier(methodIdentifier)));
                    }

                    return updatedNode.WithExpression(IdentifierName(methodIdentifier));
                }
            }
            else
            {
                string metadataName = method.GetFullyQualifiedMetadataName(includeParameters: true);

                // Check whether this is a Sample(...) call on a normalized texture type
                if (HlslKnownMethods.TryGetMappedResourceSamplerAccessType(metadataName, out string? mapping))
                {
                    // Get the syntax for the argument syntax transformation (adding the vector type constructor if needed)
                    ArgumentSyntax coordinateSyntax = mapping switch
                    {
                        not null => Argument(InvocationExpression(IdentifierName(mapping!), ArgumentList(updatedNode.ArgumentList.Arguments))),
                        _ => updatedNode.ArgumentList.Arguments[0]
                    };

                    // Rewrite texture resource sampled accesses, if needed
                    return RewriteSampledTextureAccess(operation, updatedNode.Expression, coordinateSyntax);
                }

                // If the instance is a struct type that is available in source, rewrite the instance method.
                // This path is only taken for instance methods for external struct types, not the shader itself.
                if (operation.TargetMethod.ContainingType is { TypeKind: TypeKind.Struct } structTypeSymbol &&
                    !SymbolEqualityComparer.Default.Equals(this.shaderType, structTypeSymbol))
                {
                    DiscoveredTypes.Add(structTypeSymbol);

                    string methodIdentifier = method.GetFullyQualifiedMetadataName().ToHlslIdentifierName();

                    // Same as for static methods, ensure the method is available in source, and process it if so
                    if (!this.instanceMethods.ContainsKey(method))
                    {
                        if (method.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() is not MethodDeclarationSyntax methodNode)
                        {
                            Diagnostics.Add(InvalidMethodCall, node, method);

                            return updatedNode;
                        }

                        ShaderSourceRewriter shaderSourceRewriter = new(SemanticModel, DiscoveredTypes, this.instanceMethods, this.instanceMethods, ConstantDefinitions, Diagnostics);
                        MethodDeclarationSyntax processedMethod = shaderSourceRewriter.Visit(methodNode)!.NormalizeWhitespace(eol: "\n").WithoutTrivia();

                        this.instanceMethods.Add(method, processedMethod.WithIdentifier(Identifier(methodIdentifier)));
                    }

                    // Rewrite the expression depending on its type:
                    //   - If it's a simple member expression, change the identifier name: eg. foo.Bar(...) => foo.NewName(...)
                    //   - If it's an identifier name, change it directly: eg. Bar(...) => NewName(...)
                    return updatedNode.Expression switch
                    {
                        MemberAccessExpressionSyntax memberAccess => updatedNode.WithExpression(memberAccess.WithName(IdentifierName(methodIdentifier))),
                        IdentifierNameSyntax identifierName => updatedNode.WithExpression(IdentifierName(methodIdentifier)),
                        _ => throw new NotSupportedException($"Unsupported instance method expression type \"{updatedNode.Expression.GetType()}\".")
                    };
                }
            }
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitArgument(ArgumentSyntax node)
    {
        ArgumentSyntax updatedNode = (ArgumentSyntax)base.VisitArgument(node)!;

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

    /// <summary>
    /// Rewrites a sampled texture access.
    /// </summary>
    /// <param name="operation">The <see cref="IInvocationOperation"/> instance for the sampled texture access.</param>
    /// <param name="expression">The input <see cref="ExpressionSyntax"/> instance for the node to rewrite.</param>
    /// <param name="arguments">The input <see cref="ArgumentSyntax"/> with the updated arguments for the expression to rewrite.</param>
    /// <returns>A <see cref="SyntaxNode"/> representing the rewritten sampled texture access.</returns>
    private partial SyntaxNode RewriteSampledTextureAccess(IInvocationOperation operation, ExpressionSyntax expression, ArgumentSyntax arguments);

    /// <summary>
    /// Tracks a property access to a known HLSL property.
    /// </summary>
    /// <param name="operation">The <see cref="IMemberReferenceOperation"/> instance for the operation.</param>
    /// <param name="node">The <see cref="MemberAccessExpressionSyntax"/> instance for the operation.</param>
    /// <param name="mappedName">The mapped name for the property access.</param>
    private partial void TrackKnownPropertyAccess(IMemberReferenceOperation operation, MemberAccessExpressionSyntax node, string mappedName);

    /// <summary>
    /// Tracks a method invocation for a known HLSL method.
    /// </summary>
    /// <param name="metadataName">The metadata name of the method being invoked.</param>
    private partial void TrackKnownMethodInvocation(string metadataName);
}