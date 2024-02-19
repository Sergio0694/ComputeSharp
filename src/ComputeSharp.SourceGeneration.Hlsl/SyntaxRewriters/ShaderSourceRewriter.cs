using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable IDE0051, IDE0052

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <summary>
/// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# methods to convert to HLSL compliant code.
/// </summary>
/// <param name="shaderType">The type symbol for the shader type.</param>
/// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the target syntax tree.</param>
/// <param name="discoveredTypes">The set of discovered custom types.</param>
/// <param name="staticMethods">The set of discovered and processed static methods.</param>
/// <param name="instanceMethods">The collection of discovered instance methods for custom struct types.</param>
/// <param name="constructors">The collection of discovered constructors for custom struct types.</param>
/// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
/// <param name="staticFieldDefinitions">The collection of discovered static field definitions.</param>
/// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
/// <param name="token">The <see cref="CancellationToken"/> value for the current operation.</param>
/// <param name="isEntryPoint">Whether or not the current instance is processing a shader entry point.</param>
internal sealed partial class ShaderSourceRewriter(
    INamedTypeSymbol shaderType,
    SemanticModelProvider semanticModel,
    ICollection<INamedTypeSymbol> discoveredTypes,
    IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
    IDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods,
    IDictionary<IMethodSymbol, (MethodDeclarationSyntax, MethodDeclarationSyntax)> constructors,
    IDictionary<IFieldSymbol, string> constantDefinitions,
    IDictionary<IFieldSymbol, HlslStaticField> staticFieldDefinitions,
    ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
    CancellationToken token,
    bool isEntryPoint = false)
    : HlslSourceRewriter(semanticModel, discoveredTypes, constantDefinitions, staticFieldDefinitions, diagnostics, token)
{
    /// <summary>
    /// The type symbol for the shader type.
    /// </summary>
    private readonly INamedTypeSymbol shaderType = shaderType;

    /// <summary>
    /// The collection of discovered static methods.
    /// </summary>
    private readonly IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods = staticMethods;

    /// <summary>
    /// The collection of discovered instance methods for custom struct types.
    /// </summary>
    private readonly IDictionary<IMethodSymbol, MethodDeclarationSyntax> instanceMethods = instanceMethods;

    /// <summary>
    /// The collection of discovered constructors for custom struct types.
    /// </summary>
    private readonly IDictionary<IMethodSymbol, (MethodDeclarationSyntax Stub, MethodDeclarationSyntax Ctor)> constructors = constructors;

    /// <summary>
    /// The collection of processed local functions in the current tree.
    /// </summary>
    private readonly Dictionary<string, LocalFunctionStatementSyntax> localFunctions = [];

    /// <summary>
    /// The list of implicit variables to declare at the start of the body.
    /// </summary>
    private readonly List<VariableDeclarationSyntax> implicitVariables = [];

    /// <summary>
    /// Whether or not the current instance is processing a shader entry point.
    /// </summary>
    private readonly bool isEntryPoint = isEntryPoint;

    /// <summary>
    /// The identifier of the current <see cref="ConstructorDeclarationSyntax"/>, <see cref="MethodDeclarationSyntax"/>
    /// or <see cref="LocalFunctionStatementSyntax"/> tree being visited (used to rewrite local functions).
    /// </summary>
    private SyntaxToken currentMethodIdentifier;

    /// <summary>
    /// The current depth inside local declarations.
    /// </summary>
    private int localFunctionDepth;

    /// <summary>
    /// Gets the collection of processed local functions in the current tree.
    /// </summary>
    public IReadOnlyDictionary<string, LocalFunctionStatementSyntax> LocalFunctions => this.localFunctions;

    /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
    public ConstructorDeclarationSyntax? Visit(ConstructorDeclarationSyntax? node)
    {
        if (node is null)
        {
            return null;
        }

        this.currentMethodIdentifier = node.Identifier;

        // Constructors have no return type, so there is no return type identifier to replace.
        // We simply track the type of the type being constructed and then continue normally.
        // This is done separately where the constructor operation is being processed.
        ConstructorDeclarationSyntax? updatedNode = (ConstructorDeclarationSyntax?)base.Visit(node)!;

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
    public MethodDeclarationSyntax? Visit(MethodDeclarationSyntax? node)
    {
        if (node is null)
        {
            return null;
        }

        this.currentMethodIdentifier = node.Identifier;

        MethodDeclarationSyntax? updatedNode = (MethodDeclarationSyntax?)base.Visit(node)!;

        updatedNode = ReplaceAndTrackType(updatedNode, updatedNode.ReturnType, node!.ReturnType, SemanticModel.For(node));

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

        updatedNode = ReplaceAndTrackType(updatedNode, updatedNode.ReturnType, node!.ReturnType, SemanticModel.For(node));

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

        updatedNode = updatedNode.WithAttributeLists(default);
        updatedNode = ReplaceAndTrackType(updatedNode, updatedNode.Type!, node.Type!, SemanticModel.For(node));

        return updatedNode.WithModifiers(TokenList(modifier));
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
        LocalDeclarationStatementSyntax updatedNode = (LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node)!;

        if (SemanticModel.For(node).GetOperation(node, CancellationToken) is IOperation { Kind: OperationKind.UsingDeclaration })
        {
            Diagnostics.Add(UsingStatementOrDeclaration, node);
        }

        return ReplaceAndTrackType(updatedNode, updatedNode.Declaration.Type, node.Declaration.Type, SemanticModel.For(node));
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitDeclarationExpression(DeclarationExpressionSyntax node)
    {
        DeclarationExpressionSyntax updatedNode = (DeclarationExpressionSyntax)base.VisitDeclarationExpression(node)!;

        updatedNode = ReplaceAndTrackType(updatedNode, updatedNode.Type, node.Type, SemanticModel.For(node));

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
        CancellationToken.ThrowIfCancellationRequested();

        // If the current identifier matches the one for the current method, it means the local function
        // statement is the one being inspected, and it's nto coming from a local function in a method
        // being parsed. That is, this was a blobal method that was annotated in source.
        if (node.Identifier == this.currentMethodIdentifier)
        {
            LocalFunctionStatementSyntax updatedNode =
                ((LocalFunctionStatementSyntax)base.VisitLocalFunctionStatement(node)!)
                .WithBlockBody()
                .WithAttributeLists(List<AttributeListSyntax>());

            updatedNode = ReplaceAndTrackType(updatedNode, updatedNode.ReturnType, node!.ReturnType, SemanticModel.For(node));

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

            updatedNode = ReplaceAndTrackType(updatedNode, updatedNode.ReturnType, node!.ReturnType, SemanticModel.For(node));

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
    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        // Intercept identifier names to catch whether we are accessing a static field from its containing type.
        // This needs special handling, as it would not go through the usual member access path. If we are doing
        // this, then we rewrite it with the shared logic for all static fields, otherwise we return the normal
        // visit. Before getting semantic information, we can pre-filter to exclude invocation expressions and
        // member access expressions. This is safe, because those two parent nodes would already be handled.
        // We also only process fields from external types, as those in shader types don't need handling here.
        // This is because they're lowered to the same identifier name, without fully qualified names to rewrite.
        if (node.Parent is not (InvocationExpressionSyntax or MemberAccessExpressionSyntax) &&
            SemanticModel.For(node).GetOperation(node, CancellationToken) is IFieldReferenceOperation { Field.IsStatic: true } operation &&
            !SymbolEqualityComparer.Default.Equals(operation.Field.ContainingType, this.shaderType))
        {
            return VisitExternalStaticFieldAccess(null, operation.Field) ?? base.VisitIdentifierName(node);
        }

        return base.VisitIdentifierName(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        CancellationToken.ThrowIfCancellationRequested();

        MemberAccessExpressionSyntax updatedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node)!;

        if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
            SemanticModel.For(node).GetOperation(node, CancellationToken) is IMemberReferenceOperation operation)
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

                // Special handling for 'this.' expressions, which are not always allowed in HLSL
                if (node.Expression.IsKind(SyntaxKind.ThisExpression))
                {
                    // If the member access is a this.<FIELD> access, rewrite it to strip 'this.', but only if accessing instance
                    // fields from the shader type itself (as using 'this.' in that case is not allowed in HLSL, and also it's not
                    // really useful anyway given that those fields are readonly, so less likely to cause observable conflicts).
                    if (SymbolEqualityComparer.Default.Equals(fieldOperation.Field.ContainingType, this.shaderType))
                    {
                        return updatedNode.Name;
                    }

                    // For all other cases, we can keep it in, as HLSL does allow using it to access instance fields of custom
                    // struct types. This is allowed by both FXC and DXC, on all shader profiles, so it's always safe to do.
                    return updatedNode;
                }

                // Handle static fields, which can be either in the shader type itself, or in external types
                if (fieldOperation.Field.IsStatic)
                {
                    // Special case static fields in the shader type itself. Those are already discovered separately, when crawling
                    // all members of the shader type. But, if this path was hit, it means that they are accessed via a member access
                    // expression, which means they need to be rewritten to match the simple name they will have in the shader. This
                    // is often the case if they're accessed from external types. So we just map the name and use that expression.
                    if (SymbolEqualityComparer.Default.Equals(fieldOperation.Field.ContainingType, this.shaderType))
                    {
                        _ = HlslKnownKeywords.TryGetMappedName(fieldOperation.Field.Name, out string? mappedFieldName);

                        return IdentifierName(mappedFieldName ?? fieldOperation.Field.Name);
                    }

                    return VisitExternalStaticFieldAccess(updatedNode, fieldOperation.Field);
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
                    INamedTypeSymbol resourceType = (INamedTypeSymbol)SemanticModel.For(node).GetTypeInfo(node.Expression, CancellationToken).Type!;
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
        CancellationToken.ThrowIfCancellationRequested();

        InvocationExpressionSyntax updatedNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node)!;

        if (SemanticModel.For(node).GetOperation(node, CancellationToken) is IInvocationOperation { TargetMethod: IMethodSymbol method } operation)
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

                    // Special case: handle known named intrinsics being invoked.
                    // These have special lowering to the right HLSL constructs.
                    if (VisitKnownNamedIntrinsicInvocationExpression(node, updatedNode, mapping) is SyntaxNode namedIntrinsic)
                    {
                        return namedIntrinsic;
                    }

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
                        if (!method.TryGetSyntaxNode(CancellationToken, out MethodDeclarationSyntax? methodNode))
                        {
                            // Special cases method calls to Math and MathF APIs. In this case, we also want to emit a specific
                            // diagnostic to guide developers towards using the intrinsic APIs on the Hlsl class instead. This
                            // check is only done when the current method is already invalid anyway, so it adds no overhead.
                            if (method.ContainingType is { Name: "Math" or "MathF", ContainingType: null, ContainingNamespace.Name: "System" })
                            {
                                Diagnostics.Add(InvalidMathOrMathFCall, node, method);
                            }
                            else
                            {
                                // Otherwise just emit the generic error diagnostic
                                Diagnostics.Add(InvalidMethodOrConstructorCall, node, method);
                            }

                            return updatedNode;
                        }

                        // Add a marker to avoid endless recursion in case we have any loops in the graph.
                        // This will be updated right after rewriting this method completes. This temporary
                        // value is fine, as the actual rewritten method is not used while rewriting HLSL.
                        this.staticMethods.Add(method, null!);

                        ShaderSourceRewriter shaderSourceRewriter = new(
                            this.shaderType,
                            SemanticModel,
                            DiscoveredTypes,
                            this.staticMethods,
                            this.instanceMethods,
                            this.constructors,
                            ConstantDefinitions,
                            StaticFieldDefinitions,
                            Diagnostics,
                            CancellationToken);

                        MethodDeclarationSyntax processedMethod = shaderSourceRewriter.Visit(methodNode)!.WithoutTrivia();

                        this.staticMethods[method] = processedMethod.WithIdentifier(Identifier(methodIdentifier));
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

                    // Same as for static methods, ensure the method is available in source, and process it if so
                    if (!this.instanceMethods.ContainsKey(method))
                    {
                        if (!method.TryGetSyntaxNode(CancellationToken, out MethodDeclarationSyntax? methodNode))
                        {
                            Diagnostics.Add(InvalidMethodOrConstructorCall, node, method);

                            return updatedNode;
                        }

                        this.instanceMethods[method] = null!;

                        ShaderSourceRewriter shaderSourceRewriter = new(
                            this.shaderType,
                            SemanticModel,
                            DiscoveredTypes,
                            this.staticMethods,
                            this.instanceMethods,
                            this.constructors,
                            ConstantDefinitions,
                            StaticFieldDefinitions,
                            Diagnostics,
                            CancellationToken);

                        MethodDeclarationSyntax processedMethod = shaderSourceRewriter.Visit(methodNode)!.WithoutTrivia();

                        this.instanceMethods[method] = processedMethod;
                    }

                    return updatedNode;
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
        if (SemanticModel.For(node).GetOperation(node.Expression, CancellationToken) is IDiscardOperation operation)
        {
            TypeSyntax typeSyntax = TrackType(operation.Type!);
            string identifier = $"__implicit{this.implicitVariables.Count}";

            // Add the variable to the list of implicit declarations
            this.implicitVariables.Add(VariableDeclaration(typeSyntax).AddVariables(VariableDeclarator(identifier)));

            return updatedNode.WithExpression(IdentifierName(identifier));
        }

        return updatedNode;
    }

    /// <inheritdoc/>
    protected override SyntaxNode VisitUserDefinedObjectCreationExpression(
        BaseObjectCreationExpressionSyntax node,
        BaseObjectCreationExpressionSyntax updatedNode,
        TypeSyntax targetType)
    {
        if (SemanticModel.For(node).GetOperation(node, CancellationToken) is IObjectCreationOperation { Constructor: IMethodSymbol constructor, Type: INamedTypeSymbol { TypeKind: TypeKind.Struct } typeSymbol })
        {
            DiscoveredTypes.Add(typeSymbol);

            // If there are no arguments, check that the constructor is explicitly defined and is not
            // the default parameterless constructor. In that case, we just fallback to a default value.
            if (updatedNode.ArgumentList is not { Arguments.Count: > 0 } && constructor.IsImplicitlyDeclared)
            {
                return base.VisitUserDefinedObjectCreationExpression(node, updatedNode, targetType);
            }

            string returnTypeHlslIdentifier = typeSymbol.GetFullyQualifiedName().ToHlslIdentifierName();

            if (!this.constructors.ContainsKey(constructor))
            {
                // If we can't find the constructor declaration for the syntax reference of the constructor, it means that either the
                // type is from another assembly (hence we have no syntax references at all), or the constructor is compiler generated
                // (even if IsImplicitlyDeclared is false) and a primary constructor. This is deliberately not supported, as there
                // are very subtle ways in which captures can interact with fields and methods, and we cannot guarantee to track and
                // rewrite all of them correctly to exactly preserve the same semantics. So we just block such cases entirely as well.
                if (!constructor.TryGetSyntaxNode(CancellationToken, out ConstructorDeclarationSyntax? constructorNode))
                {
                    Diagnostics.Add(InvalidMethodOrConstructorCall, node, constructor);

                    return base.VisitUserDefinedObjectCreationExpression(node, updatedNode, targetType);
                }

                // Chaining constructors is not supported, so emit a diagnostic to inform the user.
                // The rest of the code will work as usual, but the semantics of the other chained
                // constructor being invoked is not preserved, so the resulting code is not the same.
                if (constructorNode.Initializer is not null)
                {
                    Diagnostics.Add(InvalidBaseConstructorDeclaration, node, constructor);
                }

                this.constructors[constructor] = default!;

                ShaderSourceRewriter shaderSourceRewriter = new(
                    this.shaderType,
                    SemanticModel,
                    DiscoveredTypes,
                    this.staticMethods,
                    this.instanceMethods,
                    this.constructors,
                    ConstantDefinitions,
                    StaticFieldDefinitions,
                    Diagnostics,
                    CancellationToken);

                ConstructorDeclarationSyntax processedMethod = shaderSourceRewriter.Visit(constructorNode)!.WithoutTrivia();

                // Extracts the arguments from the list of parameters of the current method
                ArgumentSyntax[] ExtractArguments()
                {
                    using ImmutableArrayBuilder<ArgumentSyntax> arguments = new();

                    foreach (ParameterSyntax parameter in processedMethod.ParameterList.Parameters)
                    {
                        arguments.Add(Argument(IdentifierName(parameter.Identifier.Text)));
                    }

                    return arguments.ToArray();
                }

                // Create a static method acting as the constructor stub:
                //
                // static <TYPE_NAME> __ctor(<PARAMETERS>)
                // {
                //     <TYPE_NAME> __this = (<TYPE_NAME>)0;
                //
                //     __this.__ctor__init(<PARAMETERS>);
                //
                //     return __this;
                // }
                MethodDeclarationSyntax stubNode =
                    MethodDeclaration(IdentifierName(returnTypeHlslIdentifier), "__ctor")
                    .AddModifiers(Token(SyntaxKind.StaticKeyword))
                    .WithParameterList(processedMethod.ParameterList)
                    .AddBodyStatements(
                        LocalDeclarationStatement(
                            VariableDeclaration(IdentifierName(returnTypeHlslIdentifier)).AddVariables(
                                VariableDeclarator(Identifier("__this")).WithInitializer(EqualsValueClause(
                                    CastExpression(targetType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))))))),
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("__this"),
                                    IdentifierName("__ctor__init")))
                            .AddArgumentListArguments(ExtractArguments())),
                        ReturnStatement(IdentifierName("__this")));

                // Create the actual constructor to invoke, as an instance method:
                //
                // void __ctor__init(<PARAMETERS>)
                // {
                //     <CONSTRUCTOR_STATEMENTS>
                // }
                MethodDeclarationSyntax ctorNode =
                    MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), "__ctor__init")
                    .WithParameterList(processedMethod.ParameterList)
                    .WithBody(processedMethod.Body);

                this.constructors[constructor] = (stubNode, ctorNode);
            }

            // Rewrite the expression to invoke the rewritten constructor:
            //
            // <TYPE_NAME>::__ctor(...)
            return
                InvocationExpression(IdentifierName($"{returnTypeHlslIdentifier}::__ctor"))
                .WithArgumentList(updatedNode.ArgumentList!);
        }

        return base.VisitUserDefinedObjectCreationExpression(node, updatedNode, targetType);
    }

    /// <summary>
    /// Visits a static field access in an external type and rewrites it as needed.
    /// </summary>
    /// <param name="node">The current <see cref="SyntaxNode"/> instance for the static field.</param>
    /// <param name="fieldSymbol">The <see cref="IFieldSymbol"/> instance for <paramref name="node"/>.</param>
    /// <returns>The rewritten static field expression.</returns>
    [return: NotNullIfNotNull(nameof(node))]
    private SyntaxNode? VisitExternalStaticFieldAccess(SyntaxNode? node, IFieldSymbol fieldSymbol)
    {
        if (!StaticFieldDefinitions.TryGetValue(fieldSymbol, out (string Name, string, string?) fieldInfo))
        {
            // Execute the same logic as for shader static fields, to process them and extract the relevant info.
            // In this case, we use the fully qualified name of the field as identifier, not just the field name.
            if (HlslDefinitionsSyntaxProcessor.TryGetStaticField(
                this.shaderType,
                fieldSymbol,
                SemanticModel,
                DiscoveredTypes,
                ConstantDefinitions,
                StaticFieldDefinitions,
                Diagnostics,
                CancellationToken,
                out _,
                out string? typeDeclaration,
                out string? assignmentExpression,
                out _))
            {
                fieldInfo.Name = fieldSymbol.GetFullyQualifiedMetadataName().ToHlslIdentifierName();

                StaticFieldDefinitions.Add(fieldSymbol, (fieldInfo.Name, typeDeclaration, assignmentExpression));
            }
            else
            {
                // We failed to process the field for whatever reason. Just stop rewriting it and return
                // the current updated node. We'll have some diagnostic for this case emitted previously.
                return node;
            }
        }

        return IdentifierName(fieldInfo.Name);
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
    partial void TrackKnownPropertyAccess(IMemberReferenceOperation operation, MemberAccessExpressionSyntax node, string mappedName);

    /// <summary>
    /// Tracks a method invocation for a known HLSL method.
    /// </summary>
    /// <param name="metadataName">The metadata name of the method being invoked.</param>
    partial void TrackKnownMethodInvocation(string metadataName);

    /// <summary>
    /// Tracks an access to an external static field.
    /// </summary>
    /// <param name="staticFieldRewriter">The <see cref="StaticFieldRewriter"/> instance used to rewrite the field expression.</param>
    partial void TrackExternalStaticField(StaticFieldRewriter staticFieldRewriter);
}