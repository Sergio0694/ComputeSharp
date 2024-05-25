using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.WinUI.SourceGenerators;

/// <summary>
/// A source generator creating implementations of effect properties.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class CanvasEffectPropertyGenerator : IIncrementalGenerator
{
    /// <summary>
    /// The name of generator to include in the generated code.
    /// </summary>
    private const string GeneratorName = "ComputeSharp.D2D1.WinUI.CanvasEffectPropertyGenerator";

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<object?> propertyInfo =
            context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "ComputeSharp.D2D1.WinUI.GeneratedCanvasEffectPropertyAttribute",
                Execute.IsCandidatePropertyDeclaration,
                static (context, token) => (object?)null);
    }
}