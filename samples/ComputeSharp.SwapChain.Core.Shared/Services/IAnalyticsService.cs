using System;

#nullable enable

namespace ComputeSharp.SwapChain.Core.Services;

/// <summary>
/// The default <see langword="interface"/> for an analytics service.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Logs an event with a specified title and optional properties.
    /// </summary>
    /// <param name="title">The title of the event to track.</param>
    /// <param name="data">The optional event properties.</param>
    void Log(string title, params (string Property, object? Value)[]? data);

    /// <summary>
    /// Logs an exception with a specified title and optional properties.
    /// </summary>
    /// <param name="title">The title of the event to track.</param>
    /// <param name="exception">The exception that has been thrown.</param>
    /// <param name="data">The optional event properties.</param>
    void Log(string title, Exception exception, params (string Property, object? Value)[]? data);
}
