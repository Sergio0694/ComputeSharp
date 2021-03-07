using System;
using System.Drawing;
using TerraFX.Interop;

namespace ComputeSharp.SwapChain.Backend
{
    /// <summary>
    /// A class representing a simple Win32 application.
    /// </summary>
    internal abstract class Win32Application
    {
        /// <summary>
        /// Gets the title of the current application.
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// Initializes the current application.
        /// </summary>
        /// <param name="size">The window size.</param>
        /// <param name="hwnd">The handle for the window.</param>
        public abstract void OnInitialize(Size size, HWND hwnd);

        /// <summary>
        /// Resizes the current application.
        /// </summary>
        /// <param name="size">The new window size.</param>
        public abstract void OnResize(Size size);

        /// <summary>
        /// Updates the current application.
        /// </summary>
        /// <param name="time">The current time since the start of the application.</param>
        public abstract void OnUpdate(TimeSpan time);
    }
}
