using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Intrinsics;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever an invocation to a D2D intrinsic references an out of range or invalid shader input.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2DInputArgumentAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
    [
        IndexOutOfRangeForD2DIntrinsic,
        InvalidInputTypeForD2DIntrinsic
    ];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // If we can't get the D2D methods map, we have to stop right away
            if (!TryBuildMethodSymbolMap(context.Compilation, out ImmutableDictionary<IMethodSymbol, D2D1PixelShaderInputType>? methodSymbols))
            {
                return;
            }

            context.CancellationToken.ThrowIfCancellationRequested();

            // Get the '[D2DInputCount]' symbol, which we need to validate the source input arguments
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputCountAttribute") is not { } d2DInputCountAttributeSymbol)
            {
                return;
            }

            // We want to register a callback for each method invocation, as all 'D2D' intrinsics would map to one
            context.RegisterOperationAction(context =>
            {
                IInvocationOperation operation = (IInvocationOperation)context.Operation;

                // Cheap initial filter: we only care about static methods from the 'D2D' type
                if (operation.TargetMethod is not { IsStatic: true, ContainingType.Name: "D2D" } targetMethodSymbol)
                {
                    return;
                }

                // Second cheap inital filter: we only care about invocations with a constant 'int' argument in first position.
                // While we're validating this, let's also get the 'index' parameter, since we need to validate it anyway.
                if (operation.Arguments is not [{ Value.ConstantValue: { HasValue: true, Value: int index } }, ..])
                {
                    return;
                }

                // Validate that the target method is one of the ones we care about, and get the target input type
                if (!methodSymbols.TryGetValue(targetMethodSymbol, out D2D1PixelShaderInputType targetInputType))
                {
                    return;
                }

                // We have matched a target symbol, so let's try to get the parent shader
                if (context.ContainingSymbol.FirstAncestorOrSelf<INamedTypeSymbol>() is not { TypeKind: TypeKind.Struct } typeSymbol)
                {
                    return;
                }

                // We found a containing type, make sure it has '[D2DInputCount]'
                if (!typeSymbol.HasAttributeWithType(d2DInputCountAttributeSymbol))
                {
                    return;
                }

                // At this point we can assume the shader type is mostly valid: let's get the actual input counts and types
                D2DPixelShaderDescriptorGenerator.InputTypes.GetInfo(
                    typeSymbol,
                    out int inputCount,
                    out _,
                    out _,
                    out ImmutableArray<uint> inputTypes);

                // First validation: the index must be in range
                if ((uint)index >= (uint)inputCount)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        IndexOutOfRangeForD2DIntrinsic,
                        operation.Syntax.GetLocation(),
                        targetMethodSymbol.Name,
                        index,
                        typeSymbol,
                        inputCount));
                }
                else if ((D2D1PixelShaderInputType)inputTypes[index] != targetInputType)
                {
                    // Second validation: the input type must match
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidInputTypeForD2DIntrinsic,
                        operation.Syntax.GetLocation(),
                        targetMethodSymbol.Name,
                        index));
                }
            }, OperationKind.Invocation);
        });
    }

    /// <summary>
    /// Tries to build a map of <see cref="IMethodSymbol"/> instances for all D2D intrinsics and their associated D2D input type.
    /// </summary>
    /// <param name="compilation">The <see cref="Compilation"/> to consider for analysis.</param>
    /// <param name="methodSymbols">The resulting mapping of resolved <see cref="IMethodSymbol"/> instances.</param>
    /// <returns>Whether all requested <see cref="IMethodSymbol"/> instances could be resolved.</returns>
    private static bool TryBuildMethodSymbolMap(Compilation compilation, [NotNullWhen(true)] out ImmutableDictionary<IMethodSymbol, D2D1PixelShaderInputType>? methodSymbols)
    {
        // Get the 'D2D' symbol, to get methods from it
        if (compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2D") is not { } d2DSymbol)
        {
            methodSymbols = null;

            return false;
        }

        // Get the symbols for all relevant 'D2D' methods
        IMethodSymbol?[] d2DMethodSymbols =
        [
            d2DSymbol.GetMethod(nameof(D2D.GetInput)),
            d2DSymbol.GetMethod(nameof(D2D.GetInputCoordinate)),
            d2DSymbol.GetMethod(nameof(D2D.SampleInput)),
            d2DSymbol.GetMethod(nameof(D2D.SampleInputAtOffset)),
            d2DSymbol.GetMethod(nameof(D2D.SampleInputAtPosition))
        ];

        ImmutableDictionary<IMethodSymbol, D2D1PixelShaderInputType>.Builder inputTypeMethodMap = ImmutableDictionary.CreateBuilder<IMethodSymbol, D2D1PixelShaderInputType>(SymbolEqualityComparer.Default);

        // Validate all methods and build the map
        foreach (IMethodSymbol? d2DMethodSymbol in d2DMethodSymbols)
        {
            // We failed to find a symbol (shouldn't really ever happen), just stop here
            if (d2DMethodSymbol is null)
            {
                methodSymbols = null;

                return false;
            }

            // Lookup the attribute to get the D2D input type (the attribute only exists on the 'D2D' type loaded in the analyzer)
            D2D1PixelShaderInputType inputType = typeof(D2D).GetMethod(d2DMethodSymbol.Name).GetCustomAttribute<HlslD2DIntrinsicInputTypeAttribute>()!.InputType;

            inputTypeMethodMap.Add(d2DMethodSymbol, (D2D1PixelShaderInputType)inputType);
        }

        methodSymbols = inputTypeMethodMap.ToImmutable();

        return true;
    }
}