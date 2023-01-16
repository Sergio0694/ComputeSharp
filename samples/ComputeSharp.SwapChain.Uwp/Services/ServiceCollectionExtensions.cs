using System;
using System.Diagnostics;
using ComputeSharp.SwapChain.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ComputeSharp.SwapChain.Uwp.Services;

/// <summary>
/// Extensions for the <see cref="ServiceCollection"/> type.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the correct <see cref="IAnalyticsService"/> registration to the given <see cref="IServiceCollection"/> instance.
    /// </summary>
    /// <param name="services">The input <see cref="IServiceCollection"/> instance to use to register services.</param>
    /// <returns>The same instance as <paramref name="services"/>.</returns>
    public static IServiceCollection AddAnalyticsService(this IServiceCollection services)
    {
        if (Guid.TryParse(ServiceTokens.AppService, out _) && !Debugger.IsAttached)
        {
            _ = services.AddSingleton<IAnalyticsService>(new AppCenterService(ServiceTokens.AppService));
        }
        else
        {
            _ = services.AddSingleton<IAnalyticsService, DebugAnalyticsService>();
        }

        return services;
    }
}
