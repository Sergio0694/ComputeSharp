// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp.SourceGenerators.Constants;
#else
using ComputeSharp.D2D1.WinUI.SourceGenerators.Constants;
#endif
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
#if WINDOWS_UWP
using static ComputeSharp.D2D1.Uwp.SourceGenerators.DiagnosticDescriptors;
#else
using static ComputeSharp.D2D1.WinUI.SourceGenerators.DiagnosticDescriptors;
#endif

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.SourceGenerators;
#else
namespace ComputeSharp.D2D1.WinUI.SourceGenerators;
#endif

/// <summary>
/// A diagnostic analyzer that generates a warning when a property with <c>[GeneratedCanvasEffectProperty]</c> would generate a nullability annotations violation.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidGeneratedCanvasEffectPropertyNonNullableDeclarationAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [NonNullablePropertyDeclarationIsNotEnforced];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the [GeneratedCanvasEffectProperty] symbols
            if (context.Compilation.GetTypeByMetadataName(WellKnownTypeNames.GeneratedCanvasEffectPropertyAttribute) is not { } generatedCanvasEffectPropertyAttributeSymbol)
            {
                return;
            }

            // Attempt to also get the [MaybeNull] symbolS (there might be multiples, due to polyfills)
            ImmutableArray<INamedTypeSymbol> maybeNullAttributeSymbol = context.Compilation.GetTypesByMetadataName("System.Diagnostics.CodeAnalysis.MaybeNullAttribute");

            context.RegisterSymbolAction(context =>
            {
                // Validate that we do have a property, and that it is of some type that can be explicitly nullable.
                // We're intentionally ignoring 'Nullable<T>' values here, since those are by definition nullable.
                // Additionally, we only care about properties that are explicitly marked as not nullable.
                // Lastly, we can skip required properties, since for those it's completely fine to be non-nullable.
                if (context.Symbol is not IPropertySymbol { Type.IsValueType: false, NullableAnnotation: NullableAnnotation.NotAnnotated, IsRequired: false } propertySymbol)
                {
                    return;
                }

                // If the property is not using [GeneratedCanvasEffectProperty], there's nothing to do
                if (!propertySymbol.TryGetAttributeWithType(generatedCanvasEffectPropertyAttributeSymbol, out AttributeData? attributeData))
                {
                    return;
                }

                // If the property does not have [MaybeNull], then emit the diagnostic
                if (!propertySymbol.HasAttributeWithAnyType(maybeNullAttributeSymbol))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        NonNullablePropertyDeclarationIsNotEnforced,
                        attributeData.GetLocation(),
                        propertySymbol));
                }
            }, SymbolKind.Property);
        });
    }
}
