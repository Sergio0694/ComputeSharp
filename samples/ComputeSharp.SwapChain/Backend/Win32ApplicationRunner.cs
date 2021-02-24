using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.SwapChain.Backend
{
    /// <summary>
    /// A helper class to manage the creation and execution of Win32 applications.
    /// </summary>
    internal unsafe static class Win32ApplicationRunner
    {
        /// <summary>
        /// Whether or not a resize operation is in progress.
        /// </summary>
        private static bool isResizing = false;

        /// <summary>
        /// Whether or not the application is currently paused.
        /// </summary>
        private static bool isPaused = false;

        /// <summary>
        /// The <see cref="HWND"/> for the application window.
        /// </summary>
        private static HWND hwnd;

        /// <summary>
        /// The current size of the application window.
        /// </summary>
        private static Size windowSize;

        /// <summary>
        /// A <see cref="Stopwatch"/> instance used to track the elapsed time for the application.
        /// </summary>
        private static Stopwatch stopwatch = null!;

        /// <summary>
        /// The application being run.
        /// </summary>
        private static Win32Application application = null!;

        /// <summary>
        /// The number of elapsed milliseconds since the window title was last updated.
        /// </summary>
        private static long elapsedMillisecondsForLastTitleUpdate;

        /// <summary>
        /// The number of elapsed frames since the window title was last updated.
        /// </summary>
        private static int elapsedFramesSinceLastTitleUpdate;

        /// <summary>
        /// Runs a specified application and starts the main loop to update its state.
        /// This is the entry point for a given application of type <typeparamref name="T"/>, and it should be
        /// called as soon as the process is launched, excluding any other additional initialization needed.
        /// <para>To launch an application, simply add this line to the project being used:</para>
        /// <c>Win32ApplicationRunner.Run&lt;MyApplication>();</c>
        /// </summary>
        /// <typeparam name="T">The type of application being launched.</typeparam>
        /// <returns>The exit code for the application.</returns>
        public static int Run(Win32Application application)
        {
            Win32ApplicationRunner.application = application;

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

                MARGINS margins = default;
                margins.cxLeftWidth = -1;
                margins.cxRightWidth = -1;
                margins.cyTopHeight = -1;
                margins.cyBottomHeight = -1;

                _ = FX.DwmExtendFrameIntoClientArea(hwnd, &margins);
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

            elapsedFramesSinceLastTitleUpdate++;
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
                            FX.SetCapture(hwnd);
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

                // Size update
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

                // Size and position of the window (needed to enable the borderless mode)
                case FX.WM_NCCALCSIZE:
                {
                    return 0;
                }

                // Enable dragging the window
                case FX.WM_NCHITTEST:
                {
                    POINT point;
                    RECT rect;

                    _ = FX.GetCursorPos(&point);
                    _ = FX.GetWindowRect(hwnd, &rect);

                    bool
                        isAtTop = Math.Abs(point.y - rect.top) < 12,
                        isAtRight = Math.Abs(point.x - rect.right) < 12,
                        isAtBottom = Math.Abs(point.y - rect.bottom) < 12,
                        isAtLeft = Math.Abs(point.x - rect.left) < 12;

                    if (isAtTop)
                    {
                        if (isAtRight) return FX.HTTOPRIGHT;
                        if (isAtLeft) return FX.HTTOPLEFT;
                        return FX.HTTOP;
                    }

                    if (isAtRight)
                    {
                        if (isAtTop) return FX.HTTOPRIGHT;
                        if (isAtBottom) return FX.HTBOTTOMRIGHT;
                        return FX.HTRIGHT;
                    }

                    if (isAtBottom)
                    {
                        if (isAtRight) return FX.HTBOTTOMRIGHT;
                        if (isAtLeft) return FX.HTBOTTOMLEFT;
                        return FX.HTBOTTOM;
                    }

                    if (isAtLeft)
                    {
                        if (isAtTop) return FX.HTTOPLEFT;
                        if (isAtBottom) return FX.HTBOTTOMLEFT;
                        return FX.HTLEFT;
                    }

                    return FX.HTCAPTION;
                }

                // Restore the drop shadow
                case FX.WM_DWMCOMPOSITIONCHANGED:
                {
                    MARGINS margins = default;
                    margins.cxLeftWidth = -1;
                    margins.cxRightWidth = -1;
                    margins.cyTopHeight = -1;
                    margins.cyBottomHeight = -1;

                    _ = FX.DwmExtendFrameIntoClientArea(hwnd, &margins);

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