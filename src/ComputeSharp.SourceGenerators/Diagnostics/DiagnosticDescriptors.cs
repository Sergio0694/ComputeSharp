using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators.Diagnostics
{
    /// <summary>
    /// A container for all <see cref="DiagnosticDescriptor"/> instances for errors reported by analyzers in this project.
    /// </summary>
    internal static class DiagnosticDescriptors
    {
        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid shader field.
        /// <para>
        /// Format: <c>"The compute shader of type {0} contains a field \"{1}\" of an invalid type {2}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidShaderField { get; } = new(
            id: "CMPS0001",
            title: "Invalid shader field",
            messageFormat: "The compute shader of type {0} contains a field \"{1}\" of an invalid type {2}",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "A type representing a compute shader contains a field of a type that is not supported in HLSL.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid group shared field type.
        /// <para>
        /// Format: <c>"The compute shader of type {0} contains a group shared field \"{1}\" of an invalid type {2}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidGroupSharedFieldType { get; } = new(
            id: "CMPS0002",
            title: "Invalid group shared field type",
            messageFormat: "The compute shader of type {0} contains a group shared field \"{1}\" of an invalid type {2} (it must be an array)",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "A group shared field must be of an array type.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid group shared field element type.
        /// <para>
        /// Format: <c>"The compute shader of type {0} contains a group shared field \"{1}\" of an invalid element type {2}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidGroupSharedFieldElementType { get; } = new(
            id: "CMPS0003",
            title: "Invalid group shared field element type",
            messageFormat: "The compute shader of type {0} contains a group shared field \"{1}\" of an invalid type {2} (it must be a primitive or unmanaged type)",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "A group shared field element must be of a primitive or unmanaged type.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid group shared field declaration.
        /// <para>
        /// Format: <c>"The compute shader of type {0} contains a group shared field \"{1}\" that is not static"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidGroupSharedFieldDeclaration { get; } = new(
            id: "CMPS0004",
            title: "Invalid group shared field declaration",
            messageFormat: "The compute shader of type {0} contains a group shared field \"{1}\" that is not static",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "A group shared field must be static.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with no resources.
        /// <para>
        /// Format: <c>"The compute shader of type {0} contains no resources to work on"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor MissingShaderResources { get; } = new(
            id: "CMPS0005",
            title: "Missing shader resources",
            messageFormat: "The compute shader of type {0} contains no resources to work on",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "A compute shader must contain at least one resource.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="ThreadIds"/> usage.
        /// <para>
        /// Format: <c>"The ThreadIds type is used in method {0} of type {1}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidThreadIdsUsage { get; } = new(
            id: "CMPS0006",
            title: "Invalid ThreadIds usage",
            messageFormat: "The ThreadIds type can only be used within the main body of a compute shader",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "The ThreadIds type can only be used within the main body of a compute shader.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="GroupIds"/> usage.
        /// <para>
        /// Format: <c>"The GroupIds type is used in method {0} of type {1}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidGroupIdsUsage { get; } = new(
            id: "CMPS0007",
            title: "Invalid GroupIds usage",
            messageFormat: "The GroupIds type can only be used within the main body of a compute shader",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "The GroupIds type can only be used within the main body of a compute shader.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="GroupSize"/> usage.
        /// <para>
        /// Format: <c>"The GroupSize type is used in method {0} of type {1}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidGroupSizeUsage { get; } = new(
            id: "CMPS0008",
            title: "Invalid GroupSize usage",
            messageFormat: "The GroupSize type can only be used within the main body of a compute shader",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "The GroupSize type can only be used within the main body of a compute shader.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="WarpIds"/> usage.
        /// <para>
        /// Format: <c>"The WarpIds type is used in method {0} of type {1}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidWarpIdsUsage { get; } = new(
            id: "CMPS0009",
            title: "Invalid WarpIds usage",
            messageFormat: "The WarpIds type can only be used within the main body of a compute shader",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "The WarpIds type can only be used within the main body of a compute shader.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
    }
}
