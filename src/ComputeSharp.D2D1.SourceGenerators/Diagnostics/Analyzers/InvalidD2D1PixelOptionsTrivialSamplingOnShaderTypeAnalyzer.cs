using System;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates an error whenever [D2DPixelOptions] is used on a shader type to incorrectly request trivial sampling.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2D1PixelOptionsTrivialSamplingOnShaderTypeAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [InvalidD2D1PixelOptionsTrivialSamplingOnShaderType];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DPixelOptions], [D2DInputCount] and [D2DInputSimple] symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DPixelOptionsAttribute") is not { } d2DPixelOptionsAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputCountAttribute") is not { } d2DInputCountAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputSimpleAttribute") is not { } d2DInputSimpleAttributeSymbol)
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // Only struct types are possible targets
                if (context.Symbol is not INamedTypeSymbol { TypeKind: TypeKind.Struct } typeSymbol)
                {
                    return;
                }

                // If the type is not using [D2DPixelOptions] with D2D1PixelOptions.TrivialSampling, there's nothing to do
                if (!typeSymbol.TryGetAttributeWithType(d2DPixelOptionsAttributeSymbol, out AttributeData? pixelOptionsAttribute) ||
                    !((D2D1PixelOptions)pixelOptionsAttribute.ConstructorArguments[0].Value!).HasFlag(D2D1PixelOptions.TrivialSampling))
                {
                    return;
                }

                // Make sure we have the [D2DInputCount] (if not present, the shader is invalid anyway) and with a valid value
                if (!typeSymbol.TryGetAttributeWithType(d2DInputCountAttributeSymbol, out AttributeData? inputCountAttribute) ||
                    inputCountAttribute.ConstructorArguments is not [{ Value: >= 0 and < 8 and int inputCount }])
                {
                    return;
                }

                // Emit a diagnostic if the compile options are not valid for the shader type
                if (!IsSimpleInputShader(typeSymbol, d2DInputSimpleAttributeSymbol, inputCount))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidD2D1PixelOptionsTrivialSamplingOnShaderType,
                        pixelOptionsAttribute.GetLocation(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }

    /// <summary>
    /// Checks whether a given shader only has simple inputs.
    /// </summary>
    /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
    /// <param name="d2DInputSimpleSymbolAttributeSymbol">The symbol for the <c>[D2DInputSimple]</c> attribute.</param>
    /// <param name="inputCount">The number of inputs for the shader.</param>
    /// <returns>Whether the shader only has simple inputs.</returns>
    private static bool IsSimpleInputShader(
        INamedTypeSymbol structDeclarationSymbol,
        INamedTypeSymbol d2DInputSimpleSymbolAttributeSymbol,
        int inputCount)
    {
        // We cannot trust the input count to be valid at this point (it may be invalid and
        // with diagnostic already emitted for it). So first, just clamp it in the right range.
        inputCount = Math.Max(0, Math.Min(8, inputCount));

        // If there are no inputs, the shader is as if only had simple inputs
        if (inputCount == 0)
        {
            return true;
        }

        // Build a map of all simple inputs (unmarked inputs default to being complex).
        // We can never have more than 8 inputs, and if there are it means the shader is
        // not valid. Just ignore them, and the generator will emit a separate diagnostic.
        Span<bool> simpleInputsMap = stackalloc bool[8];

        // We first start with all inputs marked as complex (ie. not simple)
        simpleInputsMap.Clear();

        foreach (AttributeData attributeData in structDeclarationSymbol.GetAttributes())
        {
            // Only retrieve indices of simple inputs that are in range. If an input is out of
            // range, the diagnostic for it will already be emitted by a previous generator step.
            if (SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, d2DInputSimpleSymbolAttributeSymbol) &&
                attributeData.ConstructorArguments is [{ Value: >= 0 and < 8 and int index }])
            {
                simpleInputsMap[index] = true;
            }
        }

        bool isSimpleInputShader = true;

        // Validate all inputs in our range (filtered by the allowed one)
        foreach (bool isSimpleInput in simpleInputsMap[..inputCount])
        {
            isSimpleInputShader &= isSimpleInput;
        }

        return isSimpleInputShader;
    }
}