namespace ComputeSharp.SourceGeneration.Constants;

/// <summary>
/// The well known names for tracking steps, to test the incremental generators.
/// </summary>
internal static class WellKnownTrackingNames
{
    /// <summary>
    /// The initial <see cref="Microsoft.CodeAnalysis.SyntaxValueProvider.ForAttributeWithMetadataName"/> transform node.
    /// </summary>
    public const string Execute = nameof(Execute);

    /// <summary>
    /// The filtered transform with just output diagnostics.
    /// </summary>
    public const string Diagnostics = nameof(Diagnostics);

    /// <summary>
    /// The filtered transform with just output sources.
    /// </summary>
    public const string Output = nameof(Output);
}
