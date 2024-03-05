using System;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates errors whenever a shader type is declaring invalid inputs.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidShaderTypeInputsAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
    [
        MissingD2DInputCountAttribute,
        InvalidD2DInputCount,
        OutOfRangeInputIndex,
        RepeatedD2DInputSimpleIndices,
        RepeatedD2DInputComplexIndices,
        InvalidSimpleAndComplexIndicesCombination,
    ];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DInputCount], [D2DInputSimple], [D2DInputComplex] and ID2D1PixelShader symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputCountAttribute") is not { } d2DInputCountAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputSimpleAttribute") is not { } d2DInputSimpleAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputComplexAttribute") is not { } d2DInputComplexAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.ID2D1PixelShader") is not { } d2D1PixelShaderSymbol)
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // We're validating all struct types (since they're the possible shader types)
                if (context.Symbol is not INamedTypeSymbol { TypeKind: TypeKind.Struct } typeSymbol)
                {
                    return;
                }

                // Only validate fields for shader types (same as for resource textures)
                if (!typeSymbol.HasInterfaceWithType(d2D1PixelShaderSymbol))
                {
                    return;
                }

                // If there is no [D2DInputCount], we must stop here, but also emit a diagnostic (as it's mandatory)
                if (!typeSymbol.TryGetAttributeWithType(d2DInputCountAttributeSymbol, out AttributeData? inputCountData))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        MissingD2DInputCountAttribute,
                        typeSymbol.Locations.First(),
                        typeSymbol));

                    return;
                }

                int inputCount = (int)inputCountData.ConstructorArguments[0].Value!;

                // Validate the input count
                if (inputCount is not (>= 0 and <= 8))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        InvalidD2DInputCount,
                        typeSymbol.Locations.First(),
                        typeSymbol,
                        inputCount));
                }

                // If we input is not valid, we can just clamp it to ensure it's nor negative, and
                // continue to validate the rest normally. This will avoid some false positives.
                inputCount = Math.Max(inputCount, 0);

                using ImmutableArrayBuilder<int> inputSimpleIndices = new();
                using ImmutableArrayBuilder<int> inputComplexIndices = new();

                // Gather the indices of all explicitly declared simple and complex inputs
                foreach (AttributeData attributeData in typeSymbol.GetAttributes())
                {
                    if (SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, d2DInputSimpleAttributeSymbol))
                    {
                        inputSimpleIndices.Add((int)attributeData.ConstructorArguments[0].Value!);
                    }
                    else if (SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, d2DInputComplexAttributeSymbol))
                    {
                        inputComplexIndices.Add((int)attributeData.ConstructorArguments[0].Value!);
                    }
                }

                // All simple indices must be in range
                foreach (int index in inputSimpleIndices.WrittenSpan)
                {
                    if ((uint)index >= inputCount)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            OutOfRangeInputIndex,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        goto AfterOutOfRangeInputValidation;
                    }
                }

                // All complex indices must also be in range (of course)
                foreach (int index in inputComplexIndices.WrittenSpan)
                {
                    if ((uint)index >= inputCount)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            OutOfRangeInputIndex,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        goto AfterOutOfRangeInputValidation;
                    }
                }

                AfterOutOfRangeInputValidation:

                Span<bool> selectedSimpleInputIndices = stackalloc bool[8];
                Span<bool> selectedComplexInputIndices = stackalloc bool[8];

                selectedSimpleInputIndices.Clear();
                selectedComplexInputIndices.Clear();

                bool hasReportedRepeatedD2DinputSimpleIndex = false;
                bool hasReportedRepeatedD2DinputComplexIndex = false;

                // All simple indices must be unique
                foreach (int index in inputSimpleIndices.WrittenSpan)
                {
                    // Skip this path for invalid indices (just like with resource textures again).
                    // This same exact pattern is also used when validating input descriptions.
                    if ((uint)index >= 8)
                    {
                        continue;
                    }

                    ref bool isInputIndexUsed = ref selectedSimpleInputIndices[index];

                    // If we detect a repeated index, we emit the diagnostic (only at most once), but
                    // then we also continue processing all remaining items. This is done to make sure
                    // that all used indices are correctly marked, so that the detection of invalid
                    // combinations at the end of the analyzer doesn't risk having false negatives.
                    if (isInputIndexUsed && !hasReportedRepeatedD2DinputSimpleIndex)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            RepeatedD2DInputSimpleIndices,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        hasReportedRepeatedD2DinputSimpleIndex = true;
                    }

                    isInputIndexUsed = true;
                }

                // All complex indices must be unique as well (again, of course)
                foreach (int index in inputComplexIndices.WrittenSpan)
                {
                    if ((uint)index >= 8)
                    {
                        continue;
                    }

                    ref bool isInputIndexUsed = ref selectedComplexInputIndices[index];

                    if (isInputIndexUsed && !hasReportedRepeatedD2DinputComplexIndex)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            RepeatedD2DInputComplexIndices,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        hasReportedRepeatedD2DinputComplexIndex = true;
                    }

                    isInputIndexUsed = true;
                }

                // Simple and complex indices can't have indices in common. We just validate
                // the entire temporary spans, so that even if some values are out of range,
                // we still also detect that some are conflicting.
                for (int i = 0; i < 8; i++)
                {
                    if (selectedSimpleInputIndices[i] && selectedComplexInputIndices[i])
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            InvalidSimpleAndComplexIndicesCombination,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        break;
                    }
                }
            }, SymbolKind.NamedType);
        });
    }
}