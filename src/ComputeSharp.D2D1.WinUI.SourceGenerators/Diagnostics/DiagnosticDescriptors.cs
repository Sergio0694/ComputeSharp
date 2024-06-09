using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.WinUI.SourceGenerators;

/// <summary>
/// A container for all <see cref="DiagnosticDescriptor"/> instances for errors reported by analyzers in this project.
/// </summary>
internal static class DiagnosticDescriptors
{
    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid CanvasEffect property.
    /// <para>
    /// Format: <c>"The property "{0}" is not in a type that derives from CanvasEffect ([GeneratedCanvasEffectProperty] must be used in effect types that extend CanvasEffect)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGeneratedCanvasEffectPropertyContainingType = new(
        id: "CMPSD2DWINUI0001",
        title: "Invalid generated CanvasEffect property containing type",
        messageFormat: """The property "{0}" is not in a type that derives from CanvasEffect ([GeneratedCanvasEffectProperty] must be used in effect types that extend CanvasEffect)""",
        category: "ComputeSharp.D2D1.WinUI.Effects",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A property using [GeneratedCanvasEffectProperty] is in a type that does not derive from CanvasEffect ([GeneratedCanvasEffectProperty] must be used in effect types that extend CanvasEffect).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
}