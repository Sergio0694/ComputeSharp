using CommunityToolkit.Mvvm.DependencyInjection;
using ComputeSharp.SwapChain.Core.Services;
using ComputeSharp.SwapChain.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;

#if WINDOWS_UWP
namespace ComputeSharp.SwapChain.Uwp;
#else
namespace ComputeSharp.SwapChain.WinUI;
#endif

public sealed partial class App
{
    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private static void ConfigureServices()
    {
        ServiceCollection services = new();

#if !DEBUG
        if (!Debugger.IsAttached &&
            Assembly.GetExecutingAssembly().TryReadAllTextFromManifestFile("Assets/ServiceTokens/AppCenter.txt", out string? secret) &&
            Guid.TryParse(secret, out _))
        {
            services.AddSingleton<IAnalyticsService>(new AppCenterService(secret!));
        }
        else
        {
            services.AddSingleton<IAnalyticsService, DebugAnalyticsService>();
        }
#else
        services.AddSingleton<IAnalyticsService, DebugAnalyticsService>();
#endif

        services.AddTransient<MainViewModel>();

        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
    }
}
