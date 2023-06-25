using System;
#if DEBUG
using System.Diagnostics;
#endif
using System.Runtime.CompilerServices;

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
    /// The backing field for <see cref="IsDebugOutputEnabled"/>.
    /// </summary>
    private static int isDebugOutputEnabledConfigurationValue;

    /// <summary>
    /// The backing field for <see cref="IsDeviceRemovedExtendedDataEnabled"/>.
    /// </summary>
    private static int isDeviceRemovedExtendedDataEnabledConfigurationValue;

    /// <summary>
    /// The backing field for <see cref="IsGpuTimeoutEnabled"/>.
    /// </summary>
    private static int isGpuTimeoutEnabledConfigurationValue;

    /// <summary>
    /// Gets a value indicating whether or not the debug output is enabled (defaults to <see langword="false"/>).
    /// </summary>
    public static bool IsDebugOutputEnabled
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => GetConfigurationValue(IsDebugOutputEnabledPropertyName, ref isDebugOutputEnabledConfigurationValue);
    }

    /// <summary>
    /// Gets a value indicating whether or not the debug output is enabled (defaults to <see langword="false"/>).
    /// </summary>
    public static bool IsDeviceRemovedExtendedDataEnabled
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => GetConfigurationValue(IsDeviceRemovedExtendedDataEnabledPropertyName, ref isDeviceRemovedExtendedDataEnabledConfigurationValue);
    }

    /// <summary>
    /// Gets a value indicating whether or not the GPU timeout is enabled (defaults to <see langword="true"/>).
    /// </summary>
    public static bool IsGpuTimeoutEnabled
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => GetConfigurationValue(IsGpuTimeoutEnabledPropertyName, ref isGpuTimeoutEnabledConfigurationValue);
    }

    /// <summary>
    /// Gets a configuration value for a specified property.
    /// </summary>
    /// <param name="propertyName">The property name to retrieve the value for.</param>
    /// <param name="cachedResult">The cached result for the target configuration value.</param>
    /// <returns>The value of the specified configuration setting.</returns>
    private static bool GetConfigurationValue(string propertyName, ref int cachedResult)
    {
        // The cached switch value has 3 states:
        //   0: unknown.
        //   1: true
        //   -1: false
        //
        // This method doesn't need to worry about concurrent accesses to the cached result,
        // as even if the configuration value is retrieved twice, that'll always be the same.
        if (cachedResult < 0)
        {
            return false;
        }

        if (cachedResult > 0)
        {
            return true;
        }

        // Get the configuration switch value, or its default
        if (!AppContext.TryGetSwitch(propertyName, out bool isEnabled))
        {
            isEnabled = GetDefaultConfigurationValue(propertyName);
        }

        // Update the cached result
        cachedResult = isEnabled ? 1 : -1;

        return isEnabled;
    }

    /// <summary>
    /// Gets the default configuration value for a given feature switch.
    /// </summary>
    /// <param name="propertyName">The property name to retrieve the value for.</param>
    /// <returns>The default value for the target <paramref name="propertyName"/>.</returns>
    private static bool GetDefaultConfigurationValue(string propertyName)
    {
        // Debug output (always enabled in DEBUG if there is an attached debugger)
        if (propertyName == IsDebugOutputEnabledPropertyName)
        {
#if DEBUG
            return Debugger.IsAttached;
#else
            return false;
#endif
        }

        // Device removed extended data (same as for the debug output)
        if (propertyName == IsDeviceRemovedExtendedDataEnabledPropertyName)
        {
#if DEBUG
            return Debugger.IsAttached;
#else
            return false;
#endif
        }

        // GPU timeout (always enabled by default, disabling it is only recommended for debugging purposes)
        if (propertyName == IsGpuTimeoutEnabledPropertyName)
        {
            return true;
        }

        return false;
    }
}