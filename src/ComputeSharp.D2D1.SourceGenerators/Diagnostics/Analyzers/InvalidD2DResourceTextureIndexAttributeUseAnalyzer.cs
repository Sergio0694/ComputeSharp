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
/// A diagnostic analyzer that generates errors for all invalid uses of the [D2DResourceTextureIndex] attribute.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidD2DResourceTextureIndexAttributeUseAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
    [
        MissingD2DResourceTextureIndexAttribute,
        ResourceTextureIndexOverlappingWithInputIndex,
        OutOfRangeResourceTextureIndex,
        RepeatedD2DResourceTextureIndices,
    ];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get all necessary symbols (we need a whole bunch of them here)
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DResourceTextureIndexAttribute") is not { } d2DResourceTextureIndexAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2D1ResourceTexture1D`1") is not { } d2D1ResourceTexture1DSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2D1ResourceTexture2D`1") is not { } d2D1ResourceTexture2DSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2D1ResourceTexture3D`1") is not { } d2D1ResourceTexture3DSymbol ||
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

                // Only validate fields for shader types (even without a generated descriptor)
                if (!typeSymbol.HasInterfaceWithType(d2D1PixelShaderSymbol))
                {
                    return;
                }

                using ImmutableArrayBuilder<(int? Index, int Rank)> resourceTextureInfos = new();

                foreach (ISymbol memberSymbol in typeSymbol.GetMembers())
                {
                    // Only look for valid candidate fields (same logic as in D2DPixelShaderDescriptorGenerator.InputTypes)
                    if (memberSymbol is not IFieldSymbol { IsStatic: false, Type: INamedTypeSymbol { IsUnmanagedType: true } } fieldSymbol)
                    {
                        continue;
                    }

                    // Specifically check that the type is generic and grab the source one
                    if (fieldSymbol.Type is not INamedTypeSymbol { ConstructedFrom: { } nonGenericFieldTypeSymbol })
                    {
                        continue;
                    }

                    // Check that the field is a D2D resource texture and get the rank
                    int rank = nonGenericFieldTypeSymbol switch
                    {
                        _ when SymbolEqualityComparer.Default.Equals(d2D1ResourceTexture1DSymbol, nonGenericFieldTypeSymbol) => 1,
                        _ when SymbolEqualityComparer.Default.Equals(d2D1ResourceTexture2DSymbol, nonGenericFieldTypeSymbol) => 2,
                        _ when SymbolEqualityComparer.Default.Equals(d2D1ResourceTexture3DSymbol, nonGenericFieldTypeSymbol) => 3,
                        _ => 0
                    };

                    // The field is not a D2D resource texture, just skip it
                    if (rank == 0)
                    {
                        continue;
                    }

                    int? index = null;

                    // Try to get the annotated index for the D2D texture, and track it. This
                    // logic is also in sync with D2DPixelShaderDescriptorGenerator.InputTypes.
                    if (fieldSymbol.TryGetAttributeWithType(d2DResourceTextureIndexAttributeSymbol, out AttributeData? textureIndexData))
                    {
                        if (textureIndexData.TryGetConstructorArgument(0, out int resourceIndex))
                        {
                            index = resourceIndex;
                        }
                    }
                    else
                    {
                        // If the attribute is missing, emit a diagnostic
                        context.ReportDiagnostic(Diagnostic.Create(
                            MissingD2DResourceTextureIndexAttribute,
                            fieldSymbol.Locations.First(),
                            fieldSymbol.Name,
                            typeSymbol));
                    }

                    resourceTextureInfos.Add((index, rank));
                }

                int inputCount = 0;

                // Same logic as in D2DPixelShaderDescriptorGenerator.InputTypes (see there for reference).
                // Keeping this in sync to ensure a consistent behavior (this should be updated if that changes).
                if (typeSymbol.TryGetAttributeWithType(d2DInputCountAttributeSymbol, out AttributeData? inputCountData))
                {
                    inputCount = Math.Max((int)inputCountData.ConstructorArguments[0].Value!, 0);
                }

                // If the input count is invalid, do nothing and avoid emitting potentially incorrect diagnostics
                // based on the resource texture indices with respect to the input count. The generator for the
                // InputType property has already emitted diagnostic for this error, so this analyzer can just
                // wait for users to go back and fix that issue before proceeding.
                if (inputCount is not (>= 0 and <= 8))
                {
                    return;
                }

                // Validate that the resource texture indices don't overlap with the shader inputs
                foreach ((int? index, _) in resourceTextureInfos.WrittenSpan)
                {
                    if (index < inputCount)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            ResourceTextureIndexOverlappingWithInputIndex,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        break;
                    }
                }

                // Validate that no resource texture has an index greater than or equal to 16
                foreach ((int? index, _) in resourceTextureInfos.WrittenSpan)
                {
                    if (index >= 16)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            OutOfRangeResourceTextureIndex,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        break;
                    }
                }

                Span<bool> selectedResourceTextureIndices = stackalloc bool[16];

                selectedResourceTextureIndices.Clear();

                // All input description indices must be unique
                foreach ((int? index, _) in resourceTextureInfos.WrittenSpan)
                {
                    // Skip this path for invalid indices, as those will receive other diagnostics.
                    // We have to do this to avoid generating false positives for the default value.
                    // Because indices could be out of range, we also have to skip any of those.
                    if (index is null or >= 16)
                    {
                        continue;
                    }

                    ref bool isResourceTextureIndexUsed = ref selectedResourceTextureIndices[index ?? 0];

                    if (isResourceTextureIndexUsed)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            RepeatedD2DResourceTextureIndices,
                            typeSymbol.Locations.First(),
                            typeSymbol));

                        break;
                    }

                    isResourceTextureIndexUsed = true;
                }
            }, SymbolKind.NamedType);
        });
    }
}