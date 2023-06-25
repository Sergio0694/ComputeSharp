using System;
#if DEBUG
using System.Diagnostics;
#endif
using System.Runtime.CompilerServices;

/// <summary>
/// A container for all shared <see cref="AppContext"/> configuration switches for ComputeSharp.
/// </summary>
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
    /// The configuration property name for <see cref="IsGpuTimeoutDisabled"/>.
    /// </summary>
    private const string IsGpuTimeoutDisabledPropertyName = "COMPUTESHARP_DISABLE_GPU_TIMEOUT";

    /// <summary>
    /// The backing field for <see cref="IsDebugOutputEnabled"/>.
    /// </summary>
    private static readonly bool IsDebugOutputEnabledConfigurationValue = GetConfigurationValue(IsDebugOutputEnabledPropertyName);

    /// <summary>
    /// The backing field for <see cref="IsDeviceRemovedExtendedDataEnabled"/>.
    /// </summary>
    private static readonly bool IsDeviceRemovedExtendedDataEnabledConfigurationValue = GetConfigurationValue(IsDeviceRemovedExtendedDataEnabledPropertyName);

    /// <summary>
    /// The backing field for <see cref="IsGpuTimeoutDisabled"/>.
    /// </summary>
    private static readonly bool IsGpuTimeoutDisabledConfigurationValue = GetConfigurationValue(IsGpuTimeoutDisabledPropertyName);

    /// <summary>
    /// Gets a value indicating whether or not the debug output is enabled (defaults to <see langword="false"/>).
    /// </summary>
    public static bool IsDebugOutputEnabled
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => IsDebugOutputEnabledConfigurationValue;
    }

    /// <summary>
    /// Gets a value indicating whether or not the debug output is enabled (defaults to <see langword="false"/>).
    /// </summary>
    public static bool IsDeviceRemovedExtendedDataEnabled
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => IsDeviceRemovedExtendedDataEnabledConfigurationValue;
    }

    /// <summary>
    /// Gets a value indicating whether or not the GPU timeout is disabled (defaults to <see langword="false"/>).
    /// </summary>
    public static bool IsGpuTimeoutDisabled
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => IsGpuTimeoutDisabledConfigurationValue;
    }

    /// <summary>
    /// Gets a configuration value for a specified property.
    /// </summary>
    /// <param name="propertyName">The property name to retrieve the value for.</param>
    /// <returns>The value of the specified configuration setting.</returns>
    private static bool GetConfigurationValue(string propertyName)
    {
#if DEBUG
        if (Debugger.IsAttached && propertyName != IsGpuTimeoutDisabledPropertyName)
        {
            return true;
        }
#endif

        if (AppContext.TryGetSwitch(propertyName, out bool isEnabled))
        {
            return isEnabled;
        }

        return false;
    }
}