using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.D2D1.SourceGenerators.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators.SyntaxRewriters;

/// <inheritdoc cref="HlslSourceRewriter"/>
internal abstract partial class HlslSourceRewriter : CSharpSyntaxRewriter
{
    /// <inheritdoc/>
    public override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
    {
        var updatedNode = (AnonymousObjectCreationExpressionSyntax)base.VisitAnonymousObjectCreationExpression(node)!;

        Diagnostics.Add(AnonymousObjectCreationExpression, node);

        return updatedNode;
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitAwaitExpression(AwaitExpressionSyntax node)
    {
        Diagnostics.Add(AwaitExpression, node);

        return base.VisitAwaitExpression(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitCheckedExpression(CheckedExpressionSyntax node)
    {
        Diagnostics.Add(CheckedExpression, node);

        return base.VisitCheckedExpression(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitCheckedStatement(CheckedStatementSyntax node)
    {
        Diagnostics.Add(CheckedStatement, node);

        return base.VisitCheckedStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitFixedStatement(FixedStatementSyntax node)
    {
        Diagnostics.Add(FixedStatement, node);

        return base.VisitFixedStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitForEachStatement(ForEachStatementSyntax node)
    {
        Diagnostics.Add(ForEachStatement, node);

        return base.VisitForEachStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
    {
        Diagnostics.Add(ForEachStatement, node);

        return base.VisitForEachVariableStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitLockStatement(LockStatementSyntax node)
    {
        Diagnostics.Add(LockStatement, node);

        return base.VisitLockStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitQueryExpression(QueryExpressionSyntax node)
    {
        Diagnostics.Add(QueryExpression, node);

        return base.VisitQueryExpression(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitRangeExpression(RangeExpressionSyntax node)
    {
        Diagnostics.Add(RangeExpression, node);

        return base.VisitRangeExpression(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitRecursivePattern(RecursivePatternSyntax node)
    {
        Diagnostics.Add(RecursivePattern, node);

        return base.VisitRecursivePattern(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitRefType(RefTypeSyntax node)
    {
        Diagnostics.Add(RefType, node);

        return base.VisitRefType(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitRelationalPattern(RelationalPatternSyntax node)
    {
        Diagnostics.Add(RelationalPattern, node);

        return base.VisitRelationalPattern(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitSizeOfExpression(SizeOfExpressionSyntax node)
    {
        Diagnostics.Add(SizeOfExpression, node);

        return base.VisitSizeOfExpression(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
    {
        Diagnostics.Add(StackAllocArrayCreationExpression, node);

        return base.VisitStackAllocArrayCreationExpression(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitThrowExpression(ThrowExpressionSyntax node)
    {
        Diagnostics.Add(ThrowExpressionOrStatement, node);

        return base.VisitThrowExpression(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitThrowStatement(ThrowStatementSyntax node)
    {
        Diagnostics.Add(ThrowExpressionOrStatement, node);

        return base.VisitThrowStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitTryStatement(TryStatementSyntax node)
    {
        Diagnostics.Add(TryStatement, node);

        return base.VisitTryStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitTupleType(TupleTypeSyntax node)
    {
        Diagnostics.Add(TupleType, node);

        return base.VisitTupleType(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitUsingStatement(UsingStatementSyntax node)
    {
        Diagnostics.Add(UsingStatementOrDeclaration, node);

        return base.VisitUsingStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitYieldStatement(YieldStatementSyntax node)
    {
        Diagnostics.Add(YieldStatement, node);

        return base.VisitYieldStatement(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitFunctionPointerType(FunctionPointerTypeSyntax node)
    {
        Diagnostics.Add(FunctionPointer, node);

        return base.VisitFunctionPointerType(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitPointerType(PointerTypeSyntax node)
    {
        Diagnostics.Add(PointerType, node);

        return base.VisitPointerType(node);
    }

    /// <inheritdoc/>
    public override SyntaxNode? VisitUnsafeStatement(UnsafeStatementSyntax node)
    {
        Diagnostics.Add(UnsafeStatement, node);

        return base.VisitUnsafeStatement(node);
    }
}
