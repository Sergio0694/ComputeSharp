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
/// A diagnostic analyzer that generates errors for all invalid uses of the [D2DInputDescription] attribute.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2DInputDescriptionAttributeUseAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [OutOfRangeInputDescriptionIndex, RepeatedD2DInputDescriptionIndices];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [D2DInputDescription], [D2DInputCount] and ID2D1PixelShader symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputDescriptionAttribute") is not { } d2DInputDescriptionAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputCountAttribute") is not { } d2DInputCountAttributeSymbol ||
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

                // Immediately bail if [D2DInputCount] is not present. If that's the case, there's no way to properly
                // validate the indices of the input descriptions anyway. Another analyzer will catch these cases.
                if (!typeSymbol.TryGetAttributeWithType(d2DInputCountAttributeSymbol, out AttributeData? inputCountData))
                {
                    return;
                }

                int inputCount = (int)inputCountData.ConstructorArguments[0].Value!;

                // Just like in the resource texture analyzer, also bail if the input count is invalid (for the same reason)
                if (inputCount is not (>= 0 and <= 8))
                {
                    return;
                }

                using ImmutableArrayBuilder<int> inputDescriptionIndices = new();

                // Collect all indices of available input descriptions (there's no validation for the other properties)
                foreach (AttributeData attributeData in typeSymbol.GetAttributes())
                {
                    if (SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, d2DInputDescriptionAttributeSymbol))
                    {
                        inputDescriptionIndices.Add((int)attributeData.ConstructorArguments[0].Value!);
                    }
                }

                // All simple indices must be in range
                foreach (int index in inputDescriptionIndices.WrittenSpan)
                {
                    if (index >= inputCount)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            OutOfRangeInputDescriptionIndex,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        break;
                    }
                }

                Span<bool> selectedInputDescriptionIndices = stackalloc bool[8];

                selectedInputDescriptionIndices.Clear();

                // All input description indices must be unique
                foreach (int index in inputDescriptionIndices.WrittenSpan)
                {
                    // Skip this path for invalid indices (just like with resource textures again).
                    // This would just throw when accessing the span anyway (also it's unlikely).
                    if (index >= 8)
                    {
                        continue;
                    }

                    ref bool isInputDescriptionIndexUsed = ref selectedInputDescriptionIndices[index];

                    if (isInputDescriptionIndexUsed)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            RepeatedD2DInputDescriptionIndices,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        break;
                    }

                    isInputDescriptionIndexUsed = true;
                }
            }, SymbolKind.NamedType);
        });
    }
}