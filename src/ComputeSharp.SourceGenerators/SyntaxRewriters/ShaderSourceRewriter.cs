using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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
        /// The current <see cref="MethodDeclarationSyntax"/> tree being visited.
        /// </summary>
        private MethodDeclarationSyntax? currentMethod;

        /// <summary>
        /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
        /// </summary>
        /// <param name="shaderType">The type symbol for the shader type.</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the target syntax tree.</param>
        /// <param name="discoveredTypes">The set of discovered custom types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        public ShaderSourceRewriter(
            INamedTypeSymbol shaderType,
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions)
        {
            this.shaderType = shaderType;
            this.semanticModel = semanticModel;
            this.discoveredTypes = discoveredTypes;
            this.staticMethods = staticMethods;
            this.constantDefinitions = constantDefinitions;
            this.localFunctions = new();
            this.implicitVariables = new();
        }

        /// <summary>
        /// Creates a new <see cref="ShaderSourceRewriter"/> instance with the specified parameters.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> instance for the target syntax tree.</param>
        /// <param name="discoveredTypes">The set of discovered custom types.</param>
        /// <param name="staticMethods">The set of discovered and processed static methods.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        public ShaderSourceRewriter(
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IMethodSymbol, MethodDeclarationSyntax> staticMethods,
            IDictionary<IFieldSymbol, string> constantDefinitions)
        {
            this.semanticModel = semanticModel;
            this.discoveredTypes = discoveredTypes;
            this.staticMethods = staticMethods;
            this.constantDefinitions = constantDefinitions;
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

            var updatedNode = (MethodDeclarationSyntax?)base.Visit(node);

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

            return updatedNode.ReplaceAndTrackType(updatedNode.Declaration.Type, node.Declaration.Type, this.semanticModel, this.discoveredTypes);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var updatedNode = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node)!;

            updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node, this.semanticModel, this.discoveredTypes);

            // New objects use the default HLSL cast syntax, eg. (float4)0
            if (updatedNode.ArgumentList!.Arguments.Count == 0)
            {
                return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            return InvocationExpression(updatedNode.Type, updatedNode.ArgumentList);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
        {
            var updatedNode = (ImplicitObjectCreationExpressionSyntax)base.VisitImplicitObjectCreationExpression(node)!;

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
            var updatedNode =
                ((LocalFunctionStatementSyntax)base.VisitLocalFunctionStatement(node)!)
                .WithBlockBody()
                .WithAttributeLists(List<AttributeListSyntax>())
                .WithIdentifier(Identifier($"__{this.currentMethod!.Identifier.Text}__{node.Identifier.Text}"));

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
                // If the current member access is to a constant, hardcode the value in HLSL
                if (operation.ConstantValue is { HasValue: true } and { Value: object value })
                {
                    return ParseExpression(value switch
                    {
                        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
                        _ => value.ToString()
                    });
                }

                // If the current member access is a field or property access, check the lookup table
                // to see if the member should be rewritten for HLSL compliance, and rewrite if needed.
                if (HlslKnownMembers.TryGetMappedName(operation.Member.ToDisplayString(), out string? mapping))
                {
                    // Static and instance members are handled differently, with static members being
                    // converted into a literal expression, and instance getting an updated identifier.
                    // For instance, consider these two cases:
                    //   - Vector3.One (static) => float3(1.0f, 1.0f, 1.0f)
                    //   - ThredIds.X (instance) => [arg].x
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
                        ShaderSourceRewriter shaderSourceRewriter = new(this.semanticModel, this.discoveredTypes, this.staticMethods, this.constantDefinitions);
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
                TypeSyntax typeSyntax = operation.Type.TrackType(this.discoveredTypes);
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

            if (this.semanticModel.GetOperation(node) is IFieldReferenceOperation operation &&
                operation.Field.IsConst &&
                !this.constantDefinitions.ContainsKey(operation.Field))
            {
                this.constantDefinitions.Add(operation.Field, operation.Field.ConstantValue!.ToString());
            }

            return updatedNode;
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
