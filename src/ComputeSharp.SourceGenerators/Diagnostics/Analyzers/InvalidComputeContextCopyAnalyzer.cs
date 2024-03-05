using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever a <c>ComputeContext</c> value is copied.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidComputeContextCopyAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidComputeContextCopy];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(csc =>
        {
            // Initializations
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is ISymbolInitializerOperation operation)
                {
                    CheckCopyability(context, operation.Value);
                }
            },
            OperationKind.FieldInitializer,
            OperationKind.ParameterInitializer,
            OperationKind.PropertyInitializer,
            OperationKind.VariableInitializer);

            // Assignments
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is ISimpleAssignmentOperation { IsRef: false } operation)
                {
                    CheckCopyability(context, operation.Value);
                }
            }, OperationKind.SimpleAssignment);

            // Arguments
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is IArgumentOperation { Parameter.RefKind: RefKind.None } operation)
                {
                    CheckCopyability(context, operation.Value);
                }
            }, OperationKind.Argument);

            // Returns
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is IReturnOperation { ReturnedValue: not null } operation)
                {
                    // In the absence of data flow analysis we can treat
                    // return of a local variable or a parameter as a "move"
                    if (operation.ReturnedValue.Kind is OperationKind.LocalReference or OperationKind.ParameterReference &&
                        operation.Kind == OperationKind.Return)
                    {
                        ISymbol? varScope = operation.ReturnedValue.Kind switch
                        {
                            OperationKind.LocalReference => ((ILocalReferenceOperation)operation.ReturnedValue).Local,
                            OperationKind.ParameterReference => ((IParameterReferenceOperation)operation.ReturnedValue).Parameter,
                            _ => null
                        };

                        if (varScope is null)
                        {
                            return;
                        }

                        ISymbol? opScope = operation.SemanticModel?.GetEnclosingSymbol(operation.Syntax.SpanStart);

                        if (SymbolEqualityComparer.Default.Equals(varScope, opScope))
                        {
                            return;
                        }
                    }

                    CheckCopyability(context, operation.ReturnedValue);
                }
            },
            OperationKind.Return,
            OperationKind.YieldReturn);

            // Conversions
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is IConversionOperation operation)
                {
                    if (operation.Operand.Kind == OperationKind.DefaultValue)
                    {
                        return;
                    }

                    if (operation.Operand.Type is not ITypeSymbol typeSymbol)
                    {
                        return;
                    }

                    if (!IsComputeContextType(typeSymbol))
                    {
                        return;
                    }

                    if (operation.OperatorMethod is { Parameters.Length: 1 })
                    {
                        if (operation.OperatorMethod.Parameters[0].RefKind != RefKind.None)
                        {
                            return;
                        }
                    }

                    if (operation.Parent is IForEachLoopOperation { Collection: IOperation collectionOperation } &&
                        operation == collectionOperation &&
                        operation.Conversion.IsIdentity)
                    {
                        return;
                    }

                    context.ReportDiagnostic(Diagnostic.Create(InvalidComputeContextCopy, operation.Operand.Syntax.GetLocation()));
                }
            }, OperationKind.Conversion);

            // Array initialization
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is IArrayInitializerOperation { Parent.Type: IArrayTypeSymbol arrayTypeSymbol } operation)
                {
                    if (!IsComputeContextType(arrayTypeSymbol.ElementType))
                    {
                        return;
                    }

                    foreach (IOperation v in operation.ElementValues)
                    {
                        CheckCopyability(context, v);
                    }
                }
            }, OperationKind.ArrayInitializer);

            // Declaration pattern
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is IDeclarationPatternOperation { DeclaredSymbol: ILocalSymbol { Type: ITypeSymbol typeSymbol } } operation)
                {
                    if (!IsComputeContextType(typeSymbol))
                    {
                        return;
                    }

                    context.ReportDiagnostic(Diagnostic.Create(InvalidComputeContextCopy, operation.Syntax.GetLocation()));
                }
            }, OperationKind.DeclarationPattern);

            // Tuple
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is ITupleOperation operation)
                {
                    // Exclude ParenthesizedVariableDesignationSyntax
                    if (!operation.Syntax.IsKind(SyntaxKind.TupleExpression))
                    {
                        return;
                    }

                    foreach (IOperation v in operation.Elements)
                    {
                        CheckCopyability(context, v);
                    }
                }
            }, OperationKind.Tuple);

            // Member references
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is IMemberReferenceOperation { Instance: not null } operation)
                {
                    CheckInstanceReadonly(context, operation.Instance);
                }
            },
            OperationKind.PropertyReference,
            OperationKind.EventReference);

            // Invocations
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is IInvocationOperation operation)
                {
                    CheckGenericConstraints(context, operation);

                    if (operation.Instance is not null)
                    {
                        CheckInstanceReadonly(context, operation.Instance);
                    }
                }
            }, OperationKind.Invocation);

            // Method reference
            csc.RegisterOperationAction(static context =>
            {
                if (context.Operation is IMemberReferenceOperation { Instance.Type: not null } operation)
                {
                    if (!IsComputeContextType(operation.Instance.Type))
                    {
                        return;
                    }

                    context.ReportDiagnostic(Diagnostic.Create(InvalidComputeContextCopy, operation.Syntax.GetLocation()));
                }
            }, OperationKind.MethodReference);

            // Field use
            csc.RegisterSymbolAction(static context =>
            {
                if (context.Symbol is IFieldSymbol { IsStatic: false } symbol)
                {
                    if (!IsComputeContextType(symbol.Type))
                    {
                        return;
                    }

                    if (symbol.ContainingType.IsReferenceType)
                    {
                        return;
                    }

                    if (IsComputeContextType(symbol.ContainingType))
                    {
                        return;
                    }

                    context.ReportDiagnostic(Diagnostic.Create(InvalidComputeContextCopy, symbol.DeclaringSyntaxReferences[0].GetSyntax(context.CancellationToken).GetLocation()));
                }
            }, SymbolKind.Field);
        });
    }

    /// <summary>
    /// Checks the generic constraints of an operation.
    /// </summary>
    /// <param name="context">The input <see cref="OperationAnalysisContext"/> instance to use.</param>
    /// <param name="operation">The <see cref="IInvocationOperation"/> instance to check.</param>
    private static void CheckGenericConstraints(in OperationAnalysisContext context, IInvocationOperation operation)
    {
        IMethodSymbol methodSymbol = operation.TargetMethod;

        if (methodSymbol.IsGenericMethod)
        {
            foreach (ITypeSymbol argumentSymbol in methodSymbol.TypeArguments)
            {
                if (IsComputeContextType(argumentSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(InvalidComputeContextCopy, operation.Syntax.GetLocation()));
                }
            }
        }
    }

    /// <summary>
    /// Checks that an instance is not readonly.
    /// </summary>
    /// <param name="context">The input <see cref="OperationAnalysisContext"/> instance to use.</param>
    /// <param name="operation">The <see cref="IOperation"/> instance to check.</param>
    private static void CheckInstanceReadonly(in OperationAnalysisContext context, IOperation operation)
    {
        if (operation.Type is not ITypeSymbol typeSymbol)
        {
            return;
        }

        if (!IsComputeContextType(typeSymbol))
        {
            return;
        }

        if (operation is
            IFieldReferenceOperation { Field.IsReadOnly: true } or
            ILocalReferenceOperation { Local.RefKind: RefKind.In } or
            IParameterReferenceOperation { Parameter.RefKind: RefKind.In })
        {
            context.ReportDiagnostic(Diagnostic.Create(InvalidComputeContextCopy, operation.Syntax.GetLocation()));
        }
    }

    /// <summary>
    /// Checks that an operation supports copying.
    /// </summary>
    /// <param name="context">The input <see cref="OperationAnalysisContext"/> instance to use.</param>
    /// <param name="operation">The <see cref="IOperation"/> instance to check.</param>
    private static void CheckCopyability(in OperationAnalysisContext context, IOperation operation)
    {
        if (operation.Type is not ITypeSymbol typeSymbol)
        {
            return;
        }

        if (!IsComputeContextType(typeSymbol))
        {
            return;
        }

        if (CanCopy(operation))
        {
            return;
        }

        context.ReportDiagnostic(Diagnostic.Create(InvalidComputeContextCopy, operation.Syntax.GetLocation()));
    }

    /// <summary>
    /// Checks that a given type symbol is <c>ComputeContext</c>.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="ITypeSymbol"/> instance to check.</param>
    /// <returns>Whether <paramref name="typeSymbol"/> is <c>ComputeContext</c>.</returns>
    private static bool IsComputeContextType(ITypeSymbol typeSymbol)
    {
        return typeSymbol is { TypeKind: TypeKind.Struct, Name: "ComputeContext", ContainingNamespace.Name: "ComputeSharp" };
    }

    /// <summary>
    /// Checks whether an operation is copyable or not even when it is a non-copyable instance.
    /// </summary>
    /// <param name="operation">The <see cref="IOperation"/> instance to check.</param>
    /// <returns>Whether the operation suppots copying.</returns>
    private static bool CanCopy(IOperation operation)
    {
        OperationKind kind = operation.Kind;

        if (kind == OperationKind.Conversion)
        {
            return operation is IConversionOperation { Operand.Kind: OperationKind.DefaultValue or OperationKind.Invalid };
        }

        if (kind is OperationKind.LocalReference or OperationKind.FieldReference or OperationKind.PropertyReference or OperationKind.ArrayElementReference)
        {
            return operation.Syntax.Parent?.IsKind(SyntaxKind.RefExpression) == true;
        }

        if (kind == OperationKind.Conditional)
        {
            return
                operation is IConditionalOperation { WhenFalse: IOperation whenFalse, WhenTrue: IOperation whenTrue } &&
                CanCopy(whenFalse) &&
                CanCopy(whenTrue);
        }

        return
            kind is OperationKind.ObjectCreation or OperationKind.DefaultValue or OperationKind.Literal or OperationKind.Invocation ||
            (kind is OperationKind.None or OperationKind.Invalid && operation.Syntax is InvocationExpressionSyntax); // Workaround for https://github.com/dotnet/roslyn/issues/49751
    }
}