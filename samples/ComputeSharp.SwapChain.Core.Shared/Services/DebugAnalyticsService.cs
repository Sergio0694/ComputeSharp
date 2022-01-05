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
    public void Log(string title, params (string Property, string Value)[]? data)
    {
        StringBuilder builder = new();

        builder.AppendLine($"[EVENT]: \"{title}\"");

        if (data is not null)
        {
            foreach (var info in data)
            {
                builder.AppendLine($">> {info.Property}: \"{info.Value}\"");
            }
        }

        Trace.Write(builder);
    }
}
