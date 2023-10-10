using System;
using System.Diagnostics;
using System.Text;

namespace ComputeSharp.SwapChain.Core.Services;

/// <summary>
/// A <see langword="class"/> that manages the analytics service in a test environment.
/// </summary>
public sealed class DebugAnalyticsService : IAnalyticsService
{
    /// <inheritdoc/>
    public void Log(string title, params (string Property, object? Value)[]? data)
    {
        StringBuilder builder = new();

        _ = builder.AppendLine($"[EVENT]: \"{title}\"");

        if (data is not null)
        {
            foreach ((string Property, object? Value) info in data)
            {
                _ = builder.AppendLine($">> {info.Property}: \"{info.Value ?? "<NULL>"}\"");
            }
        }

        Trace.Write(builder);
    }

    /// <inheritdoc/>
    public void Log(Exception exception, params (string Property, object? Value)[]? data)
    {
        StringBuilder builder = new();

        _ = builder.AppendLine($"[EXCEPTION]: \"{exception.GetType()}\"");
        _ = builder.AppendLine(">> Stack trace");
        _ = builder.AppendLine(exception.StackTrace);

        if (data is not null)
        {
            foreach ((string Property, object? Value) info in data)
            {
                _ = builder.AppendLine($">> {info.Property}: \"{info.Value ?? "<NULL>"}\"");
            }
        }

        Trace.Write(builder);
    }
}