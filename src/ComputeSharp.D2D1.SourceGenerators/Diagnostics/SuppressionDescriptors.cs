using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators.Diagnostics;

/// <summary>
/// A container for all <see cref="SuppressionDescriptors"/> instances for suppressed diagnostics by analyzers in this project.
/// </summary>
internal static class SuppressionDescriptors
{
    /// <summary>
    /// Gets a <see cref="SuppressionDescriptor"/> for an uninitialized D2D1 resource texture field.
    /// </summary>
    public static readonly SuppressionDescriptor UninitializedD2D1ResourceTextureField = new(
        id: "CMPSD2DSPR0001",
        suppressedDiagnosticId: "CS0649",
        justification: "Fields of type D2D1ResourceTexture1D<T>, D2D1ResourceTexture2D<T> and D2D1ResourceTexture3D<T> are implicitly mapped to resource texture objects into the resulting D2D1 pixel shader for their containing type, and don't need to be initialized");
}