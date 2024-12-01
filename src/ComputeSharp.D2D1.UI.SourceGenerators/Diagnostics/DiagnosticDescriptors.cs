using Microsoft.CodeAnalysis;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.SourceGenerators;
#else
namespace ComputeSharp.D2D1.WinUI.SourceGenerators;
#endif

/// <summary>
/// A container for all <see cref="DiagnosticDescriptor"/> instances for errors reported by analyzers in this project.
/// </summary>
internal static class DiagnosticDescriptors
{
    /// <summary>
    /// The prefix of all diagnostics for available analyzers.
    /// </summary>
    private const string DiagnosticIdPrefix =
#if WINDOWS_UWP
        "CMPSD2DUWP";
#else
        "CMPSD2DWINUI";
#endif

    /// <summary>
    /// The category of all diagnostics for available analyzers.
    /// </summary>
    private const string DiagnosticCategory =
#if WINDOWS_UWP
        "ComputeSharp.D2D1.Uwp.Effects";
#else
        "ComputeSharp.D2D1.WinUI.Effects";
#endif

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid CanvasEffect property.
    /// <para>
    /// Format: <c>"The property "{0}" is not in a type that derives from CanvasEffect ([GeneratedCanvasEffectProperty] must be used in effect types that extend CanvasEffect)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGeneratedCanvasEffectPropertyContainingType = new(
        id: $"{DiagnosticIdPrefix}0001",
        title: "Invalid generated CanvasEffect property containing type",
        messageFormat: """The property "{0}" is not in a type that derives from CanvasEffect ([GeneratedCanvasEffectProperty] must be used in effect types that extend CanvasEffect)""",
        category: DiagnosticCategory,
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
        id: $"{DiagnosticIdPrefix}0002",
        title: "Invalid generated CanvasEffect property accessors",
        messageFormat: """The property "{0}" does not have a getter and a setter ([GeneratedCanvasEffectProperty] must be used on properties with get and set accessors)""",
        category: DiagnosticCategory,
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
        id: $"{DiagnosticIdPrefix}0003",
        title: "Invalid generated CanvasEffect property declaration (static member)",
        messageFormat: """The property "{0}" is static ([GeneratedCanvasEffectProperty] must be used on instance properties)""",
        category: DiagnosticCategory,
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
        id: $"{DiagnosticIdPrefix}0004",
        title: "Invalid generated CanvasEffect property declaration (not incomplete partial definition)",
        messageFormat: """The property "{0}" is not an incomplete partial definition ([GeneratedCanvasEffectProperty] must be used on partial property definitions with no implementation part)""",
        category: DiagnosticCategory,
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
        id: $"{DiagnosticIdPrefix}0005",
        title: "Invalid generated CanvasEffect property declaration (returns byref)",
        messageFormat: """The property "{0}" returns a value by reference ([GeneratedCanvasEffectProperty] must be used on properties returning a type by value)""",
        category: DiagnosticCategory,
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
        id: $"{DiagnosticIdPrefix}0006",
        title: "Invalid generated CanvasEffect property declaration (returns ref struct type)",
        messageFormat: """The property "{0}" returns a ref struct value ([GeneratedCanvasEffectProperty] must be used on properties with a type that is not a ref struct)""",
        category: DiagnosticCategory,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A property using [GeneratedCanvasEffectProperty] has a ref struct type ([GeneratedCanvasEffectProperty] must be used on properties with a type that is not a ref struct).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a CanvasEffect property with invalid accessors.
    /// <para>
    /// Format: <c>"Using [GeneratedCanvasEffectProperty] requires the C# language version to be set to 'preview', as support for the 'field' keyword is needed by the source generators to emit valid code (add &lt;LangVersion&gt;preview&lt;/LangVersion&gt; to your .csproj/.props file)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor CSharpLanguageVersionIsNotPreview = new(
        id: $"{DiagnosticIdPrefix}0007",
        title: "C# language version is not 'preview'",
        messageFormat: """Using [GeneratedCanvasEffectProperty] requires the C# language version to be set to 'preview', as support for the 'field' keyword is needed by the source generators to emit valid code (add <LangVersion>preview</LangVersion> to your .csproj/.props file)""",
        category: DiagnosticCategory,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The C# language version must be set to 'preview' when using [GeneratedComputeShaderDescriptor] for the source generators to emit valid code (the <LangVersion>preview</LangVersion> option must be set in the .csproj/.props file).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when a property should generate nullability warnings.
    /// <para>
    /// Format: <c>The property '{0}' is not annotated as nullable, but it might contain a null value upon exiting the constructor (consider adding the 'required' modifier, setting a non-null default value if possible, or declaring the property as nullable)</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor NonNullablePropertyDeclarationIsNotEnforced = new(
        id: $"{DiagnosticIdPrefix}0008",
        title: "Non-nullable dependency property is not guaranteed to not be null",
        messageFormat: """The property "{0}" is not annotated as nullable, but it might contain a null value upon exiting the constructor (consider adding the 'required' modifier, setting a non-null default value if possible, or declaring the property as nullable)""",
        category: DiagnosticCategory,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Non-nullable properties annotated with [GeneratedCanvasEffectProperty] should guarantee that their values will not be null upon exiting the constructor. This can be enforced by adding the 'required' modifier, setting a non-null default value if possible, or declaring the property as nullable.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
}