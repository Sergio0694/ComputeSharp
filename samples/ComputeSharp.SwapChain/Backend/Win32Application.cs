using System;
using TerraFX.Interop.Windows;

namespace ComputeSharp.SwapChain.Backend;

/// <summary>
/// A class representing a simple Win32 application.
/// </summary>
internal abstract class Win32Application
{
    /// <summary>
    /// Initializes the current application.
    /// </summary>
    /// <param name="hwnd">The handle for the window.</param>
    public abstract void OnInitialize(HWND hwnd);

    /// <summary>
    /// Resizes the current application.
    /// </summary>
    public abstract void OnResize();

    /// <summary>
    /// Updates the current application.
    /// </summary>
    /// <param name="time">The current time since the start of the application.</param>
    public abstract void OnUpdate(TimeSpan time);
}