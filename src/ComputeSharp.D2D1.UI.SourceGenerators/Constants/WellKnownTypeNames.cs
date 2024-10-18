#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.SourceGenerators.Constants;
#else
namespace ComputeSharp.D2D1.WinUI.SourceGenerators.Constants;
#endif

/// <summary>
/// The well known names for types used by source generators and analyzers.
/// </summary>
internal static class WellKnownTypeNames
{
    /// <summary>
    /// The fully qualified type name for the <c>[GeneratedCanvasEffectPropertyAttribute]</c> type.
    /// </summary>
    public const string GeneratedCanvasEffectPropertyAttribute =
#if WINDOWS_UWP
        "ComputeSharp.D2D1.Uwp.GeneratedCanvasEffectPropertyAttribute";
#else
        "ComputeSharp.D2D1.WinUI.GeneratedCanvasEffectPropertyAttribute";
#endif

    /// <summary>
    /// The fully qualified type name for the <c>[CanvasEffect]</c> type.
    /// </summary>
    public const string CanvasEffect =
#if WINDOWS_UWP
        "ComputeSharp.D2D1.Uwp.CanvasEffect";
#else
        "ComputeSharp.D2D1.WinUI.CanvasEffect";
#endif
}
