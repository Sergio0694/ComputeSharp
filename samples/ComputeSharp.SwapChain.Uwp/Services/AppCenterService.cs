using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComputeSharp.SwapChain.Core.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

#nullable enable

namespace ComputeSharp.SwapChain.Uwp.Services;

/// <summary>
/// An <see cref="IAnalyticsService"/> implementation using AppCenter.
/// </summary>
public sealed class AppCenterService : IAnalyticsService
{
    /// <summary>
    /// The maximum length for any property name and value
    /// </summary>
    /// <remarks>It's 125, but one character is reserved for the leading '|' to indicate trimming.</remarks>
    private const int PropertyStringMaxLength = 124;

    /// <inheritdoc/>
    public AppCenterService(string secret)
    {
        AppCenter.Start(secret, typeof(Crashes), typeof(Analytics));
    }

    /// <inheritdoc/>
    public void Log(string title, params (string Property, string Value)[]? data)
    {
        IDictionary<string, string>? properties = data?.ToDictionary(
            pair => pair.Property,
            pair => pair.Value.Length <= PropertyStringMaxLength
                ? pair.Value
                : $"|{pair.Value.Substring(pair.Value.Length - PropertyStringMaxLength)}");

        Analytics.TrackEvent(title, properties);
    }
}
