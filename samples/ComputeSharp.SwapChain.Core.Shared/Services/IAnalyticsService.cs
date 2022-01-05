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
    void Log(string title, params (string Property, string Value)[]? data);
}
