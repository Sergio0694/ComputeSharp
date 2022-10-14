using System;
using System.Collections.Generic;
using System.Linq;
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
    public void Log(string title, params (string Property, object? Value)[]? data)
    {
        Analytics.TrackEvent(title, GetProperties(data));
    }

    /// <inheritdoc/>
    public void Log(Exception exception, params (string Property, object? Value)[]? data)
    {
        Crashes.TrackError(exception, GetProperties(data));
    }

    /// <summary>
    /// Gets the additional logging properties from the input data.
    /// </summary>
    /// <param name="data">The optional event properties.</param>
    /// <returns>The additional logging properties to track.</returns>
    private static IDictionary<string, string>? GetProperties((string Property, object? Value)[]? data)
    {
        return
            data?.ToDictionary(
            pair => pair.Property,
            pair =>
            {
                string text = (pair.Value ?? "<NULL>").ToString();

                return text.Length <= PropertyStringMaxLength
                    ? text
                    : $"|{text.Substring(text.Length - PropertyStringMaxLength)}";
            });
    }
}