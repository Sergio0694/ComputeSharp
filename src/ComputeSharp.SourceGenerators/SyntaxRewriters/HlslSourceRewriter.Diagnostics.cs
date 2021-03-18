using ComputeSharp.SourceGenerators.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators.SyntaxRewriters
{
    /// <inheritdoc cref="HlslSourceRewriter"/>
    internal abstract partial class HlslSourceRewriter : CSharpSyntaxRewriter
    {
        /// <inheritdoc/>
        public override SyntaxNode? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
        {
            var updatedNode = (AnonymousObjectCreationExpressionSyntax)base.VisitAnonymousObjectCreationExpression(node)!;

            Context.ReportDiagnostic(AnonymousObjectCreationExpression, node);

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitAwaitExpression(AwaitExpressionSyntax node)
        {
            Context.ReportDiagnostic(AwaitExpression, node);

            return base.VisitAwaitExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            Context.ReportDiagnostic(CheckedExpression, node);

            return base.VisitCheckedExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitCheckedStatement(CheckedStatementSyntax node)
        {
            Context.ReportDiagnostic(CheckedStatement, node);

            return base.VisitCheckedStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitFixedStatement(FixedStatementSyntax node)
        {
            Context.ReportDiagnostic(FixedStatement, node);

            return base.VisitFixedStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitForEachStatement(ForEachStatementSyntax node)
        {
            Context.ReportDiagnostic(ForEachStatement, node);

            return base.VisitForEachStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
        {
            Context.ReportDiagnostic(ForEachStatement, node);

            return base.VisitForEachVariableStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitLockStatement(LockStatementSyntax node)
        {
            Context.ReportDiagnostic(LockStatement, node);

            return base.VisitLockStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitQueryExpression(QueryExpressionSyntax node)
        {
            Context.ReportDiagnostic(QueryExpression, node);

            return base.VisitQueryExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRangeExpression(RangeExpressionSyntax node)
        {
            Context.ReportDiagnostic(RangeExpression, node);

            return base.VisitRangeExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRecursivePattern(RecursivePatternSyntax node)
        {
            Context.ReportDiagnostic(RecursivePattern, node);

            return base.VisitRecursivePattern(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRefType(RefTypeSyntax node)
        {
            Context.ReportDiagnostic(RefType, node);

            return base.VisitRefType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitRelationalPattern(RelationalPatternSyntax node)
        {
            Context.ReportDiagnostic(RelationalPattern, node);

            return base.VisitRelationalPattern(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitSizeOfExpression(SizeOfExpressionSyntax node)
        {
            Context.ReportDiagnostic(SizeOfExpression, node);

            return base.VisitSizeOfExpression(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            Context.ReportDiagnostic(StackAllocArrayCreationExpression, node);

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
            Context.ReportDiagnostic(TryStatement, node);

            return base.VisitTryStatement(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitTupleType(TupleTypeSyntax node)
        {
            Context.ReportDiagnostic(TupleType, node);

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
                SemanticModel.For(node).GetTypeInfo(declaration.Type).Type is ITypeSymbol { IsUnmanagedType: false } type)
            {
                Context.ReportDiagnostic(InvalidObjectDeclaration, node, type);
            }

            return updatedNode;
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitYieldStatement(YieldStatementSyntax node)
        {
            Context.ReportDiagnostic(YieldStatement, node);

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
            Context.ReportDiagnostic(PointerType, node);

            return base.VisitPointerType(node);
        }

        /// <inheritdoc/>
        public override SyntaxNode? VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            Context.ReportDiagnostic(UnsafeStatement, node);

            return base.VisitUnsafeStatement(node);
        }
    }
}
