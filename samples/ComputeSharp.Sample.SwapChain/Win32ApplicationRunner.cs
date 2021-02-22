using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Sample.SwapChain
{
    internal abstract class Win32Application : IDisposable
    {
        public abstract string Title { get; }

        public abstract void OnInitialize(Size size, HWND hwnd);

        public abstract void OnResize(Size size);

        public abstract void OnUpdate(TimeSpan time);

        public abstract void Dispose();
    }

    /// <summary>
    /// A helper class to manage the creation and execution of Win32 applications.
    /// </summary>
    internal unsafe static class Win32ApplicationRunner
    {
        private static bool isResizing = false;
        private static bool isPaused = false;
        private static HWND hwnd;
        private static Size windowSize;
        private static Stopwatch stopwatch = null!;
        private static Win32Application application = null!;
        private static long elapsedMillisecondsForLastTitleUpdate;
        private static int elapsedFramesSinceLastTitleUpdate;

        public static int Run<T>()
            where T : Win32Application, new()
        {
            application = new T();

            IntPtr hInstance = FX.GetModuleHandleW(null);
            uint width = 1280;
            uint height = 720;

            fixed (char* name = Assembly.GetExecutingAssembly().FullName)
            fixed (char* windowTitle = application.Title)
            {
                // Initialize the window class
                WNDCLASSEXW windowClassEx = new()
                {
                    cbSize = (uint)sizeof(WNDCLASSEXW),
                    style = FX.CS_HREDRAW | FX.CS_VREDRAW,
                    lpfnWndProc = &WindowProc,
                    hInstance = hInstance,
                    hCursor = FX.LoadCursorW(IntPtr.Zero, FX.MAKEINTRESOURCE(32512)),
                    lpszClassName = (ushort*)name
                };

                // Register the window class
                _ = FX.RegisterClassExW(&windowClassEx);

                Rectangle windowRect = new(0, 0, (int)width, (int)height);

                // Set the target window size
                _ = FX.AdjustWindowRect((RECT*)&windowRect, FX.WS_OVERLAPPEDWINDOW, FX.FALSE);

                height = (uint)(windowRect.Bottom - windowRect.Top);
                width = (uint)(windowRect.Right - windowRect.Left);

                // Create the window and store a handle to it
                hwnd = FX.CreateWindowExW(
                    0,
                    windowClassEx.lpszClassName,
                    (ushort*)windowTitle,
                    FX.WS_OVERLAPPEDWINDOW,
                    FX.CW_USEDEFAULT,
                    FX.CW_USEDEFAULT,
                    (int)width,
                    (int)height,
                    HWND.NULL,
                    HMENU.NULL,
                    hInstance,
                    (void*)GCHandle.ToIntPtr(GCHandle.Alloc(application))
                );
            }

            windowSize = new Size((int)height, (int)width);

            // Initialize the application
            application.OnInitialize(windowSize, hwnd);

            // Display the window
            _ = FX.ShowWindow(hwnd, FX.SW_SHOWDEFAULT);

            // Start the timer
            stopwatch = Stopwatch.StartNew();

            MSG msg = default;

            // Process any messages in the queue
            while (msg.message != FX.WM_QUIT)
            {
                if (FX.PeekMessageW(&msg, HWND.NULL, 0, 0, FX.PM_REMOVE) != 0)
                {
                    _ = FX.TranslateMessage(&msg);
                    _ = FX.DispatchMessageW(&msg);
                }
                else if (!isPaused || true)
                {
                    RunApp();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }

            application.Dispose();

            // Return this part of the WM_QUIT message to Windows
            return (int)msg.wParam;
        }

        /// <summary>
        /// The application state update method that is invoked within the main application loop.
        /// </summary>
        private static void RunApp()
        {
            // Update window title approx every second
            if (stopwatch.ElapsedMilliseconds - elapsedMillisecondsForLastTitleUpdate >= 1000)
            {
                string title = isPaused switch
                {
                    true => $"{application.Title} - Paused",
                    false => $"{application.Title} - FPS: {elapsedFramesSinceLastTitleUpdate}"
                };

                fixed (char* pTitle = title)
                {
                    _ = FX.SetWindowTextW(hwnd, (ushort*)pTitle);
                }

                elapsedMillisecondsForLastTitleUpdate = stopwatch.ElapsedMilliseconds;
                elapsedFramesSinceLastTitleUpdate = 0;
            }

            // Update the application state
            application.OnUpdate(stopwatch.Elapsed);
        }

        /// <summary>
        /// Processes incoming messages for a window. 
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="uMsg">The message.</param>
        /// <param name="wParam">Additional message information (the contents depend on the value of <paramref name="uMsg"/>).</param>
        /// <param name="lParam">Additional message information (the contents depend on the value of <paramref name="uMsg"/>).</param>
        /// <returns>The result of the message processing for the input message.</returns>
        [UnmanagedCallersOnly]
        private static nint WindowProc(IntPtr hwnd, uint uMsg, nuint wParam, nint lParam)
        {
            switch (uMsg)
            {
                // Change the paused state on window activation
                case FX.WM_ACTIVATE:
                {
                    if (FX.LOWORD(wParam) == FX.WA_INACTIVE)
                    {
                        isPaused = true;
                    }
                    else
                    {
                        isPaused = false;
                    }
                    return 0;
                }

                // Change the paused state when ESC is pressed
                case FX.WM_KEYDOWN:
                {
                    if ((ConsoleKey)wParam == ConsoleKey.Escape)
                    {

                        if (isPaused)
                        {
                                FX.SetCapture(Win32ApplicationRunner.hwnd);
                        }
                        else
                        {
                            FX.ReleaseCapture();
                        }

                        isPaused = !isPaused;
                    }

                    return 0;
                }

                // Window resize started
                case FX.WM_ENTERSIZEMOVE:
                {
                    isResizing = true;

                    return 0;
                }

                // Window resize completed
                case FX.WM_EXITSIZEMOVE:
                {
                    isResizing = false;
                    application.OnResize(windowSize);

                    return 0;
                }
                case FX.WM_SIZE:
                {
                    uint size = (uint)lParam;

                    windowSize = new Size(FX.LOWORD(size), FX.HIWORD(size));

                    if (!isResizing && wParam != FX.SIZE_MINIMIZED)
                    {
                        application.OnResize(windowSize);
                    }

                    return 0;
                }

                // Shutdown
                case FX.WM_DESTROY:
                {
                    FX.PostQuitMessage(0);
                    return 0;
                }
            }

            return FX.DefWindowProcW(hwnd, uMsg, wParam, lParam);
        }
    }
}