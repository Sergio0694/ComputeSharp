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
            title: "Invalid group shared field type",
            messageFormat: "The compute shader of type {0} contains a group shared field \"{1}\" of an invalid type {2} (it must be a primitive or unmanaged type)",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "A group shared field element must be of a primitive or unmanaged type.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
    }
}
