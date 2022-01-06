using System;
using System.Diagnostics;
using System.Text;

#nullable enable

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

        builder.AppendLine($"[EVENT]: \"{title}\"");

        if (data is not null)
        {
            foreach (var info in data)
            {
                builder.AppendLine($">> {info.Property}: \"{info.Value ?? "<NULL>"}\"");
            }
        }

        Trace.Write(builder);
    }

    /// <inheritdoc/>
    public void Log(string title, Exception exception, params (string Property, object? Value)[]? data)
    {
        StringBuilder builder = new();

        builder.AppendLine($"[EXCEPTION]: \"{title}\"");
        builder.AppendLine($">> Type: \"{exception.GetType()}\"");
        builder.AppendLine(">> Stack trace");
        builder.AppendLine(exception.StackTrace);

        if (data is not null)
        {
            foreach (var info in data)
            {
                builder.AppendLine($">> {info.Property}: \"{info.Value ?? "<NULL>"}\"");
            }
        }

        Trace.Write(builder);
    }
}
