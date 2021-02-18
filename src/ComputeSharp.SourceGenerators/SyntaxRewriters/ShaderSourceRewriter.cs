using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators.SyntaxRewriters
{
    /// <summary>
    /// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# methods to convert to HLSL compliant code.
    /// </summary>
    internal sealed class ShaderSourceRewriter : CSharpSyntaxRewriter
    {
        /// <summary>
        /// The type symbol for the shader type.
        /// </summary>
        private readonly INamedTypeSymbol? shaderType;

        /// <summary>
        /// The <see cref="SemanticModel"/> instance with semantic info on the target syntax tree.
        /// </summary>
        private readonly SemanticModel semanticModel;

        /// <summary>
        /// The collection of discovered custom types.
        /// </summary>
        private readonly ICollection<INamedTypeSymbol> discoveredTypes;

        /// <summary>
        /// The collection of discovered static methods.
        /// </summary>
        private readonly IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods;

        /// <summary>
        /// The collection of discovered constant definitions.
        /// </summary>
        private readonly IDictionary<IFieldSymbol, string> constantDefinitions;

        /// <summary>
        /// The collection of processed local functions in the current tree.
        /// </summary>
        private readonly Dictionary<string, LocalFunctionStatementSyntax> localFunctions;

        /// <summary>
        /// The list of implicit variables to declare at the start of the body.
        /// </summary>
        private readonly List<VariableDeclarationSyntax> implicitVariables;

        /// <summary>
        /// The current generator context in use.
        /// </summary>
        private readonly GeneratorExecutionContext context;

        /// <summary>
        /// Whether or not the current instance is processing a shader entry point.
        /// </summary>
        private readonly bool isEntryPoint;

        /// <summary>
        /// The current <see cref="MethodDeclarationSyntax"/> tree being visited.
        /// </summary>
        private MethodDeclarationSyntax? currentMethod;

        /// <summary>
        /// The current depth inside local declarations.
        /// </summary>
        private int localFunctionDepth;

        /// <summary>
        /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
        /// </summary>
        /// <param name="shaderType">The type symbol for the shader type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the target syntax tree.</param>
        /// <param name="discoveredTypes">The set of discovered custom types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="context">The current generator context in use.</param>
        /// <param name="isEntryPoint">Whether or not the current instance is processing a shader entry point.</param>
        public ShaderSourceRewriter(
            INamedTypeSymbol shaderType,
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            GeneratorExecutionContext context,
            bool isEntryPoint)
        {
            this.shaderType = shaderType;
            this.semanticModel = semanticModel;
            this.discoveredTypes = discoveredTypes;
            this.staticMethods = staticMethods;
            this.constantDefinitions = constantDefinitions;
            this.localFunctions = new();
            this.implicitVariables = new();
            this.context = context;
            this.isEntryPoint = isEntryPoint;
        }

        /// <summary>
        /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the target syntax tree.</param>
        /// <param name="discoveredTypes">The set of discovered custom types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="context">The current generator context in use.</param>
        public ShaderSourceRewriter(
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            GeneratorExecutionContext context)
        {
            this.semanticModel = semanticModel;
            this.discoveredTypes = discoveredTypes;
            this.staticMethods = staticMethods;
            this.constantDefinitions = constantDefinitions;
            this.implicitVariables = new();
            this.localFunctions = new();
            this.context = context;
        }

        /// <summary>
        /// Gets the collection of processed local functions in the current tree.
        /// </summary>
        public IReadOnlyDictionary<string, LocalFunctionStatementSyntax> LocalFunctions => this.localFunctions;

        /// <summary>
        /// Gets whether or not the shader uses <see cref="GroupIds"/> at least once (except <see cref="GroupIds.Index"/>).
        /// </summary>
        public bool IsGroupIdsUsed { get; private set; }

        /// <summary>
        /// Gets whether or not the shader uses <see cref="GroupIds.Index/> at least once.
        /// </summary>
        public bool IsGroupIdsIndexUsed { get; private set; }

        /// <summary>
        /// Gets whether or not the shader uses <see cref="WarpIds"/> at least once.
        /// </summary>
        public bool IsWarpIdsUsed { get; set; }

        /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
        public MethodDeclarationSyntax? Visit(MethodDeclarationSyntax? node)
        {
            this.currentMethod = node;

            var updatedNode = (MethodDeclarationSyntax?)base.Visit(node);

            if (node!.Modifiers.Any(m => m.IsKind(SyntaxKind.AsyncKeyword)))
            {
                this.context.ReportDiagnostic(AsyncModifierOnMethodOrFunction, node);
            }

            if (node!.Modifiers.Any(m => m.IsKind(SyntaxKind.UnsafeKeyword)))
            {
                this.context.ReportDiagnostic(UnsafeModifierOnMethodOrFunction, node);
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
                .ReplaceAndTrackType(updatedNode.Type!, node.Type!, this.semanticModel, this.discoveredTypes)
                .WithModifiers(TokenList(modifier));
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            var updatedNode = (CastExpressionSyntax)base.VisitCastExpression(node)!;

            return updatedNode.ReplaceAndTrackType(updatedNode.Type, node.Type, this.semanticModel, this.discoveredTypes);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            var updatedNode = ((LocalDeclarationStatementSyntax)base.VisitLocalDeclarationStatement(node)!);

            if (this.semanticModel.GetOperation(node) is IOperation { Kind: OperationKind.UsingDeclaration })
            {
                this.context.ReportDiagnostic(UsingStatementOrDeclaration, node);
            }

            return updatedNode.ReplaceAndTrackType(updatedNode.Declaration.Type, node.Declaration.Type, this.semanticModel, this.discoveredTypes);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var updatedNode = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node)!;

            if (this.semanticModel.GetTypeInfo(node).Type is ITypeSymbol { IsUnmanagedType: false } type)
            {
                this.context.ReportDiagnostic(InvalidObjectCreationExpression, node, type);
            }

            updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node, this.semanticModel, this.discoveredTypes);

            // New objects use the default HLSL cast syntax, eg. (float4)0
            if (updatedNode.ArgumentList!.Arguments.Count == 0)
            {
                return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            return InvocationExpression(updatedNode.Type, updatedNode.ArgumentList);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
        {
            var updatedNode = (AnonymousObjectCreationExpressionSyntax)base.VisitAnonymousObjectCreationExpression(node)!;

            this.context.ReportDiagnostic(DiagnosticDescriptors.AnonymousObjectCreationExpression, node);

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
        {
            var updatedNode = (ImplicitObjectCreationExpressionSyntax)base.VisitImplicitObjectCreationExpression(node)!;

            if (this.semanticModel.GetTypeInfo(node).Type is ITypeSymbol { IsUnmanagedType: false } type)
            {
                this.context.ReportDiagnostic(InvalidObjectCreationExpression, node, type);
            }

            TypeSyntax explicitType = IdentifierName("").ReplaceAndTrackType(node, this.semanticModel, this.discoveredTypes);

            // Mutate the syntax like with explicit object creation expressions
            if (updatedNode.ArgumentList!.Arguments.Count == 0)
            {
                return CastExpression(explicitType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            return InvocationExpression(explicitType, updatedNode.ArgumentList);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            var updatedNode = (DefaultExpressionSyntax)base.VisitDefaultExpression(node)!;

            updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node.Type, this.semanticModel, this.discoveredTypes);

            // A default expression becomes (T)0 in HLSL
            return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            var updatedNode = (LiteralExpressionSyntax)base.VisitLiteralExpression(node)!;

            if (updatedNode.IsKind(SyntaxKind.DefaultLiteralExpression))
            {
                TypeSyntax type = node.TrackType(this.semanticModel, this.discoveredTypes);

                // Same HLSL-style expression in the form (T)0
                return CastExpression(type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }
            else if (updatedNode.IsKind(SyntaxKind.NumericLiteralExpression) &&
                     this.semanticModel.GetOperation(node) is ILiteralOperation operation &&
                     operation.Type is INamedTypeSymbol type)
            {
                // If the expression is a literal floating point value, we need to ensure the proper suffixes are
                // used in the HLSL representation. Floating point values accept either f or F, but they don't work
                // when the literal doesn't contain a decimal point. Since 32 bit floating point values are the default
                // in HLSL, we can remove the suffix entirely. As for 64 bit values, we simply use the 'L' suffix.
                if (type.GetFullMetadataName().Equals(typeof(float).FullName))
                {
                    string literal = updatedNode.Token.ValueText;

                    if (!literal.Contains('.')) literal += ".0";

                    return updatedNode.WithToken(Literal(literal, 0f));
                }
                else if (type.GetFullMetadataName().Equals(typeof(double).FullName))
                {
                    return updatedNode.WithToken(Literal(updatedNode.Token.ValueText + "L", 0d));
                }
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitDeclarationExpression(DeclarationExpressionSyntax node)
        {
            var updatedNode = (DeclarationExpressionSyntax)base.VisitDeclarationExpression(node)!;

            updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node.Type, this.semanticModel, this.discoveredTypes);

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
            this.localFunctionDepth++;

            var updatedNode =
                ((LocalFunctionStatementSyntax)base.VisitLocalFunctionStatement(node)!)
                .WithBlockBody()
                .WithAttributeLists(List<AttributeListSyntax>())
                .WithIdentifier(Identifier($"__{this.currentMethod!.Identifier.Text}__{node.Identifier.Text}"));

            if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.AsyncKeyword)))
            {
                this.context.ReportDiagnostic(AsyncModifierOnMethodOrFunction, node);
            }

            if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.UnsafeKeyword)))
            {
                this.context.ReportDiagnostic(UnsafeModifierOnMethodOrFunction, node);
            }

            this.localFunctionDepth--;

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
                this.semanticModel.GetOperation(node) is IMemberReferenceOperation operation)
            {
                // If the member access is a constant, track it and replace the tree with the processed constant name
                if (operation is IFieldReferenceOperation fieldOperation && fieldOperation.Field.IsConst)
                {
                    this.constantDefinitions[fieldOperation.Field] = ((IFormattable)fieldOperation.Field.ConstantValue!).ToString(null, CultureInfo.InvariantCulture);

                    var ownerTypeName = ((INamedTypeSymbol)fieldOperation.Field.ContainingSymbol).ToDisplayString().Replace(".", "__");
                    var constantName = $"__{ownerTypeName}__{fieldOperation.Field.Name}";

                    return IdentifierName(constantName);
                }

                // If the current member access is a field or property access, check the lookup table
                // to see if the member should be rewritten for HLSL compliance, and rewrite if needed.
                if (HlslKnownMembers.TryGetMappedName(operation.Member.ToDisplayString(), out string? mapping))
                {
                    // Mark which dispatch properties have been used, to optimize the declaration afterwards
                    if (operation.Member.IsStatic)
                    {
                        string typeName = operation.Member.ContainingType.GetFullMetadataName();

                        if (mapping == $"__{nameof(GroupIds)}__get_Index") IsGroupIdsIndexUsed = true;
                        else if (typeName == typeof(GroupIds).FullName) IsGroupIdsUsed = true;
                        else if (typeName == typeof(WarpIds).FullName) IsWarpIdsUsed = true;

                        // Check that the dispatch info types are only used from the main shader body
                        if (!this.isEntryPoint || this.localFunctionDepth > 0)
                        {
                            DiagnosticDescriptor? descriptor = typeName switch
                            {
                                _ when typeName == typeof(ThreadIds).FullName => DiagnosticDescriptors.InvalidThreadIdsUsage,
                                _ when typeName == typeof(GroupIds).FullName => DiagnosticDescriptors.InvalidGroupIdsUsage,
                                _ when typeName == typeof(GroupSize).FullName => DiagnosticDescriptors.InvalidGroupSizeUsage,
                                _ when typeName == typeof(WarpIds).FullName => DiagnosticDescriptors.InvalidWarpIdsUsage,
                                _ => null
                            };

                            if (descriptor is not null)
                            {
                                this.context.ReportDiagnostic(descriptor, node);
                            }
                        }
                    }

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
                    HlslKnownMembers.TryGetAccessorRankAndAxis(propertyOperation.Property.GetFullMetadataName(), out int rank, out int axis))
                {
                    IMethodSymbol key = propertyOperation.Property.GetMethod!;

                    if (!this.staticMethods.TryGetValue(key, out MethodDeclarationSyntax? methodSyntax))
                    {
                        INamedTypeSymbol resourceType = (INamedTypeSymbol)this.semanticModel.GetTypeInfo(node.Expression).Type!;
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
            var updatedNode = (InvocationExpressionSyntax)base.VisitInvocationExpression(node)!;

            if (this.semanticModel.GetOperation(node) is IInvocationOperation operation &&
                operation.TargetMethod is IMethodSymbol method &&
                method.IsStatic)
            {
                // If the invocation consists of invoking a static method that has a direct
                // mapping to HLSL, rewrite the expression in the current invocation node.
                // For instance: Math.Abs(expr) => abs(expr).
                if (HlslKnownMethods.TryGetMappedName(method.GetFullMetadataName(), out string? mapping))
                {
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
                    var methodIdentifier = method.GetFullMetadataName().Replace(".", "__");

                    if (!this.staticMethods.ContainsKey(method))
                    {
                        ShaderSourceRewriter shaderSourceRewriter = new(this.semanticModel, this.discoveredTypes, this.staticMethods, this.constantDefinitions, this.context);
                        MethodDeclarationSyntax
                            methodNode = (MethodDeclarationSyntax)method.DeclaringSyntaxReferences[0].GetSyntax(),
                            processedMethod = shaderSourceRewriter.Visit(methodNode)!.NormalizeWhitespace().WithoutTrivia();

                        this.staticMethods.Add(method, processedMethod.WithIdentifier(Identifier(methodIdentifier)));
                    }

                    return updatedNode.WithExpression(ParseExpression(methodIdentifier));
                }
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            var updatedNode = (ElementAccessExpressionSyntax)base.VisitElementAccessExpression(node)!;

            if (this.semanticModel.GetOperation(node) is IPropertyReferenceOperation operation &&
                HlslKnownMembers.TryGetMappedIndexerTypeName(operation.Property.GetFullMetadataName(), out string? mapping))
            {
                var index = InvocationExpression(IdentifierName(mapping!), ArgumentList(updatedNode.ArgumentList.Arguments));

                return updatedNode.WithArgumentList(BracketedArgumentList(SingletonSeparatedList(Argument(index))));
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
            if (this.semanticModel.GetOperation(node.Expression) is IDiscardOperation operation)
            {
                TypeSyntax typeSyntax = operation.Type!.TrackType(this.discoveredTypes);
                string identifier = $"__implicit{this.implicitVariables.Count}";

                // Add the variable to the list of implicit declarations
                this.implicitVariables.Add(VariableDeclaration(typeSyntax).AddVariables(VariableDeclarator(identifier)));

                return updatedNode.WithExpression(IdentifierName(identifier));
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
        {
            var updatedNode = (IdentifierNameSyntax)base.VisitIdentifierName(node)!;

            if (this.semanticModel.GetOperation(node) is IFieldReferenceOperation operation && operation.Field.IsConst)
            {
                this.constantDefinitions[operation.Field] = ((IFormattable)operation.Field.ConstantValue!).ToString(null, CultureInfo.InvariantCulture);

                var ownerTypeName = ((INamedTypeSymbol)operation.Field.ContainingSymbol).ToDisplayString().Replace(".", "__");
                var constantName = $"__{ownerTypeName}__{operation.Field.Name}";

                return IdentifierName(constantName);
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitAwaitExpression(AwaitExpressionSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.AwaitExpression, node);

            return base.VisitAwaitExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.CheckedExpression, node);

            return base.VisitCheckedExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitCheckedStatement(CheckedStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.CheckedStatement, node);

            return base.VisitCheckedStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitFixedStatement(FixedStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.FixedStatement, node);

            return base.VisitFixedStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitForEachStatement(ForEachStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.ForEachStatement, node);

            return base.VisitForEachStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.ForEachStatement, node);

            return base.VisitForEachVariableStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitLockStatement(LockStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.LockStatement, node);

            return base.VisitLockStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitQueryExpression(QueryExpressionSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.QueryExpression, node);

            return base.VisitQueryExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRangeExpression(RangeExpressionSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.RangeExpression, node);

            return base.VisitRangeExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRecursivePattern(RecursivePatternSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.RecursivePattern, node);

            return base.VisitRecursivePattern(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRefType(RefTypeSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.RefType, node);

            return base.VisitRefType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRelationalPattern(RelationalPatternSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.RelationalPattern, node);

            return base.VisitRelationalPattern(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitSizeOfExpression(SizeOfExpressionSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.SizeOfExpression, node);

            return base.VisitSizeOfExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.StackAllocArrayCreationExpression, node);

            return base.VisitStackAllocArrayCreationExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitThrowExpression(ThrowExpressionSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.ThrowExpressionOrStatement, node);

            return base.VisitThrowExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitThrowStatement(ThrowStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.ThrowExpressionOrStatement, node);

            return base.VisitThrowStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitTryStatement(TryStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.TryStatement, node);

            return base.VisitTryStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitTupleType(TupleTypeSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.TupleType, node);

            return base.VisitTupleType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitUsingStatement(UsingStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.UsingStatementOrDeclaration, node);

            return base.VisitUsingStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            var updatedNode = (VariableDeclaratorSyntax)base.VisitVariableDeclarator(node)!;

            if (node.Initializer is null &&
                node.Parent is VariableDeclarationSyntax declaration &&
                this.semanticModel.GetTypeInfo(declaration.Type).Type is ITypeSymbol { IsUnmanagedType: false } type)
            {
                this.context.ReportDiagnostic(InvalidObjectDeclaration, node, type);
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitYieldStatement(YieldStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.YieldStatement, node);

            return base.VisitYieldStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitFunctionPointerType(FunctionPointerTypeSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.FunctionPointer, node);

            return base.VisitFunctionPointerType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitPointerType(PointerTypeSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.PointerType, node);

            return base.VisitPointerType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            this.context.ReportDiagnostic(DiagnosticDescriptors.UnsafeStatement, node);

            return base.VisitUnsafeStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            SyntaxToken updatedToken = base.VisitToken(token);

            // Replace all identifier tokens when needed, to avoid colliding with HLSL keywords
            if (updatedToken.IsKind(SyntaxKind.IdentifierToken) &&
                HlslKnownKeywords.TryGetMappedName(updatedToken.Text, out string? mapped))
            {
                return ParseToken(mapped!);
            }

            return updatedToken.WithoutTrivia();
        }
    }
}
