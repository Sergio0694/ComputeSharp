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
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators.SyntaxRewriters
{
    /// <summary>
    /// A base <see cref="CSharpSyntaxRewriter"/> type that processes C# source to convert to HLSL compliant code.
    /// This class contains only the shared logic for all derived HLSL source rewriters.
    /// </summary>
    internal abstract class HlslSourceRewriter : CSharpSyntaxRewriter
    {
        /// <summary>
        /// Creates a new <see cref="HlslSourceRewriter"/> instance with the specified parameters.
        /// </summary>
        /// <param name="semanticModel">The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance for the target syntax tree.</param>
        /// <param name="discoveredTypes">The set of discovered custom types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="context">The current generator context in use.</param>
        protected HlslSourceRewriter(
            SemanticModel semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            GeneratorExecutionContext context)
        {
            SemanticModel = semanticModel;
            DiscoveredTypes = discoveredTypes;
            ConstantDefinitions = constantDefinitions;
            Context = context;
        }

        /// <summary>
        /// The <see cref="Microsoft.CodeAnalysis.SemanticModel"/> instance with semantic info on the target syntax tree.
        /// </summary>
        protected readonly SemanticModel SemanticModel;

        /// <summary>
        /// The collection of discovered custom types.
        /// </summary>
        protected readonly ICollection<INamedTypeSymbol> DiscoveredTypes;

        /// <summary>
        /// The collection of discovered constant definitions.
        /// </summary>
        protected readonly IDictionary<IFieldSymbol, string> ConstantDefinitions;

        /// <summary>
        /// The current generator context in use.
        /// </summary>
        protected readonly GeneratorExecutionContext Context;

        /// <inheritdoc/>
        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            var updatedNode = (CastExpressionSyntax)base.VisitCastExpression(node)!;

            return updatedNode.ReplaceAndTrackType(updatedNode.Type, node.Type, SemanticModel, DiscoveredTypes);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var updatedNode = (ObjectCreationExpressionSyntax)base.VisitObjectCreationExpression(node)!;

            if (SemanticModel.GetTypeInfo(node).Type is ITypeSymbol { IsUnmanagedType: false } type)
            {
                Context.ReportDiagnostic(InvalidObjectCreationExpression, node, type);
            }

            updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node, SemanticModel, DiscoveredTypes);

            // New objects use the default HLSL cast syntax, eg. (float4)0
            if (updatedNode.ArgumentList!.Arguments.Count == 0)
            {
                return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            // Add explicit casts for matrix constructors to help the overload resolution
            if (SemanticModel.GetTypeInfo(node).Type is ITypeSymbol matrixType &&
                HlslKnownTypes.IsMatrixType(matrixType.GetFullMetadataName()))
            {
                for (int i = 0; i < node.ArgumentList!.Arguments.Count; i++)
                {
                    IArgumentOperation argumentOperation = (IArgumentOperation)SemanticModel.GetOperation(node.ArgumentList.Arguments[i])!;
                    INamedTypeSymbol elementType = (INamedTypeSymbol)argumentOperation.Parameter.Type;

                    updatedNode = updatedNode.ReplaceNode(
                        updatedNode.ArgumentList!.Arguments[i].Expression,
                        CastExpression(IdentifierName(HlslKnownTypes.GetMappedName(elementType)), updatedNode.ArgumentList.Arguments[i].Expression));
                }
            }

            return InvocationExpression(updatedNode.Type, updatedNode.ArgumentList!);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
        {
            var updatedNode = (ImplicitObjectCreationExpressionSyntax)base.VisitImplicitObjectCreationExpression(node)!;

            if (SemanticModel.GetTypeInfo(node).Type is ITypeSymbol { IsUnmanagedType: false } type)
            {
                Context.ReportDiagnostic(InvalidObjectCreationExpression, node, type);
            }

            TypeSyntax explicitType = IdentifierName("").ReplaceAndTrackType(node, SemanticModel, DiscoveredTypes);

            // Mutate the syntax like with explicit object creation expressions
            if (updatedNode.ArgumentList!.Arguments.Count == 0)
            {
                return CastExpression(explicitType, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }

            // Add explicit casts like with the explicit object creation expressions above
            if (SemanticModel.GetTypeInfo(node).Type is ITypeSymbol matrixType &&
                HlslKnownTypes.IsMatrixType(matrixType.GetFullMetadataName()))
            {
                for (int i = 0; i < node.ArgumentList.Arguments.Count; i++)
                {
                    IArgumentOperation argumentOperation = (IArgumentOperation)SemanticModel.GetOperation(node.ArgumentList.Arguments[i])!;
                    INamedTypeSymbol elementType = (INamedTypeSymbol)argumentOperation.Parameter.Type;

                    updatedNode = updatedNode.ReplaceNode(
                        updatedNode.ArgumentList.Arguments[i].Expression,
                        CastExpression(IdentifierName(HlslKnownTypes.GetMappedName(elementType)), updatedNode.ArgumentList.Arguments[i].Expression));
                }
            }

            return InvocationExpression(explicitType, updatedNode.ArgumentList);
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            var updatedNode = (DefaultExpressionSyntax)base.VisitDefaultExpression(node)!;

            updatedNode = updatedNode.ReplaceAndTrackType(updatedNode.Type, node.Type, SemanticModel, DiscoveredTypes);

            // A default expression becomes (T)0 in HLSL
            return CastExpression(updatedNode.Type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            var updatedNode = (LiteralExpressionSyntax)base.VisitLiteralExpression(node)!;

            if (updatedNode.IsKind(SyntaxKind.DefaultLiteralExpression))
            {
                TypeSyntax type = node.TrackType(SemanticModel, DiscoveredTypes);

                // Same HLSL-style expression in the form (T)0
                return CastExpression(type, LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));
            }
            else if (updatedNode.IsKind(SyntaxKind.NumericLiteralExpression) &&
                     SemanticModel.GetOperation(node) is ILiteralOperation operation &&
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
            else if (updatedNode.IsKind(SyntaxKind.StringLiteralExpression))
            {
                Context.ReportDiagnostic(StringLiteralExpression, node);
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            var updatedNode = (ElementAccessExpressionSyntax)base.VisitElementAccessExpression(node)!;

            if (SemanticModel.GetOperation(node) is IPropertyReferenceOperation operation)
            {
                string propertyName = operation.Property.GetFullMetadataName();

                // Rewrite texture resource indices taking vectors into individual indices as per HLSL spec.
                // For instance: texture[ThreadIds.XY] will be rewritten as texture[ThreadIds.X, ThreadIds.Y].
                if (HlslKnownMembers.TryGetMappedResourceIndexerTypeName(propertyName, out string? mapping))
                {
                    var index = InvocationExpression(IdentifierName(mapping!), ArgumentList(updatedNode.ArgumentList.Arguments));

                    return updatedNode.WithArgumentList(BracketedArgumentList(SingletonSeparatedList(Argument(index))));
                }

                // If the current property is a swizzled matrix indexer, ensure all the arguments are constants, and rewrite
                // the property access to the corresponding HLSL syntax. For instance, m[M11, M12] will become m._m00_m01.
                if (HlslKnownMembers.IsKnownMatrixIndexer(propertyName))
                {
                    bool isValid = true;

                    // Validate the arguments
                    foreach (ArgumentSyntax argument in node.ArgumentList.Arguments)
                    {
                        if (SemanticModel.GetOperation(argument.Expression) is not IFieldReferenceOperation fieldReference ||
                            !HlslKnownMembers.IsKnownMatrixIndex(fieldReference.Field.GetFullMetadataName()))
                        {
                            Context.ReportDiagnostic(NonConstantMatrixSwizzledIndex, argument);

                            isValid = false;
                        }
                    }

                    if (isValid)
                    {
                        // Rewrite the indexer as a property access
                        string hlslPropertyName = string.Join("",
                            from argument in node.ArgumentList.Arguments
                            let fieldReference = (IFieldReferenceOperation)SemanticModel.GetOperation(argument.Expression)!
                            let fieldName = fieldReference.Field.Name
                            let row = (char)(fieldName[1] - 1)
                            let column = (char)(fieldName[2] - 1)
                            select $"_m{row}{column}");

                        return MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            updatedNode.Expression,
                            IdentifierName(hlslPropertyName));
                    }
                }
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
        {
            var updatedNode = (IdentifierNameSyntax)base.VisitIdentifierName(node)!;

            if (SemanticModel.GetOperation(node) is IFieldReferenceOperation operation &&
                operation.Field.IsConst &&
                operation.Type.TypeKind != TypeKind.Enum)
            {
                ConstantDefinitions[operation.Field] = ((IFormattable)operation.Field.ConstantValue!).ToString(null, CultureInfo.InvariantCulture);

                var ownerTypeName = ((INamedTypeSymbol)operation.Field.ContainingSymbol).ToDisplayString().Replace(".", "__");
                var constantName = $"__{ownerTypeName}__{operation.Field.Name}";

                return IdentifierName(constantName);
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
        {
            var updatedNode = (AnonymousObjectCreationExpressionSyntax)base.VisitAnonymousObjectCreationExpression(node)!;

            Context.ReportDiagnostic(DiagnosticDescriptors.AnonymousObjectCreationExpression, node);

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitAwaitExpression(AwaitExpressionSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.AwaitExpression, node);

            return base.VisitAwaitExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.CheckedExpression, node);

            return base.VisitCheckedExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitCheckedStatement(CheckedStatementSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.CheckedStatement, node);

            return base.VisitCheckedStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitFixedStatement(FixedStatementSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.FixedStatement, node);

            return base.VisitFixedStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitForEachStatement(ForEachStatementSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.ForEachStatement, node);

            return base.VisitForEachStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.ForEachStatement, node);

            return base.VisitForEachVariableStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitLockStatement(LockStatementSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.LockStatement, node);

            return base.VisitLockStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitQueryExpression(QueryExpressionSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.QueryExpression, node);

            return base.VisitQueryExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRangeExpression(RangeExpressionSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.RangeExpression, node);

            return base.VisitRangeExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRecursivePattern(RecursivePatternSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.RecursivePattern, node);

            return base.VisitRecursivePattern(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRefType(RefTypeSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.RefType, node);

            return base.VisitRefType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRelationalPattern(RelationalPatternSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.RelationalPattern, node);

            return base.VisitRelationalPattern(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitSizeOfExpression(SizeOfExpressionSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.SizeOfExpression, node);

            return base.VisitSizeOfExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.StackAllocArrayCreationExpression, node);

            return base.VisitStackAllocArrayCreationExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitThrowExpression(ThrowExpressionSyntax node)
        {
            Context.ReportDiagnostic(ThrowExpressionOrStatement, node);

            return base.VisitThrowExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitThrowStatement(ThrowStatementSyntax node)
        {
            Context.ReportDiagnostic(ThrowExpressionOrStatement, node);

            return base.VisitThrowStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitTryStatement(TryStatementSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.TryStatement, node);

            return base.VisitTryStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitTupleType(TupleTypeSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.TupleType, node);

            return base.VisitTupleType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitUsingStatement(UsingStatementSyntax node)
        {
            Context.ReportDiagnostic(UsingStatementOrDeclaration, node);

            return base.VisitUsingStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            var updatedNode = (VariableDeclaratorSyntax)base.VisitVariableDeclarator(node)!;

            if (node.Initializer is null &&
                node.Parent is VariableDeclarationSyntax declaration &&
                SemanticModel.GetTypeInfo(declaration.Type).Type is ITypeSymbol { IsUnmanagedType: false } type)
            {
                Context.ReportDiagnostic(InvalidObjectDeclaration, node, type);
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitYieldStatement(YieldStatementSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.YieldStatement, node);

            return base.VisitYieldStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitFunctionPointerType(FunctionPointerTypeSyntax node)
        {
            Context.ReportDiagnostic(FunctionPointer, node);

            return base.VisitFunctionPointerType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitPointerType(PointerTypeSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.PointerType, node);

            return base.VisitPointerType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            Context.ReportDiagnostic(DiagnosticDescriptors.UnsafeStatement, node);

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
