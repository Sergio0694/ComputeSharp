using System;
using System.Collections.Generic;
using System.Globalization;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Extensions;
using ComputeSharp.SourceGenerators.Helpers;
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
    /// A custom <see cref="CSharpSyntaxRewriter"/> type that processes C# static field to convert to HLSL static fields (possibly constant).
    /// </summary>
    internal sealed class StaticFieldRewriter : HlslSourceRewriter
    {
        /// <summary>
        /// Creates a new <see cref="StaticFieldRewriter"/> instance with the specified parameters.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModelProvider"/> instance for the target syntax tree.</param>
        /// <param name="discoveredTypes">The set of discovered custom types.</param>
        /// <param name="constantDefinitions">The collection of discovered constant definitions.</param>
        /// <param name="context">The current generator context in use.</param>
        public StaticFieldRewriter(
            SemanticModelProvider semanticModel,
            ICollection<INamedTypeSymbol> discoveredTypes,
            IDictionary<IFieldSymbol, string> constantDefinitions,
            GeneratorExecutionContext context)
            : base(semanticModel, discoveredTypes, constantDefinitions, context)
        {
        }

        /// <inheritdoc cref="CSharpSyntaxRewriter.Visit(SyntaxNode?)"/>
        public ExpressionSyntax? Visit(VariableDeclaratorSyntax? node)
        {
            if (node?.Initializer is EqualsValueClauseSyntax fieldInitializer)
            {
                return ((EqualsValueClauseSyntax)Visit(fieldInitializer))!.Value;
            }

            return null;
        }

        /// <inheritdoc/>
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var updatedNode = (MemberAccessExpressionSyntax)base.VisitMemberAccessExpression(node)!;

            if (node.IsKind(SyntaxKind.SimpleMemberAccessExpression) &&
                SemanticModel.For(node).GetOperation(node) is IMemberReferenceOperation operation)
            {
                // Track and replace constants
                if (operation is IFieldReferenceOperation fieldOperation &&
                    fieldOperation.Field.IsConst &&
                    fieldOperation.Type!.TypeKind != TypeKind.Enum)
                {
                    ConstantDefinitions[fieldOperation.Field] = ((IFormattable)fieldOperation.Field.ConstantValue!).ToString(null, CultureInfo.InvariantCulture);

                    var ownerTypeName = ((INamedTypeSymbol)fieldOperation.Field.ContainingSymbol).ToDisplayString().ToHlslIdentifierName();
                    var constantName = $"__{ownerTypeName}__{fieldOperation.Field.Name}";

                    return IdentifierName(constantName);
                }

                if (HlslKnownMembers.TryGetMappedName(operation.Member.ToDisplayString(), out string? mapping))
                {
                    if (operation.Member.IsStatic)
                    {
                        string typeName = operation.Member.ContainingType.GetFullMetadataName();

                        // Special dispatch types are not supported from static constants
                        DiagnosticDescriptor? descriptor = typeName switch
                        {
                            _ when typeName == typeof(ThreadIds).FullName || typeName == typeof(ThreadIds.Normalized).FullName => InvalidThreadIdsUsage,
                            _ when typeName == typeof(GroupIds).FullName => InvalidGroupIdsUsage,
                            _ when typeName == typeof(GroupSize).FullName => InvalidGroupSizeUsage,
                            _ when typeName == typeof(GridIds).FullName => InvalidGridIdsUsage,
                            _ when typeName == typeof(DispatchSize).FullName => InvalidDispatchSizeUsage,
                            _ => null
                        };

                        if (descriptor is not null)
                        {
                            Context.ReportDiagnostic(descriptor, node);
                        }
                    }

                    // Rewrite static and instance mapped members
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
                // Rewrite HLSL intrinsic methods
                if (HlslKnownMethods.TryGetMappedName(method.GetFullMetadataName(), out string? mapping))
                {
                    return updatedNode.WithExpression(ParseExpression(mapping!));
                }
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitArgument(ArgumentSyntax node)
        {
            var updatedNode = (ArgumentSyntax)base.VisitArgument(node)!;

            updatedNode = updatedNode.WithRefKindKeyword(Token(SyntaxKind.None));

            return updatedNode;
        }
    }
}
