using System;
#if DEBUG
using System.Diagnostics;
#endif
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A container for all shared <see cref="AppContext"/> configuration switches for ComputeSharp.
/// </summary>
/// <remarks>
/// <para>
/// This type uses a very specific setup for configuration switches to ensure ILLink can work the best.
/// This mirrors the architecture of feature switches in the runtime as well, and it's needed so that
/// no static constructor is generated for the type.
/// </para>
/// <para>
/// For more info, see <see href="https://github.com/dotnet/runtime/blob/main/docs/workflow/trimming/feature-switches.md#adding-new-feature-switch"/>.
/// </para>
/// </remarks>
/// <seealso href="https://learn.microsoft.com/dotnet/api/system.diagnostics.codeanalysis.featureswitchdefinitionattribute"/>
internal static class Configuration
{
    /// <summary>
    /// The configuration property name for <see cref="IsDebugOutputEnabled"/>.
    /// </summary>
    private const string IsDebugOutputEnabledPropertyName = "COMPUTESHARP_ENABLE_DEBUG_OUTPUT";

    /// <summary>
    /// The configuration property name for <see cref="IsDeviceRemovedExtendedDataEnabled"/>.
    /// </summary>
    private const string IsDeviceRemovedExtendedDataEnabledPropertyName = "COMPUTESHARP_ENABLE_DEVICE_REMOVED_EXTENDED_DATA";

    /// <summary>
    /// The configuration property name for <see cref="IsGpuTimeoutEnabled"/>.
    /// </summary>
    private const string IsGpuTimeoutEnabledPropertyName = "COMPUTESHARP_ENABLE_GPU_TIMEOUT";

    /// <summary>
    /// Gets a value indicating whether or not the debug output is enabled (defaults to <see langword="false"/>).
    /// </summary>
    [FeatureSwitchDefinition(IsDebugOutputEnabledPropertyName)]
    public static bool IsDebugOutputEnabled { get; } = GetConfigurationValue(IsDebugOutputEnabledPropertyName, defaultValue: IsDebuggerAttached);

    /// <summary>
    /// Gets a value indicating whether or not the device removed extended data is enabled (defaults to <see langword="false"/>).
    /// </summary>
    [FeatureSwitchDefinition(IsDeviceRemovedExtendedDataEnabledPropertyName)]
    public static bool IsDeviceRemovedExtendedDataEnabled { get; } = GetConfigurationValue(IsDeviceRemovedExtendedDataEnabledPropertyName, defaultValue: IsDebuggerAttached);

    /// <summary>
    /// Gets a value indicating whether or not the GPU timeout is enabled (defaults to <see langword="true"/>).
    /// </summary>
    [FeatureSwitchDefinition(IsGpuTimeoutEnabledPropertyName)]
    public static bool IsGpuTimeoutEnabled { get; } = GetConfigurationValue(IsGpuTimeoutEnabledPropertyName, defaultValue: true);

    /// <summary>
    /// Gets whether a debugger is attached, for switches that are only enabled by default while debugging.
    /// </summary>
    private static bool IsDebuggerAttached =>
#if DEBUG
        Debugger.IsAttached;
#else
        false;
#endif

    /// <summary>
    /// Gets a configuration value for a specified property.
    /// </summary>
    /// <param name="propertyName">The property name to retrieve the value for.</param>
    /// <param name="defaultValue">The default property value to use as a fallback.</param>
    /// <returns>The value of the specified configuration setting.</returns>
    private static bool GetConfigurationValue([ConstantExpected] string propertyName, bool defaultValue)
    {
        return AppContext.TryGetSwitch(propertyName, out bool isEnabled) ? isEnabled : defaultValue;
    }
}