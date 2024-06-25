using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <summary>
/// A processor responsible for processing special HLSL operators.
/// </summary>
internal static class HlslOperatorsSyntaxProcessor
{
    /// <summary>
    /// Tries to process a custom operator from an input expression node.
    /// </summary>
    /// <param name="originalNode">The original syntax node being processed.</param>
    /// <param name="updatedNode">The updated node to propagate and potentially rewrite.</param>
    /// <param name="semanticModel">The <see cref="SemanticModel"/> instance to use.</param>
    /// <param name="token">The cancellation token for the operation.</param>
    /// <param name="rewrittenNode">The rewritten node, if successfully produced.</param>
    /// <returns>Whether a rewritten node was produced.</returns>
    public static bool TryProcessCustomOperator(
        ExpressionSyntax originalNode,
        ExpressionSyntax updatedNode,
        SemanticModel semanticModel,
        CancellationToken token,
        [NotNullWhen(true)] out ExpressionSyntax? rewrittenNode)
    {
        // Process binary operations to see if the target operator method is an intrinsic
        if (semanticModel.GetOperation(originalNode, token) is not IBinaryOperation operation)
        {
            rewrittenNode = null;

            return false;
        }

        // Pre-filter candidates based on the operation type, to minimize allocations
        if (!HlslKnownOperators.IsCandidateOperator(operation.OperatorKind))
        {
            rewrittenNode = null;

            return false;
        }

        // Get the binary nodes, in case we need them for rewriting
        GetBinaryNodes(updatedNode, out ExpressionSyntax leftNode, out ExpressionSyntax rightNode);

        // First, try to handle unsigned right shift operators, which are a special case.
        // This is because they require explicit rewriting, and not just a change in method name.
        // We need to do this after pre-filtering, as this is also supported on 'int' and 'uint'.
        if (operation.OperatorKind == BinaryOperatorKind.UnsignedRightShift)
        {
            string typeName = operation.LeftOperand.Type!.GetFullyQualifiedMetadataName();

            // If the left operand is already unsigned, there's no rewriting to do other
            // than just replacing '>>>' with '>>', since the former doesn't exist in HLSL.
            if (HlslKnownTypes.IsKnownUnsignedIntegerType(typeName))
            {
                rewrittenNode = BinaryExpression(SyntaxKind.RightShiftExpression, leftNode, rightNode);

                return true;
            }

            // Rewrite the syntax tree as follows:
            //
            // x >>> y => (TYPE_X)((UNSIGNED_TYPE_X)x >> y)
            if (HlslKnownTypes.IsKnownSignedIntegerType(typeName))
            {
                string typeNameOfLeftOperand = HlslKnownTypes.GetMappedName(typeName);
                string unsignedTypeName = 'u' + typeNameOfLeftOperand;

                rewrittenNode =
                    CastExpression(
                        IdentifierName(typeNameOfLeftOperand),
                        ParenthesizedExpression(
                            BinaryExpression(
                                SyntaxKind.RightShiftExpression,
                                CastExpression(IdentifierName(unsignedTypeName), leftNode),
                                rightNode)));

                return true;
            }

            // All other cases are unsupported, so just return the original tree
            rewrittenNode = null;

            return false;
        }

        // Also pre-filter just operators that are defined in ComputeSharp primitives
        if (operation is not { OperatorMethod: { ContainingType.ContainingNamespace.Name: "ComputeSharp" } method })
        {
            rewrittenNode = null;

            return false;
        }

        // If the operator is indeed an HLSL overload, replace the expression with an invocation.
        // That is, do the following transformation:
        //
        // x * y => <INTRINSIC>(x, y)
        if (HlslKnownOperators.TryGetMappedName(method.GetFullyQualifiedMetadataName(), method.Parameters.Select(static p => p.Type.Name), out string? mapped))
        {
            rewrittenNode =
                InvocationExpression(IdentifierName(mapped!))
                .AddArgumentListArguments(
                    Argument(leftNode),
                    Argument(rightNode));

            return true;
        }

        rewrittenNode = null;

        return false;
    }

    /// <summary>
    /// Extracts the binary children from an expression node.
    /// </summary>
    /// <param name="node">The input expression node.</param>
    /// <param name="left">The resulting left child node.</param>
    /// <param name="right">The resulting right child node.</param>
    /// <exception cref="NotSupportedException">Thrown if <paramref name="node"/> is of an unsupported type.</exception>
    private static void GetBinaryNodes(
        ExpressionSyntax node,
        out ExpressionSyntax left,
        out ExpressionSyntax right)
    {
        switch (node)
        {
            case BinaryExpressionSyntax binaryExpression:
                left = binaryExpression.Left;
                right = binaryExpression.Right;
                break;
            case AssignmentExpressionSyntax assignmentExpression:
                left = assignmentExpression.Left;
                right = assignmentExpression.Right;
                break;
            default:
                throw new NotSupportedException("Invalid input expression node.");
        }
    }
}