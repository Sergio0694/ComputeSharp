using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Testing.Platform.Builder;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using WinRT;

namespace ComputeSharp.D2D1.WinUI.Tests;

internal static class Program
{
    [STAThread]
    internal static int Main(string[] args)
    {
        return MainAsync(args).GetAwaiter().GetResult();
    }

    internal static async Task<int> MainAsync(string[] args)
    {
        ComWrappersSupport.InitializeComWrappers();
        Application.Start((p) =>
        {
            DispatcherQueueSynchronizationContext context = new(DispatcherQueue.GetForCurrentThread());
            SynchronizationContext.SetSynchronizationContext(context);
            _ = new App();
        });

        ITestApplicationBuilder builder = await TestApplication.CreateBuilderAsync(args);
        Microsoft.Testing.Platform.MSBuild.TestingPlatformBuilderHook.AddExtensions(builder, args);
        Microsoft.Testing.Extensions.Telemetry.TestingPlatformBuilderHook.AddExtensions(builder, args);
        Microsoft.VisualStudio.TestTools.UnitTesting.TestingPlatformBuilderHook.AddExtensions(builder, args);
        using ITestApplication app = await builder.BuildAsync();
        return await app.RunAsync();
    }
}

