using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <inheritdoc cref="HlslSourceRewriter"/>
partial class HlslSourceRewriter
{
    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
    {
        Diagnostics.Add(AnonymousObjectCreationExpression, node);

        return base.VisitAnonymousObjectCreationExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitInitializerExpression(InitializerExpressionSyntax node)
    {
        Diagnostics.Add(InitializerExpression, node);

        return base.VisitInitializerExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitCollectionExpression(CollectionExpressionSyntax node)
    {
        Diagnostics.Add(CollectionExpression, node);

        return base.VisitCollectionExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitAwaitExpression(AwaitExpressionSyntax node)
    {
        Diagnostics.Add(AwaitExpression, node);

        return base.VisitAwaitExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitCheckedExpression(CheckedExpressionSyntax node)
    {
        Diagnostics.Add(CheckedExpression, node);

        return base.VisitCheckedExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitCheckedStatement(CheckedStatementSyntax node)
    {
        Diagnostics.Add(CheckedStatement, node);

        return base.VisitCheckedStatement(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitFixedStatement(FixedStatementSyntax node)
    {
        Diagnostics.Add(FixedStatement, node);

        return base.VisitFixedStatement(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitForEachStatement(ForEachStatementSyntax node)
    {
        Diagnostics.Add(ForEachStatement, node);

        return base.VisitForEachStatement(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
    {
        Diagnostics.Add(ForEachStatement, node);

        return base.VisitForEachVariableStatement(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitLockStatement(LockStatementSyntax node)
    {
        Diagnostics.Add(LockStatement, node);

        return base.VisitLockStatement(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitQueryExpression(QueryExpressionSyntax node)
    {
        Diagnostics.Add(QueryExpression, node);

        return base.VisitQueryExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitRangeExpression(RangeExpressionSyntax node)
    {
        Diagnostics.Add(RangeExpression, node);

        return base.VisitRangeExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitRecursivePattern(RecursivePatternSyntax node)
    {
        Diagnostics.Add(RecursivePattern, node);

        return base.VisitRecursivePattern(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitRefType(RefTypeSyntax node)
    {
        Diagnostics.Add(RefType, node);

        return base.VisitRefType(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitRelationalPattern(RelationalPatternSyntax node)
    {
        Diagnostics.Add(RelationalPattern, node);

        return base.VisitRelationalPattern(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitSizeOfExpression(SizeOfExpressionSyntax node)
    {
        Diagnostics.Add(SizeOfExpression, node);

        return base.VisitSizeOfExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
    {
        Diagnostics.Add(StackAllocArrayCreationExpression, node);

        return base.VisitStackAllocArrayCreationExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitThrowExpression(ThrowExpressionSyntax node)
    {
        Diagnostics.Add(ThrowExpressionOrStatement, node);

        return base.VisitThrowExpression(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitThrowStatement(ThrowStatementSyntax node)
    {
        Diagnostics.Add(ThrowExpressionOrStatement, node);

        return base.VisitThrowStatement(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitTryStatement(TryStatementSyntax node)
    {
        Diagnostics.Add(TryStatement, node);

        return base.VisitTryStatement(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitTupleType(TupleTypeSyntax node)
    {
        Diagnostics.Add(TupleType, node);

        return base.VisitTupleType(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitUsingStatement(UsingStatementSyntax node)
    {
        Diagnostics.Add(UsingStatementOrDeclaration, node);

        return base.VisitUsingStatement(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitYieldStatement(YieldStatementSyntax node)
    {
        Diagnostics.Add(YieldStatement, node);

        return base.VisitYieldStatement(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitFunctionPointerType(FunctionPointerTypeSyntax node)
    {
        Diagnostics.Add(FunctionPointer, node);

        return base.VisitFunctionPointerType(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitPointerType(PointerTypeSyntax node)
    {
        Diagnostics.Add(PointerType, node);

        return base.VisitPointerType(node);
    }

    /// <inheritdoc/>
    public sealed override SyntaxNode? VisitUnsafeStatement(UnsafeStatementSyntax node)
    {
        Diagnostics.Add(UnsafeStatement, node);

        return base.VisitUnsafeStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitThisExpression(ThisExpressionSyntax node)
    {
        // Emit a diagnostic on 'this' expressions, but only if they're not part of a member access.
        // That is, expressions such as 'this.field' are rewritten correctly to omit the 'this', so
        // so they are still allowed. But actual 'this' expressions that copy or return the entire
        // self instance are disallowed, as that use is not valid in HLSL syntax, unfortunately.
        if (node.Parent is not MemberAccessExpressionSyntax)
        {
            Diagnostics.Add(ThisExpression, node);
        }

        return base.VisitThisExpression(node);
    }
}