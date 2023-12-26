using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.Windows;

namespace ComputeSharp.SwapChain.D2D1.Backend;

using Windows = TerraFX.Interop.Windows.Windows;

/// <summary>
/// A helper class to manage the creation and execution of Win32 applications.
/// </summary>
internal static unsafe class Win32ApplicationRunner
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
    /// The application being run.
    /// </summary>
    private static Win32Application application = null!;

    /// <summary>
    /// The thread running the render loop.
    /// </summary>
    private static Thread renderThread = null!;

    /// <summary>
    /// Runs a specified application and starts the main loop to update its state. This is the entry point for a given application,
    /// and it should be called as soon as the process is launched, excluding any other additional initialization needed.
    /// </summary>
    /// <param name="application">The input application instance to run.</param>
    /// <param name="applicationName">Name of the input application instance to run.</param>
    /// <param name="windowTitle">The title to use for the window being opened.</param>
    /// <returns>The exit code for the application.</returns>
    public static int Run(Win32Application application, string applicationName, string windowTitle)
    {
        Win32ApplicationRunner.application = application;

        HMODULE hInstance = Windows.GetModuleHandleW(null);

        fixed (char* name = applicationName)
        fixed (char* title = windowTitle)
        {
            // Initialize the window class
            WNDCLASSEXW windowClassEx = new()
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                style = CS.CS_HREDRAW | CS.CS_VREDRAW,
                lpfnWndProc = &WindowProc,
                hInstance = hInstance,
                hCursor = Windows.LoadCursorW(HINSTANCE.NULL, IDC.IDC_ARROW),
                lpszClassName = name
            };

            // Register the window class
            _ = Windows.RegisterClassExW(&windowClassEx);

            Rectangle windowRect = new(0, 0, 1280, 720);

            // Set the target window size
            _ = Windows.AdjustWindowRect((RECT*)&windowRect, WS.WS_OVERLAPPEDWINDOW, Windows.FALSE);

            uint height = (uint)(windowRect.Bottom - windowRect.Top);
            uint width = (uint)(windowRect.Right - windowRect.Left);

            // Create the window and store a handle to it
            hwnd = Windows.CreateWindowExW(
                0,
                windowClassEx.lpszClassName,
                title,
                WS.WS_OVERLAPPEDWINDOW,
                Windows.CW_USEDEFAULT,
                Windows.CW_USEDEFAULT,
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

            _ = Windows.DwmExtendFrameIntoClientArea(hwnd, &margins);
        }

        // Initialize the application
        application.OnInitialize(hwnd);

        // Display the window
        _ = Windows.ShowWindow(hwnd, SW.SW_SHOWDEFAULT);

        MSG msg = default;

        // Setup the render thread that enables smooth resizing of the window
        renderThread = new Thread(static args =>
        {
            (Win32Application application, CancellationToken token) = ((Win32Application, CancellationToken))args!;

            Stopwatch stopwatch = Stopwatch.StartNew();

            while (!token.IsCancellationRequested)
            {
                application.OnUpdate(stopwatch.Elapsed);
            }
        });

        CancellationTokenSource tokenSource = new();

        renderThread.Start((application, tokenSource.Token));

        // Process any messages in the queue
        while (msg.message != WM.WM_QUIT)
        {
            if (Windows.PeekMessageW(&msg, HWND.NULL, 0, 0, PM.PM_REMOVE) != 0)
            {
                _ = Windows.TranslateMessage(&msg);
                _ = Windows.DispatchMessageW(&msg);
            }
            else if (isPaused)
            {
                Thread.Sleep(100);
            }

            // Also listen to Escape and 'Q' to quit
            if (msg.message == WM.WM_KEYUP && (msg.wParam == (WPARAM)VK.VK_ESCAPE || msg.wParam == 'Q'))
            {
                _ = Windows.DestroyWindow(hwnd);
            }
        }

        tokenSource.Cancel();

        renderThread.Join();

        // Return this part of the WM_QUIT message to Windows
        return (int)msg.wParam;
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
    private static LRESULT WindowProc(HWND hwnd, uint uMsg, WPARAM wParam, LPARAM lParam)
    {
        switch (uMsg)
        {
            // Change the paused state on window activation
            case WM.WM_ACTIVATE:
            {
                if (Windows.LOWORD(wParam) == Windows.WA_INACTIVE)
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
            case WM.WM_KEYDOWN:
            {
                if (wParam == (byte)ConsoleKey.Escape)
                {

                    if (isPaused)
                    {
                        _ = Windows.SetCapture(hwnd);
                    }
                    else
                    {
                        _ = Windows.ReleaseCapture();
                    }

                    isPaused = !isPaused;
                }

                return 0;
            }

            // Window resize started
            case WM.WM_ENTERSIZEMOVE:
            {
                isResizing = true;

                return 0;
            }

            // Window resize completed
            case WM.WM_EXITSIZEMOVE:
            {
                isResizing = false;
                application.OnResize();

                return 0;
            }

            // Size update
            case WM.WM_SIZE:
            {
                if (!isResizing && wParam != Windows.SIZE_MINIMIZED)
                {
                    application.OnResize();
                }

                return 0;
            }

            // Size and position of the window (needed to enable the borderless mode)
            case WM.WM_NCCALCSIZE:
            {
                return 0;
            }

            // Enable dragging the window
            case WM.WM_NCHITTEST:
            {
                POINT point;
                RECT rect;

                _ = Windows.GetCursorPos(&point);
                _ = Windows.GetWindowRect(hwnd, &rect);

                bool isAtTop = Math.Abs(point.y - rect.top) < 12;
                bool isAtRight = Math.Abs(point.x - rect.right) < 12;
                bool isAtBottom = Math.Abs(point.y - rect.bottom) < 12;
                bool isAtLeft = Math.Abs(point.x - rect.left) < 12;

                if (isAtTop)
                {
                    if (isAtRight)
                    {
                        return Windows.HTTOPRIGHT;
                    }

                    if (isAtLeft)
                    {
                        return Windows.HTTOPLEFT;
                    }

                    return Windows.HTTOP;
                }

                if (isAtRight)
                {
                    if (isAtTop)
                    {
                        return Windows.HTTOPRIGHT;
                    }

                    if (isAtBottom)
                    {
                        return Windows.HTBOTTOMRIGHT;
                    }

                    return Windows.HTRIGHT;
                }

                if (isAtBottom)
                {
                    if (isAtRight)
                    {
                        return Windows.HTBOTTOMRIGHT;
                    }

                    if (isAtLeft)
                    {
                        return Windows.HTBOTTOMLEFT;
                    }

                    return Windows.HTBOTTOM;
                }

                if (isAtLeft)
                {
                    if (isAtTop)
                    {
                        return Windows.HTTOPLEFT;
                    }

                    if (isAtBottom)
                    {
                        return Windows.HTBOTTOMLEFT;
                    }

                    return Windows.HTLEFT;
                }

                return Windows.HTCAPTION;
            }

            // Restore the drop shadow
            case WM.WM_DWMCOMPOSITIONCHANGED:
            {
                MARGINS margins = default;
                margins.cxLeftWidth = -1;
                margins.cxRightWidth = -1;
                margins.cyTopHeight = -1;
                margins.cyBottomHeight = -1;

                _ = Windows.DwmExtendFrameIntoClientArea(hwnd, &margins);

                return 0;
            }

            // Shutdown
            case WM.WM_DESTROY:
            {
                Windows.PostQuitMessage(0);

                return 0;
            }
        }

        return Windows.DefWindowProcW(hwnd, uMsg, wParam, lParam);
    }
}