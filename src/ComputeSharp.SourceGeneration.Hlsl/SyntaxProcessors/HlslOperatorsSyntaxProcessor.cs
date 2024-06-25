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
        // Make sure we can correctly retrieve an operation first
        if (semanticModel.GetOperation(originalNode, token) is not IOperation operation)
        {
            rewrittenNode = null;

            return false;
        }

        // Next, try to get the operation info
        if (!TryGetBinaryOperationInfo(
            operation,
            out BinaryOperatorKind operationKind,
            out IOperation? leftOperation,
            out IOperation? rightOperation,
            out IMethodSymbol? operatorMethod))
        {
            rewrittenNode = null;

            return false;
        }

        // Pre-filter candidates based on the operation type, to minimize allocations
        if (!HlslKnownOperators.IsCandidateOperator(operationKind))
        {
            rewrittenNode = null;

            return false;
        }

        // Get the binary nodes, in case we need them for rewriting
        GetBinaryNodes(updatedNode, out ExpressionSyntax leftNode, out ExpressionSyntax rightNode);

        // First, try to handle unsigned right shift operators, which are a special case.
        // This is because they require explicit rewriting, and not just a change in method name.
        // We need to do this after pre-filtering, as this is also supported on 'int' and 'uint'.
        if (operationKind == BinaryOperatorKind.UnsignedRightShift)
        {
            string typeName = leftOperation.Type!.GetFullyQualifiedMetadataName();

            // If the left operand is already unsigned, there's no rewriting to do other
            // than just replacing '>>>' with '>>', since the former doesn't exist in HLSL.
            if (HlslKnownTypes.IsKnownUnsignedIntegerType(typeName))
            {
                rewrittenNode = BinaryExpression(SyntaxKind.RightShiftExpression, leftNode, rightNode);

                WrapRewrittenNodeIfNeeded(updatedNode, operation, ref rewrittenNode);

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

                WrapRewrittenNodeIfNeeded(updatedNode, operation, ref rewrittenNode);

                return true;
            }

            // All other cases are unsupported, so just return the original tree
            rewrittenNode = null;

            return false;
        }

        // Also pre-filter just operators that are defined in ComputeSharp primitives
        if (operatorMethod is not { ContainingType.ContainingNamespace.Name: "ComputeSharp" })
        {
            rewrittenNode = null;

            return false;
        }

        // If the operator is indeed an HLSL overload, replace the expression with an invocation.
        // That is, do the following transformation:
        //
        // x * y => <INTRINSIC>(x, y)
        if (HlslKnownOperators.TryGetMappedName(
            operatorMethod.GetFullyQualifiedMetadataName(),
            operatorMethod.Parameters.Select(static p => p.Type.Name),
            out string? mapped))
        {
            rewrittenNode =
                InvocationExpression(IdentifierName(mapped!))
                .AddArgumentListArguments(
                    Argument(leftNode),
                    Argument(rightNode));

            WrapRewrittenNodeIfNeeded(updatedNode, operation, ref rewrittenNode);

            return true;
        }

        rewrittenNode = null;

        return false;
    }

    /// <summary>
    /// Tries to get the info for a given binary operation.
    /// </summary>
    /// <param name="operation">The input operation to extract info from.</param>
    /// <param name="operationKind">The resulting kind for <paramref name="operation"/>.</param>
    /// <param name="leftOrTargetOperation">The resulting left child operation, or the target.</param>
    /// <param name="rightOperation">The resulting right child operation.</param>
    /// <param name="targetMethod">The target method being invoked, if any.</param>
    /// <returns>Whether the input operation is supported and info could be retrieved.</returns>
    private static bool TryGetBinaryOperationInfo(
        IOperation operation,
        out BinaryOperatorKind operationKind,
        [NotNullWhen(true)] out IOperation? leftOrTargetOperation,
        [NotNullWhen(true)] out IOperation? rightOperation,
        out IMethodSymbol? targetMethod)
    {
        switch (operation)
        {
            case IBinaryOperation binaryOperation:
                operationKind = binaryOperation.OperatorKind;
                leftOrTargetOperation = binaryOperation.LeftOperand;
                rightOperation = binaryOperation.RightOperand;
                targetMethod = binaryOperation.OperatorMethod;

                return true;
            case ICompoundAssignmentOperation assignmentOperation:
                operationKind = assignmentOperation.OperatorKind;
                leftOrTargetOperation = assignmentOperation.Target;
                rightOperation = assignmentOperation.Value;
                targetMethod = assignmentOperation.OperatorMethod;

                return true;
            default:
                operationKind = BinaryOperatorKind.None;
                leftOrTargetOperation = null;
                rightOperation = null;
                targetMethod = null;

                return false;
        }
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

    /// <summary>
    /// Wraps a rewritten node if needed, depending on the operation type.
    /// </summary>
    /// <param name="updatedNode">The updated node to propagate and potentially rewrite.</param>
    /// <param name="operation">The input operation being processed.</param>
    /// <param name="rewrittenNode">The rewritten node, to wrap if needed.</param>
    /// <exception cref="NotSupportedException">Throw if the input operation isn't valid for the input expression node.</exception>
    private static void WrapRewrittenNodeIfNeeded(
        ExpressionSyntax updatedNode,
        IOperation operation,
        ref ExpressionSyntax rewrittenNode)
    {
        switch (operation)
        {
            case IBinaryOperation:
                break;
            case ICompoundAssignmentOperation when updatedNode is AssignmentExpressionSyntax assignmentNode:
                rewrittenNode = AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    assignmentNode.Left,
                    rewrittenNode);
                break;
            default:
                throw new NotSupportedException("Invalid input operation type.");
        }
    }
}