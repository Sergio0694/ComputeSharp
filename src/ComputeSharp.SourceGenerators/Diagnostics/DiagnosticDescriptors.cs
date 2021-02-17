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
    }
}
