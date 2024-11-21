using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever an invocation to a D2D intrinsic is using an argument that is not constant for the source shader input.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class NonConstantD2DInputArgumentAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [NonConstantSourceInputIndexForD2DIntrinsic];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Ensure we can get all methods, or stop immediately
            if (!TryBuildMethodSymbolSet(context.Compilation, out ImmutableHashSet<IMethodSymbol>? methodSymbols))
            {
                return;
            }

            context.CancellationToken.ThrowIfCancellationRequested();

            // Register a callback for each argument we want to validate
            context.RegisterOperationAction(context =>
            {
                IArgumentOperation operation = (IArgumentOperation)context.Operation;

                // Ensure the argument is for an invocation, and get its first argument
                if (operation.Parent is not IInvocationOperation { Arguments: [{ } firstArgument, ..] } invocationOperation)
                {
                    return;
                }

                // We also want to verify that the argument is the first one (that's the only one that should be constant)
                if (operation != firstArgument)
                {
                    return;
                }

                // Ensure the argument is for a 'D2D' method (ignore all other arguments, ie. almost all of them)
                if (invocationOperation is not { TargetMethod: { IsStatic: true, ContainingType.Name: "D2D" } targetMethodSymbol })
                {
                    return;
                }

                // We only want to kick in when the target parameter is an 'int'
                if (operation is not { Parameter: { Type.SpecialType: SpecialType.System_Int32 } parameterSymbol })
                {
                    return;
                }

                // This analyzer should only kick in when the target parameter is an 'int', and the argument is not constant
                if (operation.Value is ILiteralOperation { ConstantValue.HasValue: true } or IUnaryOperation { ConstantValue.HasValue: true, Operand: ILiteralOperation })
                {
                    return;
                }

                // Now we can actually verify that the target method is indeed one we care about (this should always match)
                if (!methodSymbols.Contains(targetMethodSymbol))
                {
                    return;
                }

                // Finally, we can emit the diagnostic
                context.ReportDiagnostic(Diagnostic.Create(
                    NonConstantSourceInputIndexForD2DIntrinsic,
                    operation.Syntax.GetLocation(),
                    parameterSymbol.Name,
                    targetMethodSymbol.Name));
            }, OperationKind.Argument);
        });
    }

    /// <summary>
    /// Tries to build a set of <see cref="IMethodSymbol"/> instances for all D2D intrinsics targeting a specific shader input.
    /// </summary>
    /// <param name="compilation">The <see cref="Compilation"/> to consider for analysis.</param>
    /// <param name="methodSymbols">The resulting set of resolved <see cref="IMethodSymbol"/> instances.</param>
    /// <returns>Whether all requested <see cref="IMethodSymbol"/> instances could be resolved.</returns>
    private static bool TryBuildMethodSymbolSet(Compilation compilation, [NotNullWhen(true)] out ImmutableHashSet<IMethodSymbol>? methodSymbols)
    {
        // Get the 'D2D' symbol, to get methods from it
        if (compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2D") is not { } d2DSymbol)
        {
            methodSymbols = null;

            return false;
        }

        // All 'D2D' intrinsics targeting an input (see 'InvalidD2DInputArgumentAnalyzer')
        string[] d2DMethodNames =
        [
            nameof(D2D.GetInput),
            nameof(D2D.GetInputCoordinate),
            nameof(D2D.SampleInput),
            nameof(D2D.SampleInputAtOffset),
            nameof(D2D.SampleInputAtPosition)
        ];

        ImmutableHashSet<IMethodSymbol>.Builder inputTypeMethodSet = ImmutableHashSet.CreateBuilder<IMethodSymbol>(SymbolEqualityComparer.Default);

        // Validate all methods and build the map
        foreach (string d2DMethodName in d2DMethodNames)
        {
            // Ensure we can find the target method symbol (again just like in 'InvalidD2DInputArgumentAnalyzer')
            if (d2DSymbol.GetMethod(d2DMethodName) is not { } d2DMethodSymbol)
            {
                methodSymbols = null;

                return false;
            }

            _ = inputTypeMethodSet.Add(d2DMethodSymbol);
        }

        methodSymbols = inputTypeMethodSet.ToImmutable();

        return true;
    }
}