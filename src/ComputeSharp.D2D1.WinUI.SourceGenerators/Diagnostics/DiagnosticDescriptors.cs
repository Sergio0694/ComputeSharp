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

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a CanvasEffect property with invalid accessors.
    /// <para>
    /// Format: <c>"The property "{0}" does not have a getter and a setter ([GeneratedCanvasEffectProperty] must be used on properties with get and set accessors)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGeneratedCanvasEffectPropertyAccessors = new(
        id: "CMPSD2DWINUI0002",
        title: "Invalid generated CanvasEffect property accessors",
        messageFormat: """The property "{0}" does not have a getter and a setter ([GeneratedCanvasEffectProperty] must be used on properties with get and set accessors)""",
        category: "ComputeSharp.D2D1.WinUI.Effects",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A property using [GeneratedCanvasEffectProperty] does not have a getter and a setter ([GeneratedCanvasEffectProperty] must be used on properties with get and set accessors).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a static CanvasEffect property.
    /// <para>
    /// Format: <c>"The property "{0}" is static ([GeneratedCanvasEffectProperty] must be used on instance properties)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGeneratedCanvasEffectPropertyDeclarationIsStatic = new(
        id: "CMPSD2DWINUI0003",
        title: "Invalid generated CanvasEffect property declaration (static member)",
        messageFormat: """The property "{0}" is static ([GeneratedCanvasEffectProperty] must be used on instance properties)""",
        category: "ComputeSharp.D2D1.WinUI.Effects",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A property using [GeneratedCanvasEffectProperty] cannot be static ([GeneratedCanvasEffectProperty] must be used on instance properties).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a CanvasEffect property with invalid accessors.
    /// <para>
    /// Format: <c>"The property "{0}" is not an incomplete partial definition ([GeneratedCanvasEffectProperty] must be used on partial property definitions with no implementation part)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGeneratedCanvasEffectPropertyDeclarationIsNotIncompletePartialDefinition = new(
        id: "CMPSD2DWINUI0004",
        title: "Invalid generated CanvasEffect property declaration (not incomplete partial definition)",
        messageFormat: """The property "{0}" is not an incomplete partial definition ([GeneratedCanvasEffectProperty] must be used on partial property definitions with no implementation part)""",
        category: "ComputeSharp.D2D1.WinUI.Effects",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A property using [GeneratedCanvasEffectProperty] is either not partial, or a partial implementation part ([GeneratedCanvasEffectProperty] must be used on partial property definitions with no implementation par).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a CanvasEffect property with invalid accessors.
    /// <para>
    /// Format: <c>"The property "{0}" returns a value by reference ([GeneratedCanvasEffectProperty] must be used on properties returning a type by value)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGeneratedCanvasEffectPropertyDeclarationReturnsByRef = new(
        id: "CMPSD2DWINUI0005",
        title: "Invalid generated CanvasEffect property declaration (returns byref)",
        messageFormat: """The property "{0}" returns a value by reference ([GeneratedCanvasEffectProperty] must be used on properties returning a type by value)""",
        category: "ComputeSharp.D2D1.WinUI.Effects",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A property using [GeneratedCanvasEffectProperty] returns a value by reference ([GeneratedCanvasEffectProperty] must be used on properties returning a type by value).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a CanvasEffect property with invalid accessors.
    /// <para>
    /// Format: <c>"The property "{0}" returns a ref struct value ([GeneratedCanvasEffectProperty] must be used on properties with a type that is not a ref struct)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGeneratedCanvasEffectPropertyDeclarationReturnsRefLikeType = new(
        id: "CMPSD2DWINUI0006",
        title: "Invalid generated CanvasEffect property declaration (returns ref struct type)",
        messageFormat: """The property "{0}" returns a ref struct value ([GeneratedCanvasEffectProperty] must be used on properties with a type that is not a ref struct)""",
        category: "ComputeSharp.D2D1.WinUI.Effects",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A property using [GeneratedCanvasEffectProperty] has a ref struct type ([GeneratedCanvasEffectProperty] must be used on properties with a type that is not a ref struct).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
}